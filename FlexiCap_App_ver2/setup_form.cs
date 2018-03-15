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
using System.IO;

namespace FlexiCap_App_ver2
{
    public partial class Setup_form : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        public Setup_form()
        {
            InitializeComponent();
        }
        private void conString()
        {
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile_browse_icbs = new OpenFileDialog();

            openfile_browse.InitialDirectory = @"C:\";
            openfile_browse_icbs.Title = "Browse Database";

            openfile_browse.ValidateNames = false;
            openfile_browse.CheckFileExists = false;
            openfile_browse_icbs.CheckPathExists = true;

            openfile_browse.DefaultExt = "accdb";
            openfile_browse.Filter = "Access files (*.accdb)|*.accdb";
            openfile_browse.FilterIndex = 2;
            openfile_browse.RestoreDirectory = true;


            openfile_browse.ReadOnlyChecked = true;
            openfile_browse.ShowReadOnly = true;


            if (openfile_browse.ShowDialog() == DialogResult.OK)

            {
                tb_scanned_source.Text = openfile_browse.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile_browse_icbs = new OpenFileDialog();

            openfile_browse.InitialDirectory = @"C:\";
            openfile_browse_icbs.Title = "Browse Text File";

            openfile_browse.CheckFileExists = true;
            openfile_browse_icbs.CheckPathExists = true;

            openfile_browse.DefaultExt = "txt";
            openfile_browse.Filter = "Text files (*.txt)|*.txt";
            openfile_browse.FilterIndex = 2;
            openfile_browse.RestoreDirectory = true;


            openfile_browse.ReadOnlyChecked = true;
            openfile_browse.ShowReadOnly = true;


            if (openfile_browse.ShowDialog() == DialogResult.OK)

            {
                tb_icbs_source.Text = openfile_browse.FileName;
            }
        }
        private void update_settings()
        {
            conString();
            try
            {
                //OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\PC-23\Desktop\TVVS.accdb; Persist Security Info=False;");
                conString();
                con.Open();
                string cmd = "update settings set trans_code=" + chk_tran_code.Checked + ", acct_name=" + chk_acct_name.Checked + ", acct_num=" + chk_acct_num.Checked + ", amount=" + chk_amount.Checked + ", scanned_path='" + tb_scanned_source.Text + "', icbs_path='" + tb_icbs_source.Text + "', image_path='"+txt_image.Text+"'";
                OleDbCommand command = new OleDbCommand(cmd, con);
                OleDbDataReader rdr = command.ExecuteReader();
                con.Close();
                MessageBox.Show("Save Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void insert_settings()
        {
            conString();
            try
            {
                con.Open();
                String query = "INSERT INTO settings (trans_code,acct_name,acct_num,amount,scanned_path,icbs_path,image_path) VALUES(@trans_code, @acct_name, @acct_num, @amount, @scanned_path, @icbs_path)";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.Parameters.AddWithValue("@trans_code", chk_tran_code.CheckState);
                cmd.Parameters.AddWithValue("@acct_name", chk_acct_name.CheckState);
                cmd.Parameters.AddWithValue("@acct_num", chk_acct_num.CheckState);
                cmd.Parameters.AddWithValue("@amount", chk_amount.CheckState);
                cmd.Parameters.AddWithValue("@scanned_path", tb_scanned_source.Text);
                cmd.Parameters.AddWithValue("@icbs_path", tb_icbs_source.Text);
                cmd.Parameters.AddWithValue("@image_path", txt_image.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Save Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static string data_counter(string table_name)
        {
            int counter = 0;
            OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
            Conf.conf dbcon;
            con = new OleDbConnection();
            dbcon = new Conf.conf();
            con.ConnectionString = dbcon.getConnectionString();
            try
            {
                con.Open();
                string cmd = "SELECT * FROM "+table_name+"";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            //trans_code = rdr.GetValue(1).ToString();
                            counter += 1; 
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return counter.ToString();
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(data_counter("settings")) > 0)
            {
                update_settings();
            }
            else
            {
                insert_settings();
            }
        }

        private void setup_form_Load(object sender, EventArgs e)
        {
            conString();
            try
            {
                con.Open();
                string cmd = "SELECT * FROM settings";
                {
                    OleDbCommand command = new OleDbCommand(cmd, con);
                    OleDbDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            chk_tran_code.Checked = Convert.ToBoolean(rdr.GetValue(1).ToString());
                            chk_acct_name.Checked = Convert.ToBoolean(rdr.GetValue(2).ToString());
                            chk_acct_num.Checked = Convert.ToBoolean(rdr.GetValue(3).ToString());
                            chk_amount.Checked = Convert.ToBoolean(rdr.GetValue(4).ToString());
                            tb_scanned_source.Text = rdr.GetValue(5).ToString();
                            tb_icbs_source.Text = rdr.GetValue(6).ToString();
                            txt_image.Text = rdr.GetValue(7).ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txt_image.Text = fbd.SelectedPath + @"\";
            }
        }
    }
}
