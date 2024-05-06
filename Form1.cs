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

namespace testing
{
    public partial class Form1 : Form
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-SLBI6LR\SQLEXPRESS;Initial Catalog = FinalDb;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }      

        private string AuthenticateUser(string username, string password)
        {
            string userRole = "unknown";

            
            using (SqlCommand command = new SqlCommand("AuthenticateUser", connectionString))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.Add("@UserRole", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;

                connectionString.Open();
                command.ExecuteNonQuery();

                userRole = Convert.ToString(command.Parameters["@UserRole"].Value);
            }
           
            return userRole;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password");
                return;
            }

            string userRole = AuthenticateUser(username, password);

            if (userRole != "unknown")
            {
                MessageBox.Show("Login successful! User role: " + userRole);
                // Here you can perform actions after successful login, such as opening another form

                // Open the dashboard form
                dashboard dboard = new dashboard();
                dboard.Show();
                this.Hide(); // Hide the login form
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }

       
    }
}
