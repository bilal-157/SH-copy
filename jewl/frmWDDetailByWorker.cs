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
    public partial class frmWDDetailByWorker : Form
    {
        public int workerid ;
        public frmWDDetailByWorker()
        {
            InitializeComponent();
        }

        private void frmWDDetailByWorker_Load(object sender, EventArgs e)
        {
            try
            {
                string query = @"select w.*,sd.StoneWeight, (Select isnull(Sum(PWeight),0) from workerGold_Trans Where WorkerId = w.WorkerId and [Status] = 'GoldReceive')'RPW',
                (Select isnull(Sum(PWeight),0) from workerGold_Trans Where WorkerId = w.WorkerId and [Status] = 'GoldGiven')'GPW',
                sd.Price ,sd.Rate ,st.Name'StoneType' ,sn.StoneName , wd.WorkerName from workerGold_Trans w inner join worker wd on wd.WorkerId=w.WorkerId 
                left outer join StonesDetail sd on sd.TranId = w.TranId 
                left outer join StonesType st on st.StoneTypeId = sd.StoneTypeId 
                left outer join StonesName sn on sn.StoneId = sd.StoneId where w.WorkerId = " + workerid + " order by w.EntryDate";
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand(query , con);

                cmd.CommandType = CommandType.Text ;
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                con.Open();
                DataSet ds = new DataSet();
                da.Fill(ds, "WorkerStonesRpt");

                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\WDDetailRptbyWorker.rpt";
       
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
