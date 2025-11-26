namespace TrimbleBank.Forms.Customer
{
    partial class FormCustomerManager
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
            this.ucCustomerManager = new TrimbleBank.UserControls.Customer.UcCustomerManager();
            this.SuspendLayout();
            // 
            // ucCustomerManager
            // 
            this.ucCustomerManager.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucCustomerManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCustomerManager.Location = new System.Drawing.Point(0, 0);
            this.ucCustomerManager.Margin = new System.Windows.Forms.Padding(2);
            this.ucCustomerManager.Name = "ucCustomerManager";
            this.ucCustomerManager.Size = new System.Drawing.Size(707, 335);
            this.ucCustomerManager.TabIndex = 0;
            // 
            // FormCustomerManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 335);
            this.Controls.Add(this.ucCustomerManager);
            this.Name = "FormCustomerManager";
            this.Text = "Customer Manager";
            this.Load += new System.EventHandler(this.FormCustomerManager_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.Customer.UcCustomerManager ucCustomerManager;
    }
}