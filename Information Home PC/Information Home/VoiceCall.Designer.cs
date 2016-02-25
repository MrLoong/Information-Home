namespace Information_Home_PC
{
    partial class VoiceCall
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VoiceCall));
            this.label1 = new System.Windows.Forms.Label();
            this.Voice = new Information_Home_PC.VistaButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(57, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "W语音中";
            // 
            // Voice
            // 
            this.Voice.BackColor = System.Drawing.Color.Transparent;
            this.Voice.BaseColor = System.Drawing.Color.Transparent;
            this.Voice.ButtonColor = System.Drawing.SystemColors.Highlight;
            this.Voice.ButtonText = "点击转换";
            this.Voice.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Voice.GlowColor = System.Drawing.Color.White;
            this.Voice.Location = new System.Drawing.Point(63, 74);
            this.Voice.Name = "Voice";
            this.Voice.Size = new System.Drawing.Size(125, 41);
            this.Voice.TabIndex = 0;
            this.Voice.Click += new System.EventHandler(this.Voice_Click);
            // 
            // VoiceCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(245, 136);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Voice);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VoiceCall";
            this.Text = "VoiceCall";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Information_Home_PC.VistaButton Voice;
        private System.Windows.Forms.Label label1;
    }
}