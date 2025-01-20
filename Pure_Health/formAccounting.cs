using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pure_Health
{
    public partial class formAccounting : Form
    {
        public formAccounting()
        {
            InitializeComponent();
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
        }

        private void formAccounting_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;                 
          
        }
 
    }
    
}
