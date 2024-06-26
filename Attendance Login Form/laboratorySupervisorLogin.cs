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

namespace testing.laboratory_Supervisor_Login
{
    public partial class laboratorySupervisorLogin : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-SLBI6LR\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True");



        public laboratorySupervisorLogin()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private Tuple<string, string, string> AuthenticateUser(string username, string password)
        {
            Tuple<string, string, string> userData = null;

            try
            {
                using (SqlCommand command = new SqlCommand("AuthenticateUser", connectionString))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.Add("@UserRole", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Firstname", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                    command.Parameters.Add("@Lastname", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;

                    connectionString.Open();
                    command.ExecuteNonQuery();

                    string userRole = Convert.ToString(command.Parameters["@UserRole"].Value);
                    string Firstname = Convert.ToString(command.Parameters["@Firstname"].Value);
                    string Lastname = Convert.ToString(command.Parameters["@Lastname"].Value);

                    userData = Tuple.Create(userRole, Firstname, Lastname);
                }
            }
            catch (SqlException ex)
            {
                // Check the error number to determine if it's a login failure
                if (ex.Number == 12345) // Replace 12345 with the actual error code for invalid credentials
                {
                    // Invalid username or password, return null
                    return null;
                }
                else
                {
                    // Other database-related error, rethrow the exception
                    throw;
                }
            }
            finally
            {
                connectionString.Close();
            }

            return userData;
        }

        private void login()
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                Tuple<string, string, string> userData = AuthenticateUser(username, password);

                if (userData != null && userData.Item1 != "unknown")
                {
                    // User authenticated successfully
                    string userRole = userData.Item1;
                    string FirstName = userData.Item2;
                    string LastName = userData.Item3;

                    MessageBox.Show("Login successful! User role: " + userRole);

                    // Open the dashboard form and pass the user role, first name, and last name

                    dashboard d = new dashboard(FirstName, LastName, userRole);
                    d.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }




    }
}
