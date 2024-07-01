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

namespace testing.Dashboard_Privilege.Instructor_Dashboard
{
    public partial class InstructorDashboard : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = .\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True");
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; } // Corrected property name

        private int currentSubjectIndex = -1; // Initialize to -1 to indicate no subject is selected yet.

        public InstructorDashboard(string firstName, string lastName, string userRole)
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

        private void InstructorDashboard_Load(object sender, EventArgs e)
        {
            LoadSubjects();
        }

        //--------------------------------
        //MAIN FUNCTION

        private void LoadSubjects()
        {
            try
            {
                // Open the connection
                connectionString.Open();

                // SQL command to fetch subject titles, start time, end time, and enrollment count for the logged-in user
                string query = @"
                    SELECT 
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
                        u.FirstName = @FirstName 
                        AND u.LastName = @LastName
                        AND CHARINDEX(
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
                        s.Title, se.StartTime, se.EndTime
                    ORDER BY 
                        se.StartTime";

                // Create a SqlCommand object
                SqlCommand command = new SqlCommand(query, connectionString);
                command.Parameters.AddWithValue("@FirstName", FirstName); // Add parameter for first name
                command.Parameters.AddWithValue("@LastName", LastName);   // Add parameter for last name


                // Execute the command and load results into a DataTable
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);

                subjectsEnrolledDATAGRID.DataSource = dt;


                // Initialize the current index and update the label
                currentSubjectIndex = 0;
                UpdateSubjectNameLabel();

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

        private void UpdateAttendanceCounts(string subjectTitle)
        {
            try
            {
                if (connectionString.State != ConnectionState.Closed)
                {
                    connectionString.Close();
                }

                connectionString.Open();

                string query = @"
            SELECT 
                SUM(CASE WHEN a.enrollmentStatus = 'Present' THEN 1 ELSE 0 END) AS PresentCount,
                SUM(CASE WHEN a.enrollmentStatus = 'Late' THEN 1 ELSE 0 END) AS LateCount,
                SUM(CASE WHEN a.enrollmentStatus = 'Absent' THEN 1 ELSE 0 END) AS AbsentCount
            FROM 
                Attendance a
            WHERE 
                a.Title = @SubjectTitle
                AND CHARINDEX(
                    CASE DATENAME(WEEKDAY, GETDATE())
                        WHEN 'Monday' THEN 'M'
                        WHEN 'Tuesday' THEN 'T'
                        WHEN 'Wednesday' THEN 'W'
                        WHEN 'Thursday' THEN 'Th'
                        WHEN 'Friday' THEN 'F'
                        WHEN 'Saturday' THEN 'S'
                    END, 
                    a.Schedule) > 0
                AND a.Date = CAST(GETDATE() AS DATE)";

                SqlCommand command = new SqlCommand(query, connectionString);
                command.Parameters.AddWithValue("@SubjectTitle", subjectTitle);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int present = reader["PresentCount"] != DBNull.Value ? Convert.ToInt32(reader["PresentCount"]) : 0;
                    int late = reader["LateCount"] != DBNull.Value ? Convert.ToInt32(reader["LateCount"]) : 0;
                    int absent = reader["AbsentCount"] != DBNull.Value ? Convert.ToInt32(reader["AbsentCount"]) : 0;

                    int total = present + late + absent;

                    presentCircleProgressBar.Maximum = total;
                    presentCircleProgressBar.Value = present;
                    presentCircleProgressBar.Text = $"{present}";

                    lateCircleProgressBar.Maximum = total;
                    lateCircleProgressBar.Value = late;
                    lateCircleProgressBar.Text = $"{late}";

                    absentCircleProgressBar.Maximum = total;
                    absentCircleProgressBar.Value = absent;
                    absentCircleProgressBar.Text = $"{absent}";
                }
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


        private void UpdateSubjectNameLabel()
        {
            if (subjectsEnrolledDATAGRID.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                if (currentSubjectIndex >= 0 && currentSubjectIndex < dt.Rows.Count)
                {
                    string subjectTitle = dt.Rows[currentSubjectIndex]["Subjects"].ToString();
                    subjectNameLabel.Text = subjectTitle;

                    int enrolled = Convert.ToInt32(dt.Rows[currentSubjectIndex]["Students Enrolled"]);

                    UpdateAttendanceCounts(subjectTitle);
                }
            }
        }


        private void leftSubjectBtn_Click(object sender, EventArgs e)
        {
            if (subjectsEnrolledDATAGRID.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                // Decrement the index to move to the previous subject
                currentSubjectIndex--;

                // Ensure the index is within bounds
                if (currentSubjectIndex < 0)
                {
                    currentSubjectIndex = dt.Rows.Count - 1; // Wrap around to the last subject
                }

                UpdateSubjectNameLabel();
            }



        }

        private void rightSubjectBtn_Click(object sender, EventArgs e)
        {
            if (subjectsEnrolledDATAGRID.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                // Increment the index to move to the next subject
                currentSubjectIndex++;

                // Ensure the index is within bounds
                if (currentSubjectIndex >= dt.Rows.Count)
                {
                    currentSubjectIndex = 0; // Wrap around to the first subject
                }

                UpdateSubjectNameLabel();
            }
        }

    }
}
