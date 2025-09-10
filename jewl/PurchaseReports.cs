using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.IO;

namespace jewl
{
    public partial class PurchaseReports : Form
    {
        ReportDocument crReportDocument;
        public PurchaseReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void PurchaseReports_Load(object sender, EventArgs e)
        {
            FormControls.FillCombobox(cbxSupplierCodeFrom, new SupplierDAL().GetAllSuppliers(), "PAbri", "PAbri");
            FormControls.FillCombobox(cbxSupplierCodeTo, new SupplierDAL().GetAllSuppliers(), "PAbri", "PAbri");
            FormControls.FillCombobox(cbxItemCode, new ItemDAL().GetAllItems(), "Abrivation", "ItemId");
            this.cbxSupplierCodeFrom.Enabled = false;
            this.cbxSupplierCodeTo.Enabled = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSupplierFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSupplierFrom.Checked==true)
            {
                this.cbxSupplierCodeFrom.Enabled = true;
            }
            else
            {
                this.cbxSupplierCodeFrom.Enabled = false;
            }
        }

        private void chkSupplierTo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSupplierTo.Checked == true)
            {
                this.cbxSupplierCodeTo.Enabled = true;
            }
            else
            {
                this.cbxSupplierCodeTo.Enabled = false;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string selectQuery = "";
            if (this.cbxSupplierCodeFrom.SelectedIndex >= 0 && cbxSupplierCodeTo.SelectedIndex == -1)
                selectQuery = "PAbri ='" + (string)cbxSupplierCodeFrom.SelectedValue + "'";
            else if (cbxSupplierCodeFrom.SelectedIndex >= 0 && cbxSupplierCodeTo.SelectedIndex >= 0)
                selectQuery = "PAbri >='" + (string)cbxSupplierCodeFrom.SelectedValue + "' and PAbri <='" + (string)cbxSupplierCodeTo.SelectedValue + "'";
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = null;
            cmd = new SqlCommand("GetPurchaseLgReport", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            try
            {
                da.Fill(ds, "GetPurchaseLgReport");
                DataView dv = ds.Tables[0].DefaultView;
                 dv.RowFilter = selectQuery.ToString();
                crReportDocument = new ReportDocument();
                string path = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = path + "\\Reports\\PurchaseLgReportReport.rpt";
                crReportDocument.Load(reportPath);
                crReportDocument.SetDataSource(dv);
                //if (selectQuery!=string.Empty)
                //crReportDocument.RecordSelectionFormula = selectQuery;
              this.crystalReportViewer1.ReportSource = crReportDocument;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
                con.Dispose();
            }              
            
        }

       
    }
}
