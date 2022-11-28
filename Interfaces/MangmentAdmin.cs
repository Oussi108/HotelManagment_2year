using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Managment
{
    public partial class MangmentAdmin : UserControl
    {
        public MangmentAdmin()
        {
            InitializeComponent();
        }
        HotelmanagmentEntities2 db = new HotelmanagmentEntities2();
        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        bool chekcpic = false;
        string filePath = string.Empty;

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
           
            DialogResult da = openFileDialog1.ShowDialog();
            if (DialogResult.OK== da)
            {
                filePath = openFileDialog1.FileName;
                chekcpic = true;
                Bitmap abc = (Bitmap)System.Drawing.Bitmap.FromFile(filePath);
                guna2PictureBox1.Image = abc;

            }
            else if(da == DialogResult.Cancel) {
            
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            
                int c = 0;
            if (guna2TextBox2.Text == "" ||
               int.TryParse(guna2TextBox3.Text, out c) ||
               filePath == string.Empty
               ) { 
                Food d = new Food();
            d.FoodName = guna2TextBox2.Text;
            d.Price =c;
            d.Img = filePath;

                db.Foods.Add(d);
                db.SaveChanges();
            }
               
               
            
            



        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            filePath = string.Empty;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(guna2TextBox1.Text, out i)
                && DialogResult.Yes == MessageBox.Show("Are you Sure want delete?", "Confirmation", MessageBoxButtons.YesNo)
                )
            {
                var req = from a in db.Foods
                          where a.Id == i
                          select a;
                foreach (var d in req)
                {
                    db.Foods.Remove(d);
                }
                db.SaveChanges();



            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(guna2TextBox1.Text, out i))
            {
                var req = from a in db.Foods
                          where a.Id == i
                          select a;
                foreach (var d in req)
                {
                    int c = 0;
                    if (guna2TextBox2.Text == "" ||
                       int.TryParse(guna2TextBox3.Text, out c) ||
                       filePath == string.Empty
                       )
                    {

                        d.FoodName = guna2TextBox2.Text;
                        d.Price = c;
                        d.Img = filePath;
                    }
                }
                db.SaveChanges();



            }
        }

        private void MangmentAdmin_Load(object sender, EventArgs e)
        {
            var req = from a in db.Foods
                      select new { a.Id, a.FoodName, a.Price,a.Img };

            var req1 = from a in db.Services
                      select new { a.Id, a.TypeService, a.Price };
            guna2DataGridView1.DataSource = req.ToArray();
            guna2DataGridView2.DataSource = req1.ToArray();
            guna2DataGridView1.Columns["Img"].Visible = false;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            int test;
            if (guna2TextBox4.Text == "" ||
               guna2TextBox5.Text == "" ||
               int.TryParse(guna2TextBox5.Text,out test)
               )
            {
                Service d = new Service();
                d.TypeService = guna2TextBox4.Text;
                d.Price = guna2TextBox5.Text;

                db.Services.Add(d);
                db.SaveChanges();
            }

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(guna2TextBox5.Text, out i))
            {
                var req = from a in db.Services
                          where a.Id == i
                          select a;
                foreach (var d in req)
                {
                    int c = 0;
                    if (guna2TextBox4.Text == "" ||
                       guna2TextBox5.Text == "" ||
                       int.TryParse(guna2TextBox5.Text, out c)
                       )
                    {

                        d.TypeService = guna2TextBox4.Text;
                        d.Price = guna2TextBox5.Text;
                    }
                }
                db.SaveChanges();
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(guna2TextBox5.Text, out i)
                && DialogResult.Yes == MessageBox.Show("Are you Sure want delete?","Confirmation",MessageBoxButtons.YesNo)
                )
            {
                var req = from a in db.Services
                          where a.Id == i
                          select a;
                foreach (var d in req)
                {

                    db.Services.Remove(d);
                }
                db.SaveChanges();
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            guna2TextBox1.Text = guna2DataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            guna2TextBox2.Text = guna2DataGridView1.CurrentRow.Cells["FoodName"].Value.ToString();
            guna2TextBox3.Text = guna2DataGridView1.CurrentRow.Cells["Price"].Value.ToString();
            string imgstr = guna2DataGridView1.CurrentRow.Cells["Img"].Value.ToString();

            Bitmap abc = (Bitmap)System.Drawing.Bitmap.FromFile(imgstr);
            guna2PictureBox1.Image = abc;
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBox4.Text = guna2DataGridView2.CurrentRow.Cells["Id"].Value.ToString();
            guna2TextBox5.Text = guna2DataGridView2.CurrentRow.Cells["TypeService"].Value.ToString();
            guna2TextBox6.Text = guna2DataGridView2.CurrentRow.Cells["Price"].Value.ToString();
            
        }
    }
}
