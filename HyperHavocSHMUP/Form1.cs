using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyperHavocSHMUP
{
    public partial class Form1 : Form
    {
        string state = "waiting";

        Rectangle maxNova = new Rectangle(21, 150, 40, 40);
        Rectangle background1 = new Rectangle(15, 200, 80, 200);
        Rectangle background2 = new Rectangle(95, 200, 80, 200);
        //Rectangle baseEnemy = new Rectangle(200, 150, 20, 20);
        Rectangle screenFilter = new Rectangle(0, 0, 798, 390);

        SolidBrush magentaBrush = new SolidBrush(Color.Magenta);
        SolidBrush slateblueBrush = new SolidBrush(Color.DarkSlateBlue);
        SolidBrush indigoBrush = new SolidBrush(Color.Indigo);

        int maxNovaSpeed = 8;
        int attackSpeed = 25;

        //movement bools
        bool aDown = false;
        bool dDown = false;
        bool wDown = false;
        bool sDown = false;

        //base player model
        Image playerBase1;
        Image playerBase2;
        Image playerBase3;
        //player attack
        Image shoot1;
        Image shoot2;
        Image shoot3;
        //player movement
        Image flight1;
        Image flight2;
        Image flight3;  
        Image flight4;
        //base enemy model
        //Image enemy1;

        //filter sprite
        Image filter;

        public Form1()
        {
            InitializeComponent();

            //define player model
            playerBase1 = Properties.Resources._0;
            playerBase2 = Properties.Resources._2;
            playerBase3 = Properties.Resources._3;
            //define player model attack animation
            shoot1 = Properties.Resources._1;
            shoot2 = Properties.Resources._5;
            shoot3 = Properties.Resources._4;
            //define player model movement
            flight1 = Properties.Resources._6;
            flight2 = Properties.Resources._8;
            flight3 = Properties.Resources._7;
            flight4 = Properties.Resources._9;

            //define enemy model
            //enemy1 = Properties.Resources.enemy0;

            //define screen filter
            filter = Properties.Resources.crt1200x600;

        }

        public void InitializeGame()
        {
            maxNova = new Rectangle(21, 150, 40, 40);
            background1 = new Rectangle(15, 200, 80, 200);
            // baseEnemy = new Rectangle(200, 150, 20, 20);

            //remove title screen
            titleLabel.Text = "";
            backLabel1.Text = "";
            backLabel2.Text = "";
            backLabel3.Text = "";
            backLabel4.Text = "";
            subtitleLabel.Text = "";

            //change game state and start engine
            state = "playing";
            gameTimer.Enabled = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;

                case Keys.Space:
                    if (state == "waiting" || state == "end")
                    {
                        InitializeGame();
                    }
                    break;
                case Keys.Escape:
                    if (state == "waiting" || state == "end")
                    {
                        Application.Exit();
                    }
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (dDown == true && maxNova.X < this.Width - maxNova.Width - 15)
            {
                maxNova.X += maxNovaSpeed;
            }

            if (aDown == true && maxNova.X > 15)
            {
                maxNova.X -= maxNovaSpeed;
            }

            if (wDown == true && maxNova.Y > 10)
            {
                maxNova.Y -= maxNovaSpeed;
            }

            if (sDown == true && maxNova.Y < this.Height - maxNova.Height - 10)
            {
                maxNova.Y += maxNovaSpeed;
            }

            //redraw the screen
            Refresh(); 
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            if (state == "waiting")
            {
                titleLabel.Text = "HYPER HAVOC";
                backLabel1.Text = "HYPER HAVOC";
                backLabel2.Text = "HYPER HAVOC";
                backLabel3.Text = "HYPER HAVOC";
                backLabel4.Text = "HYPER HAVOC";
                subtitleLabel.Text = "Press [Space] to Play  Press [Esc] to Exit";
            }


            if (state == "playing")
            {
                //e.Graphics.DrawImage(enemy1, baseEnemy);
                e.Graphics.FillRectangle(slateblueBrush, background1);
                e.Graphics.DrawImage(playerBase1, maxNova);


                
            }

            if (state == "playing" && wDown == true)
            {
                e.Graphics.DrawImage(flight2, maxNova);
            }
            if (state == "playing" && aDown == true)
            {
                e.Graphics.DrawImage(flight2, maxNova);
            }
            if (state == "playing" && dDown == true)
            {
                e.Graphics.DrawImage(flight2, maxNova);
            }

            //drawing screen filter (keep at bottom)
            e.Graphics.DrawImage(filter, screenFilter);

        }
    }
}

