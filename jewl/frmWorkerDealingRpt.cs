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
    public partial class frmWorkerDealingRpt : Form
    {

        public frmWorkerDealingRpt()
        {
            InitializeComponent();
        }

        public DateTime dateto;
        public DateTime datefrom;
        public int wrkid;
        public bool isdate;
        //public string selectQuery="";
        //public string selectQueryR = "";

        private void frmWorkerDealingRpt_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = null;
                //string query = "select w.Description , w.GivenWeight , w.ReceivedWeight , w.Kaat , w.Purity , w.Karat , w.PWeight , w.PMaking , w.WtWeight , w.TotalWeight , w.Ujrat , w.ujratgiven , w.cashGiven , w.CashBalance , w.GoldBalance from Workergold_trans w where workerid = " + 1;
                if (this.isdate == false)
                {
                    query = "select w.*  , wd.WorkerName from workerGold_Trans w inner join worker wd on wd.WorkerId=w.WorkerId where w.WorkerId=" + wrkid + "order by w.TDate";
                    cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                }
                else
                {
                    cmd = new SqlCommand("DDate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@df", SqlDbType.DateTime).Value = this.datefrom;
                    cmd.Parameters.Add("@dt", SqlDbType.DateTime).Value = this.dateto;
                    cmd.Parameters.Add("@WorkerId", SqlDbType.Int).Value = this.wrkid;
                }



                //SqlCommand cmd = new SqlCommand("GoldGiven", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@WorkerId", SqlDbType.Int).Value = wrkid;
                //SqlCommand cmd1 = new SqlCommand("GoldReceived", con);
                //cmd1.CommandType = CommandType.StoredProcedure;
                //cmd1.Parameters.Add("@WorkerId", SqlDbType.Int).Value = wrkid;                

                //string str =" Data Source =.; Initial Catalog = DressShopDB; Integrated Security = True";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter db = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                //DataTable dt2 = new DataTable();
                da.Fill(dt1);
                //db.Fill(dt2);
                //int t = dt1.Rows.Count;
                //int j = dt2.Rows.Count;
                DataSet ds = new DataSet("WorkerDealingsAfr");
                DataTable dt3 = new DataTable();
                dt3 = ds.Tables.Add("WorkerDealingsAfr");
                dt3.Columns.Add("GGDate", typeof(System.DateTime));
                dt3.Columns.Add("GDescription", typeof(System.String));
                dt3.Columns.Add("GGivenWeight", typeof(System.Decimal));
                dt3.Columns.Add("GCashGiven", typeof(System.Decimal));
                dt3.Columns.Add("GPWeight", typeof(System.Decimal));
                dt3.Columns.Add("WorkerName", typeof(System.String));
                dt3.Columns.Add("RAddDate", typeof(System.DateTime));
                dt3.Columns.Add("RDescription", typeof(System.String));
                dt3.Columns.Add("RPurity", typeof(System.Decimal));
                dt3.Columns.Add("RReceivedWeight", typeof(System.Decimal));
                dt3.Columns.Add("RKaat", typeof(System.Decimal));
                dt3.Columns.Add("RPWeight", typeof(System.Decimal));
                dt3.Columns.Add("Rujratgiven", typeof(System.Decimal));
                dt3.Columns.Add("CheejadDecided", typeof(System.Decimal));
                dt1.DefaultView.Sort = "TDate asc";
                DataRow[] result1 = dt1.Select("GivenWeight<>0 or CashGiven<>0");
                DataRow[] result2 = dt1.Select("ReceivedWeight<>0");
                DataRow r = null;
                if (result1.Length >= result2.Length)
                {
                    for (int i = 0; i < result1.Length; i++)
                    {
                        r = dt3.NewRow();
                        r["GGDate"] = result1[i]["TDate"];
                        r["GDescription"] = result1[i]["Description"];
                        r["GGivenWeight"] = result1[i]["GivenWeight"];
                        r["GCashGiven"] = result1[i]["CashGiven"];
                        r["GPWeight"] = result1[i]["PWeight"];
                        r["WorkerName"] = result1[i]["WorkerName"];
                        string str = result1[i]["WorkerName"].ToString();

                        if (i < result2.Length)
                        {
                            r["RAddDate"] = result2[i]["TDate"];
                            r["RReceivedWeight"] = result2[i]["ReceivedWeight"];
                            r["RDescription"] = result2[i]["Description"];
                            r["RPurity"] = result2[i]["Purity"];
                            r["RKaat"] = result2[i]["Kaat"];
                            r["RPWeight"] = result2[i]["PWeight"];
                            r["Rujratgiven"] = result2[i]["ujratgiven"];
                            r["CheejadDecided"] = result2[i]["CheejadDecided"];
                            //r["WorkerName"] = result2[i]["WorkerName"];

                            string str1 = r["GGivenWeight"].ToString();
                            //r["WorkerName"] = result2[i]["WorkerName"];

                        }
                        //else
                        //{
                        //    r["GGDate"] = DBNull.Value;
                        //    r["GDescription"] = "";
                        //    r["GGivenWeight"] = 0;
                        //    r["GCashGiven"] = 0;
                        //}
                        dt3.Rows.Add(r);
                        r = null;

                    }
                }
                else if (result2.Length >= result1.Length)
                {
                    for (int i = 0; i < result2.Length; i++)
                    {
                        r = dt3.NewRow();
                        r["RAddDate"] = result2[i]["TDate"];
                        r["RReceivedWeight"] = result2[i]["ReceivedWeight"];
                        r["RDescription"] = result2[i]["Description"];
                        r["RPurity"] = result2[i]["Purity"];
                        r["RKaat"] = result2[i]["Kaat"];
                        r["RPWeight"] = result2[i]["PWeight"];
                        r["Rujratgiven"] = result2[i]["ujratgiven"];
                        r["WorkerName"] = result2[i]["WorkerName"];
                        r["CheejadDecided"] = result2[i]["CheejadDecided"];

                        if (i < result1.Length)
                        {
                            r["GGDate"] = result1[i]["TDate"];
                            r["GDescription"] = result1[i]["Description"];
                            r["GGivenWeight"] = result1[i]["GivenWeight"];
                            r["GCashGiven"] = result1[i]["CashGiven"];
                            r["GPWeight"] = result1[i]["PWeight"];
                            //r["WorkerName"] = result2[i]["WorkerName"];
                            //r["WorkerName"] = result1[i]["WorkerName"];
                        }
                        //else
                        //{
                        //    r["RAddDate"] =DBNull.Value;
                        //    r["RReceivedWeight"] = 0;
                        //    r["RDescription"] = "";
                        //    r["RPurity"] = 0;
                        //    r["RKaat"] = 0;
                        //    r["RPWeight"] =0;
                        //    r["Rujratgiven"] = 0;
                        //}
                        dt3.Rows.Add(r);
                        r = null;

                    }
                }
                #region Comment
                //BarCodeDataSet ds = new BarCodeDataSet();
                //DataTable dt3 = new DataTable();
                //dt3 = ds.Tables.Add("WorkerDealing");
                //dt3.Columns.Add("GGDate", typeof(System.DateTime));
                //dt3.Columns.Add("GDescription", typeof(System.String));
                //dt3.Columns.Add("GGivenWeight", typeof(System.Decimal));
                //dt3.Columns.Add("GCashGiven", typeof(System.Decimal));
                //dt3.Columns.Add("WorkerName", typeof(System.String));
                //dt3.Columns.Add("RAddDate", typeof(System.DateTime));
                //dt3.Columns.Add("RDescription", typeof(System.String));
                //dt3.Columns.Add("RPurity", typeof(System.Decimal));
                //dt3.Columns.Add("RReceivedWeight", typeof(System.Decimal));
                //dt3.Columns.Add("RKaat", typeof(System.Decimal));
                //dt3.Columns.Add("RPWeight", typeof(System.Decimal));
                //dt3.Columns.Add("Rujratgiven", typeof(System.Decimal));
                //dt3.Columns.Add("GGDate", typeof(System.DateTime));
                //dt3.Columns.Add("GGDate", typeof(System.DateTime));
                //dt3.Columns.Add("GGDate", typeof(System.DateTime));
                //DataRow r = null;
                //if (j >= t)
                //{
                //    for (int i = 0; i < j; i++)
                //    {
                //        r = dt3.NewRow();
                //        r["RAddDate"] = dt2.Rows[i]["RAddDate"];
                //        r["RReceivedWeight"] = dt2.Rows[i]["RReceivedWeight"];
                //        r["RDescription"] = dt2.Rows[i]["RDescription"];
                //        r["RPurity"] = dt2.Rows[i]["RPurity"];
                //        r["RKaat"] = dt2.Rows[i]["RKaat"];
                //        r["RPWeight"] = dt2.Rows[i]["RPWeight"];
                //        r["Rujratgiven"] = dt2.Rows[i]["Rujratgiven"];
                //        r["WorkerName"] = dt2.Rows[i]["WorkerName"];
                //        if (i < t)
                //        {
                //            r["GGDate"] = dt1.Rows[i]["GGDate"];
                //            r["GDescription"] = dt1.Rows[i]["GDescription"];
                //            r["GGivenWeight"] = dt1.Rows[i]["GGivenWeight"];
                //            r["GCashGiven"] = dt1.Rows[i]["GCashGiven"];
                //            //r["WorkerName"] = dt1.Rows[i]["WorkerName"];

                //        }
                //        //else
                //        //{
                //        //    r["RAddDate"] = null; ;
                //        //    r["RReceivedWeight"] = null;
                //        //    r["RDescription"] = "";
                //        //    r["RPurity"] =null;
                //        //    r["RKaat"] = null;
                //        //    r["RPWeight"] = null;
                //        //    r["Rujratgiven"] = null;
                //        //}
                //        dt3.Rows.Add(r);
                //        r = null;
                //    }
                //}
                //else
                //{
                //    for (int i = 0; i < t; i++)
                //    {
                //        r = dt3.NewRow();
                //        r["GGDate"] = dt1.Rows[i]["GGDate"];
                //        r["GDescription"] = dt1.Rows[i]["GDescription"];
                //        r["GGivenWeight"] = dt1.Rows[i]["GGivenWeight"];
                //        r["GCashGiven"] = dt1.Rows[i]["GCashGiven"];
                //        r["WorkerName"] = dt1.Rows[i]["WorkerName"];

                //        if (i < j)
                //        {
                //            r["RAddDate"] = dt2.Rows[i]["RAddDate"];
                //            r["RReceivedWeight"] = dt2.Rows[i]["RReceivedWeight"];
                //            r["RDescription"] = dt2.Rows[i]["RDescription"];
                //            r["RPurity"] = dt2.Rows[i]["RPurity"];
                //            r["RKaat"] = dt2.Rows[i]["RKaat"];
                //            r["RPWeight"] = dt2.Rows[i]["RPWeight"];
                //            r["Rujratgiven"] = dt2.Rows[i]["Rujratgiven"];
                //        }
                //        //else
                //        //{
                //        //    r["RAddDate"] = null; ;
                //        //    r["RReceivedWeight"] = null;
                //        //    r["RDescription"] = "";
                //        //    r["RPurity"] = null;
                //        //    r["RKaat"] = null;
                //        //    r["RPWeight"] = null;
                //        //    r["Rujratgiven"] = null;
                //        //}
                //        dt3.Rows.Add(r);
                //        r = null;
                //    }
                //}
                #endregion

                int m = 0;
                m = dt3.Rows.Count;


                //dt3.DefaultView.Sort = "TDate";
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path+"\\Reports\\WorkerDealings.rpt";
                //string path = Path.GetDirectoryName(Application.ExecutablePath);
                //string reportPath = ConfigurationManager.AppSettings["ReportsPath"] + "WorkerDealings.rpt";
                //string reportPath = "../../StockSummaryRpt.rpt";
                //string reportPath = path+"/Repots/WorkerDealings.rpt";
                ReportDocument report = new ReportDocument();
                //DataRow[] result = dt1.Select("TranDate'")
                //ReportDocument report = new ReportDocument();
                //report.SetDataSource(ds);
                report.Load(reportPath);
                report.SetDataSource(ds);
                //report.RecordSelectionFormula = selectQuery ;
                //report.RecordSelectionFormula = selectQueryR;
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
