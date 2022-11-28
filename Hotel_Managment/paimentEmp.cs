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
    public partial class paimentEmp : UserControl
    {
        public paimentEmp()
        {
            InitializeComponent();
        }
        hotelDataContext db = new hotelDataContext();
        private void paimentEmp_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.ColumnHeadersHeight = 20;
            guna2DataGridView2.ColumnHeadersHeight = 20;
            var req = from a in db.Reservations
                      select new
                      {
                          a.User.Id,
                          a.User.Username,
                          Room_ID = a.Room.Id,
                          a.Room.Description
                      };
            guna2DataGridView1.DataSource = req.ToArray();

            var req1 = from a in db.Users
                       where a.PremissionId == 1
                       select new {a.Username,a.Id };

            guna2ComboBox1.DataSource = req1.ToArray();
            guna2ComboBox1.DisplayMember = "Username";
            guna2ComboBox1.ValueMember = "Id";


            if (guna2DataGridView1.Columns.Count < 5)
            {
                DataGridViewButtonColumn dw = new DataGridViewButtonColumn();
                dw.HeaderText = "Select Client";
                dw.Text = "Select";
                dw.UseColumnTextForButtonValue = true;
                guna2DataGridView1.Columns.Add(dw);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int user = -1;
            int.TryParse(guna2ComboBox1.SelectedValue.ToString(),out user);
           
            
            
            

            DataTable dt = new DataTable("ListOrders");
            dt.Columns.Add("Type of service", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("Price", typeof(string));
            dt.Columns.Add("Order Price", typeof(int));
            var req2 = from b in db.Reservations
                       where b.User.Id == user
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
            guna2DataGridView2.DataSource = dt;
            

            // Declare an object variable.
            string sumObject;
            sumObject = dt.AsEnumerable().Sum(x => x.Field<int>("Order Price")).ToString();
            label6.Text = sumObject;
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2ComboBox1.SelectedValue = guna2DataGridView1.CurrentRow.Cells["Id"].Value;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int user = -1;
            int.TryParse(guna2ComboBox1.SelectedValue.ToString(), out user);
            TheBillClient f = new TheBillClient(user);
            f.ShowDialog();
        }
    }
}
