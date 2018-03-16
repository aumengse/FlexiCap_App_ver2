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
using System.IO;

namespace FlexiCap_App_ver2
{
    public partial class Import_Match : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        public bool trans_code { get; set; }
        public bool acct_name { get; set; }
        public bool acct_num { get; set; }
        public bool amount { get; set; }
        private DataSet ds = new DataSet();
        private DataSet dsu = new DataSet();

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
            if(btn_cancel.Text == "Cancel")
            {
                bg_worker.CancelAsync();
            }
            else
            {
                Summary_View sv = new Summary_View();
                this.Close();
                sv.Show();
                
            }
            
        }

        private void bg_worker_DoWork(object sender, DoWorkEventArgs e)
        { 
            for (int i = 0; i <= 100; i+=2)
            {
                Thread.Sleep(100);
                if (bg_worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else if (i == 10)
                {                    
                    label1.Invoke( new Action(() => label1.Visible = true));
                    get_data_SCANNED("depo_slip");
                    inserting_SCANNED("depo_slip");

                    get_data_SCANNED("with_slip");
                    inserting_SCANNED("with_slip");

                    lbl_check1.Invoke(new Action(() => lbl_check1.Visible = true));
                    label2.Invoke(new Action(() => label2.Visible = true));
                    bg_worker.ReportProgress(i);
                }
                else if (i == 40)
                {
                    inserting_ICBS();
                    lbl_check2.Invoke(new Action(() => lbl_check2.Visible = true));
                    label3.Invoke(new Action(() => label3.Visible = true));
                    bg_worker.ReportProgress(i);
                }
                else if (i == 60)
                {

                    matching_data();
                    unmatching_data("scanned_trans","icbs_trans");
                    unmatching_data("icbs_trans","scanned_trans");
                    bg_worker.ReportProgress(i);

                }
                else 
                {

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
                btn_cancel.Text = "Summary";
                btn_dashboard.Enabled = true;
                

            }
        }

        public void display(string text)
        {
            MessageBox.Show(text);

        }
        
        private static string db_location(string filepath)
        {
            OleDbConnection connection = new OleDbConnection(); //Initialize OleDBConnection
            Conf.conf dbconnection;
            connection = new OleDbConnection();
            dbconnection = new Conf.conf();
            connection.ConnectionString = dbconnection.getConnectionString();
            string holder = "";

            string command_text = "SELECT scanned_path,icbs_path from settings";
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(command_text, connection);
                OleDbDataReader rdr = command.ExecuteReader();
                rdr.Read();
                
                if(filepath=="scanned_path")
                {
                    holder = rdr.GetValue(0).ToString();
                }
                else
                {
                    holder = rdr.GetValue(1).ToString();
                }
                    
                connection.Close();
            }
            return holder;
        }


        public void get_data_SCANNED(string table_name)
        {
            try
            {
                OleDbConnection impt_con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + db_location("scanned_path") + "; Persist Security Info=False;");
                impt_con.Open();
                OleDbCommand select_cmd = new OleDbCommand();
                select_cmd.CommandText = "SELECT trans_date,acct_name,acct_num,amount,image_path,trans_code FROM [" + table_name + "] where is_import=" + false + "";
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

        private void inserting_SCANNED(string table_name)
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

                    String query = "INSERT INTO scanned_trans (trans_date,acct_name,acct_num,amount,image_path,trans_code) VALUES(@trans_date, @acct_name, @acct_num, @amount, @image_path, @trans_code)";
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

                    if (!string.IsNullOrWhiteSpace(row.Cells[4].Value.ToString()))
                    {
                        cmd.Parameters.AddWithValue("@image_path", Path.GetFileName(row.Cells[4].Value.ToString()));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@image_path", DBNull.Value);
                    }
                        cmd.Parameters.AddWithValue("@trans_code", row.Cells[5].Value.ToString());

                    string acct_name = row.Cells[2].Value.ToString();

                    if (table_name == "depo_slip")
                    {
                        mark_imported_data("depo_slip", acct_name);
                    }
                    else
                    {
                        mark_imported_data("with_slip", acct_name);
                    }

                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            catch(Exception e)
            {
                //MessageBox.Show("INSERTING: "+e.Message);
            }
           

        }

        private void mark_imported_data(string table_name, string acct_num)
        {
            //string cmds = "";
            string wew = acct_num;
            string loc = db_location("scanned_path");
            try
            {
                OleDbConnection cons = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + loc + "'; Persist Security Info=False;");
                cons.Open();
                OleDbCommand commands = new OleDbCommand();
                commands.Connection = cons;
                string cmds = "update " + table_name + " set is_import=@is_import where acct_num=@acct_num";

                commands.Parameters.AddWithValue("@is_import", true);
                commands.Parameters.AddWithValue("@acct_num", acct_num);
                commands.CommandText = cmds;
                commands.ExecuteNonQuery();
                cons.Close();

                cons.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void inserting_ICBS()
        {
            conString();
            DateTime date;
            string[] lines = System.IO.File.ReadAllLines(db_location("icbs_path").ToString());
            
            try
            {
                foreach (string line in lines) //read all records
                {
                    string[] col = line.Split(new char[] { ',' });
                    string date_string = DateTime.Parse(col[1]).ToString("MM/dd/yyyy");
                    date = DateTime.Parse(date_string);

                    con.Open();

                    String query = "INSERT INTO icbs_trans (trans_code,trans_date,acct_name,acct_num,amount) VALUES(@code, @date, @acct_name, @acct_num, @amount)";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    cmd.Parameters.AddWithValue("@code", col[0]);
                    cmd.Parameters.AddWithValue("@date", date); // set parameterized query @a to fname parameter
                    cmd.Parameters.AddWithValue("@acct_name", col[2]); // set parameterized query @b to mname parameter
                    cmd.Parameters.AddWithValue("@acct_num", col[3]);
                    cmd.Parameters.AddWithValue("@amount", col[4]);
                    cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //MessageBox.Show(" ICBS Import Successfully");

        }

        /// <summary>
        /// /////////////////////////////////   MATCHING BEGINS HERE ~~~~~~~~~~~~~~
        /// </summary>
        /// 

        private void get_bool()
        {
            //bool trans_code, acct_name, acct_num, amount;
            string cmd = "SELECT trans_code,acct_name,acct_num,amount from settings ";
            {

                con.Open();
                OleDbCommand command = new OleDbCommand(cmd, con);
                OleDbDataReader rdr = command.ExecuteReader();
                rdr.Read();

                trans_code = (bool)(rdr.GetValue(0));
                acct_name = (bool)(rdr.GetValue(1));
                acct_num = (bool)(rdr.GetValue(2));
                amount = (bool)(rdr.GetValue(3));
                con.Close();
            }

        }

        private void matching_data()
        {
            
            try
            {
               
                    //SINGLES
                    if (trans_code == true & acct_name == false & acct_num == false & amount == false) //1000
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON icbs_trans.trans_code = scanned_trans.trans_code; ";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }
                    else if (trans_code == false & acct_name == true & acct_num == false & amount == false) //0100
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON icbs_trans.acct_name = scanned_trans.acct_name;";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }
                    else if (trans_code == false & acct_name == false & acct_num == true & amount == false) //0010
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON icbs_trans.acct_num = scanned_trans.acct_num;";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }
                    else if (trans_code == false & acct_name == false & acct_num == false & amount == true) //0001
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON icbs_trans.amount = scanned_trans.amount;";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }
                    //DOUBLES trans_code
                    else if (trans_code == true & acct_name == true & acct_num == false & amount == false) //1100
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON (icbs_trans.acct_name = scanned_trans.acct_name) AND (icbs_trans.trans_code = scanned_trans.trans_code);";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }
                    else if (trans_code == true & acct_name == false & acct_num == true & amount == false) //1010
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON (icbs_trans.acct_num = scanned_trans.acct_num) AND (icbs_trans.trans_code = scanned_trans.trans_code);";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }
                    else if (trans_code == true & acct_name == false & acct_num == false & amount == true) //1001
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON (icbs_trans.amount = scanned_trans.amount) AND (icbs_trans.trans_code = scanned_trans.trans_code);";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }

                    //DOUBLES acct_name

                    else if (trans_code == false & acct_name == true & acct_num == true & amount == false) //0110
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON(icbs_trans.acct_num = scanned_trans.acct_num) AND(icbs_trans.acct_name = scanned_trans.acct_name);";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }
                    else if (trans_code == false & acct_name == true & acct_num == false & amount == true) //0101
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON (icbs_trans.amount = scanned_trans.amount) AND (icbs_trans.acct_name = scanned_trans.acct_name);";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }

                    //DOUBLES acct_num

                    else if (trans_code == false & acct_name == false & acct_num == true & amount == true) //0011
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON (icbs_trans.acct_num = scanned_trans.acct_num) AND (icbs_trans.amount = scanned_trans.amount);";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }

                    //TRIPLES 

                    else if (trans_code == true & acct_name == true & acct_num == true & amount == false) //1110
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.IDFROM icbs_trans INNER JOIN scanned_trans ON(icbs_trans.acct_name = scanned_trans.acct_name) " +
                                            "AND(icbs_trans.trans_code = scanned_trans.trans_code) AND(icbs_trans.acct_num = scanned_trans.acct_num);";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }

                    else if (trans_code == true & acct_name == true & acct_num == false & amount == true) //1101
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON(icbs_trans.amount = scanned_trans.amount) " +
                                            "AND(icbs_trans.acct_name = scanned_trans.acct_name) AND(icbs_trans.trans_code = scanned_trans.trans_code);";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }

                    else if (trans_code == true & acct_name == false & acct_num == true & amount == true) //1011
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON(icbs_trans.acct_num = scanned_trans.acct_num) " +
                                            "AND(icbs_trans.amount = scanned_trans.amount) AND(icbs_trans.trans_code = scanned_trans.trans_code);";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }

                    else if (trans_code == false & acct_name == true & acct_num == true & amount == true) //0111
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT icbs_trans.ID, scanned_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON(icbs_trans.acct_name = scanned_trans.acct_name) " +
                                            "AND(icbs_trans.acct_num = scanned_trans.acct_num) AND(icbs_trans.amount = scanned_trans.amount);";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();
                    }
                    else
                    {
                        con.Open();
                        OleDbCommand cmd_s1 = new OleDbCommand();
                        cmd_s1.CommandText = "SELECT scanned_trans.ID,icbs_trans.ID FROM icbs_trans INNER JOIN scanned_trans ON(icbs_trans.trans_code = scanned_trans.trans_code) AND(icbs_trans.amount = scanned_trans.amount) " +
                                           "AND(icbs_trans.acct_num = scanned_trans.acct_num) AND(icbs_trans.acct_name = scanned_trans.acct_name) AND(icbs_trans.trans_date = scanned_trans.trans_date);";
                        cmd_s1.Connection = con;
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd_s1);
                        da.Fill(ds);
                        con.Close();

                    }

                    foreach (DataTable dt in ds.Tables)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            con.Open();
                            string nw_cmd = "UPDATE [scanned_trans] SET match_code='R', trans_src='SCAN', match_ref= " + dr["icbs_trans.id"] + " where id=" + dr["scanned_trans.id"] + "";
                            {

                                OleDbCommand nw_command = new OleDbCommand(nw_cmd, con);
                                nw_command.ExecuteNonQuery();
                            }
                            con.Close();

                            con.Open();
                            string nw_cmd_icbs = "UPDATE [icbs_trans] SET match_code='R', trans_src='ICBS', match_ref= " + dr["scanned_trans.id"] + " where id=" + dr["icbs_trans.id"] + "";
                            {

                                OleDbCommand nw_command2 = new OleDbCommand(nw_cmd_icbs, con);
                                nw_command2.ExecuteNonQuery();
                            }
                            con.Close();
                        }
                    }

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        private void unmatching_data(string tbl_fr, string tbl_to)
        {

            try
            {
                conString();
                //SINGLES
                if (trans_code == true & acct_name == false & acct_num == false & amount == false) //1000
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM "+ tbl_fr +" WHERE NOT EXISTS( SELECT  id FROM "+ tbl_to +" WHERE "+ tbl_fr + ".trans_code = "+ tbl_to +".trans_code );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }
            
                else if (trans_code == false & acct_name == true & acct_num == false & amount == false) //0100
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".acct_name = " + tbl_to + ".acct_name );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }
                else if (trans_code == false & acct_name == false & acct_num == true & amount == false) //0010
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".acct_num = " + tbl_to + ".acct_num );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();

                }
                else if (trans_code == false & acct_name == false & acct_num == false & amount == true) //0001
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".amount = " + tbl_to + ".amount );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();

                }
                //DOUBLES trans_code
                else if (trans_code == true & acct_name == true & acct_num == false & amount == false) //1100
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".trans_code = " + tbl_to + ".trans_code and " + tbl_fr + ".acct_name = " + tbl_to + ".acct_name );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }
                else if (trans_code == true & acct_name == false & acct_num == true & amount == false) //1010
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".trans_code = " + tbl_to + ".trans_code and " + tbl_fr + ".acct_num = " + tbl_to + ".acct_num );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }
                else if (trans_code == true & acct_name == false & acct_num == false & amount == true) //1001
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".trans_code = " + tbl_to + ".trans_code and " + tbl_fr + ".amount = " + tbl_to + ".amount );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }

                //DOUBLES acct_name

                else if (trans_code == false & acct_name == true & acct_num == true & amount == false) //0110
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".acct_name = " + tbl_to + ".acct_name and " + tbl_fr + ".acct_num = " + tbl_to + ".acct_num );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }
                else if (trans_code == false & acct_name == true & acct_num == false & amount == true) //0101
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".acct_name = " + tbl_to + ".acct_name and " + tbl_fr + ".amount = " + tbl_to + ".amount );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }

                //DOUBLES acct_num

                else if (trans_code == false & acct_name == false & acct_num == true & amount == true) //0011
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".acct_num = " + tbl_to + ".acct_num and " + tbl_fr + ".amount = " + tbl_to + ".amount );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }

                //TRIPLES 

                else if (trans_code == true & acct_name == true & acct_num == true & amount == false) //1110
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".trans_code = " + tbl_to + ".trans_code and " + tbl_fr + ".acct_name = " + tbl_to + ".acct_name " +
                        "and " + tbl_fr + ".acct_num = " + tbl_to + ".acct_num );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }

                else if (trans_code == true & acct_name == true & acct_num == false & amount == true) //1101
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".trans_code = " + tbl_to + ".trans_code and " + tbl_fr + ".acct_name = " + tbl_to + ".acct_name " +
                        "and " + tbl_fr + ".amount = " + tbl_to + ".amount );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }

                else if (trans_code == true & acct_name == false & acct_num == true & amount == true) //1011
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".trans_code = " + tbl_to + ".trans_code and " + tbl_fr + ".acct_num = " + tbl_to + ".acct_num " +
                        "and " + tbl_fr + ".amount = " + tbl_to + ".amount );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }

                else if (trans_code == false & acct_name == true & acct_num == true & amount == true) //0111
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM " + tbl_fr + " WHERE NOT EXISTS( SELECT  id FROM " + tbl_to + " WHERE " + tbl_fr + ".acct_name = " + tbl_to + ".acct_name and " + tbl_fr + ".acct_num = " + tbl_to + ".acct_num " +
                        "and " + tbl_fr + ".amount = " + tbl_to + ".amount );";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }
                else
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandText = "SELECT DISTINCT id FROM "+ tbl_fr +" WHERE NOT EXISTS(SELECT id FROM "+tbl_to+ " WHERE " + tbl_fr + ".trans_date = " + tbl_to + ".trans_date and " + tbl_fr + ".acct_name = " + tbl_to + ".acct_name and " + tbl_fr + ".amount = " + tbl_to + ".amount " +
                                        "and " + tbl_fr + ".acct_num = " + tbl_to + ".acct_num and " + tbl_fr + ".trans_code = " + tbl_to + ".trans_code);";
                    cmd.Connection = con;
                    OleDbDataAdapter dau = new OleDbDataAdapter(cmd);
                    dau.Fill(dsu);
                    con.Close();
                }

                foreach (DataTable dtu in dsu.Tables)
                {
                    foreach (DataRow dru in dtu.Rows)
                    {

                        
                        if (tbl_fr == "icbs_trans")
                        {
                            
                            con.Open();
                            string nw_cmd_i = "UPDATE [icbs_trans] SET match_code='U', trans_src='ICBS' where id=" + dru["id"] + "";
                            {
                                //updating ICBS
                                OleDbCommand nw_command_i = new OleDbCommand(nw_cmd_i, con);
                                nw_command_i.ExecuteNonQuery();
                                
                            }
                            con.Close();
                        }
                        else
                        {
                            con.Open();
                            string nw_cmd_s = "UPDATE [scanned_trans] SET match_code='U', trans_src='SCAN' where id=" + dru["id"] + "";
                            {
                                //updating SCANNED
                                OleDbCommand nw_command_s = new OleDbCommand(nw_cmd_s, con);
                                nw_command_s.ExecuteNonQuery();
                                
                            }
                            con.Close();
                        }

                    }
                }

            }
            catch(Exception exx)
            {
                MessageBox.Show("UNMATCHING: "+exx.Message);
            }
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            
            Dashboard d = new Dashboard();
            d.ShowDialog();
            this.Close();
        }
    }
}
