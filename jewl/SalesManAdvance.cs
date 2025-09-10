using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class SalesManAdvance : Form
    {
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        SqlTransaction trans;

        int k = 0;
        SaleMan slm;
        SaleMan sm;
        List<SaleMan> slms = new List<SaleMan>();
        SaleManDAL slmDAL = new SaleManDAL();
        AccountDAL acDAL = new AccountDAL();
        ChildAccount cha;
        Voucher pv;
        Voucher salmv;
        VouchersDAL vDAL = new VouchersDAL();
        public SalesManAdvance()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);         
        }

        private void SalesManAdvance_Load(object sender, EventArgs e)
        {
            this.cbxEmloyeeName.SelectedIndexChanged -= new EventHandler(cbxEmloyeeName_SelectedIndexChanged);
            FormControls.FillCombobox(cbxEmloyeeName, slmDAL.GetAllSaleMen(), "Name", "ID");

            this.txtAdvanceNo.Text = (slmDAL.GetMaxAdvanceNo() + 1).ToString();
            this.txtCashInHand.Text = Math.Round((acDAL.GetCashInHandBalance()), 0).ToString();
        }

        private void cbxEmloyeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            sm = (SaleMan)this.cbxEmloyeeName.SelectedItem;
            k = (int)sm.ID;
            this.txtContactNo.Text = sm.ContactNo.ToString();
            this.txtAddress.Text = sm.Address.ToString();
            this.txtSalary.Text = Math.Round((decimal)sm.Salary, 0).ToString();
            this.ShowDGVRecord(k);
        }

        private void cbxEmloyeeName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxEmloyeeName.SelectedIndexChanged += new EventHandler(cbxEmloyeeName_SelectedIndexChanged);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            if (k == 0)
            {
                MessageBox.Show("Please select Sale Man", Messages.Header);
                return;
            }
            else
            {
                slm = new SaleMan();
                try
                {
                    slm.ID = k;
                    slm.AdvanceNo = Convert.ToInt32(this.txtAdvanceNo.Text);
                    slm.AttDate = this.dtpDate.Value;
                    slm.AdvAmount = Math.Round(Convert.ToDecimal(this.txtAdvanceAmount.Text), 0);
                    if (chkMakeInstallment.Checked == true)
                    {
                        slm.NoOfInst = Convert.ToInt32(this.txtNoOfInstallment.Text);
                        slm.InstallAmount = Math.Round(Convert.ToDecimal(this.txtInstallmentAmount.Text), 0);
                    }
                    else
                    {
                        slm.NoOfInst = 0;
                        slm.InstallAmount = 0;
                    }
                    #region Cash voucher
                    if (!(this.txtAdvanceAmount.Text == "" || this.txtAdvanceAmount.Text == "0"))
                    {
                        //cash in hand entry
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Dr = 0;
                        pv.Cr = Math.Round(Convert.ToDecimal(slm.AdvAmount), 0);
                        pv.DDate = (DateTime)slm.AttDate;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("ADV", con, trans);
                        pv.Description = "Advance Given to Sale Man " + sm.Name;
                        vDAL.AddVoucher(pv, con, trans);
                        //SaleMan account entry
                        salmv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(sm.AccountCode.ChildCode, con, trans);
                        salmv.AccountCode = child;
                        salmv.Cr = 0;
                        salmv.Dr = Math.Round((decimal)slm.AdvAmount, 0);
                        salmv.DDate = (DateTime)slm.AttDate;
                        salmv.OrderNo = 0;
                        salmv.SNO = 0;
                        salmv.VNO = pv.VNO;
                        salmv.Description = pv.Description;
                        vDAL.AddVoucher(salmv, con, trans);
                    }
                    #endregion
                    slm.Status = "Pending";
                    slmDAL.AddAdvance(slm, con, trans);
                }
                catch(Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    trans.Commit();
                    con.Close();
                    MessageBox.Show("Record saved successfully");
                    this.ShowDGVRecord(k);
                    this.txtAdvanceNo.Text = (slmDAL.GetMaxAdvanceNo() + 1).ToString();
                    this.txtCashInHand.Text = (acDAL.GetCashInHandBalance()).ToString();
                    this.txtAdvanceAmount.Text = "";
                    this.chkMakeInstallment.Checked = false;
                    this.txtNoOfInstallment.Text = "";
                    this.txtInstallmentAmount.Text = "";
                }
            }
        }
        private void ShowDGVRecord(int k)
        {
            slms = slmDAL.GetAdvanceHistByEmp(k);
            if (slms == null)
            {
                this.dgvPreviousBalance.Rows.Clear();
                return;
            }
            else
            {
                this.dgvPreviousBalance.AutoGenerateColumns = false;
                this.dgvPreviousBalance.Rows.Clear();
                int count = slms.Count;
                this.dgvPreviousBalance.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvPreviousBalance.Rows[i].Cells[0].Value = slms[i].AttDate.ToString("dd-MM-yyyy");
                    this.dgvPreviousBalance.Rows[i].Cells[1].Value = Math.Round((decimal)slms[i].AdvAmount, 0).ToString();
                    this.dgvPreviousBalance.Rows[i].Cells[2].Value = Math.Round((decimal)slms[i].NoOfInst, 0).ToString();
                    this.dgvPreviousBalance.Rows[i].Cells[3].Value = Math.Round((decimal)slms[i].InstallAmount, 0).ToString();
                    this.dgvPreviousBalance.Rows[i].Cells[4].Value = Math.Round((decimal)slms[i].RemainingAmount, 0).ToString();
                }
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNoOfInstallment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void chkMakeInstallment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMakeInstallment.Checked == true)
                this.txtNoOfInstallment.ReadOnly = false;
            else
                this.txtNoOfInstallment.ReadOnly = true;
        }

        private void txtNoOfInstallment_TextChanged(object sender, EventArgs e)
        {
            decimal a = 0;
            decimal b = 0;

            if (this.txtAdvanceAmount.Text == "" || this.txtNoOfInstallment.Text == "")
            {
                a = 0;
                b = 1;
                this.txtInstallmentAmount.Text = (a / b).ToString();
            }
            else if (this.txtAdvanceAmount.Text != "" && this.txtNoOfInstallment.Text == "")
            {
                a = Convert.ToDecimal(this.txtAdvanceAmount.Text);
                b = 1;
                this.txtInstallmentAmount.Text = Math.Round((a / b), 0).ToString();
            }
            else
            {
                a = Convert.ToDecimal(this.txtAdvanceAmount.Text);
                b = Convert.ToDecimal(this.txtNoOfInstallment.Text);
                this.txtInstallmentAmount.Text = Math.Round((a / b), 0).ToString();
            }
        }
    }
}
