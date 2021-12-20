namespace TicTacToe
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.exit = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.you = new System.Windows.Forms.Label();
            this.opponent = new System.Windows.Forms.Label();
            this.markMessage = new System.Windows.Forms.Label();
            this.cell0 = new System.Windows.Forms.Button();
            this.cell1 = new System.Windows.Forms.Button();
            this.cell2 = new System.Windows.Forms.Button();
            this.cell4 = new System.Windows.Forms.Button();
            this.cell3 = new System.Windows.Forms.Button();
            this.cell5 = new System.Windows.Forms.Button();
            this.cell6 = new System.Windows.Forms.Button();
            this.cell7 = new System.Windows.Forms.Button();
            this.cell8 = new System.Windows.Forms.Button();
            this.currentName = new System.Windows.Forms.Label();
            this.otherPlayerName = new System.Windows.Forms.Label();
            this.queueInfo = new System.Windows.Forms.Label();
            this.winLine = new System.Windows.Forms.Label();
            this.winLine1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // exit
            // 
            this.exit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("exit.BackgroundImage")));
            this.exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exit.Location = new System.Drawing.Point(746, 16);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(50, 50);
            this.exit.TabIndex = 3;
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonHelp.BackgroundImage")));
            this.buttonHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonHelp.Location = new System.Drawing.Point(694, 16);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(50, 50);
            this.buttonHelp.TabIndex = 4;
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // you
            // 
            this.you.AutoSize = true;
            this.you.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.you.Location = new System.Drawing.Point(71, 23);
            this.you.Name = "you";
            this.you.Size = new System.Drawing.Size(66, 36);
            this.you.TabIndex = 14;
            this.you.Text = "Вы:";
            // 
            // opponent
            // 
            this.opponent.AutoSize = true;
            this.opponent.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.opponent.Location = new System.Drawing.Point(71, 76);
            this.opponent.Name = "opponent";
            this.opponent.Size = new System.Drawing.Size(179, 36);
            this.opponent.TabIndex = 15;
            this.opponent.Text = "Противник:";
            // 
            // markMessage
            // 
            this.markMessage.AutoSize = true;
            this.markMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.markMessage.Location = new System.Drawing.Point(286, 654);
            this.markMessage.Name = "markMessage";
            this.markMessage.Size = new System.Drawing.Size(241, 38);
            this.markMessage.TabIndex = 20;
            this.markMessage.Text = "Вы играете \"_\"";
            // 
            // cell0
            // 
            this.cell0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cell0.Font = new System.Drawing.Font("Lucida Calligraphy", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell0.ForeColor = System.Drawing.Color.Black;
            this.cell0.Location = new System.Drawing.Point(194, 226);
            this.cell0.Name = "cell0";
            this.cell0.Size = new System.Drawing.Size(130, 116);
            this.cell0.TabIndex = 21;
            this.cell0.UseVisualStyleBackColor = false;
            this.cell0.Click += new System.EventHandler(this.cell0_Click);
            // 
            // cell1
            // 
            this.cell1.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cell1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cell1.Font = new System.Drawing.Font("Lucida Calligraphy", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell1.Location = new System.Drawing.Point(339, 227);
            this.cell1.Name = "cell1";
            this.cell1.Size = new System.Drawing.Size(130, 116);
            this.cell1.TabIndex = 22;
            this.cell1.UseVisualStyleBackColor = false;
            this.cell1.Click += new System.EventHandler(this.cell1_Click);
            // 
            // cell2
            // 
            this.cell2.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cell2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cell2.Font = new System.Drawing.Font("Lucida Calligraphy", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell2.Location = new System.Drawing.Point(484, 226);
            this.cell2.Name = "cell2";
            this.cell2.Size = new System.Drawing.Size(130, 116);
            this.cell2.TabIndex = 23;
            this.cell2.UseVisualStyleBackColor = false;
            this.cell2.Click += new System.EventHandler(this.cell2_Click);
            // 
            // cell4
            // 
            this.cell4.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cell4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cell4.Font = new System.Drawing.Font("Lucida Calligraphy", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell4.Location = new System.Drawing.Point(339, 349);
            this.cell4.Name = "cell4";
            this.cell4.Size = new System.Drawing.Size(130, 116);
            this.cell4.TabIndex = 24;
            this.cell4.UseVisualStyleBackColor = false;
            this.cell4.Click += new System.EventHandler(this.cell4_Click);
            // 
            // cell3
            // 
            this.cell3.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cell3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cell3.Font = new System.Drawing.Font("Lucida Calligraphy", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell3.Location = new System.Drawing.Point(194, 349);
            this.cell3.Name = "cell3";
            this.cell3.Size = new System.Drawing.Size(130, 116);
            this.cell3.TabIndex = 24;
            this.cell3.UseVisualStyleBackColor = false;
            this.cell3.Click += new System.EventHandler(this.cell3_Click);
            // 
            // cell5
            // 
            this.cell5.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cell5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cell5.Font = new System.Drawing.Font("Lucida Calligraphy", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell5.Location = new System.Drawing.Point(484, 349);
            this.cell5.Name = "cell5";
            this.cell5.Size = new System.Drawing.Size(130, 116);
            this.cell5.TabIndex = 25;
            this.cell5.UseVisualStyleBackColor = false;
            this.cell5.Click += new System.EventHandler(this.cell5_Click);
            // 
            // cell6
            // 
            this.cell6.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cell6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cell6.Font = new System.Drawing.Font("Lucida Calligraphy", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell6.Location = new System.Drawing.Point(193, 476);
            this.cell6.Name = "cell6";
            this.cell6.Size = new System.Drawing.Size(130, 116);
            this.cell6.TabIndex = 26;
            this.cell6.UseVisualStyleBackColor = false;
            this.cell6.Click += new System.EventHandler(this.cell6_Click);
            // 
            // cell7
            // 
            this.cell7.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cell7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cell7.Font = new System.Drawing.Font("Lucida Calligraphy", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell7.Location = new System.Drawing.Point(339, 476);
            this.cell7.Name = "cell7";
            this.cell7.Size = new System.Drawing.Size(130, 116);
            this.cell7.TabIndex = 27;
            this.cell7.UseVisualStyleBackColor = false;
            this.cell7.Click += new System.EventHandler(this.cell7_Click);
            // 
            // cell8
            // 
            this.cell8.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.cell8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cell8.Font = new System.Drawing.Font("Lucida Calligraphy", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cell8.Location = new System.Drawing.Point(485, 478);
            this.cell8.Name = "cell8";
            this.cell8.Size = new System.Drawing.Size(130, 116);
            this.cell8.TabIndex = 28;
            this.cell8.UseVisualStyleBackColor = false;
            this.cell8.Click += new System.EventHandler(this.cell8_Click);
            // 
            // currentName
            // 
            this.currentName.AutoSize = true;
            this.currentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.currentName.Location = new System.Drawing.Point(145, 23);
            this.currentName.Name = "currentName";
            this.currentName.Size = new System.Drawing.Size(204, 36);
            this.currentName.TabIndex = 29;
            this.currentName.Text = "(имя игрока)";
            // 
            // otherPlayerName
            // 
            this.otherPlayerName.AutoSize = true;
            this.otherPlayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.otherPlayerName.Location = new System.Drawing.Point(259, 76);
            this.otherPlayerName.Name = "otherPlayerName";
            this.otherPlayerName.Size = new System.Drawing.Size(271, 36);
            this.otherPlayerName.TabIndex = 30;
            this.otherPlayerName.Text = "(имя противника)";
            // 
            // queueInfo
            // 
            this.queueInfo.AutoSize = true;
            this.queueInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.queueInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.queueInfo.Location = new System.Drawing.Point(71, 134);
            this.queueInfo.Name = "queueInfo";
            this.queueInfo.Size = new System.Drawing.Size(146, 38);
            this.queueInfo.TabIndex = 31;
            this.queueInfo.Text = "Ваш ход";
            // 
            // winLine
            // 
            this.winLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.winLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 4.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.winLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.winLine.Location = new System.Drawing.Point(166, 281);
            this.winLine.Name = "winLine";
            this.winLine.Size = new System.Drawing.Size(466, 8);
            this.winLine.TabIndex = 32;
            this.winLine.Text = "                                                                                 " +
    "               ";
            this.winLine.Visible = false;
            // 
            // winLine1
            // 
            this.winLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.winLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 4.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.winLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.winLine1.Location = new System.Drawing.Point(255, 206);
            this.winLine1.Name = "winLine1";
            this.winLine1.Size = new System.Drawing.Size(8, 403);
            this.winLine1.TabIndex = 33;
            this.winLine1.Text = "                                                                                 " +
    "               ";
            this.winLine1.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Location = new System.Drawing.Point(-1, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 777);
            this.panel1.TabIndex = 34;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Location = new System.Drawing.Point(806, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(10, 777);
            this.panel2.TabIndex = 35;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Location = new System.Drawing.Point(-1, 765);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(814, 10);
            this.panel3.TabIndex = 35;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.Location = new System.Drawing.Point(6, -1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(800, 10);
            this.panel4.TabIndex = 36;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BlanchedAlmond;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(812, 774);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.winLine1);
            this.Controls.Add(this.winLine);
            this.Controls.Add(this.queueInfo);
            this.Controls.Add(this.otherPlayerName);
            this.Controls.Add(this.currentName);
            this.Controls.Add(this.cell8);
            this.Controls.Add(this.cell7);
            this.Controls.Add(this.cell6);
            this.Controls.Add(this.cell5);
            this.Controls.Add(this.cell3);
            this.Controls.Add(this.cell4);
            this.Controls.Add(this.cell2);
            this.Controls.Add(this.cell1);
            this.Controls.Add(this.cell0);
            this.Controls.Add(this.markMessage);
            this.Controls.Add(this.opponent);
            this.Controls.Add(this.you);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainWindow_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Label you;
        private System.Windows.Forms.Label opponent;
        private System.Windows.Forms.Label markMessage;
        private System.Windows.Forms.Button cell0;
        private System.Windows.Forms.Button cell1;
        private System.Windows.Forms.Button cell2;
        private System.Windows.Forms.Button cell4;
        private System.Windows.Forms.Button cell3;
        private System.Windows.Forms.Button cell5;
        private System.Windows.Forms.Button cell6;
        private System.Windows.Forms.Button cell7;
        private System.Windows.Forms.Button cell8;
        private System.Windows.Forms.Label currentName;
        private System.Windows.Forms.Label otherPlayerName;
        private System.Windows.Forms.Label queueInfo;
        private System.Windows.Forms.Label winLine;
        private System.Windows.Forms.Label winLine1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
    }
}