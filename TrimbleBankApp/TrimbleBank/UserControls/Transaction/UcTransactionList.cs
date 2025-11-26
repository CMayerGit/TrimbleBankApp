using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrimbleBank.Data;
using TrimbleBank.Forms.Customer;

namespace TrimbleBank.UserControls.Transaction
{
    public partial class UcTransactionList : UcDataBase
    {
        private DataRow[] _transactionRows = null;

        public UcTransactionList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Delete selected transactions from database
        /// </summary>
        /// <param name="accountId">database id of account the transactions belong to</param>
        public void DeleteSelectedTransactions(int accountId)
        {
            DataRow selTransactionRow = null;
            int     selTransactionId  = 0;
            if (dgvTransactions.SelectedRows.Count > 0)
            {
                // --- delete selected transactions from database ---
                try
                {
                    for(int iRow = 0; iRow < dgvTransactions.SelectedRows.Count; iRow++)
                    {
                        selTransactionRow = ((DataRowView)dgvTransactions.SelectedRows[iRow].DataBoundItem).Row;
                        selTransactionId  = Convert.ToInt32(selTransactionRow["Id"]); 

                        DataBase.DeleteTransaction(selTransactionId);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"error delete transaction: {ex.Message}", "error deleting transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // --- update transaction grid ---
                SetTransactions(accountId);
            }            
            else
            {
                // --- no transaction selected -----
                MessageBox.Show("No transaction for delete was seleceted", "no selected transaction", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        /// <summary>
        /// Display transaction of certain account
        /// </summary>
        /// <param name="accountId"></param>
        public void SetTransactions(int accountId)
        {
            _transactionRows = DataBase.GetAccountTransactions(accountId);

            if (_transactionRows.Length > 0)
            {
                // --- REMARK: DataSource contains only a copy of the DataRows !! ---
                dgvTransactions.DataSource                   = null;
                dgvTransactions.DataSource                   = _transactionRows.CopyToDataTable();
                dgvTransactions.Columns["Id"].Visible        = false;
                dgvTransactions.Columns["AccountId"].Visible = false;
                dgvTransactions.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTransactions.Columns["Amount"].DefaultCellStyle.Format    = "F2";
            }   
            else
            {       
                dgvTransactions.DataSource = null;      
            }
        }

        /// <summary>
        /// replace transaction type by its name
        /// </summary>
        private void dgvTransactions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTransactions.Columns[e.ColumnIndex].Name.ToUpper() == "TYPE" && e.Value != null)
            {
                switch ((int)e.Value)
                {
                    case TransactionType.WITHDRAWAL:
                        e.Value = "Withdrawal";
                        break;
                    case TransactionType.DEPOSIT:
                        e.Value = "Deposit";
                        break;
                    case TransactionType.TRANSFER:
                        e.Value = "Transfer";
                        break;
                    case TransactionType.INCOMING:
                        e.Value = "Incoming";
                        break;
                    default:
                        e.Value = "!unknown!";
                        break;
                }
                e.FormattingApplied = true;
            }
        }
    }
}
