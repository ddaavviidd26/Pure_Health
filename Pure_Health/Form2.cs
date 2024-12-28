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
                dashboard.MdiParent=this;
                dashboard.Show();
            }
            else
            {
                dashboard.Activate();
            }
        }
        private void Dashboard_FormClosed(object sender, EventArgs e)
        {
            dashboard=null;
        }
    }
}


