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
    public partial class frmGoldPurchaseSaleRptByDate : Form
    {
        //testing
        public DateTime dateFrom;
        public DateTime dateTo;
        public frmGoldPurchaseSaleRptByDate()
        {
            InitializeComponent();
        }

        private void frmGoldPurchaseSaleRptByDate_Load(object sender, EventArgs e)
        {
            try
            {
                //string query = "";
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = null;
                //if (isdate == false)
                //{
                //    query = "select gd.*,cs.FName from GoldDetail gd inner join  CustomerInfo cs on cs.custid = gd.custid where gd.custId =" + this.CustomerId;
                //    cmd = new SqlCommand(query, con);
                //    cmd.CommandType = CommandType.Text;
                //}
                //if (isdate == true)
                //{
                    //query = "select gd.*,cs.FName from GoldDetail gd inner join  CustomerInfo cs on cs.custid = gd.custid where gd.custId =" + this.CustomerId;
                    cmd = new SqlCommand("GoldSPRptByDate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@CustId", SqlDbType.Int).Value = this.CustomerId;
                    cmd.Parameters.Add("@df", SqlDbType.DateTime).Value = this.dateFrom;
                    cmd.Parameters.Add("@dt", SqlDbType.DateTime).Value = this.dateTo;
                //}
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                DataSet ds = new DataSet("GoldPurchaseSaleRpt");
                DataTable dt3 = new DataTable();
                dt3 = ds.Tables.Add("GoldPurchaseSaleRpt");
                dt3.Columns.Add("PGDate", typeof(System.DateTime));
                dt3.Columns.Add("Description", typeof(System.String));
                dt3.Columns.Add("GPNOU", typeof(System.Int32));
                dt3.Columns.Add("GPNOP", typeof(System.Int32));
                dt3.Columns.Add("GSNOU", typeof(System.Int32));
                dt3.Columns.Add("GSNOP", typeof(System.Int32));
                dt3.Columns.Add("Weight", typeof(System.Decimal));
                dt3.Columns.Add("PWeight", typeof(System.Decimal));
                dt3.Columns.Add("Amount", typeof(System.Decimal));
                dt3.Columns.Add("UsedPAmount", typeof(System.Decimal));
                dt3.Columns.Add("GoldType", typeof(System.String));
                dt3.Columns.Add("UName", typeof(System.String));
                dt3.Columns.Add("SName", typeof(System.String));
                dt3.Columns.Add("SUName", typeof(System.String));
                dt3.Columns.Add("Name", typeof(System.String));
                dt3.Columns.Add("UsedPGDate", typeof(System.DateTime));
                dt3.Columns.Add("UsedPDescription", typeof(System.String));
                dt3.Columns.Add("UsedPWeight", typeof(System.Decimal));
                dt3.Columns.Add("UsedPKaat", typeof(System.Decimal));
                dt3.Columns.Add("UsedPPWeight", typeof(System.Decimal));
                dt3.Columns.Add("SPGDate", typeof(System.DateTime));
                dt3.Columns.Add("SWeight", typeof(System.Decimal));
                dt3.Columns.Add("SUsedWeight", typeof(System.Decimal));
                dt3.Columns.Add("SPWeight", typeof(System.Decimal));
                dt3.Columns.Add("SUsedPWeight", typeof(System.Decimal));
                dt3.Columns.Add("SAmount", typeof(System.Decimal));
                dt3.Columns.Add("SUsedAmount", typeof(System.Decimal));
                dt3.Columns.Add("SDescription", typeof(System.String));
                dt3.Columns.Add("SUsedDescription", typeof(System.String));
                dt3.Columns.Add("SRate", typeof(System.Decimal));
                dt3.Columns.Add("SUsedRate", typeof(System.Decimal));
                dt3.Columns.Add("SUsedKaat", typeof(System.Decimal));
                dt3.Columns.Add("SKaat", typeof(System.Decimal));
                dt3.Columns.Add("SUsedDate", typeof(System.DateTime));
                dt1.DefaultView.Sort = "PGDate asc";
                DataRow[] Purchase = dt1.Select("VNO Like 'GPV%'", "PGDate asc");
                DataRow[] PurePurchase = dt1.Select("GoldType = 0 and VNO Like 'GPV%'", "PGDate asc");
                DataRow[] UsedPurchase = dt1.Select("GoldType = 1 and VNO Like 'GPV%'", "PGDate asc");
                DataRow[] Sale = dt1.Select("VNO Like 'GSV%'", "PGDate asc");
                DataRow[] PureSale = dt1.Select("GoldType = 0 and VNO Like 'GSV%'", "PGDate asc");
                DataRow[] UsedSale = dt1.Select("GoldType = 1 and VNO Like 'GSV%'", "PGDate asc");
                DataRow r = null;
                if (Purchase.Length >= Sale.Length)
                {
                    for (int i = 0; i < Purchase.Length; i++)
                    {
                        r = dt3.NewRow();
                       
                        if (i < PurePurchase.Length)
                        {
                            r["Name"] = PurePurchase[i]["Name"];
                            r["PGDate"] = PurePurchase[i]["PGDate"];
                            r["PWeight"] = PurePurchase[i]["PWeight"];
                            r["Amount"] = PurePurchase[i]["Amount"];
                            r["GPNOP"] = PurePurchase[i]["GPNO"];
                        }
                        if (i < UsedPurchase.Length)
                        {
                            r["UName"] = UsedPurchase[i]["Name"];
                            r["UsedPGDate"] = UsedPurchase[i]["PGDate"];
                            r["UsedPKaat"] = UsedPurchase[i]["Kaat"];
                            r["UsedPWeight"] = UsedPurchase[i]["Weight"];
                            r["UsedPPWeight"] = UsedPurchase[i]["PWeight"];
                            r["UsedPAmount"] = UsedPurchase[i]["Amount"];
                            r["GPNOU"] = UsedPurchase[i]["GPNO"];
                        }
                        if (i < PureSale.Length)
                        {
                            r["SName"] = PureSale[i]["Name"];
                            r["SPGDate"] = PureSale[i]["PGDate"];
                            r["SPWeight"] = PureSale[i]["PWeight"];
                            r["SAmount"] = PureSale[i]["Amount"];
                            r["GSNOP"] = PureSale[i]["GSNO"];
                        }
                        if (i < UsedSale.Length)
                        {
                            r["SUName"] = UsedSale[i]["Name"];
                            r["SUsedDate"] = UsedSale[i]["PGDate"];
                            r["SUsedKaat"] = UsedSale[i]["Kaat"];
                            r["SUsedWeight"] = UsedSale[i]["Weight"];
                            r["SUsedPWeight"] = UsedSale[i]["PWeight"];
                            r["SUsedAmount"] = UsedSale[i]["Amount"];
                            r["GSNOU"] = UsedSale[i]["GSNO"];
                        }
                        dt3.Rows.Add(r);
                       
                        r = null;
                    }
                    //dt3.DefaultView.Sort = "UsedPGDate asc";
                }
                else if (Sale.Length >= Purchase.Length)
                {
                    for (int i = 0; i < Sale.Length; i++)
                    {
                        r = dt3.NewRow();
                       
                        if (i < PurePurchase.Length)
                        {
                            r["Name"] = PurePurchase[i]["Name"];
                            r["PGDate"] = PurePurchase[i]["PGDate"];
                            r["PWeight"] = PurePurchase[i]["PWeight"];
                            r["Amount"] = PurePurchase[i]["Amount"];
                            r["GPNOP"] = PurePurchase[i]["GPNO"];
                        }
                        if (i < UsedPurchase.Length)
                        {
                            r["UName"] = UsedPurchase[i]["Name"];
                            r["UsedPGDate"] = UsedPurchase[i]["PGDate"];
                            r["UsedPKaat"] = UsedPurchase[i]["Kaat"];
                            r["UsedPPWeight"] = UsedPurchase[i]["Weight"];
                            r["UsedPPWeight"] = UsedPurchase[i]["PWeight"];
                            r["UsedPAmount"] = UsedPurchase[i]["Amount"];
                            r["GPNOU"] = UsedPurchase[i]["GPNO"];
                        }
                        if (i < PureSale.Length)
                        {
                            r["SName"] = PureSale[i]["Name"];
                            r["SPGDate"] = PureSale[i]["PGDate"];
                            r["SPWeight"] = PureSale[i]["PWeight"];
                            r["SAmount"] = PureSale[i]["Amount"];
                            r["GSNOP"] = PureSale[i]["GSNO"];
                        }
                        if (i < UsedSale.Length)
                        {
                            r["SUName"] = UsedSale[i]["Name"];
                            r["SUsedDate"] = UsedSale[i]["PGDate"];
                            r["SUsedKaat"] = UsedSale[i]["Kaat"];
                            r["SUsedWeight"] = UsedSale[i]["Weight"];
                            r["SUsedPWeight"] = UsedSale[i]["PWeight"];
                            r["SUsedAmount"] = UsedSale[i]["Amount"];
                            r["GSNOU"] = UsedSale[i]["GSNO"];
                        }
                        dt3.Rows.Add(r);
                        r = null;
                    }

                }
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path+"\\Reports\\GoldSalePurchaseByDateRpt.rpt";
                //string reportPath = "../../GoldSalePurchaseByDateRpt.rpt";
                ReportDocument report = new ReportDocument();
                report.Load(reportPath);
                report.SetDataSource(ds);
                //report.RecordSelectionFormula = selectQuery ;
                //report.RecordSelectionFormula = selectQueryR;
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
