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
    public partial class frmInputStockPics : Form
    {
        ItemDAL itmDAL = new ItemDAL();
        StockDAL stkDAL = new StockDAL();
        Item itm = new Item();
        public frmInputStockPics()
        {
            InitializeComponent();
        }

        private void frmInputStockPics_Load(object sender, EventArgs e)
        {
            this.cbxItemName.SelectedIndexChanged -= new EventHandler(cbxItemName_SelectedIndexChanged); 
            this.cbxItemName.DataSource = itmDAL.GetAllItems();
            this.cbxItemName.DisplayMember = "ItemName";
            this.cbxItemName.ValueMember = "ItemId";
            this.cbxItemName.SelectedIndex = -1;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            //int  bFlag = 0;
            //if (this.txtFrom.Text.Length != itm.Abrivation.Length + 4 || this.txtTo.Text.Length != itm.Abrivation.Length + 4)
            //    MessageBox.Show("TagNo is Not Correct");
            //else
            //{
            //    bFlag = stkDAL.GetCountOfPicsTags(this.txtFrom.Text, this.txtTo.Text);
            //    if (bFlag > 10)
            //        MessageBox.Show("Range Greater than 10 Tags", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    else
            //    {
            //        ViewPicturesRpt frm = new ViewPicturesRpt();
            //        frm.tagF = this.txtFrom.Text;
            //        frm.tagT = this.txtTo.Text;
            //        frm.ShowDialog();
            //        frm.ds.Clear();
            //        frm.ds.Dispose();
            //        frm.DisposeAllControl();
            //        frm.Dispose ();
            //    }
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void cbxItemName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxItemName.SelectedIndexChanged += new EventHandler(cbxItemName_SelectedIndexChanged); 
        }

        private void cbxItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            itm = (Item)this.cbxItemName.SelectedItem;
            if (itm == null)
                return;
            else
            {

                this.txtFrom.Text = itm.Abrivation.ToString();
                this.txtTo.Text = itm.Abrivation.ToString();
            }
        }
    }
}
