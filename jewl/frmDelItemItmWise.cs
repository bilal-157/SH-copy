using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using DAL;
using System.IO;

namespace jewl
{
    public partial class frmDelItemItmWise : Form
    {
        public string selectQuery = "";

        public frmDelItemItmWise()
        {
            InitializeComponent();
        }

        private void frmDelItemItmWise_Load(object sender, EventArgs e)
        {
            string query = "sp_DeletedStock";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "StockRpt");

            ReportDocument report = new ReportDocument();
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            report.Load(path + "\\Reports\\DeletedStockItemWise.rpt");
            //report.Load("../../Reports/Stock Report.rpt");

            report.SetDataSource(ds);
            crystalReportViewer1.SelectionFormula = selectQuery;

            crystalReportViewer1.ReportSource = report;

            crystalReportViewer1.RefreshReport();
        }
    }
}
