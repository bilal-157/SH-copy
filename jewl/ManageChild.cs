using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BusinesEntities;
using jewl;

namespace jewl
{
    public partial class ManageChild : Form
    {
        public bool deleteChk;
        public string Accountcode;
        public string ChildName;
        ChildAccount cha = new ChildAccount();
        ChildAccount ucha = new ChildAccount();
        AccountDAL aDAL = new AccountDAL();
        string ccode = "";
        public ManageChild()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void ManageChild_Load(object sender, EventArgs e)
        {
            UserRights ur = new UserRights();
            string str;
            str = ur.GetRightsByUser();
            if (str == "Administrator")
            {
                this.btnSave.Enabled = true;
            }
            else
            {
                this.btnSave.Enabled = true;
            }
            cha = aDAL.GetChildByCode(Accountcode);
            this.txtAccountCode.Text = cha.ChildCode;
            this.txtAccountName.Text = cha.ChildName;
            this.txtDescription.Text = cha.Description;

            if (cha.OpCash < 0)
            {
                this.rbtCr.Checked = true;
                this.txtOpeningCash.Text = (-cha.OpCash).ToString();
            }
            else
            {
                this.rbtDr.Checked = true;
                this.txtOpeningCash.Text = cha.OpCash.ToString();
            }
            if (cha.OpGold < 0)
            {
                this.rbtGoldCr.Checked = true;
                this.txtOpeningGold.Text = (-cha.OpGold).ToString();
            }
            else
            {
                this.rbtGoldDr.Checked = true;
                this.txtOpeningGold.Text = cha.OpGold.ToString();
            }
            ccode = this.txtAccountCode.Text;
            FormControls.FillCombobox(cbxAccountType, aDAL.AllAccountsType(), "TypeName", "TypeName");
            this.cbxAccountType.SelectedValue = cha.AccountType;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CultureInfo culInfo = CultureInfo.CurrentCulture;
            TextInfo txtInfo = culInfo.TextInfo;
            string str;
            if (this.rbtDeleteAccount.Checked == true)
            {

                if (!deleteChk)
                {
                    MessageBox.Show("Account can not be deleted From Here Because of its Dependency, Please Account deleted From where It is Created", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    bool bFlag = DateDAL.IsExist("select VNO from Vouchers where AccountCode ='" + this.ccode + "'");
                    if (bFlag == false)
                    {
                        aDAL.DeleteAccount("delete from ChildAccount where ChildCode='" + ccode + "'");
                        MessageBox.Show("Child Deleted Successfully", Messages.Header);
                    }
                    else
                        MessageBox.Show("Child Not Deleted Due To Transaction", Messages.Header);

                }
            }
            else if (this.rbtEditAccount.Checked == true)
            {
                ucha.Balance = FormControls.GetDecimalValue(this.txtOpeningCash, 0);
                ucha.GoldBalance = FormControls.GetDecimalValue(this.txtOpeningGold, 3);
                str = this.txtAccountName.Text;
                ucha.ChildName = txtInfo.ToTitleCase(str);
                ucha.ChildCode = ccode;
                ucha.ParentCode = cha.ParentCode;
                ucha.HeadCode = cha.HeadCode;
                ucha.DDate = this.dtpDate.Value;
                ucha.AccountType = this.cbxAccountType.Text;

                if (this.rbtCr.Checked == true)
                {
                    ucha.Status = "Cr";
                    ucha.OpCash = -ucha.Balance;
                }
                else if (this.rbtDr.Checked == true)
                {
                    ucha.Status = "Dr";
                    ucha.OpCash = ucha.Balance;
                }
                else
                    ucha.Status = "";
                if (this.rbtGoldCr.Checked == true)
                {
                    ucha.OpGold = -FormControls.GetDecimalValue(this.txtOpeningGold, 3);
                }
                else if (this.rbtGoldDr.Checked == true)
                {
                    ucha.OpGold = FormControls.GetDecimalValue(this.txtOpeningGold, 3);
                }

                aDAL.UpdateChild(ucha.ChildCode, ucha);

                MessageBox.Show(Messages.Updated, Messages.Header);
            }
            ((ChartOfAccount)this.Owner).Reload(); this.Close();
        }

        private void ManageChildAccount_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }
    }
}

