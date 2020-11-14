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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SM2S5FV;Initial Catalog=Stock;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");
            //insert login
            con.Open();
            bool status = false; 
            if (comboBox1.SelectedIndex == 0) { status = true; } else { status = false; };
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [Stock].[dbo].[Products]
           ([ProductCode]
           ,[ProductName]
           ,[ProductStatus])
     VALUES
           ('"+Convert.ToInt32(textBox1.Text)+ "','" + textBox2.Text + "','" + status + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

            //Read data
            LoadData();



        }
        public void LoadData()
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SM2S5FV;Initial Catalog=Stock;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [Stock].[dbo]. [Products]",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                // dataGridView1.Rows[n].Cells[0].Value = item["ProductStatus"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else { dataGridView1.Rows[n].Cells[2].Value = "Deactivate"; }
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            //comboBox1.SelectedText= dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else { comboBox1.SelectedIndex = 1; 
            }
        }
    }
}
