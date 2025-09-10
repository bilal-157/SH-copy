using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.IO;
using DAL;

namespace jewl
{
    public partial class frmTagHistory : Form
    {
        ItemDAL itmDAL = new ItemDAL();
        StockDAL sDAL = new StockDAL();

        public frmTagHistory()
        {
            InitializeComponent();
            FormControls.GetAllControls(this); FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //ReportDocument doc = new ReportDocument();
            //string path = Path.GetDirectoryName(Application.ExecutablePath);
            //string reportPath = path + "\\Reports\\TagHistory.rpt";
            //doc.Load(reportPath);
            //doc.SetParameterValue("@TagNo", this.cbxTagNo.Text);
            //doc.SetParameterValue("user", DAL.login);
            //doc.SetDatabaseLogon("sa", "123");

            frmTagHistoryRpt frm = new frmTagHistoryRpt();
            string selectQuery = "{TagHistoryRpt.tagno}='" + this.cbxTagNo.Text+"'";
            frm.selectquery = selectQuery;
            //frm.tagNo  = this.cbxTagNo .Text  ;
            frm.ShowDialog();
            //frm.crystalReportViewer1.ReportSource = doc;
            //CRHelper.DBLOGONforREPORT(frm.crystalReportViewer1);
            //frm.ShowDialog();
            //frmTagHistoryRpt reload = new frmTagHistoryRpt();
            //reload.tagNo = this.cbxTagNo.Text;
            //reload.ShowDialog();
        }

        private void frmTagHistory_Load(object sender, EventArgs e)
        {
            this.cbxItemName.SelectedIndexChanged -= new System.EventHandler(this.cbxItemName_SelectedIndexChanged);
            this.cbxItemName.DataSource = itmDAL.GetAllItems();
            this.cbxItemName.DisplayMember = "ItemName";
            this.cbxItemName.ValueMember = "ItemId";
            this.cbxItemName.SelectedIndex = -1;

        }

        private void cbxItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int k = (int)this.cbxItemName.SelectedValue;

            this.cbxTagNo.DataSource = sDAL.GetAllTagNosByItemId("select StockId ,TagNo from Stock where ItemId=" + k);
            this.cbxTagNo.DisplayMember = "TagNo";
            this.cbxTagNo.ValueMember = "StockId";

            this.cbxTagNo.SelectedIndex = -1;
        }

        private void cbxItemName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxItemName.SelectedIndexChanged += new System.EventHandler(this.cbxItemName_SelectedIndexChanged);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTagHistory_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }
    }
}
