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
    public partial class frmWorkerCheejadRpt : Form
    {
        public DateTime dateto;
        public DateTime datefrom;
        public int wrkid;
        public bool isdate;
        public string strr;
        UtilityDAL utlDAL = new UtilityDAL();
        public frmWorkerCheejadRpt()
        {
            InitializeComponent();
        }

        private void frmWorkerCheejadRpt_Load(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = null;
                if (this.isdate == false)
                {
                    query = "select w.*,(select itemname from item where itemid=w.ItemId) as ItemName,(Select ISNULL(SUM(Price),0) from StonesDetail Where TranId = w.TranId)'StonesPrice',(select OpeningCash from ChildAccount where ChildCode = wd.AccountCode)'OpCash',(select OpeningGold from ChildAccount where ChildCode = wd.AccountCode)'OpGold',((select OpeningCash from ChildAccount where ChildCode = wd.AccountCode)+(select isnull(sum(dr),0)-isnull(sum(cr),0) from vouchers where AccountCode=wd.AccountCode))'AccBalance', wd.WorkerName from workerGold_Trans w inner join worker wd on wd.WorkerId=w.WorkerId where w.WorkerId=" + wrkid + " order by w.EntryDate";
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
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                DataSet ds = new DataSet("WorkerDealingsAfr");
                DataSet dss = new DataSet();
                DataTable dt3 = new DataTable();
                dt3 = ds.Tables.Add("WorkerDealingsAfr");

                dt3.Columns.Add("WorkerName", typeof(System.String));
                dt3.Columns.Add("OpCash", typeof(System.Decimal));
                dt3.Columns.Add("OpGold", typeof(System.Decimal));

                dt3.Columns.Add("GTranId", typeof(System.Int32));
                dt3.Columns.Add("GWorkerId", typeof(System.Int32));
                dt3.Columns.Add("GEntryDate", typeof(System.DateTime));
                dt3.Columns.Add("GWeightCash", typeof(System.Decimal));
                dt3.Columns.Add("GToCashGold", typeof(System.Decimal));
                dt3.Columns.Add("GStatus", typeof(System.String));
                dt3.Columns.Add("GGRStatus", typeof(System.String));
                dt3.Columns.Add("GDescription", typeof(System.String));
                dt3.Columns.Add("GUjrat", typeof(System.Decimal));
                dt3.Columns.Add("GUjratGiven", typeof(System.Decimal));
                dt3.Columns.Add("GQty", typeof(System.Decimal));
                dt3.Columns.Add("GKaat", typeof(System.Decimal));
                dt3.Columns.Add("GPWeight", typeof(System.Decimal));
                dt3.Columns.Add("GPurity", typeof(System.Decimal));
                dt3.Columns.Add("GKarat", typeof(System.String));
                dt3.Columns.Add("GPMaking", typeof(System.Decimal));
                dt3.Columns.Add("GCheejad", typeof(System.Decimal));
                dt3.Columns.Add("GCheejadDecided", typeof(System.Decimal));               
                dt3.Columns.Add("GGoldRate", typeof(System.Decimal));
                dt3.Columns.Add("GWItemId", typeof(System.String));
                dt3.Columns.Add("GItemName", typeof(System.String));
                dt3.Columns.Add("GBillNo", typeof(System.Int32));

                dt3.Columns.Add("RTranId", typeof(System.Int32));
                dt3.Columns.Add("RWorkerId", typeof(System.Int32));
                dt3.Columns.Add("REntryDate", typeof(System.DateTime));
                dt3.Columns.Add("RWeightCash", typeof(System.Decimal));
                dt3.Columns.Add("RToCashGold", typeof(System.Decimal));
                dt3.Columns.Add("RStatus", typeof(System.String));
                dt3.Columns.Add("RGRStatus", typeof(System.String));
                dt3.Columns.Add("RDescription", typeof(System.String));
                dt3.Columns.Add("RUjrat", typeof(System.Decimal));
                dt3.Columns.Add("RUjratGiven", typeof(System.Decimal));
                dt3.Columns.Add("RQty", typeof(System.Decimal));
                dt3.Columns.Add("RKaat", typeof(System.Decimal));
                dt3.Columns.Add("RPWeight", typeof(System.Decimal));
                dt3.Columns.Add("RPurity", typeof(System.Decimal));
                dt3.Columns.Add("RKarat", typeof(System.String));
                dt3.Columns.Add("RPMaking", typeof(System.Decimal));
                dt3.Columns.Add("RCheejad", typeof(System.Decimal));
                dt3.Columns.Add("RCheejadDecided", typeof(System.Decimal));                
                dt3.Columns.Add("RGoldRate", typeof(System.Decimal));
                dt3.Columns.Add("RWItemId", typeof(System.String));
                dt3.Columns.Add("RItemName", typeof(System.String));
                dt3.Columns.Add("RBillNo", typeof(System.Int32));
                          
               
              
                             
                
                
                dt1.DefaultView.Sort = "EntryDate asc";
                DataRow[] result1 = dt1.Select("GRStatus='Given'");
                DataRow[] result2 = dt1.Select("GRStatus='Receive'");
                DataRow r = null;
                
                if (result1.Length >= result2.Length)
                {
                    for (int i = 0; i < result1.Length; i++)
                    {
                        r = dt3.NewRow();
                                                                                        
                        r["GTranId"] = result1[i]["TranId"];
                        r["GWorkerId"] = result1[i]["WorkerId"];
                        r["GEntryDate"] = result1[i]["EntryDate"];
                        r["GWeightCash"] = result1[i]["WeightCash"];
                        r["GToCashGold"] = result1[i]["ToCashGold"];
                        r["GStatus"] = result1[i]["Status"];
                        r["GGRStatus"] = result1[i]["GRStatus"];
                        r["GDescription"] = result1[i]["Description"];
                        r["GUjrat"] = result1[i]["Ujrat"];
                        r["GUjratGiven"] = result1[i]["UjratGiven"];
                        r["GQty"] = result1[i]["Qty"];
                        r["GKaat"] = result1[i]["Kaat"];
                        r["GPWeight"] = result1[i]["PWeight"];
                        r["GPurity"] = result1[i]["Purity"];
                        r["GKarat"] = result1[i]["Karat"];
                        r["GPMaking"] = result1[i]["PMaking"];
                        r["GCheejad"] = result1[i]["Cheejad"];
                        r["GCheejadDecided"] = result1[i]["CheejadDecided"];
                        r["WorkerName"] = result1[i]["WorkerName"];
                        r["GGoldRate"] = result1[i]["GoldRate"];
                        r["GWItemId"] = result1[i]["WItemId"];
                        r["GItemName"] = result1[i]["ItemName"];
                        r["GBillNo"] = result1[i]["BillNo"];
                        r["OpGold"] = result1[i]["OpGold"];
                        r["OpCash"] = result1[i]["OpCash"];
                        string str = result1[i]["WorkerName"].ToString();
                        if (result2.Length == 0)
                        {                            
                        //    r["RReceivedWeight"] = 0;
                        //    r["RDescription"] = "";
                        //    r["RPWeight"] = 0;
                        //    r["Rujratgiven"] = 0;
                        //    r["RStonePrice"] = 0;                           
                        //    r["GWtWeight"] = 0;                           
                        //    string str1 = r["GGivenWeight"].ToString();
                        }
                        if (i < result2.Length)
                        {                            
                            r["RTranId"] = result2[i]["TranId"];
                            r["RWorkerId"] = result2[i]["WorkerId"];
                            r["REntryDate"] = result2[i]["EntryDate"];
                            r["RWeightCash"] = result2[i]["WeightCash"];
                            r["RToCashGold"] = result2[i]["ToCashGold"];
                            r["RStatus"] = result2[i]["Status"];
                            r["RGRStatus"] = result2[i]["GRStatus"];
                            r["RDescription"] = result2[i]["Description"];
                            r["RUjrat"] = result2[i]["Ujrat"];
                            r["RUjratGiven"] = result2[i]["UjratGiven"];
                            r["RQty"] = result2[i]["Qty"];
                            r["RKaat"] = result2[i]["Kaat"];
                            r["RPWeight"] = result2[i]["PWeight"];
                            r["RPurity"] = result2[i]["Purity"];
                            r["RKarat"] = result2[i]["Karat"];
                            r["RPMaking"] = result2[i]["PMaking"];
                            r["RCheejad"] = result2[i]["Cheejad"];
                            r["RCheejadDecided"] = result2[i]["CheejadDecided"];
                            r["WorkerName"] = result2[i]["WorkerName"];
                            r["RGoldRate"] = result2[i]["GoldRate"];
                            r["RWItemId"] = result2[i]["WItemId"];
                            r["RItemName"] = result2[i]["ItemName"];
                            r["RBillNo"] = result2[i]["BillNo"];
                            r["OpGold"] = result2[i]["OpGold"];
                            r["OpCash"] = result2[i]["OpCash"];


                          //  string str1 = r["GGivenWeight"].ToString();
                        }
                       
                        dt3.Rows.Add(r);
                        r = null;

                    }
                }
                else if (result2.Length >= result1.Length)
                {
                    for (int i = 0; i < result2.Length; i++)
                    {
                        r = dt3.NewRow();

                        r["RTranId"] = result2[i]["TranId"];
                        r["RWorkerId"] = result2[i]["WorkerId"];
                        r["REntryDate"] = result2[i]["EntryDate"];
                        r["RWeightCash"] = result2[i]["WeightCash"];
                        r["RToCashGold"] = result2[i]["ToCashGold"];
                        r["RStatus"] = result2[i]["Status"];
                        r["RGRStatus"] = result2[i]["GRStatus"];
                        r["RDescription"] = result2[i]["Description"];
                        r["RUjrat"] = result2[i]["Ujrat"];
                        r["RUjratGiven"] = result2[i]["UjratGiven"];
                        r["RQty"] = result2[i]["Qty"];
                        r["RKaat"] = result2[i]["Kaat"];
                        r["RPWeight"] = result2[i]["PWeight"];
                        r["RPurity"] = result2[i]["Purity"];
                        r["RKarat"] = result2[i]["Karat"];
                        r["RPMaking"] = result2[i]["PMaking"];
                        r["RCheejad"] = result2[i]["Cheejad"];
                        r["RCheejadDecided"] = result2[i]["CheejadDecided"];
                        r["WorkerName"] = result2[i]["WorkerName"];
                        r["RGoldRate"] = result2[i]["GoldRate"];
                        r["RWItemId"] = result2[i]["WItemId"];
                        r["RItemName"] = result2[i]["ItemName"];
                        r["RBillNo"] = result2[i]["BillNo"];
                        r["OpGold"] = result2[i]["OpGold"];
                        r["OpCash"] = result2[i]["OpCash"];
                        if (result1.Length == 0)
                        {
                            //r["GGivenWeight"] = 0;
                            //r["GPWeight"] = 0;
                            //r["GCashGiven"] = 0;
                            //r["GStonePrice"] = 0;
                        }
                        if (i < result1.Length)
                        {
                            r["GTranId"] = result1[i]["TranId"];
                            r["GWorkerId"] = result1[i]["WorkerId"];
                            r["GEntryDate"] = result1[i]["EntryDate"];
                            r["GWeightCash"] = result1[i]["WeightCash"];
                            r["GToCashGold"] = result1[i]["ToCashGold"];
                            r["GStatus"] = result1[i]["Status"];
                            r["GGRStatus"] = result1[i]["GRStatus"];
                            r["GDescription"] = result1[i]["Description"];
                            r["GUjrat"] = result1[i]["Ujrat"];
                            r["GUjratGiven"] = result1[i]["UjratGiven"];
                            r["GQty"] = result1[i]["Qty"];
                            r["GKaat"] = result1[i]["Kaat"];
                            r["GPWeight"] = result1[i]["PWeight"];
                            r["GPurity"] = result1[i]["Purity"];
                            r["GKarat"] = result1[i]["Karat"];
                            r["GPMaking"] = result1[i]["PMaking"];
                            r["GCheejad"] = result1[i]["Cheejad"];
                            r["GCheejadDecided"] = result1[i]["CheejadDecided"];
                            r["WorkerName"] = result1[i]["WorkerName"];
                            r["GGoldRate"] = result1[i]["GoldRate"];
                            r["GWItemId"] = result1[i]["WItemId"];
                            r["GItemName"] = result1[i]["ItemName"];
                            r["GBillNo"] = result1[i]["BillNo"];
                            r["OpGold"] = result1[i]["OpGold"];
                            r["OpCash"] = result1[i]["OpCash"];

                        }                       
                        dt3.Rows.Add(r);
                        r = null;
                    }
                }
                int m = 0;
                m = dt3.Rows.Count;               
                string path = Path.GetDirectoryName(DALHelper.ReportsPath);
                string reportPath =  path+"\\Reports\\WorkerDealingByCheejadWorker.rpt";             
                ReportDocument report = new ReportDocument();               
                utlDAL.VerifyReports(reportPath, report);
                report.SetDataSource(ds);               
                this.crystalReportViewer1.ReportSource = report;
            
            }
            catch (Exception ex)
            {
               
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
