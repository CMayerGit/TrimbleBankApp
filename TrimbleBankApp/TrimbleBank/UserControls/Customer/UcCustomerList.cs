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
    public partial class UcCustomerList : UcDataBase
    {
        private DataTable _dtCustomers    = null;

        /// <summary>
        /// Returns the select DataGridView row as DataRow 
        /// </summary>
        public DataRow SelectedCustomerRow
        {
            get
            {
                DataRow selectedRow = null;
                if (dgvCustomers.SelectedRows.Count > 0)
                {
                    selectedRow = ((DataRowView)dgvCustomers.SelectedRows[0].DataBoundItem).Row;
                }
                return selectedRow ;
            }
        }


        public UcCustomerList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Read customers from database and display it
        /// </summary>
        public void Init()
        {
            if (DataBase != null)
            {
                _dtCustomers                       = DataBase.GetCustomers();
                dgvCustomers.DataSource            = _dtCustomers;
                dgvCustomers.Columns["Id"].Visible = false;
            }
        }


    }
}
