namespace testing
{
    partial class OverrideAttendances
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverrideAttendances));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.attendanceIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iDNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.firstnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subjectCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scheduleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeInDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeOutDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enrollmentStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.attendanceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.finalDbDataSet = new testing.FinalDbDataSet();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SubjectCode = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.DateTimePicker();
            this.AttendaceSummary = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.Status = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAbsent = new System.Windows.Forms.Button();
            this.txtAttendanceID = new Guna.UI2.WinForms.Guna2TextBox();
            this.subEndTime = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.subStartTime = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.attendanceTableAdapter = new testing.FinalDbDataSetTableAdapters.AttendanceTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalDbDataSet)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.attendanceIDDataGridViewTextBoxColumn,
            this.iDNumberDataGridViewTextBoxColumn,
            this.firstnameDataGridViewTextBoxColumn,
            this.lastnameDataGridViewTextBoxColumn,
            this.subjectCodeDataGridViewTextBoxColumn,
            this.scheduleDataGridViewTextBoxColumn,
            this.dateDataGridViewTextBoxColumn,
            this.timeInDataGridViewTextBoxColumn,
            this.timeOutDataGridViewTextBoxColumn,
            this.enrollmentStatusDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.attendanceBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.PowderBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.GridColor = System.Drawing.Color.PowderBlue;
            this.dataGridView1.Location = new System.Drawing.Point(60, 158);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1155, 446);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // attendanceIDDataGridViewTextBoxColumn
            // 
            this.attendanceIDDataGridViewTextBoxColumn.DataPropertyName = "AttendanceID";
            this.attendanceIDDataGridViewTextBoxColumn.HeaderText = "AttendanceID";
            this.attendanceIDDataGridViewTextBoxColumn.Name = "attendanceIDDataGridViewTextBoxColumn";
            this.attendanceIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iDNumberDataGridViewTextBoxColumn
            // 
            this.iDNumberDataGridViewTextBoxColumn.DataPropertyName = "IDNumber";
            this.iDNumberDataGridViewTextBoxColumn.HeaderText = "IDNumber";
            this.iDNumberDataGridViewTextBoxColumn.Name = "iDNumberDataGridViewTextBoxColumn";
            this.iDNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstnameDataGridViewTextBoxColumn
            // 
            this.firstnameDataGridViewTextBoxColumn.DataPropertyName = "Firstname";
            this.firstnameDataGridViewTextBoxColumn.HeaderText = "Firstname";
            this.firstnameDataGridViewTextBoxColumn.Name = "firstnameDataGridViewTextBoxColumn";
            this.firstnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastnameDataGridViewTextBoxColumn
            // 
            this.lastnameDataGridViewTextBoxColumn.DataPropertyName = "Lastname";
            this.lastnameDataGridViewTextBoxColumn.HeaderText = "Lastname";
            this.lastnameDataGridViewTextBoxColumn.Name = "lastnameDataGridViewTextBoxColumn";
            this.lastnameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // subjectCodeDataGridViewTextBoxColumn
            // 
            this.subjectCodeDataGridViewTextBoxColumn.DataPropertyName = "SubjectCode";
            this.subjectCodeDataGridViewTextBoxColumn.HeaderText = "SubjectCode";
            this.subjectCodeDataGridViewTextBoxColumn.Name = "subjectCodeDataGridViewTextBoxColumn";
            this.subjectCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // scheduleDataGridViewTextBoxColumn
            // 
            this.scheduleDataGridViewTextBoxColumn.DataPropertyName = "Schedule";
            this.scheduleDataGridViewTextBoxColumn.HeaderText = "Schedule";
            this.scheduleDataGridViewTextBoxColumn.Name = "scheduleDataGridViewTextBoxColumn";
            this.scheduleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateDataGridViewTextBoxColumn
            // 
            this.dateDataGridViewTextBoxColumn.DataPropertyName = "Date";
            this.dateDataGridViewTextBoxColumn.HeaderText = "Date";
            this.dateDataGridViewTextBoxColumn.Name = "dateDataGridViewTextBoxColumn";
            this.dateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // timeInDataGridViewTextBoxColumn
            // 
            this.timeInDataGridViewTextBoxColumn.DataPropertyName = "TimeIn";
            this.timeInDataGridViewTextBoxColumn.HeaderText = "TimeIn";
            this.timeInDataGridViewTextBoxColumn.Name = "timeInDataGridViewTextBoxColumn";
            this.timeInDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // timeOutDataGridViewTextBoxColumn
            // 
            this.timeOutDataGridViewTextBoxColumn.DataPropertyName = "TimeOut";
            this.timeOutDataGridViewTextBoxColumn.HeaderText = "TimeOut";
            this.timeOutDataGridViewTextBoxColumn.Name = "timeOutDataGridViewTextBoxColumn";
            this.timeOutDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // enrollmentStatusDataGridViewTextBoxColumn
            // 
            this.enrollmentStatusDataGridViewTextBoxColumn.DataPropertyName = "enrollmentStatus";
            this.enrollmentStatusDataGridViewTextBoxColumn.HeaderText = "enrollmentStatus";
            this.enrollmentStatusDataGridViewTextBoxColumn.Name = "enrollmentStatusDataGridViewTextBoxColumn";
            this.enrollmentStatusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // attendanceBindingSource
            // 
            this.attendanceBindingSource.DataMember = "Attendance";
            this.attendanceBindingSource.DataSource = this.finalDbDataSet;
            // 
            // finalDbDataSet
            // 
            this.finalDbDataSet.DataSetName = "FinalDbDataSet";
            this.finalDbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(319, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(32, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "EDP CODE";
            // 
            // SubjectCode
            // 
            this.SubjectCode.Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubjectCode.Location = new System.Drawing.Point(134, 26);
            this.SubjectCode.Name = "SubjectCode";
            this.SubjectCode.Size = new System.Drawing.Size(172, 30);
            this.SubjectCode.TabIndex = 9;
            // 
            // Start
            // 
            this.Start.Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Start.Location = new System.Drawing.Point(372, 26);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(171, 30);
            this.Start.TabIndex = 7;
            // 
            // AttendaceSummary
            // 
            this.AttendaceSummary.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.AttendaceSummary.Location = new System.Drawing.Point(563, 24);
            this.AttendaceSummary.Name = "AttendaceSummary";
            this.AttendaceSummary.Size = new System.Drawing.Size(119, 35);
            this.AttendaceSummary.TabIndex = 13;
            this.AttendaceSummary.Text = "SEARCH";
            this.AttendaceSummary.UseVisualStyleBackColor = true;
            this.AttendaceSummary.Click += new System.EventHandler(this.AttendaceSummary_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Location = new System.Drawing.Point(282, 674);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(119, 35);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Status
            // 
            this.Status.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status.FormattingEnabled = true;
            this.Status.Items.AddRange(new object[] {
            "Present",
            "Late"});
            this.Status.Location = new System.Drawing.Point(145, 549);
            this.Status.Margin = new System.Windows.Forms.Padding(5);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(200, 30);
            this.Status.TabIndex = 53;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(61, 552);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 25);
            this.label9.TabIndex = 52;
            this.label9.Text = "STATUS";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(44, 632);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 25);
            this.label8.TabIndex = 49;
            this.label8.Text = "TIME OUT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Roboto", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(62, 592);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 25);
            this.label4.TabIndex = 46;
            this.label4.Text = "TIME IN";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.Location = new System.Drawing.Point(145, 674);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(119, 35);
            this.btnUpdate.TabIndex = 12;
            this.btnUpdate.Text = "UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Margin = new System.Windows.Forms.Padding(0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1275, 80);
            this.panel8.TabIndex = 148;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label10);
            this.panel9.Controls.Add(this.pictureBox4);
            this.panel9.Location = new System.Drawing.Point(22, 16);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(500, 50);
            this.panel9.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Roboto", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(42, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(247, 28);
            this.label10.TabIndex = 16;
            this.label10.Text = "ATTENDANCE OVERRIDE\r\n";
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Controls.Add(this.btnAbsent);
            this.panel1.Controls.Add(this.txtAttendanceID);
            this.panel1.Controls.Add(this.subEndTime);
            this.panel1.Controls.Add(this.subStartTime);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.Start);
            this.panel1.Controls.Add(this.SubjectCode);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.AttendaceSummary);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.Status);
            this.panel1.Location = new System.Drawing.Point(30, 80);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1215, 739);
            this.panel1.TabIndex = 149;
            // 
            // btnAbsent
            // 
            this.btnAbsent.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnAbsent.Location = new System.Drawing.Point(419, 674);
            this.btnAbsent.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.btnAbsent.Name = "btnAbsent";
            this.btnAbsent.Size = new System.Drawing.Size(119, 35);
            this.btnAbsent.TabIndex = 150;
            this.btnAbsent.Text = "ABSENT";
            this.btnAbsent.UseVisualStyleBackColor = true;
            this.btnAbsent.Click += new System.EventHandler(this.btnAbsent_Click);
            // 
            // txtAttendanceID
            // 
            this.txtAttendanceID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAttendanceID.DefaultText = "";
            this.txtAttendanceID.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtAttendanceID.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtAttendanceID.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAttendanceID.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtAttendanceID.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAttendanceID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtAttendanceID.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtAttendanceID.Location = new System.Drawing.Point(928, 581);
            this.txtAttendanceID.Name = "txtAttendanceID";
            this.txtAttendanceID.PasswordChar = '\0';
            this.txtAttendanceID.PlaceholderText = "";
            this.txtAttendanceID.SelectedText = "";
            this.txtAttendanceID.Size = new System.Drawing.Size(200, 36);
            this.txtAttendanceID.TabIndex = 58;
            this.txtAttendanceID.Visible = false;
            // 
            // subEndTime
            // 
            this.subEndTime.Checked = true;
            this.subEndTime.FillColor = System.Drawing.Color.White;
            this.subEndTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.subEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.subEndTime.Location = new System.Drawing.Point(145, 632);
            this.subEndTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.subEndTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.subEndTime.Name = "subEndTime";
            this.subEndTime.ShowUpDown = true;
            this.subEndTime.Size = new System.Drawing.Size(200, 30);
            this.subEndTime.TabIndex = 57;
            this.subEndTime.Value = new System.DateTime(2024, 6, 26, 5, 32, 53, 340);
            // 
            // subStartTime
            // 
            this.subStartTime.Checked = true;
            this.subStartTime.FillColor = System.Drawing.Color.White;
            this.subStartTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.subStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.subStartTime.Location = new System.Drawing.Point(145, 592);
            this.subStartTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.subStartTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.subStartTime.Name = "subStartTime";
            this.subStartTime.ShowUpDown = true;
            this.subStartTime.Size = new System.Drawing.Size(200, 30);
            this.subStartTime.TabIndex = 56;
            this.subStartTime.Value = new System.DateTime(2024, 6, 26, 5, 32, 53, 340);
            // 
            // attendanceTableAdapter
            // 
            this.attendanceTableAdapter.ClearBeforeFill = true;
            // 
            // OverrideAttendances
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel1);
            this.Name = "OverrideAttendances";
            this.Size = new System.Drawing.Size(1275, 869);
            this.Load += new System.EventHandler(this.OverrideAttendances_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.attendanceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.finalDbDataSet)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SubjectCode;
        private System.Windows.Forms.DateTimePicker Start;
        private System.Windows.Forms.Button AttendaceSummary;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ComboBox Status;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2DateTimePicker subStartTime;
        private Guna.UI2.WinForms.Guna2DateTimePicker subEndTime;
        private System.Windows.Forms.BindingSource attendanceBindingSource;
        private FinalDbDataSet finalDbDataSet;
        private FinalDbDataSetTableAdapters.AttendanceTableAdapter attendanceTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn attendanceIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastnameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subjectCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scheduleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeInDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeOutDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn enrollmentStatusDataGridViewTextBoxColumn;
        private Guna.UI2.WinForms.Guna2TextBox txtAttendanceID;
        private System.Windows.Forms.Button btnAbsent;
    }
}
