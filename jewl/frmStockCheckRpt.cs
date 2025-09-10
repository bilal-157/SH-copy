using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DAL;
using BusinesEntities;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Windows.Forms;
using System.IO;


namespace jewl
{
    public partial class frmStockCheckRpt : Form
    {
        public int sessionid, itemId, k;
        UtilityDAL utlDAL = new UtilityDAL();
        public frmStockCheckRpt()
        {
            InitializeComponent();
        }

        private void frmStockCheckRpt_Load(object sender, EventArgs e)
        {
            SqlCommand cmd;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            if (k == 0)
            {
                cmd = new SqlCommand("Scaned", con);
                cmd.Parameters.Add("@SessionId", SqlDbType.Int).Value = sessionid;
            }
            else
            {
                cmd = new SqlCommand("ScanedByItemId", con);
                cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = itemId;
            }
            cmd.CommandType = CommandType.StoredProcedure;            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "Scaned");
            ReportDocument report = new ReportDocument();
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = path + "\\Reports\\StockCheck.rpt";
            utlDAL.VerifyReports(reportPath, report);
            report.SetDataSource(ds);
            crystalReportViewer1.ReportSource = report;
            crystalReportViewer1.RefreshReport();
        }
    }
}
