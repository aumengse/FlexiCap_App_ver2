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

    public partial class TransMain : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;

        public TransMain()
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

    
        private void TransMain_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void initializeDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to initialize database? ", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                //Initialize_DB in_db = new Initialize_DB();
                //in_db.Show();
            }
            
        }

        private void matchingToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Import_Match mp = new Import_Match();
            mp.Show();
        }

        private void archiveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void importedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            View_Imported vi = new View_Imported();
            vi.Show();
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setup_form sf = new Setup_form();
            sf.ShowDialog();
        }

        private void summaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Summary_View sv = new Summary_View();
            sv.Show();
        }
    }
}
