using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace testing.Subject_Creation_System
{
    public partial class subjectCreationSystem : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-SLBI6LR\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True");
        
        public subjectCreationSystem()
        {
            InitializeComponent();

            subStartTime.Format = DateTimePickerFormat.Time;
            subEndTime.Format = DateTimePickerFormat.Time;
        }

        //Realtime load
        private void subjectCreationSystem_Load(object sender, EventArgs e)
        {
            displaySubjectData();
        }

        public void RefreshData()
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke((MethodInvoker)delegate { RefreshData(); });
                return;
            }
            displaySubjectData();
        }

        public void displaySubjectData()
        {
            subjectData sd = new subjectData();
            List<subjectData> listData = sd.GetsubjectData();

            dataGridView1.DataSource = listData;
        }
       


        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtEDPCode.Text == ""            
            || txtSubjectCode.Text == ""
            || cmbUnits.Text == ""
            || (!chkMonday.Checked && !chkTuesday.Checked && !chkWednesday.Checked && !chkThursday.Checked && !chkFriday.Checked && !chkSaturday.Checked)
            || subStartTime.Value == DateTime.MinValue
            || subEndTime.Value == DateTime.MinValue
            || txtTitle.Text == ""
            || cmbCourse.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (connectionString.State == ConnectionState.Closed)
                {
                    try
                    {
                        connectionString.Open();

                        // Check for duplicate EDP Code before registration
                        string checkEDPCodeQuery = "SELECT COUNT(*) FROM Subjects WHERE EDPCode = @EDPCode";
                        using (SqlCommand checkCmd = new SqlCommand(checkEDPCodeQuery, connectionString))
                        {
                            checkCmd.Parameters.AddWithValue("@EDPCode", int.Parse(txtEDPCode.Text.Trim()));
                            int count = (int)checkCmd.ExecuteScalar();

                            if (count >= 1)
                            {
                                MessageBox.Show(txtEDPCode.Text.Trim() + " is already taken!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method if duplicate EDP Code is found
                            }
                        }

                        string action = "Create"; // Set action for registration                       

                        using (SqlCommand cmd = new SqlCommand("dbo.ManageSubjects", connectionString))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Action", action);
                            cmd.Parameters.AddWithValue("@EDPCode", int.Parse(txtEDPCode.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                            cmd.Parameters.AddWithValue("@SubjectCode", txtSubjectCode.Text.Trim());
                            cmd.Parameters.AddWithValue("@Units", int.Parse(cmbUnits.Text.Trim()));


                            string selectedDays = "";
                            if (chkMonday.Checked)
                                selectedDays += "M";
                            if (chkTuesday.Checked)
                                selectedDays += "T";
                            if (chkWednesday.Checked)
                                selectedDays += "W";
                            if (chkThursday.Checked)
                                selectedDays += "Th";
                            if (chkFriday.Checked)
                                selectedDays += "F";
                            if (chkSaturday.Checked)
                                selectedDays += "S";


                            cmd.Parameters.AddWithValue("@Schedule", selectedDays);



                            cmd.Parameters.AddWithValue("@StartTime", subStartTime.Value.ToString("hh:mm:ss tt"));
                            cmd.Parameters.AddWithValue("@EndTime", subEndTime.Value.ToString("hh:mm:ss tt"));
                            cmd.Parameters.AddWithValue("@Course", cmbCourse.Text.Trim());


                            cmd.ExecuteNonQuery();
                            RefreshData();

                            MessageBox.Show("Subject created successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtSubjectCode.Text == ""
            || cmbUnits.Text == ""
            || (!chkMonday.Checked && !chkTuesday.Checked && !chkWednesday.Checked && !chkThursday.Checked && !chkFriday.Checked && !chkSaturday.Checked)
            || subStartTime.Value == DateTime.MinValue
            || subEndTime.Value == DateTime.MinValue
            || txtTitle.Text == ""
            || cmbCourse.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (connectionString.State == ConnectionState.Closed)
                {
                    try
                    {
                        connectionString.Open();

                        //EDP Code verifier
                        string checkEDPCodeQuery = "SELECT COUNT(*) FROM Subjects WHERE EDPCode = @EDPCode";
                        using (SqlCommand checkCmd = new SqlCommand(checkEDPCodeQuery, connectionString))
                        {
                            checkCmd.Parameters.AddWithValue("@EDPCode", int.Parse(txtEDPCode.Text.Trim()));
                            int count = (int)checkCmd.ExecuteScalar();
                            if (count == 0)
                            {
                                MessageBox.Show("User does not exist in the database. Please create a subject first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method
                            }

                            if (count > 1)
                            {
                                MessageBox.Show("User ID cannot be modified!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the method if duplicate ID is found
                            }
                        }

                        string action = "Update"; // Set action for registration                       

                        using (SqlCommand cmd = new SqlCommand("dbo.ManageSubjects", connectionString))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Action", action);
                            cmd.Parameters.AddWithValue("@EDPCode", int.Parse(txtEDPCode.Text.Trim()));
                            cmd.Parameters.AddWithValue("@Title", txtTitle.Text.Trim());
                            cmd.Parameters.AddWithValue("@SubjectCode", txtSubjectCode.Text.Trim());
                            cmd.Parameters.AddWithValue("@Units", int.Parse(cmbUnits.Text.Trim()));


                            string selectedDays = "";
                            if (chkMonday.Checked)
                                selectedDays += "M";
                            if (chkTuesday.Checked)
                                selectedDays += "T";
                            if (chkWednesday.Checked)
                                selectedDays += "W";
                            if (chkThursday.Checked)
                                selectedDays += "Th";
                            if (chkFriday.Checked)
                                selectedDays += "F";
                            if (chkSaturday.Checked)
                                selectedDays += "S";


                            cmd.Parameters.AddWithValue("@Schedule", selectedDays);
                            cmd.Parameters.AddWithValue("@StartTime", subStartTime.Value.ToString("hh:mm:ss tt"));
                            cmd.Parameters.AddWithValue("@EndTime", subEndTime.Value.ToString("hh:mm:ss tt"));
                            cmd.Parameters.AddWithValue("@Course", cmbCourse.Text.Trim());

                            cmd.ExecuteNonQuery();
                            RefreshData();

                            MessageBox.Show("Subject updated successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (string.IsNullOrWhiteSpace(txtEDPCode.Text))
            {
                MessageBox.Show("Please select a subject to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this Subject?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Delete the subject record from the database

                    connectionString.Open();

                    using (SqlCommand command = new SqlCommand("DELETE FROM Subjects WHERE EDPCode = @EDPCode", connectionString))
                    {
                        if (int.TryParse(txtEDPCode.Text, out int edpCode))
                        {
                            command.Parameters.AddWithValue("@EDPCode", edpCode);
                        }
                        else
                        {
                            MessageBox.Show("Invalid EDP Code.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        command.ExecuteNonQuery();
                        RefreshData();
                    }
                    
                    MessageBox.Show("Subject deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearFields();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error: You must disenroll the students from this subject first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connectionString.Close();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }
        public void clearFields()
        {
            txtEDPCode.Clear();
            txtSubjectCode.Clear();
            cmbUnits.SelectedItem = null;

            chkMonday.Checked = false;
            chkTuesday.Checked = false;
            chkWednesday.Checked = false;
            chkThursday.Checked = false;
            chkFriday.Checked = false;
            chkSaturday.Checked = false;

            subStartTime.Value = DateTime.Today;
            subEndTime.Value = DateTime.Today;
            txtTitle.Clear();
            cmbCourse.SelectedItem = null;
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow row = dataGridView1.SelectedRows[0];  // Assuming single selection


                string EDPCode = row.Cells[0].Value.ToString();
                string Title = row.Cells[1].Value.ToString();
                string SubjectCode = row.Cells[2].Value.ToString();  // Adjust index
                string Units = row.Cells[3].Value.ToString();  // Adjust index
                string Schedule = row.Cells[4].Value.ToString();  // Adjust index
                string StartTime = row.Cells[5].Value.ToString();  // Adjust index
                string EndTime = row.Cells[6].Value.ToString();  // Adjust index
                string Course = row.Cells[7].Value.ToString();  // Adjust index   


                txtEDPCode.Text = EDPCode;
                txtTitle.Text = Title;
                txtSubjectCode.Text = SubjectCode;
                cmbUnits.SelectedIndex = cmbUnits.FindStringExact(Units);


                chkMonday.Checked = Schedule.Contains("M");
                chkTuesday.Checked = Schedule.Contains("T");
                chkWednesday.Checked = Schedule.Contains("W");
                chkThursday.Checked = Schedule.Contains("Th");
                chkFriday.Checked = Schedule.Contains("F");
                chkSaturday.Checked = Schedule.Contains("S");


                DateTime startTime = DateTime.ParseExact(StartTime, "hh:mm:ss tt", CultureInfo.InvariantCulture);
                DateTime endTime = DateTime.ParseExact(EndTime, "hh:mm:ss tt", CultureInfo.InvariantCulture);

                // Set DateTimePicker values
                subStartTime.Value = startTime;
                subEndTime.Value = endTime;


                cmbCourse.SelectedIndex = cmbCourse.FindStringExact(Course);
            }
            else
            {
                MessageBox.Show("Invalid EDP Code format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSubSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Clear any existing data in the DataGridView

                

                if (txtSubSearch.Text.Trim() == "") // Check if search field is empty
                {
                    string defaultQuery = "SELECT * FROM Subjects";  // Replace with your default student selection logic

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

                // Use parameterized query to prevent SQL injection vulnerabilities
                string sql = "SearchSubjects";
                using (SqlCommand cmd = new SqlCommand(sql, connectionString))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@searchValue", txtSubSearch.Text.Trim());

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No Subject found matching your search criteria.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Display the default student list after showing the message
                            string defaultQuery = "SELECT * FROM Subjects"; // Replace with your default student selection logic
                            using (SqlCommand cmd2 = new SqlCommand(defaultQuery, connectionString)) // Create a new command for the default query
                            {
                                using (SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2))
                                {
                                    DataTable dt2 = new DataTable();
                                    adapter2.Fill(dt2);
                                    dataGridView1.DataSource = dt2;
                                }
                            }
                            txtSubSearch.Clear();
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
