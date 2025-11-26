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
using static System.Net.Mime.MediaTypeNames;

namespace TrimbleBank.Forms.Customer
{
    public enum CustomerEditMode       
    {
        Create,
        Update
    }

    public partial class FormCustomerEdit : FormDataBase
    {
        private CustomerEditMode _editMode    = CustomerEditMode.Create;
        private DataTable        _dtCustomers = null;
        private DataRow          _customerRow = null;

        public  DataRow CustomerRow => _customerRow;


        public FormCustomerEdit()
        {
            InitializeComponent();

            _dtCustomers = DataBase.GetCustomers();

            Text = "Create new customer";
        }

        public FormCustomerEdit(DataRow customerRow)
        {
            InitializeComponent();

            _editMode    = CustomerEditMode.Update;
            _customerRow = customerRow;

            Text = "Edit customer";
        }

        private void FormCustomerEdit_Load(object sender, EventArgs e)
        {
            if (_editMode == CustomerEditMode.Update)
            {
                ucCustomerEdit.CustomerRow = _customerRow;
            }
        }

        /// <summary>
        /// Button click "OK"
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            _customerRow = ucCustomerEdit.CustomerRow;

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
