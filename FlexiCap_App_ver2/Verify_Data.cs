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
    public partial class Verify_Data : Form
    {
        public Verify_Data()
        {
            InitializeComponent();
        }
        public string acct_name = "";
        public string acct_num = "";
        public string amount = "";
        public string scan_date = "";
        public string scan_trans_code = "";
        public string icbs_trans_code = "";

        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        private void conString()
        {
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
        }
        private static int get_id(string acct_num, string table_name)
        {
            int var_id = 0;
            OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
            Conf.conf dbcon;
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
            try
            {
                //OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\PC-23\Desktop\TVVS.accdb; Persist Security Info=False;");
                con.Open();
                string cmd = "SELECT * FROM " + table_name + " where acct_num = '" + acct_num + "'";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var_id = Convert.ToInt32(rdr.GetValue(0).ToString());
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return var_id;
        }
        private void force_match(string table_name, string acct_num, string acct_name, string trans_code, string remarks, int match_code, string var_date)
        {
            string date_string;
            DateTime? date_x = null;
            date_string = DateTime.Parse(var_date.ToString()).ToString("MM/dd/yyyy 00:00:00");
            date_x = DateTime.Parse(date_string);
            try
            {
                conString();
                con.Open();
                string cmd = "update " + table_name + " set match_code='F', remarks = '" + remarks + "', match_ref = " + match_code + " where acct_num='" + acct_num + "' and acct_name='"+ acct_name +"' and trans_code='"+ trans_code +"' and trans_date=@trans_date";
                OleDbCommand command = new OleDbCommand(cmd, con);
                command.Parameters.AddWithValue("@trans_date", date_x);
                OleDbDataReader rdr = command.ExecuteReader();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_force_match_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_remarks.Text))
            {
                DialogResult dialogResult = MessageBox.Show("Please Enter Reason for Force Matching", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //get id first, this is use in force match method
                int scan_id = get_id(acct_num, "scanned_trans");
                int icbs_id = get_id(txt_icbs_acct_num.Text, "icbs_trans");

                //Force matching method
                force_match("icbs_trans", txt_icbs_acct_num.Text,txt_icbs_acct_name.Text,icbs_trans_code, txt_remarks.Text, scan_id, txt_icbs_date.Text);
                force_match("scanned_trans", acct_num, acct_name,scan_trans_code, txt_remarks.Text, icbs_id, scan_date);

                MessageBox.Show("Force Match Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Unmatch_View uv = new Unmatch_View();
                uv.Show();
            }
        }
    }
}
