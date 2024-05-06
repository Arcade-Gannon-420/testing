namespace testing
{
    partial class dashboard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dashboard));
            this.btnRegister = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAttendanceOverrideSystem = new System.Windows.Forms.Button();
            this.registrationContainer = new System.Windows.Forms.Panel();
            this.btnStudentRegister = new System.Windows.Forms.Button();
            this.btnRegistrationSystem = new System.Windows.Forms.Button();
            this.enrollmentContainer = new System.Windows.Forms.Panel();
            this.btnSubjectCreation = new System.Windows.Forms.Button();
            this.btnSubjectEnrollment = new System.Windows.Forms.Button();
            this.btnEnrollmentSystem = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.nightControlBox1 = new ReaLTaiizor.Controls.NightControlBox();
            this.registrationSystemTransition = new System.Windows.Forms.Timer(this.components);
            this.enrollmentSystemTransition = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.registrationContainer.SuspendLayout();
            this.enrollmentContainer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRegister
            // 
            this.btnRegister.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(108)))), ((int)(((byte)(164)))));
            this.btnRegister.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(0, 53);
            this.btnRegister.Margin = new System.Windows.Forms.Padding(0);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(225, 50);
            this.btnRegister.TabIndex = 0;
            this.btnRegister.Text = "User Account";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(108)))), ((int)(((byte)(164)))));
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Controls.Add(this.btnAttendanceOverrideSystem);
            this.flowLayoutPanel1.Controls.Add(this.registrationContainer);
            this.flowLayoutPanel1.Controls.Add(this.enrollmentContainer);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 31);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(225, 869);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(225, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 153);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(222, 50);
            this.button2.TabIndex = 8;
            this.button2.Text = "Attendance Override Receiver";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnAttendanceOverrideSystem
            // 
            this.btnAttendanceOverrideSystem.Location = new System.Drawing.Point(3, 209);
            this.btnAttendanceOverrideSystem.Name = "btnAttendanceOverrideSystem";
            this.btnAttendanceOverrideSystem.Size = new System.Drawing.Size(222, 50);
            this.btnAttendanceOverrideSystem.TabIndex = 7;
            this.btnAttendanceOverrideSystem.Text = "Attendance Override System";
            this.btnAttendanceOverrideSystem.UseVisualStyleBackColor = true;
            this.btnAttendanceOverrideSystem.Click += new System.EventHandler(this.btnAttendanceOverrideSystem_Click);
            // 
            // registrationContainer
            // 
            this.registrationContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(108)))), ((int)(((byte)(164)))));
            this.registrationContainer.Controls.Add(this.btnStudentRegister);
            this.registrationContainer.Controls.Add(this.btnRegister);
            this.registrationContainer.Controls.Add(this.btnRegistrationSystem);
            this.registrationContainer.Location = new System.Drawing.Point(0, 262);
            this.registrationContainer.Margin = new System.Windows.Forms.Padding(0);
            this.registrationContainer.Name = "registrationContainer";
            this.registrationContainer.Size = new System.Drawing.Size(225, 50);
            this.registrationContainer.TabIndex = 2;
            // 
            // btnStudentRegister
            // 
            this.btnStudentRegister.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStudentRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(108)))), ((int)(((byte)(164)))));
            this.btnStudentRegister.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnStudentRegister.FlatAppearance.BorderSize = 0;
            this.btnStudentRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudentRegister.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStudentRegister.ForeColor = System.Drawing.Color.White;
            this.btnStudentRegister.Location = new System.Drawing.Point(0, 103);
            this.btnStudentRegister.Margin = new System.Windows.Forms.Padding(0);
            this.btnStudentRegister.Name = "btnStudentRegister";
            this.btnStudentRegister.Size = new System.Drawing.Size(225, 50);
            this.btnStudentRegister.TabIndex = 3;
            this.btnStudentRegister.Text = "Student Account";
            this.btnStudentRegister.UseVisualStyleBackColor = false;
            this.btnStudentRegister.Click += new System.EventHandler(this.btnStudentRegister_Click);
            // 
            // btnRegistrationSystem
            // 
            this.btnRegistrationSystem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRegistrationSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(183)))), ((int)(((byte)(233)))));
            this.btnRegistrationSystem.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnRegistrationSystem.FlatAppearance.BorderSize = 0;
            this.btnRegistrationSystem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrationSystem.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrationSystem.ForeColor = System.Drawing.Color.White;
            this.btnRegistrationSystem.Location = new System.Drawing.Point(0, 0);
            this.btnRegistrationSystem.Margin = new System.Windows.Forms.Padding(0);
            this.btnRegistrationSystem.Name = "btnRegistrationSystem";
            this.btnRegistrationSystem.Size = new System.Drawing.Size(225, 50);
            this.btnRegistrationSystem.TabIndex = 4;
            this.btnRegistrationSystem.Text = "Registration System";
            this.btnRegistrationSystem.UseVisualStyleBackColor = false;
            this.btnRegistrationSystem.Click += new System.EventHandler(this.btnRegistrationSystem_Click);
            // 
            // enrollmentContainer
            // 
            this.enrollmentContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(108)))), ((int)(((byte)(164)))));
            this.enrollmentContainer.Controls.Add(this.btnSubjectCreation);
            this.enrollmentContainer.Controls.Add(this.btnSubjectEnrollment);
            this.enrollmentContainer.Controls.Add(this.btnEnrollmentSystem);
            this.enrollmentContainer.Location = new System.Drawing.Point(0, 312);
            this.enrollmentContainer.Margin = new System.Windows.Forms.Padding(0);
            this.enrollmentContainer.Name = "enrollmentContainer";
            this.enrollmentContainer.Size = new System.Drawing.Size(225, 50);
            this.enrollmentContainer.TabIndex = 7;
            // 
            // btnSubjectCreation
            // 
            this.btnSubjectCreation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSubjectCreation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(108)))), ((int)(((byte)(164)))));
            this.btnSubjectCreation.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnSubjectCreation.FlatAppearance.BorderSize = 0;
            this.btnSubjectCreation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubjectCreation.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnSubjectCreation.ForeColor = System.Drawing.Color.White;
            this.btnSubjectCreation.Location = new System.Drawing.Point(0, 50);
            this.btnSubjectCreation.Margin = new System.Windows.Forms.Padding(0);
            this.btnSubjectCreation.Name = "btnSubjectCreation";
            this.btnSubjectCreation.Size = new System.Drawing.Size(225, 50);
            this.btnSubjectCreation.TabIndex = 5;
            this.btnSubjectCreation.Text = "Subject Creation";
            this.btnSubjectCreation.UseVisualStyleBackColor = false;
            this.btnSubjectCreation.Click += new System.EventHandler(this.btnSubjectCreation_Click);
            // 
            // btnSubjectEnrollment
            // 
            this.btnSubjectEnrollment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(108)))), ((int)(((byte)(164)))));
            this.btnSubjectEnrollment.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnSubjectEnrollment.FlatAppearance.BorderSize = 0;
            this.btnSubjectEnrollment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubjectEnrollment.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold);
            this.btnSubjectEnrollment.ForeColor = System.Drawing.Color.White;
            this.btnSubjectEnrollment.Location = new System.Drawing.Point(0, 100);
            this.btnSubjectEnrollment.Margin = new System.Windows.Forms.Padding(0);
            this.btnSubjectEnrollment.Name = "btnSubjectEnrollment";
            this.btnSubjectEnrollment.Size = new System.Drawing.Size(225, 50);
            this.btnSubjectEnrollment.TabIndex = 6;
            this.btnSubjectEnrollment.Text = "Subject Enrollment";
            this.btnSubjectEnrollment.UseVisualStyleBackColor = false;
            this.btnSubjectEnrollment.Click += new System.EventHandler(this.btnSubjectEnrollment_Click);
            // 
            // btnEnrollmentSystem
            // 
            this.btnEnrollmentSystem.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnEnrollmentSystem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(183)))), ((int)(((byte)(233)))));
            this.btnEnrollmentSystem.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnEnrollmentSystem.FlatAppearance.BorderSize = 0;
            this.btnEnrollmentSystem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnrollmentSystem.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnrollmentSystem.ForeColor = System.Drawing.Color.White;
            this.btnEnrollmentSystem.Location = new System.Drawing.Point(0, 0);
            this.btnEnrollmentSystem.Margin = new System.Windows.Forms.Padding(0);
            this.btnEnrollmentSystem.Name = "btnEnrollmentSystem";
            this.btnEnrollmentSystem.Size = new System.Drawing.Size(225, 50);
            this.btnEnrollmentSystem.TabIndex = 4;
            this.btnEnrollmentSystem.Text = "Enrollment System";
            this.btnEnrollmentSystem.UseVisualStyleBackColor = false;
            this.btnEnrollmentSystem.Click += new System.EventHandler(this.btnEnrollmentSystem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 365);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(222, 50);
            this.button1.TabIndex = 9;
            this.button1.Text = "Logout";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Location = new System.Drawing.Point(225, 31);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.MinimumSize = new System.Drawing.Size(261, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1275, 869);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(68)))));
            this.panel2.Controls.Add(this.nightControlBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1500, 31);
            this.panel2.TabIndex = 3;
            // 
            // nightControlBox1
            // 
            this.nightControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nightControlBox1.BackColor = System.Drawing.Color.Transparent;
            this.nightControlBox1.CloseHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.nightControlBox1.CloseHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nightControlBox1.DefaultLocation = true;
            this.nightControlBox1.DisableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.DisableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.nightControlBox1.EnableCloseColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMaximizeButton = true;
            this.nightControlBox1.EnableMaximizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.EnableMinimizeButton = true;
            this.nightControlBox1.EnableMinimizeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.nightControlBox1.Location = new System.Drawing.Point(1361, 0);
            this.nightControlBox1.MaximizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MaximizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.MinimizeHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.nightControlBox1.MinimizeHoverForeColor = System.Drawing.Color.White;
            this.nightControlBox1.Name = "nightControlBox1";
            this.nightControlBox1.Size = new System.Drawing.Size(139, 31);
            this.nightControlBox1.TabIndex = 0;
            // 
            // registrationSystemTransition
            // 
            this.registrationSystemTransition.Interval = 50;
            this.registrationSystemTransition.Tick += new System.EventHandler(this.registrationSystemTransition_Tick);
            // 
            // enrollmentSystemTransition
            // 
            this.enrollmentSystemTransition.Interval = 50;
            this.enrollmentSystemTransition.Tick += new System.EventHandler(this.enrollmentSystemTransition_Tick);
            // 
            // dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1500, 900);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(1500, 900);
            this.Name = "dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "themeForm1";
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.registrationContainer.ResumeLayout(false);
            this.enrollmentContainer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnStudentRegister;
        private System.Windows.Forms.Button btnSubjectCreation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSubjectEnrollment;
        private System.Windows.Forms.Button btnAttendanceOverrideSystem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer registrationSystemTransition;
        private System.Windows.Forms.Button button1;
        private ReaLTaiizor.Controls.NightControlBox nightControlBox1;
        private System.Windows.Forms.Panel registrationContainer;
        private System.Windows.Forms.Button btnRegistrationSystem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnEnrollmentSystem;
        private System.Windows.Forms.Timer enrollmentSystemTransition;
        private System.Windows.Forms.Panel enrollmentContainer;
    }
}