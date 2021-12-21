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
    public partial class Gurdian : Form
    {
        public static bool time = false;
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        public Gurdian()
        {
            InitializeComponent();

            SqlConnection conn = new SqlConnection(cs);
            string query = "select username from CHILD_TBL where username=@user";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@user", Form2.user);

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows == true)
            {
                conn.Close();
                btnGA.Enabled = false;
                childViewDisable();
            }

            if (time == true)
            {
                MessageBox.Show("You are child is assign to a kind foster parent", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                time = false;
            }
            
        }

        void childViewDisable()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "select * from CHILD_TBL where username=@user and foster='pending'";
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Helpsupport h = new Helpsupport();
            h.TopLevel = false;
            Gmainpanel.Controls.Add(h);
            h.BringToFront();
            h.Show();
        }

        private void Gurdian_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            GHomepage ghm = new GHomepage();
            ghm.TopLevel = false;
            Gmainpanel.Controls.Add(ghm);
            ghm.Show();
        }

        private void btnGA_Click(object sender, EventArgs e)
        {
            Gapplication ga = new Gapplication();
            ga.TopLevel = false;
            Gmainpanel.Controls.Add(ga);
            ga.BringToFront();
            ga.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Child_Info ci = new Child_Info();
            ci.TopLevel = false;
            Gmainpanel.Controls.Add(ci);
            ci.BringToFront();
            ci.Show();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Donation d = new Donation();
            d.TopLevel = false;
            Gmainpanel.Controls.Add(d);
            d.BringToFront();
            d.Show();
        }

        private void btnGA_Click_1(object sender, EventArgs e)
        {
            Gapplication ga = new Gapplication();
            ga.TopLevel = false;
            Gmainpanel.Controls.Add(ga);
            ga.BringToFront();
            ga.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Contactlist c = new Contactlist();
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
                this.Close();
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
            GHomepage ghm = new GHomepage();
            ghm.TopLevel = false;
            Gmainpanel.Controls.Add(ghm);
            ghm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void Gmainpanel_Enter(object sender, EventArgs e)
        {
        }

        private void Gmainpanel_ContextMenuStripChanged(object sender, EventArgs e)
        {
        }

        private void button3_EnabledChanged(object sender, EventArgs e)
        {
           
        }
    }
}
