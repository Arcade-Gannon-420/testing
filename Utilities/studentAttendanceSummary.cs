using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace testing.student_Attendance_Summary
{
    public partial class studentAttendanceSummary : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-SLBI6LR\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True");

        public studentAttendanceSummary()
        {
            InitializeComponent();

        }
        private void studentAttendanceSummary_Load(object sender, EventArgs e)
        {
            displayattendanceSummaryData();
        }
        public void displayattendanceSummaryData()
        {
            connectionString.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT IDNumber, Firstname, Lastname, Title, SubjectCode, Schedule, Date, TimeIn, TimeOut, enrollmentStatus  FROM Attendance", connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet dt = new DataSet();
            adapter.Fill(dt);
            connectionString.Close();


            // Format TimeIn and TimeOut columns to 12-hour format with AM/PM
            DataTable table = dt.Tables[0];
            table.Columns.Add("FormattedTimeIn", typeof(string));
            table.Columns.Add("FormattedTimeOut", typeof(string));

            foreach (DataRow row in table.Rows)
            {
                if (row["TimeIn"] != DBNull.Value)
                {
                    // Parse TimeIn as TimeSpan
                    if (TimeSpan.TryParse(row["TimeIn"].ToString(), out TimeSpan timeIn))
                    {
                        // Convert TimeSpan to DateTime for formatting
                        DateTime dateTimeIn = DateTime.Today.Add(timeIn);
                        // Format to 12-hour time with AM/PM
                        row["FormattedTimeIn"] = dateTimeIn.ToString("hh:mm:ss tt");
                    }
                    else
                    {
                        row["FormattedTimeIn"] = string.Empty;
                    }
                }
                else
                {
                    row["FormattedTimeIn"] = string.Empty;
                }

                if (row["TimeOut"] != DBNull.Value)
                {
                    // Parse TimeOut as TimeSpan
                    if (TimeSpan.TryParse(row["TimeOut"].ToString(), out TimeSpan timeOut))
                    {
                        // Convert TimeSpan to DateTime for formatting
                        DateTime dateTimeOut = DateTime.Today.Add(timeOut);
                        // Format to 12-hour time with AM/PM
                        row["FormattedTimeOut"] = dateTimeOut.ToString("hh:mm:ss tt");
                    }
                    else
                    {
                        row["FormattedTimeOut"] = string.Empty;
                    }
                }
                else
                {
                    row["FormattedTimeOut"] = string.Empty;
                }
            }

            // Remove the original TimeIn and TimeOut columns
            table.Columns.Remove("TimeIn");
            table.Columns.Remove("TimeOut");

            // Rename the formatted columns to TimeIn and TimeOut
            table.Columns["FormattedTimeIn"].ColumnName = "TimeIn";
            table.Columns["FormattedTimeOut"].ColumnName = "TimeOut";

            // Sort the DataTable by 'Date' and 'TimeIn' columns in descending order
            DataView view = table.DefaultView;
            view.Sort = "Date DESC, TimeIn DESC";
            DataTable sortedTable = view.ToTable();

            // Bind the sorted and formatted DataTable to the DataGridView
            dataGridView1.DataSource = sortedTable;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
