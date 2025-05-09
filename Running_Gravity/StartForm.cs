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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Running_Gravity
{
    public partial class StartForm : Form
    {
        public SoundPlayer backgroundMusic;
        public bool IsMusicOn = true;
        public int gravityValue { get; set;}
        public int obstacleSpeed { get; set;}
        public int jetShootTimer { get; set;}
        public StartForm()
        {
            backgroundMusic = new SoundPlayer(Properties.Resources.background_music);
            InitializeComponent();
        }

      
        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            string selectedDifficulty;

            if (cmbDifficulty.SelectedItem == null)
            {
                selectedDifficulty = "Medium"; // hoặc hiện thông báo lỗi
            }
            else
            {
                selectedDifficulty = cmbDifficulty.SelectedItem.ToString();
            }
            Game gameForm = new Game(backgroundMusic, IsMusicOn);
            gameForm.FormClosed += (s, args) => this.Close();
            gameForm.Show();
        }

        private void Sound_Click(object sender, EventArgs e)
        {
            if (IsMusicOn)
            {
                // Tắt âm thanh
                backgroundMusic.Stop();
                Sound.Image = Properties.Resources.nosound;
                IsMusicOn = false;
            }
            else
            {
                // Bật lại âm thanh
                backgroundMusic.PlayLooping();
                Sound.Image = Properties.Resources.sound;
                IsMusicOn = true;
            }
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            Sound.Image = Properties.Resources.sound;
            backgroundMusic.PlayLooping();
        }

        private void cmbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {   
            string Value = cmbDifficulty.SelectedItem.ToString();
            if (Value == "Low") {
                this.gravityValue = 8;
                this.obstacleSpeed = 10;
                this.jetShootTimer = 2000;
            }
            else if (Value == "Medium")
            {
                this.gravityValue = 15;
                this.obstacleSpeed = 20;
                this.jetShootTimer = 1000;
            }
            else if(Value == "High")
            {
                this.gravityValue = 20;
                this.obstacleSpeed = 25;
                this.jetShootTimer = 500;
            }
        }
    }
}
