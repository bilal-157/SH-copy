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
    public partial class frmWorkerListRpt : Form
    {
        public frmWorkerListRpt()
        {
            InitializeComponent();
        }

        private void frmWorkerListRpt_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "Select w.WorkerName as Name , w.Address as Address , w.TelNo as ContactNo , w.Refrence  as Reference,((select isnull(sum(PWeight),0) from WorkerGold_Trans where  Status='GoldGiven' and WorkerId=w.WorkerId)+(select isnull(sum(PWeight),0) from WorkerGold_Trans where  Status='CashToGold' and GRStatus='Given' and WorkerId=w.WorkerId))as GivenWeight,((select isnull(sum(PWeight),0) from WorkerGold_Trans where  Status='GoldReceive' and WorkerId=w.WorkerId)+(select isnull(sum(PWeight),0) from WorkerGold_Trans where Status='CashToGold' and GRStatus='Receive' and WorkerId=w.WorkerId)) as ReceiveWeight  from Worker w";
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
               
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                DataSet ds = new DataSet();
                da.Fill(ds, "Worker");
               
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path+"\\Reports\\WorkerRpt.rpt";
                //string reportPath = "../../StockSummaryRpt.rpt";
                ReportDocument report = new ReportDocument();
                //ReportDocument report = new ReportDocument();
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
