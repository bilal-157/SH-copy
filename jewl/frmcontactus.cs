using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace jewl
{
    public partial class frmcontactus : Form
    {
        public frmcontactus()
        {
            InitializeComponent();
        }

        private void frmcontactus_Load(object sender, EventArgs e)
        {

        }

        private void lblTimer_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.jewelmanager.com");
            }
            catch { }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://newbringootech.com/");
            }
            catch { }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/user/arzoamadina/videos");
            }
            catch { }
        }
    }
}
