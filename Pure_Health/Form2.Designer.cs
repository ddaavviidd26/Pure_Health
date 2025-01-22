namespace Pure_Health
{
    partial class Form2
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHam = new System.Windows.Forms.PictureBox();
            this.sidebar = new System.Windows.Forms.FlowLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.menuContainer = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnEmployee = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnInventory = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.menu = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnPatient = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnScheduling = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnAccounting = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.btnReports = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.menutransition = new System.Windows.Forms.Timer(this.components);
            this.sidebarTransition = new System.Windows.Forms.Timer(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).BeginInit();
            this.sidebar.SuspendLayout();
            this.panel6.SuspendLayout();
            this.menuContainer.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnHam);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1293, 45);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(39, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "PUREHEALTH DIAGNOSTIC CENTER";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnHam
            // 
            this.btnHam.Image = global::Pure_Health.Properties.Resources.menu__1_;
            this.btnHam.Location = new System.Drawing.Point(6, 7);
            this.btnHam.Name = "btnHam";
            this.btnHam.Size = new System.Drawing.Size(27, 32);
            this.btnHam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnHam.TabIndex = 1;
            this.btnHam.TabStop = false;
            this.btnHam.Click += new System.EventHandler(this.btnHam_Click);
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.Green;
            this.sidebar.Controls.Add(this.panel6);
            this.sidebar.Controls.Add(this.menuContainer);
            this.sidebar.Controls.Add(this.panel3);
            this.sidebar.Controls.Add(this.panel4);
            this.sidebar.Controls.Add(this.panel11);
            this.sidebar.Controls.Add(this.panel5);
            this.sidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sidebar.Location = new System.Drawing.Point(0, 45);
            this.sidebar.Name = "sidebar";
            this.sidebar.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.sidebar.Size = new System.Drawing.Size(280, 708);
            this.sidebar.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnDashboard);
            this.panel6.Location = new System.Drawing.Point(3, 33);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(277, 52);
            this.panel6.TabIndex = 5;
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.Color.Green;
            this.btnDashboard.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.Image = global::Pure_Health.Properties.Resources.dashboard__1_;
            this.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.Location = new System.Drawing.Point(-16, -21);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnDashboard.Size = new System.Drawing.Size(299, 99);
            this.btnDashboard.TabIndex = 4;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // menuContainer
            // 
            this.menuContainer.Controls.Add(this.panel10);
            this.menuContainer.Controls.Add(this.panel9);
            this.menuContainer.Controls.Add(this.panel7);
            this.menuContainer.Controls.Add(this.panel8);
            this.menuContainer.Location = new System.Drawing.Point(3, 91);
            this.menuContainer.Name = "menuContainer";
            this.menuContainer.Size = new System.Drawing.Size(285, 52);
            this.menuContainer.TabIndex = 7;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.btnEmployee);
            this.panel10.Location = new System.Drawing.Point(3, 144);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(280, 52);
            this.panel10.TabIndex = 6;
            // 
            // btnEmployee
            // 
            this.btnEmployee.BackColor = System.Drawing.Color.SeaGreen;
            this.btnEmployee.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmployee.Image = global::Pure_Health.Properties.Resources.teamwork;
            this.btnEmployee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEmployee.Location = new System.Drawing.Point(-13, -21);
            this.btnEmployee.Name = "btnEmployee";
            this.btnEmployee.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnEmployee.Size = new System.Drawing.Size(297, 99);
            this.btnEmployee.TabIndex = 4;
            this.btnEmployee.Text = "Employee";
            this.btnEmployee.UseVisualStyleBackColor = false;
            this.btnEmployee.Click += new System.EventHandler(this.btnEmployee_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnInventory);
            this.panel9.Location = new System.Drawing.Point(3, 97);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(280, 52);
            this.panel9.TabIndex = 5;
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.SeaGreen;
            this.btnInventory.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventory.Image = global::Pure_Health.Properties.Resources.shipping;
            this.btnInventory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInventory.Location = new System.Drawing.Point(-13, -16);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnInventory.Size = new System.Drawing.Size(297, 99);
            this.btnInventory.TabIndex = 4;
            this.btnInventory.Text = "Inventory";
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.menu);
            this.panel7.Location = new System.Drawing.Point(3, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(280, 52);
            this.panel7.TabIndex = 5;
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.Green;
            this.menu.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu.Image = global::Pure_Health.Properties.Resources.home;
            this.menu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menu.Location = new System.Drawing.Point(-19, -21);
            this.menu.Margin = new System.Windows.Forms.Padding(0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.menu.Size = new System.Drawing.Size(299, 99);
            this.menu.TabIndex = 4;
            this.menu.Text = "Menu";
            this.menu.UseVisualStyleBackColor = false;
            this.menu.Click += new System.EventHandler(this.menu_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnPatient);
            this.panel8.Location = new System.Drawing.Point(3, 49);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(280, 52);
            this.panel8.TabIndex = 5;
            // 
            // btnPatient
            // 
            this.btnPatient.BackColor = System.Drawing.Color.SeaGreen;
            this.btnPatient.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatient.Image = global::Pure_Health.Properties.Resources.patient;
            this.btnPatient.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPatient.Location = new System.Drawing.Point(-13, -12);
            this.btnPatient.Margin = new System.Windows.Forms.Padding(0);
            this.btnPatient.Name = "btnPatient";
            this.btnPatient.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnPatient.Size = new System.Drawing.Size(297, 99);
            this.btnPatient.TabIndex = 4;
            this.btnPatient.Text = "Patient";
            this.btnPatient.UseVisualStyleBackColor = false;
            this.btnPatient.Click += new System.EventHandler(this.btnPatient_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnScheduling);
            this.panel3.Location = new System.Drawing.Point(3, 149);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(277, 52);
            this.panel3.TabIndex = 5;
            // 
            // btnScheduling
            // 
            this.btnScheduling.BackColor = System.Drawing.Color.Green;
            this.btnScheduling.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScheduling.Image = global::Pure_Health.Properties.Resources.schedule;
            this.btnScheduling.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScheduling.Location = new System.Drawing.Point(-13, -21);
            this.btnScheduling.Name = "btnScheduling";
            this.btnScheduling.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnScheduling.Size = new System.Drawing.Size(296, 99);
            this.btnScheduling.TabIndex = 4;
            this.btnScheduling.Text = "Scheduling";
            this.btnScheduling.UseVisualStyleBackColor = false;
            this.btnScheduling.Click += new System.EventHandler(this.btnScheduling_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnAccounting);
            this.panel4.Location = new System.Drawing.Point(3, 207);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(277, 52);
            this.panel4.TabIndex = 6;
            // 
            // btnAccounting
            // 
            this.btnAccounting.BackColor = System.Drawing.Color.Green;
            this.btnAccounting.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccounting.Image = global::Pure_Health.Properties.Resources.profile_user;
            this.btnAccounting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccounting.Location = new System.Drawing.Point(-13, -21);
            this.btnAccounting.Name = "btnAccounting";
            this.btnAccounting.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnAccounting.Size = new System.Drawing.Size(296, 99);
            this.btnAccounting.TabIndex = 4;
            this.btnAccounting.Text = "User Account";
            this.btnAccounting.UseVisualStyleBackColor = false;
            this.btnAccounting.Click += new System.EventHandler(this.btnAccounting_Click);
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.btnReports);
            this.panel11.Location = new System.Drawing.Point(3, 265);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(280, 52);
            this.panel11.TabIndex = 6;
            // 
            // btnReports
            // 
            this.btnReports.BackColor = System.Drawing.Color.Green;
            this.btnReports.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReports.Image = global::Pure_Health.Properties.Resources.report;
            this.btnReports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReports.Location = new System.Drawing.Point(-13, -21);
            this.btnReports.Name = "btnReports";
            this.btnReports.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnReports.Size = new System.Drawing.Size(296, 99);
            this.btnReports.TabIndex = 4;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnLogout);
            this.panel5.Location = new System.Drawing.Point(3, 323);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(277, 52);
            this.panel5.TabIndex = 7;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Green;
            this.btnLogout.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Image = global::Pure_Health.Properties.Resources.logout;
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(-13, -21);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(25, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(296, 99);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // menutransition
            // 
            this.menutransition.Interval = 10;
            this.menutransition.Tick += new System.EventHandler(this.menutransition_Tick);
            // 
            // sidebarTransition
            // 
            this.sidebarTransition.Interval = 10;
            this.sidebarTransition.Tick += new System.EventHandler(this.sidebarTransition_Tick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1293, 753);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHam)).EndInit();
            this.sidebar.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.menuContainer.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnHam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel sidebar;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnScheduling;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnAccounting;
        private System.Windows.Forms.Panel menuContainer;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button menu;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnPatient;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button btnEmployee;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Timer menutransition;
        private System.Windows.Forms.Timer sidebarTransition;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}