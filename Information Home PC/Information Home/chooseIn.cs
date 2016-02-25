using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Information_Home_PC
{
    public partial class chooseIn : Form
    {
        public chooseIn()
        {
            InitializeComponent();
        }

        private void Con_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage home = new HomePage(this.textBox1.Text.ToString());
            home.Show();
        }

        private void Skip_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomePage home = new HomePage();
            home.Show();
        }
    }
}
