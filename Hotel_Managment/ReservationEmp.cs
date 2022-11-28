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
    public partial class ReservationEmp : UserControl
    {
        public ReservationEmp()
        {
            InitializeComponent();
        }
        hotelDataContext db = new hotelDataContext();
        private void ReservationEmp_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.ColumnHeadersHeight = 40;
            guna2DataGridView2.ColumnHeadersHeight = 40;
            var req = from a in db.Users
                      where a.PremissionId == 1
                      select new {a.Id, a.FirstName, a.LastName, a.Email, a.Username };
            guna2DataGridView1.DataSource = req.ToArray();
            if (guna2DataGridView1.Columns.Count < 6)
            {
                DataGridViewButtonColumn dw = new DataGridViewButtonColumn();
                dw.HeaderText = "Select Client";
                dw.Text = "Select";
                dw.UseColumnTextForButtonValue = true;
                guna2DataGridView1.Columns.Add(dw);
            }
            guna2ComboBox2.DataSource = req.ToArray();
            guna2ComboBox2.DisplayMember = "Username";
            guna2ComboBox2.ValueMember = "Id";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            if (guna2DateTimePicker1.Value > guna2DateTimePicker2.Value)
            {
                MessageBox.Show("Date error");

            }
            else
            {
                SqlConnection cnn = new SqlConnection(@"Data Source =.; Initial Catalog = Hotelmanagment; Integrated Security = True; Pooling = False");
                SqlCommand cmd = new SqlCommand("freerooms", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@D1", guna2DateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@D2", guna2DateTimePicker2.Value);
                DataTable dt = new DataTable("FreeRooms");
                cnn.Open();
                dt.Load(cmd.ExecuteReader());
                cnn.Close();

                guna2DataGridView2.DataSource = dt;
                if (guna2DataGridView2.Columns.Count < 5)
                {
                    DataGridViewButtonColumn dw = new DataGridViewButtonColumn();
                    dw.HeaderText = "Select Room";
                    dw.Text = "Select";
                    dw.UseColumnTextForButtonValue = true;
                    guna2DataGridView2.Columns.Add(dw);
                }
                guna2ComboBox1.DataSource = dt;
                guna2ComboBox1.DisplayMember = "Id";
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (guna2DateTimePicker3.Value > guna2DateTimePicker4.Value)
            {
                MessageBox.Show("Date error");

            }else if (DialogResult.Yes == MessageBox.Show("Are you sure you want reservation form " + guna2DateTimePicker3.Value.ToShortDateString() + "to " + guna2DateTimePicker3.Value.ToShortDateString() + "in Room number" + guna2ComboBox1.Text, "Confirmation", MessageBoxButtons.YesNo))
            {
                SqlConnection cnn = new SqlConnection(@"Data Source =.; Initial Catalog = Hotelmanagment; Integrated Security = True; Pooling = False");
                SqlCommand cmd = new SqlCommand("Addreservation", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@D1", guna2DateTimePicker3.Value);
                cmd.Parameters.AddWithValue("@D2", guna2DateTimePicker4.Value);
                cmd.Parameters.AddWithValue("@userid", guna2ComboBox2.SelectedValue);
                cmd.Parameters.AddWithValue("@roomid", guna2ComboBox1.Text);

                cnn.Open();
                if (cmd.ExecuteNonQuery() > 0) MessageBox.Show("Reservation done");
                else MessageBox.Show("error");
                cnn.Close();
            }
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2ComboBox1.Text = guna2DataGridView2.CurrentRow.Cells["Id"].Value.ToString();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            guna2ComboBox2.SelectedValue = guna2DataGridView1.CurrentRow.Cells["Id"].Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { guna2TextBox1.Enabled = true; }
            else guna2TextBox1.Enabled = false;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) { guna2TextBox2.Enabled = true; }
            else guna2TextBox2.Enabled = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

            int id = 0;
                
            int.TryParse(guna2TextBox1.Text,out id);
            string username = guna2TextBox2.Text;
            var req = from a in db.Users
                      where a.PremissionId == 1 
                      select new { a.Id, a.FirstName, a.LastName, a.Email, a.Username };
            if (checkBox1.Checked && !checkBox2.Checked) {
                 req = from a in db.Users
                          where a.PremissionId == 1 && a.Id == id
                          select new { a.Id, a.FirstName, a.LastName, a.Email, a.Username };
                
            }
            else if (!checkBox1.Checked && checkBox2.Checked) {
                 req = from a in db.Users
                          where a.PremissionId == 1 && a.Username == username
                          select new { a.Id, a.FirstName, a.LastName, a.Email, a.Username };
            }
            else if (checkBox1.Checked && checkBox2.Checked) {
                 req = from a in db.Users
                          where a.PremissionId == 1 && a.Id == id && a.Username == username
                          select new { a.Id, a.FirstName, a.LastName, a.Email, a.Username };
            }

            guna2DataGridView1.DataSource = req.ToArray();
        }
    }
}
