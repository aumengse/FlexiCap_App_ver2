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
    public partial class View_Match_Selected_Data : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        private void conString()
        {
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
        }
        public View_Match_Selected_Data()
        {
            InitializeComponent();
        }
        public string acct_number = "";
        public string acct_name = "";
        public string amount = "";
        public string match_code = "";
        public string trans_code = "";
        private void selected_match_view(string table_name, string acct_number, string acct_name, string amount, string match_code, string trans_code)
        {
            string h_amount = amount.Replace(",", "");
            try
            {
                conString();
                con.Open();
                string cmd = "SELECT * FROM " + table_name + " where acct_num='"+ acct_number +"' and acct_name='"+ acct_name + "' and match_code='" + match_code + "' and amount="+ h_amount +" and trans_code='"+trans_code+"'";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            if (table_name == "icbs_trans")
                            {
                                txt_icbs_date.Text = DateTime.Parse(rdr.GetValue(1).ToString()).ToString("MM/dd/yyyy");
                                txt_icbs_acct_name.Text = rdr.GetValue(2).ToString();
                                txt_icbs_acct_num.Text = rdr.GetValue(3).ToString();
                                txt_icbs_amount.Text = String.Format("{0:n}", Double.Parse(rdr.GetValue(4).ToString()));
                            }
                            else
                            {
                                txt_scan_date.Text = DateTime.Parse(rdr.GetValue(1).ToString()).ToString("MM/dd/yyyy");
                                txt_scan_acct_name.Text = rdr.GetValue(2).ToString();
                                txt_scan_acct_num.Text = rdr.GetValue(3).ToString();
                                txt_scan_amount.Text = String.Format("{0:n}", Double.Parse(rdr.GetValue(4).ToString()));
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

        private void View_Match_Selected_Data_Load(object sender, EventArgs e)
        {
            selected_match_view("icbs_trans",acct_number, acct_name, amount, match_code, trans_code);
            selected_match_view("scanned_trans", acct_number, acct_name, amount, match_code, trans_code);

        }
    }
}
