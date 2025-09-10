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
    public partial class BackUpPath : Form
    {
        SaleDAL slDAL = new SaleDAL();
        string SaleRateFix = "";
        public BackUpPath()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
        }

        private void BackUpPath_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //statp = stkDAL.GetStartUp();
            if (this.txtBackUp.Text == "")
            {
                MessageBox.Show("Must Enter Backup Path");
                return;
            }
            else
            {
                SaleRateFix = "update StartUp set BackupPath= '" + this.txtBackUp.Text + "'";
                slDAL.StartUpSaleRateFix(SaleRateFix);
            }
            MessageBox.Show("Saved Successfully", Messages.Header);
            this.Close();
        }
    }
}
