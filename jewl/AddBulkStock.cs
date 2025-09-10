using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BusinesEntities;

namespace jewl
{
    public partial class AddBulkStock : Form
    {
        ItemDAL itmDAL = new ItemDAL();
        StockDAL sDAL = new StockDAL();
        Stock stock = new Stock();
        public AddBulkStock()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void AddBulkStock_Load(object sender, EventArgs e)
        {

            this.cbxGroupItem.DataSource = itmDAL.GetAllItems();
            this.cbxGroupItem.DisplayMember = "ItemName";
            this.cbxGroupItem.ValueMember = "ItemId";
            this.cbxGroupItem.SelectedIndex = -1;

        }

        private void cbxGroupItem_SelectedIndexChanged(object sender, EventArgs e)
        {
           
          
           
          
        }
        private List<Stock> getTags(int id)
        {
            List<Stock> records = sDAL.GetAllTagNoByItemIdForBulk(id);
            List<Stock> lstStock = new List<Stock>();
            if(records!=null)
           // if (dgvItemAdded.Rows.Count <= 0 || records == null)
             //   return records;
           // else
            {
                
                if (lstStock.Count > 0)
                    lstStock.Clear();
                //lstStock = sDAL.GetAllTagNoByItemId(id);
                //foreach (Stock stk in records)
                //List<string> strList = new List<string>();
                List<Stock> lstTag = new List<Stock>();
                List<int> lstInt = new List<int>();
                //  
                //{

                //for (int i = 0; i < dgvItemAdded.Rows.Count; i++)
                {
                   // string str = dgvItemAdded.Rows[i].Cells[1].Value.ToString();
                    //strList.Add(str);
                    foreach (Stock stk in records)
                    {
                       // if (str.Equals(stk.TagNo))
                        {
                            //int num = records.IndexOf(stk);
                            //lstInt.Add(num);

                            //Stock s = new Stock();
                            lstTag.Add(stk);

                        }
                        //lstStock.Add(stk);
                    }

                }
                foreach (Stock s in records)
                {
                    lstStock.Add(s);
                }
                //foreach (Stock stks in lstTag )
                //{
                //    lstStock.re;
                //}
                //for (int i = 0; i < records .Count; i++)
                foreach (Stock st1 in records)
                {
                    //foreach (int j in lstInt )
                    //    if (i == j)
                    //    {
                    //        lstStock.RemoveAt(j);
                    //    }
                    //foreach (Stock s in lstTag)
                    //{
                    //    if (st1.StockId == s.StockId)
                    //    {
                    //        lstStock.Remove(st1);
                    //    }
                    //}

                }
                //foreach (Stock stk in records)
                //{

                //    foreach (string str1 in strList)
                //    {
                //        if (!(str1.Equals(stk.TagNo)))
                //            lstStock.Add(stk);
                //    }
                //}
               
            }
            return lstStock;
        }

        private void cbxGroupItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged += new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            int k = (int)this.cbxGroupItem.SelectedValue;
            this.cbxTagNo.DataSource = getTags(k);
            this.cbxTagNo.DisplayMember = "TagNo";
            this.cbxTagNo.ValueMember = "StockId";
            this.cbxTagNo.SelectedIndex = -1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            stock = new Stock();
            if (this.cbxTagNo.SelectedIndex == -1)
            {
                MessageBox.Show("Must Select TagNo",Messages.Header);
                return;
            }
            if (this.cbxGroupItem.SelectedIndex == -1)
            {
                MessageBox.Show("Must Select Item", Messages.Header);
                return;
            }
            if (this.txtWeight.Text==""&&this.txtQty.Text=="")
            {
                MessageBox.Show("Must Enter Weight or Qty", Messages.Header);
                return;
            }
            stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;
            stock.TagNo = this.cbxTagNo.Text;
            stock.StockDate = this.dtpDate.Value;
            stock.NetWeight =this.txtWeight.Text==""?0: Convert.ToDecimal(this.txtWeight.Text);
            stock.BWeight = this.txtWeight.Text == "" ? 0 : Convert.ToDecimal(this.txtWeight.Text);
            stock.BQuantity =this.txtQty.Text==""?0: Convert.ToInt32(this.txtQty.Text);
            sDAL.AddBulkStockTag(stock);
            sDAL.UpdateBulkStockTag(stock);
            MessageBox.Show(Messages.Saved,Messages.Header);
            this.Refresh();
        }
        public void Refresh()
        {
            this.cbxGroupItem.SelectedIndex = -1;
            this.cbxTagNo.SelectedIndex = -1;
            this.txtQty.Text = "";
            this.txtWeight.Text = "";
        }

        private void lblGroupItem_Click(object sender, EventArgs e)
        {

        }
    }
}
