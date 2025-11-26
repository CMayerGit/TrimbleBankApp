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

namespace TrimbleBank.Forms
{
    /// <summary>
    /// Empty form holding an instance of the database
    /// </summary>
    public partial class FormDataBase : Form
    {
        public static TrimbleBankDb DataBase = null;

        public FormDataBase()
        {
            InitializeComponent();
        }
    }
}
