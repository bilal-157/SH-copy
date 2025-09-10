using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.IO;

namespace jewl
{
    public partial class frmPOrderWorkerWiseRpt : Form
    {
        public int selectQuery;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        
        public frmPOrderWorkerWiseRpt()
        {
           InitializeComponent();

        }

        private void InitializeComponent()
        {
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.SelectionFormula = "";
            this.crystalReportViewer1.Size = new System.Drawing.Size(823, 458);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // frmPOrderWorkerWiseRpt
            // 
            this.ClientSize = new System.Drawing.Size(823, 458);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "frmPOrderWorkerWiseRpt";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPOrderWorkerWiseRpt_Load);
            this.ResumeLayout(false);

        }

        private void frmPOrderWorkerWiseRpt_Load(object sender, EventArgs e)
        {
            try
            {

                //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                //SqlCommand cmd = new SqlCommand("sp_PendingOrderByWrk", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //cmd.Parameters.Add("@WorkerId", SqlDbType.Int).Value = selectQuery;
                //con.Open();
                ////DataTable dt1 = new DataTable();
                //DataSet ds = new DataSet();
                //da.Fill(ds, "test");


                //string path = Path.GetDirectoryName(Application.ExecutablePath);
                ////string reportPath = path + "/LooseStonesRpt.rpt";
                //string reportPath = path + "\\Reports\\PendingOrderByWorker.rpt";
                ////string reportPath = "../../StockSummaryRpt.rpt";
                //ReportDocument report = new ReportDocument();
                ////ReportDocument report = new ReportDocument();
                ////report.SetDataSource(ds);
                //report.Load(reportPath);



                ////report.SetDataSource(ds.Tables["test"]);
                //report.SetParameterValue("@WorkerId", selectQuery);
                //this.crystalReportViewer1.ReportSource = report;
                //CRHelper.DBLOGONforREPORT(crystalReportViewer1);

                //this.crystalReportViewer1.RefreshReport();


            }
            catch (Exception ex)
            {
                //throw ex;
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
