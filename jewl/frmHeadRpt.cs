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
    public partial class frmHeadRpt : Form
    {
        public int HeadCode;
        public frmHeadRpt()
        {
            InitializeComponent();
        }

        private void frmHeadRpt_Load(object sender, EventArgs e)
        {
            try
            {
                //string query = "select (select GroupName from groupaccount where groupcode='" + this.GroupCode + "') as GroupName,(select sum(Cr)from vouchers where SubGroupCode=v.SubGroupCode ) as Cr, (select sum(dr)from vouchers where SubGroupCode=v.SubGroupCode ) as Dr ,(select sum(dr)-sum(cr)from vouchers where SubGroupCode=v.SubGroupCode )as Balance  , (select SubGroupName from SubGroupAccount where SubGroupCode=v.SubGroupCode) as SubGroupName ,v.SubGroupCode  from vouchers  v where v.SubGroupCode  like '" + this.GroupCode + "" + "%' group by v.SubGroupCode";
                string query = "select (select HeadName from HeadAccount where Headcode="+this.HeadCode+") as HeadName,(select sum(Cr)from vouchers where GroupCode=v.GroupCode ) as Cr, (select sum(dr)from vouchers where GroupCode=v.GroupCode ) as Dr ,(select sum(dr)-sum(cr)from vouchers where GroupCode=v.GroupCode )as Balance  , (select GroupName from GroupAccount where GroupCode=v.GroupCode) as GroupName ,v.GroupCode  from vouchers  v where v.GroupCode  like '"+this.HeadCode+""+"%' group by v.GroupCode";
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                //cmd.Parameters.Add("@ParentCode", SqlDbType.NVarChar).Value = "%"+this.ParentCode+"%";
                //cmd.Parameters.Add("@ParentName", SqlDbType.NVarChar).Value = this.ParentName;
                // cmd.Parameters.Add("@DTo", SqlDbType.DateTime).Value = this.Dt;
                DataSet ds = new DataSet();
                da.Fill(ds, "HeadLedger");
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\HeadRpt.rpt";
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
    }
}
