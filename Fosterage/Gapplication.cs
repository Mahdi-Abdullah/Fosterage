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
    public partial class Gapplication : Form 
    {
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        bool email = false;
        bool phone = false;
        bool nid = false;
        bool pic = false;

        public Gapplication()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && dateTimePicker1.Value < DateTime.Today && numericUpDown1.Value > 0 && ComboBox2.Text != "" && 
                textBox3.Text != "" && textBox2.Text != "" && textBox10.Text != "" && textBox6.Text != "" && textBox7.Text != ""
                && textBox4.Text != "" && textBox5.Text != "" && textBox12.Text != "" && nid != false && email != false && phone != false)
            {
                if (pic == false)
                {
                    errorProvider13.Icon = Properties.Resources.error;
                    errorProvider13.SetError(this.button1, "Enter Picture");
                    MessageBox.Show("Please select a picture.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    errorProvider13.Icon = Properties.Resources.check;
                    errorProvider13.SetError(this.button1, "");

                    SqlConnection conn = new SqlConnection(cs);

                    string query = "INSERT INTO CHILD_TBL VALUES( @pic, @username, @name, @dob, @age, @gender,@father, @mother, @religion, @phone, @email, @nid, @CurrentAddress, @PermanentAddress, @foster)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@pic", SavePhoto());
                    cmd.Parameters.AddWithValue("@username", Form2.user);
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@age", numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@gender", ComboBox2.Text);
                    cmd.Parameters.AddWithValue("@father", textBox2.Text);
                    cmd.Parameters.AddWithValue("@mother", textBox3.Text);
                    cmd.Parameters.AddWithValue("@religion", textBox10.Text);
                    cmd.Parameters.AddWithValue("@phone", textBox6.Text);
                    cmd.Parameters.AddWithValue("@email", textBox7.Text);
                    cmd.Parameters.AddWithValue("@nid", textBox4.Text);
                    cmd.Parameters.AddWithValue("@CurrentAddress", textBox5.Text);
                    cmd.Parameters.AddWithValue("@PermanentAddress", textBox12.Text);
                    cmd.Parameters.AddWithValue("@foster", "pending");

                    conn.Open();
                    int a = cmd.ExecuteNonQuery();

                    if (a > 0)
                    {
                        if (MessageBox.Show("Application successful please login again", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            Application.Restart();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Application unsuccessful", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Today)
            {
                errorProvider2.Icon = Properties.Resources.error;
                errorProvider2.SetError(this.dateTimePicker1, "Enter a correct birthdate");
            }
            else
                errorProvider2.Icon = Properties.Resources.check;
        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            if (numericUpDown1.Value <= 0)
            {
                errorProvider3.Icon = Properties.Resources.error;
                errorProvider3.SetError(this.numericUpDown1, "Enter a valid age");
            }
            else
                errorProvider3.Icon = Properties.Resources.check;
        }

        private void ComboBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ComboBox2.Text) == true)
            {
                errorProvider4.Icon = Properties.Resources.error;
                errorProvider4.SetError(this.ComboBox2, "Select a gender");
            }
            else
                errorProvider4.Icon = Properties.Resources.check;
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) == true)
            {
                errorProvider5.Icon = Properties.Resources.error;
                errorProvider5.SetError(this.textBox3, "Enter Mother's Name");
            }
            else
                errorProvider5.Icon = Properties.Resources.check;
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true)
            {
                errorProvider6.Icon = Properties.Resources.error;
                errorProvider6.SetError(this.textBox2, "Enter Father's Name");
            }
            else
                errorProvider6.Icon = Properties.Resources.check;
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox10.Text) == true)
            {
                errorProvider7.Icon = Properties.Resources.error;
                errorProvider7.SetError(this.textBox10, "Enter religion");
            }
            else
                errorProvider7.Icon = Properties.Resources.check;
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text) == true)
            {
                errorProvider8.Icon = Properties.Resources.error;
                errorProvider8.SetError(this.textBox6, "Enter Phone number");
            }
            else if (textBox6.TextLength < 11)
            {
                errorProvider8.Icon = Properties.Resources.error;
                errorProvider8.SetError(this.textBox6, "Enter Phone number");
                MessageBox.Show("Please enter a valid phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                errorProvider8.Icon = Properties.Resources.check;
        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            if (textBox6.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select PHONE from CHILD_TBL where PHONE=@phone";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@phone", textBox6.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider8.Icon = Properties.Resources.error;
                    errorProvider8.SetError(this.textBox6, "Enter phone number");
                    MessageBox.Show("This phone number is already registered. Please Choose another phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.textBox6.Clear();
                }
                else
                    phone = true;
                conn.Close();
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text) == true)
            {
                errorProvider9.Icon = Properties.Resources.error;
                errorProvider9.SetError(this.textBox7, "Enter Email");
            }
            else
                errorProvider9.Icon = Properties.Resources.check;
        }

        private void textBox7_Validating(object sender, CancelEventArgs e)
        {
            string ptrn = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(textBox7.Text, ptrn) == false)
            {
                errorProvider9.Icon = Properties.Resources.error;
                errorProvider9.SetError(this.textBox7, "Enter email");
                MessageBox.Show("Please enter a valid email.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Regex.IsMatch(textBox7.Text, ptrn))
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select EMAIL from CHILD_TBL where EMAIL=@email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@email", textBox7.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider9.Icon = Properties.Resources.error;
                    errorProvider9.SetError(this.textBox7, "Enter email");
                    MessageBox.Show("This email is already registered. Please Choose another email.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox7.Clear();
                }
                else
                {
                    email = true;
                    errorProvider9.Icon = Properties.Resources.check;
                }
                conn.Close();
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) == true)
            {
                errorProvider10.Icon = Properties.Resources.error;
                errorProvider10.SetError(this.textBox4, "Enter NID");
            }
            else if (textBox4.TextLength < 10)
            {
                errorProvider10.Icon = Properties.Resources.error;
                errorProvider10.SetError(this.textBox4, "Enter NID");
                MessageBox.Show("Please enter a valid NID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                errorProvider10.Icon = Properties.Resources.check;
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (textBox4.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select NID from CHILD_TBL where NID=@nid";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nid", textBox4.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider10.Icon = Properties.Resources.error;
                    errorProvider10.SetError(this.textBox4, "Enter NID");
                    MessageBox.Show("This NID is already registered. Please Choose another NID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.textBox4.Clear();
                }
                else
                {
                    nid = true;
                    errorProvider10.Icon = Properties.Resources.check;
                }
                conn.Close();
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text) == true)
            {
                errorProvider11.Icon = Properties.Resources.error;
                errorProvider11.SetError(this.textBox5, "Enter Current Address");
            }
            else
                errorProvider11.Icon = Properties.Resources.check;
        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox12.Text) == true)
            {
                errorProvider12.Icon = Properties.Resources.error;
                errorProvider12.SetError(this.textBox12, "Enter Permanent Address");
            }
            else
                errorProvider12.Icon = Properties.Resources.check;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("A phone number cannot contain letter or special character.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
