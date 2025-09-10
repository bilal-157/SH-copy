using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Windows.Forms;
using DAL;
using System.IO;

namespace jewl
{
    public partial class frmTagHistoryRpt : Form
    {

        public string selectquery="";      

        public frmTagHistoryRpt()
        {
            InitializeComponent();
        }
        
        private void frmTagHistoryRpt_Load(object sender, EventArgs e)
        {
            try
            {
                //string query = "SELECT sa.*,s.Karat,s.TotalPrice,s.Discount,s.NetAmount,s.TagNo,i.ItemName,sgi.SGItmName,si.SubItmName,s.NetWeight,s.WasteInGm,s.TotalWeight,s.Qty,s.[Description],w.WorkerName, s.StockDate,sn.StoneName,sd.StoneWeight,sd.Rate,sd.Price,ci.Name,ci.Address,ci.Mobile,(select picture from StandardPicsDB.dbo.jewlpictures where picid=(select max(picid) from StandardPicsDB.dbo.jewlpictures where tagno=s.tagno) and tagno=s.tagno) as Picture FROM Stock s INNER JOIN Item i ON s.ItemId=i.ItemId LEFT OUTER JOIN SubGroupItem sgi ON s.SubGItmId=sgi.SubGItmId LEFT OUTER JOIN SubItems si ON s.SubItemId=si.SubItemId LEFT OUTER JOIN Worker w ON s.WorkerId=w.WorkerId  LEFT OUTER JOIN StonesDetail sd ON s.TagNo=sd.TagNo LEFT OUTER join StonesName sn on sd.StoneId=sn.StoneId  left outer join Sale sa on s.SaleNo=sa.SaleNo left outer join CustomerInfo ci on sa.CustAccountNo=ci.AccountCode left outer join SaleMan sm on sa.SaleManId=sm.Id WHERE s.TagNo='" + tagNo + "'";
                //SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                //conn.Open();
                //SqlCommand command = new SqlCommand(query, conn);
                //command.CommandType = CommandType.Text ;
                //SqlDataAdapter da = new SqlDataAdapter(command);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "Command");
                //string path = Path.GetDirectoryName(Application.ExecutablePath);
                //string reportPath = path + "\\Reports\\TagHistory.rpt";
                //ReportDocument report = new ReportDocument();
                //report.Load(reportPath);
                //report.SetDataSource(ds);
                //this.crystalReportViewer1.ReportSource = report;
                //this.crystalReportViewer1.RefreshReport();
                SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                conn.Open();
                SqlCommand command = new SqlCommand("TagHistoryRpt", conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds, "TagHistoryRpt");
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\TagHistory.rpt";
                ReportDocument report = new ReportDocument();
                report.Load(reportPath);
                report.SetDataSource(ds);
                this.crystalReportViewer1.SelectionFormula = selectquery;
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
