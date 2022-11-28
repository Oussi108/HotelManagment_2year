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
    public partial class EmployeesSet : UserControl
    {
        public EmployeesSet()
        {
            InitializeComponent();
        }
        hotelDataContext db = new hotelDataContext();

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) { guna2TextBox2.Enabled = true; }
            else guna2TextBox2.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { guna2TextBox1.Enabled = true; }
            else guna2TextBox1.Enabled = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int id = 0;

            int.TryParse(guna2TextBox1.Text, out id);
            string username = guna2TextBox2.Text;
            var req = from a in db.Users
                      
                      select new { a.Id, a.FirstName, a.LastName, a.Email, a.Username , a.Premission.PremissionName };
            if (checkBox1.Checked && !checkBox2.Checked)
            {
                req = from a in db.Users
                      where  a.Id == id
                      select new { a.Id, a.FirstName, a.LastName, a.Email, a.Username, a.Premission.PremissionName };

            }
            else if (!checkBox1.Checked && checkBox2.Checked)
            {
                req = from a in db.Users
                      where  a.Username == username
                      select new { a.Id, a.FirstName, a.LastName, a.Email, a.Username, a.Premission.PremissionName };
            }
            else if (checkBox1.Checked && checkBox2.Checked)
            {
                req = from a in db.Users
                      where a.Id == id && a.Username == username
                      select new { a.Id, a.FirstName, a.LastName, a.Email, a.Username, a.Premission.PremissionName };
            }

            guna2DataGridView1.DataSource = req.ToArray();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int id;
            int.TryParse(guna2TextBox1.Text, out id);
            string username = guna2TextBox2.Text;
            var req = from a in db.Users
                     select new { a.Id, a.FirstName, a.LastName, a.Email, a.Username, a.Premission.PremissionName,a.PremissionId };
            guna2DataGridView1.DataSource = req.ToArray();
            guna2DataGridView1.Columns["PremissionId"].Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void EmployeesSet_Load(object sender, EventArgs e)
        {
            var req = from a in db.Users
                     select new { a.Id, a.Username,  };
            var req1 = from a in db.Premissions
                      select new { a.Id, a.PremissionName, };
            guna2ComboBox1.DataSource = req.ToArray();
            guna2ComboBox2.DataSource = req1.ToArray();
            guna2ComboBox1.DisplayMember = "Username";
            guna2ComboBox1.ValueMember = "Id";
            guna2ComboBox2.DisplayMember = "PremissionName";
            guna2ComboBox2.ValueMember = "Id";


        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2ComboBox1.SelectedValue = guna2DataGridView1.CurrentRow.Cells["Id"].Value;
            guna2ComboBox2.SelectedValue = guna2DataGridView1.CurrentRow.Cells["PremissionId"].Value;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {


            int i = (int)guna2ComboBox1.SelectedValue;
                var req = from a in db.Users
                          where a.Id == i
                          select a;
                foreach (var d in req)
                {
                d.PremissionId = (int)guna2ComboBox2.SelectedValue; 
                
                }
               
                db.SubmitChanges();
            MessageBox.Show("submit succesfuly","Validation");
            
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            SignUp s = new SignUp();
            s.ShowDialog();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure,You wanna delete userId  " + guna2ComboBox1.Text +" with all his credite cards","Delete user",MessageBoxButtons.YesNo) == DialogResult.Yes) { }
            { 
                        int g =(int) guna2ComboBox1.SelectedValue;
                    var req = from a in db.Users
                              where a.Id == g
                              select a;
                    var req1 =  from a in db.CreditCards
                               where a.UserId == g
                               select a;
                foreach (var item in req1)
                {
                    db.CreditCards.DeleteOnSubmit(item);
                }
                
                    db.Users.DeleteOnSubmit(req.FirstOrDefault());
                MessageBox.Show("Delete succesfuly", "Validation");
                db.SubmitChanges();
            }
        }
    }
}
