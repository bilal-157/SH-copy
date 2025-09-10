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
    public partial class frmGoldPurchaseSaleSummaryRpt : Form
    {
        //testing
        public frmGoldPurchaseSaleSummaryRpt()
        {
            InitializeComponent();
        }

        private void frmGoldPurchaseSaleSummaryRpt_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand("GoldPurchaseSummaryRpt", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //BarCodeDataSet ds = new BarCodeDataSet();
                DataSet ds = new DataSet();
                da.Fill(ds, "GoldPurchaseSummary");
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath =path+"\\Reports\\GoldPurchaseSummary.rpt";
                //string reportPath = "../../GoldPurchaseSummary.rpt";
                ReportDocument report = new ReportDocument();
                report.Load(reportPath);
                report.SetDataSource(ds);
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
