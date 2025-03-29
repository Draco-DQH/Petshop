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

namespace Pet_Shop_MS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DQH_DRACO;Initial Catalog=Petshop;Integrated Security=True");
        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public static string User;
        private void LoginBtn_Click(object sender, EventArgs e)
        {

            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count(*) from EmployeeTbl where [Tài khoản] = '"+ UserTb.Text+"' and [Mật khẩu]='"+PassTb.Text+"'", Con );
            DataTable dt = new DataTable();
           sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                User = UserTb.Text;
                Homes Obj  = new Homes();
                Obj.Show();
                this.Hide();
            }else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!");
            }
            Con.Close();
        }

        private void PassTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }
    }
}
