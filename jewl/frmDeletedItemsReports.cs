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
    public partial class frmDeletedItemsReports : Form
    {
        ItemDAL itmDAL = new ItemDAL();

        public frmDeletedItemsReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this); FormControls.GetAllControls(this); this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string selectQuery = "";

            if (rbtComplet.Checked == true)
            {
                frmDeletedStock cmp = new frmDeletedStock();
                cmp.ShowDialog();
            }
            if (rbtItemWise.Checked == true && cbxGroupItem.SelectedIndex != -1)
            {
                frmDelItemItmWise frmitm = new frmDelItemItmWise();
                //string selectQuery = "";

                Item itm = (Item)this.cbxGroupItem.SelectedItem;
                selectQuery = "{StockRpt.ItemId}=" + itm.ItemId;

                frmitm.selectQuery = selectQuery;
                frmitm.ShowDialog();
            }
            if (this.rbtDateWise.Checked == true)
            {
                if (this.chkDateRange.Checked == true)
                {
                    frmDeletedStock frm = new frmDeletedStock();
                    //string selectQuery = "";
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        //DateTime dt = dtpFrom.Value;

                        selectQuery = "{StockRpt.DelDate}>=Date('" + dtpFromDate.Value.ToShortDateString() + "')";
                    }

                    //if (string.IsNullOrEmpty(selectQuery))
                    selectQuery = selectQuery + "and {StockRpt.DelDate}<=Date('" + dtpToDate.Value.ToShortDateString() + "')";

                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();

                }
                else
                {
                    frmDeletedStock frm = new frmDeletedStock();
                    //string selectQuery = "";
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        selectQuery = "{StockRpt.DelDate }=Date('" + dtpDate.Value.ToShortDateString() + "')";
                    }
                    frm.selectQuery = selectQuery;
                    frm.ShowDialog();
                }
            }
        }

        private void frmDeletedItem_Load(object sender, EventArgs e)
        {
            this.panel5.Visible = false;
            this.panel2.Visible = false;
            this.panel3.Visible = false;


            this.cbxGroupItem.DataSource = itmDAL.GetAllItems();
            this.cbxGroupItem.DisplayMember = "ItemName";
            this.cbxGroupItem.ValueMember = "ItemId";

            this.cbxGroupItem.SelectedIndex = -1;
        }

        private void rbtItemWise_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtItemWise.Checked == true)
            {
                this.panel5.Visible = true;
            }
            else
            {
                this.panel5.Visible = false;
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
                this.panel4.Visible = false;
            }
        }

        private void chkDateRange_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDateRange.Checked == true)
            {
                this.panel3.Visible = true;
                this.panel4.Visible = false;
            }
            else
            {
                this.panel3.Visible = false;
                this.panel4.Visible = true;
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDeletedItemsReports_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

       
      
    }
}
