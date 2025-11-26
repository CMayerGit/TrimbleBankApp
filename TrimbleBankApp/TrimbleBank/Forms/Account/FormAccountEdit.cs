using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrimbleBank.UserControls.Customer;

namespace TrimbleBank.Forms.Account
{
    public partial class FormAccountEdit : Form
    {
        private DataRow _accountRow = null;

        public  DataRow AccountRow => _accountRow;


        public FormAccountEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Button click "OK"
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            _accountRow = ucAccountEdit.AccountRow;

            // --- check if customer was selected ---
            if (_accountRow["CustomerId"] == System.DBNull.Value) 
            {
                MessageBox.Show("Please select a customer for the new account.", "Missing customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
