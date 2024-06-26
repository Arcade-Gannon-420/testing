using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing.Print_Subject_Enrollment_Report
{
    public partial class PrintSubjectEnrollmentReport : Form
    {
        public PrintSubjectEnrollmentReport()
        {
            InitializeComponent();
        }

        private void PrintSubjectEnrollmentReport_Load(object sender, EventArgs e)
        {

        }

        private void PrintSubjectEnrollmentReport_Load_1(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
