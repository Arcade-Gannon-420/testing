using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testing;

namespace testing.Subject_System
{
    public partial class subjectEnrollmentSystem : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-SLBI6LR\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True");
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; } // Corrected property name
        public subjectEnrollmentSystem(string firstName, string lastName, string userRole)
        {
            InitializeComponent();
            SetUserInfo(firstName, lastName, userRole);
            this.Load += new EventHandler(subjectEnrollmentSystem_Load);  // Attach the load event handler
        }
        private void SetUserInfo(string firstName, string lastName, string userRole)
        {
            FirstName = firstName;
            LastName = lastName;
            UserRole = userRole; // Corrected assignment to class property
            // Display the information in the control
        }

        private void subjectEnrollmentSystem_Load(object sender, EventArgs e)
        {
            DisplayEnrolledStudents();
            DisplaySubject();
            DisplayStudents();
        }


        //------------------------------------------------------------------------------------------------------------------------  
        //STUDENTS THAT ARE ENROLLED TO A SUBJECT/S
        private void DisplayEnrolledStudents()
        {
            if (UserRole == "superadmin")
            {
                DisplayAllEnrolledStudents();
            }
            else if (UserRole == "instructor")
            {
                DisplayInstructorEnrolledStudents();
            }
        }
        private void DisplayAllEnrolledStudents()
        {
            try
            {
                if (connectionString.State != ConnectionState.Closed)
                {
                    connectionString.Close();
                }

                connectionString.Open();

                using (SqlCommand cmd = new SqlCommand("GetEnrolledStudents", connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Remove the EnrollmentID column from the DataTable
                    dt.Columns.Remove("EnrollmentID");

                    // Rename columns for display
                    dt.Columns["IDNumber"].ColumnName = "ID Number";
                    dt.Columns["Firstname"].ColumnName = "Firstname";
                    dt.Columns["Lastname"].ColumnName = "Lastname";
                    dt.Columns["EDPCode"].ColumnName = "EDP Code";
                    dt.Columns["Title"].ColumnName = "Title";
                    dt.Columns["SubjectCode"].ColumnName = "Subject Code";
                    dt.Columns["Schedule"].ColumnName = "Schedule";
                    dt.Columns["StartTime"].ColumnName = "Start Time";
                    dt.Columns["EndTime"].ColumnName = "End Time";
                    dt.Columns["Course"].ColumnName = "Course";
                    dt.Columns["Year"].ColumnName = "Year";

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

        private void DisplayInstructorEnrolledStudents()
        {
            try
            {
                if (connectionString.State != ConnectionState.Closed)
                {
                    connectionString.Close(); // Ensure the connection is closed before opening it
                }
                connectionString.Open();

                string subjectQuery = "SELECT DISTINCT s.SubjectCode FROM InstructorSubjectEnrollment ise " +
                                      "INNER JOIN Users u ON ise.UserId = u.UserId " +
                                      "INNER JOIN Subjects s ON ise.EDPCode = s.EDPCode " +
                                      "WHERE u.FirstName = @FirstName AND u.LastName = @LastName";

                SqlCommand subjectCommand = new SqlCommand(subjectQuery, connectionString);
                subjectCommand.Parameters.AddWithValue("@FirstName", FirstName);
                subjectCommand.Parameters.AddWithValue("@LastName", LastName);

                DataTable subjectTable = new DataTable();
                SqlDataAdapter subjectAdapter = new SqlDataAdapter(subjectCommand);
                subjectAdapter.Fill(subjectTable);

                DataTable studentTable = new DataTable();

                foreach (DataRow subjectRow in subjectTable.Rows)
                {
                    string subjectCode = subjectRow["SubjectCode"].ToString();

                    string studentQuery = "SELECT se.EnrollmentID, se.IDNumber, sa.Firstname, sa.Lastname, " +
                                          "se.EDPCode, s.Title, s.SubjectCode, s.Schedule, s.StartTime, s.EndTime, " +
                                          "sa.Course, sa.Year " +
                                          "FROM subjectEnrollment se " +
                                          "INNER JOIN StudentsAccounts sa ON se.IDNumber = sa.IDNumber " +
                                          "INNER JOIN Subjects s ON se.EDPCode = s.EDPCode " +
                                          "WHERE s.SubjectCode = @SubjectCode";

                    SqlCommand studentCommand = new SqlCommand(studentQuery, connectionString);
                    studentCommand.Parameters.AddWithValue("@SubjectCode", subjectCode);

                    DataTable tempTable = new DataTable();
                    SqlDataAdapter studentAdapter = new SqlDataAdapter(studentCommand);
                    studentAdapter.Fill(tempTable);

                    if (studentTable.Columns.Count == 0)
                    {
                        studentTable = tempTable.Clone();
                    }

                    foreach (DataRow row in tempTable.Rows)
                    {
                        studentTable.ImportRow(row);
                    }
                }

                // Remove the EnrollmentID column from the DataTable
                studentTable.Columns.Remove("EnrollmentID");

                // Rename columns for display
                studentTable.Columns["IDNumber"].ColumnName = "ID Number";
                studentTable.Columns["Firstname"].ColumnName = "Firstname";
                studentTable.Columns["Lastname"].ColumnName = "Lastname";
                studentTable.Columns["EDPCode"].ColumnName = "EDP Code";
                studentTable.Columns["Title"].ColumnName = "Title";
                studentTable.Columns["SubjectCode"].ColumnName = "Subject Code";
                studentTable.Columns["Schedule"].ColumnName = "Schedule";
                studentTable.Columns["StartTime"].ColumnName = "Start Time";
                studentTable.Columns["EndTime"].ColumnName = "End Time";
                studentTable.Columns["Course"].ColumnName = "Course";
                studentTable.Columns["Year"].ColumnName = "Year";

                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = studentTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connectionString.Close();
            }
        }

        //LISTS OF REGISTERED STUDENTS
        private void DisplaySubject()
        {
            if (UserRole == "superadmin")
            {
                DisplayAllSubjects();
            }
            else if (UserRole == "instructor")
            {
                DisplayInstructorSubjects();
            }
        }

        private void DisplayAllSubjects()
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
        private void DisplayInstructorSubjects()
        {
            try
            {
                // Ensure the connection is closed before attempting to open it
                if (connectionString.State != ConnectionState.Closed)
                {
                    connectionString.Close();
                }

                connectionString.Open();

                string query = "SELECT s.EDPCode, s.Title, s.SubjectCode, s.Units, s.Schedule, s.StartTime, s.EndTime, s.Course FROM InstructorSubjectEnrollment ise " +
                               "INNER JOIN Users u ON ise.UserId = u.UserId " +
                               "INNER JOIN Subjects s ON ise.EDPCode = s.EDPCode " +
                               "WHERE u.FirstName = @FirstName AND u.LastName = @LastName";

                using (SqlCommand cmd = new SqlCommand(query, connectionString))
                {
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Modify column name in the DataTable
                    dt.Columns["EDPCode"].ColumnName = "EDP Code";
                    dt.Columns["Title"].ColumnName = "Title";
                    dt.Columns["SubjectCode"].ColumnName = "Subject Code";
                    dt.Columns["Schedule"].ColumnName = "Schedule";
                    dt.Columns["StartTime"].ColumnName = "Start Time";
                    dt.Columns["EndTime"].ColumnName = "End Time";

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


        //LISTS OF REGISTERED STUDENTS
        private void DisplayStudents()
        {
            try
            {
                // Ensure the connection is closed before attempting to open it
                if (connectionString.State != ConnectionState.Closed)
                {
                    connectionString.Close();
                }

                connectionString.Open();

                using (SqlCommand cmd = new SqlCommand("GetStudentList", connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                                        
                    // Modify column name in the DataTable
                    dt.Columns["IDNumber"].ColumnName = "ID Number";
                    dt.Columns["Firstname"].ColumnName = "Firstname";
                    dt.Columns["Lastname"].ColumnName = "Lastname";
                    dt.Columns["Gender"].ColumnName = "Gender";
                    dt.Columns["Course"].ColumnName = "Course";
                    dt.Columns["Year"].ColumnName = "Year";
                    dt.Columns["SchoolYear"].ColumnName = "School Year";
                    dt.Columns["Semester"].ColumnName = "Semester";

                    // Remove the EnrollmentID column from the DataTable
                    dt.Columns.Remove("Photo");

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
                if (connectionString.State != ConnectionState.Closed)
                {
                    connectionString.Close(); // Ensure the connection is closed before opening it
                }

                connectionString.Open();

                using (SqlCommand cmd = new SqlCommand("ManageSubjectEnrollment", connectionString))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IDNumber", IDNumber);
                    cmd.Parameters.AddWithValue("@EDPCode", EDPCode);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                    cmd.Parameters.AddWithValue("@SubjectCode", txtSubjectCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@Schedule", txtSchedule.Text.Trim());
                    cmd.Parameters.AddWithValue("@StartTime", txtStartTime.Text.Trim());
                    cmd.Parameters.AddWithValue("@EndTime", txtEndTime.Text.Trim());

                    cmd.Parameters.AddWithValue("@Action", "Enroll"); // Specify the action as Enroll


                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Student enrolled successfully in the subject.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Call appropriate display method based on user role
                    if (UserRole == "superadmin")
                    {
                        DisplayAllEnrolledStudents();
                    }
                    else if (UserRole == "instructor")
                    {
                        DisplayInstructorEnrolledStudents();
                    }


                    clearFields();
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error student enrollment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    using (SqlCommand cmd = new SqlCommand("dbo.ManageSubjectEnrollment", connectionString))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Pass extracted values as parameters
                        cmd.Parameters.AddWithValue("@IDNumber", IDNumber);
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
                        // Call appropriate display method based on user role
                        if (UserRole == "superadmin")
                        {
                            DisplayAllEnrolledStudents();
                        }
                        else if (UserRole == "instructor")
                        {
                            DisplayInstructorEnrolledStudents();
                        }

                        clearFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error student disenrollment: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectionString.Close();
                }
            }
        }


        public void clearFields()
        {
            txtIDNumber.Clear();
            txtEDPCode.Clear();
            txtStudentSearch.Clear();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {

                //Himoanan ug if condition. 
                // Get the selected row
                DataGridViewRow row = dataGridView2.SelectedRows[0];  // Assuming single selection


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

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow row = dataGridView3.SelectedRows[0];  // Assuming single selection

                string IDNumber = row.Cells[0].Value.ToString();

                txtIDNumber.Text = IDNumber;
            }
            else
            {
                MessageBox.Show("Invalid EDP Code format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Searching for students
        private void txtStudentSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {            

                if (txtSubjectEnrolled.Text.Trim() == "") // Check if search field is empty
                {
                    string defaultQuery = "SELECT * FROM StudentsAccounts";  // Replace with your default student selection logic

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


                string sql = "SearchStudent";
                using (SqlCommand cmd = new SqlCommand(sql, connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@searchValue", txtSubjectEnrolled.Text.Trim());

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No student found matching your search criteria.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Display the default student list after showing the message
                            string defaultQuery = "SELECT * FROM StudentsAccounts"; // Replace with your default student selection logic
                            using (SqlCommand cmd2 = new SqlCommand(defaultQuery, connectionString)) // Create a new command for the default query
                            {
                                using (SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2))
                                {
                                    DataTable dt2 = new DataTable();
                                    adapter2.Fill(dt2);
                                    dataGridView3.DataSource = dt2;
                                }
                            }
                            txtSubjectEnrolled.Clear();
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


        //Searching for subjects
        private void txtSubjectSearch_TextChanged(object sender, EventArgs e)
        {
            if (UserRole == "superadmin")
            {
                searchAllSubjects();
            }
            else if (UserRole == "instructor")
            {
                searchInstructorSubjects();
            }
        }
        private void searchAllSubjects()
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
                            dataGridView2.DataSource = dt;
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
                                    dataGridView2.DataSource = dt2;
                                }
                            }
                            txtSubjectSearch.Clear();
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
        private void searchInstructorSubjects()
        {
            string searchValue = txtSubjectSearch.Text.Trim();

            try
            {
                connectionString.Open();

                // SQL command to fetch SubjectCode values for the logged-in user
                string query = "SELECT s.EDPCode, u.FirstName, u.LastName, s.Title, s.SubjectCode, " +
                               "s.Units, s.Schedule, s.StartTime, s.EndTime, s.Course " +
                               "FROM InstructorSubjectEnrollment ise " +
                               "INNER JOIN Users u ON ise.UserId = u.UserId " +
                               "INNER JOIN Subjects s ON ise.EDPCode = s.EDPCode " +
                               "WHERE (u.FirstName = @FirstName AND u.LastName = @LastName) " +
                               "AND (s.EDPCode LIKE '%' + @searchValue + '%' OR s.SubjectCode LIKE '%' + @searchValue + '%' OR s.Title LIKE '%' + @searchValue + '%')";


                SqlCommand cmd = new SqlCommand(query, connectionString);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@FirstName", FirstName); // Assuming FirstName and LastName are accessible here
                cmd.Parameters.AddWithValue("@LastName", LastName);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No matching records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dataGridView2.DataSource = dt; // Set the DataGridView data source if results exist
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

        //Searching for Enrolled Students
        private void txtStudentSearch_TextChanged_1(object sender, EventArgs e)
        {
            if (UserRole == "superadmin")
            {
                searchAllEnrolledStudents();
            }
            else if (UserRole == "instructor")
            {
                searchInstructorEnrolledStudent();
            }
        }
        private void searchAllEnrolledStudents()
        {
            string searchValue = txtStudentSearch.Text.Trim();

            try
            {

                connectionString.Open();

                using (SqlCommand cmd = new SqlCommand("SearchEnrolledStudents", connectionString))
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
        private void searchInstructorEnrolledStudent()
        {
            string searchValue = txtStudentSearch.Text.Trim();

            try
            {
                connectionString.Open();

                // Define the SQL query dynamically based on the instructor's subject
                string query = "SELECT se.IDNumber, sa.Firstname, sa.Lastname, " +
                               "se.EDPCode, s.Title, s.SubjectCode, s.Schedule, s.StartTime, s.EndTime, " +
                               "sa.Course, sa.Year " +
                               "FROM subjectEnrollment se " +
                               "INNER JOIN StudentsAccounts sa ON se.IDNumber = sa.IDNumber " +
                               "INNER JOIN Subjects s ON se.EDPCode = s.EDPCode " +
                               "INNER JOIN InstructorSubjectEnrollment ise ON s.EDPCode = ise.EDPCode " +
                               "INNER JOIN Users u ON ise.UserId = u.UserId " +
                               "WHERE (sa.Firstname LIKE '%' + @searchValue + '%' OR sa.Lastname LIKE '%' + @searchValue + '%' OR CAST(se.IDNumber AS NVARCHAR(50)) LIKE '%' + @searchValue + '%')" +
                               "AND u.FirstName = @FirstName AND u.LastName = @LastName";

                SqlCommand cmd = new SqlCommand(query, connectionString);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);

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
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectionString.Close();
            }
        }

    }
}
