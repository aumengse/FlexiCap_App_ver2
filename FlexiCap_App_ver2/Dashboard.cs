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
    public partial class Dashboard : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        public DateTime date_today { get; set; }
 

        public double scn_total_amt, scn_total_depo, scn_total_wdl;
        public double icbs_total_amt, icbs_total_depo, icbs_total_wdl;
        public double depo_total, wdl_total, total_amt;

        //public int icbs_total_ct { get; set; }

        public Dashboard()
        {
            InitializeComponent();
            this.CenterToScreen();
            
        }
        private void conString()
        {
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string xx_date = DateTime.Today.ToString("MM/dd/yyyy");
            date_today = DateTime.Parse(xx_date);
            //MessageBox.Show(date_today.ToString());

            count_items("scanned_trans");
            count_items("icbs_trans");

            count_breakdown_scan("scanned_trans","DEPO");
            count_breakdown_scan("scanned_trans", "WDL");
            count_breakdown_icbs("icbs_trans", "DEPO");
            count_breakdown_icbs("icbs_trans", "WDL");

            sum_breakdown_scan("scanned_trans", "DEPO");
            sum_breakdown_scan("scanned_trans", "WDL");
            sum_breakdown_icbs("icbs_trans", "DEPO");
            sum_breakdown_icbs("icbs_trans", "WDL");

            sum_amount("scanned_trans");
            sum_amount("icbs_trans");

            variance();
            

            listviews();
        }

        private void listviews()
        {
            ListViewItem lvs_d = new ListViewItem();
            lvs_d.SubItems.Add(String.Format("{0:n}", Double.Parse(scn_total_depo.ToString())));
            
            ListViewItem lvs_w = new ListViewItem();
            lvs_w.SubItems.Add(String.Format("{0:n}", Double.Parse(scn_total_wdl.ToString())));

            ListViewItem lvs_a = new ListViewItem();
            lvs_a.SubItems.Add(String.Format("{0:n}", Double.Parse(scn_total_amt.ToString())));

            lv_scanned.Items.Add("");
            lv_scanned.Items.Add("");
            lv_scanned.Items.Add(lvs_d);
            lv_scanned.Items.Add("");
            lv_scanned.Items.Add(lvs_w);
            lv_scanned.Items.Add("");
            lv_scanned.Items.Add("");
            lv_scanned.Items.Add("");
            lv_scanned.Items.Add("");
            lv_scanned.Items.Add("");
            lv_scanned.Items.Add("");
            lv_scanned.Items.Add("");
            lv_scanned.Items.Add(lvs_a);

            ListViewItem lvi_d = new ListViewItem();
            lvi_d.SubItems.Add(String.Format("{0:n}", Double.Parse(icbs_total_depo.ToString())));

            ListViewItem lvi_w = new ListViewItem();
            lvi_w.SubItems.Add(String.Format("{0:n}", Double.Parse(icbs_total_wdl.ToString())));

            ListViewItem lvi_a = new ListViewItem();
            lvi_a.SubItems.Add(String.Format("{0:n}", Double.Parse(icbs_total_amt.ToString())));

            lv_icbs.Items.Add("");
            lv_icbs.Items.Add("");
            lv_icbs.Items.Add(lvi_d);
            lv_icbs.Items.Add("");
            lv_icbs.Items.Add(lvi_w);
            lv_icbs.Items.Add("");
            lv_icbs.Items.Add("");
            lv_icbs.Items.Add("");
            lv_icbs.Items.Add("");
            lv_icbs.Items.Add("");
            lv_icbs.Items.Add("");
            lv_icbs.Items.Add("");
            lv_icbs.Items.Add(lvi_a);


            ListViewItem lvv_d = new ListViewItem();
            lvv_d.SubItems.Add(String.Format("{0:n}", Double.Parse(depo_total.ToString())));

            ListViewItem lvv_w = new ListViewItem();
            lvv_w.SubItems.Add(String.Format("{0:n}", Double.Parse(wdl_total.ToString())));

            ListViewItem lvv_a = new ListViewItem();
            lvv_a.SubItems.Add(String.Format("{0:n}", Double.Parse(total_amt.ToString())));

            lv_variance.Items.Add("");
            lv_variance.Items.Add("");
            lv_variance.Items.Add(lvv_d);
            lv_variance.Items.Add("");
            lv_variance.Items.Add(lvv_w);
            lv_variance.Items.Add("");
            lv_variance.Items.Add("");
            lv_variance.Items.Add("");
            lv_variance.Items.Add("");
            lv_variance.Items.Add("");
            lv_variance.Items.Add("");
            lv_variance.Items.Add("");
            lv_variance.Items.Add(lvv_a);
        }

        private void count_items(string table_name)
        {
            conString();
            try
            {
                con.Open();
                string cmd = "SELECT COUNT(id) from " + table_name + " where [trans_date]=#"+ date_today +"#";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    rdr.Read();
                    if (table_name == "scanned_trans")
                    {
                        lbl_scn_total_ct.Text = rdr.GetValue(0).ToString();                       
                    }

                    else
                    {
                        lbl_icbs_total_ct.Text = rdr.GetValue(0).ToString();
                    }
                }
                con.Close();

            }
            catch(Exception e)
            {
               MessageBox.Show(e.Message);
            }

        }

        private void count_breakdown_scan(string table_name, string trans_code)
        {
            try
            {
                con.Open();
                string scan_depo_count = "SELECT COUNT(id) from " + table_name + " where trans_code='" + trans_code + "' and trans_date=#"+ date_today +"#";
                {
                    OleDbCommand command = new OleDbCommand(scan_depo_count, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (table_name == "scanned_trans" & trans_code == "DEPO")
                        {
                            lbl_scn_depo_ct.Text = rdr.GetValue(0).ToString();
                        }
                        else
                        {
                            lbl_scn_wdl_ct.Text = rdr.GetValue(0).ToString();
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
                string scan_depo_count = "SELECT COUNT(id) from " + table_name + " where trans_code='" + trans_code + "' and trans_date=#" + date_today + "#";
                {
                    OleDbCommand command = new OleDbCommand(scan_depo_count, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {

                        if (table_name == "icbs_trans" & trans_code == "DEPO")
                        {
                            lbl_icbs_depo_ct.Text = rdr.GetValue(0).ToString();
                        }
                        else
                        {
                            lbl_icbs_wdl_ct.Text = rdr.GetValue(0).ToString();
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


        private void sum_amount(string table_name)
        {
            conString();

            try
            {
                con.Open();
                string cmd = "SELECT SUM(amount) from " + table_name + " where [trans_date]=#" + date_today + "#";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (table_name == "scanned_trans")
                        {
                            
                            scn_total_amt = Double.Parse(rdr.GetValue(0).ToString());
                        }
                        else
                        {                            
                            icbs_total_amt = Double.Parse(rdr.GetValue(0).ToString());
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

        private void sum_breakdown_scan(string table_name, string trans_code)
        {
            try
            {
                con.Open();
                string scan_depo = "SELECT SUM(amount) from " + table_name + " where trans_code='" + trans_code + "' and trans_date=#"+ date_today +"#";
                {
                    OleDbCommand command = new OleDbCommand(scan_depo, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (table_name == "scanned_trans" & trans_code == "DEPO")
                        {
                            scn_total_depo = Double.Parse(rdr.GetValue(0).ToString());
                        }
                        else
                        {
                            scn_total_wdl = Double.Parse(rdr.GetValue(0).ToString());
                        }
                    }

                }
                con.Close();
            }
            catch
            {

            }

        }
        private void sum_breakdown_icbs(string table_name, string trans_code)
        {
            try
            {
                con.Open();
                string scan_depo = "SELECT SUM(amount) from " + table_name + " where trans_code='" + trans_code + "' and trans_date=#" + date_today + "#";
                {
                    OleDbCommand command = new OleDbCommand(scan_depo, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {

                        if (table_name == "icbs_trans" & trans_code == "DEPO")
                        {
                            icbs_total_depo = Double.Parse(rdr.GetValue(0).ToString());
                        }
                        else
                        {
                            icbs_total_wdl = Double.Parse(rdr.GetValue(0).ToString());
                        }
                    }

                }
                con.Close();
            }
            catch
            {

            }

        }

        private void variance()
        {
            
            


            lbl_var_depo_ct.Text = Math.Abs(int.Parse(lbl_scn_depo_ct.Text) - int.Parse(lbl_icbs_depo_ct.Text)).ToString();
            lbl_var_wdl_ct.Text = Math.Abs(int.Parse(lbl_scn_wdl_ct.Text) - int.Parse(lbl_icbs_wdl_ct.Text)).ToString();
            lbl_var_total_ct.Text = Math.Abs(int.Parse(lbl_scn_total_ct.Text) - int.Parse(lbl_icbs_total_ct.Text)).ToString();

            depo_total = Math.Abs(scn_total_depo - icbs_total_depo);
            wdl_total = Math.Abs(scn_total_wdl - icbs_total_wdl);
            total_amt = Math.Abs(scn_total_amt - icbs_total_amt);

        }





    }
}
