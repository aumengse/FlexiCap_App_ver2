namespace FlexiCap_App_ver2
{
    partial class Archiving_Trans
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archiving_Trans));
            this.progress_Bar = new FlexiCap_App_ver2.Progress_Bar();
            this.SuspendLayout();
            // 
            // progress_Bar
            // 
            this.progress_Bar.Location = new System.Drawing.Point(28, 43);
            this.progress_Bar.Name = "progress_Bar";
            this.progress_Bar.Size = new System.Drawing.Size(307, 23);
            this.progress_Bar.TabIndex = 3;
            // 
            // Archiving_Trans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 86);
            this.Controls.Add(this.progress_Bar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Archiving_Trans";
            this.Text = "Archiving Transactions";
            this.Load += new System.EventHandler(this.Archiving_Trans_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private Progress_Bar progress_Bar;
    }
}