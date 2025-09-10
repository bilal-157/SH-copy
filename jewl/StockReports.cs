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
using System.Data.SqlClient;

namespace jewl
{
    public partial class StockReports : Form
    {
        ItemDAL itmDAL = new ItemDAL();
        WorkerDAL wrkDAL = new WorkerDAL();
        DesignDAL desDAL = new DesignDAL();
        StockDAL sDAL = new StockDAL();

        private DateTime qtyDate;

        //public Nullable<DateTime> QtyDate
        //{
        //    get { return qtyDate; }
        //    set { qtyDate = value; }
        //}

        public StockReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Maximized;
            IEnumerable<Control> cont = FormControls.GetAll(this.groupBox2, typeof(RadioButton));
            foreach (var item in cont)
            {
                RadioButton r = ((RadioButton)item);
                r.BackColor = Color.FromArgb(0, 188, 212);
                r.ForeColor = Color.FromArgb(255, 255, 255);
                
            }
        }

        private void StockReports_Load(object sender, EventArgs e)
        {
            this.cbxGroupItem.Enabled = false;
            this.dtpFrom.Enabled = false;
            this.dtpTo.Enabled = false;
            this.cbxKarat.Enabled = false;
            this.cbxWorkerName.Enabled = false;
            this.cbxKarat.Enabled = false;
            this.cbxDesignNo.Enabled = false;
            this.rbtCompleteStock.Checked = true;
            this.rbtDetailStock.Checked = true;
            this.dtpCompStock.Visible = false;
            this.dtpCompStockSummaryQty.Visible = false;
            this.dtpComStockSummary.Visible = false;
            this.cbxItemType.SelectedIndex = 1;
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
            FormControls.FillCombobox(cbxItemName, itmDAL.GetAllItems(), "ItemName", "ItemId");
            FormControls.FillCombobox(cbxWorkerName, wrkDAL.GetAllWorkers(), "Name", "ID");
            FormControls.FillCombobox(cbxDesignNo, desDAL.GetAllDesign(), "DesignNo", "DesignId");
        }

        private void rbtSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtDetailStock.Checked == true || this.rbtStockPics.Checked == true)
            {
               
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtDetailStock_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtDetailStock.Checked == true)
            {
                this.pnlselectionReport.Visible = true;
                this.groupBox1.BackgroundImage = null;
                this.chkPics.Visible = true;
            }
            else if (this.rbtDetailStock.Checked == false)
            {
                this.pnlselectionReport.Visible = false;
                this.chkPics.Visible = false;
            }
        }

        //private void rbtTagHistory_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.rbtTagHistory.Checked == true)
        //    {
        //        this.cbxItemName.SelectedIndexChanged -= new System.EventHandler(this.cbxItemName_SelectedIndexChanged);
        //        this.cbxItemName.DataSource = itmDAL.GetAllItems();
        //        this.cbxItemName.DisplayMember = "ItemName";
        //        this.cbxItemName.ValueMember = "ItemId";
        //        this.cbxItemName.SelectedIndex = -1;
        //        this.pnlTagHistory.Visible = true;
        //    }
        //    else if (this.rbtTagHistory.Checked == false)
        //        this.pnlTagHistory.Visible = false;
        //}

        private void rbtCompleteStockSummary_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.rbtCompleteStockSummary.Checked == true)
            //    this.dtpComStockSummary.Visible = true;
            //else if (this.rbtCompleteStockSummary.Checked == false)
            //    this.dtpComStockSummary.Visible = false;
            if (this.rbtCompleteStockSummary.Checked == true)
            {
                this.pnlCompStock.Visible = true;
                this.dtpCompStock.Visible = true;
                this.dtpCompTStock.Visible = true;
                this.groupBox1.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
                this.groupBox1.BackgroundImageLayout = ImageLayout.Center;
            }
            else if (this.rbtCompleteStockSummary.Checked == false)
            {
                this.pnlCompStock.Visible = false;
                this.dtpCompStock.Visible = false;
                this.dtpCompTStock.Visible = false;
            }
        }

        private void rbtComprehensive_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtComprehensive.Checked == true)
            {
                this.pnlCompStock.Visible = true;
                this.dtpCompStock.Visible = true;
                this.dtpCompTStock.Visible = true;
            }
            else if (this.rbtComprehensive.Checked == false)
            {
                this.pnlCompStock.Visible = false;
                this.dtpCompStock.Visible = false;
                this.dtpCompTStock.Visible = false;
            }
        }

        private void rbtCompStockQty_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtCompStockQty.Checked == true)
                this.dtpCompStockSummaryQty.Visible = true;
            else if (this.rbtCompStockQty.Checked == false)
                this.dtpCompStockSummaryQty.Visible = false;
        }

        private void rbtStockPics_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtStockPics.Checked == true)
            {
                StockReportsPics frm = new StockReportsPics();
                FormControls.FadeOut(this);
                frm.ShowDialog();
                FormControls.FadeIn(this);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ReportViewer frm = new ReportViewer();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.AutoScroll = true;
            this.m.Controls.Add(frm);
            if (this.rbtDetailStock.Checked == true && this.rbtCompleteStock.Checked == true)
            {
                string IType = (string)this.cbxItemType.SelectedItem;
                string selectQuery = "";
                string sumQuery = "Select Sum(NetWeight) as TWeight ,Sum(stQty) as TQty from Stock Where ([status] ='Available' or [status]= 'Sample')";
                if (this.chkGroupItem.Checked == true && this.cbxGroupItem.SelectedIndex != -1)
                {
                    Item itm = (Item)this.cbxGroupItem.SelectedItem;
                    selectQuery = "{StockRpt.ItemId}=" + itm.ItemId;
                }
                if (this.chkFromDate.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        DateTime dt = dtpFrom.Value;
                        selectQuery = "{StockRpt.StockDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {StockRpt.StockDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                    }
                }
                if (this.chkToDate.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{StockRpt.StockDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {StockRpt.StockDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    }
                }
                if (this.chkWorkerName.Checked == true && this.cbxWorkerName.SelectedIndex != -1)
                {
                    Worker wrk = (Worker)this.cbxWorkerName.SelectedItem;
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{StockRpt.WorkerId}=" + wrk.ID;
                        sumQuery = "Select Sum(NetWeight) as TWeight ,Sum(stQty) as TQty from Stock Where ([status] ='Available' or [status]= 'Sample') and  WorkerId =" + wrk.ID;
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {StockRpt.WorkerId}=" + wrk.ID;
                        sumQuery = "Select Sum(NetWeight) as TWeight ,Sum(stQty) as TQty from Stock Where ([status] ='Available' or [status]= 'Sample') and  WorkerId =" + wrk.ID;
                    }
                }
                if (this.chkKarat.Checked == true && this.cbxKarat.SelectedIndex != -1)
                {
                    string str = (string)this.cbxKarat.SelectedItem;
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{StockRpt.Karat}='" + str + "'";
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {StockRpt.Karat}='" + str + "'";
                    }
                }
                if (this.chkDesignNo.Checked == true && this.cbxDesignNo.SelectedIndex != -1)
                {
                    Design des = (Design)this.cbxDesignNo.SelectedItem;
                    selectQuery = "{StockRpt.DesignId}=" + des.DesignId;
                }
                if (!(IType.Equals("All")))
                {
                    if (IType == "Bulk")
                    {
                        if (string.IsNullOrEmpty(selectQuery))
                        {
                            selectQuery = "{StockRpt.BStatus}='Bulk'";
                        }
                        else
                        {
                            selectQuery = " {StockRpt.BStatus}='Bulk' and " + selectQuery;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(selectQuery))
                        {
                            selectQuery = "{StockRpt.IType}='" + IType + "'";
                        }
                        else
                        {
                            selectQuery = " {StockRpt.IType}='" + IType + "' and " + selectQuery;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{StockRpt.IType}<>'All'";
                    }
                    else
                    {
                        selectQuery = " {StockRpt.IType}<>'All' and " + selectQuery;
                    }
                }
                frm.isPage = 1;
                frm.rpt = 1;
                frm.selectQuery = selectQuery;
                if (this.chkPics.Checked == true)
                {
                    frm.rpt = 8;
                }
                if (this.cbxItemType.Text == "Diamond")
                {
                    frm.iType = "Diamond";
                }
                frm.Show();
            }

            if (this.rbtDetailStock.Checked==true && this.rbtOrderStock.Checked == true)
            {
                string IType = (string)this.cbxItemType.SelectedItem;
                string selectQuery = "";
                string sumQuery = "Select Sum(NetWeight) as TWeight ,Sum(stQty) as TQty from Stock Where ([status] ='Available' and ItemFor='Order' or [status]= 'Sample')";
                if (this.rbtOrderStock.Checked == true)
                {
                    if (this.chkGroupItem.Checked == true && this.cbxGroupItem.SelectedIndex != -1)
                    {
                        Item itm = (Item)this.cbxGroupItem.SelectedItem;
                        selectQuery = "{StockRpt.ItemId}=" + itm.ItemId;
                    }

                    if (this.chkFromDate.Checked == true)
                    {
                        if (string.IsNullOrEmpty(selectQuery))
                        {
                            DateTime dt = dtpFrom.Value;
                            selectQuery = "{StockRpt.StockDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                        }
                        else
                        {
                            selectQuery = selectQuery + "and {StockRpt.StockDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                        }
                    }
                    if (this.chkToDate.Checked == true)
                    {
                        if (string.IsNullOrEmpty(selectQuery))
                        {
                            selectQuery = "{StockRpt.StockDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                        }
                        else
                        {
                            selectQuery = selectQuery + "and {StockRpt.StockDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                        }
                    }
                    if (this.chkWorkerName.Checked == true && this.cbxWorkerName.SelectedIndex != -1)
                    {
                        Worker wrk = (Worker)this.cbxWorkerName.SelectedItem;
                        if (string.IsNullOrEmpty(selectQuery))
                        {
                            selectQuery = "{StockRpt.WorkerId}=" + wrk.ID;// + "'";
                            sumQuery = "Select Sum(NetWeight) as TWeight ,Sum(stQty) as TQty from Stock Where ([status] ='Available' or [status]= 'Sample') and  WorkerId =" + wrk.ID;
                        }
                        else
                        {
                            selectQuery = selectQuery + "and {StockRpt.WorkerId}=" + wrk.ID;// + "'";
                            sumQuery = "Select Sum(NetWeight) as TWeight ,Sum(stQty) as TQty from Stock Where ([status] ='Available' or [status]= 'Sample') and  WorkerId =" + wrk.ID;
                        }
                    }
                    if (this.chkKarat.Checked == true && this.cbxKarat.SelectedIndex != -1)
                    {
                        string str = (string)this.cbxKarat.SelectedItem;
                        if (string.IsNullOrEmpty(selectQuery))
                        {
                            selectQuery = "{StockRpt.Karat}='" + str + "'";
                        }
                        else
                        {
                            selectQuery = selectQuery + "and {StockRpt.Karat}='" + str + "'";
                        }
                    }
                    if (this.chkDesignNo.Checked == true && this.cbxDesignNo.SelectedIndex != -1)
                    {
                        Design des = (Design)this.cbxDesignNo.SelectedItem;
                        selectQuery = "{StockRpt.DesignId}=" + des.DesignId;
                    }
                }
                if (!(IType.Equals("All")))
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{StockRpt.IType}='" + IType + "'and {StockRpt.ItemFor}='Order'";
                    }
                    else
                    {
                        selectQuery = " {StockRpt.IType}='" + IType + "'and {StockRpt.ItemFor}='Order' and" + selectQuery;
                    }
                }
                frm.isPage = 1;
                frm.rpt = 13;
                frm.selectQuery = selectQuery;
                if (this.cbxItemType.Text == "Diamond")
                {
                    frm.iType = "Diamond";
                }
                frm.Show();
            }
            if (rbtStockSummaryKarat.Checked == true)
            {
                frm.isPage = 1;
                frm.rpt = 2;
                frm.Show();
            }
            if (rbtCompleteStockSummary.Checked == true || rbtStockSummaryGraph.Checked == true)
            {
                frm.isPage = 1;
                frm.rpt = 3;
                frm.Df = Convert.ToDateTime(this.dtpCompStock.Value);
                frm.Dt = Convert.ToDateTime(this.dtpCompTStock.Value);
                if (rbtStockSummaryGraph.Checked == true)
                {
                    frm.rpt = 9;
                }
                frm.Show();
            }
            if (rbtComprehensive.Checked == true||this.rbtComprehensiveBulk.Checked==true)
            {
                frm.isPage = 1;
                frm.rpt = 4;
                frm.Df = Convert.ToDateTime(this.dtpCompStock.Value);
                frm.Dt = Convert.ToDateTime(this.dtpCompTStock.Value);
                if (this.rbtComprehensiveBulk.Checked == true)
                {

                    frm.rpt = 7;
                }
                frm.Show();
            }
            if (rbtStockBalance.Checked == true)
            {
                frm.isPage = 1;
                frm.rpt = 5;
                frm.Show();
            }
            if (rbtCompStockQty.Checked == true)
            {
                frm.isPage = 1;
                frm.rpt = 6;
                frm.Df = Convert.ToDateTime(this.dtpCompStockSummaryQty.Value);
                frm.Show();
            }
            if (rbtStockPics.Checked == true)
            {
            }
        }
        private void chkGroupItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkGroupItem.Checked == true)
            {
                this.cbxGroupItem.Enabled = true;
            }
            else
            {
                this.cbxGroupItem.Enabled = false;
            }
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
        private void chkKarat_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkKarat.Checked == true)
                this.cbxKarat.Enabled = true;
            else
                this.cbxKarat.Enabled = false;
        }
        private void chkDesignNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDesignNo.Checked == true)
                this.cbxDesignNo.Enabled = true;
            else
                this.cbxDesignNo.Enabled = false;
        }
        private void chkWorkerName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkWorkerName.Checked == true)
                this.cbxWorkerName.Enabled = true;
            else
                this.cbxWorkerName.Enabled = false;
        }
        private void cbxItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int k = (int)this.cbxItemName.SelectedValue;
            FormControls.FillCombobox(cbxTagNo, sDAL.GetAllTagNosByItemId("select StockId ,TagNo from Stock where ItemId=" + k), "TagNo", "StockId");
        }
        private void cbxItemName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxItemName.SelectedIndexChanged += new System.EventHandler(this.cbxItemName_SelectedIndexChanged);
        }

        private void rbtStockSummaryKarat_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox1.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
            this.groupBox1.BackgroundImageLayout = ImageLayout.Center;
        }

        private void chkPics_CheckedChanged(object sender, EventArgs e)
        {

        }       
    }
}