using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using System.IO;
using System.Management;
using DAL;

namespace jewl
{
    public partial class VeiwPictures : Form
    {
        string st = "";
        bool pro = false;
        Process p = new Process();
        ItemDAL itmDAL = new ItemDAL();
        StockDAL stkDAL = new StockDAL();
        List<Stock> stks;
        Stock stock = new Stock();
        Bitmap bitmap;
        JewelPictures jp = new JewelPictures();
        PictureDAL pDAL = new PictureDAL();
        WorkerDAL wrkDAL = new WorkerDAL();
        Stock stk;
        StonesDAL sDAL = new StonesDAL();
        MemoryStream ms = new MemoryStream();

        public VeiwPictures()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void VeiwPictures_Load(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged -= new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            this.cbxWorkerName.SelectedIndexChanged -= new System.EventHandler(this.cbxWorkerName_SelectedIndexChanged);
            this.cbxStoneName.SelectedIndexChanged -= new EventHandler(cbxStoneName_SelectedIndexChanged);
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
            FormControls.FillCombobox(cbxWorkerName, wrkDAL.GetAllWorkers(), "Name", "ID");
            FormControls.FillCombobox(cbxStoneName, sDAL.GetAllStoneName(), "StoneName", "StoneId");
            this.rbtStockItems.Checked = true;
            this.cbxWorkerName.Enabled = false;
            this.cbxStoneName.Enabled = false;
            this.dtpFromDate.Enabled = false;
            this.dtpToDate.Enabled = false;
            this.txtWeightFrom.Enabled = false;
            this.txtWeightTo.Enabled = false;
            ShowDataGrid();
            pro = false;
        }

        #region CBX
        private void ShowDataGrid()
        {
            int i = 0, j = 1;
            this.dgvStonesDetail.Rows[i].Cells[0].Value = i + j;
            FormControls.FillCombobox(Column2, sDAL.GetAllStoneTypeName(), "TypeName", "TypeId");
            FormControls.FillCombobox(Column3, sDAL.GetAllStoneName(), "StoneName", "StoneId");
            FormControls.FillCombobox(Column8, sDAL.GetAllColorName(), "ColorName", "ColorId");
            FormControls.FillCombobox(Column9, sDAL.GetAllCutName(), "CutName", "CutId");
            FormControls.FillCombobox(Column10, sDAL.GetAllClearityName(), "ClearityName", "ClearityId");
            this.txtQty.Text = "1";
        }

        private void cbxGroupItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.chkDateRange.Checked = false;
            this.chkStoneName.Checked = false;
            this.chkWorkerName.Checked = false;
            this.chkWeightRange.Checked = false;
            this.txtWeightTo.Text = "";
            this.txtWeightFrom.Text = "";
            this.dtpFromDate.Enabled = false;
            this.dtpToDate.Enabled = false;
            if (this.cbxGroupItem.SelectedIndex == -1)
                return;
            else
            {
                int k = (int)this.cbxGroupItem.SelectedValue;
                this.lbxTagNos.Items.Clear();
                this.ShowListBox(k);
            }
        }

        private void cbxGroupItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged += new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
        }

        private void lbxTagNos_SelectedIndexChanged(object sender, EventArgs e)
        {
            stk = new Stock();
            stk = (Stock)this.lbxTagNos.SelectedItem;
            if (stk == null)
            {
                MessageBox.Show("You are not Selecting Tag!", Messages.Header);
                return;
            }
            else
            {
                this.ClearPbx();
                try
                {
                    this.ShowPics(stk.StockId);
                }
                catch
                { }
                this.ShowPictures(stk.TagNo);
            }
        }

        private void cbxWorkerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxWorkerName.SelectedIndex == -1)
                return;
            else
            {
                int j = (int)this.cbxGroupItem.SelectedValue;
                if (j == 0 || j == null)
                {
                    MessageBox.Show("Please select Item Name", Messages.Header);
                    return;
                }
                else
                {
                    int k = (int)this.cbxWorkerName.SelectedValue;
                    this.lbxTagNos.Items.Clear();
                    this.ShowListBoxByWrkId(k, j);
                }
            }
        }

        private void cbxStoneName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxStoneName.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                int i = (int)this.cbxGroupItem.SelectedValue;
                if (i == 0 || i == null)
                {
                    MessageBox.Show("Please select Item Name", Messages.Header);
                    return;
                }
                else
                {
                    if (this.chkWorkerName.Checked == true)
                    {
                        int j = (int)this.cbxWorkerName.SelectedValue;
                        int k = (int)this.cbxStoneName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowListBoxByStone(i, j, k);
                    }
                    else
                    {
                        int k = (int)this.cbxStoneName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowListBoxByStone(i, k);
                    }
                }
            }
        }
        #endregion

        #region Click
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                p.Kill();
            }
            catch { }
            File.Delete(st + "Image.Jpg");
            this.Close();
        }

        private void pbx1_Click(object sender, EventArgs e)
        {
            if (pbxMain != null)
            {
                Image tempimg;
                tempimg = this.pbxMain.Image;
                this.pbxMain.Image = null;
                this.pbxMain.Image = this.pbx1.Image;
                ms = new MemoryStream(jp.ConvertImageToBinary(this.pbxMain.Image));
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx1.Image = tempimg;
                this.pbx1.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.pbxMain.Image = this.pbx1.Image;
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx1.Image = null;
                this.pbx1.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void pbx2_Click(object sender, EventArgs e)
        {
            if (pbxMain != null)
            {
                Image tempimg;
                tempimg = this.pbxMain.Image;
                this.pbxMain.Image = null;
                this.pbxMain.Image = this.pbx2.Image;
                ms = new MemoryStream(jp.ConvertImageToBinary(this.pbxMain.Image));
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx2.Image = tempimg;
                this.pbx2.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.pbxMain.Image = this.pbx2.Image;
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx2.Image = null;
                this.pbx2.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void pbx3_Click(object sender, EventArgs e)
        {
            if (pbxMain != null)
            {
                Image tempimg;
                tempimg = this.pbxMain.Image;
                this.pbxMain.Image = null;
                this.pbxMain.Image = this.pbx3.Image;
                ms = new MemoryStream(jp.ConvertImageToBinary(this.pbxMain.Image));
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx3.Image = tempimg;
                this.pbx3.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.pbxMain.Image = this.pbx3.Image;
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx3.Image = null;
                this.pbx3.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void pbx4_Click(object sender, EventArgs e)
        {
            if (pbxMain != null)
            {
                Image tempimg;
                tempimg = this.pbxMain.Image;
                this.pbxMain.Image = null;
                this.pbxMain.Image = this.pbx4.Image;
                ms = new MemoryStream(jp.ConvertImageToBinary(this.pbxMain.Image));
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx4.Image = tempimg;
                this.pbx4.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.pbxMain.Image = this.pbx4.Image;
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx4.Image = null;
                this.pbx4.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void pbx5_Click(object sender, EventArgs e)
        {
            if (pbxMain != null)
            {
                Image tempimg;
                tempimg = this.pbxMain.Image;
                this.pbxMain.Image = null;
                this.pbxMain.Image = this.pbx5.Image;
                ms = new MemoryStream(jp.ConvertImageToBinary(this.pbxMain.Image));
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx5.Image = tempimg;
                this.pbx5.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.pbxMain.Image = this.pbx5.Image;
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx5.Image = null;
                this.pbx5.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void pbx6_Click(object sender, EventArgs e)
        {
            if (pbxMain != null)
            {
                Image tempimg;
                tempimg = this.pbxMain.Image;
                this.pbxMain.Image = null;
                this.pbxMain.Image = this.pbx6.Image;
                ms = new MemoryStream(jp.ConvertImageToBinary(this.pbxMain.Image));
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx6.Image = tempimg;
                this.pbx6.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.pbxMain.Image = this.pbx6.Image;
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx6.Image = null;
                this.pbx6.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void pbx7_Click(object sender, EventArgs e)
        {
            if (pbxMain != null)
            {
                Image tempimg;
                tempimg = this.pbxMain.Image;
                this.pbxMain.Image = null;
                this.pbxMain.Image = this.pbx7.Image;
                ms = new MemoryStream(jp.ConvertImageToBinary(this.pbxMain.Image));
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx7.Image = tempimg;
                this.pbx7.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.pbxMain.Image = this.pbx7.Image;
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx7.Image = null;
                this.pbx7.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void pbx8_Click(object sender, EventArgs e)
        {
            if (pbxMain != null)
            {
                Image tempimg;
                tempimg = this.pbxMain.Image;
                this.pbxMain.Image = null;
                this.pbxMain.Image = this.pbx8.Image;
                ms = new MemoryStream(jp.ConvertImageToBinary(this.pbxMain.Image));
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx8.Image = tempimg;
                this.pbx8.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.pbxMain.Image = this.pbx8.Image;
                this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                this.pbx8.Image = null;
                this.pbx8.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void btnShowPic_Click(object sender, EventArgs e)
        {
            this.ClearPbx();
            this.ShowPix(this.txtTagNo.Text);
            this.ShowPictures(this.txtTagNo.Text);
        }

        private void rbtPictureWithTagNo_Click(object sender, EventArgs e)
        {
            this.pnlStockItems.Visible = false;
            this.pnlPicsWithTag.Visible = true;
        }

        private void rbtStockItems_Click(object sender, EventArgs e)
        {
            this.pnlPicsWithTag.Visible = false;
            this.pnlStockItems.Visible = true;
        }
        #endregion

        #region Functions
        private void ShowPics(int stkId)
        {
            stock = new Stock();
            stock = stkDAL.GetStockBySockIdForStock(stkId);
            this.txtDescription.Text = stock.Description.ToString();
            if (stock.NetWeight == 0)
                this.txtWeight.Text = "";
            else
                this.txtWeight.Text = Convert.ToDecimal(stock.NetWeight).ToString("0.000");
            if (stock.WorkerName.ID == 0)
                this.txtWorkerName.Text = "";
            else
                this.txtWorkerName.Text = stock.WorkerName.Name;
            this.txtOrderNo.Text = stock.OrderNo.ToString();
            this.txtSaleNo.Text = stock.SaleNo.ToString();
            this.txtCustomerName.Text = stock.CustomerName.ToString();
            this.txtBillBookNo.Text = stock.BillBookNo.ToString();
            if (stock.Qty == 0)
                this.txtQty.Text = "";
            else
                this.txtQty.Text = stock.Qty.ToString();
            this.txtDesignNo.Text = stock.DesignNo.DesignNo.ToString();
            this.txtDescription.Text = stock.Description.ToString();
            this.dateTxt.Text = stock.StockDate.ToString();
            this.karatTxt.Text = stock.Karrat.ToString();
            this.txtSaleDate.Text = stock.SaleDate.ToString();
            if (stock.ImageMemory == null)
            {
                this.pbxMain.Image = null;
                this.pbxMain.BorderStyle = BorderStyle.FixedSingle;
                MessageBox.Show("No Picture to display", Messages.Header);
            }
            else
            {
                MemoryStream mst = new MemoryStream(stock.ImageMemory);
                this.pbxMain.Image = Image.FromStream(mst);
                ms = mst;
            }
            if (stock.StoneList == null)
                this.dgvStonesDetail.Rows.Clear();
            else
            {
                this.dgvStonesDetail.AutoGenerateColumns = false;
                int count = stock.StoneList.Count;
                this.dgvStonesDetail.Rows.Add(count);
                for (int i = 0; i < stock.StoneList.Count; i++)
                {
                    this.dgvStonesDetail.Rows[i].Cells[1].Value = stock.StoneList[i].StoneTypeId;
                    this.dgvStonesDetail.Rows[i].Cells[2].Value = stock.StoneList[i].StoneId;
                    if (stock.StoneList[i].StoneWeight.HasValue)
                        this.dgvStonesDetail.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(stock.StoneList[i].StoneWeight), 3);
                    else
                        this.dgvStonesDetail.Rows[i].Cells[3].Value = string.Empty;
                    if (stock.StoneList[i].Qty.HasValue)
                        this.dgvStonesDetail.Rows[i].Cells[4].Value = Convert.ToInt32(stock.StoneList[i].Qty);
                    else
                        this.dgvStonesDetail.Rows[i].Cells[4].Value = string.Empty;
                    if (stock.StoneList[i].Rate.HasValue)
                        this.dgvStonesDetail.Rows[i].Cells[5].Value = Math.Round(Convert.ToDecimal(stock.StoneList[i].Rate), 1);
                    else
                        this.dgvStonesDetail.Rows[i].Cells[5].Value = string.Empty;
                    if (stock.StoneList[i].Price.HasValue)
                        this.dgvStonesDetail.Rows[i].Cells[6].Value = Math.Round(Convert.ToDecimal(stock.StoneList[i].Price), 0);
                    else
                        this.dgvStonesDetail.Rows[i].Cells[6].Value = string.Empty;
                    if (!(string.IsNullOrEmpty(stock.StoneList[i].ColorName.ColorName.ToString())))
                    {
                        for (int j = 0; j < this.Column8.Items.Count; j++)
                        {
                            StoneColor stc = (StoneColor)this.Column8.Items[j];
                            if (stock.StoneList[i].ColorName.ColorName.Equals(stc.ColorName.ToString()))
                                this.dgvStonesDetail.Rows[i].Cells[7].Value = Convert.ToInt32(stc.ColorId);
                        }
                    }
                    if (!(string.IsNullOrEmpty(stock.StoneList[i].CutName.CutName)))
                    {
                        for (int j = 0; j < this.Column9.Items.Count; j++)
                        {
                            StoneCut stc = (StoneCut)this.Column9.Items[j];
                            if (stock.StoneList[i].CutName.CutName.Equals(stc.CutName.ToString()))
                                this.dgvStonesDetail.Rows[i].Cells[8].Value = Convert.ToInt32(stc.CutId);
                        }
                    }
                    if (!(string.IsNullOrEmpty(stock.StoneList[i].ClearityName.ClearityName.ToString())))
                    {
                        for (int j = 0; j < this.Column10.Items.Count; j++)
                        {
                            StoneClearity stc = (StoneClearity)this.Column10.Items[j];
                            if (stock.StoneList[i].ClearityName.ClearityName.Equals(stc.ClearityName.ToString()))
                                this.dgvStonesDetail.Rows[i].Cells[9].Value = Convert.ToInt32(stc.ClearityId);
                        }
                    }
                }
            }
            if (stock.StoneList == null)
            {
                this.txtStonesWeight.Text = "";
                return;
            }
            else
            {
                int count = stock.StoneList.Count;
                for (int i = 0; i < stock.StoneList.Count; i++)
                {
                    if (stock.StoneList[i].StoneWeight.HasValue)
                        this.txtStonesWeight.Text = ((decimal)stock.StoneList[i].StoneWeight).ToString("0.000");
                    else
                        this.txtStonesWeight.Text = "";
                }
            }
        }

        private void ShowPictures(string tagNo)
        {
            jp = new JewelPictures();
            jp = pDAL.GetPictures(tagNo);
            if (jp == null)
                return;
            else
            {
                if (jp.ImageMemory == null)
                {
                    this.pbxMain.Image = null;
                    this.pbxMain.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(jp.ImageMemory);
                    this.pbxMain.Image = Image.FromStream(mst);
                    ms = new MemoryStream(jp.ConvertImageToBinary(this.pbxMain.Image));
                }
                if (jp.ImageMemory1 == null)
                {
                    this.pbx1.Image = null;
                    this.pbx1.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(jp.ImageMemory1);
                    this.pbx1.Image = Image.FromStream(mst);
                }
                if (jp.ImageMemory2 == null)
                {
                    this.pbx2.Image = null;
                    this.pbx2.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(jp.ImageMemory2);
                    this.pbx2.Image = Image.FromStream(mst);
                }
                if (jp.ImageMemory3 == null)
                {
                    this.pbx3.Image = null;
                    this.pbx3.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(jp.ImageMemory3);
                    this.pbx3.Image = Image.FromStream(mst);
                }
                if (jp.ImageMemory4 == null)
                {
                    this.pbx4.Image = null;
                    this.pbx4.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(jp.ImageMemory4);
                    this.pbx4.Image = Image.FromStream(mst);
                }
                if (jp.ImageMemory5 == null)
                {
                    this.pbx5.Image = null;
                    this.pbx5.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(jp.ImageMemory5);
                    this.pbx5.Image = Image.FromStream(mst);
                }
                if (jp.ImageMemory6 == null)
                {
                    this.pbx6.Image = null;
                    this.pbx6.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(jp.ImageMemory6);
                    this.pbx6.Image = Image.FromStream(mst);
                }
                if (jp.ImageMemory7 == null)
                {
                    this.pbx7.Image = null;
                    this.pbx7.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(jp.ImageMemory7);
                    this.pbx7.Image = Image.FromStream(mst);
                }
                if (jp.ImageMemory8 == null)
                {
                    this.pbx8.Image = null;
                    this.pbx8.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(jp.ImageMemory8);
                    this.pbx8.Image = Image.FromStream(mst);
                }
            }
        }

        private void ShowListBox(int r)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetAllTagNosByItemId("select StockId, TagNo from Stock where ItemId = " + r + " and Status = 'Available' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetAllTagNosByItemId("select StockId ,TagNo from Stock where ItemId='" + r + "' and Status='Not Available' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetAllTagNosByItemId("select StockId ,TagNo from Stock where ItemId='" + r + "' and Status='Damage' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetAllTagNosByItemId("select StockId ,TagNo from Stock where ItemId='" + r + "' and Status='Deleted' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetAllTagNosByItemId("select StockId ,TagNo from Stock where ItemId='" + r + "' and Status='Sample' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowListBox(int r, int k)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetAllTagNosByItemId("select StockId ,TagNo from Stock where ItemId=" + r + " and Status='Available' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetAllTagNosByItemId("select StockId ,TagNo from Stock where ItemId='" + r + "' and Status='Not Available' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetAllTagNosByItemId("select StockId ,TagNo from Stock where ItemId='" + r + "' and Status='Damage' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetAllTagNosByItemId("select StockId ,TagNo from Stock where ItemId='" + r + "' and Status='Deleted' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetAllTagNosByItemId("select StockId ,TagNo from Stock where ItemId='" + r + "' and Status='Sample' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowListBoxByWrkId(int wid, int itmid)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = pDAL.GetAllTagNoByWrkId("select StockId ,TagNo from Stock where WorkerId=" + wid + "and ItemId=" + itmid + "and Status='Available' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = pDAL.GetAllTagNoByWrkId("select StockId ,TagNo from Stock where WorkerId=" + wid + "and ItemId=" + itmid + "and Status='Not Available' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = pDAL.GetAllTagNoByWrkId("select StockId ,TagNo from Stock where WorkerId=" + wid + "and ItemId=" + itmid + "and Status='Damage' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = pDAL.GetAllTagNoByWrkId("select StockId ,TagNo from Stock where WorkerId=" + wid + "and ItemId=" + itmid + "and Status='Deleted' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = pDAL.GetAllTagNoByWrkId("select StockId ,TagNo from Stock where WorkerId=" + wid + "and ItemId=" + itmid + "and Status='Sample' and BStatus <> 'Bulk' order by tagno");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        #region stone
        private void ShowListBoxByStone(int itmid, int wid, int stid)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where WorkerId=" + wid + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where WorkerId=" + wid + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where WorkerId=" + wid + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where WorkerId=" + wid + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where WorkerId=" + wid + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowListBoxByStone(int itmid, int stid)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        #endregion

        #region date
        private void ShowListBoxByDate(int itmid, int wid, int stid, DateTime df, DateTime dt)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and WorkerId=" + wid + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId='" + itmid + "'and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and WorkerId=" + wid + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId='" + itmid + "'and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and WorkerId=" + wid + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId='" + itmid + "'and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and WorkerId=" + wid + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId='" + itmid + "'and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and WorkerId=" + wid + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId='" + itmid + "'and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowListBoxByDate(int itmid, int wid, DateTime df, DateTime dt)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and WorkerId='" + wid + "'and ItemId='" + itmid + "'and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and WorkerId='" + wid + "'and ItemId='" + itmid + "'and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and WorkerId='" + wid + "'and ItemId='" + itmid + "'and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and WorkerId='" + wid + "'and ItemId='" + itmid + "'and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and WorkerId='" + wid + "'and ItemId='" + itmid + "'and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowListBoxByDateStid(int itmid, int stid, DateTime df, DateTime dt)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId='" + itmid + "'and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId='" + itmid + "'and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId='" + itmid + "'and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId='" + itmid + "'and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "' and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId='" + itmid + "'and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowListBoxByDate(int itmid, DateTime df, DateTime dt)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId='" + itmid + "'and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId='" + itmid + "'and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId='" + itmid + "'and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId='" + itmid + "'and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByDate("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId='" + itmid + "'and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }
        #endregion

        #region weight
        private void ShowRecByWt(int itmid, int wid, int stid, decimal wt1, decimal wt2, DateTime df, DateTime dt)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowRecByWt(int itmid, int stid, decimal wt1, decimal wt2, DateTime df, DateTime dt)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowRecByWtByWrk(int itmid, int wid, decimal wt1, decimal wt2, DateTime df, DateTime dt)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }

                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowRecByWt(int itmid, decimal wt1, decimal wt2, DateTime df, DateTime dt)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId=" + itmid + "and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId=" + itmid + "and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId=" + itmid + "and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId=" + itmid + "and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and StockDate BETWEEN '" + df + "' and '" + dt + "'and ItemId=" + itmid + "and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowRecByWt(int itmid, int wid, int stid, decimal wt1, decimal wt2)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + " and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ") and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowRecByWt(int itmid, int stid, decimal wt1, decimal wt2)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ")and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ")and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ")and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ")and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and TagNo=(select tagno from stonesdetail where stoneid=" + stid + ")and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowRecByWtByWrk(int itmid, int wid, decimal wt1, decimal wt2)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and workerid=" + wid + "and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }

        private void ShowRecByWt(int itmid, decimal wt1, decimal wt2)
        {
            if (this.rbtStockItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and Status = 'Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSoldOutItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and Status = 'Not Available'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDamageItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and Status = 'Damage'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtDeletedItems.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and Status = 'Deleted'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
            else if (this.rbtSampleItem.Checked == true)
            {
                stks = stkDAL.GetRecordsByWeight("select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where NetWeight BETWEEN " + wt1 + " and " + wt2 + "and ItemId=" + itmid + "and Status = 'Sample'");
                if (stks == null)
                    return;
                else
                {
                    foreach (Stock stk in this.stks)
                    {
                        lbxTagNos.Items.Add(stk);
                    }
                    this.lbxTagNos.DisplayMember = "TagNo";
                    this.lbxTagNos.ValueMember = "StockId";
                }
            }
        }
        #endregion

        private void ShowPix(string tagNo)
        {
            stock = new Stock();
            if (this.txtTagNo.Text != string.Empty)
            {
                stock = pDAL.GetStkPics(this.txtTagNo.Text);
                if (stock.Description != "")
                    this.txtDescription.Text = stock.Description.ToString();
                else
                    this.txtDescription.Text = "";
                this.txtStatus.Text = stock.Status.ToString();
                if (stock.NetWeight.HasValue)
                    this.txtWeight.Text = Convert.ToDecimal(stock.NetWeight).ToString("0.000");
                else
                    this.txtWeight.Text = "";

                if (stock.WorkerName.ID.HasValue)
                    this.txtWorkerName.Text = stock.WorkerName.Name;
                else
                    this.txtWorkerName.Text = "";

                if (stock.Qty.HasValue)
                    this.txtQty.Text = stock.Qty.ToString();
                else
                    this.txtQty.Text = "";

                if (stock.ImageMemory == null)
                {
                    this.pbxMain.Image = null;
                    this.pbxMain.BorderStyle = BorderStyle.FixedSingle;
                    MessageBox.Show("No Picture to display", Messages.Header);
                }
                else
                {
                    MemoryStream mst = new MemoryStream(stock.ImageMemory);
                    this.pbxMain.Image = Image.FromStream(mst);
                }
                if (stock.StoneList == null)
                    return;
                else
                {
                    int count = stock.StoneList.Count;
                    for (int i = 0; i < stock.StoneList.Count; i++)
                    {
                        if (stock.StoneList[i].StoneWeight.HasValue)
                            this.txtStonesWeight.Text = (stock.StoneList[i].StoneWeight).ToString();
                        else
                            this.txtStonesWeight.Text = "";
                    }
                }
            }
        }

        //Generate new image dimensions
        public Size GenerateImageDimensions(int currW, int currH, int destW, int destH)
        {
            //decimal to hold the final multiplier to use when scaling the image
            decimal multiplier = 0;
            //string for holding layout
            string layout;
            //determine if it's Portrait or Landscape
            if (currH > currW) 
                layout = "portrait";
            else 
                layout = "landscape";

            switch (layout.ToLower())
            {
                case "portrait":
                    //calculate multiplier on heights
                    if (destH > destW)
                        multiplier = (decimal)destW / (decimal)currW;
                    else
                        multiplier = (decimal)destH / (decimal)currH;
                    break;
                case "landscape":
                    //calculate multiplier on widths
                    if (destH > destW)
                        multiplier = (decimal)destW / (decimal)currW;
                    else
                        multiplier = (decimal)destH / (decimal)currH;
                    break;
            }
            return new Size((int)(currW * multiplier), (int)(currH * multiplier));//return the new image dimensions
        }

        //Resize the image
        private void SetImage(PictureBox pb)
        {
            try
            {
                Image img = pb.Image;//create a temp image
                Size imgSize = GenerateImageDimensions(img.Width, img.Height, this.pbxMain.Width, this.pbxMain.Height);//calculate the size of the image
                Bitmap finalImg = new Bitmap(img, imgSize.Width, imgSize.Height);//create a new Bitmap with the proper dimensions
                Graphics gfx = Graphics.FromImage(img);//create a new Graphics object from the image
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;//clean up the image (take care of any image loss from resizing)
                pb.Image = null;//empty the PictureBox
                pb.SizeMode = PictureBoxSizeMode.CenterImage;//center the new image
                pb.Image = finalImg;//set the new image
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ClearPbx()
        {
            this.pbxMain.Image = null;
            this.pbx1.Image = null;
            this.pbx2.Image = null;
            this.pbx3.Image = null;
            this.pbx4.Image = null;
            this.pbx5.Image = null;
            this.pbx6.Image = null;
            this.pbx7.Image = null;
            this.pbx8.Image = null;
            this.txtDescription.Text = "";
            this.txtQty.Text = "";
            this.txtStatus.Text = "";
            this.txtStonesWeight.Text = "";
            this.txtWeight.Text = "";
            this.txtWorkerName.Text = "";
            this.txtOrderNo.Text = "";
            this.txtSaleNo.Text = "";
            this.txtCustomerName.Text = "";
            this.txtBillBookNo.Text = "";
        }
        #endregion

        private void rbt_CheckedChanged(object sender, EventArgs e)
        {
            this.lbxTagNos.Items.Clear();
            this.ClearPbx();
            lblItems.Text = ((RadioButton)sender).Text;
        }

        private void chkWorkerName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkWorkerName.Checked == true)
            {
                this.cbxWorkerName.Enabled = true;
            }
            else
            {
                this.cbxWorkerName.Enabled = false;
                this.cbxWorkerName.SelectedIndex = -1;
            }
        }

        private void cbxWorkerName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxWorkerName.SelectedIndexChanged += new System.EventHandler(this.cbxWorkerName_SelectedIndexChanged);
        }

        private void rbtSoldOutItems_Click(object sender, EventArgs e)
        {
            this.pnlPicsWithTag.Visible = false;
            this.pnlStockItems.Visible = true;
        }

        private void chkDateRange_Click(object sender, EventArgs e)
        {
            if (this.cbxGroupItem.Text == "")
            {
                MessageBox.Show("Select First Group Item", Messages.Header);
                this.chkDateRange.Checked = false;
                return;
            }
            if (chkDateRange.Checked == true)
            {
                this.dtpFromDate.Enabled = true;
                this.dtpToDate.Enabled = true;
            }
            else
            {
                this.dtpFromDate.Enabled = false;
                this.dtpToDate.Enabled = false;
            }
        }

        private void pbxMain_DoubleClick(object sender, EventArgs e)
        {
            Process[] pArry = Process.GetProcesses();
            foreach (Process p in pArry)
            {
                string s = p.ProcessName;
                s = s.ToLower();
                if (s.CompareTo("rundll32") == 0)
                {
                    p.Kill();
                }
            }
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.Name != "C:\\" && d.IsReady && d.DriveType == DriveType.Fixed)
                {
                    st = d.Name;
                }
            }
            if (jp != null)
            {
                if (jp.ImageMemory == null)
                    return;
                else
                {
                    Image Img = Image.FromStream(ms);
                    try
                    {
                        using (Bitmap bitmap = new Bitmap(Img))
                        {
                            bitmap.Save(st + "Image.Jpg", ImageFormat.Jpeg);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error has occured: \n" + ex.Message);
                    }
                    p.StartInfo.FileName = "rundll32.exe";
                    p.StartInfo.Arguments = @"C:\WINDOWS\System32\shimgvw.dll,ImageView_Fullscreen " + st + "Image.Jpg";
                    p.Start();
                    pro = true;
                }
            }
        }

        private void chkWeightRange_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkWeightRange.Checked == true)
            {
                this.txtWeightFrom.Enabled = true;
                this.txtWeightTo.Enabled = true;
            }
            else if (this.chkWeightRange.Checked == false)
            {
                this.txtWeightFrom.Enabled = false;
                this.txtWeightTo.Enabled = false;
            }
        }

        private void txtWeightTo_KeyDown(object sender, KeyEventArgs e)
        {
            int i;
            decimal t = 0, u = 0;
            if (this.txtWeightFrom.Text != string.Empty && this.txtWeightTo.Text != string.Empty)
            {
                t = Convert.ToDecimal(this.txtWeightFrom.Text);
                u = Convert.ToDecimal(this.txtWeightTo.Text);
            }
            i = (int)this.cbxGroupItem.SelectedValue;
            if (i == 0 || i == null)
            {
                MessageBox.Show("Please select Item Name", Messages.Header);
                return;
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.chkWorkerName.Checked == true && this.chkStoneName.Checked == true && this.chkDateRange.Checked == true)
                    {
                        int k = (int)this.cbxWorkerName.SelectedValue;
                        int l = (int)this.cbxStoneName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowRecByWt(i, k, l, t, u, dtpFromDate.Value, dtpToDate.Value);
                    }
                    else if (this.chkWorkerName.Checked == true && this.chkDateRange.Checked == true)
                    {
                        int k = (int)this.cbxWorkerName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowRecByWtByWrk(i, k, t, u, dtpFromDate.Value, dtpToDate.Value);
                    }
                    else if (this.chkStoneName.Checked == true && this.chkDateRange.Checked == true)
                    {
                        int l = (int)this.cbxStoneName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowRecByWt(i, l, t, u, dtpFromDate.Value, dtpToDate.Value);
                    }
                    else if (this.chkDateRange.Checked == true)
                    {
                        this.ShowRecByWt(i, t, u, dtpFromDate.Value, dtpToDate.Value);
                    }
                    else if (this.chkWorkerName.Checked == true && this.chkStoneName.Checked == true)
                    {
                        int k = (int)this.cbxWorkerName.SelectedValue;
                        int l = (int)this.cbxStoneName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowRecByWt(i, k, l, t, u);
                    }
                    else if (this.chkStoneName.Checked == true)
                    {
                        int l = (int)this.cbxStoneName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowRecByWt(i, l, t, u);
                    }
                    else if (this.chkWorkerName.Checked == true)
                    {
                        int k = (int)this.cbxWorkerName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowRecByWtByWrk(i, k, t, u);
                    }
                    else
                    {
                        this.lbxTagNos.Items.Clear();
                        this.ShowRecByWt(i, t, u);
                    }
                }
            }
        }

        private void rbtDamageItems_Click(object sender, EventArgs e)
        {
            this.pnlPicsWithTag.Visible = false;
            this.pnlStockItems.Visible = true;
        }

        private void rbtDeletedItems_Click(object sender, EventArgs e)
        {
            this.pnlPicsWithTag.Visible = false;
            this.pnlStockItems.Visible = true;
        }

        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            int i = (int)this.cbxGroupItem.SelectedValue;
            if (i == 0 || i == null)
            {
                MessageBox.Show("Please select Item Name", Messages.Header);
                return;
            }
            else
            {
                if (this.chkWorkerName.Checked == true && this.chkStoneName.Checked == true)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        int k = (int)this.cbxWorkerName.SelectedValue;
                        int l = (int)this.cbxStoneName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowListBoxByDate(i, k, l, dtpFromDate.Value, dtpToDate.Value);
                    }
                }
                else if (this.chkStoneName.Checked == true)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        int l = (int)this.cbxStoneName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowListBoxByDateStid(i, l, dtpFromDate.Value, dtpToDate.Value);
                    }
                }
                else if (this.chkWorkerName.Checked == true)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        int k = (int)this.cbxWorkerName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowListBoxByDate(i, k, dtpFromDate.Value, dtpToDate.Value);
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        this.lbxTagNos.Items.Clear();
                        this.ShowListBoxByDate(i, dtpFromDate.Value, dtpToDate.Value);
                    }
                }
            }
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            int i = (int)this.cbxGroupItem.SelectedValue;
            if (i == 0 || i == null)
            {
                MessageBox.Show("Please select Item Name", Messages.Header);
                return;
            }
            else
            {
                if (this.chkWorkerName.Checked == true && this.chkStoneName.Checked == true)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        int k = (int)this.cbxWorkerName.SelectedValue;
                        int l = (int)this.cbxStoneName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowListBoxByDate(i, k, l, dtpFromDate.Value, dtpToDate.Value);
                    }
                }
                else if (this.chkStoneName.Checked == true)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        int l = (int)this.cbxStoneName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowListBoxByDateStid(i, l, dtpFromDate.Value, dtpToDate.Value);
                    }
                }
                else if (this.chkWorkerName.Checked == true)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        int k = (int)this.cbxWorkerName.SelectedValue;
                        this.lbxTagNos.Items.Clear();
                        this.ShowListBoxByDate(i, k, dtpFromDate.Value, dtpToDate.Value);
                    }
                }
                else
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        this.lbxTagNos.Items.Clear();
                        this.ShowListBoxByDate(i, dtpFromDate.Value, dtpToDate.Value);
                    }
                }
            }
        }

        private void chkStoneName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkStoneName.Checked == true)
            {
                this.cbxStoneName.Enabled = true;
            }
            else
            {
                this.cbxStoneName.Enabled = false;
                this.cbxStoneName.SelectedIndex = -1;
            }
        }

        private void cbxStoneName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxStoneName.SelectedIndexChanged += new EventHandler(cbxStoneName_SelectedIndexChanged);
        }

        private void chkWeightRange_Click(object sender, EventArgs e)
        {
            if (this.cbxGroupItem.Text == "")
            {
                MessageBox.Show("Select First Group Item", Messages.Header);
                this.chkWeightRange.Checked = false;
                return;
            }
        }

        private void chkWorkerName_Click(object sender, EventArgs e)
        {
            if (this.cbxGroupItem.Text == "")
            {
                MessageBox.Show("Select First Group Item", Messages.Header);
                this.chkWorkerName.Checked = false;
                return;
            }
        }

        private void chkStoneName_Click(object sender, EventArgs e)
        {
            if (this.cbxGroupItem.Text == "")
            {
                MessageBox.Show("Select First Group Item", Messages.Header);
                this.chkStoneName.Checked = false;
                return;
            }
        }

        private void txtTagNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnShowPic_Click(sender, e);
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            Process[] pArry = Process.GetProcesses();
            foreach (Process p in pArry)
            {
                string s = p.ProcessName;
                s = s.ToLower();
                if (s.CompareTo("rundll32") == 0)
                {
                    p.Kill();
                }
            }
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.Name != "C:\\" && d.IsReady && d.DriveType == DriveType.Fixed)
                {
                    st = d.Name;
                }
            }
            if (jp != null)
            {
                if (jp.ImageMemory == null)
                    return;
                else
                {
                    Image Img = Image.FromStream(ms);
                    try
                    {
                        using (Bitmap bitmap = new Bitmap(Img))
                        {
                            bitmap.Save(st + "Image.Jpg", ImageFormat.Jpeg);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Error has occured: \n" + ex.Message);
                    }
                    p.StartInfo.FileName = "rundll32.exe";
                    p.StartInfo.Arguments = @"C:\WINDOWS\System32\shimgvw.dll,ImageView_Fullscreen " + st + "Image.Jpg";
                    p.Start();
                    pro = true;
                }
            }
            else
            { }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FillPictureBox();
        }
        private void FillPictureBox()
        {
            if (this.pbxMain.Image != null)
            { }
            else
            {
                DialogResult result = openFileDialog1.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    Image img = Image.FromFile(this.openFileDialog1.FileName);
                    bitmap = new Bitmap(img);
                    this.pbxMain.Image = bitmap;
                    this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            stk = new Stock();
            stk = (Stock)this.lbxTagNos.SelectedItem;
            jp = new JewelPictures();
            if (pbxMain.Image != null)
            {
                jp.ImageMemorySmall = jp.ConvertImageToBinary(jp.resizeImage(pbxMain.Image, pbxMain.Size));
                jp.ImageMemory = jp.ConvertImageToBinary(this.pbxMain.Image);
            }
            else
            {
                jp.ImageMemory = null;
                jp.ImageMemorySmall = null;
            }

            if (pbx1.Image != null)
                jp.ImageMemory1 = jp.ConvertImageToBinary(this.pbx1.Image);
            else
                jp.ImageMemory1 = null;
            if (pbx2.Image != null)
                jp.ImageMemory2 = jp.ConvertImageToBinary(this.pbx2.Image);
            else
                jp.ImageMemory2 = null;
            if (pbx3.Image != null)
                jp.ImageMemory3 = jp.ConvertImageToBinary(this.pbx3.Image);
            else
                jp.ImageMemory3 = null;
            if (pbx4.Image != null)
                jp.ImageMemory4 = jp.ConvertImageToBinary(this.pbx4.Image);
            else
                jp.ImageMemory4 = null;
            if (pbx5.Image != null)
                jp.ImageMemory5 = jp.ConvertImageToBinary(this.pbx5.Image);
            else
                jp.ImageMemory5 = null;
            if (pbx6.Image != null)
                jp.ImageMemory6 = jp.ConvertImageToBinary(this.pbx6.Image);
            else
                jp.ImageMemory6 = null;
            if (pbx7.Image != null)
                jp.ImageMemory7 = jp.ConvertImageToBinary(this.pbx7.Image);
            else
                jp.ImageMemory7 = null;
            if (pbx8.Image != null)
                jp.ImageMemory8 = jp.ConvertImageToBinary(this.pbx7.Image);
            else
                jp.ImageMemory8 = null;

            bool pFlag = stkDAL.isTagNoExistInDbPics(stk.TagNo);
            if (pFlag == true)
                stkDAL.UpdatePics(stk.TagNo, jp);
            else
                stkDAL.AddPics(jp);
            MessageBox.Show("Saved Successfully", Messages.Header);
            this.pbxMain.Image = null;
        }

        private void rbtSampleItem_Click(object sender, EventArgs e)
        {
            this.pnlPicsWithTag.Visible = false;
            this.pnlStockItems.Visible = true;
        }
    }
}
