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
    public partial class Viewdonation : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["FDB"].ConnectionString;
        public Viewdonation()
        {
            InitializeComponent();
            BindGridView();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        void BindGridView()
        {
            SqlConnection conn = new SqlConnection(cs);
            string query = "SELECT USERNAME,AMOUNT, DATE FROM DONATION";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);

            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowTemplate.Height = 100;
        }
    }
}
