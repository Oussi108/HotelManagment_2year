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
    public partial class ManagerClient : UserControl
    {
        hotelDataContext db = new hotelDataContext();
        public ManagerClient()
        {
            InitializeComponent();
        }

        private void ManagerClient_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.ColumnHeadersHeight = 40;
            var req = from a in db.Reservations
                      select new
                      {
                          a.User.Id,
                          a.User.Username,
                          Room_ID = a.Room.Id,
                          a.Room.Description
                      };
            guna2DataGridView1.DataSource = req.ToArray();
            guna2ComboBox1.DataSource = req.ToArray();
            guna2ComboBox2.DataSource = req.ToArray();
            guna2ComboBox1.DisplayMember = "Room_ID";
            guna2ComboBox2.DisplayMember = "Room_ID";

            guna2ComboBox3.DataSource = db.Foods.ToArray() ;
            guna2ComboBox3.DisplayMember = "FoodName";
            guna2ComboBox3.ValueMember = "Id";

            guna2ComboBox4.DataSource = db.Services.ToArray() ;
            guna2ComboBox4.DisplayMember = "TypeService";
            guna2ComboBox4.ValueMember = "Id";

            if (guna2DataGridView1.Columns.Count < 5)
            {
                DataGridViewButtonColumn dw = new DataGridViewButtonColumn();
                dw.HeaderText = "Select Client";
                dw.Text = "Select";
                dw.UseColumnTextForButtonValue = true;
                guna2DataGridView1.Columns.Add(dw);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2ComboBox1.Text = guna2DataGridView1.CurrentRow.Cells["Room_ID"].Value.ToString();
            guna2ComboBox2.Text = guna2DataGridView1.CurrentRow.Cells["Room_ID"].Value.ToString();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            int roomid = int.Parse(guna2ComboBox1.Text);
            int foodid = int.Parse(guna2ComboBox3.SelectedValue.ToString());
            int Tprice = (int)(guna2NumericUpDown1.Value) * db.Foods.Where(x => x.Id == foodid).FirstOrDefault().Price;
            if (MessageBox.Show("Are you Sure,Ordering Food " + guna2NumericUpDown1.Value.ToString() + " : " + guna2ComboBox3.Text +" Total  : "+Tprice+" Mad", "confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OrderFood of = new OrderFood();
                var req = from a in db.Reservations
                          where a.UserId == LoginForm.Muser.Id
                          select a;

                of.quantity = (int)guna2NumericUpDown1.Value;
                of.RoomId = roomid;
                of.FoodId = foodid;
                of.OrderDate = DateTime.Now.Date;
                db.OrderFoods.InsertOnSubmit(of);
                MessageBox.Show("submit succesfuly", "Validation");
                db.SubmitChanges();



            }
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            int roomid = int.Parse(guna2ComboBox2.Text);
            int serviceid = int.Parse(guna2ComboBox4.SelectedValue.ToString());
            int Tprice = int.Parse(db.Services.Where(x => x.Id == serviceid).FirstOrDefault().Price);
            if (MessageBox.Show("Are you Sure,Ordering Food " + guna2NumericUpDown1.Value.ToString() + " : " + guna2ComboBox3.Text + " Total  : " + Tprice + " Mad", "confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OrderService of = new OrderService();
                var req = from a in db.Reservations
                          where a.UserId == LoginForm.Muser.Id
                          select a;


                of.RoomId = roomid;
                of.ServiceId = serviceid;
                of.SOrderDate = DateTime.Now.Date;
                db.OrderServices.InsertOnSubmit(of);
                MessageBox.Show("submit succesfuly", "Validation");
                db.SubmitChanges();
            }
        }
    }
}
