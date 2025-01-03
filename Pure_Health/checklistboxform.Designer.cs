namespace Pure_Health
{
    partial class checklistboxform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.White;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkedListBox1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "CBC + Platelet Count",
            "Blood Typing with RH Typing",
            "ESR",
            "CT BT",
            "FBS",
            "BUN",
            "Creatinine",
            "Blood Uric Acid",
            "Cholesterol",
            "Triglycerides",
            "HDL",
            "LDL",
            "VLDL",
            "SGOT",
            "SGPT",
            "Protime",
            "PTT-Retic Count",
            "Malarial Smear",
            "PBS (blood smear)",
            "VDRL/RPR",
            "Widal Test",
            "Sodium ",
            "Potassium",
            "Urinalysis",
            "Fecalysis",
            "Phosphorus",
            "Magnesium",
            "Acid Phos.",
            "Alka Phos.",
            "CPK-MB",
            "CPK-MM",
            "Troponin I/T",
            "HEPA B PROFILE",
            "HEPA ABC PROFILE",
            "ANTIGEN",
            "ASO",
            "UPCR",
            "VITAMIN D ",
            "TROPONIN I",
            "PROLACTIN",
            "HBAIC",
            "75g OGTT",
            "CHLORIDE",
            "AG",
            "CALCIUM",
            "TYPHIDOT",
            "ANTI-HAV HEPA A",
            "TCO2",
            "PSA",
            "C3",
            "IONIZED IRON",
            "SPUTUM C/S",
            "SPUTUM AFB",
            "DENGUE TRIO",
            "DENGUE TEST",
            "URINE C/S",
            "HBeAg",
            "RUBELLA IgG",
            "CA 125",
            "CRP",
            "RF",
            "RBS",
            "HBU DNA",
            "FERITIN",
            "TIBC",
            "RETIC COUNT",
            "PT SERUM"});
            this.checkedListBox1.Location = new System.Drawing.Point(29, 45);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(344, 399);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Green;
            this.button1.Location = new System.Drawing.Point(272, 459);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 32);
            this.button1.TabIndex = 1;
            this.button1.Text = "Done";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checklistboxform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(402, 503);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "checklistboxform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "checklistboxform";
            this.Load += new System.EventHandler(this.checklistboxform_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button1;
    }
}