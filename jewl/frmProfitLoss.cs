using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using DAL;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace jewl
{
    public partial class frmProfitLoss : Form
    {
        public string selectQuery = "";
        public frmProfitLoss()
        {
            InitializeComponent();
        }

        private void frmProfitLoss_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                conn.Open();
                SqlCommand command = new SqlCommand("ProfitLoss", conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "ProfitLoss");
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\Profit Loss.rpt";
                ReportDocument report = new ReportDocument();
                report.Load(reportPath);
                report.SetDataSource(ds);
                crystalReportViewer1.SelectionFormula = selectQuery;
                this.crystalReportViewer1.ReportSource = report;
            }
            catch (Exception ex)
            { //throw ex;
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
