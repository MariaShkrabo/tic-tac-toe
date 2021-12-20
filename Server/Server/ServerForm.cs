using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Server
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
        }

        private byte[] board; // представление игрового поля
        private Player[] players; // массив объектов, который будет содержать игроков
        private Thread[] playerThreads; // потоки для взаимодействия с клиентом
        private TcpListener listener; // создаём объект класса TcpListener, чтобы слушать соединение с клиентом
        private int currentPlayer; // переменная для отслеживания очереди игрока 
        private Thread getPlayers; // поток для получения клиентских подключений
        internal bool disconnected = false; // true если сервер не отвечает
        private string name; //имя сервера


        private void Form1_Load(object sender, EventArgs e)
        {
            board = new byte[9];
            players = new Player[2];
            playerThreads = new Thread[2];
            currentPlayer = 0;
            // принимаем соединения в другом потоке 
            getPlayers = new Thread(new ThreadStart(SetUp));
            getPlayers.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            disconnected = true;
            System.Environment.Exit(System.Environment.ExitCode);
        }
        // делегат, который позволяет вызывать метод DisplayMessage

        // в потоке, который создает и поддерживает графический интерфейс 

        private delegate void DisplayDelegate(string message);

        // Метод DisplayMessage устанавливает свойство Text 
        //displayTextBox потокобезопасным способом
        internal void DisplayMessage(string message)
        {
            // если изменение displayTextBox не является потокобезопасным
            if (displayTextBox.InvokeRequired)
            {
                // использовать унаследованный метод Invoke для выполнения DisplayMessage
                // через делегата 
                Invoke(new DisplayDelegate(DisplayMessage),
                new object[] { message });
            }
            else // чтобы изменить displayTextBox в текущем потоке
                displayTextBox.Text += message;
        }

        #region манипуляции для чтения имен противников

        private delegate void DisplayName1Delegate(string nameText);
    
        internal void DisplayName1(string nameText)
        {
            if (name1.InvokeRequired)
            { 
                Invoke(new DisplayName1Delegate(DisplayName1),
                new object[] { nameText});
            }
            else
                name1.Text = nameText;
        }

        private delegate void DisplayName2Delegate(string nameText);

        internal void DisplayName2(string nameText)
        {
            if (name2.InvokeRequired)
            {
                Invoke(new DisplayName2Delegate(DisplayName2),
                new object[] { nameText });
            }
            else
                name2.Text = nameText;
        }

        internal string readName1()
        {
            try
            {
                return name1.Text;
            }
            catch (InvalidOperationException)
            {
                return "Ошибка";
            }
            
        }

        internal string readName2()
        {
            try
            {
                return name2.Text;
            }
            catch (InvalidOperationException)
            {
                return "Ошибка";
            }
        }
        #endregion

        // принимаем соединения от 2-х игроков
        public void SetUp()
        {
            DisplayMessage("Ожидание подключения игроков...\r\n");

            //получаем имя сервера
            name = Dns.GetHostName();

            //преобразуем имя в адрес
            IPAddress localAddr = Dns.GetHostAddresses(name)[0];

            // создаем экземпляр класса TcpListener (сервер ждёт запроса по своему же адресу)
            listener = new TcpListener(localAddr, 50000);
            listener.Start();

            // принять первого игрока и запустить поток
            players[0] = new Player(listener.AcceptSocket(), this, 0);
            playerThreads[0] = new Thread(new ThreadStart(players[0].Run));
            playerThreads[0].Start();

            // принять второго игрока и запусти следующий поток
            players[1] = new Player(listener.AcceptSocket(), this, 1);
            playerThreads[1] = new Thread(new ThreadStart(players[1].Run));
            playerThreads[1].Start();

            // пусть первый игрок знает, что другой игрок подключился
            lock (players[0])
            {
                players[0].threadSuspended = false;
                Monitor.Pulse(players[0]); //разрешить действовать другому игроку?
            }
        }

        // определяем был ли ход корректным 
        public bool ValidMove(int location, int player)
        {
            // не даем другому потоку осуществить ход 
            lock (this)
            {
                // ждем, пока не настанет очередь текущего игрока 
                while (player != currentPlayer)
                    Monitor.Wait(this);
                 
                // если нужный квадрат не занят
                if (!IsOccupied(location))
                {
                    // устанавливаем на доске метку текущего игрока 
                    board[location] = (byte)(currentPlayer == 0 ? 'X' : 'O');

                    // устанавливаем currentPlayer как другого игрока
                    currentPlayer = (currentPlayer + 1) % 2;

                    // предупреждаем другого игрока о движении
                    players[currentPlayer].OtherPlayerMoved(location);

                    // предупреждаем другого игрока, что пора двигаться
                    Monitor.Pulse(this);
                    return true;
                }
                else
                    return false;
            }
        }

        // проверяем занят ли конкретный квадрат
        public bool IsOccupied(int location)
        {
            if (board[location] == 'X' || board[location] == 'O')
                return true;
            else
                return false;
        }

        public bool winSituation(byte playerNumber)
        {
            if (board[0] == players[playerNumber].mark && board[1] == players[playerNumber].mark && board[2] == players[playerNumber].mark
                || board[3] == players[playerNumber].mark && board[4] == players[playerNumber].mark && board[5] == players[playerNumber].mark
                || board[6] == players[playerNumber].mark && board[7] == players[playerNumber].mark && board[8] == players[playerNumber].mark
                || board[0] == players[playerNumber].mark && board[3] == players[playerNumber].mark && board[6] == players[playerNumber].mark
                || board[1] == players[playerNumber].mark && board[4] == players[playerNumber].mark && board[7] == players[playerNumber].mark
                || board[2] == players[playerNumber].mark && board[5] == players[playerNumber].mark && board[8] == players[playerNumber].mark
                || board[0] == players[playerNumber].mark && board[4] == players[playerNumber].mark && board[8] == players[playerNumber].mark
                || board[2] == players[playerNumber].mark && board[4] == players[playerNumber].mark && board[6] == players[playerNumber].mark)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string winCase(byte playerNumber)
        {
            if (board[0] == players[playerNumber].mark && board[1] == players[playerNumber].mark && board[2] == players[playerNumber].mark)
            {
                return "1";
            }
            else if (board[3] == players[playerNumber].mark && board[4] == players[playerNumber].mark && board[5] == players[playerNumber].mark)
            {
                return "2";
            }
            else if (board[6] == players[playerNumber].mark && board[7] == players[playerNumber].mark && board[8] == players[playerNumber].mark)
            {
                return "3";
            }
            else if (board[0] == players[playerNumber].mark && board[3] == players[playerNumber].mark && board[6] == players[playerNumber].mark)
            {
                return "4";
            }
            else if (board[1] == players[playerNumber].mark && board[4] == players[playerNumber].mark && board[7] == players[playerNumber].mark)
            {
                return "5";
            }
            else if (board[2] == players[playerNumber].mark && board[5] == players[playerNumber].mark && board[8] == players[playerNumber].mark)
            {
                return "6";
            }
            else if (board[0] == players[playerNumber].mark && board[4] == players[playerNumber].mark && board[8] == players[playerNumber].mark)
            {
                return "7";
            }
            else if (board[2] == players[playerNumber].mark && board[4] == players[playerNumber].mark && board[6] == players[playerNumber].mark)
            {
                return "8";
            }
            else
            {
                return "Ошибка";
            }

        }

        // определяем закончена ли игра 
        public bool GameOver()
        {
            //переменные для того, чтобы сохранить победный случай без конфликта потоков
            string winCaseFor0;
            string winCaseFor1;

            if (winSituation(0))
            {
                winCaseFor0 = winCase(0);
                players[0].condition = "победитель" + winCaseFor0;
                players[1].condition = "проигравший" + winCaseFor0;
                return true;
            }
            else if (winSituation(1))
            {
                winCaseFor1 = winCase(1);
                players[1].condition = "победитель" + winCaseFor1;
                players[0].condition = "проигравший" + winCaseFor1;
                return true;
            }
            else if ((board[0] == 'X' || board[0] == 'O') && (board[1] == 'X' || board[1] == 'O') && (board[2] == 'X' || board[2] == 'O') &&
                     (board[3] == 'X' || board[3] == 'O') && (board[4] == 'X' || board[4] == 'O') && (board[5] == 'X' || board[5] == 'O') &&
                     (board[6] == 'X' || board[6] == 'O') && (board[7] == 'X' || board[7] == 'O') && (board[8] == 'X' || board[8] == 'O'))
            {
                players[0].condition = "ничья";
                players[1].condition = "ничья";
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
