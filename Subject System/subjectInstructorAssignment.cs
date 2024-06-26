using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing.Subject_System
{
    public partial class subjectInstructorAssignment : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-SLBI6LR\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True");

        public subjectInstructorAssignment()
        {
            InitializeComponent();
            DisplayEnrolledInstructors();
            DisplaySubject();
            DisplayInstructors();
        }

        //INSTRUCTORS THAT ARE ENROLLED TO A SUBJECT/S
        private void DisplayEnrolledInstructors()
        {
            try
            {
                // Ensure the connection is closed before attempting to open it
                if (connectionString.State != ConnectionState.Closed)
                {
                    connectionString.Close();
                }

                connectionString.Open();

                using (SqlCommand cmd = new SqlCommand("GetEnrolledInstructors", connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Clear existing columns
                    dataGridView1.Columns.Clear();

                    // Bind the DataGridView to the DataTable
                    dataGridView1.DataSource = dt;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectionString.Close();
            }
        }

        //LISTS OF REGISTERED STUDENTS
        private void DisplaySubject()
        {
            try
            {
                // Ensure the connection is closed before attempting to open it
                if (connectionString.State != ConnectionState.Closed)
                {
                    connectionString.Close();
                }

                connectionString.Open();

                using (SqlCommand cmd = new SqlCommand("GetSubjectsList", connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Modify column name in the DataTable
                    dt.Columns["EDPCode"].ColumnName = "EDP Code";
                    dt.Columns["Title"].ColumnName = "Title";
                    dt.Columns["SubjectCode"].ColumnName = "Subject Code";
                    dt.Columns["Units"].ColumnName = "Units";
                    dt.Columns["Schedule"].ColumnName = "Schedule";
                    dt.Columns["StartTime"].ColumnName = "Start Time";
                    dt.Columns["EndTime"].ColumnName = "End Time";
                    dt.Columns["Course"].ColumnName = "Course";
                    // Clear existing columns
                    dataGridView3.Columns.Clear();

                    // Bind the DataGridView to the DataTable
                    dataGridView3.DataSource = dt;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectionString.Close();
            }
        }
        //LISTS OF REGISTERED INSTRUCTORS
        private void DisplayInstructors()
        {
            try
            {
                // Ensure the connection is closed before attempting to open it
                if (connectionString.State != ConnectionState.Closed)
                {
                    connectionString.Close();
                }

                connectionString.Open();

                using (SqlCommand cmd = new SqlCommand("GetInstructorsList", connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Modify column name in the DataTable
                    /*
                    dt.Columns["IDNumber"].ColumnName = "ID Number";
                    dt.Columns["Firstname"].ColumnName = "Firstname";
                    dt.Columns["Lastname"].ColumnName = "Lastname";
                    dt.Columns["Gender"].ColumnName = "Gender";
                    dt.Columns["Course"].ColumnName = "Course";
                    dt.Columns["Year"].ColumnName = "Year";
                    dt.Columns["SchoolYear"].ColumnName = "School Year";
                    dt.Columns["Semester"].ColumnName = "Semester";
                    */

                    // Clear existing columns
                    dataGridView2.Columns.Clear();

                    // Bind the DataGridView to the DataTable
                    dataGridView2.DataSource = dt;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectionString.Close();
            }
        }

        public void clearFields()
        {
            txtIDNumber.Clear();
            txtEDPCode.Clear();
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void btnEnroll_Click(object sender, EventArgs e)
        {
            int IDNumber;
            int EDPCode;

            if (!int.TryParse(txtIDNumber.Text, out IDNumber) || !int.TryParse(txtEDPCode.Text, out EDPCode))
            {
                MessageBox.Show("Please enter valid student ID and subject ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                connectionString.Open();

                using (SqlCommand cmd = new SqlCommand("ManageInstructorSubjectEnrollment", connectionString))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", IDNumber);
                    cmd.Parameters.AddWithValue("@EDPCode", EDPCode);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@SubjectCode", txtSubjectCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@Schedule", txtSchedule.Text.Trim());
                    cmd.Parameters.AddWithValue("@StartTime", txtStartTime.Text.Trim());
                    cmd.Parameters.AddWithValue("@EndTime", txtEndTime.Text.Trim());

                    cmd.Parameters.AddWithValue("@Action", "Enroll"); // Specify the action as Enroll


                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Student enrolled successfully in the subject.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DisplayEnrolledInstructors();
                    clearFields();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectionString.Close();
            }
        }

        private void btnDisenroll_Click(object sender, EventArgs e)
        {
            // Check if any row is selected in the DataGridView
            if (string.IsNullOrWhiteSpace(txtEDPCode.Text)
                || string.IsNullOrWhiteSpace(txtIDNumber.Text))
            {
                MessageBox.Show("Please select to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Parse values from TextBoxes to appropriate data types
                    int IDNumber = int.Parse(txtIDNumber.Text);
                    int EDPCode = int.Parse(txtEDPCode.Text);

                    // Ensure the connection is closed before attempting to open it
                    if (connectionString.State != ConnectionState.Closed)
                    {
                        connectionString.Close();
                    }

                    connectionString.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.ManageInstructorSubjectEnrollment", connectionString))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Pass extracted values as parameters
                        cmd.Parameters.AddWithValue("@UserId", IDNumber);
                        cmd.Parameters.AddWithValue("@EDPCode", EDPCode);
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCode", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Schedule", txtSchedule.Text.Trim());
                        cmd.Parameters.AddWithValue("@StartTime", txtStartTime.Text.Trim());
                        cmd.Parameters.AddWithValue("@EndTime", txtEndTime.Text.Trim());
                        cmd.Parameters.AddWithValue("@Action", "Disenroll");
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account disenrolled successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh the DataGridView to reflect the changes
                        DisplayEnrolledInstructors();
                        clearFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectionString.Close();
                }
            }
        }

        //------------------------------------------------------------------------------------------------------------------------

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];  // Assuming single selection

                
                string IDNumber = row.Cells[0].Value.ToString();
                string EDPCode = row.Cells[3].Value.ToString();


                txtIDNumber.Text = IDNumber;
                txtEDPCode.Text = EDPCode;
                
            }
            else
            {
                MessageBox.Show("Invalid EDP Code format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow row = dataGridView2.SelectedRows[0];  // Assuming single selection

                string UserId = row.Cells[0].Value.ToString();

                txtIDNumber.Text = UserId;
            }
            else
            {
                MessageBox.Show("Invalid EDP Code format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow row = dataGridView3.SelectedRows[0];  // Assuming single selection

                string EDPCode = row.Cells[0].Value.ToString();
                string Title = row.Cells[1].Value.ToString();
                string SubjectCode = row.Cells[2].Value.ToString();  // Adjust index
                string Units = row.Cells[3].Value.ToString();  // Adjust index
                string Schedule = row.Cells[4].Value.ToString();  // Adjust index
                string StartTime = row.Cells[5].Value.ToString();  // Adjust index
                string EndTime = row.Cells[6].Value.ToString();  // Adjust index
                string Course = row.Cells[7].Value.ToString();  // Adjust index   



                txtEDPCode.Text = EDPCode;
                txtTitle.Text = Title;
                txtSubjectCode.Text = SubjectCode;
                txtSchedule.Text = Schedule;
                txtStartTime.Text = StartTime;
                txtEndTime.Text = EndTime;
            }
            else
            {
                MessageBox.Show("Invalid EDP Code format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        //------------------------------------------------------------------------------------------------------------------------

        private void txtInstructorAssignmentSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = txtInstructorAssignmentSearch.Text.Trim();

            try
            {

                connectionString.Open();

                using (SqlCommand cmd = new SqlCommand("SearchInstructorEnrolledStudents", connectionString))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@searchValue", searchValue);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No matching records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        dataGridView1.DataSource = dt; // Set the DataGridView data source if results exist
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectionString.Close();
            }
        }

        private void txtInstructorSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtInstructorSearch.Text.Trim() == "") // Check if search field is empty
                {
                    string defaultQuery = "SELECT UserId, Firstname, Lastname, Privilege FROM Users WHERE Privilege = 'instructor' AND Privilege <> 'Admin'";



                    connectionString.Open();
                    using (SqlCommand cmd = new SqlCommand(defaultQuery, connectionString))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView2.DataSource = dt;
                        }
                    }
                    return;
                }

                string sql = "SearchUsersAssignment";
                using (SqlCommand cmd = new SqlCommand(sql, connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@searchValue", txtInstructorSearch.Text.Trim());

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No Instructors found matching your search criteria.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Display the default instructor list after showing the message
                            txtInstructorSearch.Clear();
                            // Execute the default query directly instead of calling the stored procedure again
                            string defaultQuery = "SELECT UserId, Firstname, Lastname, Privilege FROM Users WHERE Privilege = 'instructor' AND Privilege <> 'Admin'";
                            using (SqlCommand cmd2 = new SqlCommand(defaultQuery, connectionString))
                            {
                                using (SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2))
                                {
                                    DataTable dt2 = new DataTable();
                                    adapter2.Fill(dt2);
                                    dataGridView2.DataSource = dt2;
                                }
                            }
                        }
                        else
                        {
                            dataGridView2.DataSource = dt; // Set the DataGridView data source if results exist
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



        private void txtSubjectSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Clear any existing data in the DataGridView


                if (txtSubjectSearch.Text.Trim() == "") // Check if search field is empty
                {
                    string defaultQuery = "SELECT * FROM Subjects";  // Replace with your default student selection logic

                    connectionString.Open();
                    using (SqlCommand cmd = new SqlCommand(defaultQuery, connectionString))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView3.DataSource = dt;
                        }
                    }
                    return;
                }

                // Use parameterized query to prevent SQL injection vulnerabilities
                string sql = "SearchSubjects";
                using (SqlCommand cmd = new SqlCommand(sql, connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@searchValue", txtSubjectSearch.Text.Trim());

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No Subject found matching your search criteria.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Display the default student list after showing the message
                            string defaultQuery = "SELECT * FROM Subjects"; // Replace with your default student selection logic
                            using (SqlCommand cmd2 = new SqlCommand(defaultQuery, connectionString)) // Create a new command for the default query
                            {
                                using (SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2))
                                {
                                    DataTable dt2 = new DataTable();
                                    adapter2.Fill(dt2);
                                    dataGridView3.DataSource = dt2;
                                }
                            }
                            txtSubjectSearch.Clear();
                        }
                        else
                        {
                            dataGridView3.DataSource = dt; // Set the DataGridView data source if results exist
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

        //------------------------------------------------------------------------------------------------------------------------



    }
}
