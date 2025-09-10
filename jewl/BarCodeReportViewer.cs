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
    public partial class BarCodeReportViewer : Form
    {
        StonesDAL stDAL = new StonesDAL();
        StockDAL stkDAL = new StockDAL();
        ReportDocument report;
        public int isPage, RptNo = 0;
        public int ReportNo = 0, id;
        public bool dFlag = true;
        public string tagNo, tagFrom;
        public string tagTo;
        Stock stk = new Stock();
        List<Stock> stkl = new List<Stock>();
        public string[] strArray = new string[12];

        public BarCodeReportViewer()
        {
            InitializeComponent();
        }

        private void frmGoldBarCode_Load(object sender, EventArgs e)
        {
            string query = "";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd =new SqlCommand();
            
            if (isPage == 0)
            {
                if (ReportNo == 1)
                    query = "GoldBarcodeByIndexNo";
                else if (ReportNo == 2)
                    query = "GoldBarcodeByIndexNoD";
                else if (ReportNo == 3)
                    query = "GoldBarcodeByIndexNoS";
                else if (ReportNo == 4)
                    query = "GoldBarcodeByIndexNoPladium";
                else if (ReportNo == 5)
                    query = "GoldBarcodeByIndexNoPlatinum";
            }
            if (isPage == 1)
            {
                if (ReportNo == 1)
                    query = "GoldBarCode";
                else if (ReportNo == 2)
                    query = "GoldBarCodeD";
                else if (ReportNo == 3)
                    query = "GoldBarCodeS";
                else if (ReportNo == 4)
                    query = "GoldBarCodePladium";
                else if (ReportNo == 5)
                    query = "GoldBarCodePlatinum";
            }
            if (isPage == 2)
            {
                if (ReportNo == 1)
                    query = "GoldBarCodeByTagNo";
                else if (ReportNo == 2)
                    query = "GoldBarCodeByTagNoD";
                else if (ReportNo == 3)
                    query = "GoldBarCodeByTagNoS";
                else if (ReportNo == 4)
                    query = "GoldBarCodeByTagNoPladium";
                else if (ReportNo == 5)
                    query = "GoldBarCodeByTagNoPlatinum";
                cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar).Value = this.tagNo;
            }
            if (isPage == 3)
            {
                if (ReportNo == 1)
                    query = "GoldBarCodeByTagRange";
                else if (ReportNo == 2)
                    query = "GoldBarCodeByTagRangeD";
                else if (ReportNo == 3)
                    query = "GoldBarCodeByTagRangeS";
                else if (ReportNo == 4)
                    query = "GoldBarCodeByTagRangePladium";
                else if (ReportNo == 5)
                    query = "GoldBarCodeByTagRangePlatinum";
                cmd.Parameters.Add("@TagFrom", SqlDbType.NVarChar).Value = this.tagFrom;
                cmd.Parameters.Add("@TagTo", SqlDbType.NVarChar).Value = this.tagTo;
            }
            if (isPage == 4)
            {
                List<string> str = new List<string>();
                for (int j = 0; j < strArray.Length; j++)
                {
                    if (strArray[j] != "")
                    {
                        if (ReportNo == 1)
                        {
                            stk = stkDAL.GetStockBySockTagNo(strArray[j]);
                            if (stk == null)
                            { }
                            else
                                if (stk.ItemType.Equals(ItemType.Gold))
                                {
                                    stkl.Add(stk);
                                    str.Add(strArray[j]);
                                }
                        }
                        else if (ReportNo == 2)
                        {
                            stk = stkDAL.GetStockBySockTagNo(strArray[j]);
                            if (stk != null)
                                if (stk.ItemType.Equals(ItemType.Diamond))
                                {
                                    stkl.Add(stk);
                                    str.Add(strArray[j]);
                                }
                        }
                        else if (ReportNo == 3)
                        {
                            stk = stkDAL.GetStockBySockTagNo(strArray[j]);
                            if (stk != null)
                                if (stk.ItemType.Equals(ItemType.Silver))
                                {
                                    stkl.Add(stk);
                                    str.Add(strArray[j]);
                                }
                        }
                        else if (ReportNo == 4)
                        {
                            stk = stkDAL.GetStockBySockTagNo(strArray[j]);
                            if (stk != null)
                                if (stk.ItemType.Equals(ItemType.Pladium))
                                {
                                    stkl.Add(stk);
                                    str.Add(strArray[j]);
                                }
                        }
                        else if (ReportNo == 5)
                        {
                            stk = stkDAL.GetStockBySockTagNo(strArray[j]);
                            if (stk != null)
                                if (stk.ItemType.Equals(ItemType.Platinum))
                                {
                                    stkl.Add(stk);
                                    str.Add(strArray[j]);
                                }
                        }

                    }
                }
            }

            #region Com
            DataTable dt = new DataTable();
            if (isPage == 0)
            {
                cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                    this.Close();
            }
            if (isPage == 1)
            {
                cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                    this.Close();
            }
            if (isPage == 2)
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar).Value = this.tagNo;
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                    this.Close();
            }
            if (isPage == 3)
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add("@TagFrom", SqlDbType.NVarChar).Value = this.tagFrom;
                cmd.Parameters.Add("@TagTo", SqlDbType.NVarChar).Value = this.tagTo;
                cmd.CommandType = CommandType.StoredProcedure;
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                    this.Close();
            }
            if (isPage == 4)
            {
                if (stkl.Count == 0)
                    this.Close();
            }
            DataSet ds = new DataSet();
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
            dt1.Columns.Add("DesNo", typeof(System.String));
            dt1.Columns.Add("DesNo1", typeof(System.String));
            dt1.Columns.Add("StonePrice", typeof(System.Decimal));
            dt1.Columns.Add("StonePrice1", typeof(System.Decimal));
            dt1.Columns.Add("BeedsPrice", typeof(System.Decimal));
            dt1.Columns.Add("BeedsPrice1", typeof(System.Decimal));
            dt1.Columns.Add("DiamondPrice", typeof(System.Decimal));
            dt1.Columns.Add("DiamondPrice1", typeof(System.Decimal));
            dt1.Columns.Add("DiamondPrice2", typeof(System.Decimal));
            dt1.Columns.Add("DiamondPrice3", typeof(System.Decimal));
            dt1.Columns.Add("Description", typeof(System.String));
            dt1.Columns.Add("Description1", typeof(System.String));
            dt1.Columns.Add("D1cut", typeof(System.String));
            dt1.Columns.Add("D2cut", typeof(System.String));
            dt1.Columns.Add("D3cut", typeof(System.String));
            dt1.Columns.Add("D4cut", typeof(System.String));
            dt1.Columns.Add("D5cut", typeof(System.String));
            dt1.Columns.Add("D6cut", typeof(System.String));
            dt1.Columns.Add("D7cut", typeof(System.String));
            dt1.Columns.Add("D8cut", typeof(System.String));
            dt1.Columns.Add("D9cut", typeof(System.String));
            dt1.Columns.Add("D10cut", typeof(System.String));
            dt1.Columns.Add("D1cut1", typeof(System.String));
            dt1.Columns.Add("D2cut1", typeof(System.String));
            dt1.Columns.Add("D3cut1", typeof(System.String));
            dt1.Columns.Add("D4cut1", typeof(System.String));
            dt1.Columns.Add("D5cut1", typeof(System.String));
            dt1.Columns.Add("D6cut1", typeof(System.String));
            dt1.Columns.Add("D7cut1", typeof(System.String));
            dt1.Columns.Add("D8cut1", typeof(System.String));
            dt1.Columns.Add("D9cut1", typeof(System.String));
            dt1.Columns.Add("D10cut1", typeof(System.String));
            dt1.Columns.Add("D1clearity", typeof(System.String));
            dt1.Columns.Add("D2clearity", typeof(System.String));
            dt1.Columns.Add("D3clearity", typeof(System.String));
            dt1.Columns.Add("D4clearity", typeof(System.String));
            dt1.Columns.Add("D5clearity", typeof(System.String));
            dt1.Columns.Add("D6clearity", typeof(System.String));
            dt1.Columns.Add("D7clearity", typeof(System.String));
            dt1.Columns.Add("D8clearity", typeof(System.String));
            dt1.Columns.Add("D9clearity", typeof(System.String));
            dt1.Columns.Add("D10clearity", typeof(System.String));
            dt1.Columns.Add("D1clearity1", typeof(System.String));
            dt1.Columns.Add("D2clearity1", typeof(System.String));
            dt1.Columns.Add("D3clearity1", typeof(System.String));
            dt1.Columns.Add("D4clearity1", typeof(System.String));
            dt1.Columns.Add("D5clearity1", typeof(System.String));
            dt1.Columns.Add("D6clearity1", typeof(System.String));
            dt1.Columns.Add("D7clearity1", typeof(System.String));
            dt1.Columns.Add("D8clearity1", typeof(System.String));
            dt1.Columns.Add("D9clearity1", typeof(System.String));
            dt1.Columns.Add("D10clearity1", typeof(System.String));
            dt1.Columns.Add("D1color", typeof(System.String));
            dt1.Columns.Add("D2color", typeof(System.String));
            dt1.Columns.Add("D3color", typeof(System.String));
            dt1.Columns.Add("D4color", typeof(System.String));
            dt1.Columns.Add("D5color", typeof(System.String));
            dt1.Columns.Add("D6color", typeof(System.String));
            dt1.Columns.Add("D7color", typeof(System.String));
            dt1.Columns.Add("D8color", typeof(System.String));
            dt1.Columns.Add("D9color", typeof(System.String));
            dt1.Columns.Add("D10color", typeof(System.String));
            dt1.Columns.Add("D1color1", typeof(System.String));
            dt1.Columns.Add("D2color1", typeof(System.String));
            dt1.Columns.Add("D3color1", typeof(System.String));
            dt1.Columns.Add("D4color1", typeof(System.String));
            dt1.Columns.Add("D5color1", typeof(System.String));
            dt1.Columns.Add("D6color1", typeof(System.String));
            dt1.Columns.Add("D7color1", typeof(System.String));
            dt1.Columns.Add("D8color1", typeof(System.String));
            dt1.Columns.Add("D9color1", typeof(System.String));
            dt1.Columns.Add("D10color1", typeof(System.String));
            dt1.Columns.Add("D1rW", typeof(System.Decimal));
            dt1.Columns.Add("D2rW", typeof(System.Decimal));
            dt1.Columns.Add("D3rW", typeof(System.Decimal));
            dt1.Columns.Add("D4rW", typeof(System.Decimal));
            dt1.Columns.Add("D5rW", typeof(System.Decimal));
            dt1.Columns.Add("D6rW", typeof(System.Decimal));
            dt1.Columns.Add("D7rW", typeof(System.Decimal));
            dt1.Columns.Add("D8rW", typeof(System.Decimal));
            dt1.Columns.Add("D9rW", typeof(System.Decimal));
            dt1.Columns.Add("D10rW", typeof(System.Decimal));
            dt1.Columns.Add("D1rW1", typeof(System.Decimal));
            dt1.Columns.Add("D2rW1", typeof(System.Decimal));
            dt1.Columns.Add("D3rW1", typeof(System.Decimal));
            dt1.Columns.Add("D4rW1", typeof(System.Decimal));
            dt1.Columns.Add("D5rW1", typeof(System.Decimal));
            dt1.Columns.Add("D6rW1", typeof(System.Decimal));
            dt1.Columns.Add("D7rW1", typeof(System.Decimal));
            dt1.Columns.Add("D8rW1", typeof(System.Decimal));
            dt1.Columns.Add("D9rW1", typeof(System.Decimal));
            dt1.Columns.Add("D10rW1", typeof(System.Decimal));
            dt1.Columns.Add("S1Qty", typeof(System.Int32));
            dt1.Columns.Add("S2Qty", typeof(System.Int32));
            dt1.Columns.Add("S3Qty", typeof(System.Int32));
            dt1.Columns.Add("S4Qty", typeof(System.Int32));
            dt1.Columns.Add("S5Qty", typeof(System.Int32));
            dt1.Columns.Add("S6Qty", typeof(System.Int32));
            dt1.Columns.Add("S7Qty", typeof(System.Int32));
            dt1.Columns.Add("S8Qty", typeof(System.Int32));
            dt1.Columns.Add("S9Qty", typeof(System.Int32));
            dt1.Columns.Add("S10Qty", typeof(System.Int32));
            dt1.Columns.Add("S1Qty1", typeof(System.Int32));
            dt1.Columns.Add("S2Qty1", typeof(System.Int32));
            dt1.Columns.Add("S3Qty1", typeof(System.Int32));
            dt1.Columns.Add("S4Qty1", typeof(System.Int32));
            dt1.Columns.Add("S5Qty1", typeof(System.Int32));
            dt1.Columns.Add("S6Qty1", typeof(System.Int32));
            dt1.Columns.Add("S7Qty1", typeof(System.Int32));
            dt1.Columns.Add("S8Qty1", typeof(System.Int32));
            dt1.Columns.Add("S9Qty1", typeof(System.Int32));
            dt1.Columns.Add("S10Qty1", typeof(System.Int32));
            dt1.Columns.Add("St1Qty", typeof(System.Int32));
            dt1.Columns.Add("St2Qty", typeof(System.Int32));
            dt1.Columns.Add("St3Qty", typeof(System.Int32));
            dt1.Columns.Add("St1Qty1", typeof(System.Int32));
            dt1.Columns.Add("St2Qty1", typeof(System.Int32));
            dt1.Columns.Add("St3Qty1", typeof(System.Int32));

            dt1.Columns.Add("B1Qty", typeof(System.Int32));
            dt1.Columns.Add("B2Qty", typeof(System.Int32));
            dt1.Columns.Add("B3Qty", typeof(System.Int32));
            dt1.Columns.Add("B1Qty1", typeof(System.Int32));
            dt1.Columns.Add("B2Qty1", typeof(System.Int32));
            dt1.Columns.Add("B3Qty1", typeof(System.Int32));

            dt1.Columns.Add("R1ate", typeof(System.Decimal));
            dt1.Columns.Add("R2ate", typeof(System.Decimal));
            dt1.Columns.Add("R3ate", typeof(System.Decimal));
            dt1.Columns.Add("R4ate", typeof(System.Decimal));
            dt1.Columns.Add("R5ate", typeof(System.Decimal));
            dt1.Columns.Add("R6ate", typeof(System.Decimal));
            dt1.Columns.Add("R7ate", typeof(System.Decimal));
            dt1.Columns.Add("R8ate", typeof(System.Decimal));
            dt1.Columns.Add("R9ate", typeof(System.Decimal));
            dt1.Columns.Add("R10ate", typeof(System.Decimal));
            dt1.Columns.Add("R1ate1", typeof(System.Decimal));
            dt1.Columns.Add("R2ate1", typeof(System.Decimal));
            dt1.Columns.Add("R3ate1", typeof(System.Decimal));
            dt1.Columns.Add("R4ate1", typeof(System.Decimal));
            dt1.Columns.Add("R5ate1", typeof(System.Decimal));
            dt1.Columns.Add("R6ate1", typeof(System.Decimal));
            dt1.Columns.Add("R7ate1", typeof(System.Decimal));
            dt1.Columns.Add("R8ate1", typeof(System.Decimal));
            dt1.Columns.Add("R9ate1", typeof(System.Decimal));
            dt1.Columns.Add("R10ate1", typeof(System.Decimal));
            dt1.Columns.Add("DQty", typeof(System.Int32));
            dt1.Columns.Add("DQty1", typeof(System.Int32));
            dt1.Columns.Add("SilverSalePrice", typeof(System.Decimal));
            dt1.Columns.Add("SilverSalePrice1", typeof(System.Decimal));
            dt1.Columns.Add("TagNo2", typeof(System.String));
            dt1.Columns.Add("StockId2", typeof(System.Int32));
            dt1.Columns.Add("BarCode2", typeof(System.String));
            dt1.Columns.Add("TotalWeight2", typeof(System.Decimal));
            dt1.Columns.Add("Weight2", typeof(System.Decimal));
            dt1.Columns.Add("Pieces2", typeof(System.Int32));
            dt1.Columns.Add("Karat2", typeof(System.String));
            dt1.Columns.Add("StWeight2", typeof(System.Decimal));
            dt1.Columns.Add("StWeight3", typeof(System.Decimal));
            dt1.Columns.Add("BdW2", typeof(System.Decimal));
            dt1.Columns.Add("DrW2", typeof(System.Decimal));
            dt1.Columns.Add("WasteInGm2", typeof(System.Decimal));
            dt1.Columns.Add("SQty2", typeof(System.Decimal));
            dt1.Columns.Add("TotalLaker2", typeof(System.Decimal));
            dt1.Columns.Add("TotalMaking2", typeof(System.Decimal));
            dt1.Columns.Add("TotalPrice2", typeof(System.Decimal));
            dt1.Columns.Add("IndexNo2", typeof(System.Int32));
            dt1.Columns.Add("IType2", typeof(System.String));
            dt1.Columns.Add("StockId3", typeof(System.Int32));
            dt1.Columns.Add("IndexNo3", typeof(System.Int32));
            dt1.Columns.Add("IType3", typeof(System.String));
            dt1.Columns.Add("TagNo3", typeof(System.String));
            dt1.Columns.Add("BarCode3", typeof(System.String));
            dt1.Columns.Add("ItemName3", typeof(System.String));
            dt1.Columns.Add("TotalWeight3", typeof(System.Decimal));
            dt1.Columns.Add("Karat3", typeof(System.String));
            dt1.Columns.Add("Weight3", typeof(System.Decimal));
            dt1.Columns.Add("WasteInGm3", typeof(System.Decimal));
            dt1.Columns.Add("TotalLaker3", typeof(System.Decimal));
            dt1.Columns.Add("BdW3", typeof(System.Decimal));
            dt1.Columns.Add("DrW3", typeof(System.Decimal));
            dt1.Columns.Add("SQty3", typeof(System.Decimal));
            dt1.Columns.Add("TotalPrice3", typeof(System.Decimal));
            dt1.Columns.Add("Pieces3", typeof(System.Decimal));
            dt1.Columns.Add("TotalMaking3", typeof(System.Decimal));
            dt1.Columns.Add("ItemName2", typeof(System.String));
            dt1.Columns.Add("Qty2", typeof(System.Int32));
            dt1.Columns.Add("Qty3", typeof(System.Int32));
            dt1.Columns.Add("Price2", typeof(System.Decimal));
            dt1.Columns.Add("Price3", typeof(System.Decimal));
            dt1.Columns.Add("StonePrice2", typeof(System.Decimal));
            dt1.Columns.Add("StonePrice3", typeof(System.Decimal));
            dt1.Columns.Add("BeedsPrice2", typeof(System.Decimal));
            dt1.Columns.Add("BeedsPrice3", typeof(System.Decimal));
            dt1.Columns.Add("Description2", typeof(System.String));
            dt1.Columns.Add("Description3", typeof(System.String));
            dt1.Columns.Add("D1cut2", typeof(System.String));
            dt1.Columns.Add("D1cut3", typeof(System.String));
            dt1.Columns.Add("D2cut2", typeof(System.String));
            dt1.Columns.Add("D2cut3", typeof(System.String));
            dt1.Columns.Add("D3cut2", typeof(System.String));
            dt1.Columns.Add("D3cut3", typeof(System.String));
            dt1.Columns.Add("D1clearity2", typeof(System.String));
            dt1.Columns.Add("D1clearity3", typeof(System.String));
            dt1.Columns.Add("D2clearity2", typeof(System.String));
            dt1.Columns.Add("D2clearity3", typeof(System.String));
            dt1.Columns.Add("D3clearity2", typeof(System.String));
            dt1.Columns.Add("D3clearity3", typeof(System.String));
            dt1.Columns.Add("D1color2", typeof(System.String));
            dt1.Columns.Add("D1color3", typeof(System.String));
            dt1.Columns.Add("D2color2", typeof(System.String));
            dt1.Columns.Add("D2color3", typeof(System.String));
            dt1.Columns.Add("D3color2", typeof(System.String));
            dt1.Columns.Add("D3color3", typeof(System.String));
            dt1.Columns.Add("D1rW2", typeof(System.Decimal));
            dt1.Columns.Add("D1rW3", typeof(System.Decimal));
            dt1.Columns.Add("D2rW2", typeof(System.Decimal));
            dt1.Columns.Add("D2rW3", typeof(System.Decimal));
            dt1.Columns.Add("D3rW2", typeof(System.Decimal));
            dt1.Columns.Add("D3rW3", typeof(System.Decimal));
            dt1.Columns.Add("S1Qty2", typeof(System.Int32));
            dt1.Columns.Add("S1Qty3", typeof(System.Int32));
            dt1.Columns.Add("S2Qty2", typeof(System.Int32));
            dt1.Columns.Add("S2Qty3", typeof(System.Int32));
            dt1.Columns.Add("S3Qty2", typeof(System.Int32));
            dt1.Columns.Add("S3Qty3", typeof(System.Int32));
            dt1.Columns.Add("R1ate2", typeof(System.Decimal));
            dt1.Columns.Add("R1ate3", typeof(System.Decimal));
            dt1.Columns.Add("R2ate2", typeof(System.Decimal));
            dt1.Columns.Add("R2ate3", typeof(System.Decimal));
            dt1.Columns.Add("R3ate2", typeof(System.Decimal));
            dt1.Columns.Add("R3ate3", typeof(System.Decimal));
            dt1.Columns.Add("DQty2", typeof(System.Int32));
            dt1.Columns.Add("DQty3", typeof(System.Int32));
            dt1.Columns.Add("SilverSalePrice2", typeof(System.Decimal));
            dt1.Columns.Add("SilverSalePrice3", typeof(System.Decimal));
            dt1.Columns.Add("PWeight", typeof(System.Decimal));
            dt1.Columns.Add("PWeight1", typeof(System.Decimal));
            dt1.Columns.Add("PWeight2", typeof(System.Decimal));
            dt1.Columns.Add("PWeight3", typeof(System.Decimal));
            dt1.Columns.Add("Kaat", typeof(System.Decimal));
            dt1.Columns.Add("Kaat1", typeof(System.Decimal));
            dt1.Columns.Add("Kaat2", typeof(System.Decimal));
            dt1.Columns.Add("Kaat3", typeof(System.Decimal));
            dt1.Columns.Add("DesignNo", typeof(System.String));
            dt1.Columns.Add("DesignNo1", typeof(System.String));
            dt1.Columns.Add("DesignNo2", typeof(System.String));
            dt1.Columns.Add("DesignNo3", typeof(System.String));
            dt1.Columns.Add("RateA", typeof(System.String));
            dt1.Columns.Add("RateD", typeof(System.String));
            dt1.Columns.Add("PriceA", typeof(System.String));
            dt1.Columns.Add("PriceD", typeof(System.String));
            dt1.Columns.Add("RateA1", typeof(System.String));
            dt1.Columns.Add("RateD1", typeof(System.String));
            dt1.Columns.Add("PriceA1", typeof(System.String));
            dt1.Columns.Add("PriceD1", typeof(System.String));
            dt1.Columns.Add("RateA2", typeof(System.String));
            dt1.Columns.Add("RateD2", typeof(System.String));
            dt1.Columns.Add("PriceA2", typeof(System.String));
            dt1.Columns.Add("PriceD2", typeof(System.String));
            dt1.Columns.Add("RateA3", typeof(System.String));
            dt1.Columns.Add("RateD3", typeof(System.String));
            dt1.Columns.Add("PriceA3", typeof(System.String));
            dt1.Columns.Add("PriceD3", typeof(System.String));
            dt1.Columns.Add("WorkerName", typeof(System.String));
            dt1.Columns.Add("WorkerName1", typeof(System.String));
            dt1.Columns.Add("BeedsWeight", typeof(System.Decimal));
            dt1.Columns.Add("BeedsWeight1", typeof(System.Decimal));
            dt1.Columns.Add("BeedsWeight2", typeof(System.Decimal));
            dt1.Columns.Add("Beeds1Weight", typeof(System.Decimal));
            dt1.Columns.Add("Beeds1Weight1", typeof(System.Decimal));
            dt1.Columns.Add("Beeds1Weight2", typeof(System.Decimal));
            dt1.Columns.Add("BeedsP", typeof(System.Decimal));
            dt1.Columns.Add("BeedsP1", typeof(System.Decimal));
            dt1.Columns.Add("BeedsP2", typeof(System.Decimal));
            dt1.Columns.Add("Beeds1P", typeof(System.Decimal));
            dt1.Columns.Add("Beeds1P1", typeof(System.Decimal));
            dt1.Columns.Add("Beeds1P2", typeof(System.Decimal));
            dt1.Columns.Add("DiamondP", typeof(System.Decimal));
            dt1.Columns.Add("DiamondP1", typeof(System.Decimal));
            dt1.Columns.Add("DiamondP2", typeof(System.Decimal));
            dt1.Columns.Add("DiamondP3", typeof(System.Decimal));
            dt1.Columns.Add("DiamondP4", typeof(System.Decimal));
            dt1.Columns.Add("DiamondP5", typeof(System.Decimal));
            dt1.Columns.Add("DiamondP6", typeof(System.Decimal));
            dt1.Columns.Add("DiamondP7", typeof(System.Decimal));
            dt1.Columns.Add("DiamondP8", typeof(System.Decimal));
            dt1.Columns.Add("DiamondP9", typeof(System.Decimal));
            dt1.Columns.Add("Diamond1P", typeof(System.Decimal));
            dt1.Columns.Add("Diamond1P1", typeof(System.Decimal));
            dt1.Columns.Add("Diamond1P2", typeof(System.Decimal));
            dt1.Columns.Add("Diamond1P3", typeof(System.Decimal));
            dt1.Columns.Add("Diamond1P4", typeof(System.Decimal));
            dt1.Columns.Add("Diamond1P5", typeof(System.Decimal));
            dt1.Columns.Add("Diamond1P6", typeof(System.Decimal));
            dt1.Columns.Add("Diamond1P7", typeof(System.Decimal));
            dt1.Columns.Add("Diamond1P8", typeof(System.Decimal));
            dt1.Columns.Add("Diamond1P9", typeof(System.Decimal));
            dt1.Columns.Add("StoneP", typeof(System.Decimal));
            dt1.Columns.Add("StoneP1", typeof(System.Decimal));
            dt1.Columns.Add("StoneP2", typeof(System.Decimal));
            dt1.Columns.Add("Stone1P", typeof(System.Decimal));
            dt1.Columns.Add("Stone1P1", typeof(System.Decimal));
            dt1.Columns.Add("Stone1P2", typeof(System.Decimal));
            dt1.Columns.Add("StoneWeight", typeof(System.Decimal));
            dt1.Columns.Add("StoneWeight1", typeof(System.Decimal));
            dt1.Columns.Add("StoneWeight2", typeof(System.Decimal));
            dt1.Columns.Add("Stone1Weight", typeof(System.Decimal));
            dt1.Columns.Add("Stone1Weight1", typeof(System.Decimal));
            dt1.Columns.Add("Stone1Weight2", typeof(System.Decimal));

            dt1.Columns.Add("StoneName", typeof(System.String));
            dt1.Columns.Add("StoneName1", typeof(System.String));
            dt1.Columns.Add("Stone2Name", typeof(System.String));
            dt1.Columns.Add("StoneName2", typeof(System.String));
            dt1.Columns.Add("Stone3Name", typeof(System.String));
            dt1.Columns.Add("StoneName3", typeof(System.String));



            dt1.Columns.Add("D1Qty", typeof(System.Int32));
            dt1.Columns.Add("D2Qty", typeof(System.Int32));
            dt1.Columns.Add("D3Qty", typeof(System.Int32));
            dt1.Columns.Add("D1Qty1", typeof(System.Int32));
            dt1.Columns.Add("D2Qty1", typeof(System.Int32));
            dt1.Columns.Add("D3Qty1", typeof(System.Int32));



            dt1.Columns.Add("DiamondName", typeof(System.String));
            dt1.Columns.Add("DiamondName1", typeof(System.String));
            dt1.Columns.Add("DiamondName2", typeof(System.String));
            dt1.Columns.Add("DiamondName3", typeof(System.String));
            dt1.Columns.Add("Diamond1Name", typeof(System.String));
            dt1.Columns.Add("Diamond1Name1", typeof(System.String));
            dt1.Columns.Add("Diamond1Name2", typeof(System.String));
            dt1.Columns.Add("Diamond1Name3", typeof(System.String));


            #region isPage0,1,2,3
            if (isPage == 0 || isPage == 1 || isPage == 2 || isPage == 3)
            {
                #region Barcode
                DataRow r = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (r == null)
                        r = dt1.NewRow();
                    r["TagNo"] = dt.Rows[i]["TagNo"];
                    r["StockId"] = dt.Rows[i]["StockId"];
                    r["BarCode"] = dt.Rows[i]["BarCode"];
                    r["TotalWeight"] = dt.Rows[i]["TotalWeight"];
                    r["Weight"] = dt.Rows[i]["NetWeight"];
                    r["Pieces"] = dt.Rows[i]["Pieces"];
                    r["Karat"] = dt.Rows[i]["Karat"];
                    r["Description"] = dt.Rows[i]["Description"];
                    r["BdW"] = 0;
                    r["StWeight"] = 0;
                    r["DrW"] = 0;
                    r["BeedsPrice"] = 0;
                    r["StonePrice"] = 0;
                    r["SilverSalePrice"] = dt.Rows[i]["SilverSalePrice"];
                    r["PWeight"] = dt.Rows[i]["PWeight"];
                    r["Kaat"] = dt.Rows[i]["Kaat"];
                    r["DesignNo"] = dt.Rows[i]["DesignNo"];
                    r["RateA"] = dt.Rows[i]["RateA"];
                    r["RateD"] = dt.Rows[i]["RateD"];
                    r["PriceA"] = dt.Rows[i]["PriceA"];
                    r["PriceD"] = dt.Rows[i]["PriceD"];
                    r["WorkerName"] = dt.Rows[i]["WorkerName"];
                    r["DesNo"] = dt.Rows[i]["DesNo"];
                    List<Stones> lst = stDAL.GetAllStonesDetail(dt.Rows[i]["TagNo"].ToString());

                    if (lst != null)
                    {
                        Decimal price = 0;
                        Decimal BeedsPrice = 0;
                        Decimal DiamondPrice = 0;
                        Decimal StoneWeight = 0;
                        Decimal BeedsWeight = 0;
                        Decimal DiamonWeight = 0;
                        int DQty = 0;
                        int s = 0;
                        int b = 0;
                        int d = 0;
                        for (int m = 0; m < lst.Count; m++)
                        {
                            string str = lst[m].StoneTypeName.ToString();
                            if (str.Equals("Stones"))
                            {
                                StoneWeight = StoneWeight + Convert.ToDecimal(lst[m].StoneWeight);
                                price = Convert.ToDecimal(lst[m].StonePrice);
                                s = s + 1;
                                if (s == 1)
                                {
                                    r["StoneP"] = Convert.ToDecimal(lst[m].Price);
                                    r["StoneWeight"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["St1Qty"] = Convert.ToDecimal(lst[m].Qty);
                                    r["StoneName"]= lst[m].StoneName.ToString();
                                }
                                else if (s == 2)
                                {
                                    r["StoneP1"] = Convert.ToDecimal(lst[m].Price);
                                    r["StoneWeight1"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["St2Qty"] = Convert.ToDecimal(lst[m].Qty);
                                    r["Stone2Name"] = lst[m].StoneName.ToString();
                                }
                                else if (s == 3)
                                {
                                    r["StoneP2"] = Convert.ToDecimal(lst[m].Price);
                                    r["StoneWeight2"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["St3Qty"] = Convert.ToDecimal(lst[m].Qty);
                                    r["Stone3Name"] = lst[m].StoneName.ToString();
                                }
                            }
                            else if (str.Equals("Beeds"))
                            {
                                DQty = DQty + Convert.ToInt32(lst[m].Qty);
                                BeedsWeight = BeedsWeight + Convert.ToDecimal(lst[m].StoneWeight);
                                BeedsPrice = Convert.ToDecimal(lst[m].BeedsPrice);
                                b = b + 1;
                                if (b == 1)
                                {
                                    r["D1color"] = lst[m].ColorName.ColorName.ToString();
                                    r["BeedsWeight"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["BeedsP"] = Convert.ToDecimal(lst[m].Price);
                                    r["B1Qty"] = Convert.ToDecimal(lst[m].Qty);
                                }
                                else if (b == 2)
                                {
                                    r["D2color"] = lst[m].ColorName.ColorName.ToString();
                                    r["BeedsWeight1"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["BeedsP1"] = Convert.ToDecimal(lst[m].Price);
                                    r["B2Qty"] = Convert.ToDecimal(lst[m].Qty);
                                }
                                else if (b == 3)
                                {
                                    r["D3color"] = lst[m].ColorName.ColorName.ToString();
                                    r["BeedsWeight2"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["BeedsP2"] = Convert.ToDecimal(lst[m].Price);
                                    r["B3Qty"] = Convert.ToDecimal(lst[m].Qty);
                                }
                            }
                            #region Dimaond
                            else if (str.Equals("Diamonds"))
                            {
                                DQty = DQty + Convert.ToInt32(lst[m].Qty);
                                DiamonWeight = DiamonWeight + Convert.ToDecimal(lst[m].StoneWeight);
                                DiamondPrice = Convert.ToDecimal(lst[m].DiamondPrice);
                                d = d + 1;
                                if (d == 1)
                                {
                                    r["D1rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D1cut"] = lst[m].CutName.CutName.ToString();
                                    r["D1clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D1color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S1Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R1ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP"] = Convert.ToDecimal(lst[m].Price);
                                    r["DiamondName"] = (lst[m].StoneName).ToString();
                                }
                                else if (d == 2)
                                {
                                    r["D2rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D2cut"] = lst[m].CutName.CutName.ToString();
                                    r["D2clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D2color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S2Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R2ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP1"] = Convert.ToDecimal(lst[m].Price);
                                    r["DiamondName1"] = (lst[m].StoneName).ToString();
                                }
                                else if (d == 3)
                                {
                                    r["D3rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D3cut"] = lst[m].CutName.CutName.ToString();
                                    r["D3clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D3color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S3Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R3ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP2"] = Convert.ToDecimal(lst[m].Price);
                                    r["DiamondName2"] = (lst[m].StoneName).ToString();
                                }
                                else if (d == 4)
                                {
                                    r["D4rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D4cut"] = lst[m].CutName.CutName.ToString();
                                    r["D4clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D4color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S4Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R4ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP3"] = Convert.ToDecimal(lst[m].Price);
                                    r["DiamondName3"] = (lst[m].StoneName).ToString();
                                }
                                else if (d == 5)
                                {
                                    r["D5rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D5cut"] = lst[m].CutName.CutName.ToString();
                                    r["D5clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D5color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S5Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R5ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP4"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (d == 6)
                                {
                                    r["D6rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D6cut"] = lst[m].CutName.CutName.ToString();
                                    r["D6clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D6color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S6Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R6ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP5"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (d == 7)
                                {
                                    r["D7rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D7cut"] = lst[m].CutName.CutName.ToString();
                                    r["D7clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D7color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S7Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R7ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP6"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (d == 8)
                                {
                                    r["D8rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D7cut"] = lst[m].CutName.CutName.ToString();
                                    r["D8clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D8color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S8Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R8ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP7"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (d == 9)
                                {
                                    r["D9rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D9cut"] = lst[m].CutName.CutName.ToString();
                                    r["D9clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D9color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S9Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R9ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP8"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (d == 10)
                                {
                                    r["D10rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D10cut"] = lst[m].CutName.CutName.ToString();
                                    r["D10clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D10color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S10Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R10ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP9"] = Convert.ToDecimal(lst[m].Price);
                                }
                            }
                            #endregion
                        }
                        r["StonePrice"] = price;
                        r["StWeight"] = StoneWeight;
                        r["BdW"] = BeedsWeight;
                        r["BeedsPrice"] = BeedsPrice;
                        r["DrW"] = DiamonWeight;
                        r["DQty"] = DQty;
                        r["DiamondPrice"] = DiamondPrice;

                    }
                    else
                    {
                        r["BdW"] = 0;
                        r["StoneWeight"] = 0;
                        r["DrW"] = 0;
                        r["DQty"] = 0;
                        r["DiamondPrice"] = 0;
                    }
                    r["WasteInGm"] = dt.Rows[i]["WasteInGm"];
                    r["TotalLaker"] = dt.Rows[i]["TotalLaker"];
                    r["TotalMaking"] = dt.Rows[i]["TotalMaking"];
                    r["TotalPrice"] = dt.Rows[i]["TotalPrice"];
                    r["IndexNo"] = dt.Rows[i]["IndexNo"];
                    r["ItemName"] = dt.Rows[i]["ItemName"];
                    r["Qty"] = dt.Rows[i]["Qty"];
                    r["IType"] = dt.Rows[i]["IType"];
                    i = i + 1;
                    if (i < dt.Rows.Count)
                    {
                        if (r == null)
                            r = dt1.NewRow();
                        r["TagNo1"] = dt.Rows[i]["TagNo"];
                        r["StockId1"] = dt.Rows[i]["StockId"];
                        r["BarCode1"] = dt.Rows[i]["BarCode"];
                        r["TotalWeight1"] = dt.Rows[i]["TotalWeight"];
                        r["Weight1"] = dt.Rows[i]["NetWeight"];
                        r["Pieces1"] = dt.Rows[i]["Pieces"];
                        r["Karat1"] = dt.Rows[i]["Karat"];
                        r["Description1"] = dt.Rows[i]["Description"];
                        r["BdW1"] = 0;
                        r["StWeight1"] = 0;
                        r["DrW1"] = 0;
                        r["BeedsPrice1"] = 0;
                        r["StonePrice1"] = 0;
                        r["SilverSalePrice1"] = dt.Rows[i]["SilverSalePrice"];
                        r["PWeight1"] = dt.Rows[i]["PWeight"];
                        r["Kaat1"] = dt.Rows[i]["Kaat"];
                        r["DesignNo1"] = dt.Rows[i]["DesignNo"];
                        r["RateA1"] = dt.Rows[i]["RateA"];
                        r["RateD1"] = dt.Rows[i]["RateD"];
                        r["PriceA1"] = dt.Rows[i]["PriceA"];
                        r["PriceD1"] = dt.Rows[i]["PriceD"];
                        r["WorkerName1"] = dt.Rows[i]["WorkerName"];
                        r["DesNo1"] = dt.Rows[i]["DesNo"];
                        List<Stones> lst1 = stDAL.GetAllStonesDetail(dt.Rows[i]["TagNo"].ToString());

                        if (lst1 != null)
                        {
                            Decimal price1 = 0;
                            Decimal BeedsPrice1 = 0;
                            Decimal StoneWeight1 = 0;
                            Decimal DiamondPrice1 = 0;
                            Decimal BeedsWeight1 = 0;
                            Decimal DiamonWeight1 = 0;
                            int DQty1 = 0;
                            int s1 = 0;
                            int b1 = 0;
                            int d1 = 0;
                            for (int m = 0; m < lst1.Count; m++)
                            {
                                string str = lst1[m].StoneTypeName.ToString();
                                if (str.Equals("Stones"))
                                {
                                    StoneWeight1 = StoneWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                    price1 = Convert.ToDecimal(lst1[m].StonePrice);
                                    s1 = s1 + 1;
                                    if (s1 == 1)
                                    {
                                        r["Stone1P"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Stone1Weight"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["St1Qty1"] = Convert.ToDecimal(lst1[m].Qty);
                                        r["StoneName1"] = lst1[m].StoneName.ToString();
                                    }
                                    else if (s1 == 2)
                                    {
                                        r["Stone1P1"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Stone1Weight1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["St2Qty1"] = Convert.ToDecimal(lst1[m].Qty);
                                        r["StoneName2"] = lst1[m].StoneName.ToString();
                                    }
                                    else if (s1 == 3)
                                    {
                                        r["Stone1P2"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Stone1Weight2"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["St3Qty1"] = Convert.ToDecimal(lst1[m].Qty);
                                        r["StoneName3"] = lst1[m].StoneName.ToString();
                                    }
                                }
                                else if (str.Equals("Beeds"))
                                {
                                    DQty1 = DQty1 + Convert.ToInt32(lst1[m].Qty);
                                    BeedsWeight1 = BeedsWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                    BeedsPrice1 = Convert.ToDecimal(lst1[m].BeedsPrice);
                                    b1 = b1 + 1;
                                    if (b1 == 1)
                                    {
                                        r["D1color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["Beeds1Weight"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["Beeds1P"] = Convert.ToDecimal(lst1[m].Price);
                                        r["B1Qty1"] = Convert.ToDecimal(lst1[m].Qty);
                                    }
                                    else if (b1 == 2)
                                    {
                                        r["D2color2"] = lst1[m].ColorName.ColorName.ToString();
                                        r["Beeds1Weight1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["Beeds1P1"] = Convert.ToDecimal(lst1[m].Price);
                                        r["B2Qty1"] = Convert.ToDecimal(lst1[m].Qty);
                                    }
                                    else if (b1 == 3)
                                    {
                                        r["D3color3"] = lst1[m].ColorName.ColorName.ToString();
                                        r["Beeds1Weight2"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["Beeds1P2"] = Convert.ToDecimal(lst1[m].Price);
                                        r["B3Qty1"] = Convert.ToDecimal(lst1[m].Qty);
                                    }
                                }
                                #region Dimaond
                                else if (str.Equals("Diamonds"))
                                {
                                    DQty1 = DQty1 + Convert.ToInt32(lst1[m].Qty);
                                    DiamonWeight1 = DiamonWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                    DiamondPrice1 = Convert.ToDecimal(lst1[m].DiamondPrice);
                                    d1 = d1 + 1;
                                    if (d1 == 1)
                                    {
                                        r["D1rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D1cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D1clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D1color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S1Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R1ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Diamond1Name"] = (lst1[m].StoneName).ToString();
                                    }
                                    else if (d1 == 2)
                                    {
                                        r["D2rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D2cut1"] = lst1[m].CutName.CutName.ToString(); ;
                                        r["D2clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D2color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S2Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R2ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P1"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Diamond1Name1"] = (lst1[m].StoneName).ToString();
                                    }
                                    else if (d1 == 3)
                                    {
                                        r["D3rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D3cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D3clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D3color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S3Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R3ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P2"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Diamond1Name2"] = (lst1[m].StoneName).ToString();
                                    }
                                    else if (d1 == 4)
                                    {
                                        r["D4rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D4cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D4clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D4color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S4Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R4ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P3"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Diamond1Name3"] = (lst1[m].StoneName).ToString();
                                    }
                                    else if (d1 == 5)
                                    {
                                        r["D5rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D5cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D5clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D5color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S5Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R5ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P4"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (d1 == 6)
                                    {
                                        r["D6rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D6cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D6clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D6color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S6Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R6ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P5"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (d1 == 7)
                                    {
                                        r["D7rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D7cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D7clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D7color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S7Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R7ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P6"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (d1 == 8)
                                    {
                                        r["D8rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D8cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D8clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D8color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S8Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R8ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P7"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (d1 == 9)
                                    {
                                        r["D9rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D9cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D9clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D9color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S9Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R9ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P8"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (d1 == 10)
                                    {
                                        r["D10rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D10cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D10clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D10color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S10Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R10ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P9"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                }
                                #endregion
                            }
                            r["StonePrice1"] = price1;
                            r["StWeight1"] = StoneWeight1;
                            r["BdW1"] = BeedsWeight1;
                            r["BeedsPrice1"] = BeedsPrice1;
                            r["DrW1"] = DiamonWeight1;
                            r["DQty1"] = DQty1;
                            r["DiamondPrice1"] = DiamondPrice1;
                        }
                        else
                        {
                            r["BdW1"] = 0;
                            r["Stone1Weight"] = 0;
                            r["DrW1"] = 0;
                            r["DQty1"] = 0;
                            r["DiamondPrice1"] = 0;
                        }
                        r["WasteInGm1"] = dt.Rows[i]["WasteInGm"];
                        r["TotalLaker1"] = dt.Rows[i]["TotalLaker"];
                        r["TotalMaking1"] = dt.Rows[i]["TotalMaking"];
                        r["TotalPrice1"] = dt.Rows[i]["TotalPrice"];
                        r["IndexNo1"] = dt.Rows[i]["IndexNo"];
                        r["ItemName1"] = dt.Rows[i]["ItemName"];
                        r["Qty1"] = dt.Rows[i]["Qty"];
                        r["IType1"] = dt.Rows[i]["IType"];
                    }
                    #region 4BarCode
                    if (RptNo == 2)
                    {
                        i = i + 1;
                        if (i < dt.Rows.Count)
                        {
                            if (r == null)
                                r = dt1.NewRow();
                            r["TagNo2"] = dt.Rows[i]["TagNo"];
                            r["StockId2"] = dt.Rows[i]["StockId"];
                            r["BarCode2"] = dt.Rows[i]["BarCode"];
                            r["TotalWeight2"] = dt.Rows[i]["TotalWeight"];
                            r["Weight2"] = dt.Rows[i]["NetWeight"];
                            r["Pieces2"] = dt.Rows[i]["Pieces"];
                            r["Karat2"] = dt.Rows[i]["Karat"];
                            r["Description2"] = dt.Rows[i]["Description"];
                            r["BdW2"] = 0;
                            r["StWeight2"] = 0;
                            r["DrW2"] = 0;
                            r["BeedsPrice2"] = 0;
                            r["StonePrice2"] = 0;
                            r["SilverSalePrice2"] = dt.Rows[i]["SilverSalePrice"];
                            r["PWeight2"] = dt.Rows[i]["PWeight"];
                            r["Kaat2"] = dt.Rows[i]["Kaat"];
                            r["DesignNo2"] = dt.Rows[i]["DesignNo"];
                            r["RateA2"] = dt.Rows[i]["RateA"];
                            r["RateD2"] = dt.Rows[i]["RateD"];
                            r["PriceA2"] = dt.Rows[i]["PriceA"];
                            r["PriceD2"] = dt.Rows[i]["PriceD"];
                            List<Stones> lst1 = stDAL.GetAllStonesDetail(dt.Rows[i]["TagNo"].ToString());

                            if (lst1 != null)
                            {
                                Decimal price1 = 0;
                                Decimal BeedsPrice1 = 0;
                                Decimal StoneWeight1 = 0;
                                Decimal BeedsWeight1 = 0;
                                Decimal DiamonWeight1 = 0;
                                int DQty1 = 0;
                                int d1 = 0;
                                for (int m = 0; m < lst1.Count; m++)
                                {
                                    string str = lst1[m].StoneTypeName.ToString();
                                    if (str.Equals("Stones"))
                                    {
                                        StoneWeight1 = StoneWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                        price1 = price1 + Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (str.Equals("Beeds"))
                                    {
                                        BeedsWeight1 = BeedsWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                        BeedsPrice1 = BeedsPrice1 + Convert.ToDecimal(lst1[m].Price);
                                    }
                                    #region Dimaond
                                    else if (str.Equals("Diamonds"))
                                    {
                                        DQty1 = DQty1 + Convert.ToInt32(lst[m].Qty);
                                        DiamonWeight1 = DiamonWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                        d1 = d1 + 1;
                                        if (d1 == 1)
                                        {
                                            r["D1rW2"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D1cut2"] = lst1[m].CutName;
                                            r["D1clearity2"] = lst1[m].ClearityName;
                                            r["D1color2"] = lst1[m].ColorName;
                                            r["S1Qty2"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R1ate2"] = Convert.ToDecimal(lst1[m].Rate);
                                        }
                                        else if (d1 == 2)
                                        {
                                            r["D2rW2"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D2cut2"] = lst1[m].CutName;
                                            r["D2clearity2"] = lst1[m].ClearityName;
                                            r["D2color2"] = lst1[m].ColorName;
                                            r["S2Qty2"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R2ate2"] = Convert.ToDecimal(lst1[m].Rate);
                                        }
                                        else if (d1 == 3)
                                        {
                                            r["D3rW2"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D3cut2"] = lst1[m].CutName;
                                            r["D3clearity2"] = lst1[m].ClearityName;
                                            r["D3color2"] = lst1[m].ColorName;
                                            r["S3Qty2"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R3ate2"] = Convert.ToDecimal(lst1[m].Rate);
                                        }
                                    }
                                    #endregion
                                }
                                r["StonePrice2"] = price1;
                                r["StWeight2"] = StoneWeight1;
                                r["BdW2"] = BeedsWeight1;
                                r["BeedsPrice2"] = BeedsPrice1;
                                r["DrW2"] = DiamonWeight1;
                                r["DQty2"] = DQty1;
                            }
                            else
                            {
                                r["BdW2"] = 0;
                                r["StWeight2"] = 0;
                                r["DrW2"] = 0;
                                r["DQty2"] = 0;
                            }
                            r["WasteInGm2"] = dt.Rows[i]["WasteInGm"];
                            r["TotalLaker2"] = dt.Rows[i]["TotalLaker"];
                            r["TotalMaking2"] = dt.Rows[i]["TotalMaking"];
                            r["TotalPrice2"] = dt.Rows[i]["TotalPrice"];
                            r["IndexNo2"] = dt.Rows[i]["IndexNo"];
                            r["ItemName2"] = dt.Rows[i]["ItemName"];
                            r["Qty2"] = dt.Rows[i]["Qty"];
                            r["IType2"] = dt.Rows[i]["IType"];
                        }
                        i = i + 1;
                        if (i < dt.Rows.Count)
                        {
                            if (r == null)
                                r = dt1.NewRow();
                            r["TagNo3"] = dt.Rows[i]["TagNo"];
                            r["StockId3"] = dt.Rows[i]["StockId"];
                            r["BarCode3"] = dt.Rows[i]["BarCode"];
                            r["TotalWeight3"] = dt.Rows[i]["TotalWeight"];
                            r["Weight3"] = dt.Rows[i]["NetWeight"];
                            r["Pieces3"] = dt.Rows[i]["Pieces"];
                            r["Karat3"] = dt.Rows[i]["Karat"];
                            r["Description3"] = dt.Rows[i]["Description"];
                            r["BdW3"] = 0;
                            r["StWeight3"] = 0;
                            r["DrW3"] = 0;
                            r["BeedsPrice3"] = 0;
                            r["StonePrice3"] = 0;
                            r["SilverSalePrice3"] = dt.Rows[i]["SilverSalePrice"];
                            r["PWeight3"] = dt.Rows[i]["PWeight"];
                            r["Kaat3"] = dt.Rows[i]["Kaat"];
                            r["DesignNo3"] = dt.Rows[i]["DesignNo"];
                            r["RateA3"] = dt.Rows[i]["RateA"];
                            r["RateD3"] = dt.Rows[i]["RateD"];
                            r["PriceA3"] = dt.Rows[i]["PriceA"];
                            r["PriceD3"] = dt.Rows[i]["PriceD"];
                            List<Stones> lst1 = stDAL.GetAllStonesDetail(dt.Rows[i]["TagNo"].ToString());

                            if (lst1 != null)
                            {
                                Decimal price1 = 0;
                                Decimal BeedsPrice1 = 0;
                                Decimal StoneWeight1 = 0;
                                Decimal BeedsWeight1 = 0;
                                Decimal DiamonWeight1 = 0;
                                int DQty1 = 0;
                                int d1 = 0;
                                for (int m = 0; m < lst1.Count; m++)
                                {
                                    string str = lst1[m].StoneTypeName.ToString();
                                    if (str.Equals("Stones"))
                                    {
                                        StoneWeight1 = StoneWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                        price1 = price1 + Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (str.Equals("Beeds"))
                                    {
                                        BeedsWeight1 = BeedsWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                        BeedsPrice1 = BeedsPrice1 + Convert.ToDecimal(lst1[m].Price);
                                    }
                                    #region Dimaond
                                    else if (str.Equals("Diamonds"))
                                    {
                                        DQty1 = DQty1 + Convert.ToInt32(lst[m].Qty);
                                        DiamonWeight1 = DiamonWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                        d1 = d1 + 1;
                                        if (d1 == 1)
                                        {
                                            r["D1rW3"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D1cut3"] = lst1[m].CutName;
                                            r["D1clearity3"] = lst1[m].ClearityName;
                                            r["D1color3"] = lst1[m].ColorName;
                                            r["S1Qty3"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R1ate3"] = Convert.ToDecimal(lst1[m].Rate);
                                        }
                                        else if (d1 == 2)
                                        {
                                            r["D2rW3"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D2cut3"] = lst1[m].CutName;
                                            r["D2clearity3"] = lst1[m].ClearityName;
                                            r["D2color3"] = lst1[m].ColorName;
                                            r["S2Qty3"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R2ate3"] = Convert.ToDecimal(lst1[m].Rate);
                                        }
                                        else if (d1 == 3)
                                        {
                                            r["D3rW3"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D3cut3"] = lst1[m].CutName;
                                            r["D3clearity3"] = lst1[m].ClearityName;
                                            r["D3color3"] = lst1[m].ColorName;
                                            r["S3Qty3"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R3ate3"] = Convert.ToDecimal(lst1[m].Rate);
                                        }
                                    }
                                    #endregion
                                }
                                r["StonePrice3"] = price1;
                                r["StWeight3"] = StoneWeight1;
                                r["BdW3"] = BeedsWeight1;
                                r["BeedsPrice3"] = BeedsPrice1;
                                r["DrW3"] = DiamonWeight1;
                                r["DQty3"] = DQty1;
                            }
                            else
                            {
                                r["BdW3"] = 0;
                                r["StWeight3"] = 0;
                                r["DrW3"] = 0;
                                r["DQty3"] = 0;
                            }
                            r["WasteInGm3"] = dt.Rows[i]["WasteInGm"];
                            r["TotalLaker3"] = dt.Rows[i]["TotalLaker"];
                            r["TotalMaking3"] = dt.Rows[i]["TotalMaking"];
                            r["TotalPrice3"] = dt.Rows[i]["TotalPrice"];
                            r["IndexNo3"] = dt.Rows[i]["IndexNo"];
                            r["ItemName3"] = dt.Rows[i]["ItemName"];
                            r["Qty3"] = dt.Rows[i]["Qty"];
                            r["IType3"] = dt.Rows[i]["IType"];
                        }
                    }
                    #endregion
                    dt1.Rows.Add(r);
                    r = null;

                }

                #endregion
            }
            #endregion

            #region isPage4 Manual
            if (isPage == 4)
            {
                #region Barcode
                DataRow r = null;
                for (int i = 0; i < stkl.Count; i++)
                {
                    if (r == null)
                        r = dt1.NewRow();
                    stk = stkl[i];
                    r["TagNo"] = stk.TagNo.ToString();
                    r["BarCode"] = stk.BarCode.ToString();
                    r["TotalWeight"] = stk.TotalWeight;
                    r["Weight"] = stk.NetWeight;
                    r["Pieces"] = stk.Pieces;
                    r["Karat"] = stk.Karrat;
                    r["Kaat"] = stk.KaatInRatti;
                    r["PWeight"] = stk.PWeight;
                    r["WasteInGm"] = stk.WasteInGm;
                    r["TotalLaker"] = stk.TotalLaker;
                    r["TotalMaking"] = stk.TotalMaking;
                    r["BarCode"] = stk.BarCode.ToString();
                    r["TotalPrice"] = stk.TotalPrice;
                    r["WasteInGm"] = stk.WasteInGm;
                    r["ItemName"] = stk.ItemName.ItemName;
                    r["Qty"] = stk.Qty;
                    r["IType"] = stk.ItemType;
                    r["SilverSalePrice"] = stk.Silver.SalePrice;
                    r["BdW"] = 0;
                    r["StWeight"] = 0;
                    r["DrW"] = 0;
                    r["BeedsPrice"] = 0;
                    r["StonePrice"] = 0;
                    r["DesignNo"] = stk.DesignNo.DesignNo.ToString();
                    r["RateA"] = stk.Silver.RateA;
                    r["RateD"] = stk.Silver.RateD;
                    r["PriceA"] = stk.Silver.PriceA;
                    r["PriceD"] = stk.Silver.PriceD;
                    r["WorkerName"] = stk.WorkerName.Name;
                    r["DesNo"] = stk.DesNo;
                    List<Stones> lst = stDAL.GetAllStonesDetail(stk.TagNo.ToString());

                    if (lst != null)
                    {
                        Decimal price = 0;
                        Decimal BeedsPrice = 0;
                        Decimal DiamondPrice = 0;
                        Decimal StoneWeight = 0;
                        Decimal BeedsWeight = 0;
                        Decimal DiamonWeight = 0;
                        int DQty = 0;
                        int s = 0;
                        int b = 0;
                        int d = 0;
                        for (int m = 0; m < lst.Count; m++)
                        {
                            string str1 = lst[m].StoneTypeName.ToString();
                            if (str1.Equals("Stones"))
                            {
                                StoneWeight = StoneWeight + Convert.ToDecimal(lst[m].StoneWeight);
                                price = Convert.ToDecimal(lst[m].StonePrice);
                                s = s + 1;
                                if (s == 1)
                                {
                                    r["StoneP"] = Convert.ToDecimal(lst[m].Price);
                                    r["StoneWeight"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["S1Qty"] = Convert.ToDecimal(lst[m].Qty);
                                    r["StoneName"]=lst[m].StoneName.ToString();
                                }
                                else if (s == 2)
                                {
                                    r["StoneP1"] = Convert.ToDecimal(lst[m].Price);
                                    r["StoneWeight1"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["Stone2Name"]=lst[m].StoneName.ToString();
                                }
                                else if (s == 3)
                                {
                                    r["StoneP2"] = Convert.ToDecimal(lst[m].Price);
                                    r["StoneWeight2"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["Stone3Name"]=lst[m].StoneName.ToString();
                                }
                            }
                            else if (str1.Equals("Beeds"))
                            {
                                DQty = DQty + Convert.ToInt32(lst[m].Qty);
                                BeedsWeight = BeedsWeight + Convert.ToDecimal(lst[m].StoneWeight);
                                BeedsPrice = Convert.ToDecimal(lst[m].BeedsPrice);
                                b = b + 1;
                                if (b == 1)
                                {
                                    r["D1color"] = lst[m].ColorName.ColorName.ToString();
                                    r["BeedsWeight"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["BeedsP"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (b == 2)
                                {
                                    r["D2color"] = lst[m].ColorName.ColorName.ToString();
                                    r["BeedsWeight1"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["BeedsP1"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (b == 3)
                                {
                                    r["D3color"] = lst[m].ColorName.ColorName.ToString();
                                    r["BeedsWeight2"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["BeedsP2"] = Convert.ToDecimal(lst[m].Price);
                                }
                            }
                            #region Dimaond
                            else if (str1.Equals("Diamonds"))
                            {
                                DQty = DQty + Convert.ToInt32(lst[m].Qty);
                                DiamonWeight = DiamonWeight + Convert.ToDecimal(lst[m].StoneWeight);
                                DiamondPrice = Convert.ToDecimal(lst[m].DiamondPrice);
                                d = d + 1;
                                if (d == 1)
                                {
                                    r["D1rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D1cut"] = lst[m].CutName.CutName.ToString();
                                    r["D1clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D1color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S1Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R1ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP"] = Convert.ToDecimal(lst[m].Price);
                                    r["DiamondName"] = (lst[m].StoneName).ToString();

                                }
                                else if (d == 2)
                                {
                                    r["D2rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D2cut"] = lst[m].CutName.CutName.ToString();
                                    r["D2clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D2color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S2Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R2ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP1"] = Convert.ToDecimal(lst[m].Price);
                                    r["DiamondName1"] = (lst[m].StoneName).ToString();
                                }
                                else if (d == 3)
                                {
                                    r["D3rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D3cut"] = lst[m].CutName.CutName.ToString();
                                    r["D3clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D3color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S3Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R3ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP2"] = Convert.ToDecimal(lst[m].Price);
                                    r["DiamondName2"] = (lst[m].StoneName).ToString();
                                }
                                else if (d == 4)
                                {
                                    r["D4rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D4cut"] = lst[m].CutName.CutName.ToString();
                                    r["D4clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D4color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S4Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R4ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP3"] = Convert.ToDecimal(lst[m].Price);
                                    r["DiamondName3"] = (lst[m].StoneName).ToString();
                                }
                                else if (d == 5)
                                {
                                    r["D5rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D5cut"] = lst[m].CutName.CutName.ToString();
                                    r["D5clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D5color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S5Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R5ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP4"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (d == 6)
                                {
                                    r["D6rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D6cut"] = lst[m].CutName.CutName.ToString();
                                    r["D6clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D6color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S6Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R6ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP5"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (d == 7)
                                {
                                    r["D7rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D7cut"] = lst[m].CutName.CutName.ToString();
                                    r["D7clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D7color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S7Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R7ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP6"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (d == 8)
                                {
                                    r["D8rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D7cut"] = lst[m].CutName.CutName.ToString();
                                    r["D8clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D8color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S8Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R8ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP7"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (d == 9)
                                {
                                    r["D9rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D9cut"] = lst[m].CutName.CutName.ToString();
                                    r["D9clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D9color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S9Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R9ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP8"] = Convert.ToDecimal(lst[m].Price);
                                }
                                else if (d == 10)
                                {
                                    r["D10rW"] = Convert.ToDecimal(lst[m].StoneWeight);
                                    r["D10cut"] = lst[m].CutName.CutName.ToString();
                                    r["D10clearity"] = lst[m].ClearityName.ClearityName.ToString();
                                    r["D10color"] = lst[m].ColorName.ColorName.ToString();
                                    r["S10Qty"] = Convert.ToInt32(lst[m].Qty);
                                    r["R10ate"] = Convert.ToDecimal(lst[m].Rate);
                                    r["DiamondP9"] = Convert.ToDecimal(lst[m].Price);
                                }
                            }
                            #endregion
                        }
                        r["StonePrice"] = price;
                        r["StWeight"] = StoneWeight;
                        r["BdW"] = BeedsWeight;
                        r["BeedsPrice"] = BeedsPrice;
                        r["DrW"] = DiamonWeight;
                        r["DQty"] = DQty;
                        r["DiamondPrice"] = DiamondPrice;

                    }
                    else
                    {
                        r["BdW"] = 0;
                        r["StoneWeight"] = 0;
                        r["DrW"] = 0;
                        r["DQty"] = 0;
                        r["DiamondPrice"] = 0;
                    }
                    r["Qty"] = stk.Qty;
                    i = i + 1;
                    if (i < stkl.Count)
                    {
                        stk = stkl[i];
                        r["TagNo1"] = stk.TagNo.ToString();
                        r["BarCode1"] = stk.BarCode.ToString();
                        r["TotalWeight1"] = stk.TotalWeight;
                        r["Weight1"] = stk.NetWeight;
                        r["Pieces1"] = stk.Pieces;
                        r["Karat1"] = stk.Karrat;
                        r["Kaat1"] = stk.KaatInRatti;
                        r["PWeight1"] = stk.PWeight;
                        r["WasteInGm1"] = stk.WasteInGm;
                        r["TotalLaker1"] = stk.TotalLaker;
                        r["TotalMaking1"] = stk.TotalMaking;
                        r["TotalPrice1"] = stk.TotalPrice;
                        r["WasteInGm1"] = stk.WasteInGm;
                        r["ItemName1"] = stk.ItemName.ItemName;
                        r["Qty1"] = stk.Qty;
                        r["IType1"] = stk.ItemType;
                        r["SilverSalePrice"] = stk.Silver.SalePrice;
                        r["BdW1"] = 0;
                        r["StWeight1"] = 0;
                        r["DrW1"] = 0;
                        r["BeedsPrice1"] = 0;
                        r["StonePrice1"] = 0;
                        r["DesignNo1"] = stk.DesignNo.DesignNo.ToString();
                        r["RateA1"] = stk.Silver.RateA;
                        r["RateD1"] = stk.Silver.RateD;
                        r["PriceA1"] = stk.Silver.PriceA;
                        r["PriceD1"] = stk.Silver.PriceD;
                        r["WorkerName1"] = stk.WorkerName.Name;
                        r["DesNo1"] = stk.DesNo;
                        List<Stones> lst1 = stDAL.GetAllStonesDetail(stk.TagNo.ToString());

                        if (lst1 != null)
                        {
                            Decimal price1 = 0;
                            Decimal BeedsPrice1 = 0;
                            Decimal StoneWeight1 = 0;
                            Decimal DiamondPrice1 = 0;
                            Decimal BeedsWeight1 = 0;
                            Decimal DiamonWeight1 = 0;
                            int DQty1 = 0;
                            int s1 = 0;
                            int b1 = 0;
                            int d1 = 0;
                            for (int m = 0; m < lst1.Count; m++)
                            {
                                string str2 = lst1[m].StoneTypeName.ToString();
                                if (str2.Equals("Stones"))
                                {
                                    StoneWeight1 = StoneWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                    price1 = Convert.ToDecimal(lst1[m].StonePrice);
                                    s1 = s1 + 1;
                                    if (s1 == 1)
                                    {
                                        r["Stone1P"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Stone1Weight"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["S1Qty1"] = Convert.ToDecimal(lst1[m].Qty);
                                        r["StoneName1"] = lst1[m].StoneName.ToString();
                                    }
                                    else if (s1 == 2)
                                    {
                                        r["Stone1P1"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Stone1Weight1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["StoneName2"] = lst1[m].StoneName.ToString();
                                    }
                                    else if (s1 == 3)
                                    {
                                        r["Stone1P2"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Stone1Weight2"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["StoneName3"] = lst1[m].StoneName.ToString();
                                    }
                                }
                                else if (str2.Equals("Beeds"))
                                {
                                    DQty1 = DQty1 + Convert.ToInt32(lst1[m].Qty);
                                    BeedsWeight1 = BeedsWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                    BeedsPrice1 = BeedsPrice1 + Convert.ToDecimal(lst1[m].StonePrice);
                                    //BeedsPrice1 = Convert.ToDecimal(lst[m].BeedsPrice);
                                    b1 = b1 + 1;
                                    if (b1 == 1)
                                    {
                                        r["D1color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["Beeds1Weight"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["Beeds1P"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (b1 == 2)
                                    {
                                        r["D2color2"] = lst1[m].ColorName.ColorName.ToString();
                                        r["Beeds1Weight1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["Beeds1P1"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (b1 == 3)
                                    {
                                        r["D3color3"] = lst1[m].ColorName.ColorName.ToString();
                                        r["Beeds1Weight2"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["Beeds1P2"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                }
                                #region Dimaond
                                else if (str2.Equals("Diamonds"))
                                {
                                    DQty1 = DQty1 + Convert.ToInt32(lst1[m].Qty);
                                    DiamonWeight1 = DiamonWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                    DiamondPrice1 = Convert.ToDecimal(lst1[m].DiamondPrice);
                                    d1 = d1 + 1;
                                    if (d1 == 1)
                                    {
                                        r["D1rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D1cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D1clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D1color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S1Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R1ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Diamond1Name"] = (lst1[m].StoneName).ToString();
                                        ///////////////////////////////////////////////////////////////
                                    }
                                    else if (d1 == 2)
                                    {
                                        r["D2rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D2cut1"] = lst1[m].CutName.CutName.ToString(); ;
                                        r["D2clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D2color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S2Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R2ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P1"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Diamond1Name1"] = (lst1[m].StoneName).ToString();
                                    }
                                    else if (d1 == 3)
                                    {
                                        r["D3rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D3cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D3clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D3color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S3Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R3ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P2"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Diamond1Name2"] = (lst1[m].StoneName).ToString();
                                    }
                                    else if (d1 == 4)
                                    {
                                        r["D4rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D4cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D4clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D4color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S4Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R4ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P3"] = Convert.ToDecimal(lst1[m].Price);
                                        r["Diamond1Name3"] = (lst1[m].StoneName).ToString();
                                    }
                                    else if (d1 == 5)
                                    {
                                        r["D5rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D5cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D5clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D5color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S5Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R5ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P4"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (d1 == 6)
                                    {
                                        r["D6rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D6cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D6clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D6color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S6Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R6ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P5"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (d1 == 7)
                                    {
                                        r["D7rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D7cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D7clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D7color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S7Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R7ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P6"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (d1 == 8)
                                    {
                                        r["D8rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D8cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D8clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D8color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S8Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R8ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P7"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (d1 == 9)
                                    {
                                        r["D9rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D9cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D9clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D9color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S9Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R9ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P8"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (d1 == 10)
                                    {
                                        r["D10rW1"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                        r["D10cut1"] = lst1[m].CutName.CutName.ToString();
                                        r["D10clearity1"] = lst1[m].ClearityName.ClearityName.ToString();
                                        r["D10color1"] = lst1[m].ColorName.ColorName.ToString();
                                        r["S10Qty1"] = Convert.ToInt32(lst1[m].Qty);
                                        r["R10ate1"] = Convert.ToDecimal(lst1[m].Rate);
                                        r["Diamond1P9"] = Convert.ToDecimal(lst1[m].Price);
                                    }
                                }
                                #endregion
                            }
                            r["StonePrice1"] = price1;
                            r["StWeight1"] = StoneWeight1;
                            r["BdW1"] = BeedsWeight1;
                            r["BeedsPrice1"] = BeedsPrice1;
                            r["DrW1"] = DiamonWeight1;
                            r["DQty1"] = DQty1;
                            r["DiamondPrice1"] = DiamondPrice1;
                        }
                        else
                        {
                            r["BdW1"] = 0;
                            r["Stone1Weight"] = 0;
                            r["DrW1"] = 0;
                            r["DQty1"] = 0;
                            r["DiamondPrice1"] = 0;
                        }
                    }
                    if (RptNo == 2)
                    {
                        i = i + 1;
                        if (i < stkl.Count)
                        {
                            stk = stkl[i];
                            r["TagNo2"] = stk.TagNo.ToString();
                            r["BarCode2"] = stk.BarCode.ToString();
                            r["TotalWeight2"] = stk.TotalWeight;
                            r["Weight2"] = stk.NetWeight;
                            r["Pieces2"] = stk.Pieces;
                            r["Karat2"] = stk.Karrat;
                            r["Kaat2"] = stk.KaatInRatti;
                            r["PWeight2"] = stk.PWeight;
                            r["WasteInGm2"] = stk.WasteInGm;
                            r["TotalLaker2"] = stk.TotalLaker;
                            r["TotalMaking2"] = stk.TotalMaking;
                            r["TotalPrice2"] = stk.TotalPrice;
                            r["WasteInGm2"] = stk.WasteInGm;
                            r["ItemName2"] = stk.ItemName.ItemName;
                            r["Qty2"] = stk.Qty;
                            r["IType2"] = stk.ItemType;
                            r["SilverSalePrice2"] = stk.Silver.SalePrice;
                            r["BdW2"] = 0;
                            r["StWeight2"] = 0;
                            r["DrW2"] = 0;
                            r["BeedsPrice2"] = 0;
                            r["StonePrice2"] = 0;
                            r["DesignNo2"] = stk.DesignNo.DesignNo.ToString();
                            r["RateA2"] = stk.Silver.RateA;
                            r["RateD2"] = stk.Silver.RateD;
                            r["PriceA2"] = stk.Silver.PriceA;
                            r["PriceD2"] = stk.Silver.PriceD;
                            List<Stones> lst1 = stDAL.GetAllStonesDetail(stk.TagNo.ToString());

                            if (lst1 != null)
                            {
                                Decimal price1 = 0;
                                Decimal BeedsPrice1 = 0;
                                Decimal StoneWeight1 = 0;
                                Decimal BeedsWeight1 = 0;
                                Decimal DiamonWeight1 = 0;
                                int DQty1 = 0;
                                int d1 = 0;
                                for (int m = 0; m < lst1.Count; m++)
                                {
                                    string str2 = lst1[m].StoneTypeName.ToString();
                                    if (str2.Equals("Stones"))
                                    {
                                        StoneWeight1 = StoneWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                        price1 = price1 + Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (str2.Equals("Beeds"))
                                    {
                                        BeedsWeight1 = BeedsWeight1 + Convert.ToDecimal(lst[m].StoneWeight);
                                        BeedsPrice1 = BeedsPrice1 + Convert.ToDecimal(lst[m].Price);
                                    }
                                    #region Dimaond
                                    else if (str2.Equals("Diamonds"))
                                    {
                                        DQty1 = DQty1 + Convert.ToInt32(lst1[m].Qty);
                                        DiamonWeight1 = DiamonWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                        d1 = d1 + 1;
                                        if (d1 == 1)
                                        {
                                            r["D1rW2"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D1cut2"] = lst1[m].CutName.CutName.ToString();
                                            r["D1clearity2"] = lst1[m].ClearityName;
                                            r["D1color2"] = lst1[m].ColorName.ColorName.ToString();
                                            r["S1Qty2"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R1ate2"] = Convert.ToDecimal(lst1[m].Rate);
                                        }
                                        else if (d1 == 2)
                                        {
                                            r["D2rW2"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D2cut2"] = lst1[m].CutName.CutName.ToString();
                                            r["D2clearity2"] = lst1[m].ClearityName.ClearityName.ToString();
                                            r["D2color2"] = lst1[m].ColorName.ColorName.ToString();
                                            r["S2Qty2"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R2ate2"] = Convert.ToDecimal(lst1[m].Rate);
                                        }
                                        else if (d1 == 3)
                                        {
                                            r["D3rW2"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D3cut2"] = lst1[m].CutName.CutName.ToString();
                                            r["D3clearity2"] = lst1[m].ClearityName.ClearityName.ToString();
                                            r["D3color2"] = lst1[m].ColorName.ColorName.ToString();
                                            r["S3Qty2"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R3ate2"] = Convert.ToDecimal(lst1[m].Rate);
                                        }
                                    }
                                    #endregion
                                }
                                r["StonePrice2"] = price1;
                                r["StWeight2"] = StoneWeight1;
                                r["BdW2"] = BeedsWeight1;
                                r["BeedsPrice2"] = BeedsPrice1;
                                r["DrW2"] = DiamonWeight1;
                                r["DQty2"] = DQty1;
                            }
                            else
                            {
                                r["BdW2"] = 0;
                                r["StWeight2"] = 0;
                                r["DrW2"] = 0;
                                r["DQty2"] = 0;
                            }
                        }
                        i = i + 1;
                        if (i < stkl.Count)
                        {
                            stk = stkl[i];
                            r["TagNo3"] = stk.TagNo.ToString();
                            r["BarCode3"] = stk.BarCode.ToString();
                            r["TotalWeight3"] = stk.TotalWeight;
                            r["Weight3"] = stk.NetWeight;
                            r["Pieces3"] = stk.Pieces;
                            r["Karat3"] = stk.Karrat;
                            r["Kaat3"] = stk.KaatInRatti;
                            r["PWeight3"] = stk.PWeight;
                            r["WasteInGm3"] = stk.WasteInGm;
                            r["TotalLaker3"] = stk.TotalLaker;
                            r["TotalMaking3"] = stk.TotalMaking;
                            r["TotalPrice3"] = stk.TotalPrice;
                            r["WasteInGm3"] = stk.WasteInGm;
                            r["ItemName3"] = stk.ItemName.ItemName;
                            r["Qty3"] = stk.Qty;
                            r["IType3"] = stk.ItemType;
                            r["SilverSalePrice3"] = stk.Silver.SalePrice;
                            r["BdW3"] = 0;
                            r["StWeight3"] = 0;
                            r["DrW3"] = 0;
                            r["BeedsPrice3"] = 0;
                            r["StonePrice3"] = 0;
                            r["DesignNo3"] = stk.DesignNo.DesignNo.ToString();
                            r["RateA3"] = stk.Silver.RateA;
                            r["RateD3"] = stk.Silver.RateD;
                            r["PriceA3"] = stk.Silver.PriceA;
                            r["PriceD3"] = stk.Silver.PriceD;
                            List<Stones> lst1 = stDAL.GetAllStonesDetail(stk.TagNo.ToString());

                            if (lst1 != null)
                            {
                                Decimal price1 = 0;
                                Decimal BeedsPrice1 = 0;
                                Decimal StoneWeight1 = 0;
                                Decimal BeedsWeight1 = 0;
                                Decimal DiamonWeight1 = 0;
                                int DQty1 = 0;
                                int d1 = 0;
                                for (int m = 0; m < lst1.Count; m++)
                                {
                                    string str2 = lst1[m].StoneTypeName.ToString();
                                    if (str2.Equals("Stones"))
                                    {
                                        StoneWeight1 = StoneWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                        price1 = price1 + Convert.ToDecimal(lst1[m].Price);
                                    }
                                    else if (str2.Equals("Beeds"))
                                    {
                                        BeedsWeight1 = BeedsWeight1 + Convert.ToDecimal(lst[m].StoneWeight);
                                        BeedsPrice1 = BeedsPrice1 + Convert.ToDecimal(lst[m].Price);
                                    }
                                    #region Dimaond
                                    else if (str2.Equals("Diamonds"))
                                    {
                                        DQty1 = DQty1 + Convert.ToInt32(lst1[m].Qty);
                                        DiamonWeight1 = DiamonWeight1 + Convert.ToDecimal(lst1[m].StoneWeight);
                                        d1 = d1 + 1;
                                        if (d1 == 1)
                                        {
                                            r["D1rW3"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D1cut3"] = lst1[m].CutName.CutName.ToString();
                                            r["D1clearity3"] = lst1[m].ClearityName.ClearityName.ToString();
                                            r["D1color3"] = lst1[m].ColorName.ColorName.ToString();
                                            r["S1Qty3"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R1ate3"] = Convert.ToDecimal(lst1[m].Rate);
                                            r["Diamond1Name"] = (lst1[m].StoneName).ToString();
                                        }
                                        else if (d1 == 2)
                                        {
                                            r["D2rW3"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D2cut3"] = lst1[m].CutName.CutName.ToString();
                                            r["D2clearity3"] = lst1[m].ClearityName.ClearityName.ToString();
                                            r["D2color3"] = lst1[m].ColorName.ColorName.ToString();
                                            r["S2Qty3"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R2ate3"] = Convert.ToDecimal(lst1[m].Rate);
                                            r["Diamond1Name1"] = (lst1[m].StoneName).ToString();
                                        }
                                        else if (d1 == 3)
                                        {
                                            r["D3rW3"] = Convert.ToDecimal(lst1[m].StoneWeight);
                                            r["D3cut3"] = lst1[m].CutName.CutName.ToString();
                                            r["D3clearity3"] = lst1[m].ClearityName.ClearityName.ToString();
                                            r["D3color3"] = lst1[m].ColorName.ColorName.ToString();
                                            r["S3Qty3"] = Convert.ToInt32(lst1[m].Qty);
                                            r["R3ate3"] = Convert.ToDecimal(lst1[m].Rate);
                                            r["Diamond1Name2"] = (lst1[m].StoneName).ToString();
                                        }
                                    }
                                    #endregion
                                }
                                r["StonePrice3"] = price1;
                                r["StWeight3"] = StoneWeight1;
                                r["BdW3"] = BeedsWeight1;
                                r["BeedsPrice3"] = BeedsPrice1;
                                r["DrW3"] = DiamonWeight1;
                                r["DQty3"] = DQty1;
                            }
                            else
                            {
                                r["BdW3"] = 0;
                                r["StWeight3"] = 0;
                                r["DrW3"] = 0;
                                r["DQty3"] = 0;
                            }
                        }
                    }
                    dt1.Rows.Add(r);
                    r = null;
                }
                #endregion
            }
            #endregion
            int sr = 0;
            sr = dt1.Rows.Count;

            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = "";
          
                if (ReportNo == 1)
                {
                    reportPath = path + "\\Reports\\GoldBarCode.rpt";
                }
                else if (ReportNo == 2)
                    reportPath = path + "\\Reports\\DiamondBarCode.rpt";
                else if (ReportNo == 3)
                    reportPath = path + "\\Reports\\SilverBarCode.rpt";
                else if (ReportNo == 4)
                    reportPath = path + "\\Reports\\PladiumBarCode.rpt";
                else if (ReportNo == 5)
                    reportPath = path + "\\Reports\\PlatinumBarCode.rpt";
            
           
            
            #endregion

            report = new ReportDocument();
            if (dt1.Rows.Count > 0)
            {
                report.Load(reportPath);
                report.SetDataSource(ds.Tables["Stock"]);
                crystalReportViewer1.ReportSource = report;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //report.PrintOptions.PrinterName = "Zebra TLP2844";

            /*string st = "";
            bool y = false;
            foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            {
                y = strPrinter.Contains("TLP");
                if (y == true)
                    st = strPrinter;
            }
            report.PrintOptions.PrinterName = st;*/
            report.PrintToPrinter(1, false, 1, report.Rows.Count);
        }
    }
}

