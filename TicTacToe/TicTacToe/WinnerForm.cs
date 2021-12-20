using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class WinnerForm : Form
    {
        public WinnerForm()
        {
            InitializeComponent();
        }
        public string mainText;
        public string endGame = "";

        private void WinnerForm_Load(object sender, EventArgs e)
        {
            winLoser.BackColor = Color.Transparent;
            ChangeText();
            string path = "";
            if (winLoser.Text == "Победа!")
            {
                path = "D:\\BNTU\\2 курс\\КСиС\\Курсач\\победа.jpg";
            }
            else if (winLoser.Text == "Поражение(")
            {
                path = "D:\\BNTU\\2 курс\\КСиС\\Курсач\\поражение.jpg";
            }
            else if (winLoser.Text == "Ничья")
            {
                path = "D:\\BNTU\\2 курс\\КСиС\\Курсач\\ничья.jpg";
            }
            ChangeImage(@path, pictureBox1);
            ChangeImage(@path, pictureBox2);
            ChangeImage(@path, pictureBox3);
        }


        private delegate void imageDelegate(string path, PictureBox pictureBox);

        // метод ChangeImage устанавливает свойство label
        // потокобезопасным способом
        private void ChangeImage(string path, PictureBox pictureBox)
        {
            // если изменение pictureBox не является потокобезопасным
            if (pictureBox.InvokeRequired)
            {
                // используем метод Invoke, чтобы
                //выполнить ChangeImage с помощью делегата
                Invoke(new imageDelegate(ChangeImage),
                new object[] { path, pictureBox });
            }
            else
            {
                pictureBox.Image = Image.FromFile(@path);
            }
        }



        // делегат, который позволяет вызывать метод по изменению 
        // компонента label в потоке безопасным способом
        private delegate void textDelegate();

        // метод ChangeLabel устанавливает свойство label
        // потокобезопасным способом
        private void ChangeText()
        {
            // если изменение desiredLabel не является потокобезопасным
            if (winLoser.InvokeRequired)
            {
                // используем метод Invoke, чтобы
                //выполнить ChangeLabel с помощью делегата
                Invoke(new textDelegate(ChangeText),
                new object[] { });
            }
            else
            {  
                winLoser.Text = mainText;
                //чтобы не смещалась надпись, так как тексты имеют разное количество букв
                if (mainText == "Победа!" || mainText == "Ничья")
                    winLoser.Location = new Point(90, 26);
                else 
                    winLoser.Location = new Point(65, 26);
            }
        }

        //функция для того, чтобы двигать форму курсором, так как FormBorderStyle = None
        private Point MouseHook;
        private void WinnerForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) MouseHook = e.Location;
            Location = new Point((Size)Location - (Size)MouseHook + (Size)e.Location);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            endGame = "Выход";
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            endGame = "Играть заново";
            this.Close();
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.Image = Image.FromFile(@"D:\\BNTU\\2 курс\\КСиС\\Курсач\\wooddark.jpg");
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Image = Image.FromFile(@"D:\\BNTU\\2 курс\\КСиС\\Курсач\\для кнопки.jpg");
        }
    }
}
