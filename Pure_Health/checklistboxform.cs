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
            { "HDL", new ItemDetails(255.00m, "LAB") },
            { "LDL", new ItemDetails(255.00m, "LAB") },
            { "VLDL", new ItemDetails(255.00m, "LAB") },
            { "SGOT", new ItemDetails(255.00m, "LAB") },
            { "SGPT", new ItemDetails(255.00m, "LAB") },
            { "Protime", new ItemDetails(460.00m, "LAB") },
            { "PTT-Retic Count", new ItemDetails(450.00m, "LAB") },
            { "Malarial Smear", new ItemDetails(400.00m, "LAB") },
            { "PBS (blood smear)", new ItemDetails(505.00m, "LAB") },
            { "VDRL/RPR", new ItemDetails(255.00m, "LAB") },
            { "Widal Test", new ItemDetails(375.00m, "LAB") },
            { "Sodium", new ItemDetails(255.00m, "LAB") },
            { "Potassium", new ItemDetails(255.00m, "LAB") },
            { "Urinalysis", new ItemDetails(85.00m, "LAB") },
            { "Fecalysis", new ItemDetails(80.00m, "LAB") },
            { "Phosphorus", new ItemDetails(310.00m, "LAB") },
            { "Magnesium", new ItemDetails(350.00m, "LAB") },
            { "Acid Phos.", new ItemDetails(350.00m, "LAB") },
            { "Alka Phos.", new ItemDetails(350.00m, "LAB") },
            { "CPK-MB", new ItemDetails(1100.00m, "LAB") },
            { "CPK-MM", new ItemDetails(1100.00m, "LAB") },
            { "Troponin I/T", new ItemDetails(1250.00m, "LAB") },
            { "HEPA B PROFILE", new ItemDetails(2050.00m, "LAB") },
            { "HEPA ABC PROFILE", new ItemDetails(3100.00m, "LAB") },


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
