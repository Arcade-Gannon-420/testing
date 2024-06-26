using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using testing;
using testing.Utilities;
using testing.Attendance_Login_Form;
using testing.Audit_System;
using testing.laboratory_Supervisor_Login;

namespace testing
{
    public partial class Form1 : Form
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source =.\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True");


        private laboratorySupervisorLogin laboratorySupervisorLogin;
        
        public Form1()
        {
            InitializeComponent();

            panel2.MouseDown += Panel2_MouseDown;
            panel2.MouseMove += Panel2_MouseMove;
            panel2.MouseUp += Panel2_MouseUp;
             
            // Set the PasswordChar property to mask the password input
            txtPassword.PasswordChar = '*';
        }

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point difference = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(difference));
            }
        }

        private void Panel2_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
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


        private void Form1_Load(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(95,72,183,255);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }

        private void btnLabSup_Click_1(object sender, EventArgs e)
        {
            if (laboratorySupervisorLogin == null || laboratorySupervisorLogin.IsDisposed)
            {
                laboratorySupervisorLogin = new laboratorySupervisorLogin();
                laboratorySupervisorLogin.Dock = DockStyle.Fill;
                panel1.Controls.Add(laboratorySupervisorLogin);
            }
            else
            {
                laboratorySupervisorLogin.BringToFront();
            }
        }
    }
}
