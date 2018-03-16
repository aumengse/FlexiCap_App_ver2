using System;
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
        private bool _doCheck = true;
        private bool _doSelect = true;
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
        
        private static string get_trans_code(string table_name, string acct_name, string acct_num, string amount)
        {
            string trans_code = "";
            try
            {
                OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
                Conf.conf dbcon;
                con = new OleDbConnection();
                dbcon = new Conf.conf();
                con.ConnectionString = dbcon.getConnectionString();
                con.Open();
                string cmd = "SELECT trans_code FROM " + table_name + " where acct_name='"+ acct_name +"' and acct_num='"+acct_num+"' and amount="+amount.Replace(",", "")+"";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            trans_code = rdr.GetValue(0).ToString();
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return trans_code;
        }
        private void unmatched_view(string table_name, string var_match_code, string op, string match_code)
        {
            try
            {
                conString();
                con.Open();
                string cmd = "SELECT * FROM " + table_name + " where " + var_match_code + " " + op + " '" + match_code + "' and match_code = 'U' order by trans_code,trans_date,acct_name";
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
            cmb_icbs_trans.SelectedIndex = 0;
            cmb_scan_trans.SelectedIndex = 0;
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
            string icbs_trans_code = Unmatched_Icbs_Records.CheckedItems[0].SubItems[1].Text;
            string scan_trans_code = Unmatched_Scanned_Records.CheckedItems[0].SubItems[1].Text;
            try
            {
                if( icbs_trans_code == scan_trans_code)
                {
                    try
                    {
                        Verify_Data vd = new Verify_Data();
                        string icbs_acct_name = vd.txt_icbs_acct_name.Text = Unmatched_Icbs_Records.CheckedItems[0].SubItems[3].Text;
                        string icbs_acct_num = vd.txt_icbs_acct_num.Text = Unmatched_Icbs_Records.CheckedItems[0].SubItems[4].Text;
                        vd.txt_icbs_date.Text = Unmatched_Icbs_Records.CheckedItems[0].SubItems[2].Text;
                        string icbs_amount = vd.txt_icbs_amount.Text = Unmatched_Icbs_Records.CheckedItems[0].SubItems[5].Text;

                        string acct_name = Unmatched_Scanned_Records.CheckedItems[0].SubItems[3].Text;
                        string acct_num = Unmatched_Scanned_Records.CheckedItems[0].SubItems[4].Text;
                        string scan_date = Unmatched_Scanned_Records.CheckedItems[0].SubItems[2].Text;
                        string amount = Unmatched_Scanned_Records.CheckedItems[0].SubItems[5].Text;
                        string image_name = get_image_name(acct_name, acct_num, amount);
                        string image_path = get_image_path();
                        string loc_image = image_path + image_name;

                        vd.pct_scanned_data.Image = Image.FromFile(@"" + loc_image + "");
                        vd.acct_name = acct_name;
                        vd.acct_num = acct_num;
                        vd.amount = amount;
                        vd.scan_date = scan_date;
                        vd.scan_trans_code = get_trans_code("scanned_trans", acct_name, acct_num, amount);
                        vd.scan_date = scan_date;
                        vd.icbs_trans_code = get_trans_code("icbs_trans", icbs_acct_name, icbs_acct_num, icbs_amount);
                        vd.Show();
                        this.Close();

                    }
                    catch
                    {
                        MessageBox.Show("Please Select Record To Verify", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Select Data with the same trasaction code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Please choose transactions to verify", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void check_one_item(string table_name, ItemCheckedEventArgs e)
        {
            if (!_doCheck)
            {
                return;
            }

            _doCheck = false;

            _doSelect = false;
            if (table_name == "scan")
            {
                foreach (ListViewItem lvi in Unmatched_Scanned_Records.Items)
                { // clear all checked items except the one we are working with
                    if (lvi != e.Item)
                        lvi.Checked = false;
                }
            }
            else
            {
                foreach (ListViewItem lvi in Unmatched_Icbs_Records.Items)
                { // clear all checked items except the one we are working with
                    if (lvi != e.Item)
                    {
                        lvi.Checked = false;
                    }
                }
            }

            _doCheck = true;
            if (table_name == "scan")
            {
                Unmatched_Scanned_Records.SelectedItems.Clear();
            }
            else
            {
                Unmatched_Icbs_Records.SelectedItems.Clear();
            }
            e.Item.Selected = e.Item.Checked;
            e.Item.Selected = false;

            _doSelect = true;
        }

        private void select_one_item(string listview_name)
        {
            if (!_doSelect)
            {
                return;
            }

            //suppress the ItemChecked Event

            _doCheck = false;
            if (listview_name == "icbs")
            {
                foreach (ListViewItem lvi in Unmatched_Icbs_Records.Items)
                {
                    lvi.Checked = false;
                }
            }
            else
            {
                foreach (ListViewItem lvi in Unmatched_Scanned_Records.Items)
                {
                    lvi.Checked = false;
                }
            }

            if (listview_name == "icbs")
            {
                if (Unmatched_Icbs_Records.SelectedItems.Count > 0)
                {
                    string listItem = Unmatched_Icbs_Records.SelectedItems[0].Text;
                    Unmatched_Icbs_Records.SelectedItems[0].Checked = true;
                }
            }
            else
            {
                if (Unmatched_Scanned_Records.SelectedItems.Count > 0)
                {
                    string listItem = Unmatched_Scanned_Records.SelectedItems[0].Text;
                    Unmatched_Scanned_Records.SelectedItems[0].Checked = true;
                }
            }
            _doCheck = true;
        }

        private void Unmatched_Icbs_Records_SelectedIndexChanged(object sender, EventArgs e)
        {
            select_one_item("icbs");
        }

        private void Unmatched_Icbs_Records_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            check_one_item("icbs", e);
            foreach (ListViewItem lv in Unmatched_Icbs_Records.Items)
            {
                if (lv.Checked)
                {
                    lv.BackColor = Color.LightBlue;
                }
                else
                {
                    lv.BackColor = Color.White;
                }
            }
        }

        private void Unmatched_Scanned_Records_SelectedIndexChanged(object sender, EventArgs e)
        {
            select_one_item("scan");
        }

        private void Unmatched_Scanned_Records_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            check_one_item("scan", e);
            foreach (ListViewItem lv in Unmatched_Scanned_Records.Items)
            {
                if (lv.Checked)
                {
                    lv.BackColor = Color.LightBlue;
                }
                else
                {
                    lv.BackColor = Color.White;
                }
            }
        }

        private void cmb_icbs_trans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_icbs_trans.Text == "Deposits")
            {
                unmatched_view("icbs_trans", "trans_code", "=", "DEPO");
            }
            else if (cmb_icbs_trans.Text == "Withdrawals")
            {
                unmatched_view("icbs_trans", "trans_code", "=", "WDL");
            }
            else
            {
                unmatched_view("icbs_trans", "match_code", "=", "U");
            }
        }

        private void cmb_scan_trans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_scan_trans.Text == "Deposits")
            {
                unmatched_view("scanned_trans", "trans_code", "=", "DEPO");
            }
            else if (cmb_scan_trans.Text == "Withdrawals")
            {
                unmatched_view("scanned_trans", "trans_code", "=", "WDL");
            }
            else
            {
                unmatched_view("scanned_trans", "match_code", "=", "U");
            }
        }
    }
}
