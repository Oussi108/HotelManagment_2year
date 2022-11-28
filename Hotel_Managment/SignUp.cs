using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Managment
{
    public partial class SignUp : Form
    {
        hotelDataContext db = new hotelDataContext();
        
        public SignUp()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;

            if (errorshow()) { 
            User u = new User();
            u.FirstName = gunaLineTextBox1.Text;
            u.LastName = gunaLineTextBox2.Text;
            u.Username = gunaLineTextBox3.Text;
            if(gunaLineTextBox6.Text != gunaLineTextBox7.Text)
            {
                MessageBox.Show("password not matched");
                return;
            }
            u.Password = gunaLineTextBox6.Text;
            u.Email = gunaLineTextBox4.Text;
            u.Phone = gunaLineTextBox5.Text;
            u.PremissionId = 1;

            db.Users.InsertOnSubmit(u);
            db.SubmitChanges();
                MessageBox.Show("submit succesfuly", "Validation");
            }
        }

        
        
            bool errorshow()
            {
                if (string.IsNullOrEmpty(gunaLineTextBox1.Text) ||
                    string.IsNullOrEmpty(gunaLineTextBox2.Text))
                {
                    label1.Visible = true;
                    label1.Text = "The first name or last name are empty ,please enter valid name";
                    return false;
                }

            if (string.IsNullOrEmpty(gunaLineTextBox3.Text) && gunaLineTextBox3.Text.Length < 3)
            {
                label2.Visible = true;
                label2.Text = "the Username Should be atleast 3 charcters";
                return false;
            }
                if (string.IsNullOrEmpty(gunaLineTextBox6.Text) && gunaLineTextBox6.Text.Length < 3)
                {
                    label3.Visible = true;
                    label3.Text = "the Password Should be atleast 3 charcters";
                    return false;
                }
                if (db.Users.Where(x => x.Username == gunaLineTextBox3.Text).Count() > 0)
                {
                    label2.Visible = true;
                    label2.Text = "the Username already exist please try another username";
                    return false;
                
                }
               
                return true;
            }

        

        private bool mouseDown;
        private Point lastLocation;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void panel11_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
        }
    }
}
