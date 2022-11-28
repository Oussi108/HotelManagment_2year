using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Hotel_Managment;

namespace Hotel_Managment
{
    public partial class ManagmentRoom : UserControl
    {
        public ManagmentRoom()
        {
            InitializeComponent();
        }
        hotelDataContext db;

        private void ManagmentRoom_Load(object sender, EventArgs e)
        {
            db = new hotelDataContext();
            var req = from a in db.Rooms
                      select new
                      {a.Id,a.Description,a.Size,a.PriceD,a.available};
            guna2DataGridView1.DataSource = req.ToArray() ;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int c = 0;
            int b = 0;
            if (guna2TextBox3.Text != "" &&
               int.TryParse(guna2TextBox2.Text, out c) &&
               int.TryParse(guna2TextBox4.Text, out b)
               )
            {
                Room R = new Room();
                R.Size = (byte)c;
                R.available = guna2CheckBox1.Checked;
                R.Description = guna2TextBox3.Text;
                R.PriceD = b.ToString();

                db.Rooms.InsertOnSubmit(R);
                db.SubmitChanges();

            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(guna2TextBox1.Text, out i))
            {
                var req = from a in db.Rooms
                          where a.Id == i
                          select a;
                foreach (var R in req)
                {
                    int c = 0;
                    int b = 0;
                    if (guna2TextBox2.Text != "" &&
                       int.TryParse(guna2TextBox2.Text, out c) &&
                       int.TryParse(guna2TextBox4.Text, out b) 
                       )
                    {
                        R.Size = (byte)c;
                        R.available = guna2CheckBox1.Checked;
                        R.Description = guna2TextBox3.Text;
                        R.PriceD = b.ToString();

                    }
                }
                db.SubmitChanges();



            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(guna2TextBox1.Text, out i))
            {
                var req = from a in db.Rooms
                          where a.Id == i
                          select a;
                foreach (var R in req)
                {
                    db.Rooms.DeleteOnSubmit(R);
                }
                db.SubmitChanges();
            }
            }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
            guna2TextBox1.Text = guna2DataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            guna2TextBox3.Text = guna2DataGridView1.CurrentRow.Cells["Description"].Value.ToString();
            guna2TextBox2.Text = guna2DataGridView1.CurrentRow.Cells["Size"].Value.ToString();
            guna2TextBox4.Text = guna2DataGridView1.CurrentRow.Cells["PriceD"].Value.ToString();
            
            bool av =(bool) guna2DataGridView1.CurrentRow.Cells["available"].Value;
            guna2CheckBox1.Checked = av;
        }
    }
}
