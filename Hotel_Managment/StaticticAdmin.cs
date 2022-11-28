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
    public partial class StaticticAdmin : UserControl
    {
        public StaticticAdmin()
        {
            InitializeComponent();
        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        DataSet ds = new DataSet();
        private void StaticticAdmin_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (ds.Tables.Count>0)
            {
                ds.Tables.RemoveAt(0);
                ds.Tables.RemoveAt(0);
                ds.Tables.RemoveAt(0);
            }
                SqlConnection cnn = new SqlConnection(@"Data Source=.;Initial Catalog=Hotelmanagment;Integrated Security=True");
                SqlDataAdapter da = new SqlDataAdapter(
                    "showstateroombymonth"
                    , cnn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@d1", guna2DateTimePicker1.Value);
                da.SelectCommand.Parameters.AddWithValue("@d2", guna2DateTimePicker2.Value);
                da.Fill(ds, "ShowRoomIcome");

                SqlDataAdapter da1 = new SqlDataAdapter(
                   "ShowFoodStates"
                   , cnn);


                da1.SelectCommand.CommandType = CommandType.StoredProcedure;
                da1.SelectCommand.Parameters.AddWithValue("@d1", guna2DateTimePicker1.Value);
                da1.SelectCommand.Parameters.AddWithValue("@d2", guna2DateTimePicker2.Value);
                da1.Fill(ds, "ShowFoodincome");


                SqlDataAdapter da2 = new SqlDataAdapter(
               "ShowServiceStates"
               , cnn);

                da2.SelectCommand.CommandType = CommandType.StoredProcedure;
                da2.SelectCommand.Parameters.AddWithValue("@d1", guna2DateTimePicker1.Value);
                da2.SelectCommand.Parameters.AddWithValue("@d2", guna2DateTimePicker2.Value);
                da2.Fill(ds, "ShowServiceIcome");
            
            

            guna2DataGridView1.DataSource = ds.Tables["ShowRoomIcome"];
            guna2DataGridView2.DataSource = ds.Tables["ShowFoodincome"];
            guna2DataGridView3.DataSource = ds.Tables["ShowServiceIcome"];
            
            
            int totR = 0;
            for (int i = 0; i < ds.Tables["ShowRoomIcome"].Rows.Count; i++)
            {

            
                totR += (int)ds.Tables["ShowRoomIcome"].Rows[i][4];

            }
            label13.Text = totR.ToString();
            label14.Text = ds.Tables["ShowRoomIcome"].Rows.Count.ToString();
            if(ds.Tables["ShowRoomIcome"].AsEnumerable().Where(x => x.Field<int>(4) != 0).ToArray().Count() != 0)
            {
                label15.Text =(totR/ ds.Tables["ShowRoomIcome"].AsEnumerable().Where(x => x.Field<int>(4) != 0).ToArray().Count()).ToString();
            }

            int totF = 0;
            for (int i = 0; i < ds.Tables["ShowFoodincome"].Rows.Count; i++)
            {


                totF += (int)ds.Tables["ShowFoodincome"].Rows[i][4];

            }
            label16.Text = totF.ToString();
            label17.Text = ds.Tables["ShowFoodincome"].Rows.Count.ToString();
            if (ds.Tables["ShowFoodincome"].AsEnumerable().Where(x => x.Field<int>(4) != 0).ToArray().Count() != 0)
            {
                label18.Text = (totF / ds.Tables["ShowFoodincome"].AsEnumerable().Where(x => x.Field<int>(4) != 0).ToArray().Count()).ToString();
            }



            int totS = 0;
            for (int i = 0; i < ds.Tables["ShowServiceIcome"].Rows.Count; i++)
            {


                totS += (int)ds.Tables["ShowServiceIcome"].Rows[i][3];

            }
            label19.Text = totS.ToString();
            label20.Text = ds.Tables["ShowServiceIcome"].Rows.Count.ToString();
            if (ds.Tables["ShowServiceIcome"].AsEnumerable().Where(x => x.Field<int>(3) != 0).ToArray().Count() != 0)
            {
                label21.Text = (totS / ds.Tables["ShowServiceIcome"].AsEnumerable().Where(x => x.Field<int>(3) != 0).ToArray().Count()).ToString();
            }




        }
    }
}
