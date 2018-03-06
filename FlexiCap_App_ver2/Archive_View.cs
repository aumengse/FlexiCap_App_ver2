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
    public partial class Archive_View : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        private void conString()
        {
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
        }
        public Archive_View()
        {
            InitializeComponent();
        }
        private void view_archive(string field_name, string op, string field_value)
        {
            try
            {
                conString();
                con.Open();
                string cmd = "SELECT * FROM archive_trans where "+field_name+""+op+""+field_name+" ORDER by trans_code,trans_date,acct_name";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    listview_archive.Items.Clear();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ListViewItem aa = new ListViewItem(rdr.GetValue(3).ToString());
                            aa.SubItems.Add(DateTime.Parse(rdr.GetValue(2).ToString()).ToString("MM/dd/yyyy"));
                            aa.SubItems.Add(rdr.GetValue(4).ToString());
                            aa.SubItems.Add(rdr.GetValue(5).ToString());
                            aa.SubItems.Add(String.Format("{0:n}", Double.Parse(rdr.GetValue(6).ToString())));
                            aa.SubItems.Add(rdr.GetValue(9).ToString());
                            aa.SubItems.Add(rdr.GetValue(7).ToString());
                            aa.SubItems.Add(rdr.GetValue(10).ToString());
                            listview_archive.Items.Add(aa);
                        }
                    }
                }
                con.Close();
                alternateColor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Archive_View_Load(object sender, EventArgs e)
        {
            view_archive("trans_code","<>","N");
        }

        private void alternateColor()
        {
            foreach (ListViewItem item in listview_archive.Items)

                if ((item.Index % 2) == 1)
                    item.BackColor = Color.White;                   
                else
                    item.BackColor = Color.LightBlue;
        }

        private void listview_archive_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_scan_trans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_scan_trans.Text == "Deposits")
            {
                view_archive("trans_code","=","DEPO");
            }
            else if (cmb_scan_trans.Text == "Withdrawals")
            {
                view_archive("trans_code","=","DEPO");
            }
            else
            {
                view_archive("trans_code", "<>", "All");
            }
        }   
    }
}
