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
    public partial class Dashboard : Form
    {
        public  User use  = LoginForm.Muser;
        public Dashboard()
        {
            
            
            InitializeComponent();
        }
        void colorbtn(Guna.UI2.WinForms.Guna2Button a)
        {
            Reservationbtn.FillColor = Color.Transparent;
            guna2Button1.FillColor = Color.Transparent;
            guna2Button2.FillColor = Color.Transparent;
            guna2Button3.FillColor = Color.Transparent;
            guna2Button4.FillColor = Color.Transparent;
            guna2Button5.FillColor = Color.Transparent;
            
            a.PressedColor = Color.BlueViolet;
            a.FillColor = Color.BlueViolet;

        }
        void hideeverything()
        {
            
            serviceClient1.Hide();
            settingUser1.Hide();
            reservationClient1.Hide();
            foodClient1.Hide();
            paiment1.Hide();
        }
        private void Reservationbtn_Click(object sender, EventArgs e)
        {
            colorbtn((Guna.UI2.WinForms.Guna2Button)sender);
            hideeverything();
            reservationClient1.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            colorbtn((Guna.UI2.WinForms.Guna2Button)sender);
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            colorbtn((Guna.UI2.WinForms.Guna2Button)sender);
            hideeverything();
            serviceClient1.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            colorbtn((Guna.UI2.WinForms.Guna2Button)sender);
            hideeverything();
            foodClient1.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            colorbtn((Guna.UI2.WinForms.Guna2Button)sender);
            hideeverything();
            paiment1.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            colorbtn((Guna.UI2.WinForms.Guna2Button)sender);
            hideeverything();
            settingUser1.Show();
        }

        private void serviceClient1_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            hideeverything();
            Reservationbtn.FillColor = Color.Transparent;
            guna2Button1.FillColor = Color.Transparent;
            guna2Button2.FillColor = Color.Transparent;
            guna2Button3.FillColor = Color.Transparent;
            guna2Button4.FillColor = Color.Transparent;
            guna2Button5.FillColor = Color.Transparent;
            gunaLabel1.Text = LoginForm.Muser.Username;
        }

        private void reservationClient1_Load(object sender, EventArgs e)
        {

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
    }
}
