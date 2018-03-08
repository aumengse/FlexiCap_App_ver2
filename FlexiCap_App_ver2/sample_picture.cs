using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
namespace FlexiCap_App_ver2
{
    public partial class sample_picture : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        private void conString()
        {
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
        }
        public sample_picture()
        {
            InitializeComponent();
        }
        public static Bitmap ByteToImage(byte[] blob)
        {
            
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }
        private void sample_picture_Load(object sender, EventArgs e)
        {
            try
            {
                conString();
                string cmd = "SELECT * FROM depo_slip where amount=47003";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    con.Open();
                    OleDbDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        Byte[] bytes = (Byte[])rdr.GetValue(0);
                        pictureBox1.Image = ByteToImage(bytes);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
