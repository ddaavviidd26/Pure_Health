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
    public partial class Form2 : Form
    {
        formDashboard dashboard;
        formPatient patient;
        formInventory inventory;
        formEmployees employees;
        formScheduling scheduling;
        formAudittrails audittrails;
        formAccounting accounting;
        formReports reports;
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        bool menuExpand = false;
        private void menutransition_Tick(object sender, EventArgs e)
        {
            if(menuExpand == false) {
                menuContainer.Height += 10;
                if (menuContainer.Height >= 196) {
                    menutransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                menuContainer.Height -= 10;
                if(menuContainer.Height <= 52)
                {
                    menutransition.Stop();
                    menuExpand = false;
                }
            }
        }

        private void menu_Click(object sender, EventArgs e)
        {
            menutransition.Start();
        }
        bool sidebarExpand = true;
        private void sidebarTransition_Tick(object sender, EventArgs e)
        {
            if(sidebarExpand ) {
                sidebar.Width -= 10;
                if(sidebar.Width <= 64)
                {
                    sidebarExpand=false;
                    sidebarTransition.Stop();

                   
                }
                } else
            {
                sidebar.Width += 10;
                if (sidebar.Width >= 280)
                {
                    sidebarExpand=true;
                    sidebarTransition.Stop() ;

                    
                }
            }
            }

        private void btnHam_Click(object sender, EventArgs e)
        {
            sidebarTransition.Start();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            if (dashboard == null)
            {
                dashboard = new formDashboard();
                dashboard.FormClosed += Dashboard_FormClosed;
                dashboard.MdiParent = this;
                dashboard.Show();   
            }
            else
            {
                dashboard.Activate();
            }
        }
        private void Dashboard_FormClosed(object sender, EventArgs e)
        {
            dashboard = null;
        }

        private void btnPatient_Click(object sender, EventArgs e)
        {
            if(patient==null)
            {
                patient= new formPatient();
                patient.FormClosed += patient_FormClosed;
                patient.MdiParent = this;
                patient.Dock= DockStyle.Fill;
                patient.Show();
            }else
            {
                patient.Activate();
            }
        }
        private void patient_FormClosed(object sender, EventArgs e)
        {
            patient = null;
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            if (inventory == null)
            {
                inventory = new formInventory();
                inventory.FormClosed += patient_FormClosed;
                inventory.MdiParent = this;
                inventory.Dock = DockStyle.Fill;
                inventory.Show();
            }
            else
            {
                inventory.Activate();
            }
        }
        private void invenory_FormClosed(object sender, EventArgs e)
        {
            inventory = null;
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            if (employees == null)
            {
                employees = new formEmployees();
                employees.FormClosed += patient_FormClosed;
                employees.MdiParent = this;
                employees.Dock = DockStyle.Fill;
                employees.Show();
            }
            else
            {
                employees.Activate();
            }
        }
        private void employees_FormClosed(object sender, EventArgs e)
        {
            employees = null;
        }

        private void btnScheduling_Click(object sender, EventArgs e)
        {
            if (scheduling == null)
            {
                scheduling = new formScheduling();
                scheduling.FormClosed += scheduling_FormClosed;
                scheduling  .MdiParent = this;
                scheduling.Dock = DockStyle.Fill;
                scheduling.Show();
            }
            else
            {
                scheduling.Activate();
            }
        }
        private void scheduling_FormClosed(object sender, EventArgs e)
        {
            scheduling = null;
        }

        private void btnAccounting_Click(object sender, EventArgs e)
        {
            if (accounting == null)
            {
                accounting = new formAccounting();
                accounting.FormClosed += accounting_FormClosed;
                accounting.MdiParent = this;
                accounting.Dock = DockStyle.Fill;
                accounting.Show();
            }
            else
            {
                accounting.Activate();
            }
        }
        private void accounting_FormClosed(object sender, EventArgs e)
        {
            accounting = null;
        }

        private void btnAudittrails_Click(object sender, EventArgs e)
        {
            if (audittrails == null)
            {
                audittrails = new formAudittrails();
                audittrails.FormClosed += audittrails_FormClosed;
                audittrails.MdiParent = this;
                audittrails.Dock = DockStyle.Fill;
                audittrails.Show();
            }
            else
            {
                audittrails.Activate();
            }
        }
        private void audittrails_FormClosed(object sender, EventArgs e)
        {
            audittrails = null;
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            if (reports == null)
            {
                reports = new formReports();
                reports.FormClosed += reports_FormClosed;
                reports.MdiParent = this;
                reports.Dock = DockStyle.Fill;
                reports.Show();
            }
            else
            {
                reports.Activate();
            }
        }
        private void reports_FormClosed(object sender, EventArgs e)
        {
            reports = null;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            {
                var result = MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }
    }
}


