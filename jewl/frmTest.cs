using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using DAL;
using BusinesEntities;
using System.IO;

namespace jewl
{
    public partial class frmTest : Form
    {
        StockDAL stkDAL = new StockDAL();
        Stock stk = new Stock();
        public string[] strArray = new string[12];
        public frmTest()
        {
            InitializeComponent();
        }

        private void frmTest_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            //BarCodeDataSet ds = new BarCodeDataSet();
            // DataSet ds = new DataSet();

            DataTable dt1 = new DataTable();
            dt1 = ds.Tables.Add("Stock");
            dt1.Columns.Add("TagNo", typeof(System.String));
            dt1.Columns.Add("StockId", typeof(System.Int32));
            dt1.Columns.Add("BarCode", typeof(System.String));
            dt1.Columns.Add("TotalWeight", typeof(System.Decimal));
            dt1.Columns.Add("Weight", typeof(System.Decimal));
            dt1.Columns.Add("Pieces", typeof(System.Int32));
            dt1.Columns.Add("Karat", typeof(System.String));
            dt1.Columns.Add("StWeight", typeof(System.Decimal));
            dt1.Columns.Add("StWeight1", typeof(System.Decimal));
            dt1.Columns.Add("BdW", typeof(System.Decimal));
            dt1.Columns.Add("DrW", typeof(System.Decimal));
            dt1.Columns.Add("WasteInGm", typeof(System.Decimal));
            dt1.Columns.Add("SQty", typeof(System.Decimal));
            dt1.Columns.Add("TotalLaker", typeof(System.Decimal));
            dt1.Columns.Add("TotalMaking", typeof(System.Decimal));
            dt1.Columns.Add("TotalPrice", typeof(System.Decimal));
            dt1.Columns.Add("IndexNo", typeof(System.Int32));
            dt1.Columns.Add("IType", typeof(System.String));
            dt1.Columns.Add("StockId1", typeof(System.Int32));
            dt1.Columns.Add("IndexNo1", typeof(System.Int32));
            dt1.Columns.Add("IType1", typeof(System.String));
            dt1.Columns.Add("TagNo1", typeof(System.String));
            dt1.Columns.Add("BarCode1", typeof(System.String));
            dt1.Columns.Add("ItemName1", typeof(System.String));
            dt1.Columns.Add("TotalWeight1", typeof(System.Decimal));
            dt1.Columns.Add("Karat1", typeof(System.String));
            dt1.Columns.Add("Weight1", typeof(System.Decimal));
            dt1.Columns.Add("WasteInGm1", typeof(System.Decimal));
            dt1.Columns.Add("TotalLaker1", typeof(System.Decimal));
            dt1.Columns.Add("BdW1", typeof(System.Decimal));
            dt1.Columns.Add("DrW1", typeof(System.Decimal));
            dt1.Columns.Add("SQty1", typeof(System.Decimal));
            dt1.Columns.Add("TotalPrice1", typeof(System.Decimal));
            dt1.Columns.Add("Pieces1", typeof(System.Decimal));
            dt1.Columns.Add("TotalMaking1", typeof(System.Decimal));
            dt1.Columns.Add("ItemName", typeof(System.String));
            dt1.Columns.Add("Qty", typeof(System.Int32));
            dt1.Columns.Add("Qty1", typeof(System.Int32));
            dt1.Columns.Add("Price", typeof(System.Decimal));
            dt1.Columns.Add("Price1", typeof(System.Decimal));
            //string str = "BG0003";
            //string str1 = "BG0005";
             List<string> str = new List<string>();
            for (int j = 0; j < strArray.Length; j++)
            {
                if (strArray[j] != "")
                    str.Add(strArray[j]);
            }
             DataRow r = null;
             for (int i = 0; i < str.Count; i++)
             {
                 if (r == null)
                     r = dt1.NewRow();
                 stk = stkDAL.GetStockBySockTagNo(str[i]);
                 r["TagNo"] = stk.TagNo.ToString();
                 r["TotalWeight"] = stk.TotalWeight;
                 r["Weight"] = stk.TotalWeight;
                 r["Pieces"] = stk.Pieces;
                 r["Karat"] = stk.Karrat;
                 r["WasteInGm"] = stk.WasteInGm;
                 //r["SQty"] = dt.Rows[i]["SQty"];
                 r["TotalLaker"] = stk.TotalLaker;
                 r["TotalMaking"] = stk.TotalMaking;
               
                 r["TotalPrice"] = stk.TotalPrice;
                 r["WasteInGm"] = stk.WasteInGm;
                 r["ItemName"] = stk.ItemName;
                 r["Qty"] = stk.Qty;
                 r["IType"] = stk.ItemType;
                 
                     r["BdW"] = 0;
                     r["StWeight"] = 0;
                     r["DrW"] = 0;
                     if (stk.StoneList != null)
                     {
                         for (int k = 0; k < stk.StoneList.Count; k++)
                         {
                             string strng = stk.StoneList[i].StoneName.ToString();
                             if (strng.Equals("Stones"))
                             {
                                 r["StWeight"] = stk.StoneList[i].StoneWeight;
                                 //r["BdW"] = 0;
                                 //r["DrW"] = 0;
                             }
                             else if (strng.Equals("Beeds"))
                             {
                                 r["BdW"] = stk.StoneList[i].StoneWeight;
                                 //r["StWeight"] = 0;
                                 //r["DrW"] = 0;
                             }
                             else if (strng.Equals("Drops"))
                             {
                                 r["DrW"] = stk.StoneList[i].StoneWeight;
                                 //r["StWeight"] = 0;
                                 //r["BdW"] = 0;
                             }
                         }
                     }

                 i = i + 1;
                 if (i < str.Count)
                 {
                     stk = stkDAL.GetStockBySockTagNo(str[i]);
                     r["TagNo1"] = stk.TagNo.ToString();
                     r["TotalWeight1"] = stk.TotalWeight;
                     r["Weight1"] = stk.NetWeight;
                     r["Pieces1"] = stk.Pieces;
                     r["Karat1"] = stk.Karrat;
                     r["WasteInGm1"] = stk.WasteInGm;
                     //r["SQty"] = dt.Rows[i]["SQty"];
                     r["TotalLaker1"] = stk.TotalLaker;
                     r["TotalMaking1"] = stk.TotalMaking;
                     r["TotalPrice1"] = stk.TotalPrice;
                     r["ItemName1"] = stk.ItemName;
                     r["Qty1"] = stk.Qty;
                     r["IType1"] = stk.ItemType;
                     r["WasteInGm1"] = stk.WasteInGm;
                     
                         r["BdW1"] = 0;
                         r["StWeight1"] = 0;
                         r["DrW1"] = 0;
                         if (stk.StoneList != null)
                         {
                             for (int k = 0; k < stk.StoneList.Count; k++)
                             {
                                 string strng = stk.StoneList[i].StoneName.ToString();
                                 if (strng.Equals("Stones"))
                                 {
                                     r["StWeight1"] = stk.StoneList[i].StoneWeight;
                                     //r["BdW"] = 0;
                                     //r["DrW"] = 0;
                                 }
                                 else if (strng.Equals("Beeds"))
                                 {
                                     r["BdW1"] = stk.StoneList[i].StoneWeight;
                                     //r["StWeight"] = 0;
                                     //r["DrW"] = 0;
                                 }
                                 else if (strng.Equals("Drops"))
                                 {
                                     r["DrW1"] = stk.StoneList[i].StoneWeight;
                                     //r["StWeight"] = 0;
                                     //r["BdW"] = 0;
                                 }
                             }
                         }
                 }
                 dt1.Rows.Add(r);
                 r = null;
             }
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = path + "\\Reports\\ManaualBarCode.rpt";
            ReportDocument report = new ReportDocument();
            report.Load(reportPath);
            //report.Database.Tables["BarCodeRpt"].SetDataSource(ds.Tables[0]);
            report.SetDataSource(ds.Tables["Stock"]);
            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.RefreshReport();
        }
    }
}
