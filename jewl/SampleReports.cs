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
    public partial class SampleReports : Form
    {
        ItemDAL itmDAL = new ItemDAL();
        public SampleReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
        }

        private void frmSampleReports_Load(object sender, EventArgs e)
        {
            this.pnlSampleDateWise.Visible = false;
            this.pnlSampleItmWise.Visible = false;
            this.rbtCompleteSample.Checked = true;
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (this.rbtSampleReturn.Checked == true)
            {
                frmSampleReturn frmsr = new frmSampleReturn();
                frmsr.ShowDialog();
            }
            else
            {
                SampleByItmRpt sitm = new SampleByItmRpt();
                string selectQuery = "";
                if (this.rbtSampleItmWise.Checked == true)
                {
                    Item itm = (Item)this.cbxGroupItem.SelectedItem;
                    selectQuery = "{SampleReport.ItemId}=" + itm.ItemId;
                }
                if (this.rbtDateWise.Checked == true)
                {
                    if (string.IsNullOrEmpty(selectQuery))
                    {
                        DateTime dt = dtpFrom.Value;

                        // selectQuery = "{StockRpt.StockDate}>=" + dtpFrom.Value + "";
                        selectQuery = "{SampleReport.SampleDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";
                    }
                    //else
                    //{
                    //    selectQuery = selectQuery + "and {SampleReport.SampleDate}>=Date('" + dtpFrom.Value.ToShortDateString() + "')";

                    //}
                    if (string.IsNullOrEmpty(selectQuery))
                        selectQuery = "{SampleReport.SampleDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";
                    //else
                    //    selectQuery = selectQuery + "and {SampleReport.SampleDate}<=Date('" + dtpTo.Value.ToShortDateString() + "')";

                }
                if (this.rbtCompleteSample.Checked == true)
                {
                    selectQuery = "{SampleReport.SampleDate}<=Date('" + DateTime.Today.ToShortDateString() + "')";
                }
                sitm.isComplete = true;
                sitm.selectQuery = selectQuery;
                sitm.ShowDialog();
            }
        }

        private void rbtSampleItmWise_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSampleItmWise.Checked == true)
                this.pnlSampleItmWise.Visible = true;
            else if (this.rbtSampleItmWise.Checked == false)
                this.pnlSampleItmWise.Visible = false;
        }

        private void rbtDateWise_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtDateWise.Checked == true)
                this.pnlSampleDateWise.Visible = true;
            else if (this.rbtDateWise.Checked == false)
                this.pnlSampleDateWise.Visible = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtCompleteSample_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtCompleteSample.Checked == true)
            {
                this.pnlSampleItmWise.Visible = false;
                this.pnlSampleDateWise.Visible = false;
            }
        }

        private void rbtSampleReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSampleReturn.Checked == true)
            {
                this.pnlSampleItmWise.Visible = false;
                this.pnlSampleDateWise.Visible = false;
            }
        }
    }
}
