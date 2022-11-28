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
    public partial class SettingUser : UserControl
    {
        public SettingUser()
        {
            InitializeComponent();
        }
        HotelmanagmentEntities2 db = new HotelmanagmentEntities2();


        private void SettingUser_Load(object sender, EventArgs e)
        {
           

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Userinfo.Text = LoginForm.Muser.FirstName + " " + LoginForm.Muser.LastName;
            guna2TextBox1.Text = LoginForm.Muser.FirstName;
            guna2TextBox2.Text = LoginForm.Muser.LastName;
            guna2TextBox3.Text = LoginForm.Muser.Email;
            guna2TextBox4.Text = LoginForm.Muser.Phone;
        }

        private void gunaGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            var req = from a in db.Users
                      where a.Id == LoginForm.Muser.Id
                      select a;

            foreach (var item in req)
            {

                
                if(guna2TextBox5.Text == guna2TextBox6.Text)
                {
                    item.FirstName = guna2TextBox1.Text;
                    item.LastName = guna2TextBox2.Text;
                    item.Email = guna2TextBox3.Text;
                    item.Phone = guna2TextBox4.Text;
                    item.Password = guna2TextBox4.Text;
                    
                    break;
                }
            }
            db.SaveChanges();

        }
    }
}
