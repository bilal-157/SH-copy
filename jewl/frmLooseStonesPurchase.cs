using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlTypes;
using BusinesEntities;
using DAL;
using System.Globalization;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace jewl
{
    public partial class frmLooseStonesPurchase : Form
    {
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        
        SqlTransaction trans;
        PartyDAL paDAL = new PartyDAL();
        LooseStone lsp = new LooseStone();
        List<LooseStone> llsp = new List<LooseStone>();
        AccountDAL acDAL = new AccountDAL();
        AccountDAL adal = new AccountDAL();
        LooseStonesDAL lspDAL = new LooseStonesDAL();
        VouchersDAL vcDAL = new VouchersDAL();
        Voucher vchr = new Voucher();
        StonesDAL sDAL = new StonesDAL();
        Stone stype = new Stone();
        GroupAccount g;
        SubGroupAccount sg;
        ParentAccount p;
        ChildAccount c;
        int k;
        int cc =0 ;
        public int clk = 0;
        public int t;
        public frmLooseStonesPurchase()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void frmLooseStonesPurchase_Load(object sender, EventArgs e)
        {
            this.RefreshRec();

            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }
        private void RefreshRec()
        {
            this.cbxCustomerName.SelectedIndexChanged -= new System.EventHandler(cbxCustomerName_SelectedIndexChanged);
            this.cbxCustomerName.DataSource = paDAL.GetAllParties();
            this.cbxCustomerName.DisplayMember = "PName";
            this.cbxCustomerName.ValueMember = "PCode";
            this.cbxCustomerName.SelectedIndex = -1;
            this.txtRate.Text = "";
            this.txtQty.Text = "";
            this.txtWeight.Text = "";
            this.txtPrice.Text = "";
            this.cbxStonesType.SelectedIndexChanged -= new System.EventHandler(cbxStonesType_SelectedIndexChanged);
            this.cbxStonesType.DataSource = sDAL.GetAllStoneTypeName();
            this.cbxStonesType.DisplayMember = "TypeName";
            this.cbxStonesType.ValueMember = "TypeId";
            this.cbxStonesType.SelectedIndex = -1;

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            if(this.cbxCustomerName.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Party");
                return;
            }
            
           else if (this.cbxStonesType.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Stones Type");
                return;
            }
            else if (this.cbxStonesName.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Stones Name");
                return;
            }
            else
            {
                lsp.party = (Party)this.cbxCustomerName.SelectedItem;
                lsp.Stone = new Stone();
                lsp.Stone.TypeId = (int)this.cbxStonesType.SelectedValue;
                lsp.Stone.Id = (int)this.cbxStonesName.SelectedValue;
                lsp.Stone.Name = (string)this.cbxStonesName.Text;
                lsp.Stone.TypeName = (string)this.cbxStonesType.Text;
                lsp.Qty = Convert.ToInt32(this.txtQty.Text);
                lsp.Weight = Math.Round(Convert.ToDecimal(this.txtWeight.Text), 3);
                lsp.Rate = Math.Round(Convert.ToDecimal(this.txtRate.Text), 1);
                lsp.Price = Math.Round(Convert.ToDecimal(this.txtPrice.Text), 0);
                lsp.date = Convert.ToDateTime(this.dtpDate.Value);
                lsp.AccountCode = adal.CreateAccount(1, "Current Asset", lsp.Stone.Name, "Current Asset", con, trans).ToString();
                trans.Commit();
                con.Close();
                vchr = new Voucher();
                ChildAccount cha = new ChildAccount();
                cha = acDAL.GetChildByCode(lsp.AccountCode);
                vchr.AccountCode = cha;
                vchr.Dr = lsp.Price;
                vchr.Cr = 0;
                vchr.DDate = lsp.date;
                vchr.Description = "Loose Stones Purchase from " + lsp.party.PName;
                vchr.OrderNo = 0;
                vchr.SNO = 0;
                string voucher ="SPV";
                vchr.VNO = vcDAL.CreateVNO(voucher);
                vcDAL.AddVoucher(vchr);
                vchr.Cr = lsp.Price;
                vchr.Dr = 0;
                vchr.AccountCode = acDAL.GetChildByCode(lsp.party.AccountCode);
                vcDAL.AddVoucher(vchr);
                lsp.VNO = vchr.VNO;
                this.lspDAL.AddLooseStonse(lsp);
                decimal stoneweight = Math.Round(sDAL.GetAvailabaleStoneweightbyStoneId(lsp.Stone.Id), 3);
                stoneweight = stoneweight + lsp.Weight;
                sDAL.UpdateStoneweight(lsp.Stone.Id, stoneweight);

                if (this.txtCashPayment.Text != string.Empty)
                {
                    vchr.AccountCode = acDAL.GetChildByCode(lsp.party.AccountCode);
                    vchr.Dr = Math.Round(Convert.ToDecimal(this.txtCashPayment.Text), 0);
                    vchr.Cr = 0;
                    vchr.SNO = 0;
                    vchr.OrderNo = 0;
                    vchr.VNO = vcDAL.CreateVNO("CPV");
                    vchr.DDate = lsp.date;
                    vchr.Description = this.txtDescription.Text;
                    this.vcDAL.AddVoucher(vchr);

                    cha =adal.GetAccount(1, "Current Asset", "Cash In Hand");
                    if (cha == null)
                    {
                        string AccountNO = this.CreatAccount(1, "Current Asset",  "Cash In Hand");
                        cha = adal.GetAccount(1, "Current Asset", "Cash In Hand");
                    }
                    cha.Balance = Math.Round((cha.Balance - Convert.ToDecimal(this.txtCashPayment.Text)), 0);
                    adal.UpdateChildBalance(cha.Balance, cha.ChildCode);
                    vchr.AccountCode = cha;
                    vchr.Cr = Math.Round(Convert.ToDecimal(this.txtCashPayment.Text), 0);
                    vchr.Dr = 0;
                    this.vcDAL.AddVoucher(vchr);
                    adal.UpdateChildBalance(cha.Balance, cha.ChildCode);
                }
                MessageBox.Show("Record Saved Successfully");
                this.txtDescription.Text = "";
                this.dgvDetail.Rows.Clear();
                this.txtRate.Text = string.Empty;
                this.txtQty.Text = string.Empty;
                this.txtWeight.Text = string.Empty;
                this.txtPrice.Text = string.Empty;
                this.cbxStonesType.SelectedIndexChanged -= new System.EventHandler(cbxStonesType_SelectedIndexChanged);
                this.cbxStonesType.SelectionChangeCommitted -=new EventHandler(cbxStonesType_SelectionChangeCommitted);
                this.cbxStonesType.SelectedIndex = -1;
                this.cbxStonesName.SelectedIndex = -1;
                this.cbxCustomerName.SelectedIndex = -1;
            }
        }

        private void txtRate_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtRate.Text == "")
                return;
            else
            {

                if (this.txtWeight.Text == "")
                {
                    this.txtPrice.Text = Math.Round((Convert.ToDecimal(this.txtRate.Text) * 0), 0).ToString();
                    decimal a = Math.Round(Convert.ToDecimal(this.txtPrice.Text), 0);
                }
                else
                    this.txtPrice.Text = Math.Round((Convert.ToDecimal(this.txtWeight.Text) * Convert.ToDecimal(this.txtRate.Text)), 0).ToString();
            }
        }

        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtWeight.Text == string.Empty)
                return;
            else
            {
                if (this.txtRate.Text == string.Empty)
                {
                    this.txtPrice.Text = Math.Round((Convert.ToDecimal(this.txtWeight.Text) * 0), 0).ToString();
                    decimal a = Math.Round(Convert.ToDecimal(this.txtPrice.Text), 0);
                }
                else
                {
                    if (this.txtWeight.Text == ".")
                        this.txtWeight.Text = "0.";
                    this.txtPrice.Text = Math.Round((Convert.ToDecimal(this.txtWeight.Text) * Convert.ToDecimal(this.txtRate.Text)), 0).ToString();
                }
            }
        }

        private void cbxCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Party pt = (Party)this.cbxCustomerName.SelectedItem;
            if (pt != null)
                this.ShowDGV(pt);
        }

        private void cbxCustomerName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxCustomerName.SelectedIndexChanged += new System.EventHandler(cbxCustomerName_SelectedIndexChanged);
        }
        private void ShowDGV(Party pt)
        {
            llsp = this.lspDAL.GetAllStonesbyParty(pt.PCode);
            this.dgvDetail.AutoGenerateColumns = false;
            this.dgvDetail.Rows.Clear();
            if (llsp.Count == 0)
                return;
            else
            {
                int cont = llsp.Count;
                this.dgvDetail.Rows.Add(cont);
                for (int i = 0; i < cont; i++)
                {
                    this.dgvDetail.Rows[i].Cells[0].Value = llsp[i].date.ToString();
                    this.dgvDetail.Rows[i].Cells[1].Value = llsp[i].Stone.Name.ToString();
                    this.dgvDetail.Rows[i].Cells[2].Value = Math.Round(llsp[i].Weight, 3).ToString();
                    this.dgvDetail.Rows[i].Cells[3].Value = Math.Round(llsp[i].Price, 0).ToString();
                    this.dgvDetail.Rows[i].Cells[4].Value = llsp[i].LspId.ToString();
                }
            }
        }
        private void dgvDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (clk == 1)
            {
                cc = Convert.ToInt32(this.dgvDetail.Rows[e.RowIndex].Cells[4].Value);
                this.txtId.Text=cc.ToString();
                this.btnEdit.Enabled = false;
                this.btnSave.Enabled = false;
                this.btnDelete.Text = "Confirm";
            }
            else               
            {
                cc = Convert.ToInt32(this.dgvDetail.Rows[e.RowIndex].Cells[4].Value);
                if (cc != 0)
                {
                    lsp = lspDAL.GetLooseStonesbySlpId(cc);
                    this.btnEdit.Text = "Update";
                    this.cbxStonesType.DataSource = sDAL.GetAllStoneTypeName();
                    this.cbxStonesType.DisplayMember = "TypeName";
                    this.cbxStonesType.ValueMember = "TypeId";
                    this.cbxStonesType.SelectedValue = lsp.Stone.TypeId;
                    this.cbxStonesName.DataSource = sDAL.GetAllStoneNamebyId(lsp.Stone.TypeId);
                    this.cbxStonesName.DisplayMember = "Name";
                    this.cbxStonesName.ValueMember = "Id";
                    this.cbxStonesName.SelectedValue = lsp.Stone.Id;
                    this.txtStoneId.Text = lsp.Stone.Id.ToString();
                    this.txtStoneWeight.Text = Math.Round(lsp.Weight, 3).ToString();
                    this.txtQty.Text = lsp.Qty.ToString();
                    this.txtWeight.Text = Math.Round(lsp.Weight, 3).ToString();
                    this.txtRate.Text = Math.Round(lsp.Rate, 1).ToString();
                    this.txtPrice.Text = Math.Round(lsp.Price, 0).ToString();
                    this.txtId.Text = lsp.LspId.ToString();
                    this.dtpDate.Value = lsp.date;
                    this.txtVNO.Text = lsp.VNO.ToString();
                    this.btnSave.Enabled = false;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {         
            if (this.btnEdit.Text == "Update")
            {
                lsp.party = (Party)this.cbxCustomerName.SelectedItem;
                if (this.cbxStonesType.Text == "")
                {
                    MessageBox.Show("Please Select Stones Type");
                    return;
                }
                else
                {
                    lsp.Stone = new Stone();
                    lsp.Stone.TypeId = (int)this.cbxStonesType.SelectedValue;
                    lsp.Stone.Id = (int)this.cbxStonesType.SelectedValue;
                    lsp.Stone.Name = (string)this.cbxStonesName.Text;
                    lsp.Stone.TypeName = (string)this.cbxStonesType.Text;
                    lsp.Qty = Convert.ToInt32(this.txtQty.Text);
                    lsp.Weight = Math.Round(Convert.ToDecimal(this.txtWeight.Text), 3);
                    lsp.Rate = Math.Round(Convert.ToDecimal(this.txtRate.Text), 1);
                    lsp.Price = Math.Round(Convert.ToDecimal(this.txtPrice.Text), 0);
                    lsp.date = Convert.ToDateTime(this.dtpDate.Value);
                    t = Convert.ToInt32(this.txtId.Text);
                    this.lspDAL.UpdateLooseStones(t, lsp);
                    lsp.AccountCode = this.CreatAccount(1, "Current Asset", lsp.Stone.Name);
                    vchr = new Voucher();
                    ChildAccount cha = new ChildAccount();
                    cha = acDAL.GetChildByCode(lsp.AccountCode);
                    vchr.AccountCode = cha;
                    vchr.Dr = lsp.Price;
                    vchr.Cr = 0;
                    vchr.DDate = lsp.date;
                    vchr.Description = "Loose Stones Purchase from " + lsp.party.PName;
                    vchr.OrderNo = 0;
                    vchr.SNO = 0;
                    if (this.txtVNO.Text != "")
                    {
                        vchr.VNO = this.txtVNO.Text;
                        vcDAL.DeleteVoucher(vchr.VNO);
                    }
                    else
                    {
                        string voucher = "SPV";
                        vchr.VNO = vcDAL.CreateVNO(voucher);
                    }
                    vcDAL.AddVoucher(vchr);
                    vchr.Cr = lsp.Price;
                    vchr.Dr = 0;
                    vchr.AccountCode = acDAL.GetChildByCode(lsp.party.AccountCode);
                    vcDAL.AddVoucher(vchr);
                    if (Convert.ToInt32(this.txtStoneId.Text) == lsp.Stone.Id)
                    {
                        decimal stoneweight = sDAL.GetAvailabaleStoneweightbyStoneId(lsp.Stone.Id);
                        stoneweight = Math.Round((stoneweight + lsp.Weight - Convert.ToDecimal(this.txtStoneWeight.Text)), 3);
                        sDAL.UpdateStoneweight(lsp.Stone.Id, stoneweight);
                        MessageBox.Show("Record Updated");
                    }
                    else if (Convert.ToInt32(this.txtStoneId.Text) != lsp.Stone.Id)
                    {
                        decimal stoneweight = sDAL.GetAvailabaleStoneweightbyStoneId(lsp.Stone.Id);
                        stoneweight = Math.Round((stoneweight + lsp.Weight), 3);
                        sDAL.UpdateStoneweight(lsp.Stone.Id, stoneweight);
                        stoneweight = sDAL.GetAvailabaleStoneweightbyStoneId(Convert.ToInt32(this.txtStoneId.Text));
                        stoneweight = Math.Round((stoneweight - Convert.ToDecimal(this.txtStoneWeight.Text)), 3);
                        sDAL.UpdateStoneweight(Convert.ToInt32(this.txtStoneId.Text), stoneweight);
                        MessageBox.Show("Record Updated");
                    }
                    
                    this.btnDelete.Enabled = true;
                    this.btnSave.Enabled = true;
                    this.btnEdit.Text = "&Edit";
                    this.dgvDetail.AutoGenerateColumns = false;
                    this.dgvDetail.Rows.Clear();
                    this.RefreshRec();
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.btnDelete.Text == "&Delete")
                clk = 1;
            else
            {
                if (this.btnDelete.Text == "Confirm")
                {
                    if (MessageBox.Show("Are you sure you want to delete this record click Yes to confirm Or click No ", Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(this.txtId.Text);
                        this.lspDAL.DeleteLooseStones(id);
                        MessageBox.Show("Record Deleted Successfully");
                        this.btnDelete.Text = "&Delete";
                        this.btnSave.Text = "&Save";
                        this.btnEdit.Text = "&Edit";
                        this.dgvDetail.Rows.Clear();
                        this.RefreshRec();
                        this.cbxStonesName.SelectedIndex = -1;
                        this.dtpDate.Text = "";
                    }
                    else
                        return;
                }
            }
        }
        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (this.txtWeight.Text == string.Empty)
                this.txtPrice.Text = string.Empty;
        }

        private void cbxStonesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sty = (int)this.cbxStonesType.SelectedValue;
            this.cbxStonesName.DataSource = sDAL.GetAllStoneNamebyId(sty);
            this.cbxStonesName.DisplayMember = "Name";
            this.cbxStonesName.ValueMember = "Id";
            this.cbxStonesName.SelectedIndex = -1;
        }

        private void cbxStonesType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxStonesType.SelectedIndexChanged += new System.EventHandler(cbxStonesType_SelectedIndexChanged);
        }
        private string CreatAccount(int hcode,  string pname, string chname)
        {
            AccountDAL adal = new AccountDAL();
            p = adal.GetParent(pname, sg.SubGroupCode);
            if (p == null)
            {
                p = new ParentAccount();
                p.HeadCode = sg.HeadCode;
              
                p.ParentName = pname;
                p.ParentCode = adal.CreateParentCode( p.HeadCode);
                adal.CreateParentAccount(p);
            }
            c = adal.GetChild(chname, p.ParentCode);
            if (c == null)
            {
                c = new ChildAccount();
                c.HeadCode = p.HeadCode;         
                c.ParentCode = p.ParentCode;
                c.ChildName = chname;
                c.DDate = DateTime.Today;
                c.Status = "Dr";
                c.AccountType = "Stones Purchase";
                c.Description = "";
                c.ChildCode = adal.CreateChildCode(c.ParentCode, c.HeadCode);
                adal.CreateChildAccount(c,true);
            }
            return c.ChildCode;
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void btnAddParty_Click(object sender, EventArgs e)
        {
            frmPartyInfo frm = new frmPartyInfo();
            frm.ShowDialog();
        }

        private void cbxCustomerName_Enter(object sender, EventArgs e)
        {
            this.cbxCustomerName.DataSource = paDAL.GetAllParties();
            this.cbxCustomerName.DisplayMember = "PName";
            this.cbxCustomerName.ValueMember = "PCode";
            this.cbxCustomerName.SelectedIndex = -1;
        }

        private void frmLooseStonesPurchase_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }
    }
}
