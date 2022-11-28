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
    public partial class ServiceClient : UserControl
    {
        public ServiceClient()
        {
            InitializeComponent();
        }
        hotelDataContext db = new hotelDataContext();

        private void ServiceClient_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.ColumnHeadersHeight = 20;
            guna2DataGridView2.ColumnHeadersHeight = 20;
            var BB = from a in db.Services
                     select new { a.Id, a.TypeService, a.Price };
            guna2DataGridView1.DataSource = BB.ToArray();

           
            
            if (guna2DataGridView1.Columns.Count < 4)
            {
                DataGridViewButtonColumn dw = new DataGridViewButtonColumn();
                dw.HeaderText = "Select Service";
                dw.Text = "Select";
                dw.UseColumnTextForButtonValue = true;
                guna2DataGridView1.Columns.Add(dw);
            }
            guna2ComboBox1.DataSource = db.Services.ToArray();
            guna2ComboBox1.DisplayMember = "TypeService";
            guna2ComboBox1.ValueMember = "Id";

            var req = from a in db.Reservations
                      where a.UserId == LoginForm.Muser.Id
                      select new { idrm= a.RoomId };
            int K = req.FirstOrDefault().idrm;
            var rq = from b in db.OrderServices
                     where b.RoomId == K
                     select new {b.Id,b.RoomId,b.Service.TypeService,b.SOrderDate };


            guna2DataGridView2.DataSource = rq.ToArray();
            

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2ComboBox1.SelectedValue = guna2DataGridView1.CurrentRow.Cells["Id"].Value;

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure,Ordering Service: " + guna2ComboBox1.Text, "confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var check = from a in db.Reservations
                            where a.UserId == LoginForm.Muser.Id
                            select a;

                if (check.Count() > 0)
                {
                    var req = from a in db.Reservations
                              where a.UserId == LoginForm.Muser.Id
                              select a;
                    OrderService os = new OrderService();
                    os.SOrderDate = DateTime.Now.Date;

                    os.RoomId = req.FirstOrDefault().RoomId;
                    os.ServiceId = int.Parse(guna2ComboBox1.SelectedValue.ToString());
                    db.OrderServices.InsertOnSubmit(os);
                    db.SubmitChanges();
                }
                else { MessageBox.Show("Make Reservation first"); }

            }
        }
    }
}
