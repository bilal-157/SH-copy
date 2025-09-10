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
    public partial class StartUp : Form
    {
        SaleDAL slDAL = new SaleDAL();
        public StartUp()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string SaleRateFix = "";
            if (this.chkSaleRate.Checked == true)
            {
                if (this.rbtManual.Checked == true)
                {
                    if (this.cbxKarat.SelectedIndex == -1)
                    {
                        MessageBox.Show("Must Select Karat", Messages.Header);
                        return;
                    }
                    else
                        SaleRateFix = "update StartUp set FixRateStatus= '" + this.cbxKarat.Text + "'";
                }
                if (this.rbtAuto.Checked == true)
                    SaleRateFix = "update StartUp set FixRateStatus ='Auto'";
                slDAL.StartUpSaleRateFix(SaleRateFix);
            }
            if (this.chkRateType.Checked == true)
            {
                if (this.cbxWeightInGram.SelectedIndex == -1)
                {
                    MessageBox.Show("Must Select Rate WeightInGm ", Messages.Header);
                    return;
                }
                if (this.rbtSonaPasa.Checked == true)
                    SaleRateFix = "update StartUp set GoldRateType= 'SonaPasa',GoldRateGram=" + this.cbxWeightInGram.Text + "";
                if (this.rbtStandard.Checked == true)
                    SaleRateFix = "update StartUp set GoldRateType= 'Standard',GoldRateGram=" + this.cbxWeightInGram.Text + "";
                slDAL.StartUpSaleRateFix(SaleRateFix);
            }
            if (this.chkBackUp.Checked == true)
            {
                if (this.txtBackUp.Text == "")
                {
                    MessageBox.Show("Must Enter Backup Path", Messages.Header);
                    return;
                }
                else
                {
                    SaleRateFix = "update StartUp set BackupPath= '" + this.txtBackUp.Text + "'";
                    slDAL.StartUpSaleRateFix(SaleRateFix);
                }
            }
            if (this.chkJewlManger.Checked == true)
            {
                if (this.rbtComplete.Checked == true)
                    SaleRateFix = "update StartUp set JewlManagerType= 'Complete'";
                if (this.rbtTagging.Checked == true)
                    SaleRateFix = "update StartUp set JewlManagerType= 'Tagging'";
                if (this.rbtStockSale.Checked == true)
                    SaleRateFix = "update StartUp set JewlManagerType= 'StockSale'";
                slDAL.StartUpSaleRateFix(SaleRateFix);
            }
            if (this.chkReportPasword.Checked == true)
            {
                SaleRateFix = "update StartUp set ReportPassword= '" + this.txtReportPassword.Text + "'";
                slDAL.StartUpSaleRateFix(SaleRateFix);
            }
            if (this.chkRateTotaGram.Checked == true)
            {
                if (this.rbtGram.Checked == true)
                    SaleRateFix = "update StartUp set GramTolaRate= '" + this.rbtGram.Text + "'";
                if (this.rbtTola.Checked == true)
                    SaleRateFix = "update StartUp set GramTolaRate= '" + this.rbtTola.Text + "'";
                slDAL.StartUpSaleRateFix(SaleRateFix);
            }
            if (this.chkSalary.Checked == true)
            {
                if (this.rbtWithAttendance.Checked == true)
                    SaleRateFix = "update StartUp set SalaryCalculation = '" + this.rbtWithAttendance.Text + "'";
                if (this.rbtWithoutAttendance.Checked == true)
                    SaleRateFix = "update StartUp set SalaryCalculation = '" + this.rbtWithoutAttendance.Text + "'";
                slDAL.StartUpSaleRateFix(SaleRateFix);
            }
            MessageBox.Show("Saved Successfully", Messages.Header);
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSaleRate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSaleRate.Checked == true)
                this.pnlRateFixing.Enabled = true;
            if (this.chkSaleRate.Checked == false)
                this.pnlRateFixing.Enabled = false;
        }

        private void chkRateType_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkRateType.Checked == true)
                this.pnlRateTypeFixing.Enabled = true;
            if (this.chkRateType.Checked == false)
                this.pnlRateTypeFixing.Enabled = false;
        }

        private void rbtManual_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtManual.Checked == true)
                this.cbxKarat.Enabled = true;
            if (this.rbtManual.Checked == false)
                this.cbxKarat.Enabled = false;
        }

        private void chkRateType_CheckedChanged_1(object sender, EventArgs e)
        {
            chkRateType_CheckedChanged(sender, e);
        }

        private void chkSaleRate_CheckedChanged_1(object sender, EventArgs e)
        {
            chkSaleRate_CheckedChanged(sender, e);
        }

        private void rbtManual_CheckedChanged_1(object sender, EventArgs e)
        {
            rbtManual_CheckedChanged(sender, e);
        }

        private void btnBackUp_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(this);
            string startupPath = System.IO.Directory.GetCurrentDirectory();

            string startupPath1 = Environment.CurrentDirectory;
        }

        private void chkBackUp_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkBackUp.Checked == true)
                this.pnlBackUp.Enabled = true;
            if (this.chkBackUp.Checked == false)
                this.pnlBackUp.Enabled = false;
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkJewlManger_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkJewlManger.Checked == true)
                this.pnlJewlManager.Enabled = true;
            if (this.chkJewlManger.Checked == false)
                this.pnlJewlManager.Enabled = false;
        }

        private void chkReportPasword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkReportPasword.Checked == true)
                this.pnlReports.Enabled = true;
            if (this.chkReportPasword.Checked == false)
                this.pnlReports.Enabled = false;
        }

        private void chkRateTotaGram_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkRateTotaGram.Checked == true)
                this.pnlRateTolaGram.Enabled = true;
            if (this.chkRateTotaGram.Checked == false)
                this.pnlRateTolaGram.Enabled = false;
        }

        private void chkSalary_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSalary.Checked == true)
                pnlSalary.Enabled = true;
            else
                pnlSalary.Enabled = false;
        }
    }
}