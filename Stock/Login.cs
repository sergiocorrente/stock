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

namespace Stock
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {   //To Do check username and password
            textBox1.Text = "admin";
            textBox2.Text = "admin123";

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-SM2S5FV;Initial Catalog=Stock;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM[Stock].[dbo].[Login] where Username = '" + textBox1.Text + "' and Password = '" + textBox2.Text + "'", con);
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
                MessageBox.Show("Invalid username & password..! ","Error ", MessageBoxButtons.OK , MessageBoxIcon.Error);
                button1_Click(sender, e);
            }



        }
    }
}
