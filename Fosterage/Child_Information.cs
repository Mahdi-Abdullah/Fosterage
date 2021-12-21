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
    public partial class Child_Info : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        public Child_Info()
        {
            InitializeComponent();
            BindGridView();
            BindGridView2();
            BindGridView3();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        void BindGridView()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT PICTURE, NAME, AGE, GENDER, RELIGION FROM CHILD_TBL WHERE USERNAME = @user OR FOSTER=@user";
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
                dataGridView1.RowTemplate.Height = 70;
            } 
        }

        void BindGridView2()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT FATHER, MOTHER, RELIGION, PHONE, EMAIL, CURRENT_ADDRESS FROM CHILD_TBL WHERE USERNAME = @user OR FOSTER=@user";
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
                dataGridView2.DataSource = data;

                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView2.RowTemplate.Height = 70;
            }
        }

        void BindGridView3()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT Name,AGE,RELIGION,EMAIL,PHONE,ADDRESS,PICTURE FROM Foster_tbl WHERE CHILD = @user OR USERNAME = @user";
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
                dataGridView3.DataSource = data;

                DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                dgv = (DataGridViewImageColumn)dataGridView3.Columns[6];
                dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView3.RowTemplate.Height = 100;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
