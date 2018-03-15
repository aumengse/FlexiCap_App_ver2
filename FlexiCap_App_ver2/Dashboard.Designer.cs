namespace FlexiCap_App_ver2
{
    partial class Dashboard
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lv_scanned = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbl_scn_depo_ct = new System.Windows.Forms.Label();
            this.lbl_scn_wdl_ct = new System.Windows.Forms.Label();
            this.lbl_scn_total_ct = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_icbs_total_ct = new System.Windows.Forms.Label();
            this.lbl_icbs_wdl_ct = new System.Windows.Forms.Label();
            this.lbl_icbs_depo_ct = new System.Windows.Forms.Label();
            this.lv_icbs = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbl_var_total_ct = new System.Windows.Forms.Label();
            this.lbl_var_wdl_ct = new System.Windows.Forms.Label();
            this.lbl_var_depo_ct = new System.Windows.Forms.Label();
            this.lv_variance = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(37, 256);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "WDL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "DEPO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Trans Code";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(96, 384);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(42, 13);
            this.label27.TabIndex = 13;
            this.label27.Text = "TOTAL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 31);
            this.label1.TabIndex = 16;
            this.label1.Text = "TVVS Dashboard";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_scn_total_ct);
            this.groupBox1.Controls.Add(this.lbl_scn_wdl_ct);
            this.groupBox1.Controls.Add(this.lbl_scn_depo_ct);
            this.groupBox1.Controls.Add(this.lv_scanned);
            this.groupBox1.Location = new System.Drawing.Point(196, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 537);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scanned";
            // 
            // lv_scanned
            // 
            this.lv_scanned.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader7});
            this.lv_scanned.Location = new System.Drawing.Point(22, 70);
            this.lv_scanned.Name = "lv_scanned";
            this.lv_scanned.Size = new System.Drawing.Size(286, 337);
            this.lv_scanned.TabIndex = 18;
            this.lv_scanned.UseCompatibleStateImageBehavior = false;
            this.lv_scanned.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Items";
            this.columnHeader1.Width = 130;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Amount";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader7.Width = 150;
            // 
            // lbl_scn_depo_ct
            // 
            this.lbl_scn_depo_ct.AutoSize = true;
            this.lbl_scn_depo_ct.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_scn_depo_ct.Location = new System.Drawing.Point(67, 134);
            this.lbl_scn_depo_ct.Name = "lbl_scn_depo_ct";
            this.lbl_scn_depo_ct.Size = new System.Drawing.Size(13, 13);
            this.lbl_scn_depo_ct.TabIndex = 19;
            this.lbl_scn_depo_ct.Text = "0";
            // 
            // lbl_scn_wdl_ct
            // 
            this.lbl_scn_wdl_ct.AutoSize = true;
            this.lbl_scn_wdl_ct.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_scn_wdl_ct.Location = new System.Drawing.Point(67, 174);
            this.lbl_scn_wdl_ct.Name = "lbl_scn_wdl_ct";
            this.lbl_scn_wdl_ct.Size = new System.Drawing.Size(13, 13);
            this.lbl_scn_wdl_ct.TabIndex = 20;
            this.lbl_scn_wdl_ct.Text = "0";
            // 
            // lbl_scn_total_ct
            // 
            this.lbl_scn_total_ct.AutoSize = true;
            this.lbl_scn_total_ct.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_scn_total_ct.Location = new System.Drawing.Point(67, 302);
            this.lbl_scn_total_ct.Name = "lbl_scn_total_ct";
            this.lbl_scn_total_ct.Size = new System.Drawing.Size(13, 13);
            this.lbl_scn_total_ct.TabIndex = 21;
            this.lbl_scn_total_ct.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_icbs_total_ct);
            this.groupBox2.Controls.Add(this.lbl_icbs_wdl_ct);
            this.groupBox2.Controls.Add(this.lbl_icbs_depo_ct);
            this.groupBox2.Controls.Add(this.lv_icbs);
            this.groupBox2.Location = new System.Drawing.Point(537, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(331, 537);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ICBS";
            // 
            // lbl_icbs_total_ct
            // 
            this.lbl_icbs_total_ct.AutoSize = true;
            this.lbl_icbs_total_ct.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_icbs_total_ct.Location = new System.Drawing.Point(67, 302);
            this.lbl_icbs_total_ct.Name = "lbl_icbs_total_ct";
            this.lbl_icbs_total_ct.Size = new System.Drawing.Size(13, 13);
            this.lbl_icbs_total_ct.TabIndex = 21;
            this.lbl_icbs_total_ct.Text = "0";
            // 
            // lbl_icbs_wdl_ct
            // 
            this.lbl_icbs_wdl_ct.AutoSize = true;
            this.lbl_icbs_wdl_ct.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_icbs_wdl_ct.Location = new System.Drawing.Point(67, 174);
            this.lbl_icbs_wdl_ct.Name = "lbl_icbs_wdl_ct";
            this.lbl_icbs_wdl_ct.Size = new System.Drawing.Size(13, 13);
            this.lbl_icbs_wdl_ct.TabIndex = 20;
            this.lbl_icbs_wdl_ct.Text = "0";
            // 
            // lbl_icbs_depo_ct
            // 
            this.lbl_icbs_depo_ct.AutoSize = true;
            this.lbl_icbs_depo_ct.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_icbs_depo_ct.Location = new System.Drawing.Point(67, 134);
            this.lbl_icbs_depo_ct.Name = "lbl_icbs_depo_ct";
            this.lbl_icbs_depo_ct.Size = new System.Drawing.Size(13, 13);
            this.lbl_icbs_depo_ct.TabIndex = 19;
            this.lbl_icbs_depo_ct.Text = "0";
            // 
            // lv_icbs
            // 
            this.lv_icbs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader8});
            this.lv_icbs.Location = new System.Drawing.Point(20, 70);
            this.lv_icbs.Name = "lv_icbs";
            this.lv_icbs.Size = new System.Drawing.Size(286, 337);
            this.lv_icbs.TabIndex = 18;
            this.lv_icbs.UseCompatibleStateImageBehavior = false;
            this.lv_icbs.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Items";
            this.columnHeader2.Width = 130;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Amount";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader8.Width = 150;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbl_var_total_ct);
            this.groupBox3.Controls.Add(this.lbl_var_wdl_ct);
            this.groupBox3.Controls.Add(this.lbl_var_depo_ct);
            this.groupBox3.Controls.Add(this.lv_variance);
            this.groupBox3.Location = new System.Drawing.Point(882, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(331, 537);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ICBS";
            // 
            // lbl_var_total_ct
            // 
            this.lbl_var_total_ct.AutoSize = true;
            this.lbl_var_total_ct.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_var_total_ct.Location = new System.Drawing.Point(67, 302);
            this.lbl_var_total_ct.Name = "lbl_var_total_ct";
            this.lbl_var_total_ct.Size = new System.Drawing.Size(13, 13);
            this.lbl_var_total_ct.TabIndex = 21;
            this.lbl_var_total_ct.Text = "0";
            // 
            // lbl_var_wdl_ct
            // 
            this.lbl_var_wdl_ct.AutoSize = true;
            this.lbl_var_wdl_ct.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_var_wdl_ct.Location = new System.Drawing.Point(67, 174);
            this.lbl_var_wdl_ct.Name = "lbl_var_wdl_ct";
            this.lbl_var_wdl_ct.Size = new System.Drawing.Size(13, 13);
            this.lbl_var_wdl_ct.TabIndex = 20;
            this.lbl_var_wdl_ct.Text = "0";
            // 
            // lbl_var_depo_ct
            // 
            this.lbl_var_depo_ct.AutoSize = true;
            this.lbl_var_depo_ct.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_var_depo_ct.Location = new System.Drawing.Point(67, 134);
            this.lbl_var_depo_ct.Name = "lbl_var_depo_ct";
            this.lbl_var_depo_ct.Size = new System.Drawing.Size(13, 13);
            this.lbl_var_depo_ct.TabIndex = 19;
            this.lbl_var_depo_ct.Text = "0";
            // 
            // lv_variance
            // 
            this.lv_variance.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lv_variance.Location = new System.Drawing.Point(20, 70);
            this.lv_variance.Name = "lv_variance";
            this.lv_variance.Size = new System.Drawing.Size(286, 337);
            this.lv_variance.TabIndex = 18;
            this.lv_variance.UseCompatibleStateImageBehavior = false;
            this.lv_variance.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Items";
            this.columnHeader3.Width = 130;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Amount";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 150;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Description";
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1244, 661);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lv_scanned;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label lbl_scn_total_ct;
        private System.Windows.Forms.Label lbl_scn_wdl_ct;
        private System.Windows.Forms.Label lbl_scn_depo_ct;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_icbs_total_ct;
        private System.Windows.Forms.Label lbl_icbs_wdl_ct;
        private System.Windows.Forms.Label lbl_icbs_depo_ct;
        private System.Windows.Forms.ListView lv_icbs;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbl_var_total_ct;
        private System.Windows.Forms.Label lbl_var_wdl_ct;
        private System.Windows.Forms.Label lbl_var_depo_ct;
        private System.Windows.Forms.ListView lv_variance;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label3;
    }
}