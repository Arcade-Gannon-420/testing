using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace testing
{
    class studentData
    {
        public int IDNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string Course { get; set; }
        public string Year { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public byte[] Photo { get; set; } // Change from string to byte[]

        public List<studentData> GetStudentData()
        {
            List<studentData> listdata = new List<studentData>();

            string connectionString = @"Data Source = DESKTOP-SLBI6LR\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectData = "SELECT * FROM StudentsAccounts";

                    using (SqlCommand cmd = new SqlCommand(selectData, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                studentData sd = new studentData
                                {
                                    IDNumber = (int)reader["IDNumber"],
                                    Firstname = reader["Firstname"].ToString(),
                                    Lastname = reader["Lastname"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    Course = reader["Course"].ToString(),
                                    Year = reader["Year"].ToString(),
                                    SchoolYear = reader["SchoolYear"].ToString(),
                                    Semester = reader["Semester"].ToString(),
                                    Photo = (byte[])reader["Photo"] // Cast to byte[]
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
