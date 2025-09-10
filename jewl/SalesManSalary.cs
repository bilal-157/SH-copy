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
    public partial class SalesManSalary : Form
    {
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        SqlTransaction trans;
        decimal j = 0;
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
        public string IsMonthlySalary = "";

        public SalesManSalary()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void SalesManSalary_Load(object sender, EventArgs e)
        {
            this.cbxEmloyeeName.SelectedIndexChanged -= new EventHandler(cbxEmloyeeName_SelectedIndexChanged);
            this.cbxAdvanceNo.SelectedIndexChanged -= new EventHandler(cbxAdvanceNo_SelectedIndexChanged);
            this.cbxEmloyeeName.DataSource = slmDAL.GetAllSaleMen();
            this.cbxEmloyeeName.DisplayMember = "Name";
            this.cbxEmloyeeName.ValueMember = "ID";
            this.cbxEmloyeeName.SelectedIndex = -1;

            this.txtCashInHand.Text = Math.Round((acDAL.GetCashInHandBalance()), 0).ToString();
            IsMonthlySalary = slmDAL.GetSalaryCalculation();
        }

        private void cbxEmloyeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sm = (SaleMan)this.cbxEmloyeeName.SelectedItem;
                if (sm != null)
                {
                    k = (int)sm.ID;
                    this.txtContactNo.Text = sm.ContactNo.ToString();
                    this.txtAddress.Text = sm.Address.ToString();
                    this.txtSalaryAmount.Text = Math.Round((decimal)sm.Salary, 0).ToString();
                    decimal PerDaySal = 0, HalfDaySal = 0;
                    int i = 0, l = 0;
                    if (IsMonthlySalary == "With Attendance")
                    {
                        int numberOfSunday = NumberOfParticularDaysInMonth(DateTime.Now.Year, DateTime.Now.Month, DayOfWeek.Sunday);
                        PerDaySal = (decimal)(sm.Salary) / (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - numberOfSunday);
                        i = slmDAL.GetDaysOfAttendence(this.dtpDate.Value, k);
                        l = slmDAL.GetDaysOfHalfAttendence(this.dtpDate.Value, k);
                        HalfDaySal = PerDaySal / 2;
                        this.txtSalaryOfMonth.Text = Math.Round(((HalfDaySal * l) + (PerDaySal * i)), 0).ToString();
                    }
                    else
                        this.txtSalaryOfMonth.Text = this.txtSalaryAmount.Text;
                    bool f = slmDAL.isSalaryMonthExist(this.dtpDate.Value, k);
                    if (f == true)
                        this.txtSalaryOfMonth.Text = "0";
                    j = (HalfDaySal * l) + (PerDaySal * i);
                    this.ShowDGVRecord(k);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int NumberOfParticularDaysInMonth(int year, int month, DayOfWeek dayOfWeek)
        {
            DateTime startDate = new DateTime(year, month, 1);
            int totalDays = startDate.AddMonths(1).Subtract(startDate).Days;
            int answer = Enumerable.Range(1, totalDays).Select(item => new DateTime(year, month, item)).Where(date => date.DayOfWeek == dayOfWeek).Count();
            return answer;
        }

        private void cbxEmloyeeName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxEmloyeeName.SelectedIndexChanged += new EventHandler(cbxEmloyeeName_SelectedIndexChanged);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();

            int a = this.dtpDate.Value.Month;
            decimal b, c = 0;
            bool f = slmDAL.isSalaryMonthExist(this.dtpDate.Value, k);
            if (k == 0)
            {
                MessageBox.Show("Please select Sale Man", Messages.Header);
                return;
            }
            else
            {
                slm = new SaleMan();
                c = FormControls.GetDecimalValue(this.txtCashInHand, 0);
                if (f == true)
                {
                    MessageBox.Show("Salary month is already exist", Messages.Header);
                    return;
                }
                else
                {
                    try
                    {
                        slm.ID = k;
                        slm.AttDate = this.dtpDate.Value;
                        if (chkAlownce.Checked == true)
                        {
                            slm.Alownce = FormControls.GetDecimalValue(this.txtAlownce, 0);
                            slm.Description = "Alownce of Rs." + slm.Alownce + " Given";
                        }
                        else
                        {
                            slm.Alownce = 0;
                            slm.Description = "";
                        }
                        if (chkInstallmentPaid.Checked == true)
                        {
                            slm.AdvanceNo = (int)this.cbxAdvanceNo.SelectedValue;
                            if (this.txtInstallmentAmount.Text == "")
                            {
                                MessageBox.Show("There is no installment amount", Messages.Header);
                                return;
                            }
                            else
                                slm.InstallAmount = FormControls.GetDecimalValue(this.txtInstallmentAmount, 0);
                            b = FormControls.GetDecimalValue(this.txtRemainingSalary, 0);
                            if (b > c)
                            {
                                MessageBox.Show("Salary can't be greater than cash in hand", Messages.Header);
                                return;
                            }
                            else
                                slm.Salary = FormControls.GetDecimalValue(this.txtRemainingSalary, 0);
                            #region Expense voucher
                            if (!(this.txtSalaryOfMonth.Text == "" || this.txtSalaryOfMonth.Text == "0"))
                            {
                                //cash in hand entry
                                pv = new Voucher();
                                cha = acDAL.GetAccount(3, "Salary Expense", "Saleman Expense", con, trans);
                                if (cha == null)
                                {
                                    string Coode = acDAL.CreateAccount(3, "Salary Expense", "Saleman Expense", "General Account", con, trans);
                                    cha = acDAL.GetAccount(3, "Salary Expense", "Saleman Expense", con, trans);
                                }
                                pv.AccountCode = cha;
                                pv.Dr = FormControls.GetDecimalValue(this.txtSalaryOfMonth, 0);
                                pv.Cr = 0;
                                pv.DDate = (DateTime)slm.AttDate;
                                pv.OrderNo = 0;
                                pv.SNO = 0;
                                pv.VNO = vDAL.CreateVNO("XPV", con, trans);
                                pv.Description = "Salary Expense of " + sm.Name + " of Month " + a.ToString();
                                vDAL.AddVoucher(pv, con, trans);
                                //SaleMan account entry
                                salmv = new Voucher();
                                ChildAccount child = new ChildAccount();
                                child = acDAL.GetChildByCode(sm.AccountCode.ChildCode, con, trans);
                                salmv.AccountCode = child;
                                salmv.Cr = FormControls.GetDecimalValue(this.txtSalaryOfMonth, 0);
                                salmv.Dr = 0;
                                salmv.DDate = (DateTime)slm.AttDate;
                                salmv.OrderNo = 0;
                                salmv.SNO = 0;
                                salmv.VNO = pv.VNO;
                                salmv.Description = pv.Description;
                                vDAL.AddVoucher(salmv, con, trans);
                            }
                            #endregion
                            #region Cash voucher
                            if (!(this.txtSalaryOfMonth.Text == "" || this.txtSalaryOfMonth.Text == "0"))
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
                                pv.Cr = Math.Round(Convert.ToDecimal(slm.Salary), 0);
                                pv.DDate = (DateTime)slm.AttDate;
                                pv.OrderNo = 0;
                                pv.SNO = 0;
                                pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                                pv.Description = "Salary Given to " + sm.Name + " of Month " + a.ToString();
                                vDAL.AddVoucher(pv, con, trans);
                                //SaleMan account entry
                                salmv = new Voucher();
                                ChildAccount child = new ChildAccount();
                                child = acDAL.GetChildByCode(sm.AccountCode.ChildCode, con, trans);
                                salmv.AccountCode = child;
                                salmv.Cr = 0;
                                salmv.Dr = Math.Round((decimal)slm.Salary, 0);
                                salmv.DDate = (DateTime)slm.AttDate;
                                salmv.OrderNo = 0;
                                salmv.SNO = 0;
                                salmv.VNO = pv.VNO;
                                salmv.Description = pv.Description;
                                vDAL.AddVoucher(salmv, con, trans);
                            }
                            #endregion

                            slmDAL.AddSalary(slm, con, trans);
                        }
                        else
                        {
                            slm.AdvanceNo = 0;
                            slm.InstallAmount = 0;
                            b = FormControls.GetDecimalValue(this.txtSalaryOfMonth, 0);
                            if (b > c)
                            {
                                MessageBox.Show("Salary can't be greater than cash in hand", Messages.Header);
                                return;
                            }
                            else
                                slm.Salary = FormControls.GetDecimalValue(this.txtSalaryOfMonth, 0);
                            #region Expense voucher
                            if (!(this.txtSalaryOfMonth.Text == "" || this.txtSalaryOfMonth.Text == "0"))
                            {
                                //cash in hand entry
                                pv = new Voucher();
                                cha = acDAL.GetAccount(3, "Salary Expense", "Saleman Expense", con, trans);
                                if (cha == null)
                                {
                                    string Coode = acDAL.CreateAccount(3, "Salary Expense", "Saleman Expense", "General Account", con, trans);
                                    cha = acDAL.GetAccount(3, "Salary Expense", "Saleman Expense", con, trans);
                                }
                                pv.AccountCode = cha;
                                pv.Dr = FormControls.GetDecimalValue(this.txtSalaryOfMonth, 0);
                                pv.Cr = 0;
                                pv.DDate = (DateTime)slm.AttDate;
                                pv.OrderNo = 0;
                                pv.SNO = 0;
                                pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                                pv.Description = "Salary Expence of " + sm.Name + " of Month " + a.ToString();
                                vDAL.AddVoucher(pv, con, trans);
                                //SaleMan account entry
                                salmv = new Voucher();
                                ChildAccount child = new ChildAccount();
                                child = acDAL.GetChildByCode(sm.AccountCode.ChildCode, con, trans);
                                salmv.AccountCode = child;
                                salmv.Cr = FormControls.GetDecimalValue(this.txtSalaryOfMonth, 0);
                                salmv.Dr = 0;
                                salmv.DDate = (DateTime)slm.AttDate;
                                salmv.OrderNo = 0;
                                salmv.SNO = 0;
                                salmv.VNO = pv.VNO;
                                salmv.Description = pv.Description;
                                vDAL.AddVoucher(salmv, con, trans);
                            }
                            #endregion
                            #region Cash voucher
                            if (!(this.txtSalaryOfMonth.Text == "" || this.txtSalaryOfMonth.Text == "0"))
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
                                pv.Cr = Math.Round(Convert.ToDecimal(slm.Salary), 0);
                                pv.DDate = (DateTime)slm.AttDate;
                                pv.OrderNo = 0;
                                pv.SNO = 0;
                                pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                                pv.Description = "Salary Given to " + sm.Name + " of Month " + a.ToString();
                                vDAL.AddVoucher(pv, con, trans);
                                //SaleMan account entry
                                salmv = new Voucher();
                                ChildAccount child = new ChildAccount();
                                child = acDAL.GetChildByCode(sm.AccountCode.ChildCode, con, trans);
                                salmv.AccountCode = child;
                                salmv.Cr = 0;
                                salmv.Dr = Math.Round((decimal)slm.Salary, 0);
                                salmv.DDate = (DateTime)slm.AttDate;
                                salmv.OrderNo = 0;
                                salmv.SNO = 0;
                                salmv.VNO = pv.VNO;
                                salmv.Description = pv.Description;
                                vDAL.AddVoucher(salmv, con, trans);
                            }
                            #endregion
                            slmDAL.AddSalary(slm, con, trans);
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        con.Close();
                        throw ex;
                    }
                    finally
                    {
                        trans.Commit();
                        con.Close();
                        MessageBox.Show("Record saved successfully", Messages.Header);
                        this.RefreshRecordComp();
                    }
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
                    this.dgvPreviousBalance.Rows[i].Cells[0].Value = slms[i].AttDate.ToString("dd-MMM-yy");
                    this.dgvPreviousBalance.Rows[i].Cells[1].Value = Math.Round((decimal)slms[i].AdvAmount, 0).ToString();
                    this.dgvPreviousBalance.Rows[i].Cells[2].Value = slms[i].NoOfInst.ToString();
                    this.dgvPreviousBalance.Rows[i].Cells[3].Value = slms[i].NoOfInstPaid.ToString();
                    this.dgvPreviousBalance.Rows[i].Cells[4].Value = Math.Round((decimal)slms[i].InstallAmount, 0).ToString();
                    this.dgvPreviousBalance.Rows[i].Cells[5].Value = Math.Round((decimal)slms[i].RemainingAmount, 0).ToString();
                }
            }
        }

        private void chkInstallmentPaid_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkInstallmentPaid.Checked == true)
            {
                this.label10.Visible = true;
                this.cbxAdvanceNo.Visible = true;
                this.cbxAdvanceNo.SelectedIndexChanged -= new EventHandler(cbxAdvanceNo_SelectedIndexChanged);
                FormControls.FillCombobox(cbxAdvanceNo, slmDAL.GetAllAdvanceNosById("select AdvanceNo from SaleMan_Advance Where Status = 'Pending' and SmId=" + k), "AdvanceNo", "AdvanceNo");
                txtRemainingAdvance.Text = "";
            }
            else
            {
                this.label10.Visible = false;
                this.cbxAdvanceNo.Visible = false;
                this.pnlIstallment.Visible = false;
                this.txtInstallmentAmount.Text = "";
                this.txtRemainingSalary.Text = "";
                this.txtRemainingAdvance.Text = "";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpDate_Leave(object sender, EventArgs e)
        {
            sm = (SaleMan)this.cbxEmloyeeName.SelectedItem;
            if (sm != null)
            {
                k = (int)sm.ID;
                this.txtSalaryAmount.Text = Math.Round((decimal)sm.Salary, 0).ToString();
                decimal PerDaySal = 0, HalfDaySal = 0;
                int i = 0, l = 0;
                if (IsMonthlySalary == "With Attendance")
                {
                    int numberOfSunday = NumberOfParticularDaysInMonth(DateTime.Now.Year, DateTime.Now.Month, DayOfWeek.Sunday);
                    PerDaySal = (decimal)(sm.Salary) / (DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month) - numberOfSunday);
                    i = slmDAL.GetDaysOfAttendence(this.dtpDate.Value, k);
                    l = slmDAL.GetDaysOfHalfAttendence(this.dtpDate.Value, k);
                    HalfDaySal = PerDaySal / 2;
                    this.txtSalaryOfMonth.Text = Math.Round(((HalfDaySal * l) + (PerDaySal * i)), 0).ToString();
                }
                else
                    this.txtSalaryOfMonth.Text = this.txtSalaryAmount.Text;
            }
        }

        private void cbxAdvanceNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int k = (int)this.cbxAdvanceNo.SelectedValue;
                slm = slmDAL.GetInstallAmount(k);
                this.txtRemainingAdvance.Text = Math.Round((decimal)slm.RemainingAmount, 0).ToString();
                if (slm.NoOfInst == 0)
                    this.txtInstallmentAmount.Text = string.Empty;
                else
                    this.txtInstallmentAmount.Text = Math.Round((decimal)slm.InstallAmount, 0).ToString();
                this.pnlIstallment.Visible = true;
                this.txtInstallmentAmount.Focus();
            }
            catch { }
        }

        private void cbxAdvanceNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxAdvanceNo.SelectedIndexChanged += new EventHandler(cbxAdvanceNo_SelectedIndexChanged);
        }
        public void RefreshRecord()
        {
            this.txtInstallmentAmount.Text = "";
            this.txtRemainingAdvance.Text = "";
            this.txtRemainingSalary.Text = "";
            this.chkInstallmentPaid.Checked = false;
            this.cbxAdvanceNo.Text = "";
        }
        public void RefreshRecordComp()
        {
            this.txtCashInHand.Text = (acDAL.GetCashInHandBalance()).ToString();
            this.dgvPreviousBalance.Rows.Clear();
            this.txtContactNo.Text = "";
            this.txtAddress.Text = "";
            this.txtInstallmentAmount.Text = "";
            this.txtRemainingAdvance.Text = "";
            this.txtRemainingSalary.Text = "";
            this.chkInstallmentPaid.Checked = false;
            this.cbxAdvanceNo.Text = "";
            this.txtSalaryAmount.Text = "";
            this.txtSalaryOfMonth.Text = "";
            this.cbxEmloyeeName.SelectedIndex = -1;
        }

        private void txtInstallmentAmount_TextChanged(object sender, EventArgs e)
        {
            decimal a, b = 0;
            a = FormControls.GetDecimalValue(this.txtSalaryOfMonth, 0);
            if (this.txtInstallmentAmount.Text == "")
            {
                b = 0;
                this.txtRemainingSalary.Text = (a - b).ToString();
            }
            else
            {
                b = Convert.ToDecimal(this.txtInstallmentAmount.Text);
                if (b > slm.RemainingAmount)
                {
                    MessageBox.Show("Amount can't be greater than remaining advance", Messages.Header);
                    this.txtInstallmentAmount.Text = string.Empty;
                    return;
                }
                if (b > a)
                {
                    MessageBox.Show("Amount can't be greater than salary", Messages.Header);
                    this.txtInstallmentAmount.Text = string.Empty;
                    return;
                }
                else
                {
                    this.txtRemainingAdvance.Text = Math.Round(((decimal)slm.RemainingAmount - b), 0).ToString();
                    this.txtRemainingSalary.Text = Math.Round((a - b), 0).ToString();
                }
            }
        }

        private void chkAlownce_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAlownce.Checked == true)
                this.txtAlownce.Visible = true;
            else
            {
                decimal a = 0;
                this.txtAlownce.Visible = false;

                a = FormControls.GetDecimalValue(this.txtAlownce, 0);
                this.txtSalaryOfMonth.Text = Math.Round((j - a), 0).ToString();
                this.txtAlownce.Text = "";
            }
        }

        private void txtAlownce_TextChanged(object sender, EventArgs e)
        {
            decimal b;

            b = FormControls.GetDecimalValue(this.txtAlownce, 0);

            this.txtSalaryOfMonth.Text = Math.Round((j + b), 0).ToString();
        }
    }
}
