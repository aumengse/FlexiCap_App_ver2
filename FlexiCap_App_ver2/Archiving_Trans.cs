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
    public partial class Archiving_Trans : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        public Archiving_Trans()
        {
            InitializeComponent();
        }
        private void conString()
        {
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
        }

        private void Archiving_Trans_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Show();            
            load_progressbar();
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
                    archiving_SCAN();
                    archiving_ICBS();
                    MessageBox.Show("Archiving Complete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

        }

        

        private void archiving_ICBS()
        {
            conString();
            try
            {
                con.Open();
                string nw_cmd = "INSERT INTO archive_trans(trans_id,trans_date,trans_code,acct_name,acct_num,amount,match_code,match_ref,remarks,trans_src)" +
                                "SELECT id,trans_date,trans_code,acct_name,acct_num,amount,match_code,match_ref,remarks,trans_src FROM icbs_trans where match_code='R' or match_code='F';";
                {
                    OleDbCommand nw_command = new OleDbCommand(nw_cmd, con);
                    nw_command.ExecuteNonQuery();
                    //MessageBox.Show("INSERTED ICBS");
                }
                con.Close();


                con.Open();
                string del_cmd = "DELETE FROM icbs_trans where match_code='R' or match_code='F'";
                {
                    OleDbCommand nw_command2 = new OleDbCommand(del_cmd, con);
                    nw_command2.ExecuteNonQuery();
                    //MessageBox.Show("DELETED ICBS");
                }
                con.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        private void archiving_SCAN()
        {
            conString();
            try
            {
                con.Open();
                string nw_cmd = "INSERT INTO archive_trans(trans_id,trans_date,trans_code,acct_name,acct_num,amount,match_code,match_ref,remarks,trans_src)" +
                                "SELECT id,trans_date,trans_code,acct_name,acct_num,amount,match_code,match_ref,remarks,trans_src FROM scanned_trans where match_code='R' or match_code='F';";
                {
                    OleDbCommand nw_command = new OleDbCommand(nw_cmd, con);
                    nw_command.ExecuteNonQuery();
                    //MessageBox.Show("INSERTED SCAN");
                }
                con.Close();




                con.Open();
                string del_cmd = "DELETE FROM scanned_trans where match_code='R' or match_code='F'";
                {
                    OleDbCommand nw_command2 = new OleDbCommand(del_cmd, con);
                    nw_command2.ExecuteNonQuery();
                    //MessageBox.Show("DELETED SCAN");
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
