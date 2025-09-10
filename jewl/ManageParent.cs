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
    public partial class ManageParent : Form
    {
        public string pcode;
        public string ParentName;
        public bool deleteChk;
        public int headcode;
        AccountDAL aDAL = new AccountDAL();
        ParentAccount pa = new ParentAccount();
        ChildAccount ca = new ChildAccount();
        VouchersDAL vDAL = new VouchersDAL();
        Voucher vchr = new Voucher();
        bool bFlag;
        public ManageParent()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void EditParentAccount_Load(object sender, EventArgs e)
        {
            UserRights ur = new UserRights();
            string str;
            str = ur.GetRightsByUser();
            this.btnSave.Enabled = true;
            if (str == "Administrator")
            {
                this.rbtEditParent.Enabled = true;
                this.rbtDeleteParent.Enabled = true;
            }
            else
            {
                this.rbtEditParent.Enabled = false;
                this.rbtDeleteParent.Enabled = false;
            }
            this.RefrshChild();
        }
        private void RefrshChild()
        {
            if (pcode == string.Empty || pcode.Contains("Create"))
            {
                this.rbtCreateParent.Checked = true;
                //this.panel3.Visible = false;
                //this.txtParentCode.Text = aDAL.CreatParentCode(headcode);
            }
            else
            {
                this.txtParentName.Text = ParentName;
                this.txtParentCode.Text = pcode;
                this.panel3.Visible = false;
            }
        }

        private void rbtCreateChild_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtCreateChild.Checked == true)
            {
                this.panel3.Visible = true;
                this.txtAccountCode.Text = aDAL.CreateChildCode(pcode,Convert .ToInt32(pcode.Remove (1,4)));
            }
            if (this.rbtCreateChild.Checked == false)
            {
                this.panel3.Visible = false;
                //this.txtAccountCode.Text = aDAL.CreatChildCode(pcode, headcode);
            }
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
            if (this.rbtCreateParent.Checked == true)
            {
                if (this.txtParentName.Text == "")
                {
                    MessageBox.Show("Please Parent Name", Messages.Header);
                    return;
                }
                else
                {
                    pa.ParentCode = this.txtParentCode.Text;
                    str = this.txtParentName.Text;
                    pa.ParentName = txtInfo.ToTitleCase(str);
                    pa.HeadCode = headcode;
                    pa.SubGroupCode = "";// sgcode;
                    //string  sd = sgcode.Substring(0,4);
                    pa.GroupCode = "";// sd;
                    pa.DeleteCheck = true;
                    bFlag = aDAL.CreateParentAccount(pa);
                    if (bFlag == true)
                        MessageBox.Show(Messages.Saved, Messages.Header);
                    else
                        MessageBox.Show("Parent Account Already Exists", Messages.Header);
                    this.txtParentCode.Text = string.Empty;
                    this.txtParentName.Text = string.Empty;

                }
            }
            else if (this.rbtDeleteParent.Checked == true)
            {
                if (!deleteChk)
                {
                    MessageBox.Show("Account can not be deleted From Here Because of its Dependency, Please Account deleted From where It is Created", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                  
                }
                else {
                    List<ChildAccount> lca = new List<ChildAccount>();
                    lca = aDAL.GetAllChildAccounts("select ca.*, ((select isnull(sum(OpeningCash), 0) from ChildAccount where ChildCode = ca.ChildCode) - (select isnull(sum(dr) - sum(cr), 0) from vouchers where AccountCode = ca.ChildCode)) as Balance from ChildAccount ca  where ca.ParentCode='" + this.pcode + "' order by ChildName");
                    if (lca == null)
                    {
                        aDAL.DeleteAccount("delete from ParentAccount where ParentCode='" + this.pcode + "'");
                        MessageBox.Show("Record Deleted Successfully", Messages.Header);
                    }
                    else
                        MessageBox.Show("Parent Not Deleted Successfully Due To Child", Messages.Header);
                    this.txtParentCode.Text = string.Empty;
                    this.txtParentName.Text = string.Empty;
                }
                
            }
          else  if (this.rbtCreateChild.Checked == true)
            {
                if (this.txtAccountName.Text == "")
                {
                    MessageBox.Show("Please Enter Account Name", Messages.Header);
                    return;
                }
                str = this.txtParentName.Text;
                ca.AccountType = this.cbxAccountType.Text;
                ca.ChildCode = this.txtAccountCode.Text;
                str = this.txtAccountName.Text;
                ca.ChildName = txtInfo.ToTitleCase(str);
                ca.DDate = this.dtpDate.Value;
                ca.Description = this.txtDescription.Text;
                ca.HeadCode = this.headcode;
                ca.ParentCode = this.txtParentCode.Text;
                ca.DeleteCheck = true;
                if (this.rbtCr.Checked == true)
                {
                    ca.Status = "Cr";
                    //vchr.Cr = ca.Balance;
                    ca.OpCash = -Convert.ToDecimal(this.txtOpeningBalance.Text);
                    //vchr.Dr = 0;
                }
                else if (this.rbtDr.Checked == true)
                {
                    ca.Status = "Dr";
                    //vchr.Dr = ca.Balance;
                    ca.OpCash = Convert.ToDecimal(this.txtOpeningBalance.Text);
                    //vchr.Cr = 0;
                }
                else
                    ca.Status = "";
                if (this.rbtOpCredit.Checked)
                {
                    ca.OpGold = -Convert.ToDecimal(this.txtOpGold.Text);
                }
                else if (this.rbtOpDebit.Checked)
                {
                    ca.OpGold = Convert.ToDecimal(this.txtOpGold.Text);
                }

                bFlag = aDAL.CreateChildAccount(ca, true);
                if (bFlag == true)
                {
                    MessageBox.Show(Messages.Saved, Messages.Header);  
                }
                if(bFlag==false)
                    MessageBox.Show("Child Account Already Exists", Messages.Header);
                this.txtAccountCode.Text = string.Empty;
                this.txtAccountName.Text = string.Empty;
                this.txtDescription.Text = string.Empty;
                this.txtOpeningBalance.Text = string.Empty;
            
                
            }
           else if (this.rbtEditParent.Checked == true)
            {
                if (this.txtParentName.Text == "")
                {
                    MessageBox.Show("Plese Enter Parent Name", Messages.Header);
                    return;
                }
                str = this.txtParentName.Text;
                this.aDAL.UpdateParent(this.txtParentCode.Text, txtInfo.ToTitleCase(str));
                MessageBox.Show(Messages.Updated, Messages.Header);
                this.RefrshChild(); 
            }
            ((ChartOfAccount)this.Owner).Reload(); this.Close();
        }

        private void rbtEditParent_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtEditParent.Checked == true)
            {
                this.RefrshChild();
            }
        }

        private void rbtDeleteParent_CheckedChanged(object sender, EventArgs e)
        {
            this.RefrshChild();
        }

        private void rbtCreateParent_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtCreateParent.Checked == true)
            {
                this.panel3.Visible = false;
                this.txtParentCode.Text = aDAL.CreateParentCode(headcode);
                this.txtParentName.Text = "";
            }
        }

        private void ManageParentAccount_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

       
    }
}
