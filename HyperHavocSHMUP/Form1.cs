using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace HyperHavocSHMUP
{
    public partial class Form1 : Form
    {
        System.Windows.Media.MediaPlayer titleMusic = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer gameMusic = new System.Windows.Media.MediaPlayer();

        public class Enemy
        {
            public Rectangle Body = new Rectangle();
            public int Damage = 1;
            public int Health = 1;
            public int Sprites = 1;
            public Image Sprite = Properties.Resources.enemy_8;
        }

        string state = "waiting";

        //player
        Rectangle maxNova = new Rectangle(100, 170, 55, 55);

        //enemy
        Rectangle baseEnemy = new Rectangle(500, 50, 40, 32);

        List<Rectangle> shootList = new List<Rectangle>();
        List<Enemy> enemyList = new List<Enemy>();

        //foreground buildings
        Rectangle background1 = new Rectangle(0, 200, 90, 600);
        Rectangle background2 = new Rectangle(95, 100, 90, 600);
        Rectangle background3 = new Rectangle(175, 400, 165, 700);
        Rectangle background4 = new Rectangle(260, 250, 80, 500);
        Rectangle background5 = new Rectangle(350, 220, 90, 500);
        Rectangle background6 = new Rectangle(400, 350, 190, 600);
        Rectangle background7 = new Rectangle(620, 150, 90, 500);
        Rectangle background8 = new Rectangle(710, 100, 80, 700);
        Rectangle background9 = new Rectangle(800, 300, 165, 700);
        Rectangle background10 = new Rectangle(1000, 250, 100, 700);
        Rectangle background11 = new Rectangle(1100, 200, 130, 700);

        //background buildings
        Rectangle background1_2 = new Rectangle(20, 150, 90, 800);
        Rectangle background2_2 = new Rectangle(155, 60, 90, 900);
        Rectangle background3_2 = new Rectangle(200, 120, 90, 800);
        Rectangle background4_2 = new Rectangle(320, 100, 180, 800);
        Rectangle background5_2 = new Rectangle(595, 15, 100, 800);
        Rectangle background6_2 = new Rectangle(650, 400, 700, 600);
        Rectangle background7_2 = new Rectangle(960, 150, 90, 800);

        //far buildings
        Rectangle background1_3 = new Rectangle(250, 200, 100, 700);
        Rectangle background2_3 = new Rectangle(520, 80, 100, 800);
        Rectangle background3_3 = new Rectangle(780, 200, 110, 800);

        Rectangle screenFilter = new Rectangle(-4, 0, 1204, 600);

        //brushes
        SolidBrush violetBrush = new SolidBrush(Color.BlueViolet);
        SolidBrush slateblueBrush = new SolidBrush(Color.DarkSlateBlue);
        SolidBrush indigoBrush = new SolidBrush(Color.Indigo);
        SolidBrush magentaBrush = new SolidBrush(Color.Magenta);
        SolidBrush bulletBrush = new SolidBrush(Color.Transparent);
        Font introFont = new Font("OCR A Extended", 16, FontStyle.Bold);
        //SolidBrush attackBrush = new SolidBrush(Color.DeepPink);

        int flightCounter = 0;
        int fireCounter = 0;
        int enemyDeathCounter = 0;


        int maxNovaSpeed = 10;
        int attackSpeed = 30;

        //movement bools
        bool aDown = false;
        bool dDown = false;
        bool wDown = false;
        bool sDown = false;
        bool enterDown = false;
        bool shiftDown = false;
        bool enemylife = true;

        //general image
        Image drawPlayer;
        //general enemy
        Image drawEnemy;
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
        Image enemy_1;
        //enemy attacks
        Image enemy_2;
        Image enemy_3;
        Image enemy_4;
        //enemy death
        Image enemy1;
        Image enemy2;
        Image enemy3;
        Image enemy4;
        Image enemy5;

        //filter sprite
        Image filter;

        //projectile sprite
        Image projectile;

        //animation arrays
        Image[] playerFlight = new Image[4];
        Image[] playerFire = new Image[3];
        Image[] playerIdle = new Image[3];
        Image[] enemyAttack = new Image[3];
        Image[] enemyDeath = new Image[5];

        //text array
        String[] introText = new String[]{"Welcome to the neon-lit streets of NeoHavoc City,", "a place where chaos and pulsating energy reign supreme.", "In the not-so-distant future,", "the world has transformed into a vibrant paradise where synthwave beats pump through every fibre of society."};
        
        int shootCooldown;
        public Form1()
        {
            InitializeComponent();

            Enemy newEnemy = new Enemy();

            shootCooldown = Convert.ToInt32(100 / gameTimer.Interval);

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

            //base animations
            playerIdle[0] = playerBase1;
            playerIdle[1] = playerBase2;
            playerIdle[2] = playerBase3;
            //attack animations
            playerFire[0] = shoot1;
            playerFire[1] = shoot2;
            playerFire[2] = shoot3;
            //movement animations
            playerFlight[0] = flight1;
            playerFlight[1] = flight2;
            playerFlight[2] = flight3;
            playerFlight[3] = flight4;

            //define enemy model
            enemy_1 = Properties.Resources.enemy_1;

            //define projectile
            projectile = Properties.Resources.projectile3;

            //define screen filter
            filter = Properties.Resources.crt1200x600;

            //eliminate null variable
            drawPlayer = playerBase1;

            titleMusic.Open(new Uri(Application.StartupPath + "/Resources/titlemusic.wav"));
            titleMusic.MediaEnded += new EventHandler(titleMusic_MediaEnded);
            titleMusic.Play();


        }
        
        private void titleMusic_MediaEnded(object sender, EventArgs e)
        {
            titleMusic.Stop();
            titleMusic.Play();
        }

        private void gameMusic_MediaEnded(object sender, EventArgs e)
        {
            gameMusic.Stop();
            gameMusic.Play();
        }

        public void InitializeGame()
        {

            gameMusic.Open(new Uri(Application.StartupPath + "/Resources/gameplaymusic.wav"));
            gameMusic.MediaEnded += new EventHandler(gameMusic_MediaEnded);

            titleMusic.Stop();
            gameMusic.Play();
            BackColor = Color.Plum;
            maxNova = new Rectangle(100, 170, 55, 55);


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

        public void InitializeIntro()
        {

            /*foreach (string intro in introText)
            {
                introLabel.Text += $"{intro}\n";
            }*/

            introTimer.Enabled = true;

            titleLabel.Visible = false;
            backLabel1.Visible = false;
            backLabel2.Visible =false;
            backLabel3.Visible = false;
            backLabel4.Visible = false;
            subtitleLabel.Visible = false;

            state = "intro";

            Refresh();

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
                //case Keys.RShiftKey:
                //    shiftDown = true;

                case Keys.Space:
                    if(state == "waiting" || state == "end")
                    {
                        InitializeIntro();
                    }
                    else if (state == "intro")
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
            if (dDown == true && maxNova.X < this.Width - maxNova.Width - 550)
            {
                maxNova.X += maxNovaSpeed;
                flightCounter++;

                if (flightCounter == 3)
                {
                    flightCounter = 0;
                }
            }
            if (aDown == true && maxNova.X > 25)
            {
                maxNova.X -= maxNovaSpeed;
                flightCounter++;

                if (flightCounter == 3)
                {
                    flightCounter = 0;
                }
            }
            if (wDown == true && maxNova.Y > 20)
            {
                maxNova.Y -= maxNovaSpeed;
                flightCounter++;

                if (flightCounter == 3)
                {
                    flightCounter = 0;
                }
            }
            if (sDown == true && maxNova.Y < this.Height - maxNova.Height - 20)
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

                if (shootCooldown == 0)
                {
                    shootList.Add(new Rectangle(maxNova.X + maxNova.Width, maxNova.Y + (maxNova.Height / 2), 13, 10));
                    shootCooldown = Convert.ToInt32(100 / gameTimer.Interval);
                }

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

                List<Rectangle> shootListTemp = new List<Rectangle>();
                List<Enemy> enemyListTemp = new List<Enemy>();

                foreach (Rectangle shoot in shootList)
                {
                    foreach (Enemy enemy in enemyList)
                    {
                        if (shoot.IntersectsWith(enemy.Body))
                        {
                            enemyDeathCounter++;
                            drawEnemy = enemy_1;

                            if (enemyDeathCounter == 5)
                            {

                            }
                        }
                        else
                        {
                            enemyListTemp.Add(enemy);
                        }
                    }
                }

                shootList = shootListTemp;

                int bullOp = 0;
                shootListTemp = new List<Rectangle>();
                foreach (Rectangle bullet in shootList)
                {
                    Rectangle bulletStorer = bullet;
                    bulletStorer.X += attackSpeed;
                    shootListTemp.Add(bulletStorer);
                    bullOp++;
                }

                shootList = shootListTemp;

                shootCooldown--;

                if (shootCooldown < 0)
                {
                    shootCooldown = 0;
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

            if (state == "intro")
            {
                int height = 50;
                foreach (string intro in introText)
                {
                    e.Graphics.DrawString(intro, introFont, magentaBrush, Convert.ToInt16((this.Width - (intro.Count<char>() * 16)) / 5), height);
                    height += introFont.Height + 5;
                }
            }

            if (state == "playing")
            {

                //far buildings
                e.Graphics.FillRectangle(indigoBrush, background1_3);
                e.Graphics.FillRectangle(indigoBrush, background2_3);
                e.Graphics.FillRectangle(indigoBrush, background3_3);

                //background buildings
                e.Graphics.FillRectangle(slateblueBrush, background1_2);
                e.Graphics.FillRectangle(slateblueBrush, background2_2);
                e.Graphics.FillRectangle(slateblueBrush, background3_2);
                e.Graphics.FillRectangle(slateblueBrush, background4_2);
                e.Graphics.FillRectangle(slateblueBrush, background5_2);
                e.Graphics.FillRectangle(slateblueBrush, background6_2);
                e.Graphics.FillRectangle(slateblueBrush, background7_2);

                //foreground buildings
                e.Graphics.FillRectangle(violetBrush, background1);
                e.Graphics.FillRectangle(violetBrush, background2);
                e.Graphics.FillRectangle(violetBrush, background3);
                e.Graphics.FillRectangle(violetBrush, background4);
                e.Graphics.FillRectangle(violetBrush, background5);
                e.Graphics.FillRectangle(violetBrush, background6);
                e.Graphics.FillRectangle(violetBrush, background7);
                e.Graphics.FillRectangle(violetBrush, background8);
                e.Graphics.FillRectangle(violetBrush, background9);
                e.Graphics.FillRectangle(violetBrush, background10);
                e.Graphics.FillRectangle(violetBrush, background11);


                if (enemylife == true)
                {
                    e.Graphics.DrawImage(enemy_1, baseEnemy);
                    drawEnemy = enemyDeath[enemyDeathCounter];
                }

                if (enterDown == true)
                {
                    drawPlayer = playerFire[fireCounter];
                }
                else
                {
                    drawPlayer = playerFlight[flightCounter];
                }

                e.Graphics.DrawImage(drawPlayer, maxNova);

            }

            foreach (Rectangle bullet in shootList)
            {
                e.Graphics.DrawImage(projectile, bullet);
                e.Graphics.FillRectangle(bulletBrush, bullet);
            }

            //drawing screen filter (keep at bottom)
            e.Graphics.DrawImage(filter, screenFilter);
        }

    }

}