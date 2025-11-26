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

namespace TrimbleBank.UserControls
{
    /// <summary>
    /// Empty user control holding an instance of the database
    /// </summary>
    public partial class UcDataBase : UserControl
    {
        public static TrimbleBankDb DataBase = null;

        public UcDataBase()
        {
            InitializeComponent();
        }
    }
}
