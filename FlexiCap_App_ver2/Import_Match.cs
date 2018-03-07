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
    public partial class Import_Match : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        public Import_Match()
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

        private void Main_Process_Load(object sender, EventArgs e)
        {
            bg_worker.RunWorkerAsync(); this.Show();
            lbl_check3.Visible = false;
            lbl_check2.Visible = false;
            lbl_check1.Visible = false;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            bg_worker.CancelAsync();
        }

        private void bg_worker_DoWork(object sender, DoWorkEventArgs e)
        { 
            for (int i = 0; i <= 100; i+=2)
            {
                if (bg_worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else if (i == 10)
                {                    
                    label1.Invoke( new Action(() => label1.Visible = true));
                    get_data_SCANNED("depo_slip");
                    inserting("depo_slip");
                    get_data_SCANNED("with_slip");
                    inserting("with_slip");
                    lbl_check1.Invoke(new Action(() => lbl_check1.Visible = true));
                }
                else if (i == 40)
                {

                    label2.Invoke(new Action(() => label2.Visible = true));
                }
                else if (i == 70)
                {
                    lbl_check2.Invoke(new Action(() => lbl_check2.Visible = true));
                    matching_ICBS();
                    matching_SCAN();
                    unmatching_SCAN();
                    unmatching_ICBS();
                    label3.Invoke(new Action(() => label3.Visible = true));
                }
                else 
                {
                    mainprocess();
                    
                    bg_worker.ReportProgress(i);

                }
                
            }
            
        }

        private void bg_worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbar.Value = e.ProgressPercentage;
            lbl_percent.Text = e.ProgressPercentage.ToString() + " %";

        }

        private void bg_worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                display("Work Cancelled");
                lbl_check3.Invoke(new Action(() => lbl_check3.Visible = false));
            }
            else
            {
                lbl_check3.Invoke(new Action(() => lbl_check3.Visible = true));
                display("Work Successfully");
                
            }
        }

        private void mainprocess()
        {
            Thread.Sleep(100);
        }

        public void display(string text)
        {
            MessageBox.Show(text);

        }

        public string db_location()
        {
            string holder = "";
            conString();

            string cmd = "SELECT scanned_path from settings";
            {
                con.Open();
                OleDbCommand command = new OleDbCommand(cmd, con);
                OleDbDataReader rdr = command.ExecuteReader();
                rdr.Read();
                
                    holder = rdr.GetValue(0).ToString();
                con.Close();
            }
            return holder;
        }


        public void get_data_SCANNED(string table_name)
        {


            try
            {
                OleDbConnection impt_con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + db_location() + "; Persist Security Info=False;");
                impt_con.Open();
                OleDbCommand select_cmd = new OleDbCommand();
                select_cmd.CommandText = "SELECT trans_date,acct_name,acct_num,amount,trans_code FROM [" + table_name + "] where is_import=" + false + "";
                select_cmd.Connection = impt_con;

                OleDbDataAdapter da = new OleDbDataAdapter(select_cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                dg_data_imported.Invoke(new Action(() => dg_data_imported.DataSource = ds.Tables[0]));
                impt_con.Close();

                //MessageBox.Show("IMPORTED");

            }
            catch (Exception ex)
            {
                MessageBox.Show("GET DATA:" + ex.Message);
            }
        }

        private void inserting(string table_name)
        {
            string date_string;
            DateTime? date_x = null;
            conString();

            try
            {
                foreach (DataGridViewRow row in dg_data_imported.Rows)
                {
                    string is_emp_date = row.Cells[0].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(is_emp_date))
                    {
                        date_string = DateTime.Parse(row.Cells[0].Value.ToString()).ToString("MM/dd/yyyy");
                        date_x = DateTime.Parse(date_string);
                    }
                    //con.Close();
                    con.Open();

                    String query = "INSERT INTO scanned_trans (trans_date,acct_name,acct_num,amount,trans_code) VALUES(@trans_date, @acct_name, @acct_num, @amount, @trans_code)";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    if (!string.IsNullOrWhiteSpace(is_emp_date))
                    {
                        cmd.Parameters.AddWithValue("@trans_date", date_x); // set parameterized query @a to fname parameter
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@trans_date", DBNull.Value); // set parameterized query @a to fname parameter
                    }
                    if (!string.IsNullOrWhiteSpace(row.Cells[1].Value.ToString()))
                    {
                        cmd.Parameters.AddWithValue("@acct_name", row.Cells[1].Value.ToString()); // set parameterized query @b to mname parameter
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@acct_name", DBNull.Value); // set parameterized query @b to mname parameter
                    }
                    cmd.Parameters.AddWithValue("@acct_num", row.Cells[2].Value.ToString());
                    if (!string.IsNullOrWhiteSpace(row.Cells[3].Value.ToString()))
                    {
                        cmd.Parameters.AddWithValue("@amount", double.Parse(row.Cells[3].Value.ToString()));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@amount", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@trans_code", row.Cells[4].Value.ToString());
                    if (table_name == "depo_slip")
                    {
                        mark_imported_data("depo_slip", row.Cells[2].Value.ToString());
                    }
                    else
                    {
                        mark_imported_data("with_slip", row.Cells[2].Value.ToString());
                    }

                    //if (row is null)
                    //{
                    //    break;
                    //}

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch(Exception e)
            {
                MessageBox.Show("INSERTING: "+e.Message);
            }
           

        }

        private void mark_imported_data(string table_name, string acct_num)
        {
            string cmds = "";
            OleDbConnection cons = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + db_location() + "; Persist Security Info=False;");
            try
            {
                cons.Open();
                if (!string.IsNullOrWhiteSpace(acct_num))
                {
                    cmds = "update [" + table_name + "] set is_import=" + true + " where acct_num='" + acct_num + "'";
                }

                OleDbCommand commands = new OleDbCommand(cmds, cons);
                OleDbDataReader redr = commands.ExecuteReader();

                cons.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void matching_ICBS()
        {
            conString();
            int i;
            try
            {

                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT icbs_trans.ID,scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON(icbs_trans.trans_code = scanned_trans.trans_code) " +
                                "AND(icbs_trans.amount = scanned_trans.amount) AND(icbs_trans.acct_num = scanned_trans.acct_num) AND(icbs_trans.acct_name = scanned_trans.acct_name) AND(icbs_trans.trans_date = scanned_trans.trans_date);";
                cmd.Connection = con;
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        con.Open();
                        string nw_cmd = "UPDATE [icbs_trans] SET match_code='R', trans_src='ICBS', match_ref= " + dr["scanned_trans.id"] + " where id=" + dr["icbs_trans.id"] + "";
                        {

                            OleDbCommand nw_command = new OleDbCommand(nw_cmd, con);
                            nw_command.ExecuteNonQuery();
                            //MessageBox.Show("ICBS MATCHED Updated");

                        }
                        con.Close();
                        //MessageBox.Show(dr["id"].ToString());

                    }
                }
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void matching_SCAN()
        {
            conString();
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT scanned_trans.ID,icbs_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON(icbs_trans.trans_code = scanned_trans.trans_code) AND(icbs_trans.amount = scanned_trans.amount) " +
                                   "AND(icbs_trans.acct_num = scanned_trans.acct_num) AND(icbs_trans.acct_name = scanned_trans.acct_name) AND(icbs_trans.trans_date = scanned_trans.trans_date);";
                cmd.Connection = con;
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

                foreach (DataTable dt in ds.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        con.Open();
                        string nw_cmd = "UPDATE [scanned_trans] SET match_code='R', trans_src='SCAN', match_ref= " + dr["icbs_trans.id"] + " where id=" + dr["scanned_trans.id"] + "";
                        {

                            OleDbCommand nw_command = new OleDbCommand(nw_cmd, con);
                            nw_command.ExecuteNonQuery();
                            //MessageBox.Show("SCAN MATCHED Updated");

                        }
                        con.Close();
                        //MessageBox.Show(dr["id"].ToString());

                    }
                }
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //comment
            }
        }

        private void unmatching_ICBS()
        {
            conString();
            int e;
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT DISTINCT id FROM icbs_trans WHERE NOT EXISTS(SELECT  id FROM scanned_trans WHERE icbs_trans.trans_date = scanned_trans.trans_date and icbs_trans.acct_name = scanned_trans.acct_name and icbs_trans.amount = scanned_trans.amount " +
                                    "and icbs_trans.acct_num = scanned_trans.acct_num and icbs_trans.trans_code = scanned_trans.trans_code);";
                cmd.Connection = con;
                OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                DataSet dsu = new DataSet();
                dau.Fill(dsu);
                con.Close();

                foreach (DataTable dtu in dsu.Tables)
                {
                    foreach (DataRow dru in dtu.Rows)
                    {

                        con.Open();
                        string nw_cmd2 = "UPDATE [icbs_trans] SET match_code='U', trans_src='ICBS' where id=" + dru["id"] + "";
                        {

                            OleDbCommand nw_command2 = new OleDbCommand(nw_cmd2, con);
                            nw_command2.ExecuteNonQuery();
                            //MessageBox.Show("ICBS UNMATCHED Updated");

                        }
                        con.Close();
                        //MessageBox.Show(dru["id"].ToString());

                    }
                }
            }
            catch (Exception xx)
            {
                //MessageBox.Show(xx.Message);
            }
        }

        private void unmatching_SCAN()
        {
            conString();
            int e;
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandText = "SELECT DISTINCT id FROM scanned_trans WHERE NOT EXISTS(SELECT  id FROM icbs_trans WHERE scanned_trans.trans_date = icbs_trans.trans_date and scanned_trans.acct_name = icbs_trans.acct_name " +
                                    "and scanned_trans.acct_num = icbs_trans.acct_num  and scanned_trans.amount = icbs_trans.amount and scanned_trans.trans_code = icbs_trans.trans_code);";
                cmd.Connection = con;
                OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                DataSet dsu = new DataSet();
                dau.Fill(dsu);
                con.Close();

                foreach (DataTable dtu in dsu.Tables)
                {
                    foreach (DataRow dru in dtu.Rows)
                    {

                        con.Open();
                        string nw_cmd2 = "UPDATE [scanned_trans] SET match_code='U', trans_src='SCAN' where id=" + dru["id"] + "";
                        {

                            OleDbCommand nw_command2 = new OleDbCommand(nw_cmd2, con);
                            nw_command2.ExecuteNonQuery();
                            //MessageBox.Show("SCAN UNMATCHED Updated");

                        }
                        con.Close();
                        //MessageBox.Show(dru["id"].ToString());

                    }
                }
            }
            catch (Exception xx)
            {
                //MessageBox.Show(xx.Message);
            }
        }


    }
}
