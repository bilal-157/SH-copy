using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using DAL;
using System.Globalization;

namespace jewl
{
    public partial class StockSearch : Form
    {
        ManageStock mns;
        ItemDAL itmDAL = new ItemDAL();
        StockDAL stkDAL = new StockDAL();
        
        List<Stock> stks;
        Stock stk;
        
        BindingSource myBindingSource;
       
        public StockSearch()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }
        public StockSearch(ManageStock mStock):this()
        {
            this.mns = mStock;
        }
        private void Stocksearch_Load(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged -= new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
            this.txtWFrom.Enabled = false;
            this.txtWTo.Enabled = false;

            this.dtpFrom.Enabled = false;
            this.dtpTo.Enabled = false;

            this.cbxKarrat.Enabled = false;

            myBindingSource = new BindingSource();
            
        }
        private void cbxGroupItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int k = (int)this.cbxGroupItem.SelectedValue;
            this.ShowRecords(k);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowRecords(int id)
        {
            stks = stkDAL.GetRecordsByItemId(id);
            if (stks == null)
            {
                this.dgvShowRecords.Rows.Clear();
                return;
            }
            else
            {
                this.dgvShowRecords.AutoGenerateColumns = false;
                this.dgvShowRecords.Rows.Clear();
                int count = stks.Count;
                this.lblRowCount.Text = count.ToString();
                this.dgvShowRecords.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvShowRecords.Rows[i].Cells[1].Value = stks[i].TagNo.ToString();
                    this.dgvShowRecords.Rows[i].Cells[2].Value = Convert.ToDecimal(stks[i].NetWeight);
                    this.dgvShowRecords.Rows[i].Cells[3].Value = stks[i].Karrat.ToString();
                    this.dgvShowRecords.Rows[i].Cells[4].Value = stks[i].StockDate.ToString("dd-MMM-yy");
                    this.dgvShowRecords.Rows[i].Cells[5].Value = Convert.ToInt32(stks[i].ItemName.ItemId);
                    this.dgvShowRecords.Rows[i].Cells[6].Value = Convert.ToInt32(stks[i].StockId);
                }
            }
        }

        private void ShowRecByWt(decimal wt1,decimal wt2)
        {
            stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + " and Status = 'Available'");
            if (stks == null)
                return;
            else
            {
                this.dgvShowRecords.AutoGenerateColumns = false;
                this.dgvShowRecords.Rows.Clear();
                int count = stks.Count;
                this.dgvShowRecords.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvShowRecords.Rows[i].Cells[1].Value = stks[i].TagNo.ToString();
                    this.dgvShowRecords.Rows[i].Cells[2].Value = Convert.ToDecimal(stks[i].NetWeight);
                    this.dgvShowRecords.Rows[i].Cells[3].Value = stks[i].Karrat.ToString();
                    this.dgvShowRecords.Rows[i].Cells[4].Value = Convert.ToDateTime(stks[i].StockDate);
                    this.dgvShowRecords.Rows[i].Cells[5].Value = Convert.ToInt32(stks[i].ItemName.ItemId);
                    this.dgvShowRecords.Rows[i].Cells[6].Value = Convert.ToInt32(stks[i].StockId);
                }
            }
        }

        private void ShowRecByDate(DateTime dt1, DateTime dt2)
        {
            stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + dt1 + "' and '" + dt2 + "' and Status = 'Available'");//dt1, dt2);
            if (stks == null)
                return;
            else
            {
                this.dgvShowRecords.AutoGenerateColumns = false;
                this.dgvShowRecords.Rows.Clear();
                int count = stks.Count;
                this.dgvShowRecords.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvShowRecords.Rows[i].Cells[1].Value = stks[i].TagNo.ToString();
                    this.dgvShowRecords.Rows[i].Cells[2].Value = Convert.ToDecimal(stks[i].NetWeight);
                    this.dgvShowRecords.Rows[i].Cells[3].Value = stks[i].Karrat.ToString();
                    this.dgvShowRecords.Rows[i].Cells[4].Value = Convert.ToDateTime(stks[i].StockDate);
                    this.dgvShowRecords.Rows[i].Cells[5].Value = Convert.ToInt32(stks[i].ItemName.ItemId);
                    this.dgvShowRecords.Rows[i].Cells[6].Value = Convert.ToInt32(stks[i].StockId);
                }
            }
        }

        private void ShowRecByKarat(string Karat)
        {
            stks = stkDAL.GetRecordsByKarat(Karat);
            if (stks == null)
                return;
            else
            {
                this.dgvShowRecords.AutoGenerateColumns = false;
                this.dgvShowRecords.Rows.Clear();
                int count = stks.Count;
                this.dgvShowRecords.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvShowRecords.Rows[i].Cells[1].Value = stks[i].TagNo.ToString();
                    this.dgvShowRecords.Rows[i].Cells[2].Value = Convert.ToDecimal(stks[i].NetWeight);
                    this.dgvShowRecords.Rows[i].Cells[3].Value = stks[i].Karrat.ToString();
                    this.dgvShowRecords.Rows[i].Cells[4].Value = Convert.ToDateTime(stks[i].StockDate);
                    this.dgvShowRecords.Rows[i].Cells[5].Value = Convert.ToInt32(stks[i].ItemName.ItemId);
                    this.dgvShowRecords.Rows[i].Cells[6].Value = Convert.ToInt32(stks[i].StockId);
                }
            }
        }

        private void ShowRecByTagNo(string TagNo)
        {
            this.dgvShowRecords.Rows.Clear();
            stk = stkDAL.GetRecordByTagNo(TagNo);
            if (stk == null)
                return;
            else
            {
              
                    this.dgvShowRecords.Rows[0].Cells[1].Value = stk.TagNo.ToString();
                    this.dgvShowRecords.Rows[0].Cells[2].Value = Convert.ToDecimal(stk.NetWeight);
                    this.dgvShowRecords.Rows[0].Cells[3].Value = stk.Karrat.ToString();
                    this.dgvShowRecords.Rows[0].Cells[4].Value = Convert.ToDateTime(stk.StockDate).ToString("dd-MMM-yy");
                    this.dgvShowRecords.Rows[0].Cells[5].Value = Convert.ToInt32(stk.ItemName.ItemId);
                    this.dgvShowRecords.Rows[0].Cells[6].Value = Convert.ToInt32(stk.StockId);
               
            }
        }


        private void ShowRecByDesNo(string DesNo)
        {
          
            this.dgvShowRecords.Rows.Clear();
            stks = stkDAL.GetRecordByDesNo(DesNo);
            if (stks == null)
                return;
            else
            {
                this.dgvShowRecords.AutoGenerateColumns = false;
                this.dgvShowRecords.Rows.Clear();
                int count = stks.Count;
                this.dgvShowRecords.Rows.Add(count);
                for (int i = 0; i < stks.Count; i++)
                {                    
                    this.dgvShowRecords.Rows[i].Cells[1].Value = stks[i].TagNo.ToString();
                    this.dgvShowRecords.Rows[i].Cells[2].Value = stks[i].NetWeight.ToString();
                    this.dgvShowRecords.Rows[i].Cells[3].Value = stks[i].Karrat.ToString();
                    this.dgvShowRecords.Rows[i].Cells[4].Value = stks[i].StockDate.ToString("dd-MMM-yy");
                    this.dgvShowRecords.Rows[i].Cells[5].Value = stks[i].ItemName.ItemId.ToString();
                    this.dgvShowRecords.Rows[i].Cells[6].Value = stks[i].StockId.ToString();
                }
            }
        }

        private void dgvShowRecords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int id = Convert .ToInt32 (dgvShowRecords .Rows [e.RowIndex ].Cells [6].Value) ;
            //int itmid = Convert.ToInt32(dgvShowRecords.Rows[e.RowIndex].Cells[5].Value);
            //ShowSerachRecord sr = new ShowSerachRecord(mns.ShowSerach);
            //sr(id, itmid);
            //this.Hide();
        }

        private void cbxGroupItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged += new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);

        }

        private void chkWeightRange_Click(object sender, EventArgs e)
        {
            if (chkWeightRange.Checked == true)
            {
                this.txtWFrom.Enabled = true;
                this.txtWTo.Enabled = true;
            }
            else
            {
                this.txtWFrom.Enabled = false;
                this.txtWTo.Enabled = false;
            }
              
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.chkWeightRange.Checked == true)
            {
                if (this.txtWFrom.Text != string.Empty && this.txtWTo.Text !=string.Empty)
                {
                    decimal t = Convert.ToDecimal(this.txtWFrom.Text);
                    decimal u = Convert.ToDecimal(this.txtWTo.Text);
                    this.ShowRecByWt(t, u);
                }
            }
            if (this.chkDateRange.Checked==true)
            {
                DateTime df = this.dtpFrom.Value;
                DateTime dt = this.dtpTo.Value;
                this.ShowRecByDate(df, dt);
            }
            if(this.txtTagNo.Text!="")
            {
                string str = this.txtTagNo.Text;
               
            }
        }

        private void chkDateRange_Click(object sender, EventArgs e)
        {
            if (chkDateRange.Checked == true)
            {
                this.dtpFrom.Enabled = true;
                this.dtpTo.Enabled = true;
            }
            else
            {
                this.dtpFrom.Enabled = false;
                this.dtpTo.Enabled = false;
            }
        }

        private void chkKarrat_Click(object sender, EventArgs e)
        {
            if (this.chkKarrat.Checked == true)
                this.cbxKarrat.Enabled = true;
            else
                this.cbxKarrat.Enabled = false;
        }

        private void cbxKarrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string r = (string)this.cbxKarrat.SelectedItem;
            this.ShowRecByKarat(r);
        }

        private void txtTagNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CultureInfo culInfo = CultureInfo.CurrentCulture;
                TextInfo txtInfo = culInfo.TextInfo;
                string str = this.txtTagNo.Text;
                this.txtTagNo.Text = txtInfo.ToTitleCase(str);
                this.ShowRecByTagNo(this.txtTagNo.Text);
            }
        }

        private void txtWFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
       && !char.IsDigit(e.KeyChar)
       && e.KeyChar != '.')
            {
                e.Handled = true;

            }
            else e.Handled = false;
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;

            }

            if (e.KeyChar == '\b')
                e.Handled = false;
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private void txtWTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
       && !char.IsDigit(e.KeyChar)
       && e.KeyChar != '.')
            {
                e.Handled = true;

            }
            else e.Handled = false;
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;

            }

            if (e.KeyChar == '\b')
                e.Handled = false;
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private void txtTagNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && (Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8 )
                e.Handled = true;
            else
                e.Handled = false;
        }

        private bool KeyCheck(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            if (e.KeyChar == 13 || e.KeyChar == 27)
                bFlag = true;
            return bFlag;
        }

        private void chkWeightRange_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void StockSearch_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void txtTagNo_KeyUp(object sender, KeyEventArgs e)
        {
            this.ShowRecByTagNo(this.txtTagNo.Text.ToUpper());
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dgvShowRecords_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvShowRecords.Rows[e.RowIndex].Cells[6].Value);
            int itmid = Convert.ToInt32(dgvShowRecords.Rows[e.RowIndex].Cells[5].Value);
            ShowSerachRecord sr = new ShowSerachRecord(mns.ShowSerach);
            sr(id, itmid);
            this.Hide();
        }

        private void txtDesNo_KeyUp(object sender, KeyEventArgs e)
        {
            this.ShowRecByDesNo(this.txtDesNo.Text.ToUpper());
        }
        
    }
}
