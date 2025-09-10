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
    public partial class frmPassword : Form
    {
        public string Password = "";
        public int id = 0;
        public frmPassword()
        {
            InitializeComponent();
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.txtPassword.Text == Password)
            {
                id = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect Code", "Warning");
                return;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk.PerformClick();
        }

        private void frmPassword_Load(object sender, EventArgs e)
        {
            txtPassword.Select();
        }
    }
}
