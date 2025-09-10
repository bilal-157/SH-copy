using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using DAL;


namespace jewl
{
    public partial class frmRepairingRpt : Form
    {
        public string iType;
        public string selectQuery;
        public int id = 0;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        ReportDocument report;
        string path = "";

        public frmRepairingRpt()
        {
            InitializeComponent();
        }

        private void frmRepairingRpt_Load(object sender, EventArgs e)
        {
            if (id == 0)
            {
                try
                {
                    con = new SqlConnection(DALHelper.ConnectionString);
                    con.Open();
                    cmd = new SqlCommand(selectQuery, con);
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "rptReparing");
                    path = Path.GetDirectoryName(Application.ExecutablePath);
                    report = new ReportDocument();
                    report.Load(path + "\\Reports\\Repairing Estimate.rpt");
                    report.SetDataSource(ds.Tables["rptReparing"]);
                    this.crystalReportViewer1.ReportSource = report;
                    this.crystalReportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (id == 1)
            {
                try
                {
                    con = new SqlConnection(DALHelper.ConnectionString);
                    cmd = new SqlCommand("CompleteRepairing", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    con.Open();
                    ds = new DataSet();
                    da.Fill(ds, "CompleteRepairing");
                    report = new ReportDocument();
                    path = Path.GetDirectoryName(Application.ExecutablePath);
                    report.Load(path + "\\Reports\\CompleteRepairing.rpt");
                    report.SetDataSource(ds);
                    crystalReportViewer1.SelectionFormula = selectQuery;
                    crystalReportViewer1.ReportSource = report;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}

