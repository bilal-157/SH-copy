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
using System.Configuration;
using DAL;

namespace jewl
{
    public partial class StockReportFrm : Form
    {
        public StockReportFrm()
        {
            InitializeComponent();
        }

        private void StockReportFrm_Load(object sender, EventArgs e)
        {
            try
            {
//                string query = "select stk.WorkerId,stk.Qty,stk.Description,stk.ItemId,stk.TagNo,stk.StockDate,stk.NetWeight,stk.WasteInGm , wrk.WorkerId,wrk.WorkerName,itm.ItemId,itm.ItemName from Stock stk INNER JOIN Worker wrk on  stk.WorkerId=wrk.WorkerId  INNER JOIN Item itm on stk.ItemId=itm.ItemId ";
                SqlConnection conn = new SqlConnection(DALHelper.ConnectionString );
                conn.Open();
               SqlCommand command = new SqlCommand("GetStockReport", conn );
               command.CommandType= CommandType.StoredProcedure ;
               SqlDataAdapter da = new SqlDataAdapter(command);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());


                DataSet ds = new DataSet();
                da.Fill(ds);

                string firstColumn ="", secondColumn = string.Empty ;
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    if (i % 2 == 0)
                    {
                        firstColumn += "Code -- " + ds.Tables[0].Rows[i]["TagNo"].ToString() + "<BR>";

                    }
                    else
                    {
                        secondColumn += "Code -- " + ds.Tables[0].Rows[i]["TagNo"].ToString() + "<BR>";
                    }
                }

            string reportPath = "../../StockReport22.rpt";
           // ReportDocument report = new ReportDocument();
            ReportDocument report = new ReportDocument();
            report.Load(reportPath);

            //report.SetDataSource(ds);
            //report.Database.Tables["StockFirstRecord"].SetDataSource(ds.Tables[0]);
            //report.Database.Tables["StockLastRecord"].SetDataSource(ds.Tables[1]);
            report.DataDefinition.FormulaFields["FirstColumn"].Text  = "'"+firstColumn +"'";
            report.DataDefinition.FormulaFields["SecondColumn"].Text = "'"+secondColumn + "'";

            this.crystalReportViewer1.ReportSource = report;
            this.crystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            { //throw ex;
            MessageBox.Show(ex.ToString());
            }
           
            //crystalReportViewer1.ReportSource = report;
            //this.crystalReportViewer1.RefreshReport();
        }
    }
}
