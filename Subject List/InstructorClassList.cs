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
using testing;


namespace testing.Utilities
{
    public partial class InstructorClassList : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-SLBI6LR\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True");

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; } // Corrected property name
        public InstructorClassList(string firstName, string lastName, string userRole)
        {
            InitializeComponent();
            SetUserInfo(firstName, lastName, userRole);
        }

        private void SetUserInfo(string firstName, string lastName, string userRole)
        {
            FirstName = firstName;
            LastName = lastName;
            UserRole = userRole; // Corrected assignment to class property
            // Display the information in the control
        }

        private void LoadSubjects()
        {
            try
            {
                // Open the connection
                connectionString.Open();

                // SQL command to fetch SubjectCode values for the logged-in user
                string query = "SELECT DISTINCT s.SubjectCode FROM InstructorSubjectEnrollment ise " +
                               "INNER JOIN Users u ON ise.UserId = u.UserId " +
                               "INNER JOIN Subjects s ON ise.EDPCode = s.EDPCode " +
                               "WHERE u.FirstName = @FirstName AND u.LastName = @LastName";

                // Create a SqlCommand object
                SqlCommand command = new SqlCommand(query, connectionString);
                command.Parameters.AddWithValue("@FirstName", FirstName); // Add parameter for first name
                command.Parameters.AddWithValue("@LastName", LastName);   // Add parameter for last name

                // Execute the command and load results into a DataTable
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);

                // Clear existing items in the combo box
                cmbSubjects.Items.Clear();

                // Add fetched SubjectCode values to the combo box
                foreach (DataRow row in dt.Rows)
                {
                    cmbSubjects.Items.Add(row["SubjectCode"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection
                connectionString.Close();
            }
        }

        private void InstructorClassList_Load(object sender, EventArgs e)
        {
            LoadSubjects();
        }

        private void cmbSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            string searchValue = cmbSubjects.Text.Trim();

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
                        DataView dv = dt.DefaultView;
                        dv.Sort = "Lastname ASC"; // Sort by Lastname and then Firstname
                        dataGridView1.DataSource = dv.ToTable(); // Set the DataGridView data source if results exist
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

       
    }
}
