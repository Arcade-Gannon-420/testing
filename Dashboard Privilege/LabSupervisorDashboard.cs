using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace testing.Dashboard_Privilege.Lab_Supervisor_Dashboard
{
    public partial class SupervisorDashboard : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = .\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True");
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; } // Corrected property name
        public SupervisorDashboard(string firstName, string lastName, string userRole)
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

                // SQL command to fetch subject titles, start time, end time, and enrollment count for the logged-in user
                string query = @"
                    SELECT 
                        u.Firstname AS [First Name],
                        u.Lastname AS [Last Name],
                        s.Title AS Subjects,
                        se.StartTime AS [Start Time],
                        se.EndTime AS [End Time],
                        COUNT(se.IDNumber) AS [Students Enrolled]
                    FROM 
                        InstructorSubjectEnrollment ise
                    INNER JOIN 
                        Users u ON ise.UserId = u.UserId
                    INNER JOIN 
                        Subjects s ON ise.EDPCode = s.EDPCode
                    INNER JOIN 
                        subjectEnrollment se ON ise.EDPCode = se.EDPCode
                    WHERE 
                        CHARINDEX(
                            CASE DATENAME(WEEKDAY, GETDATE())
                                WHEN 'Monday' THEN 'M'
                                WHEN 'Tuesday' THEN 'T'
                                WHEN 'Wednesday' THEN 'W'
                                WHEN 'Thursday' THEN 'Th'
                                WHEN 'Friday' THEN 'F'
                                WHEN 'Saturday' THEN 'S'
                            END, 
                            se.Schedule) > 0
                    GROUP BY 
                        u.Firstname, u.Lastname, s.Title, se.StartTime, se.EndTime
                    ORDER BY 
                        se.StartTime";

                // Create a SqlCommand object
                SqlCommand command = new SqlCommand(query, connectionString);

                // Execute the command and load results into a DataTable
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);

                guna2DataGridView1.DataSource = dt;               

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

        private void LoadStudents()
        {
            try
            {
                connectionString.Open();

                // Query to fetch the total number of students enrolled today
                string totalStudentsQuery = @"
                    SELECT COUNT(*) AS TotalStudentsEnrolled
                    FROM subjectEnrollment se
                    WHERE CHARINDEX(
                        CASE DATENAME(WEEKDAY, GETDATE())
                            WHEN 'Monday' THEN 'M'
                            WHEN 'Tuesday' THEN 'T'
                            WHEN 'Wednesday' THEN 'W'
                            WHEN 'Thursday' THEN 'Th'
                            WHEN 'Friday' THEN 'F'
                            WHEN 'Saturday' THEN 'S'
                        END, 
                        se.Schedule) > 0";

                SqlCommand totalCommand = new SqlCommand(totalStudentsQuery, connectionString);
                int totalStudentsEnrolled = (int)totalCommand.ExecuteScalar();

                // Update the studentCount label
                studentCount.Text = $"{totalStudentsEnrolled}";
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
        private void LoadInstructors()
        {
            try
            {
                connectionString.Open();

                string totalInstructorsQuery = @"
                    SELECT COUNT(DISTINCT u.Firstname + ' ' + u.Lastname) AS TotalInstructors
                    FROM InstructorSubjectEnrollment ise
                    INNER JOIN Users u ON ise.UserId = u.UserId
                    INNER JOIN subjectEnrollment se ON ise.EDPCode = se.EDPCode
                    WHERE CHARINDEX(
                        CASE DATENAME(WEEKDAY, GETDATE())
                            WHEN 'Monday' THEN 'M'
                            WHEN 'Tuesday' THEN 'T'
                            WHEN 'Wednesday' THEN 'W'
                            WHEN 'Thursday' THEN 'Th'
                            WHEN 'Friday' THEN 'F'
                            WHEN 'Saturday' THEN 'S'
                        END, 
                        se.Schedule) > 0";

                SqlCommand totalCommand = new SqlCommand(totalInstructorsQuery, connectionString);
                int totalInstructors = (int)totalCommand.ExecuteScalar();

                instructorCount.Text = $"{totalInstructors}";
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

        private void LabSupervisorDashboard_Load(object sender, EventArgs e)
        {
            LoadSubjects();
            LoadStudents();
            LoadInstructors();
        }       
    }
}

            