using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using TrimbleBank.Data;
using TrimbleBank.Forms;
using TrimbleBank.Forms.Account;
using TrimbleBank.Forms.Customer;
using TrimbleBank.UserControls;

namespace TrimbleBank
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            ucAccountList.OnAccountSelectionChanged          += OnAccountSelectionChanged;
            ucAccountTransaction.OnAccountTransactionCreated += OnAccountTransactionCreated;
            ucAccountTransaction.OnDeleteTransactions        += OnDeleteTransactions;
        }

        /// <summary>
        /// Init application by getting database instance and initialize GUI
        /// </summary>
        private void FormMain_Load(object sender, EventArgs e)
        {
            TrimbleBankDb trimbleBankDb  = new TrimbleBankDb(TrimbleBankDbType.SqlServer);
            trimbleBankDb.Load();

            UcDataBase.DataBase   = trimbleBankDb;
            FormDataBase.DataBase = trimbleBankDb;

            ucAccountList.Init();
        }

        #region Menu Events
        /// <summary>
        /// File => Exit
        /// </summary>
        private void miFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Master Data => Customers
        /// </summary>
        private void miMasterDataCustomers_Click(object sender, EventArgs e)
        {
            FormCustomerManager formCustomerManager = new FormCustomerManager();
            formCustomerManager.ShowDialog();
        }

        /// <summary>
        /// Master Data => Accounts
        /// </summary>
        private void miMasterDataAccounts_Click(object sender, EventArgs e)
        {
            FormAccountManager formAccountManager = new FormAccountManager();
            formAccountManager.ShowDialog();
        }
        #endregion

        #region UserControl Event
        /// <summary>
        /// Fired when "Delete Transaction" button within ucAccountTransaction was pressed
        /// </summary>
        /// <param name="sender">ucAccountTransaction</param>
        /// <param name="accountId">database id of account showing by ucAccountTransaction</param>
        private void OnDeleteTransactions(object sender, int accountId)
        {
            ucTransactionList.DeleteSelectedTransactions(accountId);
        }

        /// <summary>
        /// Fired when "Create Transaction" button within ucAccountTransaction was pressed
        /// </summary>
        /// <param name="sender">ucAccountTransaction</param>
        /// <param name="accountId">database id of account showing by ucAccountTransaction</param>
        private void OnAccountTransactionCreated(object sender, int accountId)
        {
            ucTransactionList.SetTransactions(accountId);            
        }

        /// <summary>
        /// Fired when the selection of the account grid within ucAccountList has changed
        /// </summary>
        /// <param name="sender">ucAccountList</param>
        private void OnAccountSelectionChanged(object sender, int accountId)
        {
            ucAccountTransaction.SetAccount(accountId);
            ucTransactionList.SetTransactions(accountId);            
        }
        #endregion
    }
}
