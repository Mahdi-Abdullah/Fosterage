using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Fosterage
{
    public partial class Fosterparent : Form
    {
       
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        public Fosterparent()
        {
            InitializeComponent();
            SqlConnection conn = new SqlConnection(cs);

            string query = "select username from Foster_tbl where username=@user";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@user", Form2.user);

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                conn.Close();
                button5.Enabled = false;
                childViewDisable();
            }
            
        }

        void childViewDisable()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "select * from Foster_tbl where username=@user and child='none'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@user", Form2.user);

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.HasRows)
            {
                button3.Enabled = false;
            }
            else
                button3.Enabled = true;
            conn.Close();
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

        private void button5_Click(object sender, EventArgs e)
        {
            Fapplication fa = new Fapplication();
            fa.TopLevel = false;
            Gmainpanel.Controls.Add(fa);
            fa.BringToFront();
            fa.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Child_Info ci = new Child_Info();
            ci.TopLevel = false;
            Gmainpanel.Controls.Add(ci);
            ci.BringToFront();
            ci.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Helpsupport h = new Helpsupport();
            h.TopLevel = false;
            Gmainpanel.Controls.Add(h);
            h.BringToFront();
            h.Show();
        }

        private void Gmainpanel_Paint(object sender, PaintEventArgs e)
        {
            FHomepage ghm = new FHomepage();
            ghm.TopLevel = false;
            Gmainpanel.Controls.Add(ghm);
            ghm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Viewdonation vd = new Viewdonation();
            vd.TopLevel = false;
            Gmainpanel.Controls.Add(vd);
            vd.BringToFront();
            vd.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                new Form2().Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to close?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            FHomepage ghm = new FHomepage();
            ghm.TopLevel = false;
            Gmainpanel.Controls.Add(ghm);
            ghm.Show();
        }
    }
}
