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
    public partial class frmWorkerPurcheeRpt : Form
    {
        public frmWorkerPurcheeRpt()
        {
            InitializeComponent();
        }

        private void frmWorkerPurcheeRpt_Load(object sender, EventArgs e)
        {
            try
            {
                //string query = "select w.Description , w.GivenWeight , w.ReceivedWeight , w.Kaat , w.Purity , w.Karat , w.PWeight , w.PMaking , w.WtWeight , w.TotalWeight , w.Ujrat , w.ujratgiven , w.cashGiven , w.CashBalance , w.GoldBalance from Workergold_trans w where workerid = " + 1;
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand("WorkerPurchee", con);
                cmd.Parameters.Add("@TranId", SqlDbType.Int).Value = 8;

                cmd.CommandType = CommandType.StoredProcedure;

                //string str =" Data Source =.; Initial Catalog = DressShopDB; Integrated Security = True";
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                con.Open();
                DataSet ds = new DataSet();
                da.Fill(ds, "WorkerDealings");

                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath =  path+"\\Reports\\WorkerPurcheeRpt.rpt";
                //string reportPath = "../../StockSummaryRpt.rpt";
                // ReportDocument report = new ReportDocument();
                ReportDocument report = new ReportDocument();
                //report.SetDataSource(ds);
                report.Load(reportPath);



                report.SetDataSource(ds);

                this.crystalReportViewer1.ReportSource = report;
                this.crystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                //throw ex;
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
