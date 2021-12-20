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
    public partial class SplashForm : Form
    {
        private Timer timer1;

        public SplashForm()
        {
            InitializeComponent();
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            bar.ForeColor = Color.FromArgb(212, 11, 4);
            bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            bar.Maximum = 150;
            bar.Step = 1;
            timer1 = new Timer();
            timer1.Interval = 25;
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            bar.PerformStep();
        }

        //функция для того, чтобы двигать форму курсором, так как FormBorderStyle = None
        private Point MouseHook;
        private void SplashForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) MouseHook = e.Location;
            Location = new Point((Size)Location - (Size)MouseHook + (Size)e.Location);
        }
    }
}
