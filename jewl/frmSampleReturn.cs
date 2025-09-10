using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DAL;
using System.IO;

namespace jewl
{
    public partial class frmSampleReturn : Form
    {
        public string selectQuery = "";
        public frmSampleReturn()
        {
            InitializeComponent();
        }

        private void SampleByDateRpt_Load(object sender, EventArgs e)
        {
            try
            {
                //                string query = "select stk.WorkerId,stk.Qty,stk.Description,stk.ItemId,stk.TagNo,stk.StockDate,stk.NetWeight,stk.WasteInGm , wrk.WorkerId,wrk.WorkerName,itm.ItemId,itm.ItemName from Stock stk INNER JOIN Worker wrk on  stk.WorkerId=wrk.WorkerId  INNER JOIN Item itm on stk.ItemId=itm.ItemId ";
                SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                conn.Open();
                SqlCommand command = new SqlCommand("sp_SampleReturn", conn);

                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                //command.Parameters.Add("@date", SqlDbType.DateTime).Value = "03/22/2018";

                DataSet ds = new DataSet();
                da.Fill(ds, "SampleReport");
                //string reportPath = "../../Reports/SampleReturn.rpt";
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\SampleReturn.rpt";
                // ReportDocument report = new ReportDocument();
                ReportDocument report = new ReportDocument();
                report.Load(reportPath);

                report.SetDataSource(ds);
                if (!string.IsNullOrEmpty(selectQuery))
                    crystalReportViewer1.SelectionFormula = selectQuery;
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
