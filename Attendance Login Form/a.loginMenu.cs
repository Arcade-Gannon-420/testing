using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testing.laboratory_Supervisor_Login;

namespace testing.Attendance_Login_Form
{
    public partial class loginMenu : UserControl
    {
        private laboratorySupervisorLogin laboratorySupervisorLogin;

        public loginMenu()
        {
            InitializeComponent();
        }

        private void btnLabSup_Click(object sender, EventArgs e)
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

        private void btnInstructor_Click(object sender, EventArgs e)
        {

        }

        private void btnWorkingStudent_Click(object sender, EventArgs e)
        {

        }



    }
}
