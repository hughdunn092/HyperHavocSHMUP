using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HyperHavocSHMUP
{
    public partial class Form1 : Form
    {
        System.Windows.Media.MediaPlayer titleMusic = new System.Windows.Media.MediaPlayer();
        System.Windows.Media.MediaPlayer gameMusic = new System.Windows.Media.MediaPlayer();


        public class Enemy
        {
            public Rectangle Body = new Rectangle();
            public int Damage, Health, Sprites;
            public String State;
            public Image Sprite = Properties.Resources.enemy_1;
        }

        string state = "waiting";

        //player
        Rectangle maxNova = new Rectangle(100, 170, 55, 55);

        //enemy
        Rectangle baseEnemy = new Rectangle(500, 50, 40, 32);

        List<Rectangle> shootList = new List<Rectangle>();
        List<Enemy> enemyList = new List<Enemy>();

        #region Rectangles

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
        #endregion

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
        int enemySpeed = 0;

        Random randGen = new Random();
        int randValue = 0;

        //movement bools
        bool aDown = false;
        bool dDown = false;
        bool wDown = false;
        bool sDown = false;
        bool enterDown = false;

        #region Images
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
        #endregion

        #region Sprite Animations
        //animation arrays
        Image[] playerFlight = new Image[4];
        Image[] playerFire = new Image[3];
        Image[] playerIdle = new Image[3];
        Image[] enemyAttack = new Image[3];
        Image[] enemyDeath = new Image[5];
        #endregion

        //text array
        String[] introText = new String[] { "Welcome to the neon-lit streets of NeoHavoc City,", "a place where chaos and pulsating energy reign supreme.", "In the not-so-distant future,", "the world has transformed into a vibrant paradise", "where synthwave beats pump through every fibre of society." };
        String[] introText2 = new String[] { "The evil AI Overmind, a malevolent force lurking within the heart of the cyberspace network, has unleashed a legion of rogue programs.", "These digital minions, aptly named 'Glitchers' are wreaking havoc, corrupting everything in their path.", "With your trusty mech, the \"Cosmic Crusher,\" you embark on a righteous quest to bring peace and restore order to NeoHavoc City." };
        String[] introText3 = new String[] { "You are Max Nova,", "an unlikely hero armed with a passion for retro gaming and an impressive arsenal of pixelated firepower.", "As the city plunges into a vortex of psychedelic mayhem,", "it's up to you to save the day, one groovy bullet at a time." };

        int shootCooldown;
        public Form1()
        {
            InitializeComponent();

            Enemy newEnemy = new Enemy();

            newEnemy.Body = new Rectangle(400, 50 + (50 * 0), 40, 35);
            newEnemy.Sprites = 0;
            newEnemy.Sprite = Properties.Resources.enemy_1;
            newEnemy.Health = 5;

            enemyList.Add(newEnemy);
            enemyList[0].Sprite = Properties.Resources.enemy_1;


            shootCooldown = Convert.ToInt32(100 / gameTimer.Interval);

            #region Sprites
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
            //define enemy death sprites
            enemy1 = Properties.Resources.enemy1;
            enemy2 = Properties.Resources.enemy2;
            enemy3 = Properties.Resources.enemy3;
            enemy4 = Properties.Resources.enemy4;
            enemy5 = Properties.Resources.enemy5;
            //define enemy shoot sprites
            enemy_2 = Properties.Resources.enemy_2;
            enemy_3 = Properties.Resources.enemy_3;
            enemy_4 = Properties.Resources.enemy_4;


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

            //define enemy death animation
            enemyDeath[0] = enemy1;
            enemyDeath[1] = enemy2;
            enemyDeath[2] = enemy3;
            enemyDeath[3] = enemy4;
            enemyDeath[4] = enemy5;
            //define enemy shoot animations
            enemyAttack[0] = enemy_2;
            enemyAttack[1] = enemy_3;
            enemyAttack[2] = enemy_4;

            //define projectile
            projectile = Properties.Resources.projectile3;

            //define screen filter
            filter = Properties.Resources.crt1200x600;

            //eliminate null variable
            drawPlayer = playerBase1;
            #endregion

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


            baseEnemy = new Rectangle(200, 150, 20, 20);

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
            introTimer.Enabled = true;

            titleLabel.Visible = false;
            backLabel1.Visible = false;
            backLabel2.Visible = false;
            backLabel3.Visible = false;
            backLabel4.Visible = false;
            subtitleLabel.Text = "Press [Space] to Continue";



            state = "intro1";

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

                case Keys.Space:
                    if (state == "waiting" || state == "end")
                    {
                        InitializeIntro();
                    }
                    else if (state == "intro1")
                    {
                        state = "intro2";
                        Refresh();
                    }
                    else if (state == "intro2")
                    {
                        state = "intro3";
                        Refresh();
                    }
                    else if (state == "intro3")
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
            }



            List<Rectangle> shootListTemp = new List<Rectangle>();


            foreach (Rectangle shoot in shootList)
            {
                if (enemyList.Count > 0)
                {
                    foreach (Enemy enemy in enemyList)
                    {
                        if (shoot.IntersectsWith(enemy.Body))
                        {
                            enemy.Health--;

                            if (enemy.Health == 0 && enemy.State != "Death")
                            {
                                enemy.State = "Death";
                                enemy.Sprites = -1;
                            }
                            if (enemy.Sprites < 5 && enemy.State == "Death")
                            {
                                //enemyListTemp.Add(enemy);


                            }
                        }
                        else
                        {
                            shootListTemp.Add(shoot);

                        }
                    }
                }
                else
                {
                    shootListTemp.Add(shoot);
                }
            }

            shootList.Clear();
            for (int i = 0; i < shootListTemp.Count; i++)
            {
                shootList.Add(shootListTemp[i]);
            }

            foreach (Rectangle shootLoc in shootList)
            {
                for (int i = 0; i < shootListTemp.Count; i++)
                {
                    if (shootListTemp[i] == shootLoc)
                    {
                        shootListTemp.RemoveAt(i);
                        if (i != shootListTemp.Count)
                        {
                            i--;
                        }
                    }
                }
                shootListTemp.Add(shootLoc);
            }

            shootList = shootListTemp;


            for (int i = 0; i < shootList.Count; i++)
            {
                int x = shootList[i].X + attackSpeed;
                shootList[i] = new Rectangle(x, shootList[i].Y, shootList[i].Width, shootList[i].Height);
            }

            shootCooldown--;

            if (shootCooldown < 0)
            {
                shootCooldown = 0;
            }

            foreach (Enemy enemy in enemyList)
            {
                enemy.Sprites++;
                switch (enemy.State)
                {
                    case "Move":
                        enemy.Sprites = 0;
                        break;
                    case "Shoot":
                        if (enemy.Sprites > 3)
                        {
                            enemy.Sprites = 0;
                        }
                        break;
                    default:
                        break;
                }


            }



            if (enemyList.Count > 0)
            {
                foreach (Enemy enemy in enemyList)
                {
                    switch (enemy.State)
                    {
                        case "Move":
                            enemy.Sprite = Properties.Resources.enemy_1;
                            break;
                        case "Shoot":
                            enemy.Sprite = enemyAttack[enemy.Sprites];
                            break;
                        case "Death":
                            if (enemy.Sprites == 5)
                            {
                                enemyList.Remove(enemy);
                                // enemy.Sprites = 0;
                                return;
                            }
                            else
                            {
                                enemy.Sprite = enemyDeath[enemy.Sprites];
                                break;
                            }
                    }
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

            if (state == "intro1")
            {
                int height = 50;
                foreach (string intro in introText)
                {
                    e.Graphics.DrawString(intro, introFont, magentaBrush, Convert.ToInt16((this.Width - (intro.Count<char>() * 18)) / 6), height);
                    height += introFont.Height + 5;
                }
            }
            else if (state == "intro2")
            {
                int height = 50;
                foreach (string intro2 in introText2)
                {
                    e.Graphics.DrawString(intro2, introFont, magentaBrush, Convert.ToInt16((this.Width - (intro2.Count<char>() * 16)) / 6), height);
                    height += introFont.Height + 5;
                }
            }
            else if (state == "intro3")
            {
                int height = 50;
                foreach (string intro3 in introText3)
                {
                    e.Graphics.DrawString(intro3, introFont, magentaBrush, Convert.ToInt16((this.Width - (intro3.Count<char>() * 16)) / 6), height);
                    height += introFont.Height + 5;
                }
            }

            if (state == "playing")
            {
                #region Drawing Rectangles
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
                #endregion


                if (enemyList.Count > 0)
                {
                    foreach (Enemy enemy in enemyList)
                    {
                        e.Graphics.DrawImage(enemy.Sprite, enemy.Body);
                    }
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


//NECESSARY CODE
//DO NOT REMOVE
//https://www.youtube.com/watch?v=dQw4w9WgXcQ