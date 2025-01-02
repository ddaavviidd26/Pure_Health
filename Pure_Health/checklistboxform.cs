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
    public partial class checklistboxform : Form
    {
        public List<string> SelectedItems { get; private set; } = new List<string>();
        private Dictionary<string, decimal> itemPrices = new Dictionary<string, decimal>
    {
        { "Option 1", 260.00m },
        { "Option 2", 260.00m },
        { "Option 3", 260.00m },
        { "Option 4", 150.00m }
    };
        public decimal TotalPrice { get; private set; } = 0;
        public checklistboxform()
        {
            InitializeComponent();
            InitializeChecklistBox();
        }
        private void InitializeChecklistBox()
        {
            foreach (var item in itemPrices.Keys)
            {
                checkedListBox1.Items.Add(item);
            }

            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }


        private void checklistboxform_Load(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedItems = checkedListBox1.CheckedItems.Cast<string>().ToList();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
