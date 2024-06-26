using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing
{
    public partial class OverrideAttendances : UserControl
    {
        private readonly string connectionString = "Data Source = .\\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True";

        public OverrideAttendances()
        {
            InitializeComponent();
        }

        // Call this method to print the column names for verification
     
        //--------------------------------------------------------------------------------------
        //TASKS WAITING TO BE EXECUTE

        private async Task<DataTable> GetAttendanceDataAsync(string query, SqlParameter[] parameters)
        {
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddRange(parameters);
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                await con.OpenAsync();
                da.Fill(dt);
                return dt;
            }
        }

        private async Task ExecuteNonQueryAsync(string query, SqlParameter[] parameters)
        {
            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddRange(parameters);
                await con.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private async Task SearchRecordsByDateAsync(DateTime selectedDate)
        {
            string query = "SELECT AttendanceID, IDNumber, Firstname, Lastname, SubjectCode, Schedule, Date, " +
                           "CONVERT(varchar(15), CONVERT(time, TimeIn), 100) AS TimeIn, " +
                           "CONVERT(varchar(15), CONVERT(time, TimeOut), 100) AS TimeOut, " +
                           "enrollmentStatus " +
                           "FROM Attendance " +
                           "WHERE EDPCode = @EDPCode AND CONVERT(date, Date) = @Date";

            var parameters = new[]
            {
                new SqlParameter("@EDPCode", SubjectCode.Text),
                new SqlParameter("@Date", selectedDate.Date)
            };

            DataTable dt = await GetAttendanceDataAsync(query, parameters);

            if (dt.Rows.Count == 0)
            {
                dataGridView1.DataSource = null;
                MessageBox.Show("No attendance data found for the specified criteria.", "Attendance Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dataGridView1.DataSource = dt;
            }
        }

        private async Task LoadAttendanceDataAsync(DateTime selectedDate)
        {
            string query = "SELECT AttendanceID, IDNumber, Firstname, Lastname, SubjectCode, Schedule, Date, " +
                          "CONVERT(varchar(15), CONVERT(time, TimeIn), 100) AS TimeIn, " +
                          "CONVERT(varchar(15), CONVERT(time, TimeOut), 100) AS TimeOut, " +
                          "enrollmentStatus " +
                          "FROM Attendance " +
                          "WHERE EDPCode = @EDPCode AND CONVERT(date, Date) = @Date";

            var parameters = new[]
            {
                new SqlParameter("@EDPCode", SubjectCode.Text),
                new SqlParameter("@Date", selectedDate.Date)
            };
             
            DataTable dt = await GetAttendanceDataAsync(query, parameters);
            dataGridView1.DataSource = dt;
        }


        //--------------------------------------------------------------------------------------
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtAttendanceID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                subStartTime.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                subEndTime.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                Status.SelectedItem = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();

            }
        }
        private async void AttendaceSummary_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SubjectCode.Text))
            {
                DateTime selectedDate = Start.Value;
                await SearchRecordsByDateAsync(selectedDate);
            }
            else
            {
                MessageBox.Show("Please put an EDP Code", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {               
                string query = "UPDATE Attendance SET TimeIn=@TimeIn, TimeOut=@TimeOut, enrollmentStatus=@enrollmentStatus WHERE AttendanceID = @AttendanceID";
                var parameters = new[]
                {

                    new SqlParameter("@TimeIn", subStartTime.Value.ToString("hh:mm:ss tt")),
                    new SqlParameter("@TimeOut", subEndTime.Value.ToString("HH:mm:ss tt")),
                    new SqlParameter("@enrollmentStatus", Status.SelectedItem.ToString()),
                    new SqlParameter("@AttendanceID", int.Parse(txtAttendanceID.Text.Trim()))
                };

                try
                {
                    await ExecuteNonQueryAsync(query, parameters);
                    MessageBox.Show("Update Successful", "Successful", MessageBoxButtons.OK);


                    DateTime selectedDate = Start.Value;
                    await LoadAttendanceDataAsync(selectedDate); // Reload data after update
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error executing update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                string inTime = dataGridView1.SelectedRows[0].Cells["InTime"].Value.ToString();

                string query = "DELETE FROM Attendance WHERE TimeIn = @TimeIn";
                var parameters = new[]
                {
                    new SqlParameter("@TimeIn", inTime)
                };

                await ExecuteNonQueryAsync(query, parameters);
                dataGridView1.Rows.RemoveAt(selectedRowIndex);
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnAbsent_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string query = "UPDATE Attendance SET TimeIn=@TimeIn, TimeOut=@TimeOut, enrollmentStatus=@enrollmentStatus WHERE AttendanceID = @AttendanceID";
                var parameters = new[]
                {

                    new SqlParameter("@TimeIn", DBNull.Value),
                    new SqlParameter("@TimeOut", DBNull.Value),
                    new SqlParameter("@enrollmentStatus", "Absent"),
                    new SqlParameter("@AttendanceID", int.Parse(txtAttendanceID.Text.Trim()))
                };

                try
                {
                    await ExecuteNonQueryAsync(query, parameters);
                    MessageBox.Show("Update Successful", "Successful", MessageBoxButtons.OK);


                    DateTime selectedDate = Start.Value;
                    await LoadAttendanceDataAsync(selectedDate); // Reload data after update
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error executing update: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------------------------------------------
        private void OverrideAttendances_Load(object sender, EventArgs e)
        {
            DateTime selectedDate = Start.Value;
            Task task = LoadAttendanceDataAsync(selectedDate);
        }       
    }
}
