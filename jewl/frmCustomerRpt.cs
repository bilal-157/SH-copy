using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DAL;
using System.IO;

namespace jewl
{
    public partial class frmCustomerRpt : Form
    {
        public int id = 0;
        public DateTime Datef;
        public DateTime Datet;
        UtilityDAL utiDAL = new UtilityDAL();
        public frmCustomerRpt()
        {
            InitializeComponent();
        }

        private void frmCustomerRpt_Load(object sender, EventArgs e)
        {

            try
            {
                if (id == 0)
                {
                    SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                    conn.Open();
                    SqlCommand command = new SqlCommand("CustomerRpt", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "CustomerRpt");
                    string path = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = path + "\\Reports\\CustomerRpt.rpt";
                    ReportDocument report = new ReportDocument();
                    report.Load(reportPath);
                    report.SetDataSource(ds);
                    this.crystalReportViewer1.ReportSource = report;
                    this.crystalReportViewer1.RefreshReport();
                }
                if (id == 1)
                {
                    SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                    conn.Open();
                    SqlCommand command = new SqlCommand("GetDailyCashGoldcard", conn);
                    command.CommandType = CommandType.StoredProcedure;
                   // command.Parameters.Add("@DFrom", SqlDbType.DateTime).Value = Datef;
                    //command.Parameters.Add("@DTo", SqlDbType.DateTime).Value = Datet;
                    string path = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = path + "\\Reports\\CashGoldCard.rpt";
                    ReportDocument report = new ReportDocument();
                    report.Load(reportPath);
                    report.SetParameterValue("@DFrom", Datef);
                    report.SetParameterValue("@DTo", Datet);
                    utiDAL.VerifyReports(reportPath, report);
                    this.crystalReportViewer1.ReportSource = report;
                    //this.crystalReportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
