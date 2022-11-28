using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace Hotel_Managment
{
    public partial class FoodClient : UserControl
    {
        static public Food fd;
        HotelmanagmentEntities2 db = new HotelmanagmentEntities2();
        static void raise(Food a)
        {
            fd = a;
            
            
        }
        public FoodClient()
        {

            InitializeComponent();
        }
        SqlConnection cnn = new SqlConnection("Data Source=.;Initial Catalog=Hotelmanagment;Integrated Security=True;Pooling=False");
        SqlDataAdapter da;
        private void FoodClient_Load(object sender, EventArgs e)
        {

            guna2ComboBox1.DataSource = db.Foods.ToArray();
            guna2ComboBox1.DisplayMember = "FoodName";
            guna2ComboBox1.ValueMember = "Id";

            int x = 0;
            int y = 0;
            int count = 0;
            da = new SqlDataAdapter("select * from foods", cnn);
            DataTable dt = new DataTable("foods");
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Food f = new Food();
                f.FoodName = dt.Rows[i][1].ToString();
                f.Id = int.Parse(dt.Rows[i][0].ToString());
                f.Price = int.Parse(dt.Rows[i][2].ToString());
                f.Img = dt.Rows[i][3].ToString();
                fooditem Fi = new fooditem(f);

                Fi.Location = new Point(x, y);
                count++;
                x += Fi.Size.Width;
                Fi.Controls["guna2Button1"].Click += guna2ComboBox1_wantedchange;
                flowLayoutPanel1.Controls.Add(Fi);
                if (count % 3 == 0)
                {
                    x = 0;
                    y += Fi.Size.Height;

                }

               


                /*
                int x = 0;
                int y = 0;
                int count = 0;
                foreach (var item in db.Foods)
                {
                    fooditem Fi = new fooditem(item);



                    Fi.Location = new Point(x,y);
                    count++;
                    x += Fi.Size.Width;
                    guna2Panel1.Controls.Add(Fi);
                    if (count % 3 == 0)
                    {
                        x = 0;
                        y += Fi.Size.Height;

                    }
                }*/
            }
        }


        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_wantedchange(object sender, EventArgs e)
        {
            
            if (!(fd is null))
            {
                
                guna2ComboBox1.SelectedValue = fd.Id;

            }

        }

        private void guna2NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int i = int.Parse(guna2ComboBox1.SelectedValue.ToString());
            var req = from a in db.Foods
                      where a.Id == i
                      select a;
            gunaLabel5.Text = ((decimal)req.FirstOrDefault().Price * guna2NumericUpDown1.Value).ToString();
        }

        private void guna2NumericUpDown1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure,Ordering Food "+guna2NumericUpDown1.Value.ToString() +" : " + guna2ComboBox1.Text  , "confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                OrderFood of = new OrderFood();
                var req = from a in db.Reservations
                          where a.UserId == LoginForm.Muser.Id
                          select a;
               
                of.quantity = (int)guna2NumericUpDown1.Value;
                of.RoomId = req.FirstOrDefault().RoomId;
                of.FoodId = (int)guna2ComboBox1.SelectedValue;
                of.OrderDate = DateTime.Now.Date;
                db.OrderFoods.Add(of);
                db.SaveChanges();



            }
        }
    }
}