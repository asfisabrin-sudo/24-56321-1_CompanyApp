using System;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class frmLogin : Form
    {
        User user = new User();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = user.ValidateLogin(txtUsername.Text, txtPassword.Text);

                if (userId > 0)
                {
                    Session.UserId = userId;
                    Session.Username = txtUsername.Text;

                    new frmDashboard().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Username and Password, Please Try Again", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void clickRegister_Click(object sender, EventArgs e)
        {
            new frmRegister().Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Goodbye");
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}