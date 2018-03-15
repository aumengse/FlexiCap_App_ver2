namespace FlexiCap_App_ver2
{
    partial class Match_View
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
            this.label2 = new System.Windows.Forms.Label();
            this.match_icbs_filter = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Matched_Records = new System.Windows.Forms.ListView();
            this.check = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Account_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Account_Number = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Amount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbl_icbs_total_amount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_icbs_value_items = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_undo_fm = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(416, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Filter:";
            // 
            // match_icbs_filter
            // 
            this.match_icbs_filter.FormattingEnabled = true;
            this.match_icbs_filter.Items.AddRange(new object[] {
            "All",
            "Regular Match",
            "Force Match"});
            this.match_icbs_filter.Location = new System.Drawing.Point(454, 24);
            this.match_icbs_filter.Name = "match_icbs_filter";
            this.match_icbs_filter.Size = new System.Drawing.Size(160, 21);
            this.match_icbs_filter.TabIndex = 27;
            this.match_icbs_filter.SelectedIndexChanged += new System.EventHandler(this.match_icbs_filter_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Matched_Records);
            this.groupBox1.Location = new System.Drawing.Point(33, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(599, 474);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Match Records";
            // 
            // Matched_Records
            // 
            this.Matched_Records.CheckBoxes = true;
            this.Matched_Records.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.check,
            this.Date,
            this.Account_Name,
            this.Account_Number,
            this.Amount,
            this.columnHeader6});
            this.Matched_Records.FullRowSelect = true;
            this.Matched_Records.GridLines = true;
            this.Matched_Records.Location = new System.Drawing.Point(19, 19);
            this.Matched_Records.Name = "Matched_Records";
            this.Matched_Records.Size = new System.Drawing.Size(562, 429);
            this.Matched_Records.TabIndex = 6;
            this.Matched_Records.UseCompatibleStateImageBehavior = false;
            this.Matched_Records.View = System.Windows.Forms.View.Details;
            this.Matched_Records.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.Matched_Records_ItemChecked);
            this.Matched_Records.SelectedIndexChanged += new System.EventHandler(this.Matched_Records_SelectedIndexChanged);
            this.Matched_Records.DoubleClick += new System.EventHandler(this.Matched_Records_DoubleClick);
            // 
            // check
            // 
            this.check.Text = "Check";
            this.check.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.check.Width = 45;
            // 
            // Date
            // 
            this.Date.Text = "Date";
            this.Date.Width = 120;
            // 
            // Account_Name
            // 
            this.Account_Name.Text = "Account Name";
            this.Account_Name.Width = 100;
            // 
            // Account_Number
            // 
            this.Account_Number.Text = "Account Number";
            this.Account_Number.Width = 100;
            // 
            // Amount
            // 
            this.Amount.Text = "Amount";
            this.Amount.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Match Code";
            this.columnHeader6.Width = 70;
            // 
            // lbl_icbs_total_amount
            // 
            this.lbl_icbs_total_amount.AutoSize = true;
            this.lbl_icbs_total_amount.Location = new System.Drawing.Point(482, 540);
            this.lbl_icbs_total_amount.Name = "lbl_icbs_total_amount";
            this.lbl_icbs_total_amount.Size = new System.Drawing.Size(13, 13);
            this.lbl_icbs_total_amount.TabIndex = 32;
            this.lbl_icbs_total_amount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 540);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Total Amount";
            // 
            // lbl_icbs_value_items
            // 
            this.lbl_icbs_value_items.AutoSize = true;
            this.lbl_icbs_value_items.Location = new System.Drawing.Point(122, 540);
            this.lbl_icbs_value_items.Name = "lbl_icbs_value_items";
            this.lbl_icbs_value_items.Size = new System.Drawing.Size(13, 13);
            this.lbl_icbs_value_items.TabIndex = 30;
            this.lbl_icbs_value_items.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 540);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Total Items";
            // 
            // btn_undo_fm
            // 
            this.btn_undo_fm.Location = new System.Drawing.Point(33, 574);
            this.btn_undo_fm.Name = "btn_undo_fm";
            this.btn_undo_fm.Size = new System.Drawing.Size(132, 23);
            this.btn_undo_fm.TabIndex = 33;
            this.btn_undo_fm.Text = "Undo Force Match";
            this.btn_undo_fm.UseVisualStyleBackColor = true;
            this.btn_undo_fm.Click += new System.EventHandler(this.btn_undo_fm_Click);
            // 
            // Match_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 609);
            this.Controls.Add(this.btn_undo_fm);
            this.Controls.Add(this.lbl_icbs_total_amount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_icbs_value_items);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.match_icbs_filter);
            this.Controls.Add(this.groupBox1);
            this.Name = "Match_View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Match_View";
            this.Load += new System.EventHandler(this.Match_View_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox match_icbs_filter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView Matched_Records;
        private System.Windows.Forms.ColumnHeader check;
        private System.Windows.Forms.ColumnHeader Date;
        private System.Windows.Forms.ColumnHeader Account_Name;
        private System.Windows.Forms.ColumnHeader Account_Number;
        private System.Windows.Forms.ColumnHeader Amount;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label lbl_icbs_total_amount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_icbs_value_items;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_undo_fm;
    }
}