using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing
{
    public partial class userAccountSystem : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-SLBI6LR\SQLEXPRESS;Initial Catalog = FinalDb;Integrated Security=True");

        public userAccountSystem()
        {
            InitializeComponent();
            displayUsersData();
        }


        public void RefreshData()
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke((MethodInvoker)delegate { RefreshData(); });
                return;
            }
            displayUsersData();
        }

        public void displayUsersData()
        {
            usersData sd = new usersData();
            List<usersData> listData = sd.GetusersData();

            dataGridView1.DataSource = listData;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtUserid.Text == ""
            || txtUsername.Text == ""
            || txtPassword.Text == ""
            || txtFirstname.Text == ""
            || txtLastname.Text == ""
            || cmbPrivilege.Text == ""
            )
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (connectionString.State == ConnectionState.Closed)
                {
                    try
                    {
                        connectionString.Open();

                        // Check for duplicate ID Number before registration
                        string checkUserIdQuery = "SELECT COUNT(*) FROM Users WHERE UserId = @UserId";
                        using (SqlCommand checkCmd = new SqlCommand(checkUserIdQuery, connectionString))
                        {
                            checkCmd.Parameters.AddWithValue("@UserId", int.Parse(txtUserid.Text.Trim()));
                            int count = (int)checkCmd.ExecuteScalar();

                            if (count >= 1)
                            {
                                MessageBox.Show("ID Number "+txtUserid.Text.Trim() + " is already taken!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method if duplicate EDP Code is found
                            }
                        }

                        string action = "Register"; // Set action for registration                       

                        using (SqlCommand cmd = new SqlCommand("dbo.ManageUser", connectionString))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Action", action);
                            cmd.Parameters.AddWithValue("@UserId", txtUserid.Text.Trim());
                            cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                            cmd.Parameters.AddWithValue("@Firstname", txtFirstname.Text.Trim());
                            cmd.Parameters.AddWithValue("@Lastname", txtLastname.Text.Trim());
                            cmd.Parameters.AddWithValue("@Privilege", cmbPrivilege.Text.Trim());

                            cmd.ExecuteNonQuery();
                            RefreshData();

                            MessageBox.Show("Account created successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectionString.Close();
                    }
                }

            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (txtUserid.Text == ""
           || txtUsername.Text == ""
           || txtPassword.Text == ""
           || txtFirstname.Text == ""
           || txtLastname.Text == ""
           || cmbPrivilege.Text == ""
             )
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (connectionString.State == ConnectionState.Closed)
                {
                    try
                    {
                        connectionString.Open();

                        // Check for duplicate ID Number before registration
                        string checkUserIdQuery = "SELECT COUNT(*) FROM Users WHERE UserId = @UserId";
                        using (SqlCommand checkCmd = new SqlCommand(checkUserIdQuery, connectionString))
                        {
                            checkCmd.Parameters.AddWithValue("@UserId", int.Parse(txtUserid.Text.Trim()));
                            int count = (int)checkCmd.ExecuteScalar();

                            if (count == 0)
                            {
                                MessageBox.Show("ID number does not exist in the database. Please register the student first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method
                            }

                            if (count > 1)
                            {
                                MessageBox.Show(" ID Number cannot be modified!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method if duplicate ID is found
                            }
                        }

                        string action = "Update"; // Set action for registration                       

                        using (SqlCommand cmd = new SqlCommand("dbo.ManageUser", connectionString))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Action", action);
                            cmd.Parameters.AddWithValue("@UserId", txtUserid.Text.Trim());
                            cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                            cmd.Parameters.AddWithValue("@Firstname", txtFirstname.Text.Trim());
                            cmd.Parameters.AddWithValue("@Lastname", txtLastname.Text.Trim());
                            cmd.Parameters.AddWithValue("@Privilege", cmbPrivilege.Text.Trim());

                            cmd.ExecuteNonQuery();
                            RefreshData();

                            MessageBox.Show("Account created successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectionString.Close();
                    }
                }

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserid.Text))
            {
                MessageBox.Show("Please select a User to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm with the user before deleting
            DialogResult result = MessageBox.Show("Are you sure you want to delete this User?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Delete the student record from the database
                    using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("DELETE FROM Users WHERE UserId = @UserId", connection))
                        {
                            command.Parameters.AddWithValue("@UserId", int.Parse(txtUserid.Text));

                            command.ExecuteNonQuery();
                            RefreshData();
                        }
                    }

                    MessageBox.Show("User deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearFields()
        {
            txtUserid.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtFirstname.Text = "";
            txtLastname.Text = "";
            cmbPrivilege.Text = "";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];  // Assuming single selection

                string UserId = row.Cells[0].Value.ToString();
                string Username = row.Cells[1].Value.ToString();
                string Password = row.Cells[2].Value.ToString();  // Adjust index
                string Firstname = row.Cells[3].Value.ToString();  // Adjust index
                string Lastname = row.Cells[4].Value.ToString();  // Adjust index
                string Privilege = row.Cells[5].Value.ToString();  // Adjust index

                txtUserid.Text = UserId;
                txtUsername.Text = Username;
                txtPassword.Text = Password;
                txtFirstname.Text = Firstname;
                txtLastname.Text = Lastname;
                cmbPrivilege.SelectedIndex = cmbPrivilege.FindStringExact(Privilege);
            }
            else
            {
                MessageBox.Show("Invalid ID Number format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear any existing data in the DataGridView

                dataGridView1.DataSource = null;

                if (txtSearch.Text.Trim() == "") // Check if search field is empty
                {
                    string defaultQuery = "SELECT * FROM Users";  // Replace with your default student selection logic

                    connectionString.Open();
                    using (SqlCommand cmd = new SqlCommand(defaultQuery, connectionString))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                    return;
                }

                connectionString.Open();
                // Use parameterized query to prevent SQL injection vulnerabilities
                string sql = "SearchUsers";
                using (SqlCommand cmd = new SqlCommand(sql, connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@searchValue", txtSearch.Text.Trim());

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No User found matching your search criteria.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Display the default student list after showing the message
                            string defaultQuery = "SELECT * FROM Users"; // Replace with your default student selection logic
                            using (SqlCommand cmd2 = new SqlCommand(defaultQuery, connectionString)) // Create a new command for the default query
                            {
                                using (SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2))
                                {
                                    DataTable dt2 = new DataTable();
                                    adapter2.Fill(dt2);
                                    dataGridView1.DataSource = dt2;
                                }
                            }
                            txtSearch.Clear();
                        }
                        else
                        {
                            dataGridView1.DataSource = dt; // Set the DataGridView data source if results exist
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectionString.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Clear any existing data in the DataGridView

                

                if (txtSearch.Text.Trim() == "") // Check if search field is empty
                {
                    string defaultQuery = "SELECT * FROM Users";  // Replace with your default student selection logic

                    connectionString.Open();
                    using (SqlCommand cmd = new SqlCommand(defaultQuery, connectionString))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                    return;
                }

                
                // Use parameterized query to prevent SQL injection vulnerabilities
                string sql = "SearchUsers";
                using (SqlCommand cmd = new SqlCommand(sql, connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@searchValue", txtSearch.Text.Trim());

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No User found matching your search criteria.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Display the default student list after showing the message
                            string defaultQuery = "SELECT * FROM Users"; // Replace with your default student selection logic
                            using (SqlCommand cmd2 = new SqlCommand(defaultQuery, connectionString)) // Create a new command for the default query
                            {
                                using (SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2))
                                {
                                    DataTable dt2 = new DataTable();
                                    adapter2.Fill(dt2);
                                    dataGridView1.DataSource = dt2;
                                }
                            }
                            txtSearch.Clear();
                        }
                        else
                        {
                            dataGridView1.DataSource = dt; // Set the DataGridView data source if results exist
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectionString.Close();
            }
        }
    }
 }
