﻿namespace _2048WinFormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            scoreLabel = new Label();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            правилаToolStripMenuItem = new ToolStripMenuItem();
            рестартToolStripMenuItem = new ToolStripMenuItem();
            выходToolStripMenuItem = new ToolStripMenuItem();
            показатьРезультатыToolStripMenuItem = new ToolStripMenuItem();
            bestScoreLabel = new Label();
            label3 = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 76);
            label1.Name = "label1";
            label1.Size = new Size(71, 32);
            label1.TabIndex = 0;
            label1.Text = "Счет:";
            // 
            // scoreLabel
            // 
            scoreLabel.AutoSize = true;
            scoreLabel.Location = new Point(113, 76);
            scoreLabel.Name = "scoreLabel";
            scoreLabel.Size = new Size(27, 32);
            scoreLabel.TabIndex = 1;
            scoreLabel.Text = "0";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, показатьРезультатыToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(645, 40);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { правилаToolStripMenuItem, рестартToolStripMenuItem, выходToolStripMenuItem });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(90, 36);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // правилаToolStripMenuItem
            // 
            правилаToolStripMenuItem.Name = "правилаToolStripMenuItem";
            правилаToolStripMenuItem.Size = new Size(242, 44);
            правилаToolStripMenuItem.Text = "Правила";
            правилаToolStripMenuItem.Click += правилаToolStripMenuItem_Click;
            // 
            // рестартToolStripMenuItem
            // 
            рестартToolStripMenuItem.Name = "рестартToolStripMenuItem";
            рестартToolStripMenuItem.Size = new Size(242, 44);
            рестартToolStripMenuItem.Text = "Рестарт";
            рестартToolStripMenuItem.Click += рестартToolStripMenuItem_Click;
            // 
            // выходToolStripMenuItem
            // 
            выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            выходToolStripMenuItem.Size = new Size(242, 44);
            выходToolStripMenuItem.Text = "Выход";
            выходToolStripMenuItem.Click += выходToolStripMenuItem_Click;
            // 
            // показатьРезультатыToolStripMenuItem
            // 
            показатьРезультатыToolStripMenuItem.Name = "показатьРезультатыToolStripMenuItem";
            показатьРезультатыToolStripMenuItem.Size = new Size(272, 36);
            показатьРезультатыToolStripMenuItem.Text = "Показать результаты ";
            показатьРезультатыToolStripMenuItem.Click += показатьРезультатыToolStripMenuItem_Click;
            // 
            // bestScoreLabel
            // 
            bestScoreLabel.AutoSize = true;
            bestScoreLabel.Location = new Point(432, 76);
            bestScoreLabel.Name = "bestScoreLabel";
            bestScoreLabel.Size = new Size(27, 32);
            bestScoreLabel.TabIndex = 4;
            bestScoreLabel.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(192, 76);
            label3.Name = "label3";
            label3.Size = new Size(210, 32);
            label3.TabIndex = 3;
            label3.Text = "Лучший резултат:";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(645, 784);
            Controls.Add(bestScoreLabel);
            Controls.Add(label3);
            Controls.Add(scoreLabel);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "2048";
            Load += MainForm_Load;
            KeyDown += MainForm_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label scoreLabel;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem правилаToolStripMenuItem;
        private ToolStripMenuItem рестартToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem показатьРезультатыToolStripMenuItem;
        private Label bestScoreLabel;
        private Label label3;
    }
}
