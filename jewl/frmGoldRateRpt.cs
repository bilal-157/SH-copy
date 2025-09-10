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
using DAL;
using System.IO;
using System.Windows.Forms;

namespace jewl
{
    public partial class frmGoldRateRpt : Form
    {
        public DateTime tDate, fDate;
        public string selectQuery;
        public frmGoldRateRpt()
        {
            InitializeComponent();
        }

        private void frmGoldRateRpt_Load(object sender, EventArgs e)
        {
            try
            {
                //                string query = "select stk.WorkerId,stk.Qty,stk.Description,stk.ItemId,stk.TagNo,stk.StockDate,stk.NetWeight,stk.WasteInGm , wrk.WorkerId,wrk.WorkerName,itm.ItemId,itm.ItemName from Stock stk INNER JOIN Worker wrk on  stk.WorkerId=wrk.WorkerId  INNER JOIN Item itm on stk.ItemId=itm.ItemId ";
                SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                conn.Open();
                SqlCommand command = new SqlCommand("sp_GoldRates", conn);

                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fDate;
                command.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = tDate;
                DataSet ds = new DataSet();
                da.Fill(ds, "sp_GoldRates");
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\GoldRates.rpt";
                //string reportPath = "../../Profit Loss.rpt";
                // ReportDocument report = new ReportDocument();
                ReportDocument report = new ReportDocument();
                report.Load(reportPath);

                report.SetDataSource(ds);
               // report.RecordSelectionFormula = selectQuery;
                this.crystalReportViewer1.ReportSource = report;
                this.crystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            { //throw ex;
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
