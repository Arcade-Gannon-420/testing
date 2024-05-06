using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing.Registration_System.Student_Account_System
{
    public partial class studentModifyAccount : UserControl
    {
        public string connectionString = @"Data Source=DESKTOP-SLBI6LR\SQLEXPRESS;Initial Catalog=testv5;Integrated Security=True";

        public studentModifyAccount()
        {
            InitializeComponent();
            txtIDNumber.Enabled = false;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            string IDNumber = txtIDNumber.Text.Trim();
            string firstname = txtFirstname.Text.Trim();
            string lastname = txtLastname.Text.Trim();

            //Gender
            string g;
            if (rbMale.Checked == true)
            {
                g = "male";
            }
            else
            {
                g = "female";
            }

            string course = cmbCourse.SelectedItem.ToString();
            string year = cmbYear.SelectedItem.ToString();
            string sy = cmbSY.SelectedItem.ToString();
            string semester = cmbSemester.SelectedItem.ToString();
            byte[] photo = getPhoto();

            if (string.IsNullOrEmpty(IDNumber) ||string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(g) || string.IsNullOrEmpty(course)
                || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(sy) || string.IsNullOrEmpty(semester))
            {
                MessageBox.Show("Please enter all fields");
                return;
            }


            if (modifyAccount(IDNumber, firstname, lastname, g, course, year, sy, semester, photo))
            {
                MessageBox.Show("Account Modified successfully!");
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to Modify account");
            }

        }

        private bool modifyAccount(string IDNumber, string firstname, string lastname, string g, string course, string year, string sy, string semester, byte[] photo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("ManageStudentAccount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IDNumber", IDNumber);
                        command.Parameters.AddWithValue("@Firstname", firstname);
                        command.Parameters.AddWithValue("@Lastname", lastname);
                        command.Parameters.AddWithValue("@Gender", g);
                        command.Parameters.AddWithValue("@Course", course);
                        command.Parameters.AddWithValue("@Year", year);
                        command.Parameters.AddWithValue("@SchoolYear", sy);
                        command.Parameters.AddWithValue("@Semester", semester);
                        command.Parameters.AddWithValue("@Photo", photo);


                        command.Parameters.AddWithValue("@Action", "Update");

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                // Check if the exception is due to a duplicate username
                if (ex.Number == 2627) // SQL Server error code for unique constraint violation
                {
                    MessageBox.Show("Student ID already exists. Please choose a different username.");
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                return false;
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            int IDNumber;
            if (!int.TryParse(txtIDNumber.Text.Trim(), out IDNumber))
            {
                MessageBox.Show("Student ID must be a valid ID Number.");
                return;
            }

            string firstname = txtFirstname.Text.Trim();
            string lastname = txtLastname.Text.Trim();

            //Gender
            string g;
            if (rbMale.Checked == true)
            {
                g = "male";
            }
            else
            {
                g = "female";
            }

            string course = cmbCourse.SelectedItem?.ToString();
            string year = cmbYear.SelectedItem?.ToString();
            string sy = cmbSY.SelectedItem?.ToString();
            string semester = cmbSemester.SelectedItem?.ToString();
            byte[] photo = getPhoto();

            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(g) || string.IsNullOrEmpty(course)
                || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(sy) || string.IsNullOrEmpty(semester))
            {   
                MessageBox.Show("Please enter all fields");
                return;
            }

            if (deleteStudentAccount(IDNumber, firstname, lastname, g, course, year, sy, semester, photo))
            {
                MessageBox.Show("Account Deleted successfully!");
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to register account");
            }

        }

        private bool deleteStudentAccount(int IDNumber, string firstname, string lastname, string g, string course, string year, string sy, string semester, byte[] C)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("ManageStudentAccount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IDNumber", IDNumber);
                        command.Parameters.AddWithValue("@Firstname", firstname);
                        command.Parameters.AddWithValue("@Lastname", lastname);
                        command.Parameters.AddWithValue("@Gender", g);
                        command.Parameters.AddWithValue("@Course", course);
                        command.Parameters.AddWithValue("@Year", year);
                        command.Parameters.AddWithValue("@SchoolYear", sy);
                        command.Parameters.AddWithValue("@Semester", semester);
                        command.Parameters.AddWithValue("@Photo", DBNull.Value);


                        command.Parameters.AddWithValue("@Action", "Delete");

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (SqlException ex)
            {
                // Check if the exception is due to a duplicate username
                if (ex.Number == 2627) // SQL Server error code for unique constraint violation
                {
                    MessageBox.Show("Student ID already exists. Please choose a different username.");
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                return false;
            }
        }

        private void btnBrowseImg_Click(object sender, EventArgs e)
        {
            
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                string imagePath = "";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    AddPicture.Image = ResizeImage(AddPicture.Image, 200, 200);
                    AddPicture.ImageLocation = imagePath;  // Set ImageLocation here
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //retrieve photo from outside
        private byte[] getPhoto()
        {
            if (AddPicture.ImageLocation != null && AddPicture.ImageLocation.Length > 0)
            {
                try
                {
                    // Use the image path from ImageLocation instead of Image property
                    using (FileStream fs = File.OpenRead(AddPicture.ImageLocation))
                    {
                        AddPicture.Image = ResizeImage(AddPicture.Image, 200, 200);
                        byte[] photo = new byte[fs.Length];
                        fs.Read(photo, 0, (int)fs.Length);
                        return photo;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while loading the image: " + ex.Message);
                    return null;
                }
            }
            return null;
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            // Create a new bitmap with the specified width and height
            Bitmap bitmap = new Bitmap(width, height);

            // Create a Graphics object from the bitmap
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // Set the interpolation mode to HighQualityBicubic to ensure high-quality resizing
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // Draw the original image onto the bitmap, resizing it in the process
                graphics.DrawImage(image, 0, 0, width, height);
            }

            // Return the resized bitmap as an Image object
            return bitmap;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int IDNumber;
            if (int.TryParse(txtIDsearch.Text, out IDNumber))
            {
                GetStudentDetails(IDNumber);
                  
            }
            else
            {
                MessageBox.Show("Please enter a valid UserId");
            }
        }

        private byte[] GetStudentPhoto(int IDNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT Photo FROM StudentsAccounts WHERE IDNumber = @IDNumber", connection))
                    {
                        command.Parameters.AddWithValue("@IDNumber", IDNumber);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["Photo"] != DBNull.Value)
                            {
                                byte[] photo = (byte[])reader["Photo"];
                                return photo;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while retrieving image: " + ex.Message);
            }

            return null;
        }



        private void GetStudentDetails(int IDNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM StudentsAccounts WHERE IDNumber = @IDNumber", connection))
                    {
                        command.Parameters.AddWithValue("@IDNumber", IDNumber);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            txtIDNumber.Text = reader["IDNumber"].ToString();
                            txtFirstname.Text = reader["Firstname"].ToString();
                            txtLastname.Text = reader["Lastname"].ToString();
                            rbMale.Checked = (reader["Gender"].ToString().Equals("male", StringComparison.OrdinalIgnoreCase));
                            rbFemale.Checked = !rbMale.Checked;
                            cmbCourse.SelectedItem = reader["Course"].ToString();
                            cmbYear.SelectedItem = reader["Year"].ToString();
                            cmbSY.SelectedItem = reader["SchoolYear"].ToString();
                            cmbSemester.SelectedItem = reader["Semester"].ToString();

                            // Retrieve and display the photo (using the new GetStudentPhoto method)
                            byte[] photo = GetStudentPhoto(IDNumber);
                            if (photo != null)
                            {
                                using (MemoryStream ms = new MemoryStream(photo))
                                {
                                    AddPicture.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                // Photo is null or not found, handle accordingly
                                AddPicture.Image = null;
                            }
                        }
                        else
                        {
                            MessageBox.Show("User not found");
                            ClearFields();
                        }
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void studentModifyAccount_Load(object sender, EventArgs e)
        {

        }

        private void ClearFields()
        {
            txtIDsearch.Clear();
            txtIDNumber.Clear();
            txtFirstname.Clear();
            txtLastname.Clear();
            cmbCourse.SelectedItem = null;
            cmbYear.SelectedItem = null;
            cmbSY.SelectedItem = null;
            cmbSemester.SelectedItem = null;
            AddPicture.Image = null;
        }
    }
}
