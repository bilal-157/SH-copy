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
    public partial class frmWorkerDealingReports : Form
    {
        WorkerDAL wkDal = new WorkerDAL();
        WorkerDealing wd = new WorkerDealing();
        public frmWorkerDealingReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this); this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }
        private int wkid;
            public int Wkid
       {
              get { return wkid; }
              set { wkid = value; }
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
           
     private void frmWorkerDealingReports_Load(object sender, EventArgs e)
        {
            this.cbxWorkerName.DataSource = wkDal.GetAllWorkers();
            this.cbxWorkerName.DisplayMember = "Name";
            this.cbxWorkerName.ValueMember = "ID";
            this.cbxWorkerName.SelectedIndex = -1;

        }

        private void rbtWorker_CheckedChanged(object sender, EventArgs e)
        {
            //this.rbtWorker.Checked = true;
            if (this.rbtWorker.Checked == true)
           {
               this.panel1.Enabled = true;
                this.chkWorkerName.Enabled = true;
           }
            if (this.rbtWorker.Checked == false)
            {
                this.chkWorkerName.Enabled = false;
            }
        }

        private void rbtWorkerSummary_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtWorkerSummary.Checked == true)
            {
                this.chkWorkerName.Enabled = false;
                this.panel1.Enabled = false;

                //if (this.rbtWorkerSummary.Checked == false)
                //{
                //    this.panel1.Enabled = true;
                //    this.rbtWorker.Checked = true;

                //}

            }
            //if (this.rbtWorkerSummary.Checked == false)
            //{
            //    this.rbtWorker.Checked = true;
            //}
        }

        private void rbtWorkerList_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtWorkerList.Checked == true)
            {
                this.chkWorkerName.Enabled = false;
                this.panel1.Enabled = false;
            }
            //if (this.rbtWorkerList.Checked == false)
            //{
            //    this.rbtWorker.Checked = true;
            //    this.panel1.Enabled = true;
            //}
           
           
                //this.chkWorkerName.Enabled = false;
            
        }

        private void chkWorkerName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkWorkerName.Checked == true)
            {
                this.cbxWorkerName.Enabled = true;
                this.chkDateFrom.Enabled = true;
            }
            if (this.chkWorkerName.Checked == false)
            {
                this.cbxWorkerName.Enabled = false;
                this.chkDateFrom.Enabled = false;
            }
        }

        private void chkDateFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDateFrom.Checked == true)
            {
                this.dtpDateFrom.Enabled = true;
            }
            else
                this.dtpDateFrom.Enabled = false;
        }

        private void chkWorkerName_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.chkWorkerName.Checked == true)
            {
                this.chkDateFrom.Enabled = true;
                this.cbxWorkerName.Enabled = true;
                this.chkStoneDetail.Enabled = true;
            }
            if (this.chkWorkerName.Checked == false)
            {
                this.chkDateFrom.Enabled = false;
                this.cbxWorkerName.Enabled = false;
                this.chkStoneDetail.Enabled = false ;
            }
            
        }

        private void chkDateFrom_CheckedChanged_1(object sender, EventArgs e)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.rbtWorker.Checked == true)
            {
                if (this.cbxWorkerName.SelectedIndex != -1)
                {
                    Worker wrk = (Worker)this.cbxWorkerName.SelectedItem;
                    wkid = Convert.ToInt32(wrk.ID);
                    if (this.chkDateFrom.Checked == false && this.chkStoneDetail.Checked == false)
                    {
                        Worker wrk1 = (Worker)this.cbxWorkerName.SelectedItem;
                        this.wkid = Convert.ToInt32(wrk1.ID);//(wd.Worker.ID);
                        this.dt = false;
                        frmWorkerCheejadRpt frm = new frmWorkerCheejadRpt();
                        frm.wrkid = this.wkid;
                        frm.isdate = dt;
                        frm.ShowDialog();
                    }
                    if (this.chkDateFrom.Checked == false && this.chkStoneDetail.Checked == true )
                    {
                        Worker wrk1 = (Worker)this.cbxWorkerName.SelectedItem;
                        this.wkid = Convert.ToInt32(wrk1.ID);//(wd.Worker.ID);
                        this.dt = false;
                        frmWDDetailByWorker frm = new frmWDDetailByWorker();
                        frm.workerid = this.wkid;
                        frm.ShowDialog();
                    }
                    if (this.chkDateFrom.Checked == true)
                    {
                        this.to = Convert.ToDateTime(this.dtpDateTo.Value);
                        this.from = Convert.ToDateTime(this.dtpDateFrom.Value);
                        frmWorkerCheejadRpt frm = new frmWorkerCheejadRpt();
                        frm.wrkid = wkid;
                        frm.datefrom = this.from;
                        frm.dateto = this.to;
                        frm.isdate = true;
                        frm.ShowDialog();
                    }
                   
                }
            }
            if (this.rbtWorkerSummary.Checked == true)
            {
                frmWorkerDealingSummaryRpt frm = new frmWorkerDealingSummaryRpt();
                frm.ShowDialog();
            }
            if (this.rbtWorkerList.Checked == true)
            {
                frmWorkerListRpt frm = new frmWorkerListRpt();
                frm.ShowDialog();
            }
            if (this.rbtBillNo.Checked == true)
            {
                WorkerDealingBill frm = new WorkerDealingBill();
                frm.BillNo = Convert.ToInt32(this.txtBillNo.Text);
                frm.ShowDialog();
            }
      }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmWorkerDealingReports_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void rbtBillNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtBillNo.Checked == true)
            {
                this.chkWorkerName.Enabled = false;
                this.panel1.Enabled = false;
            }

            else
            {
                this.panel1.Enabled = true;
            }
        }

       
    }
}
