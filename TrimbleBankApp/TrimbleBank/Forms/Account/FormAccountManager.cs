using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrimbleBank.Forms.Account
{
    public partial class FormAccountManager : Form
    {
        public FormAccountManager()
        {
            InitializeComponent();
        }

        private void FormAccountManager_Load(object sender, EventArgs e)
        {
            ucAccountManager.Init();
        }
    }
}
