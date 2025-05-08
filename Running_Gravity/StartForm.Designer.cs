namespace Running_Gravity
{
    partial class StartForm
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
            this.Start = new System.Windows.Forms.Button();
            this.Bg_Music = new System.Windows.Forms.CheckBox();
            this.cmbDifficulty = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(303, 124);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(134, 68);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Bg_Music
            // 
            this.Bg_Music.AutoSize = true;
            this.Bg_Music.Location = new System.Drawing.Point(126, 301);
            this.Bg_Music.Name = "Bg_Music";
            this.Bg_Music.Size = new System.Drawing.Size(103, 24);
            this.Bg_Music.TabIndex = 1;
            this.Bg_Music.Text = "Nhạc nền";
            this.Bg_Music.UseVisualStyleBackColor = true;
            this.Bg_Music.CheckedChanged += new System.EventHandler(this.Bg_Music_CheckedChanged);
            // 
            // cmbDifficulty
            // 
            this.cmbDifficulty.FormattingEnabled = true;
            this.cmbDifficulty.Items.AddRange(new object[] {
            "Dễ",
            "Trung Bình",
            "Khó"});
            this.cmbDifficulty.Location = new System.Drawing.Point(303, 299);
            this.cmbDifficulty.Name = "cmbDifficulty";
            this.cmbDifficulty.Size = new System.Drawing.Size(121, 28);
            this.cmbDifficulty.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(336, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mức độ";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDifficulty);
            this.Controls.Add(this.Bg_Music);
            this.Controls.Add(this.Start);
            this.Name = "StartForm";
            this.Text = "StartForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.CheckBox Bg_Music;
        private System.Windows.Forms.ComboBox cmbDifficulty;
        private System.Windows.Forms.Label label1;
    }
}