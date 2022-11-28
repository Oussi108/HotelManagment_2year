using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Hotel_Managment.Properties;

namespace Hotel_Managment
{
    public partial class LoginForm : Form
    {
        public static User Muser;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            SignUp s = new SignUp();
            s.Show();
        }
        hotelDataContext db = new hotelDataContext();
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (gunaWinSwitch1.Checked) { 
              Settings.Default["checksaveaccount"] = true; 
           
            Settings.Default["Username"] = guna2TextBox1.Text;
            Settings.Default["Pass"] = guna2TextBox2.Text;
                Settings.Default.Save();
            }
             else { Settings.Default["checksaveaccount"] = false;
                Settings.Default.Reset();
            }
           
            string user = guna2TextBox1.Text;
            string pass = guna2TextBox2.Text;

            var req = from a in db.Users
                      where a.Username == user && a.Password == pass
                      select a;
            Muser = req.FirstOrDefault();
            if (req.Count() == 0) {
                MessageBox.Show("username or password is wrong try again"); 
                return; }
            if (gunaWinSwitch1.Checked)
            {
                
            }
           switch (req.FirstOrDefault().PremissionId) {
                        case 1:
                                Dashboard d = new Dashboard();
                                d.Show();
                            break;
                        case 2:
                                EmpDashboard emp = new EmpDashboard();
                                emp.Show();
                                break;
                             case 3:
                                AdminDashboard a = new AdminDashboard();
                                a.Show();
                                break;
                        default:
                            MessageBox.Show("username or password is wrong try again");
                break;
                }
           



            }

        private void gunaWinSwitch1_CheckedChanged(object sender, EventArgs e)
        {
           
            
            
        }
        private bool mouseDown;
        private Point lastLocation;
        private void panel11_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
           mouseDown = false;
        }

        private void gunaLabel2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("contact the hotel number for more information");
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            gunaWinSwitch1.Checked = (bool)Settings.Default["checksaveaccount"];
            
            if (gunaWinSwitch1.Checked) {
               guna2TextBox1.Text = Settings.Default["Username"].ToString();
                guna2TextBox2.Text = Settings.Default["Pass"].ToString();
            }
        }
    }
}
