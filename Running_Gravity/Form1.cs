﻿using System;
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
        int gravityValue;
        int obstacleSpeed;
        int score = 0;
        int highScore = 0;
        bool gameOver = false;
        private bool isPaused = false;
        // biến đánh dấu tăng chỉ số 1 lần duy nhất tại mốc 5đ và 10đ
        bool boostedAt5 = false;
        bool boostedAt10 = false;
        SoundPlayer backgroundMusic;
        SoundPlayer GameOverSound;
        Random random = new Random();
        private bool isMusicOn;
        PictureBox jet;
        Timer jetMoveTimer = new Timer();
        Timer jetShootTimer = new Timer();
        int jetDirection = 1;
        int jetSpeed = 3;
        private int jetMoveChangeCounter = 0;

        public Game(SoundPlayer sharedMusic, bool musicOn, int gravityValue1, int obstacleSpeed1, int jetShootInterval)
        {
            InitializeComponent();
            backgroundMusic = sharedMusic;
            isMusicOn = musicOn;
            gravityValue = gravityValue1;
            obstacleSpeed = obstacleSpeed1;
            jetShootTimer.Interval = jetShootInterval;
            GameOverSound = new SoundPlayer(Properties.Resources.gameOverSound);
            if (isMusicOn)
                backgroundMusic.PlayLooping();
            else
                backgroundMusic.Stop(); 
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
                        GameOver();
                    }
                }

                if (x is PictureBox && (string)x.Tag == "enemy_bullet")
                {
                    x.Left -= 15;
                    if (x.Right < 0)
                    {
                        this.Controls.Remove(x);
                        x.Dispose();
                    }

                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        GameOver();
                    }
                }
            }
            if(score >= 5 && !boostedAt5)
{
                obstacleSpeed += 3;
                gravityValue += 3;
                boostedAt5 = true;
            }

            if (score >= 10 && !boostedAt10)
            {
                obstacleSpeed += 5;
                gravityValue += 5;
                boostedAt10 = true;
            }
        }

        private void JetMoveTimer_Tick(object sender, EventArgs e)
        {
            jet.Top += jetSpeed * jetDirection;

            // Giới hạn phạm vi
            if (jet.Top <= 48)
            {
                jet.Top = 48;
                jetDirection = 1;
            }
            else if (jet.Top >= 272)
            {
                jet.Top = 272;
                jetDirection = -1;
            }

            // Thay đổi hướng ngẫu nhiên sau mỗi vài tick
            jetMoveChangeCounter++;
            if (jetMoveChangeCounter >= 30) // sau 600ms nếu Interval là 20ms
            {
                jetMoveChangeCounter = 0;
                jetDirection = random.Next(0, 2) == 0 ? -1 : 1;
            }
        }

        private void JetShootTimer_Tick(object sender, EventArgs e)
        {
            PictureBox bullet = new PictureBox();
            bullet.Size = new Size(20, 5);
            bullet.BackColor = Color.Yellow;
            bullet.Top = jet.Top + jet.Height / 2;
            bullet.Left = jet.Left - 10;
            bullet.Tag = "enemy_bullet";
            this.Controls.Add(bullet);
            bullet.BringToFront();
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (gravity == 0)
                {
                    if (player.Top >= 292)
                    {
                        player.Top -= 10;
                        gravity = -gravityValue;
                        player.Image = Properties.Resources.run_up0;
                    }
                    else if (player.Top <= 41)
                    {
                        player.Top += 10;
                        gravity = gravityValue;
                        player.Image = Properties.Resources.run_down0;
                    }
                }
                else
                {
                    gravity = -gravity;
                    if (gravity < 0)
                        player.Image = Properties.Resources.run_up0;
                    else
                        player.Image = Properties.Resources.run_down0;
                }
            }

            if (e.KeyCode == Keys.R && gameOver == true)
            {
                RestartGame();
            }
            // Tạm dừng và tiếp tục trò chơi bằng phím enter
            if(e.KeyCode == Keys.Enter && gameOver == false)
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    gameTimer.Stop();
                    jetMoveTimer.Stop();
                    jetShootTimer.Stop();
                    backgroundMusic.Stop();
                }
                else
                {
                    gameTimer.Start();
                    jetMoveTimer.Start();
                    jetShootTimer.Start();
                    if (isMusicOn)
                        backgroundMusic.PlayLooping();
                }
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
            gravity = 0;
            if (isMusicOn)
                backgroundMusic.PlayLooping();
            else
                backgroundMusic.Stop();
            gameOver = false;

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "chuong_ngai_vat")
                    x.Left = random.Next(1000, 3800);

                if (x is PictureBox && (string)x.Tag == "enemy_bullet")
                {
                    this.Controls.Remove(x);
                    x.Dispose();
                }
            }

            if (File.Exists(highScoreFile))
                int.TryParse(File.ReadAllText(highScoreFile), out highScore);
            else
                highScore = 0;

            jet = picjet;
            jet.BringToFront();

            // 🔴 Gỡ bỏ Tick event cũ (nếu có)
            jetMoveTimer.Tick -= JetMoveTimer_Tick;
            jetShootTimer.Tick -= JetShootTimer_Tick;

            // ✅ Gán lại chính xác 1 lần
            jetMoveTimer.Interval = 20;
            jetMoveTimer.Tick += JetMoveTimer_Tick;
            jetMoveTimer.Start();   
            jetShootTimer.Tick += JetShootTimer_Tick;
            jetShootTimer.Start();

            gameTimer.Start();
        }


        private void GameOver()
        {
            gameTimer.Stop();
            jetMoveTimer.Stop();
            jetShootTimer.Stop();
            backgroundMusic.Stop();
            GameOverSound.Play();
            lbScore.Text += " Game Over!! Press \"R\" to Restart.";
            gameOver = true;
        }
        private void player_Click(object sender, EventArgs e)
        {
        }
    }
}
