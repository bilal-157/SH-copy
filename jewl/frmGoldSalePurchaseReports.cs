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
    public partial class frmGoldSalePurchaseReports : Form
    {
        CustomerDAL custDAL = new CustomerDAL();
        private int custid;
        public int Custid
        {
            get { return custid; }
            set { custid = value; }
        }
        private DateTime to;

        public DateTime To
        {
            get { return to; }
            set { to = value; }
        }
        private DateTime from;

        public DateTime From
        {
            get { return from; }
            set { from = value; }
        }
        public bool dt { get; set; }
        public frmGoldSalePurchaseReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void rbtCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtCustomer.Checked == true)
            {
                this.panel1.Enabled = true;
                this.chkCustomerName.Enabled = true;
            }
            if (this.rbtCustomer.Checked == false)
            {
                this.chkCustomerName.Enabled = false;
            }
        }

        private void rbtCustomerSummary_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtCustomerSummary.Checked == true)
            {
                this.chkCustomerName.Enabled = false;
                this.panel1.Enabled = false;

                //if (this.rbtWorkerSummary.Checked == false)
                //{
                //    this.panel1.Enabled = true;
                //    this.rbtWorker.Checked = true;

                //}

            }
        }

    

        private void chkCustomerName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCustomerName.Checked == true)
            {
                this.chkDateFrom.Enabled = true;
                this.cbxCustomerName.Enabled = true;
            }
            if (this.chkCustomerName.Checked == false)
            {
                this.chkDateFrom.Enabled = false;
                this.cbxCustomerName.Enabled = false;
            }
        }

        private void chkDateFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDateFrom.Checked == true)
            {
                this.dtpDateFrom.Enabled = true;
                this.chkDateTo.Enabled = true;
            }
            if (this.chkDateFrom.Checked == false)
            {
                this.dtpDateFrom.Enabled = false;
                this.chkDateTo.Enabled = false;
                this.dtpDateTo.Enabled = false;
            }
        }

        private void chkDateTo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDateTo.Checked == true)
            {
                this.dtpDateTo.Enabled = true;
            }
            if (this.chkDateTo.Checked == false)
            {
                this.dtpDateTo.Enabled = false;
            }
        }

        private void frmGoldSalePurchaseReports_Load(object sender, EventArgs e)
        {
            this.cbxCustomerName.DataSource = custDAL.GetAllCustomer();
            this.cbxCustomerName.DisplayMember = "Name";
            this.cbxCustomerName.ValueMember = "ID";
            this.cbxCustomerName.SelectedIndex = -1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.rbtCustomer.Checked == true)
            {
                if (this.cbxCustomerName.SelectedIndex != -1)
                {
                    Customer cust = (Customer)this.cbxCustomerName.SelectedItem;
                    custid = Convert.ToInt32(cust.ID);
                  
                    //string selectQuery = "";



                    if (this.chkDateFrom.Checked == false)
                    {
                        Customer cust1 = (Customer)this.cbxCustomerName.SelectedItem;
                        cust1 = (Customer)this.cbxCustomerName.SelectedItem;
                        this.custid = Convert.ToInt32(cust1.ID);
                        this.dt = false;
                        //selectQuery = "{WorkerDealingsAfr.WorkerId}=" + Convert.ToInt32(wrk1.ID);
                        frmGoldPurchaseSaleRpt frm = new frmGoldPurchaseSaleRpt();
                        frm.CustomerId = this.custid;
                        frm.isdate = dt;
                        frm.ShowDialog();
                    }


                    if (this.chkDateFrom.Checked == true)
                    {
                        this.to = Convert.ToDateTime(this.dtpDateTo.Value);
                        this.from = Convert.ToDateTime(this.dtpDateFrom.Value);



                        //if (this.chkDateTo.Checked == false)
                        //{
                        //    this.from = Convert.ToDateTime(this.dtpDateFrom.Value);
                        //    this.to = Convert.ToDateTime(this.dtpDateTo.Value);
                        //}
                        //wd.Worker = (Worker)this.cbxWorkerName.SelectedItem;
                        //this.wkid = Convert.ToInt32(wd.Worker.ID);

                        //frmWorkerDealByDateRpt frmd = new frmWorkerDealByDateRpt();
                        //frmd.datefrom = this.from;
                        //frmd.dateto = this.to;
                        //frmd.wkd = this.wkid;
                        //frmd.ShowDialog
                        frmGoldPurchaseSaleRpt frm = new frmGoldPurchaseSaleRpt();
                        //test frm = new test();
                        frm.CustomerId = this.custid;
                        frm.datefrom = this.from;
                        frm.dateto = this.to;
                        frm.isdate = true;
                        frm.ShowDialog();
                    }

                }
            }
            if (this.rbtDate.Checked == true)
            {
                this.to = Convert.ToDateTime(this.dtpTo.Value);
                this.from = Convert.ToDateTime(this.dtpFrom.Value);
                frmGoldPurchaseSaleRptByDate frm = new frmGoldPurchaseSaleRptByDate();
                frm.dateFrom = this.from;
                frm.dateTo = this.to;
                frm.ShowDialog();
            }
            if (this.rbtCustomerSummary.Checked == true)
            {
                frmGoldPurchaseSaleSummaryRpt frm = new frmGoldPurchaseSaleSummaryRpt();
                frm.ShowDialog();
            }
            if (this.rbtGoldBalances.Checked == true)
            {
                frmGoldRpt frmg = new frmGoldRpt();
                frmg.df = Convert.ToDateTime(this.dtpGoldDateFrom.Value);
                frmg.dt = this.dtpGoldDateTo.Value.AddDays(1);
                frmg.ShowDialog();
            }
            if (rbtPurchaseBill.Checked == true)
            {
                frmBalanceInvoiceRpt frm = new frmBalanceInvoiceRpt();
                frm.id = 1;
                frm.selectQuery = "{GoldSalePurchaseBill.GPNO}=" + this.txtVNO.Text;
                frm.ShowDialog();
            }
            if (rbtSaleBill.Checked == true)
            {
                frmBalanceInvoiceRpt frm = new frmBalanceInvoiceRpt();
                frm.id = 1;
                frm.selectQuery = "{GoldSalePurchaseBill.GSNO}=" +this.txtVNO.Text;
                frm.ShowDialog();
            }
        }

        private void rbtDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtDate.Checked == true)
            {
                this.panel1.Visible = false;
                this.panel2.Visible = true;
                this.checFrom.Enabled = true;
            }
            if (this.rbtDate.Checked == false)
            {
                this.panel1.Visible = true;
                this.panel2.Visible = false;
                this.checFrom.Enabled = true;
            }
        }

        private void checFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checFrom.Checked == true)
            {
                this.dtpFrom.Enabled = true;
                this.ChkTo.Enabled = true;
            }
            if (this.checFrom.Checked == false)
            {
                this.dtpFrom.Enabled = false;
                this.ChkTo.Enabled = false;
                this.dtpTo.Enabled = false;
            }
        }

        private void ChkTo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChkTo.Checked == true)
            {
                this.dtpTo.Enabled = true;
            }
            if (this.ChkTo.Checked == false)
            {
                this.dtpTo.Enabled = false;
            }
        }

        private void rbtGoldBalances_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtGoldBalances.Checked == true)
            {
                this.pnlGold.Visible = true;
                this.chkGoldDateFrom.Enabled = true;
            }
            else
            {
                this.pnlGold.Visible = false;
                this.chkGoldDateFrom.Enabled = false;
            }
        }

        private void chkGoldDateFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkGoldDateFrom.Checked == true)
            {
                this.dtpGoldDateFrom.Enabled = true;
                this.chkGoldDateTo.Enabled = true;
            }
            else
            {
                this.dtpGoldDateFrom.Enabled = false;
                this.chkGoldDateTo.Enabled = false;
            }
        }

        private void chkGoldDateTo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkGoldDateTo.Checked == true)
                this.dtpGoldDateTo.Enabled = true;
            else
                this.dtpGoldDateTo.Enabled = false;
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

            FormControls.PanelBorder(sender, e);
        }

        private void pnlGold_Paint(object sender, PaintEventArgs e)
        {

            FormControls.PanelBorder(sender, e);
        }

        private void frmGoldSalePurchaseReports_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void rbtVoucherBill_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtPurchaseBill.Checked == true)
            {
               // this.txtVNO.Text = "GPV";
                txtVNO.Select();
                //txtVNO.Select(txtVNO.Text.Length, 4);

            }
            else
            {
                this.txtVNO.Text = "";
            }
        }

        private void rbtSaleBill_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSaleBill.Checked == true)
            {
                // this.txtVNO.Text = "GPV";
                txtVNO.Select();
                //txtVNO.Select(txtVNO.Text.Length, 4);

            }
            else
            {
                this.txtVNO.Text = "";
            }
        }

     
    }
}