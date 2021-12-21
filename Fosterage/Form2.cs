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
    public partial class Form2 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        public static string user = "";

        public Form2()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Registration().Show();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select typename from ACCOUNT_TBL where username=@user and password =@pass COLLATE SQL_Latin1_General_CP1_CS_AS";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                
                if (rdr.HasRows == true)
                {
                    string type;

                    while (rdr.Read())
                    {
                        type = rdr.GetString(0);

                        if (type == "GUARDIAN")
                        {
                            user = textBox1.Text;
                            this.Hide();
                            new Gurdian().Show();
                            this.Close();
                        }

                       else if (type == "FOSTER")
                        {
                            user = textBox1.Text;
                            this.Hide();
                            new Fosterparent().Show();
                            this.Close();
                        }
                        else if (type == "ADMIN")
                        {
                            user = textBox1.Text;
                            this.Hide();
                            new Admin().Show();
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Login Unsuccessful", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn.Close();
            }
            else
            {
                MessageBox.Show("Fill Both Boxes", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
                textBox2.UseSystemPasswordChar = true;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                errorProvider1.Icon = Properties.Resources.error;
                errorProvider1.SetError(this.textBox1, "Enter Username");
            }
            else
                errorProvider1.Icon = Properties.Resources.check;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                errorProvider2.Icon = Properties.Resources.error;
                errorProvider2.SetError(this.textBox2, "Enter Password");
            }
            else
                errorProvider2.Icon = Properties.Resources.check;
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
