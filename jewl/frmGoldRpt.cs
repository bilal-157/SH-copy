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
using BusinesEntities;
using System.IO;

namespace jewl
{
    public partial class frmGoldRpt : Form
    {
        public DateTime df;
        public DateTime dt;
        AccountDAL acDAL = new AccountDAL();
        public frmGoldRpt()
        {
            InitializeComponent();
        }

        private void frmGoldRpt_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand("GoldTransactionRpt", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                cmd.Parameters.Add("@dt", SqlDbType.DateTime).Value = this.dt;
                cmd.Parameters.Add("@df", SqlDbType.DateTime).Value = this.df;
                ChildAccount ca = new ChildAccount();
                ca = acDAL.GetAccount(1, "Current Asset", "Gold In Hand");
                cmd.Parameters.Add("@PCode", SqlDbType.NVarChar).Value = ca.ChildCode;
                ca = acDAL.GetAccount(1, "Current Asset", "Gold In Hand");
                cmd.Parameters.Add("@UCode", SqlDbType.NVarChar).Value = ca.ChildCode;
                DataSet ds = new DataSet();
                da.Fill(ds, "Command");
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\GoldRpt.rpt";
                ReportDocument report = new ReportDocument();
                report.Load(reportPath);
                report.SetDataSource(ds);
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
