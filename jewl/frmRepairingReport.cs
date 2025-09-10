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
    public partial class frmRepairingReport : Form
    {
        CustomerDAL cDAL = new CustomerDAL();
        WorkerDAL wDAL = new WorkerDAL();
        public frmRepairingReport()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }
        string selectQuery;
        private void btnView_Click(object sender, EventArgs e)
        {
            if (this.rbtReapirBill.Checked == true)
            {
                frmRepairingRpt reload1 = new frmRepairingRpt();
                string selectQuery = "select rep.*, repd.*, ci.* from tblrepair rep inner join CustomerInfo ci on ci.CustId=rep.CustId inner join tblRepairDetail repd on repd.RepairId=rep.RepairId where rep.RepairId = '" + int.Parse(txtReapirBill.Text) + "'";
                reload1.selectQuery = selectQuery;
                reload1.ShowDialog();
            }
            else
            {
                selectQuery = "";
                frmRepairingRpt frm = new frmRepairingRpt();
                if (rbtRepairingGivenToWorker.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{CompleteRepairing.repairingstatus}='Reparing'";
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {CompleteRepairing.repairingstatus}='Reparing'";
                    }
                }
                if (rbtReceiveFromWorker.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{CompleteRepairing.Rstatus}='Repaired'";
                        selectQuery = selectQuery + "and {CompleteRepairing.Status}='Not Deliverd'";
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {CompleteRepairing.Rstatus}='Repaired'";
                        selectQuery = selectQuery + "and {CompleteRepairing.Status}='Not Deliverd'";
                    }

                }
                if (rbtDelivered.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{CompleteRepairing.status}='Deliverd'";
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {CompleteRepairing.status}='Deliverd'";
                    }
                }
                if (this.chkFrom.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{CompleteRepairing.ReceivedDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {CompleteRepairing.ReceivedDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                    }
                }
                if (this.chkTo.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{CompleteRepairing.ReceivedDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {CompleteRepairing.ReceivedDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    }
                }
                if (this.chkDDateFrom.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{CompleteRepairing.DeliveryDate}>=Date('" + dtpDFrom.Value.ToShortDateString() + "')";
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {CompleteRepairing.DeliveryDate}>=Date('" + dtpDFrom.Value.ToShortDateString() + "')";
                    }
                }
                if (this.chkDDateTo.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{CompleteRepairing.DeliveryDate}<=Date('" + dtpDTo.Value.ToShortDateString() + "')";
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {CompleteRepairing.DeliveryDate}<=Date('" + dtpDTo.Value.ToShortDateString() + "')";
                    }
                }
                if (this.chkWorker.Checked == true && this.cbxWorker.SelectedIndex != -1)
                {
                    Worker wrk = (Worker)this.cbxWorker.SelectedItem;
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{CompleteRepairing.WorkerId}=" + wrk.ID;
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {CompleteRepairing.WorkerId}=" + wrk.ID;
                    }
                }
                if (this.chkCustomer.Checked == true && this.cbxCustomer.SelectedIndex != -1)
                {
                    Customer wrk = (Customer)this.cbxCustomer.SelectedItem;
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{CompleteRepairing.CustId}=" + wrk.ID;
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {CompleteRepairing.CustId}=" + wrk.ID;
                    }
                }
                frm.selectQuery = selectQuery;
                frm.id = 1;
                frm.ShowDialog();
            }
        }

        private void frmRepairingReport_Load(object sender, EventArgs e)
        {
            this.cbxCustomer.DataSource = cDAL.GetAllCustomer();
            this.cbxCustomer.DisplayMember = "Name";
            this.cbxCustomer.ValueMember = "Id";
            this.cbxCustomer.SelectedIndex = -1;

            this.cbxWorker.DataSource = wDAL.GetAllWorkers();
            this.cbxWorker.DisplayMember = "Name";
            this.cbxWorker.ValueMember = "Id";
            this.cbxWorker.SelectedIndex = -1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtReapirBill_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtReapirBill.Checked == true)
            {
                this.pnlBill.Visible = true;
                this.pnlSampleDateWise.Visible = false;
            }
            if (this.rbtReapirBill.Checked == false)
            {
                this.pnlBill.Visible = false;
                this.pnlSampleDateWise.Visible = true;
            }
        }

        private void chkFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkFrom.Checked == true)
            {
                this.dtpFrom.Enabled = true;
            }
            if (this.chkFrom.Checked == false)
            {
                this.dtpFrom.Enabled = false;
            }
        }

        private void chkTo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTo.Checked == true)
            {
                this.dtpTo.Enabled = true;
            }
            if (this.chkTo.Checked == false)
            {
                this.dtpTo.Enabled = false;
            }
        }

        private void chkDDateFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDDateFrom.Checked == true)
            {
                this.dtpDFrom.Enabled = true;
            }
            if (this.chkDDateFrom.Checked == false)
            {
                this.dtpDFrom.Enabled = false;
            }
        }

        private void chkDDateTo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDDateTo.Checked == true)
            {
                this.dtpDTo.Enabled = true;
            }
            if (this.chkDDateTo.Checked == false)
            {
                this.dtpDTo.Enabled = false;
            }
        }

        private void chkCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCustomer.Checked == true)
            {
                this.cbxCustomer.Enabled = true;
            }
            if (this.chkCustomer.Checked == false)
            {
                this.cbxCustomer.Enabled = false;
            }
        }

        private void chkWorker_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkWorker.Checked == true)
            {
                this.cbxWorker.Enabled = true;
            }
            if (this.chkWorker.Checked == false)
            {
                this.cbxWorker.Enabled = false;
            }
        }
    }
}