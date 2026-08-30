using System;
using System.Windows.Forms;
using EmployeeDetails;
namespace CompanyApp
{
    public partial class frmEmployee : Form
    {
        Employee employee = new Employee();

        public frmEmployee()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        // Add employee details when clicking the Add button
        private void btnAdd_Click(object sender, EventArgs e)
        {
            employee.EmpId = txtEmpId.Text;
            employee.EmpName = txtEmpName.Text;
            employee.Age = txtAge.Text;
            employee.ContactNo = txtContactNo.Text;
            employee.Gender = cboGender.Text;
            employee.CreatedBy = Session.UserId;
            var success = employee.InsertEmployee(employee);
            RefreshGrid();

            if (success)
            {
                ClearControls();
                MessageBox.Show("Employee has been added successfully");
            }
            else
            {
                MessageBox.Show("Error occurred. Please try again...");
            }
        }

        // Update selected employee details when clicking the Update button
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            employee.EmpId = txtEmpId.Text;
            employee.EmpName = txtEmpName.Text;
            employee.Age = txtAge.Text;
            employee.ContactNo = txtContactNo.Text;
            employee.Gender = cboGender.Text;

            var success = employee.UpdateEmployee(employee);
            RefreshGrid();

            if (success)
            {
                ClearControls();
                MessageBox.Show("Employee has been updated successfully");
            }
            else
            {
                MessageBox.Show("Error occurred. Please try again...");
            }
        }

        // Delete selected employee when clicking the Delete button
        private void btnDelete_Click(object sender, EventArgs e)
        {
            employee.EmpId = txtEmpId.Text;

            var success = employee.DeleteEmployee(employee);
            RefreshGrid();

            if (success)
            {
                ClearControls();
                MessageBox.Show("Employee has been deleted successfully");
            }
            else
            {
                MessageBox.Show("Error occurred. Please try again...");
            }
        }

        // Clear all controls when clicking the Clear button
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            txtEmpId.Text = "";
            txtEmpName.Text = "";
            txtAge.Text = "";
            txtContactNo.Text = "";
            cboGender.Text = "";
        }

        private void RefreshGrid()
        {
            dgvEmployeeDetails.DataSource = employee.GetEmployees();
            dgvEmployeeDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Fill the controls when a grid row is clicked, for Update/Delete purposes
        private void dgvEmployeeDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvEmployeeDetails.Rows[e.RowIndex];
            txtEmpId.Text = row.Cells["EmpId"].Value.ToString();
            txtEmpName.Text = row.Cells["EmpName"].Value.ToString();
            txtAge.Text = row.Cells["EmpAge"].Value.ToString();
            txtContactNo.Text = row.Cells["EmpContact"].Value.ToString();
            cboGender.Text = row.Cells["EmpGender"].Value.ToString();
        }
    }
}