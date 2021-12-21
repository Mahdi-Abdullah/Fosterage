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
    public partial class GHomepage : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        public GHomepage()
        {
            InitializeComponent();
            BindGridView();

            SqlConnection conn = new SqlConnection(cs);
            string query = "select username from CHILD_TBL where username=@user and FOSTER='pending'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@user", Form2.user);

            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                conn.Close();
                dataGridView2.Size = new Size(1017, 311);
                label1.Visible = true;
                dataGridView1.Visible = true;
                BindGridView2();
            }
        }
        void BindGridView()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT Name,AGE,RELIGION,EMAIL,PHONE,ADDRESS,PICTURE FROM Foster_tbl WHERE CHILD = 'NONE'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView2.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView2.Columns[6];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.RowTemplate.Height = 100;
        }

        void BindGridView2()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT PICTURE,NAME,AGE,GENDER,FATHER,MOTHER,RELIGION,PHONE,EMAIL,CURRENT_ADDRESS FROM CHILD_TBL WHERE USERNAME=@user";
            SqlCommand comm = new SqlCommand(query, conn);
            comm.Parameters.AddWithValue("@user", Form2.user);
            SqlDataAdapter sda = new SqlDataAdapter(comm);

            conn.Open();
            SqlDataReader rdr = comm.ExecuteReader();
            if (rdr.HasRows)
            {
                conn.Close();
                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;

                DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                dgv = (DataGridViewImageColumn)dataGridView1.Columns[0];
                dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.RowTemplate.Height = 100;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);
                string query = "SELECT Name,AGE,RELIGION,EMAIL,PHONE,ADDRESS,PICTURE FROM Foster_tbl WHERE NAME=@name AND CHILD = 'NONE'";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@name", textBox1.Text);
                SqlDataAdapter sda = new SqlDataAdapter(comm);

                conn.Open();
                SqlDataReader rdr = comm.ExecuteReader();
                if (rdr.HasRows)
                {
                    conn.Close();
                    DataTable data = new DataTable();
                    sda.Fill(data);
                    dataGridView2.DataSource = data;

                    DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                    dgv = (DataGridViewImageColumn)dataGridView2.Columns[6];
                    dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.RowTemplate.Height = 100;
                }
                else
                {
                    MessageBox.Show("Not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (textBox1.Text == "")
            {
                SqlConnection conn = new SqlConnection(cs);
                string query = "SELECT Name,AGE,RELIGION,EMAIL,PHONE,ADDRESS,PICTURE FROM Foster_tbl WHERE CHILD = 'NONE'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView2.DataSource = data;

                DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                dgv = (DataGridViewImageColumn)dataGridView2.Columns[6];
                dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.RowTemplate.Height = 100;
            }
            else
            {
                MessageBox.Show("Not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT Name,AGE,RELIGION,EMAIL,PHONE,ADDRESS,PICTURE FROM Foster_tbl WHERE CHILD = 'NONE'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView2.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView2.Columns[6];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.RowTemplate.Height = 100;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
