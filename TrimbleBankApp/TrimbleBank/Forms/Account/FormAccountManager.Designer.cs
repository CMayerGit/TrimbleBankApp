namespace TrimbleBank.Forms.Account
{
    partial class FormAccountManager
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
            this.ucAccountManager = new TrimbleBank.UserControls.Account.UcAccountManager();
            this.SuspendLayout();
            // 
            // ucAccountManager
            // 
            this.ucAccountManager.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucAccountManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAccountManager.Location = new System.Drawing.Point(0, 0);
            this.ucAccountManager.Name = "ucAccountManager";
            this.ucAccountManager.Size = new System.Drawing.Size(678, 375);
            this.ucAccountManager.TabIndex = 0;
            // 
            // FormAccountManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 375);
            this.Controls.Add(this.ucAccountManager);
            this.Name = "FormAccountManager";
            this.Text = "Account Manager";
            this.Load += new System.EventHandler(this.FormAccountManager_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.Account.UcAccountManager ucAccountManager;
    }
}