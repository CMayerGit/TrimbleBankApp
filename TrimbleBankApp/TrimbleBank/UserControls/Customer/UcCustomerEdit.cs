using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace TrimbleBank.UserControls.Customer
{
    public partial class UcCustomerEdit : UcDataBase
    {
        private DataRow _valueRow = null;

        public DataRow CustomerRow
        {
            get => GetValue();
            set => SetValue(value);
        }   
        public UcCustomerEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set readOnly state of all text boxes
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetReadOnly(bool readOnly)
        {
            SetTextboxReadOnly(txtFirstName  , readOnly);
            SetTextboxReadOnly(txtLastName   , readOnly);
            SetTextboxReadOnly(txtStreet     , readOnly);
            SetTextboxReadOnly(txtHouseNumber, readOnly);
            SetTextboxReadOnly(txtZipCode    , readOnly);
            SetTextboxReadOnly(txtCity       , readOnly);
            SetTextboxReadOnly(txtPhone      , readOnly);
            SetTextboxReadOnly(txtEmail      , readOnly);
        }

        /// <summary>
        /// Set textbox readOnly state and change color
        /// </summary>
        /// <param name="tb">TextBix to change readOnly state</param>
        /// <param name="readOnly">readOnly flag to set</param>
        private void SetTextboxReadOnly(TextBox tb, bool readOnly)
        {
            tb.ReadOnly  = readOnly;
            tb.BackColor = readOnly ? SystemColors.Info : SystemColors.Window;
        }

        /// <summary>
        /// Set values from DataRow to user control
        /// </summary>
        /// <param name="customerRow">DataRow to set user control values</param>
        private void SetValue(DataRow customerRow)
        {
            // --- store _valueRow and write row values to form ---
            _valueRow = customerRow;
            if (_valueRow != null ) 
            {
                txtFirstName.Text   = Convert.ToString(_valueRow["FirstName"]);
                txtLastName.Text    = Convert.ToString(_valueRow["LastName"]);
                txtStreet.Text      = Convert.ToString(_valueRow["Street"]);
                txtHouseNumber.Text = Convert.ToString(_valueRow["HouseNumber"]);
                txtZipCode.Text     = Convert.ToString(_valueRow["ZipCode"]);      
                txtCity.Text        = Convert.ToString(_valueRow["City"]);
                txtPhone.Text       = Convert.ToString(_valueRow["PhoneNumber"]);
                txtEmail.Text       = Convert.ToString(_valueRow["EmailAddress"]);
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
                _valueRow = DataBase.CreateCustomerRow();
            }

            // --- write form values to _valueRow ---
            _valueRow["FirstName"]    = txtFirstName.Text;
            _valueRow["LastName"]     = txtLastName.Text;
            _valueRow["Street"]       = txtStreet.Text;
            _valueRow["HouseNumber"]  = txtHouseNumber.Text;
            _valueRow["ZipCode"]      = txtZipCode.Text;      
            _valueRow["City"]         = txtCity.Text;
            _valueRow["PhoneNumber"]  = txtPhone.Text;
            _valueRow["EmailAddress"] = txtEmail.Text;

            // --- return _valueRow with form content ---
            return _valueRow;
        }


    }
}
