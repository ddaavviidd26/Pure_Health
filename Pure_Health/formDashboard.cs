using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pure_Health
{
    public partial class formDashboard : Form
    {
        public formDashboard()
        {
            InitializeComponent();
            Anchor = AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left;
        }

        private void formDashboard_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
