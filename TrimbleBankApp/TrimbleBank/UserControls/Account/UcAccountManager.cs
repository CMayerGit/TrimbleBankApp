using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrimbleBank.Forms.Account;
using TrimbleBank.Forms.Customer;

namespace TrimbleBank.UserControls.Account
{

    public partial class UcAccountManager : UcDataBase
    {
        private DataTable     _dtAccounts   = null;

        public UcAccountManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Read accounts from database and display it
        /// </summary>
        public void Init()
        {
            _dtAccounts   = DataBase.GetAccounts();
            if (_dtAccounts != null) 
            {
                dgvAccounts.DataSource            = _dtAccounts;
                dgvAccounts.Columns["Id"].Visible = false;
                dgvAccounts.Columns["CustomerId"].Visible = false;
            }
        }

        /// <summary>
        /// Button click "Create account"
        /// </summary>
        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            FormAccountEdit formAccountEdit = new FormAccountEdit();
            if (formAccountEdit.ShowDialog() == DialogResult.OK)
            {
                // --- write new account to database ---
                try
                {
                    DataBase.AddAccount(formAccountEdit.AccountRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"cannot create account: {ex.Message}", "error creating account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Button click "Delete account"
        /// </summary>
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            // --- get select row as DataRow ---
            DataRow deleteAccountRow = null;
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                deleteAccountRow = ((DataRowView)dgvAccounts.SelectedRows[0].DataBoundItem).Row;
            }
            else
            {
                MessageBox.Show("No account selected!", "Select account", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // --- ask and delete account from database ---
            if (MessageBox.Show($"Should the account [{deleteAccountRow["Number"]}] of customer {deleteAccountRow["FirstName"]}, {deleteAccountRow["LastName"]} be deleted?", "Confirm delete customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DataBase.DeleteAccount(deleteAccountRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"cannot delete account: {ex.Message}", "error deleting account", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
            }

        }
    }
}
