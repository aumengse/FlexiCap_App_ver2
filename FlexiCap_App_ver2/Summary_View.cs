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
    public partial class Summary_View : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        public Summary_View()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.Show();
        }

        private void conString()
        {
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
        }

        private void Summary_View_Load(object sender, EventArgs e)
        {
            count_items("scanned_trans");
            count_items("icbs_trans");

            count_breakdown_scan("scanned_trans", "DEPO");
            count_breakdown_scan("scanned_trans", "WDL");
            count_breakdown_icbs("icbs_trans", "DEPO");
            count_breakdown_icbs("icbs_trans", "WDL");

            sum_amount("scanned_trans");
            sum_amount("icbs_trans");

            sum_breakdown("scanned_trans", "DEPO");
            sum_breakdown("scanned_trans", "WDL");
            sum_breakdown("icbs_trans", "DEPO");
            sum_breakdown("icbs_trans", "WDL");
        }


        private void count_items(string table_name)
        {
            conString();
            try
            {
                con.Open();
                string cmd = "SELECT COUNT(id) from " + table_name + " where match_code is Null";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    rdr.Read();
                    if (table_name == "scanned_trans")
                    {

                        scn_total_count.Text = rdr.GetValue(0).ToString();
                    }

                    else
                    {
                        icbs_total_count.Text = rdr.GetValue(0).ToString();
                    }
                }
                con.Close();

            }
            catch
            {
                //MessageBox.Show("No Results Found");
            }

        }

        private void count_breakdown_scan(string table_name, string trans_code)
        {
            try
            {
                con.Open();
                string scan_depo_count = "SELECT COUNT(id) from " + table_name + " where trans_code='" + trans_code + "' and match_code is Null";
                {
                    OleDbCommand command = new OleDbCommand(scan_depo_count, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (table_name == "scanned_trans" & trans_code == "DEPO")
                        {
                            scn_depo_total_count.Text = rdr.GetValue(0).ToString();
                        }
                        else
                        {
                            scn_wdl_total_count.Text = rdr.GetValue(0).ToString();
                        }
                    }
                }
                con.Close();

            }
            catch 
            {
                //MessageBox.Show(e.Message);
            }
        }

        private void count_breakdown_icbs(string table_name, string trans_code)
        {
            try
            {
                con.Open();
                string scan_depo_count = "SELECT COUNT(id) from " + table_name + " where trans_code='" + trans_code + "' and match_code is Null";
                {
                    OleDbCommand command = new OleDbCommand(scan_depo_count, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (table_name == "icbs_trans" & trans_code == "WDL")
                        {
                            icbs_depo_total_count.Text = rdr.GetValue(0).ToString();
                        }
                        else
                        {
                            icbs_wdl_total_count.Text = rdr.GetValue(0).ToString();
                        }
                    }
                }
                con.Close();

            }
            catch
            {

            }
        }


        private void sum_amount(string table_name)
        {
            conString();

            try
            {
                con.Open();
                string cmd = "SELECT SUM(amount) from "+ table_name + " where match_code is Null";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    
                    while (rdr.Read())
                    {
                        if (table_name == "scanned_trans")
                        {
                            scn_total_amt.Text = String.Format("{0:n}", Double.Parse(rdr.GetValue(0).ToString()));
                        }
                        else
                        {
                            icbs_total_amt.Text = String.Format("{0:n}", Double.Parse(rdr.GetValue(0).ToString()));
                        }
                    }
                }
                con.Close();


            }
            catch
            {
                //MessageBox.Show("No Result found.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                //MessageBox.Show(e.Message);
            }
        }
        private void sum_breakdown(string table_name, string trans_code)
        {
            try
            {
                con.Open();
                string scan_depo = "SELECT SUM(amount) from "+ table_name +" where trans_code='"+ trans_code +"' and match_code is Null";
                {
                    OleDbCommand command = new OleDbCommand(scan_depo, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (table_name == "scanned_trans" & trans_code =="DEPO")
                        {
                            scn_depo_total_amt.Text = String.Format("{0:n}", Double.Parse(rdr.GetValue(0).ToString()));
                        }
                        else if (table_name == "scanned_trans" & trans_code == "WDL")
                        {
                            scn_wdl_total_amt.Text = String.Format("{0:n}", Double.Parse(rdr.GetValue(0).ToString()));
                        }
                        else if (table_name == "icbs_trans" & trans_code == "DEPO")
                        {
                            icbs_depo_total_amt.Text = String.Format("{0:n}", Double.Parse(rdr.GetValue(0).ToString()));
                        }
                        else
                        {
                            icbs_wdl_total_amt.Text = String.Format("{0:n}", Double.Parse(rdr.GetValue(0).ToString()));
                        }
                    }

                }
                con.Close();

                

            }
            catch
            {

            }

        }


        


    }
}
