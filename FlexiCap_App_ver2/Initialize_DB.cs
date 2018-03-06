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
using System.Configuration;
using System.Threading;

namespace FlexiCap_App_ver2
{
    public partial class Initialize_DB : Form
    {

        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        public Initialize_DB()
        {
            InitializeComponent();
        }
        private void conString()
        {
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
        }

        private void Initialize_DB_Load(object sender, EventArgs e)
        {
            this.Show();
            this.CenterToScreen();
            load_progressbar();
            this.Close();
        }

        private void load_progressbar()
        {
            progress_Bar.Minimum = 0;
            progress_Bar.Maximum = 101;
            for (int i = 0; i <= 100; i++)
            {
                progress_Bar.Value = i;
                Thread.Sleep(20);

                if (i == 100)
                {
                    initialize_db("scanned_trans");
                    initialize_db("icbs_trans");
                    initialize_db("archive_trans");
                    MessageBox.Show("Database Initialized");
                }
            }

        }


        private void initialize_db(string table_name)
        {
            conString();
            try
            {
                con.Open();
                string del_scan = "DELETE * FROM " + table_name + "";
                {
                    OleDbCommand nw_command = new OleDbCommand(del_scan, con);
                    nw_command.ExecuteNonQuery();
                    //MessageBox.Show("DELETED ALL");
                }
                con.Close();

                con.Open();
                string alter_to_int = "ALTER table " + table_name + " ALTER column ID INT ";
                {
                    OleDbCommand nw_command = new OleDbCommand(alter_to_int, con);
                    nw_command.ExecuteNonQuery();
                    //MessageBox.Show("ALTER TO INT");
                }
                con.Close();

                con.Open();
                string alter_to_autoint = "ALTER table " + table_name + " ALTER column ID AUTOINCREMENT ";
                {
                    OleDbCommand nw_command = new OleDbCommand(alter_to_autoint, con);
                    nw_command.ExecuteNonQuery();
                    //MessageBox.Show("ALTER TO AUTOINT");
                }
                con.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
