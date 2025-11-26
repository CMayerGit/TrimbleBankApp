namespace TrimbleBank
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.scMainVertical = new System.Windows.Forms.SplitContainer();
            this.ucAccountList = new TrimbleBank.UserControls.Customer.UcAccountList();
            this.scMainHorizontal = new System.Windows.Forms.SplitContainer();
            this.ucAccountTransaction = new TrimbleBank.UserControls.Account.UcAccountTransaction();
            this.ucTransactionList = new TrimbleBank.UserControls.Transaction.UcTransactionList();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.miFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miMasterData = new System.Windows.Forms.ToolStripMenuItem();
            this.miMasterDataCustomers = new System.Windows.Forms.ToolStripMenuItem();
            this.miMasterDataAccounts = new System.Windows.Forms.ToolStripMenuItem();
            this.miInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.miInfoAbout = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.scMainVertical)).BeginInit();
            this.scMainVertical.Panel1.SuspendLayout();
            this.scMainVertical.Panel2.SuspendLayout();
            this.scMainVertical.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMainHorizontal)).BeginInit();
            this.scMainHorizontal.Panel1.SuspendLayout();
            this.scMainHorizontal.Panel2.SuspendLayout();
            this.scMainHorizontal.SuspendLayout();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMainVertical
            // 
            this.scMainVertical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMainVertical.Location = new System.Drawing.Point(0, 24);
            this.scMainVertical.Name = "scMainVertical";
            // 
            // scMainVertical.Panel1
            // 
            this.scMainVertical.Panel1.Controls.Add(this.ucAccountList);
            // 
            // scMainVertical.Panel2
            // 
            this.scMainVertical.Panel2.Controls.Add(this.scMainHorizontal);
            this.scMainVertical.Size = new System.Drawing.Size(848, 475);
            this.scMainVertical.SplitterDistance = 257;
            this.scMainVertical.TabIndex = 0;
            // 
            // ucAccountList
            // 
            this.ucAccountList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucAccountList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAccountList.Location = new System.Drawing.Point(0, 0);
            this.ucAccountList.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ucAccountList.Name = "ucAccountList";
            this.ucAccountList.Size = new System.Drawing.Size(257, 475);
            this.ucAccountList.TabIndex = 0;
            // 
            // scMainHorizontal
            // 
            this.scMainHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMainHorizontal.Location = new System.Drawing.Point(0, 0);
            this.scMainHorizontal.Name = "scMainHorizontal";
            this.scMainHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMainHorizontal.Panel1
            // 
            this.scMainHorizontal.Panel1.Controls.Add(this.ucAccountTransaction);
            // 
            // scMainHorizontal.Panel2
            // 
            this.scMainHorizontal.Panel2.Controls.Add(this.ucTransactionList);
            this.scMainHorizontal.Size = new System.Drawing.Size(587, 475);
            this.scMainHorizontal.SplitterDistance = 148;
            this.scMainHorizontal.TabIndex = 0;
            // 
            // ucAccountTransaction
            // 
            this.ucAccountTransaction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucAccountTransaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAccountTransaction.Location = new System.Drawing.Point(0, 0);
            this.ucAccountTransaction.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.ucAccountTransaction.Name = "ucAccountTransaction";
            this.ucAccountTransaction.Size = new System.Drawing.Size(587, 148);
            this.ucAccountTransaction.TabIndex = 0;
            // 
            // ucTransactionList
            // 
            this.ucTransactionList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ucTransactionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTransactionList.Location = new System.Drawing.Point(0, 0);
            this.ucTransactionList.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.ucTransactionList.Name = "ucTransactionList";
            this.ucTransactionList.Size = new System.Drawing.Size(587, 323);
            this.ucTransactionList.TabIndex = 0;
            // 
            // msMain
            // 
            this.msMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miMasterData,
            this.miInfo});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.msMain.Size = new System.Drawing.Size(848, 24);
            this.msMain.TabIndex = 1;
            this.msMain.Text = "menuStrip1";
            // 
            // miFile
            // 
            this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFileExit});
            this.miFile.Name = "miFile";
            this.miFile.Size = new System.Drawing.Size(37, 22);
            this.miFile.Text = "File";
            // 
            // miFileExit
            // 
            this.miFileExit.Name = "miFileExit";
            this.miFileExit.Size = new System.Drawing.Size(93, 22);
            this.miFileExit.Text = "Exit";
            this.miFileExit.Click += new System.EventHandler(this.miFileExit_Click);
            // 
            // miMasterData
            // 
            this.miMasterData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMasterDataCustomers,
            this.miMasterDataAccounts});
            this.miMasterData.Name = "miMasterData";
            this.miMasterData.Size = new System.Drawing.Size(82, 22);
            this.miMasterData.Text = "Master Data";
            // 
            // miMasterDataCustomers
            // 
            this.miMasterDataCustomers.Name = "miMasterDataCustomers";
            this.miMasterDataCustomers.Size = new System.Drawing.Size(131, 22);
            this.miMasterDataCustomers.Text = "Customers";
            this.miMasterDataCustomers.Click += new System.EventHandler(this.miMasterDataCustomers_Click);
            // 
            // miMasterDataAccounts
            // 
            this.miMasterDataAccounts.Name = "miMasterDataAccounts";
            this.miMasterDataAccounts.Size = new System.Drawing.Size(131, 22);
            this.miMasterDataAccounts.Text = "Accounts";
            this.miMasterDataAccounts.Click += new System.EventHandler(this.miMasterDataAccounts_Click);
            // 
            // miInfo
            // 
            this.miInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miInfoAbout});
            this.miInfo.Name = "miInfo";
            this.miInfo.Size = new System.Drawing.Size(40, 22);
            this.miInfo.Text = "Info";
            // 
            // miInfoAbout
            // 
            this.miInfoAbout.Name = "miInfoAbout";
            this.miInfoAbout.Size = new System.Drawing.Size(192, 22);
            this.miInfoAbout.Text = "About Trimble DB App";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 499);
            this.Controls.Add(this.scMainVertical);
            this.Controls.Add(this.msMain);
            this.MainMenuStrip = this.msMain;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormMain";
            this.Text = "Trimble Bank App";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.scMainVertical.Panel1.ResumeLayout(false);
            this.scMainVertical.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMainVertical)).EndInit();
            this.scMainVertical.ResumeLayout(false);
            this.scMainHorizontal.Panel1.ResumeLayout(false);
            this.scMainHorizontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMainHorizontal)).EndInit();
            this.scMainHorizontal.ResumeLayout(false);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMainVertical;
        private System.Windows.Forms.SplitContainer scMainHorizontal;
        private UserControls.Customer.UcAccountList ucAccountList;
        private UserControls.Account.UcAccountTransaction ucAccountTransaction;
        private UserControls.Transaction.UcTransactionList ucTransactionList;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem miFile;
        private System.Windows.Forms.ToolStripMenuItem miFileExit;
        private System.Windows.Forms.ToolStripMenuItem miMasterData;
        private System.Windows.Forms.ToolStripMenuItem miMasterDataCustomers;
        private System.Windows.Forms.ToolStripMenuItem miMasterDataAccounts;
        private System.Windows.Forms.ToolStripMenuItem miInfo;
        private System.Windows.Forms.ToolStripMenuItem miInfoAbout;
    }
}

