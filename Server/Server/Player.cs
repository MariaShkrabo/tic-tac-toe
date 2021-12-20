using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Server
{
    public class Player
    {
        internal Socket connection; // сокет для приема соединения
        private NetworkStream socketStream; // данные из потока
        private ServerForm server; // ссылка на сервер 
        private BinaryWriter writer; // облегчает запись в поток
        private BinaryReader reader; // облегчает чтение из потока 
        private int number; // номер игрока 
        public char mark; // метка игрока на доске
        private string playerName;
        private string opponentName;
        internal bool threadSuspended = true; // если мы ждем второго игрока 
        public string condition;
        public string playAgain;

        // конструктор, требующий Socket, TicTacToeServerForm и int объекты в качесте аргументов

        public Player(Socket socket, ServerForm serverValue, int newNumber)
        {
            mark = (newNumber == 0 ? 'X' : 'O');
            connection = socket;
            server = serverValue;
            number = newNumber;
            playerName = "";
            opponentName = "...";
            condition = "";

            // создаём объект NetworkStream для сокета
            socketStream = new NetworkStream(connection);

            // создаём Streams для чтения/отправки байтов данных
            writer = new BinaryWriter(socketStream);
            reader = new BinaryReader(socketStream);
        }

        // оповещение о том, что другой игрок сделал ход
        public void OtherPlayerMoved(int location)
        {
            // сообщаем, что оппонент походил
            if (!server.GameOver())
            {
                writer.Write("Противник сделал ход.");
            }
            else
            {
                writer.Write(condition);
            }
            writer.Write(location); // отправляем информацию о ходе
        }


        // позволяет игрокам делать ходы и получать ходы от другого игрока 
        public void Run()
        {
            playerName = reader.ReadString();
            //если пользователь не указал имя, под которым он хочет играть, его имя будет - "Игрок - (метка, которой он играет)"
            if (playerName == "")
            {
                playerName = "Игрок - " + mark;
            }

            bool done = false;
            // отобразить на сервере, что соединение было установлено

            //записываем имена игроков в лэйблы
            if (server.readName1() == "name1")
            {
                server.DisplayName1(playerName);
            }
            else
            {
                server.DisplayName2(playerName);
            }

            // отправить клиенту метку текущего игрока
            writer.Write(mark);

            server.DisplayMessage("Игрок под именем " + playerName + " присоединился и ходит \""
                                             + (number == 0 ? 'X' : 'O') + "\".\r\n");

            // отправить клиенту имя текущего игрока
            writer.Write(playerName);

            // X должен ждать пока подключится другой игрок
            if (mark == 'X') 
            {
                writer.Write("Ожидаем второго игрока.");

                // ждем уведомления от сервера, что другой игрок подключился
                lock (this)
                {
                    while (threadSuspended)
                        Monitor.Wait(this);
                }
                writer.Write("Второй игрок подключился. Ваш ход.");
            }
            else
            {
                writer.Write("Вы подключились. Ход противника.");                 
            }


            while (opponentName == "name1" || opponentName == "name2" || opponentName == "...")
            {
                //проверяем какое из двух имен является именем оппонента
                if (server.readName1() == playerName)
                {
                    opponentName = server.readName2();
                }
                else
                {
                    opponentName = server.readName1();
                }
            }
            //отправляем имя оппонента клиенту
            writer.Write(opponentName);
            

            // играем
            while (!done)
            {
                // ждём, пока данные станут доступными 
                while (connection.Available == 0)
                {
                    Thread.Sleep(1000);

                    if (server.disconnected)
                    {
                        server.DisplayMessage("Что-то пошло не так... Игра закончена.");
                        return;
                    }
                }

                // получаем данные
                int location = reader.ReadInt32();
                                
                // если ход корректный, отобразим его на сервере
                if (server.ValidMove(location, number) && !server.GameOver())
                {
                    server.DisplayMessage(playerName + ": ход на ячейку № " + location + ".\r\n");
                    writer.Write("Ход был корректным.");
                }
                else if (server.GameOver())
                {
                    server.DisplayMessage(playerName + ": ход на ячейку № " + location + ".\r\n");
                    writer.Write(condition);
                }
                else // ход некорректный
                    writer.Write("Ход был некорректным. Попробуйте снова.");

                // если игра окончена, установливаем для параметра done значение true, чтобы выйти из цикла while 

                if (server.GameOver())
                {
                    if (condition == "победитель1" || condition == "победитель2" || condition == "победитель3" ||
                        condition == "победитель4" || condition == "победитель5" || condition == "победитель6" ||
                        condition == "победитель7" || condition == "победитель8")
                    {
                        server.DisplayMessage("Игрок " + playerName + " победил.");
                        writer.Write(condition);
                    }
                    else if (condition == "ничья")
                    {
                        server.DisplayMessage("Это ничья");
                        writer.Write(condition);
                    }

                    lock (this)
                    {
                        playAgain = reader.ReadString();
                        while (playAgain == null)
                            Monitor.Wait(this);
                    }
                   
                    if (playAgain == "Выход")
                    {
                        server.DisplayMessage("\r\nИгра окончена.\r\n");
                        done = true;
                    }
                }
            }

            // всё закрываем
            writer.Close();
            reader.Close();
            socketStream.Close();
            connection.Close();
        }
    }
}

