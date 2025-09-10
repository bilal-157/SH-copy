using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using DAL;
using BusinesEntities;
using System.Windows.Forms;

namespace jewl
{
    public partial class StockReportsPics : Form
    {
        ItemDAL itmDAL = new ItemDAL();
        StockDAL stkDAL = new StockDAL();
        static int cont = 0;
        public StockReportsPics()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtRange_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtRange.Checked)
                this.panel1.Visible = true;
            else
                this.panel1.Visible = false;
        }

        private void rbtManual_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtManual.Checked)
                this.pnlManual.Visible = true;
            else
                this.pnlManual.Visible = false;
        }

        private void frmStockPics_Load(object sender, EventArgs e)
        {
            this.panel1.Visible = false;
            this.pnlManual.Visible = false;
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
        }

        private void cbxGroupItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)this.cbxGroupItem.SelectedItem;
            if (itm == null)
                return;
            else
            {

                this.txtFrom.Text = itm.Abrivation.ToString();
                this.txtTo.Text = itm.Abrivation.ToString();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.rbtRange.Checked)
            {
                cont = stkDAL.GetCountOfPicsTags(this.txtFrom.Text, this.txtTo.Text);
                if (cont > 8)
                {
                    MessageBox.Show("Range Exceeded than 8 Tags");
                    return;
                }
                else
                {
                    ViewPicturesRpt vpr = new ViewPicturesRpt();
                    vpr.TgFrom = this.txtFrom.Text;
                    vpr.TgTo = this.txtTo.Text;
                    vpr.Show();
                }
            }
            else if (this.rbtManual.Checked)
            {
                string[] strArray = new string[10];
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
                ViewPicturesRpt vpr = new ViewPicturesRpt();
                vpr.isManual = true;
                vpr.ArrayStr = strArray;
                vpr.Show();                  
            }
        }

        private void pnlManual_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }
    }
}
