using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Fosterage
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void button4_Click(object sender, EventArgs e)
        {
            Contactlist c = new Contactlist();
            c.TopLevel = false;
            Gmainpanel.Controls.Add(c);
            c.BringToFront();
            c.Show();
        }

        private void btnGA_Click(object sender, EventArgs e)
        {

        }

        private void Gmainpanel_Paint(object sender, PaintEventArgs e)
        {
            Grequest c = new Grequest();
            c.TopLevel = false;
            Gmainpanel.Controls.Add(c);
            c.BringToFront();
            c.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                new Form2().Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Grequest c = new Grequest();
            c.TopLevel = false;
            Gmainpanel.Controls.Add(c);
            c.BringToFront();
            c.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Grequest c = new Grequest();
            c.TopLevel = false;
            Gmainpanel.Controls.Add(c);
            c.BringToFront();
            c.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
