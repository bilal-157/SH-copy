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
using System.IO;
using System.Globalization;
using System.Drawing.Drawing2D;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;


namespace jewl
{
    public partial class SaleReports : Form
    {
        CustomerDAL custDAL = new CustomerDAL();
        ItemDAL itmDAL = new ItemDAL();
        WorkerDAL wrkDAL = new WorkerDAL();
        DesignDAL desDAL = new DesignDAL();
        UtilityDAL utlDAL = new UtilityDAL();
        SaleDAL slDAL = new SaleDAL();

        public SaleReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Maximized;
            IEnumerable<Control> cont = FormControls.GetAll(this.groupBox1, typeof(RadioButton));
            foreach (var item in cont)
            {
                RadioButton r = ((RadioButton)item);
                r.BackColor = Color.FromArgb(0, 188, 212);
                r.ForeColor = Color.FromArgb(255, 255, 255);

            }
        }

        private void SaleReports_Load(object sender, EventArgs e)
        {
            rbtCompleteSale.Checked = true;
            this.pnlSaleSelection.Visible = false;
            this.rbtSaleReceiveable.Enabled = true;
            this.cbxWorker.DataSource = wrkDAL.GetAllWorkers();
            this.cbxWorker.DisplayMember = "Name";
            this.cbxWorker.ValueMember = "ID";
            this.cbxWorker.SelectedIndex = -1;
            this.cbxItemType.SelectedIndex = 1;
            this.label1.Visible = false;
            this.label5.Visible = false;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ReportViewer frm = new ReportViewer();
            try
            {
                string IType = (string)this.cbxItemType.SelectedItem;
                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                this.FormPanel.Controls.Add(frm);

                string selectQuery = "";
                if (this.rbtCompleteSale.Checked == true)
                {
                    if (cbxItemType.Text == "")
                    {
                        MessageBox.Show("First Select Item Type", Messages.Header);
                        return;
                    }
                    else
                        if (IType == "All")
                        {
                            selectQuery += "{CompleteSaleReport.IType}<>'" + IType + "'";
                        }


                        else if (IType == "OrderSale")
                        {
                            selectQuery += "{CompleteSaleReport.OrderNo}>" + 0 + "";
                        }
                        else if (IType == "BulkSale")
                        {
                            selectQuery += "{CompleteSaleReport.BStatus}='Bulk'";
                        }
                        else
                            selectQuery += "{CompleteSaleReport.IType}='" + IType + "'";
                }
                else if (rbtSaleSelection.Checked == true)
                {
                    if (cbxItemType.Text == "")
                    {
                        MessageBox.Show("First Select Item Type", Messages.Header);
                        return;
                    }
                    else
                    {
                        if (IType == "All")
                            selectQuery += "{CompleteSaleReport.IType}<>'" + IType + "'";
                        else
                            selectQuery += "{CompleteSaleReport.IType}='" + IType + "'";
                    }
                }
                else if (rbtSaleItemDetail.Checked == true)
                {
                    if (cbxItemType.Text == "")
                    {
                        MessageBox.Show("First Select Item Type", Messages.Header);
                        return;
                    }
                    else
                    {
                        selectQuery += "{CompleteSaleReport.IType}='" + IType + "'";
                        frm = new ReportViewer();
                        frm.TopLevel = false;
                        frm.FormBorderStyle = FormBorderStyle.None;
                        this.FormPanel.Controls.Add(frm);
                        selectQuery += " and {CompleteSaleReport.SaleDate}>= Date('" + dtpFromSID.Value.ToShortDateString() + "')";
                        selectQuery += " and {CompleteSaleReport.SaleDate}<= Date('" + dtpToSID.Value.ToShortDateString() + "')";
                        frm.isPage = 3;
                        frm.rpt = 2;
                        frm.iType = IType;
                        frm.selectQuery = selectQuery;
                        if (this.chkPics.Checked == true)
                        {
                            frm.id = 1;
                        }
                        frm.Show();
                        return;
                    }
                }
                else if (rbtSaleRange.Checked == true)
                {
                    if (txtFSNO.Text != "" && txtTSNO.Text != "")
                    {
                        selectQuery = "{CompleteSaleReport.SaleNo}>=" + txtFSNO.Text + "";
                        selectQuery += " and {CompleteSaleReport.SaleNo}<=" + txtTSNO.Text + "";
                    }
                    else
                    {
                        MessageBox.Show("First Enter Sale No", Messages.Header);
                        return;
                    }
                }
                else if (this.rbtSaleReceiveable.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    this.FormPanel.Controls.Add(frm);
                    frm.isPage = 3;
                    frm.rpt = 3;
                    frm.Show();
                    return;
                }
                else if (rbtSaleNo.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    this.FormPanel.Controls.Add(frm);
                    frm.isPage = 3;
                    frm.rpt = 4;
                    frm.sNo = FormControls.GetIntValue(this.txtSaleNo);
                    frm.Show();
                    return;
                }
                else if (rbtSilveSaleNo.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    this.FormPanel.Controls.Add(frm);
                    frm.isPage = 3;
                    frm.rpt = 4;
                    frm.sNo = FormControls.GetIntValue(this.txtSaleNo);
                    frm.id = 1;
                    frm.Show();
                    return;
                }
                else if (rbtOrderNo.Checked == true)
                {
                    selectQuery = "{CompleteSaleReport.OrderNo}=" + this.txtSaleNo.Text + "";
                }
                else if (rbtSaleSummary.Checked == true || rbtSaleByDate.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    this.FormPanel.Controls.Add(frm);
                    frm.Df = Convert.ToDateTime(this.dtpFromSID.Value);
                    frm.Dt = Convert.ToDateTime(this.dtpToSID.Value);
                    frm.isPage = 3;
                    frm.rpt = 5;
                    if (this.rbtSaleByDate.Checked == true)
                    {
                        frm.srid = 1;
                    }
                    frm.Show();
                    return;
                }
                else if (rbtSaleSummaryGraph.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    this.FormPanel.Controls.Add(frm);
                    frm.Df = Convert.ToDateTime(this.dtpFromSID.Value);
                    frm.Dt = Convert.ToDateTime(this.dtpToSID.Value);
                    frm.isPage = 3;
                    frm.rpt = 5;
                    frm.id = 1;
                    frm.Show();
                    return;
                }
                else if (rbtSaleSummaryByKarat.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    this.FormPanel.Controls.Add(frm);
                    frm.isPage = 3;
                    frm.rpt = 6;
                    frm.Show();
                    return;
                }
                else if (rbtSaleCom.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    this.FormPanel.Controls.Add(frm);
                    frm.Df = Convert.ToDateTime(this.dtpSaleCom.Value);
                    frm.isPage = 3;
                    frm.rpt = 7;
                    frm.Show();
                    return;
                }
                else if (rbtSaleComQty.Checked == true)
                {
                    frm = new ReportViewer();
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    this.FormPanel.Controls.Add(frm);
                    frm.Df = Convert.ToDateTime(this.dtpSaleCom.Value);
                    frm.isPage = 3;
                    frm.rpt = 8;
                    frm.Show();
                    return;
                }
                else if (rbtBillBookNo.Checked == true)
                {
                    if (txtSaleNo.Text == "")
                    {
                        MessageBox.Show("First Enter Bill Book No.", Messages.Header);
                        return;
                    }
                    else
                        selectQuery = "{CompleteSaleReport.BillBookNo}=  '" + (txtSaleNo.Text) + "'";
                }
                else if (rbtSalePicture.Checked == true)
                {
                    IType = (string)this.cbxItemType.SelectedItem;
                    frm = new ReportViewer();
                    frm.TopLevel = false;
                    frm.FormBorderStyle = FormBorderStyle.None;
                    this.FormPanel.Controls.Add(frm);
                    selectQuery = "";

                    if (this.rbtSaleSelect.Checked == true)
                    {
                        if (this.chkCustomer.Checked == true)
                        {
                            if (string.IsNullOrEmpty(selectQuery))
                                selectQuery = "{StockRpt.CustId}=" + (int)cbxCustomer.SelectedValue;
                            else selectQuery += " and {StockRpt.CustId}=" + (int)cbxCustomer.SelectedValue;
                        }
                        if (this.chkGroupItem.Checked == true)
                        {
                            Item itm = (Item)this.cbxGroupItem.SelectedItem;
                            if (string.IsNullOrEmpty(selectQuery))
                                selectQuery = "{StockRpt.ItemId}=" + itm.ItemId;
                            else selectQuery = selectQuery + "and {StockRpt.ItemId}=" + itm.ItemId;// +"'";
                        }
                        if (this.chkFromDate.Checked == true)
                        {
                            if (string.IsNullOrEmpty(selectQuery))
                            {
                                DateTime dt = dtpFrom.Value;
                                selectQuery = "{StockRpt.SaleDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                            }
                            else
                            {
                                selectQuery = selectQuery + "and {StockRpt.SaleDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                            }
                        }
                        if (this.chkToDate.Checked == true)
                        {
                            if (string.IsNullOrEmpty(selectQuery))
                            {
                                selectQuery = "{StockRpt.SaleDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                            }
                            else
                            {
                                selectQuery = selectQuery + "and {StockRpt.SaleDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                            }
                        }
                        if (this.chkWorker.Checked == true)
                        {
                            Worker wrk = (Worker)this.cbxWorker.SelectedItem;
                            if (string.IsNullOrEmpty(selectQuery))
                                selectQuery = "{StockRpt.WorkerId}=" + wrk.ID;// + "'";
                            else
                                selectQuery = selectQuery + "and {StockRpt.WorkerId}=" + wrk.ID;// +"'";
                        }

                        if (this.chkDesignNo.Checked == true)
                        {
                            Design des = (Design)this.cbxDesignNo.SelectedItem;
                            if (string.IsNullOrEmpty(selectQuery))
                                selectQuery = "{StockRpt.DesignId}=" + des.DesignId;// + "'";
                            else
                                selectQuery = selectQuery + "and {StockRpt.DesignId}='" + des.DesignId;// +"'";
                        }
                    }
                    if (!(IType.Equals("All")))
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

                    frm.isPage = 3;
                    frm.rpt = 8;
                    frm.iType = IType;
                    frm.selectQuery = selectQuery;
                    if (this.chkPics.Checked == true)
                    {
                        frm.id = 1;
                    }
                    frm.Show();
                    return;
                } 
                if (this.chkCustomer.Checked == true)
                {
                    selectQuery += " and {CompleteSaleReport.CustId}=" + (int)cbxCustomer.SelectedValue;
                }
                if (this.chkGroupItem.Checked == true)
                {
                    selectQuery += " and {CompleteSaleReport.ItemId}=" + (int)cbxGroupItem.SelectedValue;

                }
                if (this.chkWorker.Checked == true)
                {
                    Worker wrk = (Worker)this.cbxWorker.SelectedItem;
                    if (string.IsNullOrEmpty(selectQuery))
                        selectQuery = "{CompleteSaleReport.WorkerId}=" + wrk.ID;// + "'";
                    else
                        selectQuery = selectQuery + "and {CompleteSaleReport.WorkerId}=" + wrk.ID;// +"'";
                }
                if (chkFromDate.Checked == true)
                {
                    selectQuery += " and  {CompleteSaleReport.SaleDate}>= Date('" + dtpFrom.Value.ToShortDateString() + "')";
                }
                if (chkToDate.Checked == true)
                {
                    if (chkFromDate.Checked == true)
                        selectQuery += " and  {CompleteSaleReport.SaleDate}<= Date('" + dtpTo.Value.ToShortDateString() + "')";
                    else
                    {
                        MessageBox.Show("First Select From Date", " jewel Manager 2018");
                        return;
                    }
                }
                if (this.chkDesignNo.Checked == true)
                {
                    if (cbxDesignNo.Text == "")
                    {
                        MessageBox.Show("First Select Design No", Messages.Header);
                        return;
                    }
                    else
                        selectQuery += " and {CompleteSaleReport.DesignId}=" + (int)cbxDesignNo.SelectedValue;
                }
                else
                {
                    
                }
                frm.isPage = 3;
                frm.rpt = 1;
                frm.iType = IType;
                if (this.chkPics.Checked == true)
                {
                    frm.id = 1;
                }
                frm.selectQuery = selectQuery;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Messages.Header);
            }
        }
        
        private void rbtCompleteSale_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtCompleteSale.Checked == true)
            {
                this.groupBox2.BackgroundImage = null;
               
                this.pnlItemType.Visible = true;
            }
            else if (this.rbtCompleteSale.Checked == false)
                this.pnlItemType.Visible = false;

        

        }
        //----------------------------------------------------------------///
        /// ////////////////  optioin button sale selectiopn on ho ga//////////////// 
        private void rbtSaleSelection_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSaleSelection.Checked == true)
            {
                this.groupBox2.BackgroundImage = null;
                
                this.pnlSaleSelection.Visible = true;
                this.pnlItemType.Visible = true;
                this.cbxCustomer.SelectedIndex = -1;
                cbxCustomer.Enabled = false;
                chkCustomer.Checked = false;
                cbxGroupItem.Enabled = false;
                chkGroupItem.Checked = false;
                cbxDesignNo.Enabled = false;
                chkDesignNo.Checked = false;
            }
            else if (this.rbtSaleSelection.Checked == false)
            {
                this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
                this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
                this.pnlSaleSelection.Visible = false;
                this.pnlItemType.Visible = false;
                chkGroupItem.Checked = false;
            }
        }
        ///------------------------------------------------------------------------///////
        ///////////////////////////////   inside sellection panal checked /////////////////////////
        private void chkCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCustomer.Checked == true)
            {
                this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
                this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
                cbxCustomer.Enabled = true;
                FormControls.FillCombobox(this.cbxCustomer, custDAL.GetAllCustomer(), "Name", "ID");
            }
            else
            {
                this.cbxCustomer.SelectedIndex = -1;
                cbxCustomer.Enabled = false;
            }
        }
        private void chkGroupItem_CheckedChanged(object sender, EventArgs e)
        {

            if (chkGroupItem.Checked == true)
            {
                cbxGroupItem.Enabled = true;
                FormControls.FillCombobox(this.cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
            }
            else
            {
                this.cbxGroupItem.SelectedIndex = -1;
                cbxGroupItem.Enabled = false;               
            }
        }
        private void chkDesignNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDesignNo.Checked == true)
            {
                cbxDesignNo.Enabled = true;
                FormControls.FillCombobox(this.cbxDesignNo, desDAL.GetAllDesign(), "DesignNo", "DesignId");
            }
            else
            {
                this.cbxDesignNo.SelectedIndex = -1;
                cbxDesignNo.Enabled = false;
            }
        }
        private void cbxGroupItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                //label5.Visible = true;
             
            }
            catch
            { }

        }
        private void cbxSubGroupItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }
        private void chkFromDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFromDate.Checked == true)
                dtpFrom.Enabled = true;
            if (chkFromDate.Checked == false)
                dtpFrom.Enabled = false ;  
        }
        private void chkToDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkToDate.Checked == true)
                dtpTo.Enabled = true;
            if (chkToDate.Checked == false)
                dtpTo.Enabled = false;
        }
        private void rbtSaleRange_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSaleRange.Checked == true )
            {
                this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
                this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
                pnlSaleRange.Visible = true;
                pnlSaleSelection.Visible = false;
                pnlItemType.Visible = false;
            }
            if (rbtSaleRange.Checked == false)
            {
                pnlSaleRange.Visible = false ;
                //pnlSaleSelection.Visible = true;
               //pnlItemType.Visible = true;
            }
        }
        private void rbtSaleItemDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSaleItemDetail.Checked == true)
            {
                this.groupBox2.BackgroundImage = null;
                
                pnlItemDetail.Visible = true;
                pnlSaleSelection.Visible = false;
                pnlItemType.Visible = true;
            }
            if (rbtSaleItemDetail.Checked == false)
            {
                pnlItemDetail.Visible = false;
                //pnlSaleSelection.Visible = true;
                pnlItemType.Visible = false;
                this.rbtSaleSelect.Checked = false;
            }

        }
        private void rbtSaleReceiveables_CheckedChanged(object sender, EventArgs e)
        {
        }
        private void rbtSaleNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSaleNo.Checked == true)
            {
                this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
                this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
                label5.Visible = true;
                txtSaleNo.Visible = true;   
            }
            if (rbtSaleNo.Checked == false)
            {
                label5.Visible = false;
                txtSaleNo.Visible = false;
                this.rbtSaleSelect.Checked = false;
            }

        }
        private void rbtOrderNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtOrderNo.Checked == true)
            {
                this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
                this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
                label5.Visible = true;
                txtSaleNo.Visible = true;
            }
            if (rbtOrderNo.Checked == false)
            {
                label5.Visible = false;
                txtSaleNo.Visible = false;
                this.rbtSaleSelect.Checked = false;
            }
        }
        private void rbtSaleSample_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSaleSummary.Checked == true)
            {
                this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
                this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
               // label1.Visible = true;
               // dtpSaleCom.Visible = true;
                this.pnlItemDetail.Visible = true;
            }
            if (rbtSaleSummary.Checked == false)
            {
               // label1.Visible = false;
               // dtpSaleCom.Visible = false;
                this.rbtSaleSelect.Checked = false;
                this.pnlItemDetail.Visible = false;
            }
        }
        private void rbtBillBookNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtBillBookNo.Checked == true)
            {
                this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
                this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
                label5.Visible = true;
                txtSaleNo .Visible = true;

            }
            if (rbtBillBookNo.Checked == false)
            {
                label5.Visible = false;
                txtSaleNo.Visible = false;
                this.rbtSaleSelect.Checked = false;
            }
        }
        //--------------------------------------------------------------------------//
        /// ///////// all key press event 
        private void txtFSNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }
        private void txtTSNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtBillBookNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rbtSaleCom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSaleCom.Checked == true)
            {
                this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
                this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
                label1.Visible = true;
                this.dtpSaleCom.Visible = true;
            }
            else if (this.rbtSaleCom.Checked == false)
            {
                label1.Visible = false;
                this.dtpSaleCom.Visible = false;
            }
        }

        private void rbtSaleComQty_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSaleComQty.Checked == true)
                this.dtpSaleCom.Visible = true;
            else if (this.rbtSaleComQty.Checked == false)
                this.dtpSaleCom.Visible = false;
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void rbtCompSale_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtCompSale.Checked == true)
            {
                this.pnlItemType.Visible = true;
            }
            else if (this.rbtCompSale.Checked == false)
                this.pnlItemType.Visible = false;
        }

        private void rbtSaleSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSaleSelect.Checked == true)
            {
                this.pnlSaleSelection.Visible = true;
                this.pnlItemType.Visible = true;
                this.cbxCustomer.SelectedIndex = -1;
                cbxCustomer.Enabled = false;
                chkCustomer.Checked = false;
                cbxGroupItem.Enabled = false;
                chkGroupItem.Checked = false;
                cbxDesignNo.Enabled = false;
                chkDesignNo.Checked = false;
            }
            else if (this.rbtSaleSelect.Checked == false)
            {
                this.pnlSaleSelection.Visible = false;
                this.pnlItemType.Visible = false;
                chkGroupItem.Checked = false;
            }
        }

        private void chkWorker_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkWorker.Checked == true)
                this.cbxWorker.Enabled = true;
            else if (this.chkWorker.Checked == false )
                this.cbxWorker.Enabled = false;
            
        }

        private void rbtSalePicture_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSalePicture.Checked == true)
            {
                this.groupBox2.BackgroundImage = null;

               // this.pnlSaleSelection.Visible = true;
                this.pnlItemType.Visible = true;
                this.cbxCustomer.SelectedIndex = -1;
                cbxCustomer.Enabled = false;
                chkCustomer.Checked = false;
                cbxGroupItem.Enabled = false;
                chkGroupItem.Checked = false;
                cbxDesignNo.Enabled = false;
                chkDesignNo.Checked = false;
            }
            else if (this.rbtSalePicture.Checked == false)
            {
                this.pnlSaleSelection.Visible = false;
                this.pnlItemType.Visible = false;
                chkGroupItem.Checked = false;
            }
        }

        private void SaleReports_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void rbtSaleReceiveable_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
            this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
        }

        private void rbtSaleSummaryByKarat_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
            this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
        }

        private void rbtSilveSaleNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSilveSaleNo.Checked == true)
            {
                this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
                this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
                label5.Visible = true;
                txtSaleNo.Visible = true;
            }
            if (rbtSilveSaleNo.Checked == false)
            {
                label5.Visible = false;
                txtSaleNo.Visible = false;
                this.rbtSaleSelect.Checked = false;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.txtSaleNo.Text = ((this.txtSaleNo.Text == "" ? 0 : Convert.ToDecimal(this.txtSaleNo.Text))+1).ToString();
            ReportViewer frm = new ReportViewer();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            this.FormPanel.Controls.Add(frm);
            frm.isPage = 3;
            frm.rpt = 4;
            frm.sNo = FormControls.GetIntValue(this.txtSaleNo);
            frm.Show();           
        }
        private void btnPre_Click(object sender, EventArgs e)
        {
            this.txtSaleNo.Text = ((this.txtSaleNo.Text == "" ? 0 : Convert.ToDecimal(this.txtSaleNo.Text)) - 1).ToString();

            ReportViewer frm = new ReportViewer();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            this.FormPanel.Controls.Add(frm);
            frm.isPage = 3;
            frm.rpt = 4;
            frm.sNo = FormControls.GetIntValue(this.txtSaleNo);
            frm.Show();
        }

        private void btnSN_Click(object sender, EventArgs e)
        {
            this.txtSaleNo.Text = ((this.txtSaleNo.Text == "" ? 0 : Convert.ToDecimal(this.txtSaleNo.Text)) + 1).ToString();
            ReportViewer frm = new ReportViewer();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            this.FormPanel.Controls.Add(frm);
            frm.isPage = 3;
            frm.rpt = 4;
            frm.sNo = FormControls.GetIntValue(this.txtSaleNo);
            frm.id = 1;
            frm.Show();
        }

        private void btnSP_Click(object sender, EventArgs e)
        {
            this.txtSaleNo.Text = ((this.txtSaleNo.Text == "" ? 0 : Convert.ToDecimal(this.txtSaleNo.Text)) - 1).ToString();
            ReportViewer frm = new ReportViewer();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            this.FormPanel.Controls.Add(frm);
            frm.isPage = 3;
            frm.rpt = 4;
            frm.sNo = FormControls.GetIntValue(this.txtSaleNo);
            frm.id = 1;
            frm.Show();
        }

        private void rbtSaleSummaryGraph_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtSaleSummaryGraph.Checked==true)
            this.pnlItemDetail.Visible = true;
            if (rbtSaleSummaryGraph.Checked == false)
                this.pnlItemDetail.Visible = false;
        }

        private void chkPics_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            ReportViewer frm = new ReportViewer();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            this.FormPanel.Controls.Add(frm);
            frm.isPage = 3;
            frm.rpt = 4;
            frm.sNo = FormControls.GetIntValue(this.txtSaleNo);
            frm.snmbr = 1;
            frm.Show();
            return;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            ReportViewer frm = new ReportViewer();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            this.FormPanel.Controls.Add(frm);
            frm.isPage = 3;
            frm.rpt = 4;            
            frm.sNo = FormControls.GetIntValue(this.txtSaleNo);
            frm.snmbr = 2;
            frm.Show();
            return;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            ReportViewer frm = new ReportViewer();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            this.FormPanel.Controls.Add(frm);
            frm.isPage = 3;
            frm.rpt = 4;
            frm.sNo = FormControls.GetIntValue(this.txtSaleNo);
            frm.snmbr = 3;
            frm.Show();
            return;
        }

        private void rbtSaleByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSaleByDate.Checked == true)
            {
                this.groupBox2.BackgroundImage = global::jewl.Properties.Resources.Jewelry_Banner_01;
                this.groupBox2.BackgroundImageLayout = ImageLayout.Center;
                // label1.Visible = true;
                // dtpSaleCom.Visible = true;
                this.pnlItemDetail.Visible = true;
            }
            if (rbtSaleByDate.Checked == false)
            {
                // label1.Visible = false;
                // dtpSaleCom.Visible = false;
                this.rbtSaleSelect.Checked = false;
                this.pnlItemDetail.Visible = false;
            }
        }

        //--------------------------------------------------------------------------///
    }
}
