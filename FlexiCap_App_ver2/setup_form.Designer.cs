namespace FlexiCap_App_ver2
{
    partial class Setup_form
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
            this.label4 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_icbs_source = new System.Windows.Forms.TextBox();
            this.btn_browse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_scanned_source = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_amount = new System.Windows.Forms.CheckBox();
            this.chk_acct_num = new System.Windows.Forms.CheckBox();
            this.chk_acct_name = new System.Windows.Forms.CheckBox();
            this.chk_tran_code = new System.Windows.Forms.CheckBox();
            this.openfile_browse = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 20);
            this.label4.TabIndex = 55;
            this.label4.Text = "Setup Import Settings.";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(504, 230);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(83, 23);
            this.btn_save.TabIndex = 54;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(504, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 20);
            this.button1.TabIndex = 53;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(225, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Enter location/file name of ICBS Transactions:";
            // 
            // tb_icbs_source
            // 
            this.tb_icbs_source.Location = new System.Drawing.Point(269, 188);
            this.tb_icbs_source.Name = "tb_icbs_source";
            this.tb_icbs_source.Size = new System.Drawing.Size(229, 20);
            this.tb_icbs_source.TabIndex = 51;
            // 
            // btn_browse
            // 
            this.btn_browse.Location = new System.Drawing.Point(504, 144);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(83, 20);
            this.btn_browse.TabIndex = 50;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.btn_browse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(244, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Enter location/file name of Scanned Transactions:";
            // 
            // tb_scanned_source
            // 
            this.tb_scanned_source.Location = new System.Drawing.Point(269, 145);
            this.tb_scanned_source.Name = "tb_scanned_source";
            this.tb_scanned_source.Size = new System.Drawing.Size(229, 20);
            this.tb_scanned_source.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "Setup Matching Criteria";
            // 
            // chk_amount
            // 
            this.chk_amount.AutoSize = true;
            this.chk_amount.Location = new System.Drawing.Point(403, 65);
            this.chk_amount.Name = "chk_amount";
            this.chk_amount.Size = new System.Drawing.Size(62, 17);
            this.chk_amount.TabIndex = 46;
            this.chk_amount.Text = "Amount";
            this.chk_amount.UseVisualStyleBackColor = true;
            // 
            // chk_acct_num
            // 
            this.chk_acct_num.AutoSize = true;
            this.chk_acct_num.Location = new System.Drawing.Point(269, 65);
            this.chk_acct_num.Name = "chk_acct_num";
            this.chk_acct_num.Size = new System.Drawing.Size(106, 17);
            this.chk_acct_num.TabIndex = 45;
            this.chk_acct_num.Text = "Account Number";
            this.chk_acct_num.UseVisualStyleBackColor = true;
            // 
            // chk_acct_name
            // 
            this.chk_acct_name.AutoSize = true;
            this.chk_acct_name.Location = new System.Drawing.Point(147, 65);
            this.chk_acct_name.Name = "chk_acct_name";
            this.chk_acct_name.Size = new System.Drawing.Size(97, 17);
            this.chk_acct_name.TabIndex = 44;
            this.chk_acct_name.Text = "Account Name";
            this.chk_acct_name.UseVisualStyleBackColor = true;
            // 
            // chk_tran_code
            // 
            this.chk_tran_code.AutoSize = true;
            this.chk_tran_code.Location = new System.Drawing.Point(18, 65);
            this.chk_tran_code.Name = "chk_tran_code";
            this.chk_tran_code.Size = new System.Drawing.Size(110, 17);
            this.chk_tran_code.TabIndex = 43;
            this.chk_tran_code.Text = "Transaction Code";
            this.chk_tran_code.UseVisualStyleBackColor = true;
            // 
            // openfile_browse
            // 
            this.openfile_browse.FileName = "openFileDialog1";
            // 
            // setup_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 272);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_icbs_source);
            this.Controls.Add(this.btn_browse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_scanned_source);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chk_amount);
            this.Controls.Add(this.chk_acct_num);
            this.Controls.Add(this.chk_acct_name);
            this.Controls.Add(this.chk_tran_code);
            this.Name = "setup_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "setup_form";
            this.Load += new System.EventHandler(this.setup_form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_icbs_source;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_scanned_source;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chk_amount;
        private System.Windows.Forms.CheckBox chk_acct_num;
        private System.Windows.Forms.CheckBox chk_acct_name;
        private System.Windows.Forms.CheckBox chk_tran_code;
        private System.Windows.Forms.OpenFileDialog openfile_browse;
    }
}