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
    public partial class SplitSaleReports : Form
    {
        ItemDAL itmDAL = new ItemDAL();
        public SplitSaleReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void SplitSaleReports_Load(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.panel3.Visible = false;
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
        }

        private void rbtItemWise_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtItemWise.Checked == true)
            {
                this.panel1.Visible = true;
            }
            else
            {
                this.panel1.Visible = false;
            }
        }

        private void rbtDateWise_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtDateWise.Checked == true)
            {
                this.panel2.Visible = true;
                this.panel4.Visible = true;
            }
            else
            {
                this.panel2.Visible = false;
                this.chkDateRange.Checked = false;
                this.panel3.Visible = false;
                this.panel4.Visible = false;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ReportViewer frm = new ReportViewer();
            string selectQuery = "";
            if (rbtComplet.Checked == true)
            {
                frm.isPage = 4;
                frm.rpt = 1;
                frm.ShowDialog();
            }
            if (rbtItemWise.Checked == true && cbxGroupItem.SelectedIndex != -1)
            {
                frm = new ReportViewer();
                Item itm = (Item)this.cbxGroupItem.SelectedItem;
                selectQuery = "{SplitSaleRpt.ItemId}=" + itm.ItemId;
                frm.isPage = 4;
                frm.rpt = 1;
                frm.selectQuery = selectQuery;
                frm.ShowDialog();
            }
            if (this.rbtDateWise.Checked == true)
            {
                if (this.chkDateRange.Checked == true)
                {
                    frm = new ReportViewer();
                    selectQuery = "";
                    if (string.IsNullOrEmpty(selectQuery))
                    {

                        selectQuery = "{SplitSaleRpt.SaleDate}>=Date('" + dtpFromDate.Value.ToShortDateString() + "')";
                    }
                    else
                    {
                        selectQuery = selectQuery + "and {SplitSaleRpt.SaleDate}>=Date('" + dtpFromDate.Value.ToShortDateString() + "')";
                    }
                   // else
                    selectQuery = selectQuery + "and {SplitSaleRpt.SaleDate}<=Date('" + dtpToDate.Value.ToShortDateString() + "')";
                    frm.isPage = 4;
                    frm.rpt = 1;
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
                else
                {
                    frm = new ReportViewer();
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{SplitSaleRpt.SaleDate}=Date('" + dtpDate.Value.ToShortDateString() + "')";
                    }
                    frm.isPage = 4;
                    frm.rpt = 1;
                    frm.selectQuery = selectQuery;
                    frm.Show();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkDateRange_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDateRange.Checked == true)
               this. panel3.Visible = true;
            else if (this.chkDateRange.Checked == false )
               this. panel3.Visible = false;
        }

        private void frmSplitSaleReports_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }
    }
}
