using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing.Attendance_Override_System.Override_Buttons
{
    public partial class overrideSender : UserControl
    {
        public overrideSender()
        {
            InitializeComponent();

            subTimeIn.Format = DateTimePickerFormat.Time;
            subTimeOut.Format = DateTimePickerFormat.Time;

            // Remove the calendar button
            subTimeIn.ShowUpDown = true;
            subTimeOut.ShowUpDown = true;
        }
    }
}
