using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing
{
    internal class usersData
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Privilege { get; set; }

        public List<usersData> GetusersData()
        {
            List<usersData> listdata = new List<usersData>();

            string connectionString = @"Data Source = DESKTOP-SLBI6LR\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectData = "SELECT * FROM Users";

                    using (SqlCommand cmd = new SqlCommand(selectData, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usersData ud = new usersData
                                {
                                    UserId = (int)reader["UserId"],
                                    Username = reader["Username"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    Firstname = reader["Firstname"].ToString(),
                                    Lastname = reader["Lastname"].ToString(),
                                    Privilege = reader["Privilege"].ToString()
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
