using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DAL;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace jewl
{
    public partial class frmStockBalance : Form
    {
        UtilityDAL uDAL = new UtilityDAL();
        public frmStockBalance()
        {
            InitializeComponent();
        }

        private void StockComDetailbyDateRangeRpt_Load(object sender, EventArgs e)
        {
            try
            {
                #region Detail

                // DataSet ds = new DataSet("StockDateRange");
               // DataTable dt3 = new DataTable();
               // dt3 = ds.Tables.Add("StockDateRange");
               // dt3.Columns.Add("Datef", typeof(System.DateTime));
               // dt3.Columns.Add("Datet", typeof(System.DateTime));
               // DataRow r = null;
               // r = dt3.NewRow();
               // r["Datef"] = df;
               // r["Datet"] = dt;
               // dt3.Rows.Add(r);
               // SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
               // SqlCommand cmd = new SqlCommand("StockInByDateRange", con);

               // cmd.Parameters.Add("@Datef", SqlDbType.DateTime ).Value = df;
               // cmd.Parameters.Add("@Datet", SqlDbType.DateTime ).Value = dt;
               // cmd.CommandType = CommandType.StoredProcedure;

               // SqlDataAdapter da = new SqlDataAdapter(cmd);
               // con.Open();
               //// DataSet ds = new DataSet();
               // da.Fill(ds, "StockRpt");
               // string path = Path.GetDirectoryName(Application.ExecutablePath);
               // string reportPath = path + "\\Reports\\StockComDetailbyDateRange.rpt";
               // ReportDocument report = new ReportDocument();
               // report.Load(reportPath);
               // report.SetDataSource(ds);
               // this.crystalReportViewer1.ReportSource = report;
               // this.crystalReportViewer1.RefreshReport();
             #endregion
                 SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                conn.Open();
                SqlCommand command = new SqlCommand("sp_StockBalance", conn);

                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                DataSet ds = new DataSet();
                da.Fill(ds, "StockBalance");
                
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\StockBalance.rpt";
                // ReportDocument report = new ReportDocument();

                ReportDocument report = new ReportDocument();   
                                         
                report.Load(reportPath);               

                report.SetDataSource(ds);
                uDAL.VerifyReports(reportPath, report);
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
