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
        public bool isMusicOn => Bg_Music.Checked;
        public StartForm()
        {
            backgroundMusic = new SoundPlayer(Properties.Resources.background_music);
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            this.Hide();
            string selectedDifficulty;

            if (cmbDifficulty.SelectedItem == null)
            {
                selectedDifficulty = "Trung bình"; // hoặc hiện thông báo lỗi
            }
            else
            {
                selectedDifficulty = cmbDifficulty.SelectedItem.ToString();
            }
            Game gameForm = new Game(backgroundMusic, isMusicOn, selectedDifficulty); 
            gameForm.FormClosed += (s, args) => this.Close();
            gameForm.Show();
        }

        private void Bg_Music_CheckedChanged(object sender, EventArgs e)
        {
            if (Bg_Music.Checked)
            {
                backgroundMusic.PlayLooping();
            }
            else
            {
               backgroundMusic.Stop();
            }
        }
    }
}
