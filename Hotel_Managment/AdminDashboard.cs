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
    public partial class AdminDashboard : Form
    {
        public User use = LoginForm.Muser;
        public AdminDashboard()
        {
            
            InitializeComponent();
        }

        void hideeverything()
        {
            mangmentAdmin1.Hide();
            staticticAdmin1.Hide();
            settingUser1.Hide();
            employeesSet1.Hide();
            managmentRoom1.Hide();


        }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            hideeverything();
            employeesSet1.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            hideeverything();
            mangmentAdmin1.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            hideeverything();
            staticticAdmin1.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            hideeverything();
            settingUser1.Show();
        }

        private void staticticAdmin1_Load(object sender, EventArgs e)
        {
            hideeverything();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            hideeverything();
            managmentRoom1.Show();

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

        private void AdminDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
