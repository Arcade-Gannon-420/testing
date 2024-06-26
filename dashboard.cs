using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using testing.Registration_System.Student_Account_System;
using testing.Subject_Creation_System;
using testing.Subject_System;
using testing.student_Attendance_Summary;
using testing.Audit_System;
using testing.Dashboard_Privilege.Lab_Supervisor_Dashboard;
using testing.Dashboard_Privilege.Instructor_Dashboard;
using testing.Dashboard_Privilege.Working_Student_Dashboard;
using QRCoder;
using testing.Utilities;
using testing.Dashboard_Privilege.Dashboard_Privilege_Display;
using System.IO;
using System.Data.SqlClient;
using testing;


namespace testing
{
    public partial class dashboard : Form
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source =.\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True");

        //REGISTRATION SYSTEM
        private userAccountSystem userAccountSystem;
        private studentAccountSystem studentAccountSystem;

        //SUBJECT SYSTEM
        private subjectCreationSystem subjectCreationSystem;
        private subjectEnrollmentSystem subjectEnrollmentSystem;
        private subjectInstructorAssignment subjectInstructorAssignment;

        //REALTIME STUDENT ATTENDANCE SUMMARY
        private studentAttendanceSummary studentAttendanceSummary;

        //AUDIT SYSTEM
        private AuditSystem AuditSystem;

        //ROLE BASED DASHBOARD INTERFACE
        private SupervisorDashboard SupervisorDashboard;
        private InstructorDashboard InstructorDashboard;
        private WorkingStudentDashboard WorkingStudentDashboard;

        //ROLE BASED DASHBOARD DISPLAY
        private InstructorDisplay InstructorDisplay;
        private LabSupervisorDisplay LabSupervisorDisplay;
        private WorkingStudentDisplay WorkingStudentDisplay;

        //
        private InstructorClassList InstructorClassList;

        private OverrideAttendances OverrideAttendances;


        private Form1 Form1;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private string Firstname;
        private string Lastname;
        private string userRole;

        public dashboard(string Firstname, string Lastname, string userRole)
        {
            InitializeComponent();
            panel2.MouseDown += Panel2_MouseDown;
            panel2.MouseMove += Panel2_MouseMove;
            panel2.MouseUp += Panel2_MouseUp;

            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.userRole = userRole;   


            label1.Text = $"{Firstname} {Lastname}";


            // Retrieve and display the user's photo
            Image userPhoto = RetrieveUserPhoto();
            pictureBox3.Image = userPhoto;
        }

        private Image RetrieveUserPhoto()
        {
            Image userPhoto = null;

            try
            {
                // Check if the connection is already open; if so, close it
                if (connectionString.State != ConnectionState.Closed)
                {
                    connectionString.Close();
                }

                connectionString.Open();

                string query = "SELECT Photo FROM Users WHERE FirstName = @FirstName AND LastName = @LastName";
                using (SqlCommand command = new SqlCommand(query, connectionString))
                {
                    command.Parameters.AddWithValue("@FirstName", Firstname);
                    command.Parameters.AddWithValue("@LastName", Lastname);

                    var result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        byte[] imgData = (byte[])result;
                        userPhoto = byteArrayToImage(imgData);
                    }
                    else
                    {
                        // If no image data is retrieved, set a placeholder image
                        string placeholderImagePath = "C:\\Thesis Finale\\Backup\\Data\\Design\\user_3421697.png";
                        userPhoto = Image.FromFile(placeholderImagePath);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving user photo: " + ex.Message);
            }
            finally
            {
                // Always ensure the connection is closed in the finally block
                if (connectionString.State != ConnectionState.Closed)
                {
                    connectionString.Close();
                }
            }
            return userPhoto;
        }

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Image returnImage = Image.FromStream(ms);
                return returnImage;
            }
        }
       
        private void dashboard_Load(object sender, EventArgs e)
        {
            // Check if the logged-in user is an instructor
            if (userRole == "instructor")
            {
                if (InstructorDashboard == null || InstructorDashboard.IsDisposed)
                {
                    InstructorDashboard = new InstructorDashboard(Firstname, Lastname, userRole);
                    InstructorDashboard.Dock = DockStyle.Fill;
                    panel1.Controls.Add(InstructorDashboard);
                }
                else
                {
                    InstructorDashboard.BringToFront();
                }

                InstructorDisplay = new InstructorDisplay();
                InstructorDisplay.Dock = DockStyle.Fill;
                panel4.Controls.Add(InstructorDisplay);

                /*
                 instructor buttons that needs to be active
                -Dashboard
                - Subject List
                - Subject and Student Enrollment
                - Generate Report (Classlist and Attendance Report)
                 */


                //Button Visibility
                //Dashboard
                panel8.Visible = true;
                btnDashboard.Visible = true;

                //Registration System
                registrationContainer.Visible = false;
                btnRegistrationSystem.Visible = false;
                btnRegister.Visible = false;
                btnStudentRegister.Visible = false;

                //Enrollment System
                enrollmentContainer.Visible = true;
                btnEnrollmentSystem.Visible = true;
                btnSubjectCreation.Visible = false;
                panel14.Visible = false;
                btnSubjectEnrollment.Visible = true;

                //Audit System
                btnAuditSystem.Visible = true;
                panel11.Visible=true;

                //Instructor Subject Assignment
                panel18.Visible = true;
                btnSubjectAssignment.Visible = true;
                
                //Instructor Class List
                panel7.Visible=true;
                btnClassList.Visible =true;

                //Attendance Override
                panel12.Visible=true;
                btnAttendanceOverride.Visible=true;

                //Student Attendance Summary
                panel6.Visible=false;
                btnAttendanceSummary.Visible=false;
            }
            //Working Student Visibility
            else if (userRole == "admin")
            {
                if (WorkingStudentDashboard == null || WorkingStudentDashboard.IsDisposed)
                {
                    WorkingStudentDashboard = new WorkingStudentDashboard();
                    WorkingStudentDashboard.Dock = DockStyle.Fill;
                    panel1.Controls.Add(WorkingStudentDashboard);
                }
                else
                {
                    WorkingStudentDashboard.BringToFront();
                }

                WorkingStudentDisplay = new WorkingStudentDisplay();
                WorkingStudentDisplay.Dock = DockStyle.Fill;
                panel4.Controls.Add(WorkingStudentDisplay);

                //Button Visibility
                //Dashboard
                panel8.Visible = true;
                btnDashboard.Visible = true;

                //Registration System
                registrationContainer.Visible = false;
                btnRegistrationSystem.Visible = false;
                btnRegister.Visible = false;
                btnStudentRegister.Visible = false;

                //Enrollment System
                enrollmentContainer.Visible = false;
                btnEnrollmentSystem.Visible = false;
                btnSubjectCreation.Visible = false;
                btnSubjectEnrollment.Visible = false;

                //Audit System
                panel11.Visible = true;
                btnAuditSystem.Visible = true;

                //Instructor Subject Assignment
                panel18.Visible = false;
                btnSubjectAssignment.Visible = false;

                //Instructor Class List
                panel7.Visible=false;
                btnClassList.Visible =false;

                //Attendance Override
                panel12.Visible=false;
                btnAttendanceOverride.Visible=false;

                //Student Attendance Summary
                panel6.Visible=true;
                btnAttendanceSummary.Visible=true;
            }
            else if (userRole == "superadmin")
            {
                if (SupervisorDashboard == null || SupervisorDashboard.IsDisposed)
                {
                    SupervisorDashboard = new SupervisorDashboard(Firstname, Lastname, userRole);
                    SupervisorDashboard.Dock = DockStyle.Fill;
                    panel1.Controls.Add(SupervisorDashboard);
                }
                else
                {
                    SupervisorDashboard.BringToFront();
                }

                LabSupervisorDisplay = new LabSupervisorDisplay();
                LabSupervisorDisplay.Dock = DockStyle.Fill;
                panel4.Controls.Add(LabSupervisorDisplay);

                //Instructor Class List
                panel7.Visible=false;
                btnClassList.Visible =false;
            }
            else
            {
                // Handle unknown role
                MessageBox.Show("Unknown role detected!");
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // Check if the logged-in user is an instructor
            if (userRole == "instructor")
            {
                if (InstructorDashboard == null || InstructorDashboard.IsDisposed)
                {
                    InstructorDashboard = new InstructorDashboard(Firstname, Lastname, userRole);
                    InstructorDashboard.Dock = DockStyle.Fill;
                    panel1.Controls.Add(InstructorDashboard);
                }
                else
                {
                    InstructorDashboard.BringToFront();
                }
            }
            else if (userRole == "admin")
            {
                if (WorkingStudentDashboard == null || WorkingStudentDashboard.IsDisposed)
                {
                    WorkingStudentDashboard = new WorkingStudentDashboard();
                    WorkingStudentDashboard.Dock = DockStyle.Fill;
                    panel1.Controls.Add(WorkingStudentDashboard);
                }
                else
                {
                    WorkingStudentDashboard.BringToFront();
                }
            }
            else if (userRole == "superadmin")
            {
                if (SupervisorDashboard == null || SupervisorDashboard.IsDisposed)
                {
                    SupervisorDashboard = new SupervisorDashboard(Firstname, Lastname, userRole);
                    SupervisorDashboard.Dock = DockStyle.Fill;
                    panel1.Controls.Add(SupervisorDashboard);
                }
                else
                {
                    SupervisorDashboard.BringToFront();
                }
            }
            else
            {
                // Handle unknown role
                MessageBox.Show("Unknown role detected!");
            }
        }
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

      
       

        //-------------------------------------------        

        bool registrationExpand = false;
        private void btnRegistrationSystem_Click(object sender, EventArgs e)
        {
            registrationSystemTransition.Start();
        }
        private void registrationSystemTransition_Tick(object sender, EventArgs e)
        {
            if (registrationExpand == false)
            {
                registrationContainer.Height += 80;
                if (registrationContainer.Height >= 210)
                {
                    registrationContainer.AutoSize = true;
                    registrationSystemTransition.Stop();
                    registrationExpand = true;
                }
            }
            else
            {
                registrationContainer.AutoSize = false;

                registrationContainer.Height -= 80;
                if (registrationContainer.Height <= 80)
                {
                    registrationSystemTransition.Stop();
                    registrationExpand = false;
                }
            }
        }
     
        //-------------------------------------------
        bool enrollmentExpand = false;
        private void btnEnrollmentSystem_Click(object sender, EventArgs e)
        {
            enrollmentSystemTransition.Start();
        }
        private void enrollmentSystemTransition_Tick(object sender, EventArgs e)
        {
            if (enrollmentExpand == false)
            {
                enrollmentContainer.Height += 80;
                if (enrollmentContainer.Height >= 210)
                {
                    enrollmentContainer.AutoSize = true;
                    enrollmentSystemTransition.Stop();
                    enrollmentExpand = true;
                }
            }
            else
            {
                enrollmentContainer.AutoSize = false;

                enrollmentContainer.Height -= 80;
                if (enrollmentContainer.Height <= 80)
                {
                    enrollmentSystemTransition.Stop();
                    enrollmentExpand = false;
                }
            }
        }
        //-------------------------------------------

        private void btnAttendanceOverride_Click(object sender, EventArgs e)
        {
            if (OverrideAttendances == null || OverrideAttendances.IsDisposed)
            {
                OverrideAttendances = new OverrideAttendances();
                OverrideAttendances.Dock = DockStyle.Fill;
                panel1.Controls.Add(OverrideAttendances);
            }
            else
            {
                OverrideAttendances.BringToFront();
            }
        }

        private void btnSubjectCreation_Click(object sender, EventArgs e)
        {
            if (subjectCreationSystem == null || subjectCreationSystem.IsDisposed)
            {
                subjectCreationSystem = new subjectCreationSystem();
                subjectCreationSystem.Dock = DockStyle.Fill;
                panel1.Controls.Add(subjectCreationSystem);
            }
            else
            {
                subjectCreationSystem.BringToFront();
            }
        }
        private void btnSubjectEnrollment_Click(object sender, EventArgs e)
        {
            if (subjectEnrollmentSystem == null || subjectEnrollmentSystem.IsDisposed)
            {
                subjectEnrollmentSystem = new subjectEnrollmentSystem(Firstname, Lastname, userRole);
                subjectEnrollmentSystem.Dock = DockStyle.Fill;
                panel1.Controls.Add(subjectEnrollmentSystem);
            }
            else
            {
                subjectEnrollmentSystem.BringToFront();
            }
        }

        private void btnClassList_Click(object sender, EventArgs e)
        {
            if (InstructorClassList == null || InstructorClassList.IsDisposed)
            {
                InstructorClassList = new InstructorClassList(Firstname, Lastname, userRole);
                InstructorClassList.Dock = DockStyle.Fill;
                panel1.Controls.Add(InstructorClassList);
            }
            else
            {
                InstructorClassList.BringToFront();
            }
        }

        private void btnAuditSystem_Click(object sender, EventArgs e)
        {

            if (AuditSystem == null || AuditSystem.IsDisposed)
            {
                AuditSystem = new AuditSystem();
                AuditSystem.Dock = DockStyle.Fill;
                panel1.Controls.Add(AuditSystem);
            }
            else
            {
                AuditSystem.BringToFront();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Check if the registerAccount control is null or disposed
            if (userAccountSystem == null || userAccountSystem.IsDisposed)
            {
                // Create a new instance of RegisterAccount
                userAccountSystem = new userAccountSystem();

                // Dock the RegisterAccount control within panel1
                userAccountSystem.Dock = DockStyle.Fill;

                // Add the RegisterAccount control to panel1
                panel1.Controls.Add(userAccountSystem);
            }
            else
            {
                // If the RegisterAccount control already exists, bring it to the front
                userAccountSystem.BringToFront();
            }
        }

        private void btnStudentRegister_Click(object sender, EventArgs e)
        {
            if (studentAccountSystem == null || studentAccountSystem.IsDisposed)
            {
                studentAccountSystem = new studentAccountSystem();
                studentAccountSystem.Dock = DockStyle.Fill;
                panel1.Controls.Add(studentAccountSystem);
            }
            else
            {
                studentAccountSystem.BringToFront();
            }
        }

        private void btnSubjectAssignment_Click(object sender, EventArgs e)
        {
            if (subjectInstructorAssignment == null || subjectInstructorAssignment.IsDisposed)
            {
                subjectInstructorAssignment = new subjectInstructorAssignment();
                subjectInstructorAssignment.Dock = DockStyle.Fill;
                panel1.Controls.Add(subjectInstructorAssignment);
            }
            else
            {
                subjectInstructorAssignment.BringToFront();
            }
        }

        private void btnAttendanceSummary_Click(object sender, EventArgs e)
        {
            if (studentAttendanceSummary == null || studentAttendanceSummary.IsDisposed)
            {
                studentAttendanceSummary = new studentAttendanceSummary();
                studentAttendanceSummary.Dock = DockStyle.Fill;
                panel1.Controls.Add(studentAttendanceSummary);
            }
            else
            {
                studentAttendanceSummary.BringToFront();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (Form1 == null || Form1.IsDisposed)
            {
                Form1 = new Form1();
                Form1.Dock = DockStyle.Fill;
                Form1.Show();
                this.Close();
            }
            else
            {
                Form1.BringToFront();
            }
        }
    }
}
