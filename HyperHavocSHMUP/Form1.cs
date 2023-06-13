using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;

namespace HyperHavocSHMUP
{
    public partial class Form1 : Form
    {
        string state = "waiting";

        int flightCounter = 0;
        int fireCounter = 0;

        //player
        Rectangle maxNova = new Rectangle(100, 170, 40, 40);

        //foreground buildings
        Rectangle background1 = new Rectangle(0, 200, 90, 200);
        Rectangle background2 = new Rectangle(95, 100, 90, 300);
        Rectangle background3 = new Rectangle(175, 300, 165, 400);
        Rectangle background4 = new Rectangle(260, 250, 80, 200);
        Rectangle background5 = new Rectangle(350, 220, 90, 200);
        Rectangle background6 = new Rectangle(400, 300, 190, 300);
        Rectangle background7 = new Rectangle(620, 150, 90, 250);
        Rectangle background8 = new Rectangle(710, 100, 80, 400);

        //background buildings
        Rectangle background1_2 = new Rectangle(20, 150, 90, 250);
        Rectangle background2_2 = new Rectangle(155, 60, 90, 400);
        Rectangle background3_2 = new Rectangle(200, 120, 90, 400);
        Rectangle background4_2 = new Rectangle(320, 100, 180, 300);
        Rectangle background5_2 = new Rectangle(595, 15, 100, 400);

        //far buildings
        Rectangle background1_3 = new Rectangle(250, 200, 100, 300);
        Rectangle background2_3 = new Rectangle(520, 80, 100, 400);


        //Rectangle baseEnemy = new Rectangle(200, 150, 20, 20);
        Rectangle screenFilter = new Rectangle(-3, 0, 803, 390);

        //brushes
        SolidBrush violetBrush = new SolidBrush(Color.BlueViolet);
        SolidBrush slateblueBrush = new SolidBrush(Color.DarkSlateBlue);
        SolidBrush indigoBrush = new SolidBrush(Color.Indigo);

        int maxNovaSpeed = 10 ;
        int attackSpeed = 25;

        //movement bools
        bool aDown = false;
        bool dDown = false;
        bool wDown = false;
        bool sDown = false;
        bool enterDown = false;

        //general image
        Image drawPlayer;
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

        //animation arrays
        Image[] playerFlight = new Image[4];
        Image[] playerFire = new Image[3];
        Image[] playerIdle = new Image[3];

        public Form1()
        {
            InitializeComponent();

            //System.Windows.Media.MediaPlayer shmupMusic = new System.Windows.Media.MediaPlayer();

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

            playerIdle[0] = playerBase1;
            playerIdle[1] = playerBase2;
            playerIdle[2] = playerBase3;

            playerFire[0] = shoot1;
            playerFire[1] = shoot2;
            playerFire[2] = shoot3;

            playerFlight[0] = flight1;
            playerFlight[1] = flight2;
            playerFlight[2] = flight3;
            playerFlight[3] = flight4;

            //define enemy model
            //enemy1 = Properties.Resources.enemy0;

            //define screen filter
            filter = Properties.Resources.crt1200x600;

            //Eliminate null variable
            drawPlayer = playerBase1;
        }

        public void InitializeGame()
        {
            BackColor = Color.Plum;
            maxNova = new Rectangle(100, 170, 40, 40);
            //background1 = new Rectangle(5, 200, 90, 200);
            //background2 = new Rectangle(95, 100, 90, 300);
            //background3 = new Rectangle(185, 300, 165, 400);
            //background4 = new Rectangle(270, 250, 80, 200);

            //baseEnemy = new Rectangle(200, 150, 20, 20);

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
                case Keys.Enter:
                    enterDown = true;
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
                case Keys.Enter:
                    enterDown = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (dDown == true && maxNova.X < this.Width - maxNova.Width - 400)
            {
                maxNova.X += maxNovaSpeed;
                flightCounter++;

                if (flightCounter == 3)
                {
                    flightCounter = 0;
                }
            }
            if (aDown == true && maxNova.X > 15)
            {
                maxNova.X -= maxNovaSpeed;
                flightCounter++;

                if (flightCounter == 3)
                {
                    flightCounter = 0;
                }
            }
            if (wDown == true && maxNova.Y > 10)
            {
                maxNova.Y -= maxNovaSpeed;
                flightCounter++;

                if (flightCounter == 3)
                { 
                    flightCounter = 0;
                }
            }
            if (sDown == true && maxNova.Y < this.Height - maxNova.Height - 10)
            {
                maxNova.Y += maxNovaSpeed;
                drawPlayer = flight2;
                flightCounter++;

                if (flightCounter == 3)
                {
                    flightCounter = 0;
                }
            }
            if (enterDown == true)
            { 
                drawPlayer = shoot1;
                fireCounter++;
                //recoil
                if (maxNova.X > 15)
                {
                    maxNova.X--;
                }
                //reset animation loop
                if (fireCounter == 3)
                {
                    fireCounter = 0;
                }

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

                //far buildings
                e.Graphics.FillRectangle(indigoBrush, background1_3);
                e.Graphics.FillRectangle(indigoBrush, background2_3);

                //background buildings
                e.Graphics.FillRectangle(slateblueBrush, background1_2);
                e.Graphics.FillRectangle(slateblueBrush, background2_2);
                e.Graphics.FillRectangle(slateblueBrush, background3_2);
                e.Graphics.FillRectangle(slateblueBrush, background4_2);
                e.Graphics.FillRectangle(slateblueBrush, background5_2);

                //foreground buildings
                e.Graphics.FillRectangle(violetBrush, background1);
                e.Graphics.FillRectangle(violetBrush, background2);
                e.Graphics.FillRectangle(violetBrush, background3);
                e.Graphics.FillRectangle(violetBrush, background4); 
                e.Graphics.FillRectangle(violetBrush, background5);
                e.Graphics.FillRectangle(violetBrush, background6);
                e.Graphics.FillRectangle(violetBrush, background7); 
                e.Graphics.FillRectangle(violetBrush, background8);

                e.Graphics.DrawImage(drawPlayer, maxNova);
                drawPlayer = playerFlight[flightCounter];

                if (enterDown == true)
                { 
                    drawPlayer = playerFire[fireCounter];
                }

                
            }

            //drawing screen filter (keep at bottom)
            e.Graphics.DrawImage(filter, screenFilter);

        }
    }
}

