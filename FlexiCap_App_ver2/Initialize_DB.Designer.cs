namespace FlexiCap_App_ver2
{
    partial class Initialize_DB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Initialize_DB));
            this.progress_Bar = new FlexiCap_App_ver2.Progress_Bar();
            this.SuspendLayout();
            // 
            // progress_Bar
            // 
            this.progress_Bar.Location = new System.Drawing.Point(26, 36);
            this.progress_Bar.Name = "progress_Bar";
            this.progress_Bar.Size = new System.Drawing.Size(311, 23);
            this.progress_Bar.TabIndex = 0;
            // 
            // Initialize_DB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 84);
            this.Controls.Add(this.progress_Bar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Initialize_DB";
            this.Text = "Initializing Database";
            this.Load += new System.EventHandler(this.Initialize_DB_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Progress_Bar progress_Bar;
    }
}