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
using System.Drawing.Printing;

namespace jewl
{
    public partial class WorkerDealingBill : Form
    {

        public string selectQuery;
        public int BillNo, rpt = 0;
        ReportDocument report;
        UtilityDAL utlDAL = new UtilityDAL();
        public WorkerDealingBill()
        {
            InitializeComponent();
        }

        private void WorkerDealingBill_Load(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
                conn.Open();
                SqlCommand command = new SqlCommand("WorkerDealingBill", conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(command);
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = DALHelper.ReportsPath + "\\WorkerOrderBilling.rpt";
                 report = new ReportDocument();
                report.Load(reportPath);
                utlDAL.VerifyReports(reportPath, report);
                report.SetParameterValue("@BillNo", BillNo);
                this.crystalReportViewer1.ReportSource = report;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string st = "";
            //bool y = false;
            //foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            //{
            //    y = strPrinter.Contains("Microsoft XPS Document Writer");
            //    if (y == true)
            //        st = strPrinter;
            //}
            //report.PrintOptions.PrinterName = st;
            //report.PrintToPrinter(1, false, 1, report.Rows.Count);

            //z
            //report.PrintToPrinter(1, false, 0, 0);
            //z
            //report.PrintOptions.PrinterName = "Zebra TLP2844";


            //g        

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //string st = "";
            //bool y = false;
            //foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            //{
            //    y = strPrinter.Contains("pos-80");
            //    if (y == true)
            //        st = strPrinter;
            //}
            //report.PrintOptions.PrinterName = st;
            report.PrintToPrinter(1, false, 1, report.Rows.Count);                                                                                       

        }
    }
}
