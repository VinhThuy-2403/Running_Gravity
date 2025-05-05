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
using System.IO;
namespace Running_Gravity
{
    public partial class Game : Form
    {
        string highScoreFile = "highscore.txt";
        int gravity;
        int gravityValue = 8;
        int obstacleSpeed = 10;
        int score = 0;
        int highScore = 0;
        bool gameOver = false;
        SoundPlayer backgroundMusic;
        SoundPlayer GameOverSound;
        Random random = new Random();





        public Game()
        {
            InitializeComponent();
            backgroundMusic = new SoundPlayer(Properties.Resources.background_music);
            GameOverSound = new SoundPlayer(Properties.Resources.gameOverSound);
            backgroundMusic.PlayLooping(); // phát lặp vô hạn
            RestartGame();
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            lbScore.Text = "Score: " + score;
            lbHighScore.Text = "High Score: " + highScore;
            player.Top += gravity;
            if (score > highScore)
            {
                highScore = score;
                File.WriteAllText(highScoreFile, highScore.ToString());
            }
            if (player.Top > 292)
            {
                gravity = 0;
                player.Top = 292;
                player.Image = Properties.Resources.run_down0;
            }
            else if (player.Top < 41)
            {
                gravity = 0;
                player.Top = 41;
                player.Image = Properties.Resources.run_up0;
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "chuong_ngai_vat")
                {
                    x.Left -= obstacleSpeed;
                    if (x.Left < -100)
                    {
                        x.Left = random.Next(1000, 2800);
                        score++;
                    }

                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        gameTimer.Stop();
                        backgroundMusic.Stop(); // DỪNG NHẠC ở đây
                        GameOverSound.Play();
                        lbScore.Text += " Game Over!! Press \"R\" to Restart.";
                        gameOver = true;
                        // set the high score 
                        if (score > highScore)
                        {
                            highScore = score;
                        }
                    }
                }

                if (score > 5)
                {
                    obstacleSpeed = 15;
                    gravityValue = 12;
                }

                if (score > 10)
                {
                    obstacleSpeed = 20;
                    gravityValue = 15;
                }    
                if(score > 20)
                {
                    obstacleSpeed = 25;
                    gravityValue = 20;
                }
            }    

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (player.Top == 292)
                {
                    player.Top -= 10;
                    gravity = -gravityValue;
                }
                else if (player.Top == 41)
                {
                    player.Top += 10;
                    gravity = gravityValue;
                }
            }
            if (e.KeyCode == Keys.R && gameOver == true)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {
            lbScore.Parent = pictureBox1;
            lbHighScore.Parent = pictureBox2;
            lbHighScore.Top = 5;
            player.Location = new Point(150, 292);
            player.Image = Properties.Resources.run_down0;
            score = 0;
            gravityValue = 8;
            gravity = 0;
            obstacleSpeed = 10;
            backgroundMusic.PlayLooping();

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "chuong_ngai_vat")
                {
                    x.Left = random.Next(1000, 3800);
                }
            }
            if (File.Exists(highScoreFile))
            {
                int.TryParse(File.ReadAllText(highScoreFile), out highScore);
            }
            else
            {
                highScore = 0;
            }
            gameTimer.Start();
        }

        private void player_Click(object sender, EventArgs e)
        {

        }
    }
}
