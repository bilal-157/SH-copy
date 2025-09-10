using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.IO;

namespace jewl
{
    public partial class frmLooseStonesRpt : Form
    {
        public string selectquery;
        public frmLooseStonesRpt()
        {
            InitializeComponent();
        }

        private void frmLooseStonesRpt_Load(object sender, EventArgs e)
        {
            try
            {
               
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand("LooseStonesRpt", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                //DataTable dt1 = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(ds,"LooseStones");
              
               
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                //string reportPath = path + "/LooseStonesRpt.rpt";
                string reportPath = path+ "\\Reports\\LooseStonesRpt.rpt";
                //string reportPath = "../../StockSummaryRpt.rpt";
                ReportDocument report = new ReportDocument();
                //ReportDocument report = new ReportDocument();
                //report.SetDataSource(ds);
                report.Load(reportPath);
                report.SetDataSource(ds);
                crystalReportViewer1.SelectionFormula = selectquery;
                this.crystalReportViewer1.ReportSource = report;
                this.crystalReportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                //throw ex;
                MessageBox.Show(ex.ToString());
            }

            }


        }
    }

