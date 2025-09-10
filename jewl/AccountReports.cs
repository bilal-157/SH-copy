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
    public partial class AccountReports : Form
    {
        AccountDAL acDAL = new AccountDAL();
        ChildAccount ca = new ChildAccount();
        ParentAccount pa = new ParentAccount();
        SubGroupAccount sga = new SubGroupAccount();
        GroupAccount ga = new GroupAccount();
        public AccountReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this); FormControls.GetAllControls(this); this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

        }

        private void rbtParentReport_MouseClick(object sender, MouseEventArgs e)
        {
            this.cbxParentName.Visible = true;
            this.cbxChildAccount.Visible = false ;
            this.cbxHeadAccount.Visible = true;
            this.lblParentName.Visible = true;
            this.lblChildAccount.Visible = true;
            this.lblHeadAccount.Visible = true;
            this.lblFrom.Visible = true;
            this.lblTo.Visible = true;
            this.dtpFrom.Visible = true;
            this.dtpTo.Visible = true;
        }

        private void rbtDayBook_MouseClick(object sender, MouseEventArgs e)
        {
            this.cbxParentName.Visible = false ;
            this.cbxChildAccount.Visible = false;
            this.cbxHeadAccount.Visible = true;
            this.lblParentName.Visible = true;
            this.lblChildAccount.Visible = true;
            this.lblHeadAccount.Visible = true;
            this.lblFrom.Visible = true;
            this.lblTo.Visible = true;
            this.dtpFrom.Visible = true;
            this.dtpTo.Visible = true;
        }

        private void rbtHeadReport_MouseClick(object sender, MouseEventArgs e)
        {
            this.cbxParentName.Visible = false;
            this.cbxChildAccount.Visible = false;
            this.cbxHeadAccount.Visible = true;
            this.lblParentName.Visible = true;
            this.lblChildAccount.Visible = true;
            this.lblHeadAccount.Visible = true;
            this.lblFrom.Visible = true;
            this.lblTo.Visible = true;
            this.dtpFrom.Visible = true;
            this.dtpTo.Visible = true;
        }

        private void rbtTrialBalance_MouseClick(object sender, MouseEventArgs e)
        {
            this.cbxParentName.Visible = false;
            this.cbxChildAccount.Visible = false;
            this.cbxHeadAccount.Visible = false;
            this.lblParentName.Visible = false;
            this.lblChildAccount.Visible = false;
            this.lblHeadAccount.Visible = false;
            this.lblFrom.Visible = false;
            this.lblTo.Visible = false;
            this.dtpFrom.Visible = false;
            this.dtpTo.Visible = false;
        }

        private void rbtComprehensiveLedger_MouseClick(object sender, MouseEventArgs e)
        {
            this.cbxParentName.Visible = true;
            this.cbxChildAccount.Visible = true;
            this.cbxHeadAccount.Visible = true;
            this.lblParentName.Visible = true;
            this.lblChildAccount.Visible = true;
            this.lblHeadAccount.Visible = true;
            this.lblFrom.Visible = true;
            this.lblTo.Visible = true;
            this.dtpFrom.Visible = true;
            this.dtpTo.Visible = true;
        }

        private void rbtProfiyAndLoss_MouseClick(object sender, MouseEventArgs e)
        {
            this.cbxParentName.Visible = false;
            this.cbxChildAccount.Visible = false;
            this.cbxHeadAccount.Visible = false;
            this.lblParentName.Visible = false;
            this.lblChildAccount.Visible = false;
            this.lblHeadAccount.Visible = false;
            this.lblFrom.Visible = true;
            this.lblTo.Visible = true;
            this.dtpFrom.Visible = true;
            this.dtpTo.Visible = true;
        }

        private void rbtChartOfAccount_MouseClick(object sender, MouseEventArgs e)
        {
            this.cbxParentName.Visible = false;
            this.cbxChildAccount.Visible = false;
            this.cbxHeadAccount.Visible = false;
            this.lblParentName.Visible = false;
            this.lblChildAccount.Visible = false;
            this.lblHeadAccount.Visible = false;
            this.lblFrom.Visible = false;
            this.lblTo.Visible = false;
            this.dtpFrom.Visible = false;
            this.dtpTo.Visible = false;
        }

        private void rbtAccountLedger_MouseClick(object sender, MouseEventArgs e)
        {
            this.cbxParentName.Visible = true;
            this.cbxChildAccount.Visible = true;
            this.cbxHeadAccount.Visible = true;
            this.lblParentName.Visible = true;
            this.lblChildAccount.Visible = true;
            this.lblHeadAccount.Visible = true;
            this.lblFrom.Visible = true;
            this.lblTo.Visible = true;
            this.dtpFrom.Visible = true;
            this.dtpTo.Visible = true;
        }

        private void cbxHeadAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxHeadAccount.SelectedIndexChanged += new System.EventHandler(this.cbxHeadAccount_SelectedIndexChanged);
        }

        private void cbxHeadAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            int k = 0;
            if (this.cbxHeadAccount.Text == "Asset")
                k = 1;
            else if (this.cbxHeadAccount.Text == "Liability")
                k = 2;
            else if (this.cbxHeadAccount.Text == "Expense")
                k = 3;
            else if (this.cbxHeadAccount.Text == "Revenue")
                k = 4;
            else if (this.cbxHeadAccount.Text == "Capital")
                k = 5;
            FormControls.FillCombobox(cbxParentName, acDAL.GetParentByHeadCode(k), "ParentName", "ParentCode");
        }

        private void AccountReports_Load(object sender, EventArgs e)
        {
            this.cbxHeadAccount.SelectedIndexChanged -= new System.EventHandler(this.cbxHeadAccount_SelectedIndexChanged);
            this.cbxParentName.SelectedIndexChanged -= new System.EventHandler(this.cbxParentName_SelectedIndexChanged);
            this.cbxChildAccount.SelectedIndexChanged -= new System.EventHandler(this.cbxChildAccount_SelectedIndexChanged);
            //this.cbxHeadAccount.DataSource = acDAL.Allhead();
            //this.cbxHeadAccount.DisplayMember = "HeadName";
            //this.cbxHeadAccount.ValueMember = "HeadCode";
            this.cbxHeadAccount.SelectedIndex = -1;
            this.rbtAccountLedger.Checked = true;
        }
               
        private void cbxParentName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = (string)this.cbxParentName.SelectedValue;
            FormControls.FillCombobox(cbxChildAccount, acDAL.GetAllChildAccounts("select ca.*, ((select isnull(sum(OpeningCash), 0) from ChildAccount where ChildCode = ca.ChildCode) - (select isnull(sum(dr) - sum(cr), 0) from vouchers where AccountCode = ca.ChildCode)) as Balance from ChildAccount ca  where ca.ParentCode='" + str + "' order by ChildName"), "ChildName", "Childcode");
        }

        private void cbxParentName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxParentName.SelectedIndexChanged += new System.EventHandler(this.cbxParentName_SelectedIndexChanged);
        }

        private void cbxChildAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void cbxChildAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (rbtVoucherBill.Checked == true)
            {
                frmBalanceInvoiceRpt frm1 = new frmBalanceInvoiceRpt();
                frm1.selectQuery = "{VoucherInvoice.VNo}='" + this.txtVNO.Text + "'";
                frm1.ShowDialog();

            }
            if (this.rbtDailyCashGoldInOut.Checked == true)
            {
                frmCustomerRpt frm = new frmCustomerRpt();
                frm.id = 1;
                frm.Datef = Convert.ToDateTime(this.dtpFrom.Value);
                frm.Datet = Convert.ToDateTime(this.dtpTo.Value);
                frm.ShowDialog();
            }
            else
            {
                ReportViewer frm = new ReportViewer();
                if (this.rbtAccountLedger.Checked == true && this.cbxChildAccount.SelectedIndex != -1)
                {
                    //if (ValidateChildren(ValidationConstraints.Enabled))
                    {
                        ca = (ChildAccount)this.cbxChildAccount.SelectedItem;
                        frm.isPage = 5;
                        frm.rpt = 1;
                        frm.accountCode = ca.ChildCode.ToString();
                        frm.Df = this.dtpFrom.Value;
                        frm.Dt = this.dtpTo.Value;
                    }
                }
                else if (this.rbtParentReport.Checked == true)
                {
                    //if (ValidateChildren(ValidationConstraints.Enabled))
                    {
                        pa = (ParentAccount)this.cbxParentName.SelectedItem;
                        frm.isPage = 5;
                        frm.rpt = 2;
                        frm.parentCode = pa.ParentCode.ToString();
                        frm.Df = this.dtpFrom.Value;
                        frm.Dt = this.dtpTo.Value;
                    }
                }
                else if (this.rbtHeadReport.Checked == true)
                {
                    int k = 0;
                    if (this.cbxHeadAccount.Text == "Asset")
                        k = 1;
                    else if (this.cbxHeadAccount.Text == "Liability")
                        k = 2;
                    else if (this.cbxHeadAccount.Text == "Expense")
                        k = 3;
                    else if (this.cbxHeadAccount.Text == "Revenue")
                        k = 4;
                    else if (this.cbxHeadAccount.Text == "Capital")
                        k = 5;
                    frm.isPage = 5;
                    frm.rpt = 3;
                    frm.headCode = k;
                }
                else if (this.rbtGoldLedger.Checked == true)
                {
                    ca = (ChildAccount)this.cbxChildAccount.SelectedItem;
                    frm.isPage = 5;
                    frm.rpt = 4;
                    frm.accountCode = (string)ca.ChildCode;
                }
                else if (this.rbtComprehensiveLedger.Checked == true)
                {
                    //if (ValidateChildren(ValidationConstraints.Enabled))
                    {
                        ca = (ChildAccount)this.cbxChildAccount.SelectedItem;
                        frm.isPage = 5;
                        frm.rpt = 5;
                        frm.accountCode = (string)ca.ChildCode;
                        frm.Df = this.dtpFrom.Value;
                        frm.Dt = this.dtpTo.Value;
                    }
                }
                else if (this.rbtTrialBalance.Checked == true)
                {
                    frm.isPage = 5;
                    frm.rpt = 6;
                }
                else if (this.rbtDayBook.Checked == true)
                {
                    frm.isPage = 5;
                    frm.rpt = 7;
                    frm.Df = this.dtpFrom.Value;
                }
                frm.ShowDialog();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void frmAccountReports_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void cbxHeadAccount_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbxHeadAccount.Text))
            {
                e.Cancel = true;
                cbxHeadAccount.Focus();
                errorProvider1.SetError(cbxHeadAccount, "Head Account" + Messages.Empty);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cbxHeadAccount, "");
            }
        }

        private void cbxParentName_Validating(object sender, CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(cbxHeadAccount.Text))
            //{
            //    e.Cancel = true;
            //    cbxHeadAccount.Focus();
            //    errorProvider1.SetError(cbxHeadAccount, "Head Account" + Messages.Empty);
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(cbxHeadAccount, "");
            //}
        }

        private void cbxChildAccount_Validating(object sender, CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(cbxChildAccount.Text))
            //{
            //    e.Cancel = true;
            //    cbxHeadAccount.Focus();
            //    errorProvider1.SetError(cbxChildAccount, "Child Account" + Messages.Empty);
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(cbxChildAccount, "");
            //}
        }

        private void rbtVoucherBill_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtVoucherBill.Checked == true)
            {
                this.txtVNO.Text = "CPV";
                txtVNO.Focus();
                txtVNO.Select(txtVNO.Text.Length, 4);

            }
            else
            {
                this.txtVNO.Text = "";
            }
        }

        private void rbtParentReport_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
