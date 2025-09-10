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
    public partial class Login : Form
    {
        public static int userId;
        public static string str, userName = "";
        public bool lFlag;
        public static bool exit = true;
        UserDAL uDAL = new UserDAL();
        public Login()
        {
            InitializeComponent();
           
        }

        private void Login_Load(object sender, EventArgs e)
        {
            FormControls.FillCombobox(cbxUserName, uDAL.GetAllUsers(), "UserName", "UserId");
            this.cbxUserName.SelectedIndex = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (exit)
                Application.Exit();
            else
            {
                exit = true;
                this.Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            int k = (int)this.cbxUserName.SelectedValue;
            str = this.txtPassword.Text;
            userId = k; DateDAL.userId = userId;
            userName = this.cbxUserName.Text;
            bool result = uDAL.ValidateUser(k, str);
            

            if (result)
            {
                this.Hide();                     
                Main mn = new Main();
                lFlag = true;
                //mn.Show();                
            }
            else
            {
                MessageBox.Show("Invalid user id or password",Messages.Header);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender, e);
            }
        }

        private void cbxUserName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxUserName_DrawItem(object sender, DrawItemEventArgs e)
        {
            //if (e.Index < 0) return;
            //Font f = cbxUserName.Font;
            //int yOffset = 10;

            //if ((e.State & DrawItemState.Focus) == 0)
            //{
            //    e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            //    e.Graphics.DrawString(cbxUserName.Items[e.Index].ToString(), f, Brushes.Black,
            //                          new Point(e.Bounds.X, e.Bounds.Y + yOffset));
            //}
            //else
            //{
            //    e.Graphics.FillRectangle(Brushes.Blue, e.Bounds);
            //    e.Graphics.DrawString(cbxUserName.Items[e.Index].ToString(), f, Brushes.White,
            //                          new Point(e.Bounds.X, e.Bounds.Y + yOffset));
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Paint(object sender, PaintEventArgs e)
        {
            Form panel = sender as Form;

            if (panel.FormBorderStyle == FormBorderStyle.None)
            {
                int thickness = 1;//it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.Black, thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                              halfThickness,
                                                              panel.ClientSize.Width - thickness,
                                                              panel.ClientSize.Height - thickness));
                }
            }
        }

            
    }
}
