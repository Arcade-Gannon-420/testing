using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing
{
    internal class attendanceData
    {
        public int AttendanceID { get; set; }
        public int IDNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int EDPCode { get; set; }
        public string Title { get; set; }
        public string SubjectCode { get; set; }
        public string Schedule { get; set; }
        public string Date { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public string enrollmentStatus { get; set; }


        public List<attendanceData> GetattendanceData()
        {
            List<attendanceData> listdata = new List<attendanceData>();

            string connectionString = @"Data Source = DESKTOP-SLBI6LR\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectData = "SELECT * FROM Attendance";

                    using (SqlCommand cmd = new SqlCommand(selectData, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                attendanceData ud = new attendanceData
                                {
                                    IDNumber = (int)reader["IDNumber"],
                                    Firstname = reader["Firstname"].ToString(),
                                    Lastname = reader["Lastname"].ToString(),
                                    EDPCode = (int)reader["EDPCode"],
                                    Title = reader["Title"].ToString(),
                                    SubjectCode = reader["SubjectCode"].ToString(),
                                    Schedule = reader["Schedule"].ToString(),
                                    Date = reader["Date"].ToString(),
                                    TimeIn = reader["TimeIn"].ToString(),
                                    TimeOut = reader["TimeOut"].ToString(),
                                    enrollmentStatus = reader["enrollmentStatus"].ToString()
                                };
                                listdata.Add(ud);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return listdata;
        }
    }
}
