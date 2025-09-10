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
using AxCrystalActiveXReportViewerLib105;
using CrystalDecisions.CrystalReports.Engine;
using DAL;
using BusinesEntities;
using System.IO;

namespace jewl
{
    public partial class frmtest1 : Form
    {
        StonesDAL stDAL = new StonesDAL();
        public frmtest1()
        {
            InitializeComponent();
        }

        private void frmtest1_Load(object sender, EventArgs e)
        {
         
             string query = "GoldBarcodeByIndexNo";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            //string query = "select si.* ,  sd.StoneName , sd.SQty ,sd.StoneWeight , sd.Price , it.ItemName from Stock  si"+
            //    "inner join StonesDetail sd on sd.TagNo = si.TagNo "+
            //    "inner join Item it on it.ItemId = si.ItemId order by IndexNo ASC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            DataSet ds = new DataSet();
           // BarCodeDataSet ds = new BarCodeDataSet();
            // DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            //DataTable dt1 = ds.Tables["Stock"];
            dt1 = ds.Tables.Add("Stock");
            dt1.Columns.Add("TagNo", typeof(System.String));
            dt1.Columns.Add("StockId", typeof(System.Int32));
            dt1.Columns.Add("BarCode", typeof(System.String));
            dt1.Columns.Add("TotalWeight", typeof(System.Double));
            dt1.Columns.Add("Weight", typeof(System.Double));
            dt1.Columns.Add("Pieces", typeof(System.Int32));
            dt1.Columns.Add("Karat", typeof(System.String));
            dt1.Columns.Add("StWeight", typeof(System.Double));
            dt1.Columns.Add("StWeight1", typeof(System.Double));
            dt1.Columns.Add("BdW", typeof(System.Double));
            dt1.Columns.Add("DrW", typeof(System.Double));
            dt1.Columns.Add("WasteInGm", typeof(System.Double));
            dt1.Columns.Add("SQty", typeof(System.Double));
            dt1.Columns.Add("TotalLaker", typeof(System.Double));
            dt1.Columns.Add("TotalMaking", typeof(System.Double));
            dt1.Columns.Add("TotalPrice", typeof(System.Double));
            dt1.Columns.Add("IndexNo", typeof(System.Int32));
            dt1.Columns.Add("IType", typeof(System.String));
            dt1.Columns.Add("StockId1", typeof(System.Int32));
            dt1.Columns.Add("IndexNo1", typeof(System.Int32));
            dt1.Columns.Add("IType1", typeof(System.String));
            dt1.Columns.Add("TagNo1", typeof(System.String));
            dt1.Columns.Add("BarCode1", typeof(System.String));
            dt1.Columns.Add("ItemName1", typeof(System.String));
            dt1.Columns.Add("TotalWeight1", typeof(System.Double));
            dt1.Columns.Add("Karat1", typeof(System.String));
            dt1.Columns.Add("Weight1", typeof(System.Double));
            dt1.Columns.Add("WasteInGm1", typeof(System.Double));
            dt1.Columns.Add("TotalLaker1", typeof(System.Double));
            dt1.Columns.Add("BdW1", typeof(System.Double));
            dt1.Columns.Add("DrW1", typeof(System.Double));
            dt1.Columns.Add("SQty1", typeof(System.Double));
            dt1.Columns.Add("TotalPrice1", typeof(System.Double));
            dt1.Columns.Add("Pieces1", typeof(System.Double));
            dt1.Columns.Add("TotalMaking1", typeof(System.Double));
            dt1.Columns.Add("ItemName", typeof(System.String));
            dt1.Columns.Add("Qty", typeof(System.Int32));
            dt1.Columns.Add("Qty1", typeof(System.Int32));
            dt1.Columns.Add("Price", typeof(System.Double));
            dt1.Columns.Add("Price1", typeof(System.Double));
            dt1.Columns.Add("StonePrice", typeof(System.Double));
            dt1.Columns.Add("StonePrice1", typeof(System.Double));
            dt1.Columns.Add("BeedsPrice", typeof(System.Double));
            dt1.Columns.Add("BeedsPrice1", typeof(System.Double));
            dt1.Columns.Add("Description", typeof(System.String));
            dt1.Columns.Add("Description1", typeof(System.String));
            //DataSet ds1=dataset
            #region Barcode
            DataRow r = null;
            if (dt.Rows.Count % 2 == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i % 2 == 0)
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
                        List<Stones> lst = stDAL.GetAllStonesDetail(dt.Rows[i]["TagNo"].ToString());

                        //string str = dt.Rows[i]["Name"].ToString();
                        if (lst != null)
                        {
                            Double price = 0;
                            Double BeedsPrice = 0;
                            Double StoneWeight = 0;
                            Double BeedsWeight = 0;
                            for (int m = 0; m < lst.Count; m++)
                            {
                                string str = lst[m].StoneTypeName.ToString();
                                if (str.Equals("Stones"))
                                {
                                    StoneWeight = StoneWeight + Convert.ToDouble(lst[m].StoneWeight);
                                    price = price + Convert.ToDouble(lst[m].Price);
                                    //r["BdW"] = 0;
                                    //r["DrW"] = 0;
                                }
                                else if (str.Equals("Beeds"))
                                {
                                    BeedsWeight = BeedsWeight + Convert.ToDouble(lst[m].StoneWeight);
                                    BeedsPrice = BeedsPrice + Convert.ToDouble(lst[m].Price);
                                    //r["StWeight"] = 0;
                                    //r["DrW"] = 0;
                                }
                                else if (str.Equals("Drops"))
                                {
                                    r["DrW"] = Convert.ToDouble(lst[m].StoneWeight);
                                    // price3 = price3 + Convert.ToDouble(lst[m].Price);
                                    //r["StWeight"] = 0;
                                    //r["BdW"] = 0;
                                }
                            }
                            r["StonePrice"] = price;
                            r["StWeight"] = StoneWeight;
                            r["BdW"] = BeedsWeight;
                            r["BeedsPrice"] = BeedsPrice;
                        }
                        else
                        {
                            r["BdW"] = 0;
                            r["StWeight"] = 0;
                            r["DrW"] = 0;
                        }
                        r["WasteInGm"] = dt.Rows[i]["WasteInGm"];
                        //r["SQty"] = dt.Rows[i]["SQty"];
                        r["TotalLaker"] = dt.Rows[i]["TotalLaker"];
                        r["TotalMaking"] = dt.Rows[i]["TotalMaking"];
                        r["TotalPrice"] = dt.Rows[i]["TotalPrice"];
                        r["IndexNo"] = dt.Rows[i]["IndexNo"];
                        r["ItemName"] = dt.Rows[i]["ItemName"];
                        r["Qty"] = dt.Rows[i]["Qty"];
                        r["IType"] = dt.Rows[i]["IType"];
                    }
                    else
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
                        List<Stones> lst = stDAL.GetAllStonesDetail(dt.Rows[i]["TagNo"].ToString());

                        //string str = dt.Rows[i]["Name"].ToString();
                        if (lst != null)
                        {
                            Double price1 = 0;
                            Double BeedsPrice1 = 0;
                            Double StoneWeight1 = 0;
                            Double BeedsWeight1 = 0;
                            for (int m = 0; m < lst.Count; m++)
                            {
                                string str = lst[m].StoneTypeName.ToString();
                                if (str.Equals("Stones"))
                                {
                                    StoneWeight1 = StoneWeight1 + Convert.ToDouble(lst[m].StoneWeight);
                                    price1 = price1 + Convert.ToDouble(lst[m].Price);
                                    //r["BdW"] = 0;
                                    //r["DrW"] = 0;
                                }
                                else if (str.Equals("Beeds"))
                                {
                                    BeedsWeight1 = BeedsWeight1 + Convert.ToDouble(lst[m].StoneWeight);
                                    BeedsPrice1 = BeedsPrice1 + Convert.ToDouble(lst[m].Price);
                                    //r["StWeight"] = 0;
                                    //r["DrW"] = 0;
                                }
                                else if (str.Equals("Drops"))
                                {
                                    r["DrW1"] = Convert.ToDouble(lst[m].StoneWeight);
                                    // price3 = price3 + Convert.ToDouble(lst[m].Price);
                                    //r["StWeight"] = 0;
                                    //r["BdW"] = 0;
                                }
                            }
                            r["StonePrice1"] = price1;
                            r["StWeight1"] = StoneWeight1;
                            r["BdW1"] = BeedsWeight1;
                            r["BeedsPrice1"] = BeedsPrice1;
                        }
                        else
                        {
                            r["BdW1"] = 0;
                            r["StWeight1"] = 0;
                            r["DrW1"] = 0;
                        }
                        r["WasteInGm1"] = dt.Rows[i]["WasteInGm"];
                        //r["SQty1"] = dt.Rows[i]["SQty"];
                        r["TotalLaker1"] = dt.Rows[i]["TotalLaker"];
                        r["TotalMaking1"] = dt.Rows[i]["TotalMaking"];
                        r["TotalPrice1"] = dt.Rows[i]["TotalPrice"];
                        r["IndexNo1"] = dt.Rows[i]["IndexNo"];
                        r["ItemName1"] = dt.Rows[i]["ItemName"];
                        r["Qty1"] = dt.Rows[i]["Qty"];
                        r["IType1"] = dt.Rows[i]["IType"];

                    }
                    if (i % 2 != 0)
                    {
                        dt1.Rows.Add(r);
                        r = null;

                    }

                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i % 2 != 0)
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
                        List<Stones> lst = stDAL.GetAllStonesDetail(dt.Rows[i]["TagNo"].ToString());

                        //string str = dt.Rows[i]["Name"].ToString();
                        if (lst != null)
                        {
                            Double price2 = 0;
                            Double BeedsPrice2 = 0;
                            Double StoneWeight2 = 0;
                            Double BeedsWeight2 = 0;
                            for (int m = 0; m < lst.Count; m++)
                            {
                                string str = lst[m].StoneTypeName.ToString();
                                if (str.Equals("Stones"))
                                {
                                    StoneWeight2 = StoneWeight2 + Convert.ToDouble(lst[m].StoneWeight);
                                    price2 = price2 + Convert.ToDouble(lst[m].Price);
                                    //r["BdW"] = 0;
                                    //r["DrW"] = 0;
                                }
                                else if (str.Equals("Beeds"))
                                {
                                    BeedsWeight2 = BeedsWeight2 + Convert.ToDouble(lst[m].StoneWeight);
                                    BeedsPrice2 = BeedsPrice2 + Convert.ToDouble(lst[m].Price);
                                    //r["StWeight"] = 0;
                                    //r["DrW"] = 0;
                                }
                                else if (str.Equals("Drops"))
                                {
                                    r["DrW"] = Convert.ToDouble(lst[m].StoneWeight);
                                    // price3 = price3 + Convert.ToDouble(lst[m].Price);
                                    //r["StWeight"] = 0;
                                    //r["BdW"] = 0;
                                }
                            }
                            r["StonePrice"] = price2;
                            r["StWeight"] = StoneWeight2;
                            r["BdW"] = BeedsWeight2;
                            r["BeedsPrice"] = BeedsPrice2;
                        }
                        else
                        {
                            r["BdW"] = 0;
                            r["StWeight"] = 0;
                            r["DrW"] = 0;
                        }

                        r["WasteInGm"] = dt.Rows[i]["WasteInGm"];
                        //r["SQty"] = dt.Rows[i]["SQty"];
                        r["TotalLaker"] = dt.Rows[i]["TotalLaker"];
                        r["TotalMaking"] = dt.Rows[i]["TotalMaking"];
                        r["TotalPrice"] = dt.Rows[i]["TotalPrice"];
                        r["IndexNo"] = dt.Rows[i]["IndexNo"];
                        r["ItemName"] = dt.Rows[i]["ItemName"];
                        r["Qty"] = dt.Rows[i]["Qty"];
                        r["IType"] = dt.Rows[i]["IType"];




                    }
                    else
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
                        List<Stones> lst = stDAL.GetAllStonesDetail(dt.Rows[i]["TagNo"].ToString());
                        //string str = dt.Rows[i]["Name"].ToString();
                        if (lst != null)
                        {
                            Double price3 = 0;
                            Double BeedsPrice3 = 0;
                            Double StoneWeight3 = 0;
                            Double BeedsWeight3 = 0;
                            for (int m = 0; m < lst.Count; m++)
                            {
                                string str = lst[m].StoneTypeName.ToString();
                                if (str.Equals("Stones"))
                                {
                                    StoneWeight3 = StoneWeight3 + Convert.ToDouble(lst[m].StoneWeight);
                                    price3 = price3 + Convert.ToDouble(lst[m].Price);
                                    //r["BdW"] = 0;
                                    //r["DrW"] = 0;
                                }
                                else if (str.Equals("Beeds"))
                                {
                                    BeedsWeight3 = BeedsWeight3 + Convert.ToDouble(lst[m].StoneWeight);
                                    BeedsPrice3 = BeedsPrice3 + Convert.ToDouble(lst[m].Price);
                                    //r["StWeight"] = 0;
                                    //r["DrW"] = 0;
                                }
                                else if (str.Equals("Drops"))
                                {
                                    r["DrW1"] = Convert.ToDouble(lst[m].StoneWeight);
                                    // price3 = price3 + Convert.ToDouble(lst[m].Price);
                                    //r["StWeight"] = 0;
                                    //r["BdW"] = 0;
                                }
                            }
                            r["StonePrice1"] = price3;
                            r["StWeight1"] = StoneWeight3;
                            r["BdW1"] = BeedsWeight3;
                            r["BeedsPrice1"] = BeedsPrice3;
                        }
                        else
                        {
                            r["BdW1"] = 0;
                            r["StWeight1"] = 0;
                            r["DrW1"] = 0;
                        }


                        r["WasteInGm1"] = dt.Rows[i]["WasteInGm"];
                        //r["SQty1"] = dt.Rows[i]["SQty"];
                        r["TotalLaker1"] = dt.Rows[i]["TotalLaker"];
                        r["TotalMaking1"] = dt.Rows[i]["TotalMaking"];
                        r["TotalPrice1"] = dt.Rows[i]["TotalPrice"];
                        r["IndexNo1"] = dt.Rows[i]["IndexNo"];
                        r["ItemName1"] = dt.Rows[i]["ItemName"];
                        r["Qty1"] = dt.Rows[i]["Qty"];
                        r["IType1"] = dt.Rows[i]["IType"];
                    }
                    if (i % 2 != 0)
                    {
                        dt1.Rows.Add(r);
                        r = null;

                    }

                }

            }
            #endregion
            int s = 0;
            s = dt1.Rows.Count;
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = path + "\\Reports\\GoldBarCode.rpt";
            //string reportPath = "Reports/GoldBarCode.rpt";
            ReportDocument report = new ReportDocument(); 
            //printDialog1.Document = report;
            report.Load(reportPath);
            //report.Database.Tables["BarCodeRpt"].SetDataSource(ds.Tables[0]);
            report.SetDataSource(ds.Tables["Stock"]);
           // crystalReportViewer1.ReportSource = report;
            axCrystalActiveXReportViewer1.ReportSource = report;
           // axCrystalActiveXReportViewer1.ReportSource = report;
            //crystalReportViewer1.RefreshReport();
            axCrystalActiveXReportViewer1.Refresh();
        }

       
    }
}
