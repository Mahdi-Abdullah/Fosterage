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
    public partial class Contactlist : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        public Contactlist()
        {
            InitializeComponent();
            BindGridView();
            BindGridView2();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        void BindGridView()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT NAME,FATHER,MOTHER,PHONE,EMAIL,CURRENT_ADDRESS FROM CHILD_TBL";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 100;
        }

        void BindGridView2()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT Name,EMAIL,PHONE,ADDRESS FROM Foster_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView2.DataSource = data;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.RowTemplate.Height = 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);
                string query = "SELECT NAME,FATHER,MOTHER,PHONE,EMAIL,CURRENT_ADDRESS FROM CHILD_TBL WHERE NAME = @name";
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
                    dataGridView1.DataSource = data;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.RowTemplate.Height = 100;
                }
                else
                {
                    MessageBox.Show("Not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (textBox1.Text == "")
            {
                SqlConnection conn = new SqlConnection(cs);
                string query = "SELECT NAME,FATHER,MOTHER,PHONE,EMAIL,CURRENT_ADDRESS FROM CHILD_TBL";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.RowTemplate.Height = 100;
            }
            else
            {
                MessageBox.Show("Not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT NAME,FATHER,MOTHER,PHONE,EMAIL,CURRENT_ADDRESS FROM CHILD_TBL";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 100;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);
                string query = "SELECT Name,EMAIL,PHONE,ADDRESS FROM Foster_tbl WHERE NAME = @name";
                SqlCommand comm = new SqlCommand(query, conn);
                comm.Parameters.AddWithValue("@name", textBox2.Text);
                SqlDataAdapter sda = new SqlDataAdapter(comm);

                conn.Open();
                SqlDataReader rdr = comm.ExecuteReader();
                if (rdr.HasRows)
                {
                    conn.Close();
                    DataTable data = new DataTable();
                    sda.Fill(data);
                    dataGridView2.DataSource = data;

                    dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView2.RowTemplate.Height = 100;
                }
                else
                {
                    MessageBox.Show("Not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (textBox2.Text == "")
            {
                SqlConnection conn = new SqlConnection(cs);
                string query = "SELECT Name,EMAIL,PHONE,ADDRESS FROM Foster_tbl";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView2.DataSource = data;

                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.RowTemplate.Height = 100;
            }
            else
            {
                MessageBox.Show("Not Found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT Name,EMAIL,PHONE,ADDRESS FROM Foster_tbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView2.DataSource = data;

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.RowTemplate.Height = 100;
        }
    }
}
