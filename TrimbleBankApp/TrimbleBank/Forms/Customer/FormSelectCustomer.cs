using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrimbleBank.Forms.Customer
{
    public partial class FormSelectCustomer : Form
    {
        public DataRow _selectedCustomerRow = null;

        public DataRow SelectedCustomerRow
        {
            get => _selectedCustomerRow;
        }

        public FormSelectCustomer()
        {
            InitializeComponent();

            ucCustomerList.Init();
        }

        /// <summary>
        /// Button click "OK"
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ucCustomerList.SelectedCustomerRow == null)
            {
                MessageBox.Show("Please select or double click customer from list", "No customer selected", MessageBoxButtons.OK, MessageBoxIcon.Error);    
                return;
            }

            _selectedCustomerRow = ucCustomerList.SelectedCustomerRow;
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
