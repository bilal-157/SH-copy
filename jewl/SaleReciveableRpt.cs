using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using DAL;

namespace jewl
{
    public partial class SaleReciveableRpt : Form
    {

        public string iType;
        public string selectQuery;
        public SaleReciveableRpt()
        {
            InitializeComponent();
        }

        private void SaleReciveableRpt_Load(object sender, EventArgs e)
        {
             try
            {
                //                string query = "select stk.WorkerId,stk.Qty,stk.Description,stk.ItemId,stk.TagNo,stk.StockDate,stk.NetWeight,stk.WasteInGm , wrk.WorkerId,wrk.WorkerName,itm.ItemId,itm.ItemName from Stock stk INNER JOIN Worker wrk on  stk.WorkerId=wrk.WorkerId  INNER JOIN Item itm on stk.ItemId=itm.ItemId ";
                SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                conn.Open();
                SqlCommand command = new SqlCommand("sp_SaleItemDetail", conn);
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.Add("@SaleNo", SqlDbType.Int).Value = 2;
                SqlDataAdapter da = new SqlDataAdapter(command);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                //command.Parameters.Add("@WorkerId", SqlDbType.Int).Value = 2;

                DataSet ds = new DataSet();
                da.Fill(ds, "sp_SaleItemDetail");

                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path+ "\\Reports\\SaleReciveableReport.rpt";
                // creat new object for load and record selection 
                ReportDocument report = new ReportDocument();
                report.Load(reportPath);

                report.SetDataSource(ds);
                if (iType != "All")
                {
                    crystalReportViewer1.SelectionFormula = selectQuery;
                }
                this.crystalReportViewer1.ReportSource = report;
                this.crystalReportViewer1.RefreshReport();
                //SaleInvoiceRpt .sta
            }
            catch (Exception ex)
            { //throw ex;
                MessageBox.Show(ex.ToString());
            }
        
        }
    }
}
