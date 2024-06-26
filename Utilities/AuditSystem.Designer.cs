namespace testing.Audit_System
{
    partial class AuditSystem
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditSystem));
            this.GetAuditAttendanceSummaryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.finalDbDataSet = new testing.FinalDbDataSet();
            this.getAuditEnrolledStudentsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel10 = new System.Windows.Forms.Panel();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnScanGenerate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEDPCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerEnd = new ReaLTaiizor.Controls.PoisonDateTime();
            this.dateTimePickerStart = new ReaLTaiizor.Controls.PoisonDateTime();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnStudentsGenerate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEDPCode2 = new System.Windows.Forms.TextBox();
            this.getAuditEnrolledStudentsTableAdapter = new testing.FinalDbDataSetTableAdapters.GetAuditEnrolledStudentsTableAdapter();
            this.finalDbDataSet1 = new testing.FinalDbDataSet();
            this.getAuditAttendanceSummaryBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.getAuditAttendanceSummaryTableAdapter = new testing.FinalDbDataSetTableAdapters.GetAuditAttendanceSummaryTableAdapter();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.GetAuditAttendanceSummaryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalDbDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getAuditEnrolledStudentsBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.finalDbDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getAuditAttendanceSummaryBindingSource1)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // GetAuditAttendanceSummaryBindingSource
            // 
            this.GetAuditAttendanceSummaryBindingSource.DataMember = "GetAuditAttendanceSummary";
            this.GetAuditAttendanceSummaryBindingSource.DataSource = this.finalDbDataSet;
            // 
            // finalDbDataSet
            // 
            this.finalDbDataSet.DataSetName = "FinalDbDataSet";
            this.finalDbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // getAuditEnrolledStudentsBindingSource
            // 
            this.getAuditEnrolledStudentsBindingSource.DataMember = "GetAuditEnrolledStudents";
            this.getAuditEnrolledStudentsBindingSource.DataSource = this.finalDbDataSet;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(30, 80);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1235, 739);
            this.panel1.TabIndex = 139;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(30, 21);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1175, 688);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel10);
            this.tabPage1.Controls.Add(this.reportViewer2);
            this.tabPage1.Controls.Add(this.btnScanGenerate);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtEDPCode);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dateTimePickerEnd);
            this.tabPage1.Controls.Add(this.dateTimePickerStart);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1167, 662);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SCAN HISTORY";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.AliceBlue;
            this.panel10.Location = new System.Drawing.Point(0, 632);
            this.panel10.Margin = new System.Windows.Forms.Padding(0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(30, 30);
            this.panel10.TabIndex = 10;
            // 
            // reportViewer2
            // 
            reportDataSource1.Name = "DataSet_Report2";
            reportDataSource1.Value = this.GetAuditAttendanceSummaryBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "testing.Utilities.AttendanceSummaryReport.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(30, 83);
            this.reportViewer2.Margin = new System.Windows.Forms.Padding(0);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.ServerReport.BearerToken = null;
            this.reportViewer2.Size = new System.Drawing.Size(1107, 549);
            this.reportViewer2.TabIndex = 9;
            // 
            // btnScanGenerate
            // 
            this.btnScanGenerate.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScanGenerate.Location = new System.Drawing.Point(898, 34);
            this.btnScanGenerate.Name = "btnScanGenerate";
            this.btnScanGenerate.Size = new System.Drawing.Size(116, 31);
            this.btnScanGenerate.TabIndex = 7;
            this.btnScanGenerate.Text = "Generate";
            this.btnScanGenerate.UseVisualStyleBackColor = true;
            this.btnScanGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "EDP CODE";
            // 
            // txtEDPCode
            // 
            this.txtEDPCode.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEDPCode.Location = new System.Drawing.Point(137, 37);
            this.txtEDPCode.Name = "txtEDPCode";
            this.txtEDPCode.Size = new System.Drawing.Size(191, 30);
            this.txtEDPCode.TabIndex = 4;
            this.txtEDPCode.TextChanged += new System.EventHandler(this.txtEDPCode_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(640, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "TO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(350, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "FROM";
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(679, 36);
            this.dateTimePickerEnd.MinimumSize = new System.Drawing.Size(0, 29);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(200, 29);
            this.dateTimePickerEnd.TabIndex = 1;
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerStart.Location = new System.Drawing.Point(419, 36);
            this.dateTimePickerStart.MinimumSize = new System.Drawing.Size(0, 29);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(200, 29);
            this.dateTimePickerStart.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.reportViewer1);
            this.tabPage2.Controls.Add(this.btnStudentsGenerate);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtEDPCode2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1167, 662);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CLASS LIST";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            reportDataSource2.Name = "DataSet_Report";
            reportDataSource2.Value = this.getAuditEnrolledStudentsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "testing.Utilities.SubjectEnrollmentReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(30, 84);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1107, 549);
            this.reportViewer1.TabIndex = 19;
            // 
            // btnStudentsGenerate
            // 
            this.btnStudentsGenerate.Font = new System.Drawing.Font("Roboto", 14.25F);
            this.btnStudentsGenerate.Location = new System.Drawing.Point(354, 36);
            this.btnStudentsGenerate.Name = "btnStudentsGenerate";
            this.btnStudentsGenerate.Size = new System.Drawing.Size(116, 31);
            this.btnStudentsGenerate.TabIndex = 14;
            this.btnStudentsGenerate.Text = "Generate";
            this.btnStudentsGenerate.UseVisualStyleBackColor = true;
            this.btnStudentsGenerate.Click += new System.EventHandler(this.btnStudentsGenerate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(35, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "EDP CODE";
            // 
            // txtEDPCode2
            // 
            this.txtEDPCode2.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEDPCode2.Location = new System.Drawing.Point(137, 37);
            this.txtEDPCode2.Name = "txtEDPCode2";
            this.txtEDPCode2.Size = new System.Drawing.Size(191, 30);
            this.txtEDPCode2.TabIndex = 12;
            // 
            // getAuditEnrolledStudentsTableAdapter
            // 
            this.getAuditEnrolledStudentsTableAdapter.ClearBeforeFill = true;
            // 
            // finalDbDataSet1
            // 
            this.finalDbDataSet1.DataSetName = "FinalDbDataSet";
            this.finalDbDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // getAuditAttendanceSummaryBindingSource1
            // 
            this.getAuditAttendanceSummaryBindingSource1.DataMember = "GetAuditAttendanceSummary";
            this.getAuditAttendanceSummaryBindingSource1.DataSource = this.finalDbDataSet1;
            // 
            // getAuditAttendanceSummaryTableAdapter
            // 
            this.getAuditAttendanceSummaryTableAdapter.ClearBeforeFill = true;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1275, 80);
            this.panel8.TabIndex = 146;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label10);
            this.panel9.Controls.Add(this.pictureBox4);
            this.panel9.Location = new System.Drawing.Point(22, 16);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(467, 50);
            this.panel9.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(42, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(157, 28);
            this.label10.TabIndex = 16;
            this.label10.Text = "AUDIT SYSTEM";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.White;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(19, 15);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 20);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 15;
            this.pictureBox4.TabStop = false;
            // 
            // AuditSystem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(236)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AuditSystem";
            this.Size = new System.Drawing.Size(1275, 869);
            this.Load += new System.EventHandler(this.AuditSystem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GetAuditAttendanceSummaryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalDbDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getAuditEnrolledStudentsBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.finalDbDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getAuditAttendanceSummaryBindingSource1)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ReaLTaiizor.Controls.PoisonDateTime dateTimePickerEnd;
        private ReaLTaiizor.Controls.PoisonDateTime dateTimePickerStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEDPCode;
        private System.Windows.Forms.Button btnScanGenerate;
        private System.Windows.Forms.Button btnStudentsGenerate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEDPCode2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource getAuditEnrolledStudentsBindingSource;
        private FinalDbDataSet finalDbDataSet;
        private FinalDbDataSetTableAdapters.GetAuditEnrolledStudentsTableAdapter getAuditEnrolledStudentsTableAdapter;
        private System.Windows.Forms.BindingSource GetAuditAttendanceSummaryBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.BindingSource getAuditAttendanceSummaryBindingSource1;
        private FinalDbDataSet finalDbDataSet1;
        private FinalDbDataSetTableAdapters.GetAuditAttendanceSummaryTableAdapter getAuditAttendanceSummaryTableAdapter;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel10;
    }
}
