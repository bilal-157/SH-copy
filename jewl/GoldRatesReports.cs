using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace jewl
{
    public partial class GoldRatesReports : Form
    {
        public GoldRatesReports()
        {
            InitializeComponent();
            FormControls.GetAllControls(this); FormControls.GetAllControls(this); this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
           

                frmGoldRateRpt frm = new frmGoldRateRpt();

               
                if (rbtDate.Checked == true)
                {
                    frm.fDate = this.dtpDate.Value;
                    frm.tDate = this.dtpDate.Value;
                }
                else
                {
                    frm.fDate = this.dtpFromDate.Value;
                    frm.tDate = this.dtpToDate.Value;
                }
                frm.ShowDialog();

            
        }

        private void rbtDateRange_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtDateRange.Checked == true)
            {
                this.dtpDate.Visible = false;
                this.pnlDateWise.Visible = true;
            }
            else
            {
                this.dtpDate.Visible = true;
                this.pnlDateWise.Visible = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmGoldRatesReports_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void frmGoldRatesReports_Load(object sender, EventArgs e)
        {

        }

      
    }
}
