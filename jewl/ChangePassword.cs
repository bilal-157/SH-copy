using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class ChangePassword : Form
    {
        UserDAL uDAL = new UserDAL();
        public ChangePassword()
        {
            InitializeComponent();
            FormControls.GetAllControls(this); this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            FormControls.FillCombobox(cbxUserName, uDAL.GetAllUsers(), "UserName", "UserId");
            this.cbxUserName.SelectedValue = DateDAL.userId;
            this.txtOldPassword.Select();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int k = (int)this.cbxUserName.SelectedValue;
            string st=this.txtOldPassword.Text;
            if (k == Login.userId && st == Login.str)
            {
                if (this.txtNewPassword.Text == "")
                {
                    MessageBox.Show("Must Enter Password");
                }
                else if (this.txtVerifyPassword.Text == "")
                {
                    MessageBox.Show("Must Enter Verify Password");
                }
                else if (this.txtNewPassword.Text == this.txtVerifyPassword.Text)
                {
                    uDAL.UpdatePassword(Login.userId, this.txtVerifyPassword.Text, Login.str);
                    MessageBox.Show("Password has been changed successfully", Messages.Header);
                }
                else
                {
                    MessageBox.Show("Incorrect password", Messages.Header);
                }
            }
            else
            {
                MessageBox.Show("Invalid user name or password", Messages.Header);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangePassword_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ChangePassword frm = new ChangePassword();
            frm.ShowDialog();
        }
    }
}
