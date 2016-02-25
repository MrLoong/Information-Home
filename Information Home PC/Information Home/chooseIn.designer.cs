namespace Information_Home_PC
{
    partial class chooseIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Information_Home_PC.VistaButton Con;
        private Information_Home_PC.VistaButton Skip;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chooseIn));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Con = new Information_Home_PC.VistaButton();
            this.Skip = new Information_Home_PC.VistaButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入大厅地址：";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.textBox1.ForeColor = System.Drawing.Color.Transparent;
            this.textBox1.Location = new System.Drawing.Point(157, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(157, 21);
            this.textBox1.TabIndex = 1;
            // 
            // Con
            // 
            this.Con.BackColor = System.Drawing.Color.Transparent;
            this.Con.BaseColor = System.Drawing.Color.Transparent;
            this.Con.ButtonColor = System.Drawing.SystemColors.Highlight;
            this.Con.ButtonText = "连接";
            this.Con.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Con.GlowColor = System.Drawing.Color.White;
            this.Con.Location = new System.Drawing.Point(12, 97);
            this.Con.Name = "Con";
            this.Con.Size = new System.Drawing.Size(125, 41);
            this.Con.TabIndex = 0;
            this.Con.Click += new System.EventHandler(this.Con_Click);
            // 
            // Skip
            // 
            this.Skip.BackColor = System.Drawing.Color.Transparent;
            this.Skip.BaseColor = System.Drawing.Color.Transparent;
            this.Skip.ButtonColor = System.Drawing.SystemColors.Highlight;
            this.Skip.ButtonText = "跳过";
            this.Skip.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Skip.GlowColor = System.Drawing.Color.White;
            this.Skip.Location = new System.Drawing.Point(189, 97);
            this.Skip.Name = "Skip";
            this.Skip.Size = new System.Drawing.Size(125, 41);
            this.Skip.TabIndex = 0;
            this.Skip.Click += new System.EventHandler(this.Skip_Click);
            // 
            // chooseIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(326, 176);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Con);
            this.Controls.Add(this.Skip);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(342, 215);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(342, 215);
            this.Name = "chooseIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "chooseIn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}