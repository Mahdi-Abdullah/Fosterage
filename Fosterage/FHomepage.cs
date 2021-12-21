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
    public partial class FHomepage : Form
    {
        
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        string name;
        public FHomepage()
        {
            InitializeComponent();
            BindGridView();
        }
        void BindGridView()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT PICTURE,NAME,AGE,GENDER,FATHER,MOTHER,RELIGION,PHONE,EMAIL,CURRENT_ADDRESS FROM CHILD_TBL WHERE FOSTER ='PENDING'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[0];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                SqlConnection conn = new SqlConnection(cs);
                string query = "SELECT PICTURE,NAME,AGE,GENDER,FATHER,MOTHER,RELIGION,PHONE,EMAIL,CURRENT_ADDRESS FROM CHILD_TBL WHERE FOSTER ='pending' AND NAME = @name";
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

                    DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                    dgv = (DataGridViewImageColumn)dataGridView1.Columns[0];
                    dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

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
                string query = "SELECT PICTURE,NAME,AGE,GENDER,FATHER,MOTHER,RELIGION,PHONE,EMAIL,CURRENT_ADDRESS FROM CHILD_TBL WHERE FOSTER ='PENDING'";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);

                DataTable data = new DataTable();
                sda.Fill(data);
                dataGridView1.DataSource = data;

                DataGridViewImageColumn dgv = new DataGridViewImageColumn();
                dgv = (DataGridViewImageColumn)dataGridView1.Columns[0];
                dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

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
            string query = "SELECT PICTURE,NAME,AGE,GENDER,FATHER,MOTHER,RELIGION,PHONE,EMAIL,CURRENT_ADDRESS FROM CHILD_TBL WHERE FOSTER ='PENDING'";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            DataGridViewImageColumn dgv = new DataGridViewImageColumn();
            dgv = (DataGridViewImageColumn)dataGridView1.Columns[0];
            dgv.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 100;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                SqlConnection conn = new SqlConnection(cs);
                string query3 = "SELECT USERNAME FROM CHILD_TBL WHERE PHONE = @phone";
                SqlCommand cmdr = new SqlCommand(query3, conn);
                cmdr.Parameters.AddWithValue("@phone", dataGridView1.Rows[e.RowIndex].Cells["PHONE"].Value.ToString());

                conn.Open();
                SqlDataReader rd = cmdr.ExecuteReader();

                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        name = rd.GetString(0);
                    }
                }
                conn.Close();


                string querys = "SELECT * FROM Foster_tbl WHERE USERNAME=@user AND CHILD='NONE'";
                SqlCommand cmds = new SqlCommand(querys, conn);
                cmds.Parameters.AddWithValue("@user", Form2.user);

                conn.Open();
                SqlDataReader rdrs = cmds.ExecuteReader();

                if (rdrs.HasRows == true)
                {
                    conn.Close();
                    if (MessageBox.Show("Do you want to be assign to this child?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        string query = "UPDATE CHILD_TBL SET FOSTER=@user WHERE USERNAME=@name";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@user", Form2.user);
                        cmd.Parameters.AddWithValue("@name", name);
                        conn.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        conn.Close();

                        string query2 = "UPDATE Foster_tbl SET CHILD=@uname WHERE USERNAME=@user";
                        SqlCommand cmmd = new SqlCommand(query2, conn);
                        cmmd.Parameters.AddWithValue("@uname", name);
                        cmmd.Parameters.AddWithValue("@user", Form2.user);

                        conn.Open();
                        SqlDataReader sdr = cmmd.ExecuteReader();
                        conn.Close();



                        if (MessageBox.Show("Success, You are now assign to this child. Please login again", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            Gurdian.time = true;
                            Application.Restart();
                        }
                    }
                }
                else
                {
                    conn.Close();
                    MessageBox.Show("You are not registered or already assign to a child.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }   
        }

        
    }
}
