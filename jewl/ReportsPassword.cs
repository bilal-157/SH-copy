using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BusinesEntities;
namespace jewl
{
    public partial class ReportsPassword : Form
    {
        StockDAL stkDAL = new StockDAL();
        StartUpp statp = new StartUpp();
        public int id = 0;
        public ReportsPassword()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void ReportsPassword_Load(object sender, EventArgs e)
        {
            statp = stkDAL.GetStartUp();

            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (statp.ReportPassword == this.txtPassword.Text)
            {
                id = 1;
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong Password");
                return;

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
