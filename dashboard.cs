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
using testing.Attendance_Override_System;
using testing.Registration_System.Student_Account_System;
using testing.Subject_Creation_System;
using testing.Subject_System;

namespace testing
{
    public partial class dashboard : Form
    {
        private userAccountSystem userAccountSystem;
        private studentAccountSystem studentAccountSystem;
        private subjectCreationSystem subjectCreationSystem;
        private subjectEnrollmentSystem subjectEnrollmentSystem;
        private attendanceOverrideSystem attendanceOverrideSystem;
        private Form1 Form1;

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        public dashboard()
        {
            InitializeComponent();
            panel2.MouseDown += Panel2_MouseDown;
            panel2.MouseMove += Panel2_MouseMove;
            panel2.MouseUp += Panel2_MouseUp;
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
                subjectEnrollmentSystem = new subjectEnrollmentSystem();
                subjectEnrollmentSystem.Dock = DockStyle.Fill;
                panel1.Controls.Add(subjectEnrollmentSystem);
            }
            else
            {
                subjectEnrollmentSystem.BringToFront();
            }
        }

        private void btnAttendanceOverrideSystem_Click(object sender, EventArgs e)
        {
            if (attendanceOverrideSystem == null || attendanceOverrideSystem.IsDisposed)
            {
                attendanceOverrideSystem = new attendanceOverrideSystem();
                attendanceOverrideSystem.Dock = DockStyle.Fill;
                panel1.Controls.Add(attendanceOverrideSystem);
            }
            else
            {
                attendanceOverrideSystem.BringToFront();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1 == null || Form1.IsDisposed)
            {
                Form1 = new Form1();
                Form1.Dock = DockStyle.Fill;
                Form1.Show();
                this.Hide();
            }
            else
            {
                Form1.BringToFront();
            }
        }

        private void btnRegistrationSystem_Click(object sender, EventArgs e)
        {
            registrationSystemTransition.Start();
        }

        bool registrationExpand = false;
        private void registrationSystemTransition_Tick(object sender, EventArgs e)
        {
            if (registrationExpand == false)
            {
                registrationContainer.Height += 50;
                if (registrationContainer.Height >= 150)
                {
                    registrationSystemTransition.Stop();
                    registrationExpand = true;
                }
            }
            else
            {
                registrationContainer.Height -= 50;
                if (registrationContainer.Height <= 50)
                {
                    registrationSystemTransition.Stop();
                    registrationExpand = false;
                }
            }
        }

        private void btnEnrollmentSystem_Click(object sender, EventArgs e)
        {
            enrollmentSystemTransition.Start();
        }

        bool enrollmentExpand = false;
        private void enrollmentSystemTransition_Tick(object sender, EventArgs e)
        {
            if (enrollmentExpand == false)
            {
                enrollmentContainer.Height += 50;
                if (enrollmentContainer.Height >= 150)
                {
                    enrollmentSystemTransition.Stop();
                    enrollmentExpand = true;
                }
            }
            else
            {
                enrollmentContainer.Height -= 50;
                if (enrollmentContainer.Height <= 50)
                {
                    enrollmentSystemTransition.Stop();
                    enrollmentExpand = false;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            dashboard dashboard = new dashboard();
            dashboard.Show();
        }
    }
}
