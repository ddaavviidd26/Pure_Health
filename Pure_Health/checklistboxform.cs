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
        { "CBC + Platelet Count", 260.00m },
        { "Blood Typing with RH Typing", 260.00m },
        { "ESR", 260.00m },
        { "CT BT", 150.00m },
        { "FBS",  155.00m },
        { "BUN", 155.00m },
        { "Creatinine",  155.00m },
        { "Blood Uric Acid",  155.00m },
        { "Cholesterol",  255.00m },
        { "Triglycerides", 255.00m },
        { "HDL", 255.00m },
        { "LDL", 255.00m },
        { "VLDL", 255.00m },
        { "SGOT", 255.00m },
        { "SGPT", 255.00m },
        { "Protime", 460.00m },
        { "PTT-Retic Count", 450.00m },
        { "Malarial Smear", 400.00m },
        { "PBS (blood smear)", 505.00m },
        { "VDRL/RPR", 255.00m },
        { "Widal Test", 375.00m },
        { "Sodium", 255.00m },
        { "Potassium", 255.00m },
        { "Urinalysis", 85.00m },
        { "Fecalysis", 80.00m },
        { "Phosphorus", 310.00m },
        { "Magnesium", 350.00m },
        { "Acid Phos.", 350.00m },
        { "Alka Phos.", 350.00m },
        { "CPK-MB", 1100.00m },
        { "CPK-MM", 1100.00m },
        { "Troponin I/T", 1250.00m },
        { "HEPA B PROFILE", 2050.00m },
        { "HEPA ABC PROFILE", 3100.00m },


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
            var item = checkedListBox1.Items[e.Index].ToString();

            if (itemPrices.ContainsKey(item))
            {
                var price = itemPrices[item];

                // Adjust TotalPrice based on the new state of the item
                if (e.NewValue == CheckState.Checked && !SelectedItems.Contains(item))
                {
                    TotalPrice += price;
                    SelectedItems.Add(item);
                }
                else if (e.NewValue == CheckState.Unchecked)
                {
                    TotalPrice -= price;
                    SelectedItems.Remove(item);
                }
            }
        }


        private void checklistboxform_Load(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Pass the selected items and total price to the other form
            formPatient formPatient = (formPatient)Owner; // Assuming the calling form is FormPatient
            formPatient.UpdateSummary(SelectedItems, (int)TotalPrice);

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
