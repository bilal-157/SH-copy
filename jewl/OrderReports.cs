using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.IO;
using DAL;

namespace jewl
{
    public partial class OrderReports : Form
    {
        ItemDAL itmDAL = new ItemDAL();
        SaleDAL slDAL = new SaleDAL();
        public OrderReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this); this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }
        WorkerDAL wrkDAL = new WorkerDAL();
      

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOrderReports_Load(object sender, EventArgs e)
        {
            this.rbtOrderEstimate.Checked = true;
            this.pnlOrderItmWise.Visible = false;
            this.pnlOrderNo.Visible = false;
            this.pnlDateWise.Visible = false;
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ReportViewer frm = new ReportViewer();
            if (this.rbtOrderEstimate.Checked == true)
            {
                string selectQuery = "";
                if (this.rbtBill.Checked == true)
                {
                    frm = new ReportViewer();
                    selectQuery = "{OrderEstimateBill.OrderNo}= " + Convert.ToInt32(this.txtBOrderNo.Text);
                    frm.selectQuery = selectQuery;
                    frm.isPage = 2;
                    frm.rpt = 1;
                    frm.ShowDialog();
                }
                if (this.rbtCompleteRpt.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 2;
                    frm.ShowDialog();
                }
                if (this.rbtItemName.Checked == true)
                {
                    selectQuery = "{sp_CompleteOrder.ItemId}=" + (int)cbxGroupItem.SelectedValue;
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 3;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                if (this.rbtOrderNo.Checked == true)
                {
                    if (this.txtFOrderNo.Text != "" && this.txtTOrderNo.Text == "")
                    {
                        selectQuery = "{sp_CompleteOrder.OrderNo}>=" + Convert.ToInt32(this.txtFOrderNo.Text);
                    }
                    if (this.txtFOrderNo.Text == "" && this.txtTOrderNo.Text != "")
                    {
                        selectQuery = "{sp_CompleteOrder.OrderNo}<=" + Convert.ToInt32(this.txtTOrderNo.Text);
                    }
                    if (this.txtFOrderNo.Text != "" && this.txtTOrderNo.Text != "")
                    {
                        selectQuery = "{sp_CompleteOrder.OrderNo}>=" + Convert.ToInt32(this.txtFOrderNo.Text);
                        selectQuery += "and {sp_CompleteOrder.OrderNo}<=" + Convert.ToInt32(this.txtTOrderNo.Text);
                    }
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 2;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                if (this.rbtOrderDate.Checked == true)
                {
                    if (this.chkDFrom.Checked == true)
                    {
                        if (string.IsNullOrEmpty(selectQuery))
                        {
                            selectQuery = "{sp_CompleteOrder.ODate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                        }
                        else
                        {
                            selectQuery = selectQuery + "and {sp_CompleteOrder.ODate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                        }
                    }
                    if (this.chkDTo.Checked == true)
                    {
                        if (string.IsNullOrEmpty(selectQuery))
                            selectQuery = "{sp_CompleteOrder.ODate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                        else
                            selectQuery = selectQuery + "and {sp_CompleteOrder.ODate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    }
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 2;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                if (this.rbtDeliveryDate.Checked == true)
                {
                    if (this.chkDFrom.Checked == true)
                    {
                        if (string.IsNullOrEmpty(selectQuery))
                        {
                            selectQuery = "{sp_CompleteOrder.DDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                        }
                        else
                        {
                            selectQuery = selectQuery + "and {sp_CompleteOrder.DDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                        }
                    }
                    if (this.chkDTo.Checked == true)
                    {
                        if (string.IsNullOrEmpty(selectQuery))
                            selectQuery = "{sp_CompleteOrder.DDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                        else
                            selectQuery = selectQuery + "and {sp_CompleteOrder.DDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    }
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 2;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                if (this.rbtJobCard.Checked == true)
                {
                    selectQuery = "{sp_JobCard.OrderNo}=" + Convert.ToInt32(this.txtBillBookNo.Text);
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 4;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                if (this.rbtBillBookNo.Checked == true)
                {
                    selectQuery = "{sp_CompleteOrder.BillBookNo}='" + (this.txtBillBookNo.Text)+"'";
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 2;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                if (this.rbtAdv.Checked)
                {
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 5;
                    frm.oNo = FormControls.GetIntValue(this.txtAdv);
                    frm.ShowDialog();
                }
            }
            if (this.rbtOrderSale.Checked == true)
            {
                
                string selectQuery = "";
                if (this.rbtBill.Checked == true)
                {
                    int saleno = slDAL.GetSaleNoByONO(Convert.ToInt32(this.txtBOrderNo.Text));
                    if (saleno != 0)
                    {
                        frm = new ReportViewer();
                        frm.isPage = 2;
                        frm.rpt = 4;
                        frm.sNo = saleno;
                        frm.Show();
                    }
                }
                if (this.rbtCompleteRpt.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 6;
                    frm.ShowDialog();
                }
                if (this.rbtItemName.Checked == true)
                {
                    selectQuery = "{CompleteSaleReport.ItemId}=" + (int)cbxGroupItem.SelectedValue;
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 6;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                if (this.rbtOrderNo.Checked == true)
                {
                    if (this.txtFOrderNo.Text != "" && this.txtTOrderNo.Text == "")
                    {
                        selectQuery = "{CompleteSaleReport.OrderNo1}>=" + Convert.ToInt32(this.txtFOrderNo.Text);
                    }
                    if (this.txtFOrderNo.Text == "" && this.txtTOrderNo.Text != "")
                    {
                        selectQuery = "{CompleteSaleReport.OrderNo1}<=" + Convert.ToInt32(this.txtTOrderNo.Text);
                    }
                    if (this.txtFOrderNo.Text != "" && this.txtTOrderNo.Text != "")
                    {
                        selectQuery = "{CompleteSaleReport.OrderNo1}>=" + Convert.ToInt32(this.txtFOrderNo.Text);
                        selectQuery += "and {CompleteSaleReport.OrderNo1}<=" + Convert.ToInt32(this.txtTOrderNo.Text);
                    }
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 6;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                if (this.rbtOrderDate.Checked == true)
                {
                    if (this.chkDFrom.Checked == true)
                    {

                        if (string.IsNullOrEmpty(selectQuery))
                        {
                            selectQuery = "{CompleteSaleReport.ODate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                        }
                        else
                        {
                            selectQuery = selectQuery + "and {CompleteSaleReport.ODate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                        }

                    }
                    if (this.chkDTo.Checked == true)
                    {
                        if (string.IsNullOrEmpty(selectQuery))
                            selectQuery = "{CompleteSaleReport.ODate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                        else
                            selectQuery = selectQuery + "and {CompleteSaleReport.ODate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    }
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 6;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                if (this.rbtSaleDate.Checked == true)
                {
                    if (this.chkDFrom.Checked == true)
                    {
                        if (string.IsNullOrEmpty(selectQuery))
                        {
                            selectQuery = "{CompleteSaleReport.SDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                        }
                        else
                        {
                            selectQuery = selectQuery + "and {CompleteSaleReport.SDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                        }
                    }
                    if (this.chkDTo.Checked == true)
                    {
                        if (string.IsNullOrEmpty(selectQuery))
                            selectQuery = "{CompleteSaleReport.SDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                        else
                            selectQuery = selectQuery + "and {CompleteSaleReport.SDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    }
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 6;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                if (this.rbtJobCard.Checked == true)
                {
                    selectQuery = "{sp_JobCard.OrderNo}=" + Convert.ToInt32(this.txtBillBookNo.Text);
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 4;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                if (this.rbtBillBookNo.Checked == true)
                {
                    selectQuery = "{sp_OrderSaleRpt.BillBookNo}=" + Convert.ToInt32(this.txtBillBookNo.Text);
                    frm = new ReportViewer();
                    frm.isPage = 2;
                    frm.rpt = 6;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
            }
            if (this.rbtPendingOrder.Checked == true)
            {
                if (this.rbtPendingONo.Checked == true)
                {
                    if (this.txtBOrderNo.Text != string.Empty)
                    {
                        frm = new ReportViewer();
                        frm.isPage = 3;
                        frm.rpt = 7;
                        frm.selectQuery = "{sp_PendingOrder.OrderNo} = " + Convert.ToInt32(this.txtBOrderNo.Text);
                        frm.ShowDialog();
                    }
                }
                if (this.rbtPendingOrd.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.isPage = 3;
                    frm.rpt = 7;
                    frm.ShowDialog();
                }
                if (this.rbtPOrderWorkerWise.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.isPage = 3;
                    frm.rpt = 7;
                    frm.selectQuery = "{sp_PendingOrder.WorkerId} = " + this .cbxWorker .SelectedValue; 
                    frm.ShowDialog();
                }
            }
            if (this.rbtOrderBalance.Checked == true)
            {
                frm = new ReportViewer();
                frm.isPage = 3;
                frm.rpt = 8;
                frm.selectQuery = "{sp_PendingOrder.OrderNo} = " + Convert .ToInt32 (FormControls.StringFormate(this.txtBOrderNo .Text));
                frm.Df = DateDAL.GetDate("select Min(StockDate) as Date from Stock");
                frm.ShowDialog();
            }
        }

        private void rbtOrderNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtOrderNo.Checked == true)
                this.pnlOrderNo.Visible = true;
            else
                this.pnlOrderNo.Visible = false;
        }

        private void rbtItemName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtItemName.Checked == true)
                this.pnlOrderItmWise.Visible = true;
            else
                this.pnlOrderItmWise.Visible = false;
        }

        private void chkFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkFrom.Checked == true)
                this.txtFOrderNo.Enabled = true;
            else
            {
                this.txtFOrderNo.Enabled = false;
                this.txtFOrderNo.Text = "";
            }
        }

        private void chkTo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTo.Checked == true)
                this.txtTOrderNo.Enabled = true;
            else
            {
                this.txtTOrderNo.Enabled = false;
                this.txtTOrderNo.Text = "";
            }
        }

        private void chkDFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDFrom.Checked == true)
                this.dtpFrom.Enabled = true;
            else
            {
                this.dtpFrom.Enabled = false;
                this.dtpFrom.Text = "";
            }
        }

        private void chkDTo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDTo.Checked == true)
                this.dtpTo.Enabled = true;
            else
            {
                this.dtpTo.Enabled = false;
                this.dtpTo.Text = "";
            }
        }

        private void rbtOrderDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtOrderDate.Checked == true)
                this.pnlDateWise.Visible = true;
            else
                this.pnlDateWise.Visible = false;
        }

        private void rbtDeliveryDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtDeliveryDate.Checked == true)
                this.pnlDateWise.Visible = true;
            else
                this.pnlDateWise.Visible = false;
        }

       

        private void rbtJobCard_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtJobCard.Checked == true)
                txtBillBookNo.Visible = true;
            else
                txtBillBookNo.Visible = false ;          
        }

        private void rbtBillBookNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtBillBookNo.Checked == true)
                txtBillBookNo.Visible = true;
            else
                txtBillBookNo.Visible = false;
        }

        private void rbtOrderEstimate_CheckedChanged(object sender, EventArgs e)
        {
            lblName.Text = "Order Estimate Reports";
            //lblName.Text = ((RadioButton)sender).Text + " Reports";
        }

        private void rbtOrderSale_CheckedChanged(object sender, EventArgs e)
        {
            lblName.Text = "Order Sale Reports";
            //lblName.Text = ((RadioButton)sender).Text + " Reports";
            this.rbtSaleDate.Enabled = true;
            if(rbtOrderSale.Checked)
            this.rbtAdv.Visible = false;
            else
                this.rbtAdv.Visible = true;
        }
        
        private void rbtSaleDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSaleDate.Checked == true)
                this.pnlDateWise.Visible = true;
            else
                this.pnlDateWise.Visible = false;
        }

        private void rbtPendingOrder_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPendingOrder.Checked == true)
            {
                this.pnlOrderItmWise.Visible = false;
                this.pnlOrderNo.Visible = false;
                this.pnlDateWise.Visible = false;
                lblName.Text = "Pending Order Reports";
                this.pnlMain.Visible = false;
                this.pnlPendingOrder.Visible = true;
            }
            else 
            {
                this.pnlPendingOrder.Visible = false;
                this.pnlMain.Visible = true;               
            }
        }

        private void rbtPOrderWorkerWise_CheckedChanged(object sender, EventArgs e)
        {
            this.cbxWorker.DataSource = wrkDAL.GetAllWorkers();
            this.cbxWorker.DisplayMember = "Name";
            this.cbxWorker.ValueMember = "ID";
            this.cbxWorker.SelectedIndex = -1;
        }

        private void cbxWorker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rbtOrderBalance_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtOrderBalance.Checked == true)
            {
                this.pnlOrderItmWise.Visible = false;
                this.pnlOrderNo.Visible = false;
                this.pnlDateWise.Visible = false;
                lblName.Text = "Order Balance Reports";
                this.pnlMain.Visible = false;
                this.pnlPendingOrder.Visible = false;
                this.pnlOrderBalance.Visible = true;
            }
            else
            {
                this.pnlOrderBalance.Visible = true ;
                this.pnlMain.Visible = true;
            }
        }

        private void rbtBill_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtBill.Checked)
                this.pnlOrderBalance.Visible = true;
            else
                this.pnlOrderBalance.Visible = false ;
        }

        private void rbtAdv_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtAdv.Checked)
                this.txtAdv.Visible = true;
            else
                this.txtAdv.Visible = false;
        }

        private void OrderReports_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

    }
}
