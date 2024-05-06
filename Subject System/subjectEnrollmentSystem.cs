using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing.Subject_System
{
    public partial class subjectEnrollmentSystem : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-SLBI6LR\SQLEXPRESS;Initial Catalog = FinalDb;Integrated Security=True");

        public subjectEnrollmentSystem()
        {
            InitializeComponent();
            DisplayEnrolledStudents();
            displaySubjectData();
            displayStudentData();
        }

        public void RefreshData()
        {
            if (dataGridView2.InvokeRequired)
            {
                dataGridView2.Invoke((MethodInvoker)delegate { RefreshData(); });
                return;
            }
            displaySubjectData();
        }
        public void displaySubjectData()
        {
            subjectData sd = new subjectData();
            List<subjectData> listData = sd.GetsubjectData();

            dataGridView2.DataSource = listData;
        }

        public void displayStudentData()
        {
            studentData sd = new studentData();
            List<studentData> listData = sd.GetStudentData();

            dataGridView3.DataSource = listData;
        }

        private void DisplayEnrolledStudents()
        {
            try
            {
                // Ensure the connection is closed before attempting to open it
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

                    // Clear existing columns
                    dataGridView1.Columns.Clear();

                    // Bind the DataGridView to the DataTable
                    dataGridView1.DataSource = dt;

                    // Customize column headers
                    dataGridView1.Columns["IDNumber"].HeaderText = "ID Number"; // Customize the IDNumber column header
                    dataGridView1.Columns["EDPCode"].HeaderText = "EDP Code"; // Customize the EDPCode column header
                    dataGridView1.Columns["Title"].HeaderText = "Title"; // Customize the Title column header

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

                    DisplayEnrolledStudents();
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
                    // Ensure the connection is closed before attempting to open it
                    if (connectionString.State != ConnectionState.Closed)
                    {
                        connectionString.Close();
                    }

                    connectionString.Open();
                    using (SqlCommand cmd = new SqlCommand("dbo.ManageSubjectEnrollment", connectionString))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IDNumber", txtIDNumber.Text.Trim());
                        cmd.Parameters.AddWithValue("@EDPCode", txtEDPCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Action", "Disenroll");
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account disenrolled successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh the DataGridView to reflect the changes
                        DisplayEnrolledStudents();
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

        public void clearFields()
        {
            txtIDNumber.Clear();
            txtEDPCode.Clear();
            txtSearch.Clear();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];  // Assuming single selection

                string IDNumber = row.Cells[1].Value.ToString();
                string EDPCode = row.Cells[2].Value.ToString();


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
                // Get the selected row
                DataGridViewRow row = dataGridView2.SelectedRows[0];  // Assuming single selection

                
                string EDPCode = row.Cells[0].Value.ToString();
                string Title = row.Cells[1].Value.ToString();
                string SubjectCode = row.Cells[2].Value.ToString();
                string Schedule = row.Cells[4].Value.ToString();
                string StartTime = row.Cells[5].Value.ToString();
                string EndTime = row.Cells[6].Value.ToString();

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




        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            try
            {
                connectionString.Open();

                using (SqlCommand cmd = new SqlCommand("SearchEnrolledStudents", connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Keyword", keyword);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No matching records found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txtStudentSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Clear any existing data in the DataGridView



                if (txtStudentSearch.Text.Trim() == "") // Check if search field is empty
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

                    cmd.Parameters.AddWithValue("@searchValue", txtStudentSearch.Text.Trim());

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
                            txtStudentSearch.Clear();
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

    }
}
