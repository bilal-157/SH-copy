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
    public partial class frmWorkerDealByDateRpt : Form
    {
        public frmWorkerDealByDateRpt()
        {
            InitializeComponent();
        }
        public int wkd;
        public DateTime datefrom;
        public DateTime dateto;

        private void frmWorkerDealByDateRpt_Load(object sender, EventArgs e)
        {
            try
            {
                //string query = "select w.Description , w.GivenWeight , w.ReceivedWeight , w.Kaat , w.Purity , w.Karat , w.PWeight , w.PMaking , w.WtWeight , w.TotalWeight , w.Ujrat , w.ujratgiven , w.cashGiven , w.CashBalance , w.GoldBalance from Workergold_trans w where workerid = " + 1;
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand("GoldGivenDate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@WorkerId", SqlDbType.Int).Value = wkd;
                cmd.Parameters.Add("@datef", SqlDbType.DateTime).Value = datefrom;
                cmd.Parameters.Add("@Datet", SqlDbType.DateTime).Value = dateto;

                SqlCommand cmd1 = new SqlCommand("GoldReceivedDate", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@WorkerId", SqlDbType.Int).Value = wkd;
                cmd1.Parameters.Add("@datef", SqlDbType.DateTime).Value = datefrom;
                cmd1.Parameters.Add("@Datet", SqlDbType.DateTime).Value = dateto;

                //string str =" Data Source =.; Initial Catalog = DressShopDB; Integrated Security = True";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlDataAdapter db = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                DataTable dt2 = new DataTable();              
                db.Fill(dt2);
                int t = dt1.Rows.Count;
                int j = dt2.Rows.Count;

                DataSet ds = new DataSet();
                DataTable dt3 = new DataTable();
                dt3 = ds.Tables.Add("WorkerDealing");
                dt3.Columns.Add("GGDate", typeof(System.DateTime));
                dt3.Columns.Add("GDescription", typeof(System.String));
                dt3.Columns.Add("GGivenWeight", typeof(System.Decimal));
                dt3.Columns.Add("GCashGiven", typeof(System.Decimal));
                dt3.Columns.Add("WorkerName", typeof(System.String));
                dt3.Columns.Add("RAddDate", typeof(System.DateTime));
                dt3.Columns.Add("RDescription", typeof(System.String));
                dt3.Columns.Add("RPurity", typeof(System.Decimal));
                dt3.Columns.Add("RReceivedWeight", typeof(System.Decimal));
                dt3.Columns.Add("RKaat", typeof(System.Decimal));
                dt3.Columns.Add("RPWeight", typeof(System.Decimal));
                dt3.Columns.Add("Rujratgiven", typeof(System.Decimal));
                //dt3.Columns.Add("GGDate", typeof(System.DateTime));
                //dt3.Columns.Add("GGDate", typeof(System.DateTime));
                //dt3.Columns.Add("GGDate", typeof(System.DateTime));
                DataRow r = null;
                if (j >= t)
                {
                    for (int i = 0; i < j; i++)
                    {
                        r = dt3.NewRow();
                        r["GGDate"] = dt1.Rows[i]["GGDate"];
                        r["GDescription"] = dt1.Rows[i]["GDescription"];
                        r["GGivenWeight"] = dt1.Rows[i]["GGivenWeight"];
                        r["GCashGiven"] = dt1.Rows[i]["GCashGiven"];
                        r["WorkerName"] = dt1.Rows[i]["WorkerName"];
                        if (i < t)
                        {
                            r["RAddDate"] = dt2.Rows[i]["RAddDate"];
                            r["RReceivedWeight"] = dt2.Rows[i]["RReceivedWeight"];
                            r["RDescription"] = dt2.Rows[i]["RDescription"];
                            r["RPurity"] = dt2.Rows[i]["RPurity"];
                            r["RKaat"] = dt2.Rows[i]["RKaat"];
                            r["RPWeight"] = dt2.Rows[i]["RPWeight"];
                            r["Rujratgiven"] = dt2.Rows[i]["Rujratgiven"];

                        }
                        dt3.Rows.Add(r);
                        r = null;
                    }
                }
                else
                {
                    for (int i = 0; i < t; i++)
                    {
                        r = dt3.NewRow();
                        r["GGDate"] = dt1.Rows[i]["GGDate"];
                        r["GDescription"] = dt1.Rows[i]["GDescription"];
                        r["GGivenWeight"] = dt1.Rows[i]["GGivenWeight"];
                        r["GCashGiven"] = dt1.Rows[i]["GCashGiven"];
                        r["WorkerName"] = dt1.Rows[i]["WorkerName"];

                        if (i < j)
                        {
                            r["RAddDate"] = dt2.Rows[i]["RAddDate"];
                            r["RReceivedWeight"] = dt2.Rows[i]["RReceivedWeight"];
                            r["RDescription"] = dt2.Rows[i]["RDescription"];
                            r["RPurity"] = dt2.Rows[i]["RPurity"];
                            r["RKaat"] = dt2.Rows[i]["RKaat"];
                            r["RPWeight"] = dt2.Rows[i]["RPWeight"];
                            r["Rujratgiven"] = dt2.Rows[i]["Rujratgiven"];
                        }
                        dt3.Rows.Add(r);
                        r = null;
                    }
                }

                int m = 0;
                m = dt3.Rows.Count;




                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path+"\\Reports\\WorkerDealings.rpt";
                //string reportPath = "../../WorkerDealings.rpt";
                //string reportPath = "../../StockSummaryRpt.rpt";
                ReportDocument report = new ReportDocument();
                //ReportDocument report = new ReportDocument();
                //report.SetDataSource(ds);
                report.Load(reportPath);
                report.SetDataSource(ds.Tables["WorkerDealing"]);
                this.crystalReportViewer1.ReportSource = report;
                this.crystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                //throw ex;
                MessageBox.Show(ex.ToString());
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
