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

namespace FlexiCap_App_ver2
{
    public partial class Match_View : Form
    {
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
            matched_listview_view("scanned_trans", "=", "R");
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
    }
}
