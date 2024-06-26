using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace testing.Audit_System
{
    public partial class AuditSystem : UserControl
    {
        SqlConnection connectionString = new SqlConnection(@"Data Source = DESKTOP-SLBI6LR\MSSQLSERVER2024;Initial Catalog = FinalDb;Integrated Security=True");


        public AuditSystem()
        {
            InitializeComponent();
        }


        private void FetchAttendanceData()
        {
            
            DateTime startDate = dateTimePickerStart.Value.Date;
            DateTime endDate = dateTimePickerEnd.Value.Date;

            // Adjust the endDate to include the whole day
            endDate = endDate.AddDays(1).AddTicks(-1);

            string edpCode = txtEDPCode.Text.Trim();

            if (string.IsNullOrWhiteSpace(edpCode))
            {
                MessageBox.Show("Please enter a valid EDPCode.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(edpCode, out int edpCodeInt))
            {
                MessageBox.Show("Invalid EDPCode format. Please enter a valid integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dt = new DataTable();
                
            using (SqlCommand command = new SqlCommand("GetAuditAttendanceSummary", connectionString))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@startDate", SqlDbType.Date).Value = startDate;
                command.Parameters.Add("@endDate", SqlDbType.Date).Value = endDate;
                command.Parameters.Add("@EDPCode", SqlDbType.Int).Value = edpCodeInt;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }

            ReportDataSource datasource = new ReportDataSource("DataSet_Report2", dt);
            reportViewer2.LocalReport.DataSources.Clear();
            reportViewer2.LocalReport.DataSources.Add(datasource);
            reportViewer2.RefreshReport();
        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            FetchAttendanceData();
        }

        private void FetchSubjectEnrollmentData()
        {
            string edpCode = txtEDPCode2.Text.Trim();

            if (string.IsNullOrWhiteSpace(edpCode))
            {
                MessageBox.Show("Please enter a valid EDPCode.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(edpCode, out int edpCodeInt))
            {
                MessageBox.Show("Invalid EDPCode format. Please enter a valid integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dt = new DataTable();
      
            using (SqlCommand command = new SqlCommand("GetAuditEnrolledStudents", connectionString))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@EDPCode", SqlDbType.Int).Value = edpCodeInt;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }

            ReportDataSource datasource = new ReportDataSource("DataSet_Report", dt);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(datasource);
            reportViewer1.RefreshReport();
        }

        private void btnStudentsGenerate_Click(object sender, EventArgs e)
        {
            FetchSubjectEnrollmentData();            
        }





        private void AuditSystem_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            reportViewer1.ShowExportButton = true;

            this.reportViewer2.RefreshReport();
            reportViewer2.ShowExportButton = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtEDPCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }