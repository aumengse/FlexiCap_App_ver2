namespace FlexiCap_App_ver2
{
    partial class Import_Match
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_warning = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.bg_worker = new System.ComponentModel.BackgroundWorker();
            this.pbar = new System.Windows.Forms.ProgressBar();
            this.lbl_percent = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_check3 = new System.Windows.Forms.Label();
            this.lbl_check2 = new System.Windows.Forms.Label();
            this.lbl_check1 = new System.Windows.Forms.Label();
            this.dg_data_imported = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_data_imported)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "3. Regular Matching";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "2. Import ICBS Transactions";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "1. Import Scanned Transactions";
            this.label1.Visible = false;
            // 
            // lbl_warning
            // 
            this.lbl_warning.AutoSize = true;
            this.lbl_warning.Location = new System.Drawing.Point(36, 41);
            this.lbl_warning.Name = "lbl_warning";
            this.lbl_warning.Size = new System.Drawing.Size(272, 13);
            this.lbl_warning.TabIndex = 2;
            this.lbl_warning.Text = "Warning: This window will perform the following process:";
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(508, 277);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 28);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // bg_worker
            // 
            this.bg_worker.WorkerReportsProgress = true;
            this.bg_worker.WorkerSupportsCancellation = true;
            this.bg_worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bg_worker_DoWork);
            this.bg_worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bg_worker_ProgressChanged);
            this.bg_worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bg_worker_RunWorkerCompleted);
            // 
            // pbar
            // 
            this.pbar.Location = new System.Drawing.Point(39, 228);
            this.pbar.Name = "pbar";
            this.pbar.Size = new System.Drawing.Size(544, 23);
            this.pbar.TabIndex = 4;
            // 
            // lbl_percent
            // 
            this.lbl_percent.AutoSize = true;
            this.lbl_percent.Location = new System.Drawing.Point(589, 238);
            this.lbl_percent.Name = "lbl_percent";
            this.lbl_percent.Size = new System.Drawing.Size(24, 13);
            this.lbl_percent.TabIndex = 5;
            this.lbl_percent.Text = "0 %";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_check3);
            this.groupBox1.Controls.Add(this.lbl_check2);
            this.groupBox1.Controls.Add(this.lbl_check1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(39, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(544, 121);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // lbl_check3
            // 
            this.lbl_check3.AutoSize = true;
            this.lbl_check3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_check3.Location = new System.Drawing.Point(25, 71);
            this.lbl_check3.Name = "lbl_check3";
            this.lbl_check3.Size = new System.Drawing.Size(22, 24);
            this.lbl_check3.TabIndex = 5;
            this.lbl_check3.Text = "✓";
            // 
            // lbl_check2
            // 
            this.lbl_check2.AutoSize = true;
            this.lbl_check2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_check2.Location = new System.Drawing.Point(25, 43);
            this.lbl_check2.Name = "lbl_check2";
            this.lbl_check2.Size = new System.Drawing.Size(22, 24);
            this.lbl_check2.TabIndex = 4;
            this.lbl_check2.Text = "✓";
            // 
            // lbl_check1
            // 
            this.lbl_check1.AutoSize = true;
            this.lbl_check1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_check1.Location = new System.Drawing.Point(25, 17);
            this.lbl_check1.Name = "lbl_check1";
            this.lbl_check1.Size = new System.Drawing.Size(22, 24);
            this.lbl_check1.TabIndex = 3;
            this.lbl_check1.Text = "✓";
            // 
            // dg_data_imported
            // 
            this.dg_data_imported.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_data_imported.Location = new System.Drawing.Point(21, 329);
            this.dg_data_imported.Name = "dg_data_imported";
            this.dg_data_imported.Size = new System.Drawing.Size(439, 150);
            this.dg_data_imported.TabIndex = 7;
            this.dg_data_imported.Visible = false;
            // 
            // Import_Match
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 329);
            this.Controls.Add(this.dg_data_imported);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_percent);
            this.Controls.Add(this.pbar);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.lbl_warning);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Import_Match";
            this.Text = "Import and Match Transactions";
            this.Load += new System.EventHandler(this.Main_Process_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_data_imported)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_warning;
        private System.Windows.Forms.Button btn_cancel;
        private System.ComponentModel.BackgroundWorker bg_worker;
        private System.Windows.Forms.ProgressBar pbar;
        private System.Windows.Forms.Label lbl_percent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_check3;
        private System.Windows.Forms.Label lbl_check2;
        private System.Windows.Forms.Label lbl_check1;
        private System.Windows.Forms.DataGridView dg_data_imported;
    }
}