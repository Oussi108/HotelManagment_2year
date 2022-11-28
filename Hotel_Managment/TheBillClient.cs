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
    public partial class TheBillClient : Form
    {
        int ClientId;
        public TheBillClient(int a)
        {
            ClientId = a;
            InitializeComponent();
        }

        private void TheBillClient_Load(object sender, EventArgs e)
        {
            

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Thbill cr = new Thbill();
            cr.SetParameterValue("IdClient", ClientId);
            cr.SetParameterValue("IdClientS", ClientId);
            cr.SetParameterValue("IdClient2", ClientId);
            crystalReportViewer1.ReportSource = cr;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            TheBillFood cr = new TheBillFood();
            cr.SetParameterValue("Clientid", ClientId);
           
            crystalReportViewer1.ReportSource = cr;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            TheBillRoom cr = new TheBillRoom();
            cr.SetParameterValue("clientid", ClientId);

            crystalReportViewer1.ReportSource = cr;
        }
    }
}
