using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Stock
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // to-do chek login username 
            string connString = "server=bendras;user id=dba; password=sql;persistsecurityinfo=True;database=Stock";
            MySqlConnection con = new MySqlConnection(connString);
            //string query = '@SELECT * FROM `Login` WHERE `UserName` = 'admin' AND `Password` = 'admin'';
            MySqlDataAdapter sda = new MySqlDataAdapter(@"SELECT * FROM Login WHERE UserName = '"+ textBox1.Text +"' AND Password = '"+ textBox2.Text +"'",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
                {
                    this.Hide();
                    StockMain main = new StockMain();
                    main.Show();
                }
            else
                {
                MessageBox.Show("Invalid Username or Password ..!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                button1_Click(sender, e);
                }

        }
    }
}
