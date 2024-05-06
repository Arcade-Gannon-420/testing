using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing.Registration_System.Student_Account_System
{
    public partial class studentRegisterAccount : UserControl
    {
        private string connectionString = @"Data Source=DESKTOP-0U6GMF4\SQLEXPRESS;Initial Catalog=testv2;Integrated Security=True";

        public studentRegisterAccount()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
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

            if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(g)|| string.IsNullOrEmpty(course)
                || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(sy) || string.IsNullOrEmpty(semester))
            {
                MessageBox.Show("Please enter all fields");
                return;
            }


            if (RegisterNewAccount(IDNumber, firstname, lastname,g,course,year,sy,semester,photo))
            {
                MessageBox.Show("Account registered successfully!");
                ClearFields();
            }
            else
            {
                MessageBox.Show("Failed to register account");
            }
        }

        private bool RegisterNewAccount(int IDNumber, string firstname, string lastname, string g, string course, string year,string sy,string semester, byte[] photo)
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


                        command.Parameters.AddWithValue("@Action", "Register");

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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                AddPicture.Image = new Bitmap(openFileDialog.FileName);
                AddPicture.Image = ResizeImage(AddPicture.Image, 200, 200);
            }
        }
        private byte[] getPhoto()
        {
            if (AddPicture.Image != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    // Save the image as JPEG format
                    AddPicture.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return stream.ToArray();
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


        private void ClearFields()
        {
            txtIDNumber.Text = "";
            txtFirstname.Text = "";
            txtLastname.Text = "";
            cmbCourse.Text = "";
            cmbYear.Text = "";
            cmbSY.Text = "";
            cmbSemester.Text = "";
            AddPicture.Image = null;
        }



    }

}
