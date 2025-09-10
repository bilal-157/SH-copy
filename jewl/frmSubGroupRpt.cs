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
    public partial class frmSubGroupRpt : Form
    {
        public string SubGCode;
        public string SubGName;
        public frmSubGroupRpt()
        {
            InitializeComponent();
        }

        private void frmSubGroupRpt_Load(object sender, EventArgs e)
        {
            try
            {
                //string query = "select (select parentname from parentaccount where parentcode='" + this.ParentCode + "') as ParentName,(select sum(Cr)from vouchers where AccountCode=v.Accountcode ) as Cr, (select sum(dr)from vouchers where AccountCode=v.Accountcode ) as Dr ,(select sum(dr)-sum(cr)from vouchers where AccountCode=v.Accountcode )as Balance  , v.AccountCode ,v.AccountName from vouchers  v where v.AccountCode  like '" + this.ParentCode + "" + "%' group by v.AccountCode,v.AccountName";
                string query = "select (select SubGroupName from subgroupaccount where subgroupcode='" + this.SubGCode + "') as SubGroupName,(select sum(Cr)from vouchers where ParentCode=v.ParentCode ) as Cr, (select sum(dr)from vouchers where ParentCode=v.ParentCode ) as Dr ,(select sum(dr)-sum(cr)from vouchers where ParentCode=v.ParentCode )as Balance  , (select parentname from parentaccount where parentcode=v.parentcode) as ParentName ,v.ParentCode  from vouchers  v where v.ParentCode  like '" + this.SubGCode + "" + "%' group by v.ParentCode";
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                //cmd.Parameters.Add("@ParentCode", SqlDbType.NVarChar).Value = "%"+this.ParentCode+"%";
                //cmd.Parameters.Add("@ParentName", SqlDbType.NVarChar).Value = this.ParentName;
                // cmd.Parameters.Add("@DTo", SqlDbType.DateTime).Value = this.Dt;
                DataSet ds = new DataSet();
                da.Fill(ds, "SGroupLedger");
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\SubGroupRpt.rpt";
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
