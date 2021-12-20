using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Drawing.Drawing2D;
using System.Net;

namespace TicTacToe
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Square[,] board; // представление игрового поля
        private Square currentSquare; // квадрат, который выбирает игрок 
        private Thread outputThread; // поток для полученных от сервера данных
        private TcpClient connection; // клиент для установления соединения
        private NetworkStream stream; // данные из потока
        private BinaryWriter writer; // облегчает запись в поток
        private BinaryReader reader; // облегчает чтение из потока
        private char myMark; // метка игрока на доске
        private bool myTurn; // очередь этого игрока? 
        private bool done = false; // true когда игра заканчивается
        private string myName; //имя текущего игрока
        private string address;

        WinnerForm form = new WinnerForm();
        Help helper = new Help();

        private void MainWindow_Load(object sender, EventArgs e)
        {
            you.BackColor = Color.Transparent;
            opponent.BackColor = Color.Transparent;
            currentName.BackColor = Color.Transparent;
            otherPlayerName.BackColor = Color.Transparent;
            queueInfo.BackColor = Color.Transparent;
            markMessage.BackColor = Color.Transparent;

            queueInfo.Text = "";

            board = new Square[3, 3];

            // создать 9 квадратных объектов и разместить их на доске
            board[0, 0] = new Square(cell0, ' ', 0);
            board[0, 1] = new Square(cell1, ' ', 1);
            board[0, 2] = new Square(cell2, ' ', 2);
            board[1, 0] = new Square(cell3, ' ', 3);
            board[1, 1] = new Square(cell4, ' ', 4);
            board[1, 2] = new Square(cell5, ' ', 5);
            board[2, 0] = new Square(cell6, ' ', 6);
            board[2, 1] = new Square(cell7, ' ', 7);
            board[2, 2] = new Square(cell8, ' ', 8);

            //настраиваем стиль кнопок
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j].SquareButton.FlatAppearance.BorderSize = 0;
                    board[i, j].SquareButton.FlatStyle = FlatStyle.Flat;
                    board[i, j].SquareButton.BackColor = Color.Transparent;
                }
            }

            //подключиться к серверу и получить связанный
            // сетевой поток 
            try
            {
                //преобразуем dns-имя в ip-адрес
                address = Dns.GetHostAddresses(Greeting.serverName)[0].ToString();

                connection = new TcpClient(address, 50000);
                stream = connection.GetStream();
                writer = new BinaryWriter(stream);
                reader = new BinaryReader(stream);

                // запустить новый поток для отправки/получения сообщения
                outputThread = new Thread(new ThreadStart(Run));
                outputThread.Start();
                PaintSquares();
                writer.Write(Greeting.playerName);
            }
            catch (SocketException)
            {
                MessageBox.Show("Что-то пошло не так... Игра закончена.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChangeLabel("Что-то пошло не так... Игра закончена.", queueInfo);
            }                
        }

        //функция для того, чтобы двигать форму курсором, так как FormBorderStyle = None
        private Point MouseHook;

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) MouseHook = e.Location;
            Location = new Point((Size)Location - (Size)MouseHook + (Size)e.Location);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            done = true;
            System.Environment.Exit(System.Environment.ExitCode);
        }

        // управляющий поток, который позволяет непрерывно обновлять отображение игрового поля
        public void Run()
        {
            // сначала получить метку игрока (X или O) 
            myMark = reader.ReadChar();
            //потом имя игрока
            myName = reader.ReadString();

            ChangeLabel("Вы играете \"" + myMark + "\"", markMessage);
            ChangeLabel(myName, currentName);
            myTurn = (myMark == 'X' ? true : false);
           
            // обработка входящих сообщений 
            try
            {
                // получение сообщений, отправленных клиенту
                while (!done)
                    ProcessMessage(reader.ReadString());
            }  
            catch (IOException)
            {
                MessageBox.Show("Что-то пошло не так... Игра закончена.", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChangeLabel("Что-то пошло не так... Сервер не отвечает.", queueInfo);
            } 
        } 

        // обработка сообщений, отправленных клиенту
        public void ProcessMessage(string message)
        {
            // если ход, отправленный игроком на сервер, действителен
            // обновляем отображение, устанавливаем отметку квадрата равной
            // метке текущего игрока и перекрашиваем доску
            if (myTurn == false && message != "Ожидаем второго игрока.")
            {
                cellsEnabling(false);
                ChangeLabel("Ход противника", queueInfo);
            }            
            else if (message == "Ожидаем второго игрока.")
            {
                cellsEnabling(false);
                ChangeLabel("Ожидание подключения противника...", queueInfo);
            }
            else if (message == "Второй игрок подключился. Ваш ход.")
            {
                cellsEnabling(true);
                ChangeLabel("Ваш ход", queueInfo);
                ChangeLabel(reader.ReadString(), otherPlayerName);
            }
            else if (message == "Вы подключились. Ход противника.")
            {
                cellsEnabling(false);
                ChangeLabel("Ход противника", queueInfo);
                ChangeLabel(reader.ReadString(), otherPlayerName);
            }

            if (message == "Ход был корректным.")
            {
                ChangeLabel("Ход противника", queueInfo);
                currentSquare.Mark = myMark;
                PaintSquares();
            }
            else if (message == "Ход был некорректным. Попробуйте снова.")
            {
                // если ход недействителен, отобразить его, и теперь
                // снова ход этого игрока 
                ChangeLabel(" Что-то пошло не так...", queueInfo);
                myTurn = true;
            }
            else if (message == "Противник сделал ход.")
            {
                cellsEnabling(true);
                // если противник двинулся, найдите место его хода
                int location = reader.ReadInt32();

                // устанавливаем в этой клетке отметку оппонента 
                // перекрашиваем доску
                board[location / 3, location % 3].Mark = (myMark == 'X' ? 'O' : 'X');
                PaintSquares();

                ChangeLabel("Ваш ход", queueInfo);

                // очередь другого игрока 
                myTurn = true;
                // другой игрок походил, можно дать походить другому, разблокировав кнопки
                cellsEnabling(true);
            }

            if (message.Contains("победитель"))
            {
                ChangeLabel("Вы победили!", queueInfo);
                //решаем как именно зачеркивать клетки в зависимости от номера победной ситуации            
                lock (this)
                {
                    moveWinLine(message);
                }

                form.mainText = "Победа!";
                Application.Run(form);
            }
            if (message.Contains("проигравший"))
            {
                ChangeLabel("Вы проиграли(", queueInfo);
                lock (this)
                {
                    moveWinLine(message);
                }

                // устанавливаем в этой клетке отметку оппонента 
                // перекрашиваем доску
                int location = reader.ReadInt32();

                // устанавливаем в этой клетке отметку оппонента 
                // перекрашиваем доску
                board[location / 3, location % 3].Mark = (myMark == 'X' ? 'O' : 'X');
                PaintSquares();

                form.mainText = "Поражение(";
                Application.Run(form);
            }

            if (message == "ничья")
            {
                // устанавливаем в этой клетке отметку оппонента 
                // перекрашиваем доску
                PaintSquares();
                ChangeLabel("Ничья", queueInfo);
                form.mainText = "Ничья";
                Application.Run(form);
            }

            //для вывода имени противника
            if (message != "Ожидаем второго игрока." && message != "Второй игрок подключился. Ваш ход."
                && message != "Вы подключились. Ход противника." && message != "Ход был корректным."
                && message != "Ход был некорректным. Попробуйте снова." && message != "Противник сделал ход."
                && !message.Contains("победитель") && !message.Contains("проигравший") && message != "ничья" && message != "")
            {
                ChangeLabel(message, otherPlayerName);
            }

            if (form.endGame == "Выход")
            {
                done = true;
                writer.Write(form.endGame);
                this.Close();
            }            
        }
        

        public void moveWinLine(string message)
        {
            if (message.Contains("1"))
            {
                ChangeWinLine(true, winLine);
            }
            if (message.Contains("2"))
            {
                winLine.Location = new Point(135, 330);
                ChangeWinLine(true, winLine);
            }
            else if (message.Contains("3"))
            {
                winLine.Location = new Point(135, 430);
                ChangeWinLine(true, winLine);
            }
            else if (message.Contains("4"))
            {
                ChangeWinLine(true, winLine1);
            }
            else if (message.Contains("5"))
            {
                winLine.Location = new Point(135, 330);
                ChangeWinLine(true, winLine1);
            }
            else if (message.Contains("6"))
            {
                winLine.Location = new Point(135, 430);
                ChangeWinLine(true, winLine1);
            }
            else if (message.Contains("7"))
            {
                if (cell0.Text == "X")
                {
                    ChangeImage("D:\\BNTU\\2 курс\\КСиС\\Курсач\\gameFieldXwinnerDiagonal1.jpg");                  
                }
                else if (cell0.Text == "O")
                {
                    ChangeImage("D:\\BNTU\\2 курс\\КСиС\\Курсач\\gameFieldOwinnerDiagonal1.jpg");
                }
                board[0, 0].Mark = ' ';
                board[1, 1].Mark = ' ';
                board[2, 2].Mark = ' ';
                PaintSquares();
            }
            else if (message.Contains("8"))
            {
                if (cell2.Text == "X")
                {
                    ChangeImage("D:\\BNTU\\2 курс\\КСиС\\Курсач\\gameFieldXwinnerDiagonal2.jpg");
                }
                else if (cell2.Text == "O")
                {
                    ChangeImage("D:\\BNTU\\2 курс\\КСиС\\Курсач\\gameFieldOwinnerDiagonal2.jpg");
                }
                board[0, 2].Mark = ' ';
                board[1, 1].Mark = ' ';
                board[2, 0].Mark = ' ';
                PaintSquares();
            }
        }


        // отправить серверу номер выбранного квадрата 
        public void SendClickedSquare(int location)
        {
            if (location == -1)
            {
                writer.Write(location);
                done = true;
            }
            // если ход текущего игрока
            if (myTurn)
            {
                // отправить расположение хода серверу
                writer.Write(location);

                // очередь оппонента
                myTurn = false;
            }  
        } 

        // свойство только для записи для текущего квадрата
        public Square CurrentSquare
        {
            set
            {
                currentSquare = value;
            }
        }

        #region Делаем линию видимой/невидимой
        // делегат, который позволяет вызывать метод по изменению 
        // компонента label в потоке безопасным способом
        private delegate void winLineDelegate(bool enabling, Label line);

        // метод ChangeLabel устанавливает свойство label
        // потокобезопасным способом
        private void ChangeWinLine(bool enabling, Label line)
        {
            // если изменение desiredLabel не является потокобезопасным
            if (line.InvokeRequired)
            {
                // используем метод Invoke, чтобы
                //выполнить ChangeLabel с помощью делегата
                Invoke(new winLineDelegate(ChangeWinLine),
                new object[] { enabling, line });
            }
            else
                line.Visible = enabling;
        }
        #endregion

        #region Изменение фоновой картинки безопасным способом

        private delegate void imageDelegate(string pictureName);

        // метод ChangeLabel устанавливает свойство формы
        // потокобезопасным способом
        private void ChangeImage(string pictureName)
        {
            if (this.InvokeRequired)
            {
                // используем метод Invoke, чтобы
                //выполнить ChangeImage с помощью делегата
                Invoke(new imageDelegate(ChangeImage),
                new object[] { pictureName });
            }
            else
                this.BackgroundImage = Image.FromFile(@pictureName);
        }
        #endregion

        #region Изменение label безопасным способом

        // делегат, который позволяет вызывать метод по изменению 
        // компонента label в потоке безопасным способом
        private delegate void labelDelegate(string message, Label desiredLabel);

        // метод ChangeLabel устанавливает свойство label
        // потокобезопасным способом
        private void ChangeLabel(string message, Label desiredLabel)
        {
            // если изменение desiredLabel не является потокобезопасным
            if (desiredLabel.InvokeRequired)
            {
                // используем метод Invoke, чтобы
                //выполнить ChangeLabel с помощью делегата
                Invoke(new labelDelegate(ChangeLabel),
                new object[] { message, desiredLabel });
            }
            else
                desiredLabel.Text = message;
        }
        #endregion

        #region Делаем конопку доступной/недоступной безопасным способом

        // делегат, который позволяет безопасно сделать кнопку доступной/недоступной 
        private delegate void boardCellDelegate(bool enabling, byte row, byte column);

        // метод ChangeEnablingCell устанавливает свойство enabling
        // потокобезопасным способом
        private void ChangeEnablingCell(bool enabling, byte row, byte column)
        {
            // если изменение  не является потокобезопасным
            if (board[row,column].SquareButton.InvokeRequired)
            {
                // use inherited method Invoke to execute ChangeIdLabel 
                // via a delegate 
                Invoke(new boardCellDelegate(ChangeEnablingCell),
                new object[] { enabling, row, column });
            }
            else
                board[row, column].SquareButton.Enabled = enabling;
        }

        // делает кнопки доступными или недоступными для нажатия
        private void cellsEnabling(bool enabling)
        {
            for (byte i = 0; i < 3; i++)
            {
                for (byte j = 0; j < 3; j++)
                {
                    // кнопка, на которой уже есть метка в любом случае недоступна для нажатия
                    if (board[i, j].SquareButton.Text == "X" || board[i, j].SquareButton.Text == "O" && form.endGame != "Играть заново")
                    {
                        ChangeEnablingCell(false, i, j);
                    }
                    else
                    {
                        ChangeEnablingCell(enabling, i, j);
                    }
                }
            }
        }
        #endregion

        #region Меняем текст кнопки безопасным способом

        // делегат, который позволяет безопасно поменять текст кнопки 
        private delegate void paintBoardCellDelegate(byte row, byte column);

        // метод ChangeTextCell меняет текст
        // потокобезопасным способом
        private void ChangeTextCell(byte row, byte column)
        {
            // если изменение  не является потокобезопасным
            if (board[row, column].SquareButton.InvokeRequired)
            {
                Invoke(new paintBoardCellDelegate(ChangeTextCell),
                new object[] { row, column });
            }
            else 
            {
                    board[row, column].SquareButton.Text = board[row, column].Mark.ToString();
            }
        }

        // рисует отметку каждого квадрата (Х или О)
        public void PaintSquares()
        {
            for (byte i = 0; i < 3; i++)
            {
                for (byte j = 0; j < 3; j++)
                {
                    ChangeTextCell(i, j);
                }
            }
        }

        #endregion


        private void cell0_Click(object sender, EventArgs e)
        {
            board[0, 0].Mark = myMark;
            board[0, 0].Location = 0;
            CurrentSquare = board[0, 0];
            // отправить ход серверу 
            SendClickedSquare(board[0, 0].Location);
            PaintSquares();
            cellsEnabling(false);
        }

        private void cell1_Click(object sender, EventArgs e)
        {
            board[0, 1].Mark = myMark;
            board[0, 1].Location = 1;
            CurrentSquare = board[0, 1];
            // отправить ход серверу 
            SendClickedSquare(board[0, 1].Location);
            PaintSquares();
            cellsEnabling(false);
        }

        private void cell2_Click(object sender, EventArgs e)
        {
            board[0, 2].Mark = myMark;
            board[0, 2].Location = 2;
            CurrentSquare = board[0, 2];
            // отправить ход серверу 
            SendClickedSquare(board[0, 2].Location);
            PaintSquares();
            cellsEnabling(false);
        }

        private void cell3_Click(object sender, EventArgs e)
        {
            board[1, 0].Mark = myMark;
            board[1, 0].Location = 3;
            CurrentSquare = board[1, 0];
            // отправить ход серверу 
            SendClickedSquare(board[1, 0].Location);
            PaintSquares();
            cellsEnabling(false);
        }

        private void cell4_Click(object sender, EventArgs e)
        {
            board[1, 1].Mark = myMark;
            board[1, 1].Location = 4;
            CurrentSquare = board[1, 1];
            // отправить ход серверу 
            SendClickedSquare(board[1, 1].Location);
            PaintSquares();
            cellsEnabling(false);
        }

        private void cell5_Click(object sender, EventArgs e)
        {
            board[1, 2].Mark = myMark;
            board[1, 2].Location = 5;
            CurrentSquare = board[1, 2];
            // отправить ход серверу 
            SendClickedSquare(board[1, 2].Location);
            PaintSquares();
            cellsEnabling(false);
        }

        private void cell6_Click(object sender, EventArgs e)
        {
            board[2, 0].Mark = myMark;
            board[2, 0].Location = 6;
            CurrentSquare = board[2, 0];
            // отправить ход серверу 
            SendClickedSquare(board[2, 0].Location);
            PaintSquares();
            cellsEnabling(false);
        }

        private void cell7_Click(object sender, EventArgs e)
        {
            board[2, 1].Mark = myMark;
            board[2, 1].Location = 7;
            CurrentSquare = board[2, 1];
            // отправить ход серверу 
            SendClickedSquare(board[2, 1].Location);
            PaintSquares();
            cellsEnabling(false);
        }

        private void cell8_Click(object sender, EventArgs e)
        {
            board[2, 2].Mark = myMark;
            board[2, 2].Location = 8;
            CurrentSquare = board[2, 2];
            // отправить ход серверу 
            SendClickedSquare(board[2, 2].Location);
            PaintSquares();
            cellsEnabling(false);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            helper.Show();
        }
    }
}

