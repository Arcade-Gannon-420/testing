using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testing
{
    public partial class testDashboard : Form
    {

        

        public testDashboard()
        {
            InitializeComponent();
        }

        bool menuExpand = false;

        private void menuTransition_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                menuContainer.Height += 30;
                if (menuContainer.Height >= 107)
                {
                    menuTransition.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                menuContainer.Height -= 30;
                if (menuContainer.Height <= 53)
                {
                    menuTransition.Stop();
                    menuExpand = false ;
                }
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            menuTransition.Start();
        }

        
    }
}
