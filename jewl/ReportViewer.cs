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
using BusinesEntities;

namespace jewl
{
    public partial class ReportViewer : Form
    {
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);        
        UtilityDAL uDAL = new UtilityDAL();
        ReportDocument report;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;
        string reportPath;
        public int id = 0;
        public int srid = 0;
        public int snmbr = 0;
        public int isPage, rpt, rpt1, sNo, SaleNo, oNo, isNew, q, RpNO, headCode;
        public string iType, selectQuery, category, custName, accountCode, parentCode;

        public DateTime? Df;
        public DateTime? Dt;
        public ReportViewer()
        {
            InitializeComponent();
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            #region BarCode
            if (isPage == 0)
            {
                if (rpt == 1 || rpt == 8 || rpt == 13)
                {
                    con.Open();
                    con = new SqlConnection(DALHelper.ConnectionString);
                    if(rpt == 1)
                    cmd = new SqlCommand("StockRpt", con);                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StockRpt");
                    report = new ReportDocument();
                    if (rpt == 1)
                        report.Load(DALHelper.ReportsPath + "\\Stock Report.rpt");
                    if (rpt == 8)
                        report.Load(DALHelper.ReportsPath + "\\Stock ReportPic.rpt");
                    if (rpt == 13)
                        report.Load(DALHelper.ReportsPath + "\\StockOrderReport.rpt");
                    //report.Load("../../Reports/Stock Report.rpt");
                    report.SetDataSource(ds);
                    if (!string.IsNullOrEmpty(selectQuery))
                        crystalReportViewer1.SelectionFormula = selectQuery;
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
                if (rpt == 2)
                {
                    con.Open();
                    con = new SqlConnection(DALHelper.ConnectionString);
                    cmd = new SqlCommand("StockSummaryByKarat", con);
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter(cmd);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StockSummaryByKarat");
                    report = new ReportDocument();
                    report.Load(DALHelper.ReportsPath + "\\StockSummryByKarat.rpt");
                    report.SetDataSource(ds);
                    //if (!string.IsNullOrEmpty(selectQuery))
                    //    report.RecordSelectionFormula = selectQuery;
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
                if (rpt == 3)
                {
                    con.Open();
                    SqlDataAdapter da;
                    if (q == 0)
                    {
                        string query = "Select itm.ItemName ,count(s.itemid) as Entries, sum(s.Qty) as Qty,sum(s.Pieces) as Pieces,  Sum(s.NetWeight) as NetWeight from Stock s inner join Item  itm on itm.itemid = s.itemid left outer join subgroupitem sbg on sbg.SubGItmId = s.SubGItmId where itm.itemid = s.itemid  and (s.Status = 'Sample' or s.Status = 'Available') group by itm.ItemName,sbg.SGItmName order by itm.ItemName,sbg.SGItmName";
                        da = new SqlDataAdapter(query, DALHelper.ConnectionString);
                    }
                    else
                    {
                        cmd = new SqlCommand("sp_CompleteStockCombyDateRange", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);
                        cmd.Parameters.Add("@datef", SqlDbType.DateTime).Value = Df;
                        cmd.Parameters.Add("@datet", SqlDbType.DateTime).Value = Dt;
                    }
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Command");
                    report = new ReportDocument();
                    report.Load(DALHelper.ReportsPath + "\\Stock Summary.rpt");
                    report.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
                if (rpt == 4)
                {
                    con.Open();
                    SqlDataAdapter da;
                    cmd = new SqlCommand("sp_StockCombyDateRange", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add("@datef", SqlDbType.DateTime).Value = Df;
                    cmd.Parameters.Add("@datet", SqlDbType.DateTime).Value = Dt;
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StockCom");
                    report = new ReportDocument();
                    report.Load(DALHelper.ReportsPath + "\\StockCom.rpt");
                    report.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
                if (rpt == 5)
                {
                    con.Open();
                    con = new SqlConnection(DALHelper.ConnectionString);
                    cmd = new SqlCommand("sp_StockBalance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StockBalance");
                    report = new ReportDocument();
                    report.Load(DALHelper.ReportsPath + "\\StockBalance.rpt");
                    report.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
                if (rpt == 6)
                {
                    con.Open();
                    SqlDataAdapter da;
                    cmd = new SqlCommand("sp_StockComQty", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = Df;
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StockComQty");
                    report = new ReportDocument();
                    report.Load(DALHelper.ReportsPath + "\\ComStockByQty.rpt");
                    report.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
            }
            #endregion

            #region Stock
            if (isPage == 1)
            {
                if (rpt == 1 || rpt == 8 || rpt == 13)
                {
                    con.Open();
                    con = new SqlConnection(DALHelper.ConnectionString);
                   
                        cmd = new SqlCommand("StockRpt", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StockRpt");
                    report = new ReportDocument();
                    if (rpt == 1)
                    {
                        //if (iType == "Diamond")
                        //{
                        //    report.Load(DALHelper.ReportsPath + "\\Stock ReportDiamond.rpt");
                        //}
                        //else
                        {
                            report.Load(DALHelper.ReportsPath + "\\Stock Report.rpt");
                        }
                    }
                    if (rpt == 8)
                    {
                        if (iType == "Diamond")
                        {
                            report.Load(DALHelper.ReportsPath + "\\Stock ReportPicDiamond.rpt");
                        }
                        else
                        {
                            report.Load(DALHelper.ReportsPath + "\\Stock ReportPic.rpt");
                        }
                    }
                    if (rpt == 13)
                    {
                        if (iType == "Diamond")
                        {
                            report.Load(DALHelper.ReportsPath + "\\StockOrderReportDiamond.rpt");
                        }
                        else
                        {
                            report.Load(DALHelper.ReportsPath + "\\StockOrderReport.rpt");
                        }
                    }
                    //report.Load("../../Reports/Stock Report.rpt");
                    report.SetDataSource(ds);
                    if (!string.IsNullOrEmpty(selectQuery))
                        crystalReportViewer1.SelectionFormula = selectQuery;
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                    //con.Open();
                    //con = new SqlConnection(DALHelper.ConnectionString);
                    //cmd = new SqlCommand("StockRpt", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //da = new SqlDataAdapter(cmd);
                    //con.Open();
                    //DataSet ds = new DataSet();
                    //da.Fill(ds, "StockRpt");
                    //report = new ReportDocument();
                    //report.Load(DALHelper.ReportsPath + "\\Stock Report.rpt");
                    ////report.Load("../../Reports/Stock Report.rpt");
                    //report.SetDataSource(ds);
                    //if (!string.IsNullOrEmpty(selectQuery))
                    //    report.RecordSelectionFormula = selectQuery;
                    //crystalReportViewer1.ReportSource = report;
                    //crystalReportViewer1.RefreshReport();
                }
                if (rpt == 2)
                {
                    con.Open();
                    con = new SqlConnection(DALHelper.ConnectionString);
                    cmd = new SqlCommand("StockSummryByKarat", con);
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter(cmd);
                    con.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StockSummaryByKarat");
                    report = new ReportDocument();
                    report.Load(DALHelper.ReportsPath + "\\StockSummryByKarat.rpt");
                    report.SetDataSource(ds);
                    //if (!string.IsNullOrEmpty(selectQuery))
                    //    report.RecordSelectionFormula = selectQuery;
                    crystalReportViewer1.ReportSource = report;
                   // crystalReportViewer1.RefreshReport();
                }
                if (rpt == 3||rpt == 9)
                {
                    con.Open();
                    SqlDataAdapter da;
                    if (q == 0)
                    {
                        string query = "Select itm.ItemName ,count(s.itemid) as Entries, sum(s.Qty) as Qty,sum(s.Pieces) as Pieces,Sum(s.PWeight) as PWeight,  Sum(s.NetWeight) as NetWeight from Stock s inner join Item  itm on itm.itemid = s.itemid left outer join subgroupitem sbg on sbg.SubGItmId = s.SubGItmId where itm.itemid = s.itemid  and (s.Status = 'Sample' or s.Status = 'Available')and s.bstatus='Standard' group by itm.ItemName,sbg.SGItmName order by itm.ItemName,sbg.SGItmName";
                        da = new SqlDataAdapter(query, DALHelper.ConnectionString);
                    }
                    else
                    {
                        cmd = new SqlCommand("sp_CompleteStockCombyDateRange", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);
                        cmd.Parameters.Add("@datef", SqlDbType.DateTime).Value = Df;
                        cmd.Parameters.Add("@datet", SqlDbType.DateTime).Value = Dt;
                    }
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Command");
                    report = new ReportDocument();
                    if (rpt == 3)
                    {
                        report.Load(DALHelper.ReportsPath + "\\Stock Summary.rpt");
                        uDAL.VerifyReports(DALHelper.ReportsPath + "\\Stock Summary.rpt", report);
                    }
                    if (rpt == 9)
                    {
                        report.Load(DALHelper.ReportsPath + "\\Stock Summarygraph.rpt");
                        uDAL.VerifyReports(DALHelper.ReportsPath + "\\Stock Summarygraph.rpt", report);
                    }
                    report.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
                if (rpt == 4 || rpt == 7)
                {
                    con.Open();
                    SqlDataAdapter da;
                    if (rpt == 4)
                    cmd = new SqlCommand("sp_StockCombyDateRange", con);
                    if (rpt == 7)
                        cmd = new SqlCommand("sp_StockCombyDateRangeBulk", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add("@datef", SqlDbType.DateTime).Value = Df;
                    cmd.Parameters.Add("@datet", SqlDbType.DateTime).Value = Dt;
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StockCom");
                    report = new ReportDocument();
                    if (rpt == 4)
                    report.Load(DALHelper.ReportsPath + "\\StockCom.rpt");
                    if (rpt == 7)
                        report.Load(DALHelper.ReportsPath + "\\StockComBulK.rpt");
                    report.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
                if (rpt == 5)
                {
                    con.Open();
                    con = new SqlConnection(DALHelper.ConnectionString);
                    cmd = new SqlCommand("sp_StockBalance", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //da = new SqlDataAdapter(cmd);
                    //con.Open();
                   // DataSet ds = new DataSet();
                   // da.Fill(ds, "StockBalance");
                    report = new ReportDocument();
                    report.Load(DALHelper.ReportsPath + "\\StockBalance.rpt");
                    //report.SetDataSource(ds);
                    uDAL.VerifyReports(DALHelper.ReportsPath + "\\StockBalance.rpt", report);
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
                if (rpt == 6)
                {
                    con.Open();
                    SqlDataAdapter da;
                    cmd = new SqlCommand("sp_StockComQty", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = Df;
                    DataSet ds = new DataSet();
                    da.Fill(ds, "StockComQty");
                    report = new ReportDocument();
                    report.Load(DALHelper.ReportsPath + "\\ComStockByQty.rpt");
                    report.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
            }
            #endregion

            #region Order
            if (isPage == 2)
            {
                if (rpt == 1)
                {
                    try
                    {
                        if (Main.City == "Islamabad")
                        {
                            con.Open();
                            cmd = new SqlCommand("OrderEstimateBillPasa", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                            ds = new DataSet();
                            da.Fill(ds, "OrderEstimateBill");
                            reportPath = DALHelper.ReportsPath + "\\OrderEstimateBillRpt.rpt";
                            // creat new object for load and record selection 
                            report = new ReportDocument();
                        }
                        else
                        {
                            con.Open();
                            cmd = new SqlCommand("OrderEstimateBill", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                            ds = new DataSet();
                            da.Fill(ds, "OrderEstimateBill");
                            reportPath = DALHelper.ReportsPath + "\\OrderEstimateBillRpt.rpt";
                            // creat new object for load and record selection 
                            report = new ReportDocument();
                        }
                        report.Load(reportPath);
                        
                        report.SetDataSource(ds);
                        uDAL.VerifyReports(reportPath, report); 
                        crystalReportViewer1.SelectionFormula = selectQuery;
                        this.crystalReportViewer1.ReportSource = report;
                        //this.crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 2)
                {
                    try
                    {
                        con.Open();
                        if(id==0)
                        cmd = new SqlCommand("sp_CompleteOrder", con);
                        if (id == 1)
                            cmd = new SqlCommand("sp_CompleteOrderDDate", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                        ds = new DataSet();
                        da.Fill(ds, "sp_CompleteOrder");
                        reportPath = DALHelper.ReportsPath + "\\CompleteOrderRpt.rpt";
                        report = new ReportDocument();
                        report.Load(reportPath);
                        report.SetDataSource(ds);
                        crystalReportViewer1.SelectionFormula = selectQuery;
                        this.crystalReportViewer1.ReportSource = report;
                        this.crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    { 
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 3)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("sp_CompleteOrder", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                        ds = new DataSet();
                        da.Fill(ds, "sp_CompleteOrder");
                        reportPath = DALHelper.ReportsPath + "\\CompleteOrderByItemRpt.rpt";
                        report = new ReportDocument();
                        report.Load(reportPath);
                        report.SetDataSource(ds);
                        crystalReportViewer1.SelectionFormula = selectQuery;
                        this.crystalReportViewer1.ReportSource = report;
                        this.crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 4)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("sp_JobCard", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                        ds = new DataSet();
                        da.Fill(ds, "sp_JobCard");
                        reportPath = DALHelper.ReportsPath + "\\JobCardReport.rpt";
                        report = new ReportDocument();
                        report.Load(reportPath);
                        report.SetDataSource(ds);
                        crystalReportViewer1.SelectionFormula = selectQuery;
                        this.crystalReportViewer1.ReportSource = report;
                        this.crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 5)
                {
                    report = new ReportDocument();
                    reportPath = DALHelper.ReportsPath + "\\OrderBillRpt.rpt";
                    uDAL.VerifyReports(reportPath, report);
                    report.SetParameterValue("@ONO", oNo);
                    this.crystalReportViewer1.ReportSource = report;
                }
                if (rpt == 6)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("sp_OrderSaleRpt", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                        ds = new DataSet();
                        da.Fill(ds, "CompleteSaleReport");
                        reportPath = DALHelper.ReportsPath + "\\CompletOrderSaleReport.rpt";
                        report = new ReportDocument();
                        report.Load(reportPath);
                        report.SetDataSource(ds);
                        if (selectQuery != null)
                            crystalReportViewer1.SelectionFormula = selectQuery;
                        this.crystalReportViewer1.ReportSource = report;
                        this.crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 7)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                {
                    try
                    {
                        reportPath = DALHelper.ReportsPath + "\\PendingOrder.rpt";
                        report = new ReportDocument();
                        uDAL.VerifyReports(reportPath, report);
                        this.crystalReportViewer1.ReportSource = report;
                        if (selectQuery != null)
                            this.crystalReportViewer1.SelectionFormula = selectQuery; 
                        this.crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 8)
                {
                    try
                    {
                        reportPath = DALHelper.ReportsPath + "\\PendingOrder.rpt";
                        report = new ReportDocument();
                        uDAL.VerifyReports(reportPath, report);
                        report.Load(reportPath);
                        if (selectQuery != null)
                            crystalReportViewer1.SelectionFormula = selectQuery;
                        this.crystalReportViewer1.ReportSource = report;
                        this.crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 9)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("sp_WorkerOrder", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                        ds = new DataSet();
                        da.Fill(ds, "sp_WorkerOrder");
                        reportPath = DALHelper.ReportsPath + "\\WorkerOrderRpt.rpt";
                        report = new ReportDocument();
                        report.Load(reportPath);
                        report.SetDataSource(ds);
                        if (selectQuery != null)
                            crystalReportViewer1.SelectionFormula = selectQuery;
                        this.crystalReportViewer1.ReportSource = report;
                        this.crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            #endregion

            #region Sale
            if (isPage == 3)
            {
                if (rpt == 1 || rpt == 2)
                {
                    try
                    {
                        con.Open();
                        if (rpt == 1)
                            cmd = new SqlCommand("CompleteSaleReport", con);
                        if (rpt == 2)
                            cmd = new SqlCommand("sp_SaleItemDetail", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds, "CompleteSaleReport");
                        if (id == 0)
                        {
                            if (iType == "Silver")
                                reportPath = DALHelper.ReportsPath + "\\CompletSilverSaleReport.rpt";
                            else
                                reportPath = DALHelper.ReportsPath + "\\CompletSaleReport.rpt";
                        }
                        else
                        {
                            if (iType == "Silver")
                                reportPath = DALHelper.ReportsPath + "\\CompletSilverSaleReport.rpt";
                            else
                                reportPath = DALHelper.ReportsPath + "\\CompletSaleReportPic.rpt";
                        }
                        report = new ReportDocument();
                        report.Load(reportPath);
                        report.SetDataSource(ds);
                        crystalReportViewer1.SelectionFormula = selectQuery;
                        this.crystalReportViewer1.ReportSource = report;
                        this.crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 3)
                {
                    try
                    {
                        con.Open();
                        if (id == 0)
                            cmd = new SqlCommand("SaleReceiveable", con);
                        if (id == 1)
                            cmd = new SqlCommand("SaleReceiveablePromiseDate", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds, "Command");
                        reportPath = DALHelper.ReportsPath + "\\SaleReceiveableRpt.rpt";
                        report = new ReportDocument();
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
                if (rpt == 4)
                {
                    if (snmbr == 1 || snmbr == 2 || snmbr == 3)
                    {
                        if (snmbr == 1)
                            reportPath = DALHelper.ReportsPath + "\\SaleBillRpt.rpt";
                        if (snmbr == 2)
                            reportPath = DALHelper.ReportsPath + "\\SaleBillRptWithPic.rpt";
                        if (snmbr == 3)
                            reportPath = DALHelper.ReportsPath + "\\SaleBillRptDetail.rpt";
                        report = new ReportDocument();
                        report.Load(reportPath);
                        report.SetParameterValue("@SaleNo", sNo);
                        uDAL.VerifyReports(reportPath, report);
                        this.crystalReportViewer1.ReportSource = report;
                    }
                    else
                    {
                        if (MessageBox.Show("Print SaleInvoice With Picture Click Yes Or With Out Picture Click No", Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (id == 0)
                                reportPath = DALHelper.ReportsPath + "\\SaleBillRptWithPic.rpt";
                            else
                                reportPath = DALHelper.ReportsPath + "\\SilverSaleBillRptWithPic.rpt";
                        }
                        else
                        {
                            if (id == 0)
                                reportPath = DALHelper.ReportsPath + "\\SaleBillRpt.rpt";
                            else
                                reportPath = DALHelper.ReportsPath + "\\SilverSaleBillRpt.rpt";
                        }
                        report = new ReportDocument();
                        report.Load(reportPath);
                        report.SetParameterValue("@SaleNo", sNo);
                        uDAL.VerifyReports(reportPath, report);
                        this.crystalReportViewer1.ReportSource = report;
                    }
                }
                if (rpt == 5)
                {
                    if (srid == 0)
                    {
                        con.Open();
                        cmd = new SqlCommand("sp_CompleteSaleSummarybyDateRange", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);
                        cmd.Parameters.Add("@datef", SqlDbType.DateTime).Value = Df;
                        cmd.Parameters.Add("@datet", SqlDbType.DateTime).Value = Dt;
                        ds = new DataSet();
                        da.Fill(ds, "Command");
                        if (id == 0)
                            reportPath = DALHelper.ReportsPath + "\\SaleSummary.rpt";
                        else
                            reportPath = DALHelper.ReportsPath + "\\SaleSummaryGraph.rpt";
                        report = new ReportDocument();
                        report.Load(reportPath);
                        uDAL.VerifyReports(reportPath, report);
                        crystalReportViewer1.ReportSource = report;
                    }
                    if (srid == 1)
                    {
                        con.Open();                       
                        cmd = new SqlCommand("GetStockByDate", con);                     
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@datef", SqlDbType.DateTime).Value = Df;
                        cmd.Parameters.Add("@datet", SqlDbType.DateTime).Value = Dt;
                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds, "GetStockByDate");
                        reportPath = DALHelper.ReportsPath + "\\StockByDate.rpt";                        
                        report = new ReportDocument();
                        report.Load(reportPath);
                        report.SetDataSource(ds);
                        crystalReportViewer1.SelectionFormula = selectQuery;
                        this.crystalReportViewer1.ReportSource = report;
                        this.crystalReportViewer1.RefreshReport();
                    }
                }
                if (rpt == 6)
                {
                    con.Open();
                    cmd = new SqlCommand("SaleSummryByKarat", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds, "SaleSummryByKarat");
                    reportPath = DALHelper.ReportsPath + "\\SaleSummryByKarat.rpt";
                    report = new ReportDocument();
                    report.Load(reportPath);
                    report.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
                if (rpt == 7)
                {
                    con.Open();
                    cmd = new SqlCommand("sp_SaleCom", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da = new SqlDataAdapter(cmd);
                    cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = Df;
                    ds = new DataSet();
                    da.Fill(ds, "StockCom");
                    reportPath = DALHelper.ReportsPath + "\\SaleCom.rpt";
                    report = new ReportDocument();
                    report.Load(reportPath);
                    report.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
                if (rpt == 8)
                {
                    con.Open();
                    cmd = new SqlCommand("Sp_SaleComQty", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = Df;
                    da = new SqlDataAdapter(cmd);
                  
                    ds = new DataSet();
                    
                    da.Fill(ds, "StockComQty");
                    reportPath = DALHelper.ReportsPath + "\\ComSaleByQty.rpt";
                    report = new ReportDocument();
                    report.Load(reportPath);
                    report.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = report;
                    crystalReportViewer1.RefreshReport();
                }
                if (rpt == 11)
                {
                    if (SaleNo > 0)
                        reportPath = DALHelper.ReportsPath + "\\SaleParchi.rpt";
                    if (oNo > 0)
                        reportPath = DALHelper.ReportsPath + "\\OrderParchi.rpt";
                    report = new ReportDocument();
                    report.Load(reportPath);
                    if (SaleNo > 0)
                        report.SetParameterValue("@SaleNo", SaleNo);
                    if (oNo > 0)
                        report.SetParameterValue("@OrderNo", oNo);
                    uDAL.VerifyReports(reportPath, report);
                    this.crystalReportViewer1.ReportSource = report;
                }
                if (rpt == 9)
                {
                    try
                    {
                        cmd = new SqlCommand("SaleRpt", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);
                        con.Open();
                        ds = new DataSet();
                        da.Fill(ds, "StockRpt");
                        report = new ReportDocument();
                        report.Load(DALHelper.ReportsPath + "\\ViewSalePictures.rpt");
                        report.SetDataSource(ds);
                        crystalReportViewer1.SelectionFormula = selectQuery;
                        crystalReportViewer1.ReportSource = report;
                        crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            #endregion

            #region Split Sale
            if (isPage == 4)
            {
                if (rpt == 1)
                {
                    try
                    {
                        con.Open();
                        cmd = new SqlCommand("SplitSaleRpt", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                        ds = new DataSet();
                        da.Fill(ds, "SplitSaleRpt");
                        reportPath = DALHelper.ReportsPath + "\\SplitSale.rpt";
                        report = new ReportDocument();
                        report.Load(reportPath);
                        report.SetDataSource(ds);
                        if (selectQuery != null)
                            crystalReportViewer1.SelectionFormula = selectQuery;
                        this.crystalReportViewer1.ReportSource = report;
                        this.crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    { 
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            #endregion

            #region Accounts
            if (isPage == 5)
            {
                if (rpt == 1)
                {
                    if (Df == null)
                        Df = DateDAL.GetDate("select Min(DDate) as Date from vouchers where AccountCode='" + accountCode + "'");
                    if (Dt == null)
                        Dt = DateDAL.GetDate("select Max(DDate) as Date from vouchers where AccountCode='" + accountCode + "'");
                    try
                    {
                        reportPath = DALHelper.ReportsPath + "\\Ledger.rpt";
                        report = new ReportDocument();
                        report.Load(reportPath);
                        report.SetParameterValue("@DFrom", Df.Value.ToShortDateString());
                        report.SetParameterValue("@DTo", Dt.Value.ToShortDateString());
                        report.SetParameterValue("@AccCode", accountCode);
                        uDAL.VerifyReports(reportPath, report);
                        this.crystalReportViewer1.ReportSource = report;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 2)
                {
                    try
                    {
                        string query = "select (select parentname from parentaccount where parentcode='" + parentCode + "') as ParentName,isnull(sum(v.Cr),0) as Cr, isnull(sum(v.dr),0) as Dr ,((select OpeningCash from childaccount where childcode=c.ChildCode) + (isnull(sum(v.dr),0)-isnull(sum(v.Cr),0)))'Balance' , c.ChildCode'AccountCode' ,c.ChildName'AccountName' from childaccount c left outer join vouchers v on c.ChildCode=v.AccountCode where convert(varchar,cast(v.DDate as datetime),112) between convert(varchar,cast('"+Df+"' as datetime),112) and convert(varchar,cast('"+Dt+"' as datetime),112) and c.ChildCode  like '" + parentCode + "%" + "' group by c.ChildCode ,c.ChildName order by childcode";
                        cmd = new SqlCommand(query, con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                        ds = new DataSet();
                        da.Fill(ds, "ParentLedger");
                        reportPath = DALHelper.ReportsPath + "\\ParentAccountRpt.rpt";
                        report = new ReportDocument();
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
                if (rpt == 3)
                {
                    try
                    {
                        //string query = "select (select HeadName from HeadAccount where HeadCode=" + headCode + ") as HeadName,(select sum(Cr)from vouchers where GroupCode=v.GroupCode ) as Cr, (select sum(dr)from vouchers where GroupCode=v.GroupCode ) as Dr ,(select sum(dr)-sum(cr)from vouchers where GroupCode=v.GroupCode )as Balance  , (select GroupName from GroupAccount where GroupCode=v.GroupCode) as GroupName ,v.GroupCode  from vouchers  v where v.GroupCode  like '" + headCode + "" + "%' group by v.GroupCode";
                        string query = "select (select HeadName from HeadAccount where HeadCode=" + headCode + ") as HeadName,(select sum(Cr)from vouchers where ParentCode=v.ParentCode ) as Cr, (select sum(dr)from vouchers where ParentCode=v.ParentCode ) as Dr ,(select sum(dr)-sum(cr)from vouchers where ParentCode=v.ParentCode )as Balance  , (select ParentName from ParentAccount where ParentCode=v.ParentCode) as GroupName ,v.ParentCode as GroupCode  from vouchers  v where v.ParentCode  like '" + headCode + "" + "%' group by v.ParentCode";
                        cmd = new SqlCommand(query, con);
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                        ds = new DataSet();
                        da.Fill(ds, "HeadLedger");
                        reportPath = DALHelper.ReportsPath + "\\HeadRpt.rpt";
                        report = new ReportDocument();
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
                if (rpt == 4)
                {
                    try
                    {
                        cmd = new SqlCommand("GoldLedger", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        da = new SqlDataAdapter(cmd);// , ConfigurationManager.ConnectionStrings["RecordDB1"].ToString());
                        cmd.Parameters.Add("@AccountCode", SqlDbType.NVarChar).Value = accountCode;
                        ds = new DataSet();
                        da.Fill(ds, "Command");
                        reportPath = DALHelper.ReportsPath + "\\GoldLedger.rpt";
                        report = new ReportDocument();
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
                if (rpt == 5)
                {
                    try
                    {
                        reportPath = DALHelper.ReportsPath + "\\Cash Ledger.rpt";
                        report = new ReportDocument();
                        uDAL.VerifyReports(reportPath, report);
                        report.Load(reportPath);
                        this.crystalReportViewer1.ReportSource = report;
                        report.SetParameterValue("@DFrom", Df == null ? null : Df.Value.ToShortDateString());
                        report.SetParameterValue("@DTo", Dt == null ? null : Dt.Value.ToShortDateString());
                        report.SetParameterValue("@AccCode", accountCode);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 6)
                {
                    try
                    {
                        cmd = new SqlCommand("TrialBalance", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        da = new SqlDataAdapter(cmd);// 
                        ds = new DataSet();
                        da.Fill(ds, "TrialBalance");
                        reportPath = DALHelper.ReportsPath + "\\TrialBalance.rpt";
                        report = new ReportDocument();
                        uDAL.VerifyReports(reportPath, report);
                        report.SetDataSource(ds);
                        this.crystalReportViewer1.ReportSource = report;
                        this.crystalReportViewer1.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 7)
                {
                    try
                    {
                        reportPath = DALHelper.ReportsPath + "\\DayBook.rpt";
                        report.Load(reportPath);
                        uDAL.VerifyReports(reportPath, report);
                        report.SetParameterValue("@Date", Df == null ? null : Df.Value.ToShortDateString());
                        this.crystalReportViewer1.ReportSource = report;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                if (rpt == 8)
                {

                    reportPath = DALHelper.ReportsPath + "\\Repairing Estimate.rpt";
                    report = new ReportDocument();
                    report.Load(reportPath);
                    uDAL.VerifyReports(reportPath, report);
                    report.SetParameterValue("@RepairId", RpNO);
                    this.crystalReportViewer1.ReportSource = report;
                }
            }
            #endregion

            #region S
            if (isPage == 10)
            {
                if (rpt == 1)
                {
                    con.Open();
                    if (isNew == 0)
                        cmd = new SqlCommand(@"select o.OrderNo,o.BillBookNo,o.OrderDate as [Date],o.Advance as Amount,o.SaleManId,sm.Name as SalesManName from [Order] o left outer join SaleMan sm on sm.Id = o.SaleManId Order by o.OrderDate", con);
                    else
                        cmd = new SqlCommand(@"select o.OrderNo,o.BillBookNo,v.DDate as [Date],v.Cr as Amount,o.SaleManId,sm.Name as SalesManName
                                               from Vouchers v inner join [Order] o on (o.OrderNo = v.OrderNO and v.[Description] Not like 'Cash Received From Order No.%' and v.[Description] Not like 'Cash Received From Sale No.%' and v.Cr <> 0) left outer join SaleMan sm on sm.Id = o.SaleManId where v.OrderNO != 0  Order by v.DDate", con);
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter(cmd);// 
                    ds = new DataSet();
                    da.Fill(ds, "OrderAdvances");
                    reportPath = DALHelper.ReportsPath + "\\OrderCashLedger.rpt";
                    report = new ReportDocument();
                    report.Load(reportPath);
                    report.SetDataSource(ds);
                    CrystalDecisions.CrystalReports.Engine.TextObject myTextObject;
                    myTextObject = (TextObject)report.ReportDefinition.Sections[0].ReportObjects[0];
                    if (isNew == 0)
                        myTextObject.Text = "New Order Advances";
                    else
                        myTextObject.Text = "Old Order Advances";
                    if (!string.IsNullOrEmpty(selectQuery))
                    {
                        crystalReportViewer1.SelectionFormula = selectQuery;
                    }
                    this.crystalReportViewer1.ReportSource = report;
                    this.crystalReportViewer1.RefreshReport();
                }
            }
            #endregion
        }
    }
}
