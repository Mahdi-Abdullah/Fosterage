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
using System.IO;

namespace Fosterage
{
    public partial class Fapplication : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        bool email = false;
        bool phone = false;
        bool nid = false;
        bool pic = false;

        public Fapplication()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "IMAGE FILE (*.*) | *.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
                pic = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && numericUpDown1.Value > 24 && textBox10.Text != "" && textBox6.Text != "" && textBox7.Text != ""
                && textBox4.Text != "" && textBox5.Text != "" && nid != false && email != false && phone != false)
            {
                if (pic == false)
                {
                    errorProvider8.Icon = Properties.Resources.error;
                    errorProvider8.SetError(this.button1, "Enter Picture");
                    MessageBox.Show("Please select a picture.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SqlConnection conn = new SqlConnection(cs);

                    string query = "INSERT INTO Foster_tbl VALUES( @username, @name, @age, @religion, @email, @phone, @nid, @address, @pic, 'NONE')";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", Form2.user);
                    cmd.Parameters.AddWithValue("@name", textBox3.Text);
                    cmd.Parameters.AddWithValue("@age", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@religion", textBox10.Text);
                    cmd.Parameters.AddWithValue("@email", textBox7.Text);
                    cmd.Parameters.AddWithValue("@phone", textBox6.Text);
                    cmd.Parameters.AddWithValue("@nid", textBox4.Text);
                    cmd.Parameters.AddWithValue("@address", textBox5.Text);
                    cmd.Parameters.AddWithValue("@pic", SavePhoto());

                    conn.Open();
                    int a = cmd.ExecuteNonQuery();

                    if (a > 0)
                    {
                        if (MessageBox.Show("Registration successful please login again", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            Application.Restart();
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("Registration unsuccessful", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Fill All the Boxes", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) == true)
            {
                errorProvider1.Icon = Properties.Resources.error;
                errorProvider1.SetError(this.textBox3, "Enter Name");
            }
            else
                errorProvider1.Icon = Properties.Resources.check;
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            if (numericUpDown1.Value < 25)
            {
                errorProvider2.Icon = Properties.Resources.error;
                errorProvider2.SetError(this.numericUpDown1, "Enter age (you need to be at least 25 years old)");
            }
            else
                errorProvider2.Icon = Properties.Resources.check;
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox10.Text) == true)
            {
                errorProvider3.Icon = Properties.Resources.error;
                errorProvider3.SetError(this.textBox10, "Enter religion");
            }
            else
                errorProvider3.Icon = Properties.Resources.check;
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text) == true)
            {
                errorProvider4.Icon = Properties.Resources.error;
                errorProvider4.SetError(this.textBox6, "Enter Phone number");
            }
            else if (textBox6.TextLength < 11)
            {
                errorProvider4.Icon = Properties.Resources.error;
                errorProvider4.SetError(this.textBox6, "Enter Phone number");
                MessageBox.Show("Please enter a valid phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                errorProvider4.Icon = Properties.Resources.check;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("A phone number cannot contain letter or special character.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text) == true)
            {
                errorProvider5.Icon = Properties.Resources.error;
                errorProvider5.SetError(this.textBox7, "Enter Email");
            }
            else
                errorProvider5.Icon = Properties.Resources.check;
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) == true)
            {
                errorProvider6.Icon = Properties.Resources.error;
                errorProvider6.SetError(this.textBox4, "Enter NID");
            }
            else if (textBox4.TextLength < 10)
            {
                errorProvider6.Icon = Properties.Resources.error;
                errorProvider6.SetError(this.textBox4, "Enter NID");
                MessageBox.Show("Please enter a valid NID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                errorProvider6.Icon = Properties.Resources.check;
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text) == true)
            {
                errorProvider7.Icon = Properties.Resources.error;
                errorProvider7.SetError(this.textBox5, "Enter Address");
            }
            else
                errorProvider7.Icon = Properties.Resources.check;
        }


        private void pictureBox1_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            if (textBox6.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select PHONE from Foster_tbl where PHONE=@phone";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@phone", textBox6.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider4.Icon = Properties.Resources.error;
                    errorProvider4.SetError(this.textBox6, "Enter phone number");
                    MessageBox.Show("This phone number is already registered. Please Choose another phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.textBox6.Clear();
                }
                else
                    phone = true;
                conn.Close();
            }
        }

        private void textBox7_Validating(object sender, CancelEventArgs e)
        {
            string ptrn = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(textBox7.Text, ptrn) == false)
            {
                errorProvider5.Icon = Properties.Resources.error;
                errorProvider5.SetError(this.textBox7, "Enter email");
                MessageBox.Show("Please enter a valid email.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Regex.IsMatch(textBox7.Text, ptrn))
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select EMAIL from Foster_tbl where EMAIL=@email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", textBox7.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider5.Icon = Properties.Resources.error;
                    errorProvider5.SetError(this.textBox7, "Enter email");
                    MessageBox.Show("This email is already registered. Please Choose another email.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox7.Clear();
                }
                else
                {
                    email = true;
                    errorProvider5.Icon = Properties.Resources.check;
                }
                conn.Close();
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (textBox4.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select NID from Foster_tbl where NID=@nid";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nid", textBox4.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider6.Icon = Properties.Resources.error;
                    errorProvider6.SetError(this.textBox4, "Enter NID");
                    MessageBox.Show("This NID is already registered. Please Choose another NID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.textBox4.Clear();
                }
                else
                {
                    nid = true;
                    errorProvider6.Icon = Properties.Resources.check;
                }
                conn.Close();
            }
        }
    }
}
