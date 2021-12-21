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
    public partial class Grequest : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;

        bool username = false;
        bool email = false;
        bool phone = false;
        bool password = false;
        bool nid = false;
        bool pic = false;
        bool amount = false;
        bool date = false;

        public Grequest()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }



        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {

        }

        //================================================================================

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = true; groupBox3.Visible = false; groupBox4.Visible = false; groupBox5.Visible = false;
            groupBox2.Location = new Point(12, 367);

            reset();

            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT * FROM ACCOUNT_TBL";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 50;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = false; groupBox3.Visible = true; groupBox4.Visible = false; groupBox5.Visible = false;
            groupBox3.Location = new Point(12, 367);

            reset();

            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT PICTURE, USERNAME, NAME, BIRTHDAY, AGE, GENDER, FATHER, MOTHER, RELIGION, PHONE, EMAIL, NID, CURRENT_ADDRESS, PERMANENT_ADDRESS, FOSTER FROM CHILD_TBL";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[0];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 50;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = false; groupBox3.Visible = false; groupBox4.Visible = true; groupBox5.Visible = false;
            groupBox4.Location = new Point(12, 367);

            reset();

            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT * FROM Foster_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[8];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 50;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = false; groupBox3.Visible = false; groupBox4.Visible = false; groupBox5.Visible = true;
            groupBox5.Location = new Point(12, 367);

            reset();

            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT * FROM DONATION";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 50;
        }

        //================================================================================

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        //================================================================================

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (username == true && phone == true && email == true && password == true && textBox1.Text != "")
                {
                    if (comboBox1.SelectedItem == null)
                    {
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
                        cmd.Parameters.AddWithValue("@type", comboBox1.SelectedItem);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        if (MessageBox.Show("Insertion successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            reset();

                            string query2 = "SELECT * FROM ACCOUNT_TBL";
                            SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                            DataTable data = new DataTable();
                            sda.Fill(data);
                            dataGridView1.DataSource = data;

                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1.RowTemplate.Height = 50;
                        }
                    }
                }
                else
                    MessageBox.Show("Fill All the Boxes Correctly", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButton2.Checked)
            {
                if (username == true && email == true && phone == true && nid == true && textBox6.Text != "" && textBox7.Text != ""
                    && dateTimePicker1.Value < DateTime.Today && numericUpDown1.Value > 0 && comboBox2.Text != "" && textBox8.Text != ""
                    && textBox9.Text != "" && textBox10.Text != "" && textBox14.Text != "" && textBox15.Text != "" && textBox16.Text != "")
                {
                    if (pic == false)
                    {
                        errorProvider15.Icon = Properties.Resources.error;
                        errorProvider15.SetError(this.button5, "Enter Picture");
                        MessageBox.Show("Please select a picture.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        errorProvider13.Icon = Properties.Resources.check;
                        errorProvider13.SetError(this.button5, "");

                        SqlConnection conn = new SqlConnection(cs);

                        string query = "INSERT INTO CHILD_TBL VALUES( @pic, @username, @name, @dob, @age, @gender,@father, @mother, @religion, @phone, @email, @nid, @CurrentAddress, @PermanentAddress, @foster)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@pic", SavePhoto());
                        cmd.Parameters.AddWithValue("@username", textBox6.Text);
                        cmd.Parameters.AddWithValue("@name", textBox7.Text);
                        cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                        cmd.Parameters.AddWithValue("@age", numericUpDown1.Value);
                        cmd.Parameters.AddWithValue("@gender", comboBox2.Text);
                        cmd.Parameters.AddWithValue("@father", textBox8.Text);
                        cmd.Parameters.AddWithValue("@mother", textBox9.Text);
                        cmd.Parameters.AddWithValue("@religion", textBox10.Text);
                        cmd.Parameters.AddWithValue("@phone", textBox11.Text);
                        cmd.Parameters.AddWithValue("@email", textBox12.Text);
                        cmd.Parameters.AddWithValue("@nid", textBox13.Text);
                        cmd.Parameters.AddWithValue("@CurrentAddress", textBox14.Text);
                        cmd.Parameters.AddWithValue("@PermanentAddress", textBox15.Text);
                        cmd.Parameters.AddWithValue("@foster", textBox16.Text);

                        conn.Open();
                        int a = cmd.ExecuteNonQuery();

                        if (a > 0)
                        {
                            conn.Close();
                            if (MessageBox.Show("Insertion successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                reset();

                                string query2 = "SELECT * FROM CHILD_TBL";
                                SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                                DataTable data = new DataTable();
                                sda.Fill(data);
                                dataGridView1.DataSource = data;

                                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                dataGridView1.RowTemplate.Height = 50;
                            }

                        }
                        else
                        {
                            conn.Close();
                            MessageBox.Show("Insertion successful unsuccessful", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                    MessageBox.Show("Fill All the Boxes", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButton3.Checked)
            {
                if (username == true && textBox18.Text != "" && numericUpDown2.Value > 24 && textBox19.Text != "" && email == true && phone == true
                    && nid == true && textBox23.Text != "" && textBox24.Text != "")
                {
                    if (pic == false)
                    {
                        errorProvider10.Icon = Properties.Resources.error;
                        errorProvider10.SetError(this.button6, "Enter Picture");
                        MessageBox.Show("Please select a picture.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        SqlConnection conn = new SqlConnection(cs);

                        string query = "INSERT INTO Foster_tbl VALUES( @username, @name, @age, @religion, @email, @phone, @nid, @address, @pic, 'NONE')";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@username", textBox17.Text);
                        cmd.Parameters.AddWithValue("@name", textBox18.Text);
                        cmd.Parameters.AddWithValue("@age", numericUpDown2.Value);
                        cmd.Parameters.AddWithValue("@religion", textBox19.Text);
                        cmd.Parameters.AddWithValue("@email", textBox20.Text);
                        cmd.Parameters.AddWithValue("@phone", textBox21.Text);
                        cmd.Parameters.AddWithValue("@nid", textBox22.Text);
                        cmd.Parameters.AddWithValue("@address", textBox23.Text);
                        cmd.Parameters.AddWithValue("@pic", SavePhoto2());

                        conn.Open();
                        int a = cmd.ExecuteNonQuery();

                        if (a > 0)
                        {
                            conn.Close();
                            if (MessageBox.Show("Insertion successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                            {
                                reset();

                                string query2 = "SELECT * FROM Foster_tbl";
                                SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                                DataTable data = new DataTable();
                                sda.Fill(data);
                                dataGridView1.DataSource = data;

                                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                                dataGridView1.RowTemplate.Height = 50;
                            }

                        }
                        else
                        {
                            conn.Close();
                            MessageBox.Show("Insertion successful unsuccessful", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                    MessageBox.Show("Fill All the Boxes", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButton4.Checked)
            {
                if (textBox25.Text != "" && amount == true && dateTimePicker2.Value < DateTime.Today && date == true)
                {
                    SqlConnection conn = new SqlConnection(cs);

                    string query = "INSERT INTO DONATION VALUES(@username, @amount, @date)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", textBox25.Text);
                    cmd.Parameters.AddWithValue("@amount", textBox26.Text);
                    cmd.Parameters.AddWithValue("@date", dateTimePicker2.Text);

                    conn.Open();
                    int a = cmd.ExecuteNonQuery();

                    if (a > 0)
                    {
                        conn.Close();
                        if (MessageBox.Show("Insertion successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            reset();

                            string query2 = "SELECT * FROM DONATION";
                            SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                            DataTable data = new DataTable();
                            sda.Fill(data);
                            dataGridView1.DataSource = data;

                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1.RowTemplate.Height = 50;
                        }

                    }
                    else
                    {
                        conn.Close();
                        MessageBox.Show("Insertion unsuccessful", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Insertion unsuccessful", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Insertion failed, please try again", "Unsuccessfull", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private object SavePhoto2()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox2.Image.Save(ms, pictureBox2.Image.RawFormat);
            return ms.GetBuffer();
        }

        private byte[] SavePhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (username == true && phone == true && email == true && password == true && textBox1.Text != "")
                {
                    if (comboBox1.SelectedItem == null)
                    {
                        MessageBox.Show("Please select an account type.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        SqlConnection conn = new SqlConnection(cs);

                        string query = "UPDATE ACCOUNT_TBL SET USERNAME=@user, NAME=@name, TYPENAME=@type, EMAIL=@email, PHONE=@phone, PASSWORD=@pass WHERE USERNAME=@user";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@user", textBox2.Text);
                        cmd.Parameters.AddWithValue("@email", textBox3.Text);
                        cmd.Parameters.AddWithValue("@phone", textBox4.Text);
                        cmd.Parameters.AddWithValue("@pass", textBox5.Text);
                        cmd.Parameters.AddWithValue("@type", comboBox1.SelectedItem);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        if (MessageBox.Show("Update successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            reset();

                            string query2 = "SELECT * FROM ACCOUNT_TBL";
                            SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                            DataTable data = new DataTable();
                            sda.Fill(data);
                            dataGridView1.DataSource = data;

                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1.RowTemplate.Height = 50;
                        }
                    }
                }
                else
                    MessageBox.Show("Fill All the Boxes Correctly", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButton2.Checked)
            {
                if (username == true && email == true && phone == true && nid == true && textBox6.Text != "" && textBox7.Text != ""
                    && dateTimePicker1.Value < DateTime.Today && numericUpDown1.Value > 0 && comboBox2.Text != "" && textBox8.Text != ""
                    && textBox9.Text != "" && textBox10.Text != "" && textBox14.Text != "" && textBox15.Text != "" && textBox16.Text != "")
                {
                    if (pic == false)
                    {
                        errorProvider15.Icon = Properties.Resources.error;
                        errorProvider15.SetError(this.button5, "Enter Picture");
                        MessageBox.Show("Please select a picture.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        errorProvider13.Icon = Properties.Resources.check;
                        errorProvider13.SetError(this.button5, "");

                        SqlConnection conn = new SqlConnection(cs);

                        string query = "UPDATE CHILD_TBL SET  PICTURE=@pic, USERNAME=@username, NAME=@name, BIRTHDAY=@dob, AGE=@age, GENDER=@gender, FATHER=@father, MOTHER=@mother, RELIGION=@religion, PHONE=@phone, EMAIL=@email, NID=@nid, CURRENT_ADDRESS=@CurrentAddress, PERMANENT_ADDRESS=@PermanentAddress, FOSTER=@foster WHERE USERNAME=@username";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@pic", SavePhoto());
                        cmd.Parameters.AddWithValue("@username", textBox6.Text);
                        cmd.Parameters.AddWithValue("@name", textBox7.Text);
                        cmd.Parameters.AddWithValue("@dob", dateTimePicker1.Text);
                        cmd.Parameters.AddWithValue("@age", numericUpDown1.Value);
                        cmd.Parameters.AddWithValue("@gender", comboBox2.Text);
                        cmd.Parameters.AddWithValue("@father", textBox8.Text);
                        cmd.Parameters.AddWithValue("@mother", textBox9.Text);
                        cmd.Parameters.AddWithValue("@religion", textBox10.Text);
                        cmd.Parameters.AddWithValue("@phone", textBox11.Text);
                        cmd.Parameters.AddWithValue("@email", textBox12.Text);
                        cmd.Parameters.AddWithValue("@nid", textBox13.Text);
                        cmd.Parameters.AddWithValue("@CurrentAddress", textBox14.Text);
                        cmd.Parameters.AddWithValue("@PermanentAddress", textBox15.Text);
                        cmd.Parameters.AddWithValue("@foster", textBox16.Text);

                        conn.Open();
                        int a = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (MessageBox.Show("Update successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            reset();

                            string query2 = "SELECT * FROM CHILD_TBL";
                            SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                            DataTable data = new DataTable();
                            sda.Fill(data);
                            dataGridView1.DataSource = data;

                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1.RowTemplate.Height = 50;
                        }

                    }
                }
                else
                    MessageBox.Show("Fill All the Boxes", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButton3.Checked)
            {
                if (username == true && textBox18.Text != "" && numericUpDown2.Value > 24 && textBox19.Text != "" && email == true && phone == true
                    && nid == true && textBox23.Text != "" && textBox24.Text != "")
                {
                    if (pic == false)
                    {
                        errorProvider10.Icon = Properties.Resources.error;
                        errorProvider10.SetError(this.button6, "Enter Picture");
                        MessageBox.Show("Please select a picture.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        SqlConnection conn = new SqlConnection(cs);

                        string query = "UPDATE Foster_tbl SET USERNAME=@username, NAME=@name, AGE=@age, RELIGION=@religion, EMAIL=@email, PHONE=@phone, NID=@nid, ADDRESS=@address, PICTURE=@pic, CHILD=@child WHERE USERNAME=@username";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@username", textBox17.Text);
                        cmd.Parameters.AddWithValue("@name", textBox18.Text);
                        cmd.Parameters.AddWithValue("@age", numericUpDown2.Value);
                        cmd.Parameters.AddWithValue("@religion", textBox19.Text);
                        cmd.Parameters.AddWithValue("@email", textBox20.Text);
                        cmd.Parameters.AddWithValue("@phone", textBox21.Text);
                        cmd.Parameters.AddWithValue("@nid", textBox22.Text);
                        cmd.Parameters.AddWithValue("@address", textBox23.Text);
                        cmd.Parameters.AddWithValue("@child", textBox24.Text);
                        cmd.Parameters.AddWithValue("@pic", SavePhoto2());

                        conn.Open();
                        int a = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (MessageBox.Show("Update successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            reset();

                            string query2 = "SELECT * FROM Foster_tbl";
                            SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                            DataTable data = new DataTable();
                            sda.Fill(data);
                            dataGridView1.DataSource = data;

                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1.RowTemplate.Height = 50;
                        }
                    }
                }
                else
                    MessageBox.Show("Fill All the Boxes", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButton4.Checked)
            {
                if (textBox25.Text != "" && amount == true && dateTimePicker2.Value < DateTime.Today && date == true)
                {
                    errorProvider3.Icon = Properties.Resources.check;
                    errorProvider3.SetError(this.label4, "");

                    SqlConnection conn = new SqlConnection(cs);

                    string query = "UPDATE DONATION SET USERNAME=@username, AMOUNT=@amount, DATE=@date WHERE DATE=@date";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", textBox25.Text);
                    cmd.Parameters.AddWithValue("@amount", textBox26.Text);
                    cmd.Parameters.AddWithValue("@date", dateTimePicker2.Text);

                    conn.Open();
                    int a = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (MessageBox.Show("Update successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        reset();

                        string query2 = "SELECT * FROM DONATION";
                        SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                        DataTable data = new DataTable();
                        sda.Fill(data);
                        dataGridView1.DataSource = data;

                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.RowTemplate.Height = 50;
                    }
                }
                else
                    MessageBox.Show("Fill All the Boxes", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Update failed, please try again", "Unsuccessfull", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (username == true && phone == true && email == true && password == true && textBox1.Text != "")
                {
                    if (comboBox1.SelectedItem == null)
                    {
                        MessageBox.Show("Please select an account type.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        SqlConnection conn = new SqlConnection(cs);

                        string query = "DELETE FROM ACCOUNT_TBL WHERE USERNAME = @user";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@user", textBox2.Text);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        if (MessageBox.Show("Deletion successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            reset();

                            string query2 = "SELECT * FROM ACCOUNT_TBL";
                            SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                            DataTable data = new DataTable();
                            sda.Fill(data);
                            dataGridView1.DataSource = data;

                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1.RowTemplate.Height = 50;
                        }
                    }
                }
                else
                    MessageBox.Show("Fill All the Boxes Correctly", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButton2.Checked)
            {
                if (username == true && email == true && phone == true && nid == true && textBox6.Text != "" && textBox7.Text != ""
                   && dateTimePicker1.Value < DateTime.Today && numericUpDown1.Value > 0 && comboBox2.Text != "" && textBox8.Text != ""
                   && textBox9.Text != "" && textBox10.Text != "" && textBox14.Text != "" && textBox15.Text != "" && textBox16.Text != "")
                {
                    if (pic == false)
                    {
                        errorProvider15.Icon = Properties.Resources.error;
                        errorProvider15.SetError(this.button5, "Enter Picture");
                        MessageBox.Show("Please select a picture.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        errorProvider13.Icon = Properties.Resources.check;
                        errorProvider13.SetError(this.button5, "");

                        SqlConnection conn = new SqlConnection(cs);

                        string query = "DELETE FROM CHILD_TBL WHERE USERNAME=@username";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@username", textBox6.Text);

                        conn.Open();
                        int a = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (MessageBox.Show("Deletion successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            reset();

                            string query2 = "SELECT * FROM CHILD_TBL";
                            SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                            DataTable data = new DataTable();
                            sda.Fill(data);
                            dataGridView1.DataSource = data;

                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1.RowTemplate.Height = 50;
                        }
                    }
                }
                else
                    MessageBox.Show("Fill All the Boxes", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButton3.Checked)
            {
                if (username == true && textBox18.Text != "" && numericUpDown2.Value > 24 && textBox19.Text != "" && email == true && phone == true
                    && nid == true && textBox23.Text != "" && textBox24.Text != "")
                {
                    if (pic == false)
                    {
                        errorProvider10.Icon = Properties.Resources.error;
                        errorProvider10.SetError(this.button6, "Enter Picture");
                        MessageBox.Show("Please select a picture.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        SqlConnection conn = new SqlConnection(cs);

                        string query = "DELETE FROM FOSTER_TBL WHERE USERNAME=@username";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@username", textBox17.Text);

                        conn.Open();
                        int a = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (MessageBox.Show("Deletion successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            reset();

                            string query2 = "SELECT * FROM FOSTER_TBL";
                            SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                            DataTable data = new DataTable();
                            sda.Fill(data);
                            dataGridView1.DataSource = data;

                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            dataGridView1.RowTemplate.Height = 50;
                        }
                    }
                }
                else
                    MessageBox.Show("Fill All the Boxes", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (radioButton4.Checked)
            {
                if (textBox25.Text != "" && amount == true && dateTimePicker2.Value < DateTime.Today && date == true)
                {
                    errorProvider3.Icon = Properties.Resources.check;
                    errorProvider3.SetError(this.label4, "");

                    SqlConnection conn = new SqlConnection(cs);

                    string query = "DELETE FROM DONATION WHERE DATE=@date";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@date", dateTimePicker2.Text);

                    conn.Open();
                    int a = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (MessageBox.Show("Deletion successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        reset();

                        string query2 = "SELECT * FROM DONATION";
                        SqlDataAdapter sda = new SqlDataAdapter(query2, conn);

                        DataTable data = new DataTable();
                        sda.Fill(data);
                        dataGridView1.DataSource = data;

                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView1.RowTemplate.Height = 50;
                    }
                }
                else
                    MessageBox.Show("Fill All the Boxes", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Deletion failed, please try again", "Unsuccessfull", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                reset();
            }
            else if (radioButton2.Checked)
            {
                reset();
            }
            else if (radioButton3.Checked)
            {
                reset();
            }
            else if (radioButton4.Checked)
            {
                reset();
            }
            else
                MessageBox.Show("Reset failed, please try again", "Unsuccessfull", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //================================================================================

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
                    errorProvider2.SetError(this.textBox2, "");
                }
                conn.Close();
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) == true)
            {
                errorProvider4.Icon = Properties.Resources.error;
                errorProvider4.SetError(this.textBox3, "Enter Email");
            }
            else
            {
                string ptrn = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                if (Regex.IsMatch(textBox3.Text, ptrn) == false)
                {
                    errorProvider4.Icon = Properties.Resources.error;
                    errorProvider4.SetError(this.textBox3, "Enter email");
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
                        errorProvider4.Icon = Properties.Resources.error;
                        errorProvider4.SetError(this.textBox3, "Enter email");
                        MessageBox.Show("This email is already registered. Please Choose another email.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox3.Clear();
                    }
                    else
                    {
                        email = true;
                        errorProvider4.Icon = Properties.Resources.check;
                    }
                    conn.Close();
                }
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text) == true)
            {
                errorProvider5.Icon = Properties.Resources.error;
                errorProvider5.SetError(this.textBox4, "Enter Phone number");
            }
            else if (textBox4.TextLength < 11)
            {
                errorProvider5.Icon = Properties.Resources.error;
                errorProvider5.SetError(this.textBox4, "Enter Phone number");
                MessageBox.Show("Please enter a valid phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select PHONE from ACCOUNT_TBL where PHONE=@phone";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@phone", textBox4.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider5.Icon = Properties.Resources.error;
                    errorProvider5.SetError(this.textBox4, "Enter phone number");
                    MessageBox.Show("This phone number is already registered. Please Choose another phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.Clear();
                }
                else
                {
                    phone = true;
                    errorProvider5.Icon = Properties.Resources.check;
                }
                conn.Close();
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text) == true)
            {
                errorProvider6.Icon = Properties.Resources.error;
                errorProvider6.SetError(this.textBox5, "Enter Password");
            }
            else
            {
                password = true;
                errorProvider6.Icon = Properties.Resources.check;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("A phone number cannot contain letter or special character.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void reset()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); textBox8.Clear();
            textBox9.Clear(); textBox10.Clear(); textBox11.Clear(); textBox12.Clear(); textBox13.Clear(); textBox14.Clear(); textBox15.Clear(); textBox16.Clear();
            textBox17.Clear(); textBox18.Clear(); textBox19.Clear(); textBox20.Clear(); textBox21.Clear(); textBox22.Clear(); textBox23.Clear(); textBox24.Clear();
            textBox25.Clear(); textBox26.Clear();

            comboBox1.SelectedItem = null; comboBox2.SelectedItem = null; username = false; phone = false; email = false; password = false; nid = false;
            pic = false; date = false; numericUpDown1.Value = 0; numericUpDown2.Value = 0; dateTimePicker1.Value = DateTime.Today; dateTimePicker2.Value = DateTime.Today; pictureBox1.Image = Properties.Resources.Profile_Logo_3_35;
            pictureBox2.Image = Properties.Resources.Profile_Logo_3_35;

            errorProvider1.Clear(); errorProvider2.Clear(); errorProvider3.Clear(); errorProvider4.Clear(); errorProvider5.Clear(); errorProvider6.Clear();
            errorProvider7.Clear(); errorProvider8.Clear(); errorProvider9.Clear(); errorProvider10.Clear(); errorProvider11.Clear(); errorProvider12.Clear();
            errorProvider13.Clear(); errorProvider14.Clear(); errorProvider15.Clear();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (e.RowIndex != -1)
                {
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["USERNAME"].Value.ToString(); username = true;
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["NAME"].Value.ToString();
                    comboBox1.SelectedItem = dataGridView1.Rows[e.RowIndex].Cells["TYPENAME"].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["EMAIL"].Value.ToString(); email = true;
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["PHONE"].Value.ToString(); phone = true;
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["PASSWORD"].Value.ToString(); password = true;
                    errorProvider1.Clear(); errorProvider2.Clear(); errorProvider3.Clear(); errorProvider4.Clear(); errorProvider5.Clear(); errorProvider6.Clear();
                }

            }
            else if (radioButton2.Checked)
            {
                if (e.RowIndex != -1)
                {
                    username = true; phone = true; email = true; nid = true; pic = true;
                    pictureBox1.Image = GetPhoto((byte[])dataGridView1.Rows[e.RowIndex].Cells["PICTURE"].Value);
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells["USERNAME"].Value.ToString();
                    textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells["NAME"].Value.ToString();
                    dateTimePicker1.Value = (DateTime)dataGridView1.Rows[e.RowIndex].Cells["BIRTHDAY"].Value;
                    numericUpDown1.Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["AGE"].Value);
                    comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["GENDER"].Value.ToString();
                    textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells["FATHER"].Value.ToString();
                    textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells["MOTHER"].Value.ToString();
                    textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells["RELIGION"].Value.ToString();
                    textBox11.Text = dataGridView1.Rows[e.RowIndex].Cells["PHONE"].Value.ToString();
                    textBox12.Text = dataGridView1.Rows[e.RowIndex].Cells["EMAIL"].Value.ToString();
                    textBox13.Text = dataGridView1.Rows[e.RowIndex].Cells["NID"].Value.ToString();
                    textBox14.Text = dataGridView1.Rows[e.RowIndex].Cells["Current_Address"].Value.ToString();
                    textBox15.Text = dataGridView1.Rows[e.RowIndex].Cells["Permanent_Address"].Value.ToString();
                    textBox16.Text = dataGridView1.Rows[e.RowIndex].Cells["Foster"].Value.ToString();
                    errorProvider1.Clear(); errorProvider2.Clear(); errorProvider3.Clear(); errorProvider4.Clear(); errorProvider5.Clear(); errorProvider6.Clear();
                    errorProvider7.Clear(); errorProvider8.Clear(); errorProvider9.Clear(); errorProvider10.Clear(); errorProvider11.Clear(); errorProvider12.Clear();
                    errorProvider13.Clear(); errorProvider14.Clear(); errorProvider15.Clear();
                }

            }
            else if (radioButton3.Checked)
            {
                if (e.RowIndex != -1)
                {
                    username = true; phone = true; email = true; nid = true; pic = true;
                    pictureBox2.Image = GetPhoto((byte[])dataGridView1.Rows[e.RowIndex].Cells["PICTURE"].Value);
                    textBox17.Text = dataGridView1.Rows[e.RowIndex].Cells["USERNAME"].Value.ToString();
                    textBox18.Text = dataGridView1.Rows[e.RowIndex].Cells["NAME"].Value.ToString();
                    numericUpDown2.Value = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["AGE"].Value);
                    textBox19.Text = dataGridView1.Rows[e.RowIndex].Cells["RELIGION"].Value.ToString();
                    textBox21.Text = dataGridView1.Rows[e.RowIndex].Cells["PHONE"].Value.ToString();
                    textBox20.Text = dataGridView1.Rows[e.RowIndex].Cells["EMAIL"].Value.ToString();
                    textBox22.Text = dataGridView1.Rows[e.RowIndex].Cells["NID"].Value.ToString();
                    textBox23.Text = dataGridView1.Rows[e.RowIndex].Cells["Address"].Value.ToString();
                    textBox24.Text = dataGridView1.Rows[e.RowIndex].Cells["Child"].Value.ToString();
                    errorProvider1.Clear(); errorProvider2.Clear(); errorProvider3.Clear(); errorProvider4.Clear(); errorProvider5.Clear(); errorProvider6.Clear();
                    errorProvider7.Clear(); errorProvider8.Clear(); errorProvider9.Clear(); errorProvider10.Clear(); errorProvider11.Clear(); errorProvider12.Clear();
                    errorProvider13.Clear(); errorProvider14.Clear(); errorProvider15.Clear();
                }
            }
            else if (radioButton4.Checked)
            {
                if (e.RowIndex != -1)
                {
                    amount = true; date = true;
                    textBox25.Text = dataGridView1.Rows[e.RowIndex].Cells["USERNAME"].Value.ToString();
                    textBox26.Text = dataGridView1.Rows[e.RowIndex].Cells["AMOUNT"].Value.ToString();
                    dateTimePicker2.Value = (DateTime)dataGridView1.Rows[e.RowIndex].Cells["DATE"].Value;

                    errorProvider1.Clear(); errorProvider2.Clear(); errorProvider3.Clear(); errorProvider4.Clear(); errorProvider5.Clear(); errorProvider6.Clear();
                    errorProvider7.Clear(); errorProvider8.Clear(); errorProvider9.Clear(); errorProvider10.Clear(); errorProvider11.Clear(); errorProvider12.Clear();
                    errorProvider13.Clear(); errorProvider14.Clear(); errorProvider15.Clear();
                }
            }
            else
                MessageBox.Show("Failed", "Unsuccessfull", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private Image GetPhoto(byte[] photo)
        {
            MemoryStream ms = new MemoryStream(photo);
            return Image.FromStream(ms);
        }

        private void button5_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "IMAGE FILE (*.*) | *.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(ofd.FileName);
                pic = true;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text) == true)
            {
                errorProvider1.Icon = Properties.Resources.error;
                errorProvider1.SetError(this.textBox6, "Enter Username");
            }
            else
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select USERNAME from CHILD_TBL where USERNAME=@user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", textBox6.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider1.Icon = Properties.Resources.error;
                    errorProvider1.SetError(this.textBox6, "Enter Username");
                    MessageBox.Show("This username already taken. Please Choose another username.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    username = true;
                    errorProvider1.Icon = Properties.Resources.check;
                    errorProvider1.SetError(this.textBox6, "");
                }
                conn.Close();
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text) == true)
            {
                errorProvider2.Icon = Properties.Resources.error;
                errorProvider2.SetError(this.textBox7, "Enter Name");
            }
            else
                errorProvider2.Icon = Properties.Resources.check;
        }

        private void dateTimePicker1_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_Leave(object sender, EventArgs e)
        {
            if (numericUpDown1.Value <= 0)
            {
                errorProvider4.Icon = Properties.Resources.error;
                errorProvider4.SetError(this.numericUpDown1, "Enter a valid age");
            }
            else
                errorProvider4.Icon = Properties.Resources.check;
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox2.Text) == true)
            {
                errorProvider5.Icon = Properties.Resources.error;
                errorProvider5.SetError(this.comboBox2, "Select a gender");
            }
            else
                errorProvider5.Icon = Properties.Resources.check;
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox8.Text) == true)
            {
                errorProvider6.Icon = Properties.Resources.error;
                errorProvider6.SetError(this.textBox8, "Enter Father's Name");
            }
            else
                errorProvider6.Icon = Properties.Resources.check;
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox9.Text) == true)
            {
                errorProvider7.Icon = Properties.Resources.error;
                errorProvider7.SetError(this.textBox9, "Enter Mother's Name");
            }
            else
                errorProvider7.Icon = Properties.Resources.check;
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox10.Text) == true)
            {
                errorProvider8.Icon = Properties.Resources.error;
                errorProvider8.SetError(this.textBox10, "Enter religion");
            }
            else
                errorProvider8.Icon = Properties.Resources.check;
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("A phone number cannot contain letter or special character.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox11_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox11.Text) == true)
            {
                errorProvider9.Icon = Properties.Resources.error;
                errorProvider9.SetError(this.textBox11, "Enter Phone number");
            }
            else if (textBox11.TextLength < 11)
            {
                errorProvider9.Icon = Properties.Resources.error;
                errorProvider9.SetError(this.textBox11, "Enter Phone number");
                MessageBox.Show("Please enter a valid phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                errorProvider9.Icon = Properties.Resources.check;
                SqlConnection conn = new SqlConnection(cs);

                string query = "select PHONE from CHILD_TBL where PHONE=@phone";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@phone", textBox11.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider9.Icon = Properties.Resources.error;
                    errorProvider9.SetError(this.textBox11, "Enter phone number");
                    MessageBox.Show("This phone number is already registered. Please Choose another phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.textBox11.Clear();
                }
                else
                    phone = true;
                conn.Close();
            }
        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox12.Text) == true)
            {
                errorProvider10.Icon = Properties.Resources.error;
                errorProvider10.SetError(this.textBox12, "Enter Email");
            }
            else
            {
                errorProvider10.Icon = Properties.Resources.check;
                string ptrn = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                if (Regex.IsMatch(textBox12.Text, ptrn) == false)
                {
                    errorProvider10.Icon = Properties.Resources.error;
                    errorProvider10.SetError(this.textBox12, "Enter email");
                    MessageBox.Show("Please enter a valid email.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Regex.IsMatch(textBox12.Text, ptrn))
                {
                    SqlConnection conn = new SqlConnection(cs);

                    string query = "select EMAIL from CHILD_TBL where EMAIL=@email";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@email", textBox12.Text);

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows == true)
                    {
                        errorProvider10.Icon = Properties.Resources.error;
                        errorProvider10.SetError(this.textBox12, "Enter email");
                        MessageBox.Show("This email is already registered. Please Choose another email.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox12.Clear();
                    }
                    else
                    {
                        email = true;
                        errorProvider10.Icon = Properties.Resources.check;
                    }
                    conn.Close();
                }
            }
        }

        private void textBox13_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox13.Text) == true)
            {
                errorProvider11.Icon = Properties.Resources.error;
                errorProvider11.SetError(this.textBox13, "Enter NID");
            }
            else if (textBox13.TextLength < 10)
            {
                errorProvider11.Icon = Properties.Resources.error;
                errorProvider11.SetError(this.textBox13, "Enter NID");
                MessageBox.Show("Please enter a valid NID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                errorProvider11.Icon = Properties.Resources.check;
                SqlConnection conn = new SqlConnection(cs);

                string query = "select NID from CHILD_TBL where NID=@nid";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nid", textBox13.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider11.Icon = Properties.Resources.error;
                    errorProvider11.SetError(this.textBox13, "Enter phone number");
                    MessageBox.Show("This phone number is already registered. Please Choose another phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.textBox13.Clear();
                }
                else
                {
                    nid = true;
                    errorProvider11.Icon = Properties.Resources.check;
                }
                conn.Close();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Today)
            {
                errorProvider3.Icon = Properties.Resources.error;
                errorProvider3.SetError(this.dateTimePicker1, "Enter a correct birthdate");
            }
            else
                errorProvider3.Icon = Properties.Resources.check;
        }

        private void textBox14_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox14.Text) == true)
            {
                errorProvider12.Icon = Properties.Resources.error;
                errorProvider12.SetError(this.textBox14, "Enter Current Address");
            }
            else
                errorProvider12.Icon = Properties.Resources.check;
        }

        private void textBox15_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox15.Text) == true)
            {
                errorProvider13.Icon = Properties.Resources.error;
                errorProvider13.SetError(this.textBox15, "Enter Permanent Address");
            }
            else
                errorProvider13.Icon = Properties.Resources.check;
        }

        private void textBox16_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox16.Text) == true)
            {
                errorProvider14.Icon = Properties.Resources.error;
                errorProvider14.SetError(this.textBox16, "Enter Foster");
            }
            else
                errorProvider14.Icon = Properties.Resources.check;
        }

        private void textBox17_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox17.Text) == true)
            {
                errorProvider1.Icon = Properties.Resources.error;
                errorProvider1.SetError(this.textBox17, "Enter Username");
            }
            else
            {
                SqlConnection conn = new SqlConnection(cs);

                string query = "select USERNAME from Foster_tbl where USERNAME=@user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", textBox17.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider1.Icon = Properties.Resources.error;
                    errorProvider1.SetError(this.textBox17, "Enter Username");
                    MessageBox.Show("This username already taken. Please Choose another username.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    username = true;
                    errorProvider1.Icon = Properties.Resources.check;
                    errorProvider1.SetError(this.textBox6, "");
                }
                conn.Close();
            }
        }

        private void textBox18_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox18.Text) == true)
            {
                errorProvider2.Icon = Properties.Resources.error;
                errorProvider2.SetError(this.textBox18, "Enter Name");
            }
            else
                errorProvider2.Icon = Properties.Resources.check;
        }

        private void numericUpDown2_Leave(object sender, EventArgs e)
        {
            if (numericUpDown2.Value < 25)
            {
                errorProvider3.Icon = Properties.Resources.error;
                errorProvider3.SetError(this.numericUpDown2, "Enter age (you need to be at least 25 years old)");
            }
            else
                errorProvider3.Icon = Properties.Resources.check;
        }

        private void textBox19_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox19.Text) == true)
            {
                errorProvider4.Icon = Properties.Resources.error;
                errorProvider4.SetError(this.textBox19, "Enter religion");
            }
            else
                errorProvider4.Icon = Properties.Resources.check;
        }

        private void textBox20_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox20.Text) == true)
            {
                errorProvider5.Icon = Properties.Resources.error;
                errorProvider5.SetError(this.textBox20, "Enter Email");
            }
            else
            {
                errorProvider5.Icon = Properties.Resources.check;
                string ptrn = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
                if (Regex.IsMatch(textBox20.Text, ptrn) == false)
                {
                    errorProvider5.Icon = Properties.Resources.error;
                    errorProvider5.SetError(this.textBox20, "Enter email");
                    MessageBox.Show("Please enter a valid email.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Regex.IsMatch(textBox20.Text, ptrn))
                {
                    SqlConnection conn = new SqlConnection(cs);

                    string query = "select EMAIL from Foster_tbl where EMAIL=@email";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@email", textBox20.Text);

                    conn.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows == true)
                    {
                        errorProvider5.Icon = Properties.Resources.error;
                        errorProvider5.SetError(this.textBox20, "Enter email");
                        MessageBox.Show("This email is already registered. Please Choose another email.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox20.Clear();
                    }
                    else
                    {
                        email = true;
                        errorProvider5.Icon = Properties.Resources.check;
                    }
                    conn.Close();
                }
            }
        }

        private void textBox21_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox21.Text) == true)
            {
                errorProvider6.Icon = Properties.Resources.error;
                errorProvider6.SetError(this.textBox21, "Enter Phone number");
            }
            else if (textBox21.TextLength < 11)
            {
                errorProvider6.Icon = Properties.Resources.error;
                errorProvider6.SetError(this.textBox21, "Enter Phone number");
                MessageBox.Show("Please enter a valid phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                errorProvider6.Icon = Properties.Resources.check;
                SqlConnection conn = new SqlConnection(cs);

                string query = "select PHONE from Foster_tbl where PHONE=@phone";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@phone", textBox21.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider6.Icon = Properties.Resources.error;
                    errorProvider6.SetError(this.textBox21, "Enter phone number");
                    MessageBox.Show("This phone number is already registered. Please Choose another phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.textBox21.Clear();
                }
                else
                    phone = true;
                conn.Close();
            }
        }

        private void textBox21_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("A phone number cannot contain letter or special character.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox22_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox22.Text) == true)
            {
                errorProvider7.Icon = Properties.Resources.error;
                errorProvider7.SetError(this.textBox22, "Enter NID");
            }
            else if (textBox22.TextLength < 10)
            {
                errorProvider7.Icon = Properties.Resources.error;
                errorProvider7.SetError(this.textBox22, "Enter NID");
                MessageBox.Show("Please enter a valid NID.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                errorProvider7.Icon = Properties.Resources.check;
                SqlConnection conn = new SqlConnection(cs);

                string query = "select NID from Foster_tbl where NID=@nid";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nid", textBox22.Text);

                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows == true)
                {
                    errorProvider7.Icon = Properties.Resources.error;
                    errorProvider7.SetError(this.textBox22, "Enter phone number");
                    MessageBox.Show("This phone number is already registered. Please Choose another phone number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.textBox22.Clear();
                }
                else
                {
                    nid = true;
                    errorProvider7.Icon = Properties.Resources.check;
                }
                conn.Close();
            }
        }

        private void textBox23_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox23.Text) == true)
            {
                errorProvider8.Icon = Properties.Resources.error;
                errorProvider8.SetError(this.textBox23, "Enter Address");
            }
            else
                errorProvider8.Icon = Properties.Resources.check;
        }

        private void textBox24_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox24.Text) == true)
            {
                errorProvider9.Icon = Properties.Resources.error;
                errorProvider9.SetError(this.textBox24, "Enter Foster");
            }
            else
                errorProvider9.Icon = Properties.Resources.check;
        }

        private void textBox25_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox25.Text) == true)
            {
                errorProvider1.Icon = Properties.Resources.error;
                errorProvider1.SetError(this.textBox25, "Enter Username");
            }
            else
            {
                username = true;
                errorProvider1.Icon = Properties.Resources.check;
                errorProvider1.SetError(this.textBox25, "");
            }
        }

        private void textBox26_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Amount cannot contain letter or special character.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox26_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox26.Text) == true)
            {
                errorProvider2.Icon = Properties.Resources.error;
                errorProvider2.SetError(this.textBox26, "Enter a Valid amount");
            }
            else if (textBox26.TextLength <= 0)
            {
                errorProvider2.Icon = Properties.Resources.error;
                errorProvider2.SetError(this.textBox26, "Enter a Valid amount");
                MessageBox.Show("Please enter a Valid amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                errorProvider2.Icon = Properties.Resources.check;
                amount = true;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value > DateTime.Today)
            {
                errorProvider3.Icon = Properties.Resources.error;
                errorProvider3.SetError(this.dateTimePicker2, "Enter a date");
            }
            else
            {
                errorProvider3.Icon = Properties.Resources.check;
            }
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(cs);

            string query = "select DATE from DONATION where DATE=@date";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@date", dateTimePicker2.Value);

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows == true)
            {
                errorProvider3.Icon = Properties.Resources.error;
                errorProvider3.SetError(this.dateTimePicker2, "Enter date");
                MessageBox.Show("Invalid.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                date = true;
                errorProvider3.Icon = Properties.Resources.check;
                errorProvider3.SetError(this.dateTimePicker2, "");
            }
            conn.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
