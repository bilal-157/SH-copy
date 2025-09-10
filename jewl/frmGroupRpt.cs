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
    public partial class frmGroupRpt : Form
    {
        public string GroupCode;
        public DateTime DFrom;
        public DateTime DTo;
        public frmGroupRpt()
        {
            InitializeComponent();
        }

        private void frmGroupRpt_Load(object sender, EventArgs e)
        {
            try
            {
                //string query = "select (select GroupName from groupaccount where groupcode='"+this.GroupCode+"') as GroupName,(select sum(Cr)from vouchers where SubGroupCode=v.SubGroupCode ) as Cr, (select sum(dr)from vouchers where SubGroupCode=v.SubGroupCode ) as Dr ,(select sum(dr)-sum(cr)from vouchers where SubGroupCode=v.SubGroupCode )as Balance  , (select SubGroupName from SubGroupAccount where SubGroupCode=v.SubGroupCode) as SubGroupName ,v.SubGroupCode  from vouchers  v where v.SubGroupCode  like '"+this.GroupCode+""+"%' group by v.SubGroupCode";
                string query = "GroupLedger";
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                cmd.Parameters.Add("@GroupCode", SqlDbType.NVarChar).Value = this.GroupCode;
                cmd.Parameters.Add("@DFrom", SqlDbType.DateTime).Value = this.DFrom;
                cmd.Parameters.Add("@DTo", SqlDbType.DateTime).Value = this.DTo;
                DataSet ds = new DataSet();
                da.Fill(ds, "GroupLedger");
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\GrLedger.rpt";
                // ReportDocument report = new ReportDocument();
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

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
