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
    public partial class fooditem : UserControl
    {
        public Food F;
        public fooditem(Food F)
        {
          
            this.F = F;
            InitializeComponent();
        }

        private void fooditem_Load(object sender, EventArgs e)
        {
            Bitmap abc = (Bitmap)System.Drawing.Bitmap.FromFile(F.Img);
            guna2PictureBox1.Image = abc;
            label2.Text = F.Price.ToString() ;
            label4.Text = F.FoodName;          
        }
        
        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
            FoodClient.fd = this.F;
        }

        private void fooditem_Click(object sender, EventArgs e)
        {

        }
    }
}
