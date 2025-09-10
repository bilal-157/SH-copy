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
using System.IO;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class SalesManSalaryRpt : Form
    {
        public string selectQuery;
        public SalesManSalaryRpt()
        {
            InitializeComponent();
        }

        private void SalesManSalaryRpt_Load(object sender, EventArgs e)
        {
            try
            {
                string Query = "select sm.Id,sa.GDate,sa.Salary,MONTH(sa.SalaryMonth)'Month',YEAR(sa.SalaryMonth)'Year',sm.Name from SaleMan_Salary sa inner join SaleMan sm on sa.SmId=sm.Id";

                SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                conn.Open();
                SqlCommand command = new SqlCommand(Query, conn);
                //command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.Add("@SaleNo", SqlDbType.Int).Value = 2;
                SqlDataAdapter da = new SqlDataAdapter(command);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                //command.Parameters.Add("@WorkerId", SqlDbType.Int).Value = 2;

                DataSet ds = new DataSet();
                da.Fill(ds, "Command");

                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\SalManSalaryRpt.rpt";
                //string reportPath = "../../Reports/SaleSummaryReport.rpt";
                // creat new object for load and record selection 
                ReportDocument report = new ReportDocument();
                report.Load(reportPath);

                report.SetDataSource(ds.Tables["Command"]);

                crystalReportViewer1.SelectionFormula = selectQuery;
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
