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
    public partial class StonesSummaryRpt : Form
    {
        public StonesSummaryRpt()
        {
            InitializeComponent();
        }
       public string selectquery = "";

        private void StonesSummaryRpt_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("StonesStockSummary", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "LooseStones");
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = path+ "\\Reports\\StonesStockSummary.rpt";
            ReportDocument report = new ReportDocument();
            report.Load(reportPath);
            report.SetDataSource(ds);
            this.crystalReportViewer1.ReportSource = report;
            this.crystalReportViewer1.RefreshReport();

        }
    }
}
