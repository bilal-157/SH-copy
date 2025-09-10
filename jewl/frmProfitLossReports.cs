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
    public partial class frmProfitLossReports : Form
    {
        ItemDAL itmDAL = new ItemDAL();
        public frmProfitLossReports()
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

        private void rbtSaleNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSaleNo.Checked == true)
            {
                this.label3.Visible = true;
                this.txtSaleNo.Visible = true;
            }
            else
            {
                this.label3.Visible = false;
                this.txtSaleNo.Visible = false;
            }
        }

        private void rbtItemWise_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtItemWise.Checked == true)
            {
                this.label2.Visible = true;
                this.cbxGroupItem.Visible = true;
            }
            else
            {
                this.label2.Visible = false;
                this.cbxGroupItem.Visible = true;
            }
        }

        private void rbtDateWise_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtDateWise.Checked == true)
                this.pnlDateWise.Visible = true;
            else
                this.pnlDateWise.Visible = false;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string selectQuery = "";
            if (this.rbtSaleNo.Checked == true)
            {
                selectQuery = "";
                frmProfitLoss frm = new frmProfitLoss();
                selectQuery = "{ProfitLoss.SaleNo}=" + Convert.ToInt32(this.txtSaleNo.Text);
                frm.selectQuery = selectQuery;
                frm.ShowDialog();
            }
            if (this.rbtItemWise.Checked == true)
            {
                selectQuery = "";
                frmProfitLoss frm = new frmProfitLoss();
                Item itm = (Item)this.cbxGroupItem.SelectedItem;
                selectQuery = "{ProfitLoss.ItemId}=" + itm.ItemId;
                frm.selectQuery = selectQuery;
                frm.ShowDialog();
            }
            if (this.rbtDateWise.Checked == true)
            {
                selectQuery = "";
                frmProfitLoss frm = new frmProfitLoss();               
                if (string.IsNullOrEmpty(selectQuery))
                    selectQuery = "{ProfitLoss.SDate}>=Date('" + dtpFromDate.Value.ToShortDateString() + "')";
                selectQuery = selectQuery + "and {ProfitLoss.SDate}<=Date('" + dtpToDate.Value.ToShortDateString() + "')";
                frm.selectQuery = selectQuery;
                frm.ShowDialog();
            }
        }

        private void frmProfitLossReports_Load(object sender, EventArgs e)
        {
            this.cbxGroupItem.DataSource = itmDAL.GetAllItems();
            this.cbxGroupItem.DisplayMember = "ItemName";
            this.cbxGroupItem.ValueMember = "ItemId";
            this.cbxGroupItem.SelectedIndex = -1;
        }

        private void frmProfitLossReports_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }
    }
}
