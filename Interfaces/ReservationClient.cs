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

namespace Hotel_Managment
{
    public partial class ReservationClient : UserControl
    {
        
        HotelmanagmentEntities2 db = new HotelmanagmentEntities2();
        public ReservationClient()
        {
            
            
            InitializeComponent();
        }

        private void ReservationClient_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.ColumnHeadersHeight = 20;

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            
            if(guna2DateTimePicker1.Value > guna2DateTimePicker2.Value)
            {
                MessageBox.Show("Date error");

            }
            else
            {
                SqlConnection cnn = new SqlConnection(@"Data Source =.; Initial Catalog = Hotelmanagment; Integrated Security = True; Pooling = False");
                SqlCommand cmd = new SqlCommand("freerooms", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@D1",guna2DateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@D2", guna2DateTimePicker2.Value);
                DataTable dt = new DataTable("FreeRooms");
                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();

                guna2DataGridView1.DataSource = dt;
                if(guna2DataGridView1.Columns.Count < 5) { 
                DataGridViewButtonColumn dw = new DataGridViewButtonColumn();
                dw.HeaderText = "Select Room";
                dw.Text = "Select";
                dw.UseColumnTextForButtonValue = true;
                guna2DataGridView1.Columns.Add(dw);}
                guna2ComboBox1.DataSource = dt;
                guna2ComboBox1.DisplayMember="Id";
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2ComboBox1.Text = guna2DataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void guna2DateTimePicker4_CloseUp(object sender, EventArgs e)
        {

            calculPrix();
        }
        void calculPrix()
        {
            int d = int.Parse(guna2ComboBox1.Text);
            var req = from a in db.Rooms
                      where a.Id == d
                      select a;
            TimeSpan timediff = guna2DateTimePicker3.Value - guna2DateTimePicker4.Value;
            int datedif = Convert.ToInt32(timediff.TotalDays);
            label8.Text = (Math.Abs(datedif * (int.Parse(req.FirstOrDefault().PriceD)))).ToString();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2DateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            calculPrix();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes == MessageBox.Show("Are you sure you want reservation form "+ guna2DateTimePicker3.Value.ToShortDateString()+"to " + guna2DateTimePicker3.Value.ToShortDateString() + "in Room number"+ guna2ComboBox1.Text, "Confirmation", MessageBoxButtons.YesNo) )
            {
                SqlConnection cnn = new SqlConnection(@"Data Source =.; Initial Catalog = Hotelmanagment; Integrated Security = True; Pooling = False");
                SqlCommand cmd = new SqlCommand("Addreservation", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@D1", guna2DateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@D2", guna2DateTimePicker2.Value);
                cmd.Parameters.AddWithValue("@userid", LoginForm.Muser.Id);
                cmd.Parameters.AddWithValue("@roomid", guna2ComboBox1.Text);

                cnn.Open();
                if (cmd.ExecuteNonQuery() > 0) MessageBox.Show("Reservation done");
                cnn.Close();
            }
            



        }
    }
}
