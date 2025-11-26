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

namespace TrimbleBank.UserControls.Customer
{
    public partial class UcAccountList : UcDataBase
    {
        public event EventHandler<int> OnAccountSelectionChanged;

        private DataTable     _dtAccounts    = null;


        public UcAccountList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Read accounts from database and display it
        /// </summary>
        public void Init()
        {
            if (DataBase != null)
            {
                _dtAccounts                                 = DataBase.GetAccounts();
                dgvAccounts.DataSource                      = _dtAccounts;
                dgvAccounts.Columns["Id"].Visible           = false;
                dgvAccounts.Columns["CustomerId"].Visible   = false;
                dgvAccounts.Columns["Balance"].Visible      = false;
                dgvAccounts.Columns["BalanceCheck"].Visible = false;
            }
        }


        /// <summary>
        /// Fires the OnAccountSelectionChanged event
        /// </summary>
        private void dgvAccounts_SelectionChanged(object sender, EventArgs e)
        {
            DataRow selAccountRow = null;
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                // --- get selected data row ---
                selAccountRow = ((DataRowView)dgvAccounts.SelectedRows[0].DataBoundItem).Row;
                // --- fire OnAccountSelectionChanged with appropriate account database id ---
                OnAccountSelectionChanged?.Invoke(this, Convert.ToInt32(selAccountRow["id"]));
            }

        }
    }
}
