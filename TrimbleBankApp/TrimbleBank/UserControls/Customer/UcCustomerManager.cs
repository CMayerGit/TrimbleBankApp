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

namespace TrimbleBank.UserControls.Customer
{
    public partial class UcCustomerManager : UcDataBase
    {
        private DataTable     _dtCustomers   = null;

        public UcCustomerManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Read customers from database and display it
        /// </summary>
        public void Init()
        {
            _dtCustomers = DataBase != null ? DataBase.GetCustomers() : null;
            if (_dtCustomers != null) 
            {
                dgvCustomers.DataSource            = _dtCustomers;
                dgvCustomers.Columns["Id"].Visible = false;
            }
        }

        /// <summary>
        /// Button click "Create Customer"
        /// </summary>
        private void btnCreateCustomer_Click(object sender, EventArgs e)
        {
            // --- show customer edit form to create new DataRow for customer ---
            FormCustomerEdit formCustomerEdit = new FormCustomerEdit();
            if (formCustomerEdit.ShowDialog() == DialogResult.OK)
            {
                // --- write new customer to database ---
                try
                {
                    DataBase.AddCustomer(formCustomerEdit.CustomerRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"cannot create customer: {ex.Message}", "error creating customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Button click "Edit Customer"
        /// </summary>
        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            // --- get select row as DataRow ---
            DataRow editCustomerRow = null;
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                editCustomerRow = ((DataRowView)dgvCustomers.SelectedRows[0].DataBoundItem).Row;
            }
            else
            {
                MessageBox.Show("No customer selected!", "Select customer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // --- show customer edit form ---
            FormCustomerEdit formCustomerEdit = new FormCustomerEdit(editCustomerRow);
            if (formCustomerEdit.ShowDialog() == DialogResult.OK)
            {
                // --- update changes to database ---
                try
                {
                    DataBase.UpdateCustomers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"cannot write changes to customer: {ex.Message}", "error modifying customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Button click "Delete Customer"
        /// </summary>
        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            // --- get select row as DataRow ---
            DataRow deleteCustomerRow = null;
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                deleteCustomerRow = ((DataRowView)dgvCustomers.SelectedRows[0].DataBoundItem).Row;
            }
            else
            {
                MessageBox.Show("No customer selected!", "Select customer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // --- ask and delete user from database ---
            if (MessageBox.Show($"Should the customer {deleteCustomerRow["FirstName"]}, {deleteCustomerRow["LastName"]} be deleted?", "Confirm delete customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DataBase.DeleteCustomer(deleteCustomerRow);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"cannot delete customer: {ex.Message}", "error deleting customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
