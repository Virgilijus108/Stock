using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connString = "server=bendras;user id=dba; password=sql;persistsecurityinfo=True;database=Stock";
            MySqlConnection con = new MySqlConnection(connString);
            con.Open();
            int status = 0;
            if (comboBox1.SelectedIndex == 0)
            {
                status = 1;
            }
            else {
                status = 0;
            }
            var sqlQuery = "";
            if (IfProductsExists(con, textBox1.Text))
            {
                sqlQuery = @"UPDATE Products SET ProductName = '" + textBox2.Text + "',ProductStatus = '" + status + "' WHERE ProductCode = '" + textBox1.Text + "'";
            }
            else
            {
                sqlQuery = @"INSERT INTO Products(ProductCode, ProductName, ProductStatus) VALUES('" + textBox1.Text + "', '" + textBox2.Text + "', '" + status + "')";
            }
            
            MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
            cmd.ExecuteNonQuery();
            con.Close();

            LoadData();
            
        }

        private bool IfProductsExists(MySqlConnection con, string productCode)
        {
            MySqlDataAdapter sda = new MySqlDataAdapter("Select 1 From Products Where ProductCode ='" + productCode +"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
            
        }

        public void LoadData()
        {
            string connString = "server=bendras;user id=dba; password=sql;persistsecurityinfo=True;database=Stock";
            MySqlConnection con = new MySqlConnection(connString);

            MySqlDataAdapter sda = new MySqlDataAdapter("Select * From Products", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }

            }
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connString = "server=bendras;user id=dba; password=sql;persistsecurityinfo=True;database=Stock";
            MySqlConnection con = new MySqlConnection(connString);

            var sqlQuery = "";
            if (IfProductsExists(con, textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM Products WHERE ProductCode = '" + textBox1.Text + "'";
                MySqlCommand cmd = new MySqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Record Not Exists....!");
            }

            LoadData();

        }
    }
}
