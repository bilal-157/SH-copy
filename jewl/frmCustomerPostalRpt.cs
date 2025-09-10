using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using DAL;
using BusinesEntities;
using System.IO;

namespace jewl
{
    public partial class frmCustomerPostalRpt : Form
    {
        string query = "";
        ReportDocument report;
        public frmCustomerPostalRpt()
        {           
            InitializeComponent();
        }

        private void frmCustomerPostalRpt_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;          
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                this.Close();
            DataSet ds = new DataSet();
            DataTable dt1 = new DataTable();
            dt1 = ds.Tables.Add("Customer");
            dt1.Columns.Add("Name", typeof(System.String));
            dt1.Columns.Add("Address", typeof(System.String));
            dt1.Columns.Add("Address2", typeof(System.String));
            dt1.Columns.Add("Address11", typeof(System.String));
            dt1.Columns.Add("Address22", typeof(System.String));
            DataRow r = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (r == null)
                    r = dt1.NewRow();
                r["Name"] = dt.Rows[i]["Name"];
                r["Address"] = dt.Rows[i]["Address"];
                r["Address2"] = dt.Rows[i]["Address2"];

                i = i + 1;
                if (i < dt.Rows.Count)
                {
                    if (r == null)
                        r = dt1.NewRow();
                    r["Name1"] = dt.Rows[i]["Name"];                 
                    r["Address11"] = dt.Rows[i]["Address1"];
                    r["Address22"] = dt.Rows[i]["Address2"];
                }
            }
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string reportPath = "";
            reportPath = path + "\\Reports\\GoldBarCode.rpt";
            report = new ReportDocument();
            report.Load(reportPath);
            report.SetDataSource(ds.Tables["Customer"]);
            crystalReportViewer1.ReportSource = report;
        }
    }
}