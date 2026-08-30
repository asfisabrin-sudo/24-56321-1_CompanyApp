using CompanyApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void visitWeb_Click(object sender, EventArgs e)
        {
            bmBrowser.Navigate("https://bloggingmetrics.com/");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                Session.Clear();
                new frmLogin().Show();
                this.Close();
            }
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {

        }

        private void btnManageEmployees_Click(object sender, EventArgs e)
        {
            new frmEmployee().ShowDialog();
        }
    }
}
