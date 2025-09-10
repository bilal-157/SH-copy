using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using DAL;
using System.IO;

namespace jewl
{
    public partial class SampleByItmRpt : Form
    {
        public bool isComplete;
        public string selectQuery = "";
        public SampleByItmRpt()
        {
            InitializeComponent();
        }

        private void SampleByItmRpt_Load(object sender, EventArgs e)
        {
            try
            {
                //                string query = "select stk.WorkerId,stk.Qty,stk.Description,stk.ItemId,stk.TagNo,stk.StockDate,stk.NetWeight,stk.WasteInGm , wrk.WorkerId,wrk.WorkerName,itm.ItemId,itm.ItemName from Stock stk INNER JOIN Worker wrk on  stk.WorkerId=wrk.WorkerId  INNER JOIN Item itm on stk.ItemId=itm.ItemId ";
                SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                
                SqlCommand command = new SqlCommand("sp_SampleByItem", conn);

                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                conn.Open();
                //command.Parameters.Add("@ItemId", SqlDbType.Int).Value = selectQuery;

                DataSet ds = new DataSet();
                da.Fill(ds, "SampleReport");

                //string reportPath = "../../Reports/ItemSample.rpt";
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path+ "\\Reports\\ItemSample.rpt";
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
