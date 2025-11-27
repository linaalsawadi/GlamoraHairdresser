using GlamoraHairdresser.Data;
using GlamoraHairdresser.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GlamoraHairdresser.WinForms.Forms.AdminForms
{
    public partial class CustomerForm : Form
    {
        private readonly GlamoraDbContext _db;

        public CustomerForm(GlamoraDbContext db)
        {
            InitializeComponent();
            _db = db;

            LoadCustomers();
            dgvCustomers.SelectionChanged += (_, __) => FillSelectedCustomer();
        }

        // =====================================================
        // LOAD CUSTOMERS
        // =====================================================
        private void LoadCustomers()
        {
            var data = _db.Customers
                .OrderBy(c => c.FullName)
                .Select(c => new
                {
                    c.Id,
                    c.FullName,
                    c.Email,
                    c.Phone,
                    Created = c.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd")
                })
                .ToList();

            dgvCustomers.DataSource = data;
            FormatGrid();
        }

        // =====================================================
        // FORMAT GRID
        // =====================================================
        private void FormatGrid()
        {
            var g = dgvCustomers;

            if (g.Columns["Id"] != null)
            {
                g.Columns["Id"].Width = 50;
                g.Columns["Id"].HeaderText = "ID";
            }

            if (g.Columns["FullName"] != null)
                g.Columns["FullName"].HeaderText = "Customer Name";

            if (g.Columns["Email"] != null)
                g.Columns["Email"].HeaderText = "Email";

            if (g.Columns["Phone"] != null)
            {
                g.Columns["Phone"].HeaderText = "Phone";
                g.Columns["Phone"].Width = 120;
            }

            if (g.Columns["Created"] != null)
            {
                g.Columns["Created"].HeaderText = "Created At";
                g.Columns["Created"].Width = 100;
            }
        }

        // =====================================================
        // FILL SELECTED CUSTOMER INTO TEXTBOXES
        // =====================================================
        private void FillSelectedCustomer()
        {
            if (dgvCustomers.CurrentRow == null)
                return;

            txtId.Text = dgvCustomers.CurrentRow.Cells["Id"].Value.ToString();
            txtName.Text = dgvCustomers.CurrentRow.Cells["FullName"].Value.ToString();
            txtEmail.Text = dgvCustomers.CurrentRow.Cells["Email"].Value.ToString();
            txtPhone.Text = dgvCustomers.CurrentRow.Cells["Phone"].Value.ToString();
        }

        // =====================================================
        // ADD NEW CUSTOMER
        // =====================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim() == "" || txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Name and Email are required.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newCustomer = new Customer
            {
                FullName = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            _db.Customers.Add(newCustomer);
            _db.SaveChanges();

            MessageBox.Show("Customer added successfully!", "Success");

            LoadCustomers();
            ClearInputs();
        }

        // =====================================================
        // UPDATE CUSTOMER
        // =====================================================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Please select a customer.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customer = _db.Customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                MessageBox.Show("Customer not found!", "Error");
                return;
            }

            customer.FullName = txtName.Text.Trim();
            customer.Email = txtEmail.Text.Trim();
            customer.Phone = txtPhone.Text.Trim();

            _db.SaveChanges();

            MessageBox.Show("Customer updated successfully!");
            LoadCustomers();
        }

        // =====================================================
        // DELETE CUSTOMER
        // =====================================================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Select a customer first.",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customer = _db.Customers
                .Include(c => c.Appointments)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                MessageBox.Show("Customer not found.", "Error");
                return;
            }

            if (customer.Appointments.Any())
            {
                MessageBox.Show("Cannot delete customer with existing appointments!",
                    "Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _db.Customers.Remove(customer);
            _db.SaveChanges();

            MessageBox.Show("Customer deleted.");
            LoadCustomers();
            ClearInputs();
        }

        // =====================================================
        // CLEAR INPUTS
        // =====================================================
        private void ClearInputs()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
        }

        // =====================================================
        // REFRESH BUTTON
        // =====================================================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        // =====================================================
        // CLOSE BUTTON
        // =====================================================
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
