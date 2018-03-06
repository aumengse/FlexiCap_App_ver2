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
    public partial class setup_form : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        public setup_form()
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

            openfile_browse.CheckFileExists = true;
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
            openfile_browse_icbs.Title = "Browse Database";

            openfile_browse.CheckFileExists = true;
            openfile_browse_icbs.CheckPathExists = true;

            openfile_browse.DefaultExt = "accdb";
            openfile_browse.Filter = "Access files (*.accdb)|*.accdb";
            openfile_browse.FilterIndex = 2;
            openfile_browse.RestoreDirectory = true;


            openfile_browse.ReadOnlyChecked = true;
            openfile_browse.ShowReadOnly = true;


            if (openfile_browse.ShowDialog() == DialogResult.OK)

            {
                tb_icbs_source.Text = openfile_browse.FileName;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            conString();
            try
            {
                con.Open();
                String query = "INSERT INTO settings (trans_code,acct_name,acct_num,amount,scanned_path,icbs_path) VALUES(@trans_code, @acct_name, @acct_num, @amount, @scanned_path, @icbs_path)";
                OleDbCommand cmd = new OleDbCommand(query, con);
                cmd.Parameters.AddWithValue("@trans_code", chk_tran_code.CheckState);
                cmd.Parameters.AddWithValue("@acct_name", chk_acct_name.CheckState);
                cmd.Parameters.AddWithValue("@acct_num", chk_acct_num.CheckState);
                cmd.Parameters.AddWithValue("@amount", chk_amount.CheckState);
                cmd.Parameters.AddWithValue("@scanned_path", tb_scanned_source.Text);
                cmd.Parameters.AddWithValue("@icbs_path", tb_icbs_source.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
