using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using QRCoder;
using System.Drawing.Imaging;





namespace testing.Registration_System.Student_Account_System
{
    public partial class studentAccountSystem : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-SLBI6LR\SQLEXPRESS;Initial Catalog = FinalDb;Integrated Security=True");

        public studentAccountSystem()
        {
            InitializeComponent();
            displayStudentData();
            txtIDNumber.TextChanged += txtIDNumber_TextChanged;
        }
        public void RefreshData()
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke((MethodInvoker)delegate { RefreshData(); });
                return;
            }

            displayStudentData();
        }
        public void displayStudentData()
        {
            studentData sd = new studentData();
            List<studentData> listData = sd.GetStudentData();

            dataGridView1.DataSource = listData;
        }

        private byte[] GetStudentPhoto(int IDNumber)
        {
            try
            {
                // Create a new connection instance using the latest connection string
                using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create the command
                    using (SqlCommand command = new SqlCommand("SELECT Photo FROM StudentsAccounts WHERE IDNumber = @IDNumber", connection))
                    {
                        // Add parameter
                        command.Parameters.AddWithValue("@IDNumber", IDNumber);

                        // Execute the query
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while retrieving image: " + ex.Message);
            }

            // If an error occurs or no photo is found, return null
            return null;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtIDNumber.Text == ""
                || txtFirstname.Text == ""
                || txtLastname.Text == ""
                || (rbMale.Checked == false && rbFemale.Checked == false)
                || cmbCourse.Text == ""
                || cmbYear.Text == ""
                || cmbSY.Text == ""
                || cmbSemester.Text == ""
                || AddStudentPicture.Image == null)
            {
                MessageBox.Show("Please fill all blank fields"
                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (connectionString.State == ConnectionState.Closed)
                {
                    try
                    {
                        connectionString.Open();

                        // Check for duplicate student ID before registration
                        string checkStudentID = "SELECT COUNT(*) FROM StudentsAccounts WHERE IDNumber = @IDNumber";
                        using (SqlCommand checkCmd = new SqlCommand(checkStudentID, connectionString))
                        {
                            checkCmd.Parameters.AddWithValue("@IDNumber", int.Parse(txtIDNumber.Text.Trim()));
                            int count = (int)checkCmd.ExecuteScalar();


                            if (count >= 1)
                            {
                                MessageBox.Show(txtIDNumber.Text.Trim() + " is already taken!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method if duplicate ID is found
                            }
                        }

                        string action = "Register"; // Set action for registration                       

                        using (SqlCommand cmd = new SqlCommand("dbo.ManageStudentAccount", connectionString))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Action", action);
                            cmd.Parameters.AddWithValue("@IDNumber", int.Parse(txtIDNumber.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Firstname", txtFirstname.Text.Trim());
                            cmd.Parameters.AddWithValue("@Lastname", txtLastname.Text.Trim());

                            //Gender logic
                            string g;
                            if (rbMale.Checked == true)
                            {
                                g = "male";
                            }
                            else
                            {
                                g = "female";
                            }

                            cmd.Parameters.AddWithValue("@Gender", g);
                            cmd.Parameters.AddWithValue("@Course", cmbCourse.Text.Trim());
                            cmd.Parameters.AddWithValue("@Year", cmbYear.Text.Trim());
                            cmd.Parameters.AddWithValue("@SchoolYear", cmbSY.Text.Trim());
                            cmd.Parameters.AddWithValue("@Semester", cmbSemester.Text.Trim());

                            // Image handling logic
                            if (AddStudentPicture.Image != null)
                            {
                                string imagePath = Path.Combine( // Replace with your desired image directory path
                                    @"C:\Thesis Finale\Backup\Data\images", // Example path, modify accordingly
                                    txtIDNumber.Text.Trim() + ".jpg");

                                string directoryPath = Path.GetDirectoryName(imagePath);

                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                File.Copy(AddStudentPicture.ImageLocation, imagePath, true);

                                // Assuming the image column in your table is a varbinary(max) type
                                cmd.Parameters.AddWithValue("@Photo", File.ReadAllBytes(imagePath));
                            }
                            else
                            {
                                // Handle case where no image is selected (optional: set a default image path)
                                cmd.Parameters.AddWithValue("@Photo", DBNull.Value); // Set to null if no image
                            }

                            cmd.ExecuteNonQuery();
                            RefreshData();


                            if (addQRCode.Image != null)
                            {
                                // Get the IDNumber from the user interface
                                int IDNumber = int.Parse(txtIDNumber.Text.Trim());

                                // Prompt user to choose where to save the file
                                SaveFileDialog saveFileDialog = new SaveFileDialog();
                                saveFileDialog.Filter = "PNG Image|*.png";
                                saveFileDialog.Title = "Save QR Code Image";
                                saveFileDialog.FileName = IDNumber.ToString(); // Set default filename

                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    // Convert the QR code image to a byte array
                                    byte[] qrCodeBytes;
                                    using (MemoryStream stream = new MemoryStream())
                                    {
                                        addQRCode.Image.Save(stream, ImageFormat.Png);
                                        qrCodeBytes = stream.ToArray();
                                    }

                                    // Save the QR code image to the selected file
                                    addQRCode.Image.Save(saveFileDialog.FileName, ImageFormat.Png);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No QR Code image to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            MessageBox.Show("Student registered successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearFields();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex
                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectionString.Close();
                    }
                }
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
                    AddStudentPicture.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error Message"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void clearFields()
        {
            txtIDsearch.Clear();
            txtIDNumber.Clear();
            txtFirstname.Clear();
            txtLastname.Clear();
            rbMale.Checked = false;
            rbFemale.Checked = false;
            cmbCourse.SelectedItem = null;
            cmbYear.SelectedItem = null;
            cmbSY.SelectedItem = null;
            cmbSemester.SelectedItem = null;
            AddStudentPicture.Image = null;
        }      

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (txtFirstname.Text == ""
                || txtLastname.Text == ""
                || (rbMale.Checked == false && rbFemale.Checked == false)
                || cmbCourse.Text == ""
                || cmbYear.Text == ""
                || cmbSY.Text == ""
                || cmbSemester.Text == "")
            {
                MessageBox.Show("Please fill all blank fields except ID number", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connectionString.State == ConnectionState.Closed)
                {
                    try
                    {
                        connectionString.Open();

                        //ID Number verifier
                        string checkStudentID = "SELECT COUNT(*) FROM StudentsAccounts WHERE IDNumber = @IDNumber";
                        using (SqlCommand checkCmd = new SqlCommand(checkStudentID, connectionString))
                        {
                            checkCmd.Parameters.AddWithValue("@IDNumber", int.Parse(txtIDNumber.Text.Trim()));
                            int count = (int)checkCmd.ExecuteScalar();
                            if (count == 0)
                            {
                                MessageBox.Show("ID number does not exist in the database. Please register the student first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method
                            }
                              
                            if (count > 1)
                            {
                                MessageBox.Show(" ID Number cannot be modified!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method if duplicate ID is found
                            }
                        }


                        string action = "Update"; // Set action for update

                        using (SqlCommand cmd = new SqlCommand("dbo.ManageStudentAccount", connectionString))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Action", action);
                            cmd.Parameters.AddWithValue("@IDNumber", int.Parse(txtIDNumber.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Firstname", txtFirstname.Text.Trim());
                            cmd.Parameters.AddWithValue("@Lastname", txtLastname.Text.Trim());

                            // Gender logic
                            string g;
                            if (rbMale.Checked == true)
                            {
                                g = "male";
                            }
                            else
                            {
                                g = "female";
                            }

                            cmd.Parameters.AddWithValue("@Gender", g);
                            cmd.Parameters.AddWithValue("@Course", cmbCourse.Text.Trim());
                            cmd.Parameters.AddWithValue("@Year", cmbYear.Text.Trim());
                            cmd.Parameters.AddWithValue("@SchoolYear", cmbSY.Text.Trim());
                            cmd.Parameters.AddWithValue("@Semester", cmbSemester.Text.Trim());


                            if (!string.IsNullOrEmpty(AddStudentPicture.ImageLocation))
                            {
                                string imagePath = Path.Combine( // Replace with your desired image directory path
                                    @"C:\Thesis Finale\Backup\Data\images",
                                    txtIDNumber.Text.Trim() + ".jpg"); // Use ID number for filename

                                string directoryPath = Path.GetDirectoryName(imagePath);

                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }

                                // Check if a file with the same name exists
                                bool fileExists = File.Exists(imagePath);

                                if (fileExists)
                                {
                                    // Prompt the user if they want to overwrite the existing photo
                                    DialogResult result = MessageBox.Show("Do you want to overwrite the existing photo?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (result == DialogResult.No)
                                    {
                                        // If the user chooses not to overwrite, exit the method
                                        return;
                                    }
                                }

                                // Copy the new photo to the image directory, overwriting if it already exists
                                File.Copy(AddStudentPicture.ImageLocation, imagePath, true);

                                // Read the image file and add it to the parameters
                                cmd.Parameters.AddWithValue("@Photo", File.ReadAllBytes(imagePath));
                            }
                            else
                            {
                                // Get the existing photo from the database
                                byte[] existingPhoto = GetStudentPhoto(int.Parse(txtIDNumber.Text.Trim()));

                                // If there's an existing photo, use it; otherwise, show an error message
                                if (existingPhoto != null)
                                {
                                    cmd.Parameters.AddWithValue("@Photo", existingPhoto);
                                }
                                else
                                {
                                    MessageBox.Show("No existing photo found. Please add a photo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return; // Exit the method to avoid further processing without a photo
                                }
                            }

                            cmd.ExecuteNonQuery();
                            RefreshData();

                            MessageBox.Show("Student Credential updated successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            clearFields();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connectionString.Close();
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDNumber.Text))
            {
                MessageBox.Show("Please select a student to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm with the user before deleting
            DialogResult result = MessageBox.Show("Are you sure you want to delete this student?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Delete the student record from the database
                    using (SqlConnection connection = new SqlConnection(connectionString.ConnectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("DELETE FROM StudentsAccounts WHERE IDNumber = @IDNumber", connection))
                        {
                            command.Parameters.AddWithValue("@IDNumber", int.Parse(txtIDNumber.Text));

                            command.ExecuteNonQuery();
                            RefreshData();
                        }
                    }

                    // Delete the associated photo file from the file system
                    string imagePath = Path.Combine(@"C:\Thesis Finale\Backup\Data\images", txtIDNumber.Text.Trim() + ".jpg");
                    string qrimagePath = Path.Combine(@"C:\Thesis Finale\Backup\Data\qr code", txtIDNumber.Text.Trim() + ".png");
                    if (File.Exists(imagePath)||File.Exists(qrimagePath))
                    {
                        File.Delete(imagePath);
                        File.Delete(qrimagePath);
                    }

                    MessageBox.Show("Student deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void txtIDNumber_TextChanged(object sender, EventArgs e)
        {
            string text = txtIDNumber.Text;
            if (!string.IsNullOrEmpty(text))
            {
                // Generate QR code for the text
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                // Convert QR code to Bitmap
                Bitmap qrCodeImage = qrCode.GetGraphic(5);
                // Display QR code in PictureBox
                addQRCode.Image = qrCodeImage;
            }
            else
            {
                // Clear PictureBox if TextBox is empty
                addQRCode.Image = null;
            }
        }        


        private void saveQRCode_Click_1(object sender, EventArgs e)
        {
            if (addQRCode.Image != null)
            {
                // Get the IDNumber from the user interface
                int IDNumber = int.Parse(txtIDNumber.Text.Trim());

                // Prompt user to choose where to save the file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG Image|*.png";
                saveFileDialog.Title = "Save QR Code Image";

                saveFileDialog.FileName = IDNumber.ToString(); // Set default filename

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Convert the QR code image to a byte array
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(txtIDNumber.Text.Trim(), QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20); // Adjust the size here (e.g., 20)

                    // Save the QR code image to the selected file
                    qrCodeImage.Save(saveFileDialog.FileName, ImageFormat.Png);
                }
            }
            else
            {
                MessageBox.Show("No QR Code image to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Check if a row is selected
            {
                // Get the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];  // Assuming single selection

                // Access data from the row
                string firstName = row.Cells[1].Value.ToString();
                string lastName = row.Cells[2].Value.ToString();
                string gender = row.Cells[3].Value.ToString();  // Adjust index
                string course = row.Cells[4].Value.ToString();  // Adjust index
                string year = row.Cells[5].Value.ToString();  // Adjust index
                string schoolYear = row.Cells[6].Value.ToString();  // Adjust index
                string semester = row.Cells[7].Value.ToString();  // Adjust index   

                int idNumber;

                // Populate textboxes
                txtFirstname.Text = firstName;
                txtLastname.Text = lastName;

                // Set radio button selection based on gender
                if (gender.ToLower() == "female")
                {
                    rbFemale.Checked = true;
                }
                else
                {
                    rbMale.Checked = true;
                }

                cmbCourse.SelectedIndex = cmbCourse.FindStringExact(course);  // Find exact course match
                cmbYear.SelectedIndex = cmbYear.FindStringExact(year);  // Find exact year match
                cmbSY.SelectedIndex = cmbSY.FindStringExact(schoolYear);  // Find exact school year match
                cmbSemester.SelectedIndex = cmbSemester.FindStringExact(semester);  // Find exact semester match

                // Assuming ID number is an integer
                if (int.TryParse(row.Cells[0]?.Value?.ToString(), out idNumber))
                {
                    txtIDNumber.Text = idNumber.ToString();
                    byte[] photoData = GetStudentPhoto(idNumber);
                    if (photoData != null)
                    {
                        using (MemoryStream ms = new MemoryStream(photoData))
                        {
                            AddStudentPicture.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        AddStudentPicture.Image = Properties.Resources.NoImagePlaceholder; // Use placeholder image resource
                    }
                }
                else
                {
                    MessageBox.Show("Invalid ID number format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void txtIDsearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Clear any existing data in the DataGridView



                if (txtIDsearch.Text.Trim() == "") // Check if search field is empty
                {
                    string defaultQuery = "SELECT * FROM StudentsAccounts";  // Replace with your default student selection logic

                    connectionString.Open();
                    using (SqlCommand cmd = new SqlCommand(defaultQuery, connectionString))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                    return;
                }


                string sql = "SearchStudent";
                using (SqlCommand cmd = new SqlCommand(sql, connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@searchValue", txtIDsearch.Text.Trim());

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No student found matching your search criteria.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Display the default student list after showing the message
                            string defaultQuery = "SELECT * FROM StudentsAccounts"; // Replace with your default student selection logic
                            using (SqlCommand cmd2 = new SqlCommand(defaultQuery, connectionString)) // Create a new command for the default query
                            {
                                using (SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2))
                                {
                                    DataTable dt2 = new DataTable();
                                    adapter2.Fill(dt2);
                                    dataGridView1.DataSource = dt2;
                                }
                            }
                            txtIDsearch.Clear();
                        }
                        else
                        {
                            dataGridView1.DataSource = dt; // Set the DataGridView data source if results exist
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connectionString.Close();
            }
        }

       
    }
}
