using System;
using System.Windows.Forms;

namespace EmployeeDetails
{
    public partial class frmRegister : Form
    {
        User user = new User();

        public frmRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "" || txtConPassword.Text == "")
            {
                MessageBox.Show("Username and Password fields are empty", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtPassword.Text == txtConPassword.Text)
            {
                try
                {
                    bool success = user.RegisterUser(txtUsername.Text, txtPassword.Text);

                    if (success)
                    {
                        txtUsername.Text = "";
                        txtPassword.Text = "";
                        txtConPassword.Text = "";
                        txtUsername.Focus();

                        MessageBox.Show("Your Account has been Successfully Created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Registration failed. Please try again.", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Password does not matched, Please Re-enter", "Register Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtConPassword.Text = "";
                txtPassword.Focus();
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtConPassword.PasswordChar = '•';
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConPassword.Text = "";
            txtUsername.Focus();
        }

        private void clickLogin_Click(object sender, EventArgs e)
        {
            new frmLogin().Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Goodbye");
            Application.Exit();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmRegister_Load(object sender, EventArgs e)
        {

        }
    }
}