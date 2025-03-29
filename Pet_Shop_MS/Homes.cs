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
    public partial class Homes : Form
    {
        public Homes()
        {
            InitializeComponent();
            CountDogs();
            CountBirds();
            CountCats();
            CountFishes();
            Finance();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DQH_DRACO;Initial Catalog=Petshop;Integrated Security=True");
        private void CountDogs()
        {
            string Cat = "Chó";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from PetTbl where [Loài]='"+ Cat +"'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DogsLbl.Text = dt. Rows[0][0].ToString();
            Con.Close();
        }
        private void CountBirds()
        {
            string Cat = "Chim";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from PetTbl where [Loài]='" + Cat + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BirdsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountCats()
        {
            string Cat = "Mèo";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from PetTbl where [Loài]='" + Cat + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CatsLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountFishes()
        {
            string Cat = "Cá";
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from PetTbl where [Loài]='" + Cat + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            FishesLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void Finance()
        {
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select Sum([Doanh thu]) from BillTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            PriceLbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCircleProgress1_ProgressChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCircleProgress1_ProgressChanged_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings Obj = new Billings();
            Obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Pets Obj = new Pets();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
          
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }
    }
}
