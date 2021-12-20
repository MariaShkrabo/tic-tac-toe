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

namespace TicTacToe
{
    public partial class Greeting : Form
    {
        public Greeting()
        {
            Thread splashThread = new Thread(new ThreadStart(StartSplashForm));
            splashThread.Start();
            Thread.Sleep(5000);
            splashThread.Abort();
            InitializeComponent();
        }

        public static string playerName;
        public static string serverName;

        private void StartSplashForm()
        {
            Application.Run(new SplashForm());
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enterTheGame_Click(object sender, EventArgs e)
        {
            playerName = textBox1.Text;
            //принимаем имя сервера
            serverName = AddressTextBox.Text;
            this.Hide();
            MainWindow mainform = new MainWindow();
            mainform.Show();   
        }

        //функция для того, чтобы двигать форму курсором, так как FormBorderStyle = None
        private Point MouseHook;
        private void Greeting_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) MouseHook = e.Location;
            Location = new Point((Size)Location - (Size)MouseHook + (Size)e.Location);
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Help helper = new Help();
            helper.Show();
        }
    }
}
