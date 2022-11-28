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
using System.Data.Objects.DataClasses;


namespace Hotel_Managment
{
      
    public partial class Paiment : UserControl
    {
        HotelmanagmentEntities2 db = new HotelmanagmentEntities2();
        public Paiment()
        {
            InitializeComponent();
        }
        int idcard=-1;

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            var BB = from a in db.CreditCards
                     select new { a.Id, a.CardNum, a.Pin };
            guna2DataGridView2.DataSource = BB.ToArray();

            if (guna2DataGridView2.Columns.Count < 4)
            {
                DataGridViewButtonColumn dw = new DataGridViewButtonColumn();
                dw.HeaderText = "Select card";
                dw.Text = "Select";
                dw.UseColumnTextForButtonValue = true;
                guna2DataGridView2.Columns.Add(dw);
            }



        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(!(guna2DataGridView1.CurrentRow is null)) {
            guna2TextBox1.Text =  guna2DataGridView2.CurrentRow.Cells["CardNum"].Value.ToString();
            guna2TextBox4.Text = guna2DataGridView2.CurrentRow.Cells["Pin"].Value.ToString();
            idcard = (int)guna2DataGridView2.CurrentRow.Cells["Id"].Value;
            //}
        }
        
        private void Paiment_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.ColumnHeadersHeight = 20;
            guna2DataGridView2.ColumnHeadersHeight = 20;
            var req = from a in db.CreditCards
                      where a.UserId == LoginForm.Muser.Id
                      select a;
            guna2ComboBox1.DataSource = req.ToArray();
            guna2ComboBox1.DisplayMember = "CardNum";
            guna2ComboBox1.ValueMember = "Id";

            DataTable dt = new DataTable("ListOrders");
            dt.Columns.Add("Type of service", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Order Price", typeof(string));
            var req2 = from b in db.Reservations
                       where b.User.Id == LoginForm.Muser.Id
                       select b;
            
            

             
            
            foreach (var item in req2)
            {
                TimeSpan timediff = item.EndDate.Date - item.StartDate.Date;
                int datedif = Convert.ToInt32(timediff.TotalDays);
                dt.Rows.Add(item.Room.Description, (datedif).ToString() + "Days", item.Room.PriceD.ToString() + " a day", datedif * int.Parse(item.Room.PriceD));

                foreach (var item1 in item.Room.OrderFoods)
                {
                    dt.Rows.Add(item1.Food.FoodName, item1.quantity, item1.Food.Price, item1.Food.Price * item1.quantity);

                }
                foreach (var item1 in item.Room.OrderServices)
                {
                    dt.Rows.Add(item1.Service.TypeService, 1, item1.Service.Price, item1.Service.Price);

                }

            }
            guna2DataGridView1.DataSource = dt;

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            var req = from a in db.CreditCards
                      where a.UserId == LoginForm.Muser.Id
                      select a;
            int C;
            if (int.TryParse(guna2TextBox1.Text, out C) && C.ToString().Length == 12
                    && int.TryParse(guna2TextBox4.Text, out C) && C.ToString().Length == 3)
            {
                foreach (var item in req)
                {
                    if (idcard < 0)
                    {
                        if (item.Id == idcard)
                        {
                            item.CardNum = guna2TextBox1.Text;
                            item.Pin = int.Parse(guna2TextBox4.Text);
                            db.SaveChanges();
                        }
                    }

                }
            }
        }
        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure,Add Card " + guna2TextBox1.Text, "confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int C;
                if (int.TryParse(guna2TextBox1.Text, out C) && C.ToString().Length == 12
                    && int.TryParse(guna2TextBox4.Text, out C) && C.ToString().Length == 3)
                {
                    CreditCard cr = new CreditCard();
                    var req = from a in db.Users
                              where a.Id == LoginForm.Muser.Id
                              select a;

                    cr.CardNum = guna2TextBox1.Text;
                    cr.Pin = int.Parse(guna2TextBox4.Text);
                    cr.UserId = LoginForm.Muser.Id;
                    db.CreditCards.Add(cr);
                    db.SaveChanges();
                }
            }


            
        }
    }
}
