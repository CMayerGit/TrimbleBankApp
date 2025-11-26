using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrimbleBank.UserControls.Transaction
{
    public partial class UcTransactionEdit : UcDataBase
    {
        private DataRow _valueRow = null;

        private string[] _taTypeNames = {"Withdrawal", "Deposit", "Transfer", "Incoming"};

        public DataRow TransactionRow
        {
            get => GetValue();
            set => SetValue(value);
        }   

        public UcTransactionEdit()
        {
            InitializeComponent();

            // --- combobox names for tansaction types ---
            cbType.DataSource = _taTypeNames;
        }

        /// <summary>
        /// Set values from DataRow to user control
        /// </summary>
        /// <param name="transactionRow">DataRow to set user control values</param>
        private void SetValue(DataRow transactionRow)
        {
            // --- store _valueRow and write row values to form ---
            _valueRow            = transactionRow;
            if (_valueRow != null)
            {
                txtAmount.Text       = Convert.ToDouble(_valueRow["Amount"]).ToString("F2");
                txtIBAN.Text         = Convert.ToString(_valueRow["IBAN"]);
                txtNumber.Text       = Convert.ToString(_valueRow["Number"]);
                txtPurpose.Text      = Convert.ToString(_valueRow["Purpose"]);
                dtpDate.Value        = Convert.ToDateTime(_valueRow["Date"]);
                cbType.SelectedIndex = Convert.ToInt32(_valueRow["Type"]);
            }
        }

        /// <summary>
        /// Get values from user control as DataRow
        /// </summary>
        /// <returns>DataRow containing user control values</returns>
        private DataRow GetValue()
        {
            // --- create value if not already exists ----
            if (_valueRow == null)
            {
                if (DataBase == null)
                {
                    return null;
                }
                _valueRow = DataBase.CreateTransactionRow();
            }

            // --- write form values to _valueRow ---
            _valueRow["Amount"]  = Convert.ToDouble(txtAmount.Text).ToString("F2");
            _valueRow["IBAN"]    = txtIBAN.Text;
            _valueRow["Number"]  = txtNumber.Text;
            _valueRow["Purpose"] = txtPurpose.Text;
            _valueRow["Date"]    = dtpDate.Value;
            _valueRow["Type"]    = cbType.SelectedIndex;

            // --- return _valueRow with form content ---
            return _valueRow;
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
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
