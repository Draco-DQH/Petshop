using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Pet_Shop_MS
{
    public partial class Pets : Form
    {
        public Pets()
        {
            InitializeComponent();
            DisplayPets();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DQH_DRACO;Initial Catalog=Petshop;Integrated Security=True");
        private void DisplayPets()
        {
            Con.Open();
            string Query = "Select * from PetTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PetsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Clear()
        {
            PetNameTb.Text = "";
            CatCb.SelectedIndex = 0;
            QtyTb.Text = "";
            PriceTb.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PetNameTb.Text == "" || CatCb.SelectedIndex == -1 || QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into PetTbl ([Tên],[Loài],[Số lượng],[Giá]) values (@PN,@PC,@PQ,@PP)", Con);
                    cmd.Parameters.AddWithValue("@PN", PetNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", CatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    Con.Close();
                    DisplayPets();
                    Clear();
                }
            }
        }
        int key = 0;
        private void PetsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PetNameTb.Text = PetsDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.Text = PetsDGV.SelectedRows[0].Cells[2].Value.ToString();
            QtyTb.Text = PetsDGV.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = PetsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PetNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(PetsDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PetNameTb.Text == "" || CatCb.SelectedIndex == -1 || QtyTb.Text == "" || PriceTb.Text == "")
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update PetTbl set [Tên]=@PN,[Loài]=@PC,[Số lượng]=@PQ,[Giá]=@PP where [Mã]=@Pkey", Con);
                    cmd.Parameters.AddWithValue("@PN", PetNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", CatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.Parameters.AddWithValue("@Pkey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thành công!");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    Con.Close();
                    DisplayPets();
                    Clear();
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Xin hãy chọn thú cưng!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from PetTbl where [Mã] = @PKey", Con);
                    cmd.Parameters.AddWithValue("@PKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Xoá thành công!");
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    Con.Close();
                    DisplayPets();
                    Clear();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Homes Obj = new Homes();
            Obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings Obj = new Billings();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
          
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
    }
}
