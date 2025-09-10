using BusinesEntities;
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
    public partial class Password : Form
    {
        string Passwordd = "bringoo786";
        public Password()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtPassword.Text == Passwordd)
            {
                StartUp s = new StartUp();
                s.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorect Password", Messages.Header);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Password_Load(object sender, EventArgs e)
        {
            this.txtPassword.Select();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSave.PerformClick();
        }
    }
}
