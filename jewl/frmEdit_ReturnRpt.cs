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
    public partial class frmEdit_ReturnRpt : Form
    {
        public DateTime Datet, datef;
        UtilityDAL utlDAL = new UtilityDAL();
        public int id = 0;
        public DateTime Df;
        public DateTime Dt;
        public frmEdit_ReturnRpt()
        {
            InitializeComponent();
        }

        private void frmEdit_ReturnRpt_Load(object sender, EventArgs e)
        {
            if (id == 1)
            {
                try
                {
                    SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                    conn.Open();
                    SqlCommand command = new SqlCommand("EditRpt", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    string path = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = path + "\\Reports\\EditRpt.rpt";
                    ReportDocument report = new ReportDocument();
                    report.Load(reportPath);
                    utlDAL.VerifyReports(reportPath, report);
                    this.crystalReportViewer1.ReportSource = report;
                    this.crystalReportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (id == 2)
            {
                try
                {
                    //SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                    //conn.Open();
                    //SqlCommand command = new SqlCommand("ReturnRpt", conn);
                    //command.CommandType = CommandType.StoredProcedure;
                    //string path = Path.GetDirectoryName(Application.ExecutablePath);
                    //string reportPath = path + "\\Reports\\ReturnRpt.rpt";
                    //ReportDocument report = new ReportDocument();
                    //report.Load(reportPath);
                    //utlDAL.VerifyReports(reportPath, report);
                    //this.crystalReportViewer1.ReportSource = report;
                    //this.crystalReportViewer1.RefreshReport();
                    SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                    conn.Open();
                    SqlCommand command = new SqlCommand("ReturnRpt", conn);

                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    // , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                    command.Parameters.Add("@Datef", SqlDbType.DateTime).Value = Df;
                    command.Parameters.Add("@Datet", SqlDbType.DateTime).Value = Dt;
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ReturnRpt");
                    string path = Path.GetDirectoryName(Application.ExecutablePath);
                    string reportPath = path + "\\Reports\\ReturnRpt.rpt";
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
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
