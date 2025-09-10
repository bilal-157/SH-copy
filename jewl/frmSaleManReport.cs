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
    public partial class frmSaleManReport : Form
    {
        SaleManDAL slmDAL = new SaleManDAL();
        public frmSaleManReport()
        {
            InitializeComponent();
            FormControls.GetAllControls(this); FormControls.GetAllControls(this); this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string selectQuery = "";
            if (this.rbtAdvanceReport.Checked == true)
            {
                if (this.chkSalesMan.Checked == true)
                {
                    SaleMan slm = (SaleMan)this.cbxSalesMan.SelectedItem;
                    selectQuery = "{Command.SmId}=" + slm.ID;
                }
                if (this.chkFromDate.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                        selectQuery = "{Command.GDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                    else
                        selectQuery = selectQuery + "and {Command.GDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                }
                if (this.chkToDate.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                        selectQuery = "{Command.GDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    else
                        selectQuery = selectQuery + "and {Command.GDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                }
                SalesManAdvanceRpt frm = new SalesManAdvanceRpt();
                frm.selectQuery = selectQuery;
                frm.Show();
            }
            if (this.rbtAttendanceReport.Checked == true)
            {
                if (this.chkSalesMan.Checked == true)
                {
                    SaleMan slm = (SaleMan)this.cbxSalesMan.SelectedItem;
                    selectQuery = "{Command.Id}=" + slm.ID;

                }
                if (this.chkFromDate.Checked == true)
                {

                    if (string.IsNullOrEmpty(selectQuery))
                        selectQuery = "{Command.ADate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                    else
                        selectQuery = selectQuery + "and {Command.ADate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                }
                if (this.chkToDate.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                        selectQuery = "{Command.ADate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    else
                        selectQuery = selectQuery + "and {Command.ADate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                }
                SalesManAttendenceRpt frm = new SalesManAttendenceRpt();
                frm.selectQuery = selectQuery;
                frm.Show();
            }
            if (this.rbtSalaryReport.Checked == true)
            {
                if (this.chkSalesMan.Checked == true)
                {
                    SaleMan slm = (SaleMan)this.cbxSalesMan.SelectedItem;
                    selectQuery = "{Command.Id}=" + slm.ID;

                }
                if (this.chkFromDate.Checked == true)
                {

                    if (string.IsNullOrEmpty(selectQuery))
                        selectQuery = "{Command.GDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                    else
                        selectQuery = selectQuery + "and {Command.GDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                }
                if (this.chkToDate.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                        selectQuery = "{Command.GDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    else
                        selectQuery = selectQuery + "and {Command.GDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                }
                SalesManSalaryRpt frm = new SalesManSalaryRpt();
                frm.selectQuery = selectQuery;
                frm.Show();
            }
            if (this.rbtSalesManList.Checked == true)
            {
                SaleManListRpt frm = new SaleManListRpt();
                frm.Show();
            }
        }

        private void frmSaleManReport_Load(object sender, EventArgs e)
        {
            //this.cbxSaleManName.SelectedIndexChanged -= new EventHandler(cbxSaleManName_SelectedIndexChanged);
            this.cbxSalesMan.DataSource = slmDAL.GetAllSaleMen();
            this.cbxSalesMan.DisplayMember = "Name";
            this.cbxSalesMan.ValueMember = "ID";
            this.cbxSalesMan.SelectedIndex = -1;
        }

        private void rbtSalesManList_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSalesManList.Checked == true)
                this.pnlSelection.Visible = false;
            else
                this.pnlSelection.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSalesMan_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSalesMan.Checked == true)
                this.cbxSalesMan.Enabled = true;
            else
                this.cbxSalesMan.Enabled = false;
        }

        private void chkFromDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkFromDate.Checked == true)
                this.dtpFrom.Enabled = true;
            else
                this.dtpFrom.Enabled = false;
        }

        private void chkToDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkToDate.Checked == true)
                this.dtpTo.Enabled = true;
            else
                this.dtpTo.Enabled = false;
        }

        private void frmSaleManReport_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void pnlSelection_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

  
    }
}
