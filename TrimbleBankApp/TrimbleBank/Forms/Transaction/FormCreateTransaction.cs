using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrimbleBank.Forms.Transaction
{
    public partial class FormCreateTransaction : FormDataBase
    {
        private DataRow _transactionRow = null;
        private int     _accountId      = -1;

        public DataRow Transaction
        { 
            get => _transactionRow;
        }

        public FormCreateTransaction(int accountId)
        {
            InitializeComponent();

            _accountId = accountId;

            Text = "Create new transaction";
        }

        /// <summary>
        /// Button click "OK"
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            _transactionRow = ucTransactionEdit.TransactionRow;
            _transactionRow["AccountId"] = _accountId;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Button click "Cancel"
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
