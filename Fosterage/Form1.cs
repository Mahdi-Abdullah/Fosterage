using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fosterage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Enabled = true;

        }

        int time = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            progressBar1.Value = time;
            if(time == 100)
            {
                timer1.Enabled = false;
                var fm2 = new Form2();
                this.Hide();
                fm2.Show();
                time = 0;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_VisibleChanged(object sender, EventArgs e)
        {
            progressBar1.Hide();
        }
    }
}
