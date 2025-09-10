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
    public partial class frmStonesRpt : Form
    {
        PartyDAL paDAL = new PartyDAL();
        LooseStone lsp = new LooseStone();
        List<LooseStone> llsp = new List<LooseStone>();
        LooseStonesDAL lspDAL = new LooseStonesDAL();
        StonesDAL stDAL = new StonesDAL();
        StonesDAL sDAL = new StonesDAL();
        string SelectQuery = "";
        public frmStonesRpt()
        {
            InitializeComponent();
            FormControls.GetAllControls(this); this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmStonesRpt_Load(object sender, EventArgs e)
        {
            this.cbxParty.DataSource = paDAL.GetAllParties();
            this.cbxParty.DisplayMember = "PName";
            this.cbxParty.ValueMember = "PCode";
            this.cbxParty.SelectedIndex = -1;
            this.panel1.Visible = false;
            this.cbxStonesType.SelectedIndexChanged -= new System.EventHandler(cbxStonesType_SelectedIndexChanged);
            this.cbxStonesType.DataSource = stDAL.GetAllStoneTypeName();
            this.cbxStonesType.DisplayMember = "TypeName";
            this.cbxStonesType.ValueMember = "TypeId";
            this.cbxStonesType.SelectedIndex = -1;
            this.ChkStoneName.Enabled = false;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.ChkParty.Checked == true)
            {
                if (this.cbxParty.SelectedIndex != -1)
                {
                    if (this.chkDateFrom.Checked == false)
                    {

                        Party pt = (Party)this.cbxParty.SelectedItem;
                        SelectQuery = "{LooseStones.PName}=('" + pt.PName + "')";
                        //frmLooseStonesRpt frm = new frmLooseStonesRpt();
                        //frm.selectquery = this.SelectQuery;
                        //frm.ShowDialog();

                    }

                }
            }
            if (this.ChkStonesType.Checked == true)
            {
                if (this.ChkParty.Checked == false)
                    SelectQuery = "{LooseStones.StonesType}=('" + this.cbxStonesType.Text + "')";
                else
                    SelectQuery = SelectQuery + " and {LooseStones.StonesType}=('" + this.cbxStonesType.Text + "')";
                //frmLooseStonesRpt frm = new frmLooseStonesRpt();
                //frm.selectquery = this.SelectQuery;
                //frm.ShowDialog();
            }
            if (this.ChkStoneName.Checked == true)
            {
                SelectQuery = SelectQuery + " and {LooseStones.StonesName}=('" + this.cbxStonesName.Text + "')";
                //frmLooseStonesRpt frm = new frmLooseStonesRpt();
                //frm.selectquery = this.SelectQuery;
                //frm.ShowDialog();
            }


           if (this.ChkDate.Checked == true)
            {


                SelectQuery = SelectQuery +  "and {LooseStones.SDate}>=Date('" + dtpDateFrom.Value.ToShortDateString() + "')";
                SelectQuery = SelectQuery + "and {LooseStones.SDate}<=Date('" + dtpDateTo.Value.ToShortDateString() + "')";
                //frmLooseStonesRpt frm = new frmLooseStonesRpt();
                //frm.selectquery = this.SelectQuery;
                //frm.ShowDialog();

            }
          
           if (this.ChkAll.Checked == true)
            {
                SelectQuery = "";
                //frmLooseStonesRpt frm = new frmLooseStonesRpt();
                //frm.selectquery = this.SelectQuery;
                //frm.ShowDialog();

            }
           if (this.ChkStkSumary.Checked == false)
           {
               frmLooseStonesRpt frm = new frmLooseStonesRpt();
               frm.selectquery = this.SelectQuery;
               frm.ShowDialog();
           }
           if (this.ChkStkSumary.Checked == true)
           {
               StonesSummaryRpt stk = new StonesSummaryRpt();
               stk.selectquery = this.SelectQuery;
               stk.ShowDialog();
           }
        }
        private void chkDateFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDateFrom.Checked == true)
            {
                this.chkDateTo.Enabled = true;
                this.dtpDateFrom.Enabled = true;
            }
            if (this.chkDateFrom.Checked == false)
            {
                this.chkDateTo.Enabled = false;
                this.dtpDateFrom.Enabled = false;
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
        private void ChkParty_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChkParty.Checked == true)
            {
                this.cbxParty.Enabled = true;
                this.cbxParty.DataSource = paDAL.GetAllParties();
                this.cbxParty.DisplayMember = "PName";
                this.cbxParty.ValueMember = "PCode";
                this.cbxParty.SelectedIndex = -1;
                //this.chkDateFrom.Enabled = true;
            }
            if (this.ChkParty.Checked == false)
            {
                this.cbxParty.Enabled = false;
                //this.chkDateFrom.Enabled = false;
            }







        }
        private void ChkStonesType_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChkStonesType.Checked == true)
            {
                this.cbxStonesType.Enabled = true;
                this.ChkStoneName.Enabled = true;
                this.chkDateFrom.Enabled = false;
                this.chkDateTo.Enabled = false;

            }
            if (this.ChkStonesType.Checked == false)
            {
                this.cbxStonesType.Enabled = false;
                this.chkDateFrom.Enabled = true;
                this.ChkStoneName.Enabled = false;
            }
        }
        private void ChkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChkDate.Checked == true)
            {
                this.panel1.Visible = true;
                this.ChkAll.Checked = false;
            }
            if (this.ChkDate.Checked == false)
            {
                this.panel1.Visible = false;
            }
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
        private void ChkStoneName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChkStoneName.Checked == true)
            {
                this.cbxStonesName.Enabled = true;
            }
            if (this.ChkStoneName.Checked == false)
            {
                this.cbxStonesName.Enabled = false;
            }
        }

        private void ChkStkSumary_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChkStkSumary.Checked == true)
            {
                this.ChkParty.Checked = false;
                this.ChkAll.Checked = false;
                this.ChkDate.Checked = false;
            }
            if (this.ChkStkSumary.Checked == false)
            {
                this.ChkParty.Checked = true;
            }
        }

        private void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ChkAll.Checked == true)
            {
                this.ChkDate.Checked = false;
                this.ChkParty.Checked = false;
                this.ChkStkSumary.Checked = false;
                this.ChkStonesType.Checked = false;
                this.ChkStoneName.Checked = false;
                //this.Checked = false;
                //this.ChkDate.Checked = false;
                //this.ChkDate.Checked = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void frmStonesRpt_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

    }
}
