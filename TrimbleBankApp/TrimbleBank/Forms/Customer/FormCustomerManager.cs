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
    public partial class FormCustomerManager : Form
    {
        public FormCustomerManager()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init user control holding the manager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCustomerManager_Load(object sender, EventArgs e)
        {
            ucCustomerManager.Init();
        }
    }
}
