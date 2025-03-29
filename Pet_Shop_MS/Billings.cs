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
    public partial class Billings : Form
    {
        public Billings()
        {
            InitializeComponent();
            EmpNameLbl.Text = Login.User;
            GetCustomer();
            DisplayPets();
            DisplayTrans();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DQH_DRACO;Initial Catalog=Petshop;Integrated Security=True");
        private void GetCustomer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select Mã from CustomerTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã", typeof(int));
            dt.Load(Rdr);
            CustIdCb.ValueMember = "Mã";
            CustIdCb.DataSource = dt;
            Con.Close();
        }
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
        private void DisplayTrans()
        {
            Con.Open();
            string Query = "Select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            TransDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void GetCustName()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl where [Mã] = '" + CustIdCb.SelectedValue.ToString() + "'";

            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["Họ và tên"].ToString();
            }
            Con.Close();
           
        }
        private void UpdateStock()
        {
            try
            {
                int NewQty = Stock - Convert.ToInt32(QtyTb.Text);
                Con.Open ();
                SqlCommand cmd = new SqlCommand ("Update PetTbl set [Số lượng]=@PQ where [Mã]=@Pkey", Con );
                
                cmd.Parameters.AddWithValue("@PQ", NewQty);
                cmd.Parameters.AddWithValue("@Pkey", key);
                cmd.ExecuteNonQuery ();
                Con.Close();
                DisplayPets();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            /*finally
            {
                Con.Close();
                DisplayPets();
                //Clear();
            }*/
        }
        //int n = 0;
        int GrdTotal = 0;
        int key = 0, Stock = 0;
        private void Reset()
        {
            PetNameTb.Text = "";
            PriceTb.Text = "";
            QtyTb.Text = "";
            Stock = 0;
            key = 0;
        }
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void PetsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PetNameTb.Text = PetsDGV.SelectedRows[0].Cells[1].Value.ToString();
            Stock = Convert.ToInt32(PetsDGV.SelectedRows[0].Cells[3].Value.ToString()); 
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
        private void InsertBill()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into BillTbl ([Ngày],[Mã KH],[Họ và tên],[Họ và tên nv],[Doanh thu]) values(@BD,@CI,@CN,@EN,@Am)", Con);
                cmd.Parameters.AddWithValue("@BD", DateTime.Today.Date);
                cmd.Parameters.AddWithValue("@CI", CustIdCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                cmd.Parameters.AddWithValue("@EN", EmpNameLbl.Text);
                cmd.Parameters.AddWithValue("@Am", GrdTotal);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thanh toán thành công!!!");    
                /*Con.Close();
                DisplayTrans();*/
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                Con.Close();
                DisplayTrans();
            }
        }
        private void Printbtn_Click(object sender, EventArgs e)
        {
            InsertBill();
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 325, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }   
            
        }

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BillDGV.Columns[0].HeaderText = "A";
            BillDGV.Columns[1].HeaderText = "B";
            BillDGV.Columns[2].HeaderText = "C";
            BillDGV.Columns[3].HeaderText = "D";
            BillDGV.Columns[4].HeaderText = "E";
        }
        int pid, pqty, pprice, tottal, pos = 60;

        private void label3_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
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

        private void CustIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }

        private void Billings_Load(object sender, EventArgs e)
        {
          
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Pets Obj = new Pets();
            Obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Homes Obj = new Homes();
            Obj.Show();
            this.Hide();
        }

        string pname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Cửa Hàng Thú Cưng", new Font("Segoe UI", 14, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("Mã  Tên     Số lượng      Giá           Tổng", new Font("Segoe UI", 10, FontStyle.Bold), Brushes.Red, new Point(20, 40));
            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                pid = Convert.ToInt32(row.Cells["Column1"].Value);
                pname = "" + row.Cells["Column2"].Value;
                pprice = Convert.ToInt32(row.Cells["Column3"].Value);
                pqty = Convert.ToInt32(row.Cells["Column4"].Value);
                tottal = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + pid, new Font("Segoe UI", 8, FontStyle.Bold), Brushes.Blue, new Point(30,pos));
                e.Graphics.DrawString("" + pname, new Font("Segoe UI", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + pprice, new Font("Segoe UI", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + pqty, new Font("Segoe UI", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + tottal, new Font("Segoe UI", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Tổng: " + GrdTotal +" VND", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos +50));
            e.Graphics.DrawString("***************Pet_Shop***************", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            //n= 0;
           
        }

        private void CustIdCb_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            int n = 0;
            if (QtyTb.Text == "" || Convert.ToInt32 (QtyTb.Text) > Stock)
            {
                MessageBox.Show("Tiếc quá! Store không đủ mất rồi...");
            }
            else if(QtyTb.Text == "" || key == 0)
            {
                MessageBox.Show("Xin hãy điền số lượng!");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PriceTb.Text);

                

                string[] row = new string[]
                {
                    (n+1).ToString(),
                     PetNameTb.Text,
                     QtyTb.Text,
                     PriceTb.Text,
                     total.ToString(),
                };
                GrdTotal = GrdTotal + total;
                BillDGV.Rows.Add(row);
                


                //DataGridViewRow newRow = new DataGridViewRow();
                //newRow.CreateCells(BillDGV);
                //newRow.Cells[0].Value = n + 1;
                //newRow.Cells[1].Value = PetNameTb.Text;
                //newRow.Cells[2].Value = QtyTb.Text;
                //newRow.Cells[3].Value = PriceTb.Text;
                //newRow.Cells[4].Value = total;
                //GrdTotal = GrdTotal + total;
                //BillDGV.Rows.Add(newRow);
                n++;
                TotalLbl.Text = GrdTotal + " VND";
                UpdateStock();
                Reset();
            }    
        }
    }
}
