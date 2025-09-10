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
using DAL;
using System.IO;
using System.Drawing.Printing;

namespace jewl
{
    public partial class frmBalanceInvoiceRpt : Form
    {
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString); 
        public string selectQuery = "";
        SqlDataAdapter da;
        DataSet ds;
        public int id = 0;
        public int VNO, rpt = 0;
        SqlCommand cmd;
        string reportPath;
        ReportDocument report;
        public frmBalanceInvoiceRpt()
        {
            InitializeComponent();
        }

        private void frmBalanceInvoiceRpt_Load(object sender, EventArgs e)
        {
            if (id == 0)
            {
                string query = "VoucherInvoice";
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                DataSet ds = new DataSet();
                da.Fill(ds, "VoucherInvoice");
                report = new ReportDocument();
                string path = Path.GetDirectoryName(DALHelper.ReportsPath);
                report.Load(path + "\\Reports\\VoucherInvoice.rpt");
                report.SetDataSource(ds);
                crystalReportViewer1.SelectionFormula = selectQuery;
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.RefreshReport();
            }
            if (id == 1)
            {
                {
                    //string query = "GoldSalePurchaseBill";
                    //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                    //SqlCommand cmd = new SqlCommand(query, con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //con.Open();
                    //DataSet ds = new DataSet();
                    //da.Fill(ds, "GoldSalePurchaseBill");
                    con.Open();
                    cmd = new SqlCommand("GoldSalePurchaseBill", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "GoldSalePurchaseBill");

                    if (MessageBox.Show("Print With [Kaat] Click Yes Or With Out [Kaat] Click No", BusinesEntities.Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        reportPath = DALHelper.ReportsPath + "\\GoldSalePurchaseBill.rpt";
                    }
                    else
                    {
                        reportPath = DALHelper.ReportsPath + "\\GoldSalePurchaseBillKaat.rpt";
                    }
                    report = new ReportDocument();
                }
                //string path = Path.GetDirectoryName(DALHelper.ReportsPath);
                //report.Load(path + "\\Reports\\GoldSalePurchaseBill.rpt");
                //report.SetDataSource(ds);
                //report.RecordSelectionFormula = selectQuery;
                //crystalReportViewer1.ReportSource = report;
                //crystalReportViewer1.RefreshReport();
                report.Load(reportPath);
                report.SetDataSource(ds);
                crystalReportViewer1.SelectionFormula = selectQuery;
               this.crystalReportViewer1.ReportSource = report;
               this.crystalReportViewer1.RefreshReport();
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
