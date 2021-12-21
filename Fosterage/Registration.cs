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
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Fosterage
{
    public partial class Registration : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        string type = "";
        bool username = false;
        bool email = false;
        bool phone = false;
        bool password = false;

        public Registration()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" 
                && textBox6.Text != "" && username != false && email != false && phone != false && password != false)
            {
                if (!radioButton1.Checked && !radioButton3.Checked)
                {
                    errorProvider7.Icon = Properties.Resources.error;
                    errorProvider7.SetError(this.groupBox1, "Please select an account type.");
                    MessageBox.Show("Please select an account type.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlConnection conn = new SqlConnection(cs);

                    string query = "INSERT INTO ACCOUNT_TBL VALUES( @user, @name, @type, @email, @phone, @pass)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@user", textBox2.Text);
                    cmd.Parameters.AddWithValue("@email", textBox3.Text);
                    cmd.Parameters.AddWithValue("@phone", textBox4.Text);
                    cmd.Parameters.AddWithValue("@pass", textBox5.Text);
                    cmd.Parameters.AddWithValue("@type", type);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    if (MessageBox.Show("Registration successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        this.Hide();
                        new Form2().Show();
                        this.Close();
                    }
                }
            }
            else
                MessageBox.Show("Fill All the Boxes", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                type = "FOSTER";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                type = "GUARDIAN";
            }
        }


        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                errorProvider1.Icon = Properties.Resources.error;
                errorProvider1.SetError(this.textBox1, "Enter Name");
            }
            else
                errorProvider1.Icon = Properties.Resources.check;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                errorProvider2.Icon = Properties.Resources.error;
                errorProvider2.SetError(this.textBox2, "Enter Username");
            }
            else
                errorProvider2.Icon = Properties.Resources.check;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) == true)
            {
                errorProvider3.Icon = Properties.Resources.error;
                errorProvider3.SetError(this.textBox3, "Enter Email");
            }
            else
                errorProvider3.Icon = Properties.Resources.check;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) == true)
            {
                errorProvider4.Icon = Properties.Resources.error;
                errorProvider4.SetError(this.textBox4, "Enter Phone number");
            }
            else if(textBox4.TextLength < 11)
            {
                errorProvider4.Icon = Properties.Resources.error;
                errorProvider4.SetError(this.textBox4, "Enter Phone number");
                MessageBox.Show("Please enter a valid phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                errorProvider4.Icon = Properties.Resources.check;
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text) == true)
            {
                errorProvider6.Icon = Properties.Resources.error;
                errorProvider6.SetError(this.textBox6, "Enter Password");
            }
            else
                errorProvider6.Icon = Properties.Resources.check;
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text) == true)
            {
                errorProvider5.Icon = Properties.Resources.error;
                errorProvider5.SetError(this.textBox5, "Enter Password Again");
            }
            else
                errorProvider5.Icon = Properties.Resources.check;
        }


        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("A phone number cannot contain letter or special character.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (textBox2.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select USERNAME from ACCOUNT_TBL where USERNAME=@user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", textBox2.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider2.Icon = Properties.Resources.error;
                    errorProvider2.SetError(this.textBox2, "Enter Username");
                    MessageBox.Show("This username already taken. Please Choose another username.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    username = true;
                    errorProvider2.Icon = Properties.Resources.check;
                }
                conn.Close();
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (textBox5.Text != textBox6.Text)
            {
                errorProvider5.Icon = Properties.Resources.error;
                errorProvider5.SetError(this.textBox5, "Enter password again");
                MessageBox.Show("Password does not match.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox5.Clear();
            }
            else if (string.IsNullOrEmpty(textBox5.Text) == false)
            {
                password = true;
                errorProvider5.Icon = Properties.Resources.check;
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            string ptrn = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if(Regex.IsMatch(textBox3.Text, ptrn) == false)
            {
                errorProvider3.Icon = Properties.Resources.error;
                errorProvider3.SetError(this.textBox3, "Enter email");
                MessageBox.Show("Please enter a valid email.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Regex.IsMatch(textBox3.Text, ptrn))
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select EMAIL from ACCOUNT_TBL where EMAIL=@email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", textBox3.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider3.Icon = Properties.Resources.error;
                    errorProvider3.SetError(this.textBox3, "Enter email");
                    MessageBox.Show("This email is already registered. Please Choose another email.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox3.Clear();
                }
                else
                {
                    email = true;
                    errorProvider3.Icon = Properties.Resources.check;
                }
                conn.Close();
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (textBox4.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select PHONE from ACCOUNT_TBL where PHONE=@phone";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@phone", textBox4.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider4.Icon = Properties.Resources.error;
                    errorProvider4.SetError(this.textBox4, "Enter phone number");
                    MessageBox.Show("This phone number is already registered. Please Choose another phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.Clear();
                }
                else
                    phone = true;
                conn.Close();
            }
        }

        private void groupBox1_Leave(object sender, EventArgs e)
        {
        }

        private void groupBox1_Validating(object sender, CancelEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form2().Show();
            this.Close();
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
