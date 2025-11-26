using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrimbleBank.Classes;
using TrimbleBank.Forms.Customer;

namespace TrimbleBank.UserControls.Account
{
    public partial class UcAccountEdit : UcDataBase
    {
        private DataRow _valueRow    = null;
        private DataRow _customerRow = null;

        /// <summary>
        /// UserControl <=> DataRow
        /// </summary>
        public DataRow AccountRow
        {
            get => GetValue();
            set => SetValue(value);
        }   
        

        public UcAccountEdit()
        {
            InitializeComponent();

            ucCustomerEdit.SetReadOnly(true);

            txtAccountNumber.Text = Guid.NewGuid().ToString();
            txtBalance.Text       = "0,00";
        }

        /// <summary>
        /// Set values from DataRow to user control
        /// </summary>
        /// <param name="accountRow">DataRow to set user control values</param>
        private void SetValue(DataRow accountRow)
        {
            // --- store _valueRow and write row values to user control ---
            _valueRow = accountRow;
            if (_valueRow != null)
            {
                txtAccountNumber.Text = Convert.ToString(_valueRow["Number"]);
                txtBalance.Text       = Convert.ToDouble(_valueRow["Balance"]).ToString("0.##");
            }
        }


        /// <summary>
        /// Get values from user control as DataRow
        /// </summary>
        /// <returns>DataRow containing user control values</returns>
        private DataRow GetValue()
        {
            // --- create DataRow value if not already exists ----
            if (_valueRow == null)
            {
                if (DataBase == null)
                {                
                    return null;
                }
                _valueRow = DataBase.CreateAccountRow();
            }

            // --- write user control values to _valueRow ---
            double balance            = Convert.ToDouble(txtBalance.Text);
            _valueRow["Balance"]      = balance;
            _valueRow["CustomerId"]   = _customerRow != null ? _customerRow["Id"] : System.DBNull.Value;
            _valueRow["Number"]       = txtAccountNumber.Text;

            // --- return _valueRow with user controlm content ---
            return _valueRow;
        }

        /// <summary>
        /// Button click "Select customer"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectCustomer_Click(object sender, EventArgs e)
        {
            FormSelectCustomer formSelectCustomer = new FormSelectCustomer();
            if (formSelectCustomer.ShowDialog() == DialogResult.OK)
            {
                // --- get customer DataRow from form ---
                _customerRow               = formSelectCustomer.SelectedCustomerRow;

                // --- display selected customer ---
                ucCustomerEdit.CustomerRow = _customerRow;
            }
        }
    }
}
