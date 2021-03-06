﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace FlexiCap_App_ver2
{
    public partial class Match_View : Form
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
        public Match_View()
        {
            InitializeComponent();
        }
        private void matched_listview_view(string table_name, string op, string match_code_value)
        {
            try
            {
                conString();
                con.Open();
                string cmd = "SELECT * FROM " + table_name + " where match_code " + op + " '" + match_code_value + "' order by trans_code, trans_date, acct_name";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    Matched_Records.Items.Clear();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ListViewItem aa = new ListViewItem();
                            aa.SubItems.Add(DateTime.Parse(rdr.GetValue(1).ToString()).ToString("MM/dd/yyyy"));
                            aa.SubItems.Add(rdr.GetValue(2).ToString());
                            aa.SubItems.Add(rdr.GetValue(3).ToString());
                            aa.SubItems.Add(String.Format("{0:n}", Double.Parse(rdr.GetValue(4).ToString())));
                            aa.SubItems.Add(rdr.GetValue(6).ToString());
                            Matched_Records.Items.Add(aa);
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
                con.Open();
                string cmd = "SELECT COUNT(acct_name) FROM " + table_name + " where match_code <> 'U'";
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
                con.Open();
                string cmd = "SELECT sum(amount) FROM " + table_name + " where match_code <> 'U'";
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
                string cmd = "SELECT * FROM " + table_name + " where acct_name='"+acct_name+"' and acct_num='"+acct_num+"' and amount="+ amount.Replace(",","") +"";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            trans_code = rdr.GetValue(5).ToString();
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
        private void Match_View_Load(object sender, EventArgs e)
        {
            match_icbs_filter.SelectedIndex = 0;
            matched_listview_view("icbs_trans", "<>", "U");
            if (Matched_Records.Items.Count > 0)
            {
                string icbs_items = items_counter("icbs_trans");
                string icbs_sum = amount_sum("icbs_trans");
                lbl_icbs_value_items.Text = icbs_items;
                lbl_icbs_total_amount.Text = icbs_sum;
            }
        }

        private void Matched_Records_DoubleClick(object sender, EventArgs e)
        {
            foreach (ListViewItem listItem in Matched_Records.Items)
            {
                listItem.Checked = false;
            }

            string acct_name = Matched_Records.SelectedItems[0].SubItems[2].Text;
            string acct_number = Matched_Records.SelectedItems[0].SubItems[3].Text;
            string amount = Matched_Records.SelectedItems[0].SubItems[4].Text;
            string match_code = Matched_Records.SelectedItems[0].SubItems[5].Text;
            string trans_code = get_trans_code("icbs_trans", acct_name, acct_number, amount);

            View_Match_Selected_Data sd = new View_Match_Selected_Data();
            sd.acct_name = acct_name;
            sd.acct_number = acct_number;
            sd.amount = amount;
            sd.match_code = match_code;
            sd.trans_code = trans_code;
            sd.ShowDialog();
        }
        private static string get_match_ref(string table_name, string acct_name, string acct_num, string amount)
        {
            string match_ref = "";
            try
            {
                OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
                Conf.conf dbcon;
                con = new OleDbConnection();
                dbcon = new Conf.conf();
                con.ConnectionString = dbcon.getConnectionString();
                con.Open();
                string cmd = "SELECT ID FROM " + table_name + " where acct_name='"+acct_name+"' and acct_num='"+acct_num+"' and amount="+amount.Replace(",","")+"";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            match_ref = rdr.GetValue(0).ToString();
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return match_ref;
        }
        
        private void undo_force_match(string table_name, string field_name, string table_id)
        {
            try
            {
                conString();
                con.Open();
                string cmd = "update " + table_name + " set match_code='U', remarks = Null, match_ref=0 where "+field_name+"=" + table_id + "";
                OleDbCommand command = new OleDbCommand(cmd, con);
                OleDbDataReader rdr = command.ExecuteReader();
                con.Close();
                if (table_name == "icbs_trans")
                {
                    MessageBox.Show("Success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void btn_undo_fm_Click(object sender, EventArgs e)
        {
            string match_code = Matched_Records.CheckedItems[0].SubItems[5].Text;
            if (match_code == "F")
            {
                try
                {
                    string var_date = Matched_Records.CheckedItems[0].SubItems[1].Text;
                    string acct_name = Matched_Records.CheckedItems[0].SubItems[2].Text;
                    string acct_num = Matched_Records.CheckedItems[0].SubItems[3].Text;
                    string amount = Matched_Records.CheckedItems[0].SubItems[4].Text;

                    string match_ref = get_match_ref("icbs_trans", acct_name, acct_num, amount);

                    undo_force_match("icbs_trans", "ID", match_ref);
                    undo_force_match("scanned_trans", "match_ref", match_ref);

                    matched_listview_view("icbs_trans", "<>", "U");
                }
                catch
                {
                    MessageBox.Show("No data selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("You can't undo regular match data","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private void check_one_item(ItemCheckedEventArgs e)
        {
            if (!_doCheck)
            {
                return;
            }

            _doCheck = false;

            _doSelect = false;
            
            foreach (ListViewItem lvi in Matched_Records.Items)
            { // clear all checked items except the one we are working with
                if (lvi != e.Item)
                {
                    lvi.Checked = false;
                }
            }

            _doCheck = true;
           
            Matched_Records.SelectedItems.Clear();
            e.Item.Selected = e.Item.Checked;
            e.Item.Selected = false;

            _doSelect = true;
        }

        private void select_one_item()
        {
            if (!_doSelect)
            {
                return;
            }

            //suppress the ItemChecked Event

            _doCheck = false;
            foreach (ListViewItem lvi in Matched_Records.Items)
            {
                lvi.Checked = false;
            }
            
            if (Matched_Records.SelectedItems.Count > 0)
            {
                string listItem = Matched_Records.SelectedItems[0].Text;
                Matched_Records.SelectedItems[0].Checked = true;
            }
            _doCheck = true;
        }
        private void Matched_Records_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            check_one_item(e);
        }

        private void Matched_Records_SelectedIndexChanged(object sender, EventArgs e)
        {
            select_one_item();
        }

        private void match_icbs_filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (match_icbs_filter.Text == "Regular Match")
            {
                matched_listview_view("icbs_trans", "=", "R");
            }
            else if (match_icbs_filter.Text == "Force Match")
            {
                matched_listview_view("icbs_trans", "=", "F");
            }
            else
            {
                matched_listview_view("icbs_trans", "<>", "U");
            }
        }
    }
}
