using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrimbleBank.Classes;
using TrimbleBank.Data;
using TrimbleBank.Forms.Customer;
using TrimbleBank.Forms.Transaction;

namespace TrimbleBank.UserControls.Account
{
    public partial class UcAccountTransaction : UcDataBase
    {
        // --- fired when new transaction created ---
        public event EventHandler<int> OnAccountTransactionCreated;
        // --- fired when transaction was deleted ---
        public event EventHandler<int> OnDeleteTransactions;

        private DataRow       _accountRow    = null;
        private int           _accountId     = 0;

        public UcAccountTransaction()
        {
            InitializeComponent();
        }

        /// <summary>
        /// display account with transactions
        /// </summary>
        /// <param name="id">database of accouht to display</param>
        public void SetAccount(int id)
        {

            // --- write new customer to database ---
            try
            {
                _accountRow = DataBase.GetAccount(id);
                _accountId  = id;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"cannot load account: {ex.Message}", "error loaind account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            

            // --- display account ---
            if (_accountRow != null) 
            {
                AccountToForm();
            }

            // --- Verify the BalanceCheck signature ---
            if (!VerifyBalanceSignature())
            {
                MessageBox.Show("--- Security Alert ---\nAccount balance doesn't fit to it's signature, balance maybe was changed from outside the system!", "", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        /// <summary>
        /// Verify the BalanceCheck signature
        /// </summary>
        private bool VerifyBalanceSignature()
        {
            double balance          = Convert.ToDouble(_accountRow["Balance"]);
            string balanceSignature = Convert.ToString(_accountRow["BalanceCheck"]);
            //            
            return  SignatureManager.Verify(balance, _accountRow, balanceSignature); 
        }

        /// <summary>
        /// Write accountRow values to user control
        /// </summary>
        private void AccountToForm()
        {
            txtAccountNumber.Text = Convert.ToString(_accountRow["Number"]);
            txtBalance.Text       = Convert.ToDouble(_accountRow["Balance"]).ToString("F2");
            txtFirstName.Text     = Convert.ToString(_accountRow["FirstName"]); 
            txtLastName.Text      = Convert.ToString(_accountRow["LastName"]); 
        }

        /// <summary>
        /// Button click "New transaction"
        /// </summary>
        private void btnNewTransaction_Click(object sender, EventArgs e)
        {
            // --- show form for creating transaction ---
            FormCreateTransaction formCreateTransaction = new FormCreateTransaction(_accountId);
            if (formCreateTransaction.ShowDialog() == DialogResult.OK)
            {
                // --- add transaction (and changes to _accountRow) to database ---
                _accountRow = DataBase.AddTransaction(formCreateTransaction.Transaction, _accountRow);

                // --- update Balance textbox ---
                AccountToForm();

                // --- send AccountTransactionCreated event to ensure correct display of new transaction ---
                OnAccountTransactionCreated?.Invoke(this, _accountId);
            }
        }

        /// <summary>
        /// Button click "Delete Transaction"
        /// </summary>
        private void btnDeleteTransaction_Click(object sender, EventArgs e)
        {
            // --- this will be handled within ucTransactionList which ---
            // --- contains the selected transactions                  ---
            OnDeleteTransactions?.Invoke(this, _accountId);
        }

        /// <summary>
        /// Ensure txtBalance contains a valid double value string
        /// </summary>
        private void txtBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            char decimalSeparator = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            // --- check for number, decimal seperator and control ---
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != decimalSeparator)
            {
                // --- input not allowed ---
                e.Handled = true; 
            }

            // --- check for just one decimal seperator --- 
            if (e.KeyChar == decimalSeparator &&
                (sender as TextBox).Text.Contains(decimalSeparator.ToString()))
            {
                // --- input not allowed ---
                e.Handled = true;
            }
        }
    }
}
