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
    public partial class frmWorkerDealingSummaryRpt : Form
    {
        public frmWorkerDealingSummaryRpt()
        {
            InitializeComponent();
        }

        private void frmWorkerDealingSummaryRpt_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand("WorkerSummaryRpt", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                DataSet ds = new DataSet();
                da.Fill(ds, "WorkerSummaryRpt");
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path+"\\Reports\\WorkerSummaryRpt.rpt";
                ReportDocument report = new ReportDocument();
                report.Load(reportPath);
                report.SetDataSource(ds);
                this.crystalReportViewer1.ReportSource = report;
                this.crystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
