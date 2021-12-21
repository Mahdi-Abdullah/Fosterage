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

namespace Fosterage
{
    public partial class Donation : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        bool amount = false;
        public Donation()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                errorProvider1.Icon = Properties.Resources.error;
                errorProvider1.SetError(this.textBox1, "Enter a Valid amount");
                amount = false;
            }
            else if (textBox1.TextLength <= 0)
            {
                errorProvider1.Icon = Properties.Resources.error;
                errorProvider1.SetError(this.textBox1, "Enter a Valid amount");
                MessageBox.Show("Please enter a Valid amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                amount = false;
            }
            else
            {
                errorProvider1.Icon = Properties.Resources.check;
                amount = true;
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Amount cannot contain letter or special character.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && amount != false)
            {
                if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
                {
                    errorProvider2.Icon = Properties.Resources.error;
                    errorProvider2.SetError(this.label4, "Enter a payment method");
                }
                else
                {
                    errorProvider2.Icon = Properties.Resources.check;
                    errorProvider2.SetError(this.label4, "");

                    SqlConnection conn = new SqlConnection(cs);

                    string query = "INSERT INTO DONATION VALUES(@username, @amount, @date)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", Form2.user);
                    cmd.Parameters.AddWithValue("@amount", textBox1.Text);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString());

                    conn.Open();
                    int a = cmd.ExecuteNonQuery();

                    if (a > 0)
                    {
                        MessageBox.Show("Thank you for your contribution", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.textBox1.Clear();

                    }
                    else
                    {
                        MessageBox.Show("Donation unsuccessful", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Donation unsuccessful", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
