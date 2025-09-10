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
    public partial class frmProfitLossByItm : Form
    {
        public string selectQuery;
        public frmProfitLossByItm()
        {
            InitializeComponent();
        }

        private void frmProfitLossByItm_Load(object sender, EventArgs e)
        {
            try
            {
                //                string query = "select stk.WorkerId,stk.Qty,stk.Description,stk.ItemId,stk.TagNo,stk.StockDate,stk.NetWeight,stk.WasteInGm , wrk.WorkerId,wrk.WorkerName,itm.ItemId,itm.ItemName from Stock stk INNER JOIN Worker wrk on  stk.WorkerId=wrk.WorkerId  INNER JOIN Item itm on stk.ItemId=itm.ItemId ";
                SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                conn.Open();
                SqlCommand command = new SqlCommand("ProfitLoss", conn);

                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                //command.Parameters.Add("@date", SqlDbType.DateTime).Value = "03/22/2018";

                DataSet ds = new DataSet();
                da.Fill(ds, "ProfitLoss");
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\ProfitLossByItem.rpt";
                //string reportPath = "../../Profit Loss.rpt";
                // ReportDocument report = new ReportDocument();
                ReportDocument report = new ReportDocument();
                report.Load(reportPath);

                report.SetDataSource(ds);
                crystalReportViewer1.SelectionFormula = selectQuery;
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
