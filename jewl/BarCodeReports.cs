using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class BarCodeReports : Form
    {
        ItemDAL itmDAL = new ItemDAL();
        BarCodeReportViewer frm;
        public BarCodeReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }
        private bool isDate;
        public bool IsDate
        {
            get { return this.isDate; }
            set { this.isDate = value; }
        }
        private string tagNo;
        public string TagNo
        {
            get { return this.tagNo; }
            set { this.tagNo = value; }

        }
        private string tagfrom;
        public string Tagfrom
        {
            get { return this.tagfrom; }
            set { this.tagfrom = value; }

        }
        private string tagto;
        public string Tagto
        {
            get { return this.tagto; }
            set { this.tagto = value; }

        }

        private void BarCodeReports_Load(object sender, EventArgs e)
        {
            FormControls.FillCombobox(cbxItemName, itmDAL.GetAllItems(), "ItemName", "ItemId");

            this.rbtComStkRpt.Checked = true;
            this.txtFrom.Text = "";
            this.txtTo.Text = "";
            this.pnlManual.Visible = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {            
                if (this.rbtComStkRpt.Checked == true)
                {
                    frm = new BarCodeReportViewer();
                    frm.isPage = 1;
                    frm.ReportNo = 1;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 1;
                    frm.ReportNo = 2;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 1;
                    frm.ReportNo = 3;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 1;
                    frm.ReportNo = 4;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 1;
                    frm.ReportNo = 5;
                    frm.id = 1;
                    frm.Show();
                }
                if (this.rbtTagNo.Checked == true)
                {
                    this.tagNo = this.txtTagNo.Text.ToString();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 2;
                    frm.tagNo = this.tagNo;
                    frm.ReportNo = 1;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 2;
                    frm.tagNo = this.tagNo;
                    frm.ReportNo = 2;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 2;
                    frm.tagNo = this.tagNo;
                    frm.ReportNo = 3;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 2;
                    frm.tagNo = this.tagNo;
                    frm.ReportNo = 4;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 2;
                    frm.tagNo = this.tagNo;
                    frm.ReportNo = 5;
                    frm.id = 1;
                    frm.Show();
                }
                if (this.rbtTagRange.Checked == true)
                {
                    this.tagfrom = this.txtFrom.Text.ToString();
                    this.tagto = this.txtTo.Text.ToString();
                    this.isDate = true;
                    frm = new BarCodeReportViewer();
                    frm.isPage = 3;
                    frm.ReportNo = 1;
                    frm.id = 1;
                    frm.tagFrom = this.tagfrom;
                    frm.tagTo = this.tagto;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 3;
                    frm.ReportNo = 2;
                    frm.id = 1;
                    frm.tagFrom = this.tagfrom;
                    frm.tagTo = this.tagto;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 3;
                    frm.ReportNo = 3;
                    frm.id = 1;
                    frm.tagFrom = this.tagfrom;
                    frm.tagTo = this.tagto;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 3;
                    frm.ReportNo = 4;
                    frm.id = 1;
                    frm.tagFrom = this.tagfrom;
                    frm.tagTo = this.tagto;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.isPage = 3;
                    frm.ReportNo = 5;
                    frm.id = 1;
                    frm.tagFrom = this.tagfrom;
                    frm.tagTo = this.tagto;
                    frm.Show();
                }
                if (this.rbtManual.Checked == true)
                {
                    string[] strArray = new string[12];
                    if (this.txtTagNo1.Text.Length <= 0)
                        strArray[0] = "";
                    else
                        strArray[0] = this.txtTagNo1.Text;
                    if (this.txtTagNo2.Text.Length <= 0)
                        strArray[1] = "";
                    else
                        strArray[1] = this.txtTagNo2.Text;
                    if (this.txtTagNo3.Text.Length <= 0)
                        strArray[2] = "";
                    else
                        strArray[2] = this.txtTagNo3.Text;
                    if (this.txtTagNo4.Text.Length <= 0)
                        strArray[3] = "";
                    else
                        strArray[3] = this.txtTagNo4.Text;
                    if (this.txtTagNo5.Text.Length <= 0)
                        strArray[4] = "";
                    else
                        strArray[4] = this.txtTagNo5.Text;
                    if (this.txtTagNo6.Text.Length <= 0)
                        strArray[5] = "";
                    else
                        strArray[5] = this.txtTagNo6.Text;
                    if (this.txtTagNo7.Text.Length <= 0)
                        strArray[6] = "";
                    else
                        strArray[6] = this.txtTagNo7.Text;
                    if (this.txtTagNo8.Text.Length <= 0)
                        strArray[7] = "";
                    else
                        strArray[7] = this.txtTagNo8.Text;
                    if (this.txtTagNo9.Text.Length <= 0)
                        strArray[8] = "";
                    else
                        strArray[8] = this.txtTagNo9.Text;
                    if (this.txtTagNo10.Text.Length <= 0)
                        strArray[9] = "";
                    else
                        strArray[9] = this.txtTagNo10.Text;
                    if (this.txtTagNo11.Text.Length <= 0)
                        strArray[10] = "";
                    else
                        strArray[10] = this.txtTagNo11.Text;
                    if (this.txtTagNo12.Text.Length <= 0)
                        strArray[11] = "";
                    else
                        strArray[11] = this.txtTagNo12.Text;

                    frm = new BarCodeReportViewer();
                    frm.strArray = strArray;
                    frm.isPage = 4;
                    frm.ReportNo = 1;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.strArray = strArray;
                    frm.isPage = 4;
                    frm.ReportNo = 2;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.strArray = strArray;
                    frm.isPage = 4;
                    frm.ReportNo = 3;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.strArray = strArray;
                    frm.isPage = 4;
                    frm.ReportNo = 4;
                    frm.id = 1;
                    frm.Show();
                    frm = new BarCodeReportViewer();
                    frm.strArray = strArray;
                    frm.isPage = 4;
                    frm.ReportNo = 5;
                    frm.id = 1;
                    frm.Show();
                }
            }
            
    

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtTagNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtTagNo.Checked == true)
            {
                this.pnlTagNo.Visible = true;
                this.pnlTagRange.Visible = false;
            }
            else
            {
                this .pnlTagNo.Visible =false ;
            }
            //this.pnlStockSelection.Visible = false;
        }

        private void rbtComStkRpt_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlTagNo.Visible = false;
            this.pnlTagRange.Visible = false;
            //this.pnlStockSelection.Visible = false;
        }

        private void rbtTagRange_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlTagNo.Visible = false;
            this.pnlTagRange.Visible = true;
            //this.pnlStockSelection.Visible = false;
        }

        private void cbxItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)this.cbxItemName.SelectedItem;
            if (itm == null)
                return;
            else
            {               
                this.txtFrom.Text = itm.Abrivation.ToString();
                this.txtTo.Text = itm.Abrivation.ToString();
            }
        }

        private void rbtManual_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtManual.Checked == true)
            {
                pnlTagRange.Visible=false;
                pnlManual.Visible = true;
            }                
           else if (this.rbtManual.Checked == false)
                pnlManual.Visible = false;
        }

        private void pnlTagRange_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void pnlManual_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }
    }
}
