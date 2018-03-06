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
    public partial class Main_Process : Form
    {
        private OleDbConnection con = new OleDbConnection(); //Initialize OleDBConnection
        private Conf.conf dbcon;
        public Main_Process()
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
                }
                else if (i == 40)
                {
                    lbl_check1.Invoke(new Action (() => lbl_check1.Visible = true));
                    label2.Invoke(new Action(() => label2.Visible = true));
                }
                else if (i == 70)
                {
                    lbl_check2.Invoke(new Action(() => lbl_check2.Visible = true));
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

        
    }
}
