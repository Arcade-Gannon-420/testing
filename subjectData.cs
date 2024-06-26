using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace testing
{
    internal class subjectData
    {
        public int EDPCode { get; set; }
        public string Title { get; set; }
        public string SubjectCode { get; set; }
        public int Units { get; set; }
        public string Schedule { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Course { get; set; }


        public List<subjectData> GetsubjectData()
        {
            List<subjectData> listdata = new List<subjectData>();

            string connectionString = @"Data Source = DESKTOP-SLBI6LR\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectData = "SELECT * FROM Subjects";

                    using (SqlCommand cmd = new SqlCommand(selectData, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                subjectData sd = new subjectData
                                {
                                    EDPCode = (int)reader["EDPCode"],
                                    Title = reader["Title"].ToString(),
                                    SubjectCode = reader["SubjectCode"].ToString(),
                                    Units = (int)reader["Units"],
                                    Schedule = reader["Schedule"].ToString(),
                                    StartTime  = reader["StartTime"].ToString(),
                                    EndTime = reader["EndTime"].ToString(),
                                    Course = reader["Course"].ToString()
                                };

                                listdata.Add(sd);
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
