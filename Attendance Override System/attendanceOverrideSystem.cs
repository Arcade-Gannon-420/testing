using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testing.Attendance_Override_System.Override_Buttons;


namespace testing.Attendance_Override_System
{
    public partial class attendanceOverrideSystem : UserControl
    {
        private overrideSender overrideSender;

        public attendanceOverrideSystem()
        {
            InitializeComponent();
        }

    
        private void btnOverride_Click(object sender, EventArgs e)
        {
            if (overrideSender == null || overrideSender.IsDisposed)
            {
                // Create a new instance of RegisterAccount
                overrideSender = new overrideSender();

                // Dock the RegisterAccount control within panel1
                overrideSender.Dock = DockStyle.Fill;

                // Add the RegisterAccount control to panel1
                panel5.Controls.Add(overrideSender);
            }
            else
            {
                // If the RegisterAccount control already exists, bring it to the front
                overrideSender.BringToFront();
            }
        }

    }
}
