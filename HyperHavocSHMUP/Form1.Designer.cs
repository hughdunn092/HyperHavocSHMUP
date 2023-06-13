namespace HyperHavocSHMUP
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.titleLabel = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.backLabel1 = new System.Windows.Forms.Label();
            this.backLabel2 = new System.Windows.Forms.Label();
            this.backLabel3 = new System.Windows.Forms.Label();
            this.backLabel4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.BackColor = System.Drawing.Color.Transparent;
            this.titleLabel.Font = new System.Drawing.Font("OCR A Extended", 48F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.Magenta;
            this.titleLabel.Location = new System.Drawing.Point(-1, 180);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1175, 99);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "HYPER HAVOC";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.subtitleLabel.Font = new System.Drawing.Font("OCR A Extended", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subtitleLabel.ForeColor = System.Drawing.Color.Magenta;
            this.subtitleLabel.Location = new System.Drawing.Point(4, 461);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(1173, 59);
            this.subtitleLabel.TabIndex = 1;
            this.subtitleLabel.Text = "Press [Space] to Play  Press [Esc] to Exit";
            this.subtitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backLabel1
            // 
            this.backLabel1.BackColor = System.Drawing.Color.Transparent;
            this.backLabel1.Font = new System.Drawing.Font("OCR A Extended", 40F, System.Drawing.FontStyle.Bold);
            this.backLabel1.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.backLabel1.Location = new System.Drawing.Point(4, 100);
            this.backLabel1.Name = "backLabel1";
            this.backLabel1.Size = new System.Drawing.Size(1160, 80);
            this.backLabel1.TabIndex = 2;
            this.backLabel1.Text = "HYPER HAVOC";
            this.backLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backLabel2
            // 
            this.backLabel2.BackColor = System.Drawing.Color.Transparent;
            this.backLabel2.Font = new System.Drawing.Font("OCR A Extended", 40F, System.Drawing.FontStyle.Bold);
            this.backLabel2.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.backLabel2.Location = new System.Drawing.Point(1, 269);
            this.backLabel2.Name = "backLabel2";
            this.backLabel2.Size = new System.Drawing.Size(1165, 77);
            this.backLabel2.TabIndex = 3;
            this.backLabel2.Text = "HYPER HAVOC";
            this.backLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backLabel3
            // 
            this.backLabel3.BackColor = System.Drawing.Color.Transparent;
            this.backLabel3.Font = new System.Drawing.Font("OCR A Extended", 32F, System.Drawing.FontStyle.Bold);
            this.backLabel3.ForeColor = System.Drawing.Color.Indigo;
            this.backLabel3.Location = new System.Drawing.Point(1, 346);
            this.backLabel3.Name = "backLabel3";
            this.backLabel3.Size = new System.Drawing.Size(1163, 77);
            this.backLabel3.TabIndex = 4;
            this.backLabel3.Text = "HYPER HAVOC";
            this.backLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backLabel4
            // 
            this.backLabel4.BackColor = System.Drawing.Color.Transparent;
            this.backLabel4.Font = new System.Drawing.Font("OCR A Extended", 32F, System.Drawing.FontStyle.Bold);
            this.backLabel4.ForeColor = System.Drawing.Color.Indigo;
            this.backLabel4.Location = new System.Drawing.Point(1, 35);
            this.backLabel4.Name = "backLabel4";
            this.backLabel4.Size = new System.Drawing.Size(1176, 77);
            this.backLabel4.TabIndex = 5;
            this.backLabel4.Text = "HYPER HAVOC";
            this.backLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.backLabel4);
            this.Controls.Add(this.backLabel3);
            this.Controls.Add(this.backLabel2);
            this.Controls.Add(this.backLabel1);
            this.Controls.Add(this.subtitleLabel);
            this.Controls.Add(this.titleLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "HYPER HAVOC";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label subtitleLabel;
        private System.Windows.Forms.Label backLabel1;
        private System.Windows.Forms.Label backLabel2;
        private System.Windows.Forms.Label backLabel3;
        private System.Windows.Forms.Label backLabel4;
    }
}

