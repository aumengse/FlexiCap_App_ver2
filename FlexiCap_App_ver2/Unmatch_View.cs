﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace FlexiCap_App_ver2
{
    public partial class Unmatch_View : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        private void conString()
        {
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
        }
        public Unmatch_View()
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
        private void unmatched_view(string table_name, string var_match_code, string op, string match_code)
        {
            try
            {
                conString();
                con.Open();
                string cmd = "SELECT * FROM " + table_name + " where " + var_match_code + " " + op + " '" + match_code + "' order by trans_code,trans_date,acct_name";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    if (table_name == "icbs_trans")
                    {
                        Unmatched_Icbs_Records.Items.Clear();
                    }
                    else
                    {
                        Unmatched_Scanned_Records.Items.Clear();
                    }
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ListViewItem aa = new ListViewItem();
                            aa.SubItems.Add(rdr.GetValue(5).ToString());
                            aa.SubItems.Add(DateTime.Parse(rdr.GetValue(1).ToString()).ToString("MM/dd/yyyy"));
                            aa.SubItems.Add(rdr.GetValue(2).ToString());
                            aa.SubItems.Add(rdr.GetValue(3).ToString());
                            aa.SubItems.Add(String.Format("{0:n}", Double.Parse(rdr.GetValue(4).ToString())));
                            if (table_name == "icbs_trans")
                            {
                                Unmatched_Icbs_Records.Items.Add(aa);
                            }
                            else
                            {
                                Unmatched_Scanned_Records.Items.Add(aa);
                            }
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static string amount_sum(string table_name)
        {
            string amount = "";
            OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
            Conf.conf dbcon;
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();

            try
            {
                //OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\PC-23\Desktop\TVVS.accdb; Persist Security Info=False;");
                //conString();
                con.Open();
                string cmd = "SELECT sum(amount) FROM " + table_name + " where match_code = 'U'";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    rdr.Read();
                    {
                        amount = String.Format("{0:n}", Double.Parse(rdr.GetValue(0).ToString()));
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return amount;
        }
        private static string items_counter(string table_name)
        {
            string items = "";
            OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
            Conf.conf dbcon;
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
            try
            {
                //OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\PC-23\Desktop\TVVS.accdb; Persist Security Info=False;");
                //conString();
                con.Open();
                string cmd = "SELECT COUNT(acct_name) FROM " + table_name + " where match_code = 'U'";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    rdr.Read();
                    items = rdr.GetValue(0).ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return items;
        }

        private void Unmatch_View_Load(object sender, EventArgs e)
        {
            unmatched_view("icbs_trans", "match_code", "=", "U");
            unmatched_view("scanned_trans", "match_code", "=", "U");
            if (Unmatched_Icbs_Records.Items.Count > 0)
            {
                string icbs_items = items_counter("icbs_trans");
                string icbs_sum = amount_sum("icbs_trans");
                lbl_icbs_value_items.Text = icbs_items;
                lbl_icbs_total_amount.Text = icbs_sum;
            }

            if (Unmatched_Scanned_Records.Items.Count > 0)
            {
                string scan_items = items_counter("scanned_trans");
                string scan_sum = amount_sum("scanned_trans");
                lbl_scan_value_items.Text = scan_items;
                lbl_scan_total_amount.Text = scan_sum;
            }
        }
        private static string get_image_name(string acct_name, string acct_num, string amount)
        {
            OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
            Conf.conf dbcon;
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
            string image_name = "";
            try
            {
                con.Open();
                string cmd = "SELECT image_path FROM scanned_trans where acct_name = '"+acct_name+"' and acct_num='"+acct_num+"'";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    rdr.Read();
                    image_name = rdr.GetValue(0).ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return image_name;
        }
        private static string get_image_path()
        {
            OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
            Conf.conf dbcon;
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
            string img_path = "";
            try
            {
                con.Open();
                string cmd = "SELECT image_path FROM settings";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    rdr.Read();
                    img_path = rdr.GetValue(0).ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return img_path;
        }
        private void btn_verify_Click(object sender, EventArgs e)
        {
            Verify_Data vd = new Verify_Data();
            vd.txt_icbs_acct_name.Text = Unmatched_Icbs_Records.CheckedItems[0].SubItems[3].Text;
            vd.txt_icbs_acct_num.Text = Unmatched_Icbs_Records.CheckedItems[0].SubItems[4].Text;
            vd.txt_icbs_date.Text = Unmatched_Icbs_Records.CheckedItems[0].SubItems[2].Text;
            vd.txt_icbs_amount.Text = Unmatched_Icbs_Records.CheckedItems[0].SubItems[5].Text;

            string acct_name = Unmatched_Scanned_Records.CheckedItems[0].SubItems[3].Text;
            string acct_num = Unmatched_Scanned_Records.CheckedItems[0].SubItems[4].Text;
            string date = Unmatched_Scanned_Records.CheckedItems[0].SubItems[2].Text;
            string amount = Unmatched_Scanned_Records.CheckedItems[0].SubItems[5].Text;
            string image_name = get_image_name(acct_name, acct_num, amount);
            string image_path = get_image_path();
            string loc_image = image_path + image_name;
            vd.pct_scanned_data.Image = Image.FromFile(@""+loc_image+"");
            vd.ShowDialog();
        }
    }
}
