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
    public partial class EmpDashboard : Form
    {
        public User use = LoginForm.Muser;
        public EmpDashboard()
        {
            
            InitializeComponent();
        }
        void hideeverything()
        {
            managerClient1.Hide();
            reservationEmp1.Hide();
            
            settingUser1.Hide();
            paimentEmp1.Hide();


        }
        void colorbtn(Guna.UI2.WinForms.Guna2Button a)
        {
            
            guna2Button1.FillColor = Color.FromArgb(63, 121, 177);
            guna2Button2.FillColor = Color.FromArgb(63, 121, 177);
            guna2Button3.FillColor = Color.FromArgb(63, 121, 177);
            guna2Button4.FillColor = Color.FromArgb(63, 121, 177);
            

            
            a.FillColor = Color.MediumAquamarine;

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void managmentEmp1_Load(object sender, EventArgs e)
        {
            
          
           
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            colorbtn((Guna.UI2.WinForms.Guna2Button)sender);
            hideeverything();
            reservationEmp1.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            colorbtn((Guna.UI2.WinForms.Guna2Button)sender);
            hideeverything();
            managerClient1.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            colorbtn((Guna.UI2.WinForms.Guna2Button)sender);
            hideeverything();
            settingUser1.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            colorbtn((Guna.UI2.WinForms.Guna2Button)sender);
            hideeverything();
            paimentEmp1.Show();
        }

        private void guna2PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
           
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

        private void EmpDashboard_Load(object sender, EventArgs e)
        {
            reservationEmp1.Show();
            hideeverything();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}
