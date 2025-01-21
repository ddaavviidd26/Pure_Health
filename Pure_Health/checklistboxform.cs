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
        public class ItemDetails
        {
            public decimal Price { get; set; }
            public string Label { get; set; }

            public ItemDetails(decimal price, string label)
            {
                Price = price;
                Label = label;
            }
        }
        public List<string> SelectedItems { get; private set; } = new List<string>();
        private Dictionary<string, ItemDetails> itemDetails = new Dictionary<string, ItemDetails>
    {
        { "CBC + Platelet Count", new ItemDetails(260.00m, "LAB") },
            { "Blood Typing with RH Typing", new ItemDetails(260.00m, "LAB") },
            { "ESR", new ItemDetails(260.00m, "LAB") },
            { "CT BT", new ItemDetails(150.00m, "LAB") },
            { "FBS", new ItemDetails(155.00m, "LAB") },
            { "BUN", new ItemDetails(155.00m, "LAB") },
            { "Creatinine", new ItemDetails(155.00m, "LAB") },
            { "Blood Uric Acid", new ItemDetails(155.00m, "LAB") },
            { "Cholesterol", new ItemDetails(255.00m, "LAB") },
            { "Triglycerides", new ItemDetails(255.00m, "LAB") },
            { "HDL", new ItemDetails(255.00m, "UTZ") },
            { "LDL", new ItemDetails(255.00m, "UTZ") },
            { "VLDL", new ItemDetails(255.00m, "UTZ") },
            { "SGOT", new ItemDetails(255.00m, "UTZ") },
            { "SGPT", new ItemDetails(255.00m, "UTZ") },
            { "Protime", new ItemDetails(460.00m, "UTZ") },
            { "PTT-Retic Count", new ItemDetails(450.00m, "UTZ") },
            { "Malarial Smear", new ItemDetails(400.00m, "UTZ") },
            { "PBS (blood smear)", new ItemDetails(505.00m, "UTZ") },
            { "VDRL/RPR", new ItemDetails(255.00m, "UTZ") },
            { "Widal Test", new ItemDetails(375.00m, "ECG") },
            { "Sodium", new ItemDetails(255.00m, "ECG") },
            { "Potassium", new ItemDetails(255.00m, "ECG") },
            { "Urinalysis", new ItemDetails(85.00m, "ECG") },
            { "Fecalysis", new ItemDetails(80.00m, "ECG") },
            { "Phosphorus", new ItemDetails(310.00m, "ECG") },
            { "Magnesium", new ItemDetails(350.00m, "ECG") },
            { "Acid Phos.", new ItemDetails(350.00m, "ECG") },
            { "Alka Phos.", new ItemDetails(350.00m, "ECG") },
            { "CPK-MB", new ItemDetails(1100.00m, "XRAY") },
            { "CPK-MM", new ItemDetails(1100.00m, "XRAY") },
            { "Troponin I/T", new ItemDetails(1250.00m, "XRAY") },
            { "HEPA B PROFILE", new ItemDetails(2050.00m, "XRAY") },
            { "HEPA ABC PROFILE", new ItemDetails(3100.00m, "XRAY") },


    };
        public decimal TotalPrice { get; private set; } = 0;
        public checklistboxform()
        {
            InitializeComponent();
            InitializeChecklistBox();
        }
        private void InitializeChecklistBox()
        {
            foreach (var item in itemDetails)
            {
                checkedListBox1.Items.Add($"{item.Key} ({item.Value.Label})");
            }

            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
        }


        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var itemDisplay = checkedListBox1.Items[e.Index].ToString();
            var itemName = itemDisplay.Split('(')[0].Trim(); // Extract the name before the label

            if (itemDetails.ContainsKey(itemName))
            {
                var details = itemDetails[itemName];

                if (e.NewValue == CheckState.Checked && !SelectedItems.Contains(itemName))
                {
                    TotalPrice += details.Price;
                    SelectedItems.Add($"{itemName} ({details.Label})");
                }
                else if (e.NewValue == CheckState.Unchecked)
                {
                    TotalPrice -= details.Price;
                    SelectedItems.Remove($"{itemName} ({details.Label})");
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
            
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
