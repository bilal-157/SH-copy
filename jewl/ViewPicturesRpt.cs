using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using BusinesEntities;
using System.Windows.Forms;
using DAL;
using System.IO;
namespace jewl
{
    public partial class ViewPicturesRpt : Form
    {
        public string iType = "";
        public int ItemId = 0;
        public int SubItemId = 0;
        public string  TgFrom ="";
        public string  TgTo="";
        public string Karat = "";
        public int WorkerId = 0;
        public bool isManual = false ;
        public bool isDetail;
        public string selectQuery = "";
        public string sumQuery = "";
        public decimal tWt = 0;
        public  string[] ArrayStr = new string[10];
        StockDAL stDAL = new StockDAL();
        List<Stock> stkl = new List<Stock>();
        StonesDAL stnDAL = new StonesDAL();
        UtilityDAL utlDAL = new UtilityDAL();
        public ViewPicturesRpt()
        {
            InitializeComponent();
        }
        private void ViewPicturesRpt_Load(object sender, EventArgs e)
        {
            try
            {
                if (isManual == false )
                {
                    string query = "sp_StockRpt";
                    SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add("@TagNoF", SqlDbType.NVarChar).Value = TgFrom;
                    cmd.Parameters.Add("@TagNoT", SqlDbType.NVarChar).Value = TgTo;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StockRpt");                    
                    ReportDocument report = new ReportDocument();                    
                    string path = Path.GetDirectoryName(Application.ExecutablePath);                    
                    report.Load(path + "\\Reports\\ViewPictures.rpt");                  
                    report.SetDataSource(ds);                  
                    crystalReportViewer1.ReportSource = report;                  
                    ds = new DataSet();
                }
                else if (isManual == true)
                {
                    Stock st ;
                    DataSet ds = new DataSet();
                    DataTable dt1 = new DataTable();
                    dt1 = ds.Tables.Add("StockRpt");
                    dt1.Columns.Add("TagNo", typeof(System.String));
                    dt1.Columns.Add("StockId", typeof(System.Int32));
                    dt1.Columns.Add("BarCode", typeof(System.String));
                    dt1.Columns.Add("TotalWeight", typeof(System.Decimal));
                    dt1.Columns.Add("NetWeight", typeof(System.Decimal));
                    dt1.Columns.Add("Pieces", typeof(System.Int32));
                    dt1.Columns.Add("Karat", typeof(System.String));
                    dt1.Columns.Add("StWeight", typeof(System.Decimal));
                    dt1.Columns.Add("BdW", typeof(System.Decimal));
                    dt1.Columns.Add("DrW", typeof(System.Decimal));
                    dt1.Columns.Add("WasteInGm", typeof(System.Decimal));
                    dt1.Columns.Add("SQty", typeof(System.Decimal));
                    dt1.Columns.Add("TotalLaker", typeof(System.Decimal));
                    dt1.Columns.Add("TotalMaking", typeof(System.Decimal));
                    dt1.Columns.Add("TotalPrice", typeof(System.Decimal));
                    dt1.Columns.Add("IndexNo", typeof(System.Int32));
                    dt1.Columns.Add("IType", typeof(System.String));
                    dt1.Columns.Add("ItemName", typeof(System.String));
                    dt1.Columns.Add("Qty", typeof(System.Int32));
                    dt1.Columns.Add("Price", typeof(System.Decimal));
                    dt1.Columns.Add("StonePrice", typeof(System.Decimal));
                    dt1.Columns.Add("BeedsPrice", typeof(System.Decimal));
                    dt1.Columns.Add("Description", typeof(System.String));
                    dt1.Columns.Add("Kaat", typeof(System.Decimal));
                    dt1.Columns.Add("WorkerId", typeof(System.Int32));
                    dt1.Columns.Add("WorkerName", typeof(System.String));
                    dt1.Columns.Add("ThumbNail", typeof(System.Byte []));                    
                    List<string> str = new List<string>();
                    for (int j = 0; j < ArrayStr.Length; j++)
                    {
                        if (ArrayStr[j] != "")
                        {
                            st = new Stock();
                            st = stDAL.GetStockBySockTagNo(ArrayStr[j]);
                            if (st != null)
                            {
                                stkl.Add(st);
                                str.Add(ArrayStr[j]);
                            }
                        }
                    }                   
                        DataRow r = null;
                        for (int i = 0; i < stkl.Count; i++)
                        {
                            Stock stk = new Stock();
                            if (r == null)
                                r = dt1.NewRow();
                            stk = stkl[i];
                            r["TagNo"] = stk.TagNo.ToString();
                            r["BarCode"] = stk.BarCode.ToString();
                            r["TotalWeight"] = stk.TotalWeight;
                            r["NetWeight"] = stk.NetWeight;
                            r["Pieces"] = stk.Pieces;
                            r["Karat"] = stk.Karrat;
                            r["Kaat"] = stk.KaatInRatti;                            
                            r["WasteInGm"] = stk.WasteInGm;
                            r["TotalLaker"] = stk.TotalLaker;
                            r["TotalMaking"] = stk.TotalMaking;
                            r["BarCode"] = stk.BarCode.ToString();
                            r["TotalPrice"] = stk.TotalPrice;
                            r["WasteInGm"] = stk.WasteInGm;
                            r["ItemName"] = stk.ItemName.ItemName;
                            r["WorkerId"] = stk.WorkerName.ID ;
                            r["WorkerName"] = stk.WorkerName .Name ;
                            r["ThumbNail"] = stk.ImageMemory;
                            r["Qty"] = stk.Qty;
                            r["IType"] = stk.ItemType;                                                      
                            dt1.Rows.Add(r);
                            r = null;                                                    
                    }
                    ReportDocument report = new ReportDocument();
                    string path = Path.GetDirectoryName(Application.ExecutablePath);
                    report.Load(path + "\\Reports\\ViewPictures.rpt");                    
                    report.SetDataSource(ds);                   
                    crystalReportViewer1.ReportSource = report;                  
                    dt1 = new DataTable();
                    ds = new DataSet();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DisposeAllControl()
        {
            GC.Collect();
        }
    }
}
