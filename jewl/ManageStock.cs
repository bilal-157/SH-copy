using System;
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
using DAL;
using System.IO;
using System.Globalization;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO.Ports;
using System.Threading;

namespace jewl
{
    public delegate void ShowSerachRecord(int x, int y);
    public partial class ManageStock : Form
    {
        private WebCam_Capture.WebCamCapture WebCamCapture;
        string st = "", strr = "";
        string newPath, activeDir = "";
        Bitmap bitmap;
        int t = 0;
        DataGridViewComboBoxColumn dgvCbx = new DataGridViewComboBoxColumn();
        OrderDAL oDAL = new OrderDAL();
        DesignDAL desDAL = new DesignDAL();
        ItemDAL itmDAL = new ItemDAL();
        WorkerDAL wrkDAL = new WorkerDAL();
        StonesDAL sDAL = new StonesDAL();
        Item itm = new Item();
        Worker wrk = new Worker();
        Stones stns = new Stones();
        SaleDAL slDAL = new SaleDAL();
        Formulas frm = new Formulas();
        OrderLineItem olitm;
        Panel pnl = new Panel();
        Stock stock = new Stock();
        StockDAL stkDAL = new StockDAL();
        JewelPictures jp = new JewelPictures();
        MemoryStream mst;
        PictureDAL pDAL = new PictureDAL();
        GoldRateDAL grDAL = new GoldRateDAL();
        Supplier supplier;
        string str, pAcc;
        public bool lFlag = false;
        public bool CostFlag = false;
        decimal s;
        bool bflag, editFlag = false;
        private bool DeviceExist = false;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource = null;
        BarCodeReportViewer brv;
        private SerialPort _serialPort = new SerialPort();

        public ManageStock()
        {
            InitializeComponent();
            InitializeComponent(0);
            FormControls.GetAllControls(this);
            this.txtTagNo.ForeColor = Color.FromArgb(0, 188, 212);
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        }

        #region Components
        private void InitializeComponent(int i)
        {
            this.WebCamCapture = new WebCam_Capture.WebCamCapture();
            this.WebCamCapture.CaptureHeight = 288;
            this.WebCamCapture.CaptureWidth = 202;
            this.WebCamCapture.FrameNumber = ((ulong)(0ul));
            this.WebCamCapture.Location = new System.Drawing.Point(17, 17);
            this.WebCamCapture.Name = "WebCamCapture";
            this.WebCamCapture.Size = new System.Drawing.Size(342, 252);
            this.WebCamCapture.TabIndex = 0;
            this.WebCamCapture.TimeToCapture_milliseconds = 100;
            this.WebCamCapture.ImageCaptured += new WebCam_Capture.WebCamCapture.WebCamEventHandler(this.WebCamCapture_ImageCaptured);
            this.Load += new System.EventHandler(this.ManageStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCaptureTime)).EndInit();
        }
        #endregion

        #region keyPress
        private void txtTotalNetWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
            this.txtTotalNetWeight.Leave += new EventHandler(txtTotalNetWeight_Leave);
        }

        private void txtWaste_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtWasteInGm_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtRatti_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtMakingPerGm_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtTotalMaking_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtLacquer_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtTotalLacquer_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
            if (e.KeyChar == '\b')
                e.Handled = false;
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
            if (e.KeyChar == 46)
                e.Handled = true;
        }

        private void txtPieces_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
            if (e.KeyChar == '\b')
                e.Handled = false;
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
            if (e.KeyChar == 46)
                e.Handled = true;
        }

        private void txtSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
            if (e.KeyChar == '\b')
                e.Handled = false;
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private void txtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private void txtMakingType_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private void txtItemCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private void txtSalePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }
        #endregion

        #region clickEvent
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
               stock = new Stock();
            jp = new JewelPictures();
            if (this.rbtGold.Checked == true)
                stock.ItemType = ItemType.Gold;
            else if (this.rbtDiamond.Checked == true)
                stock.ItemType = ItemType.Diamond;
            else if (this.rbtSilver.Checked == true)
                stock.ItemType = ItemType.Silver;
            else if (this.rbtPladium.Checked == true)
                stock.ItemType = ItemType.Pladium;
            else
                stock.ItemType = ItemType.Platinum;
            if (this.rbtStockItem.Checked == true)
                stock.ItemFor = ItemFor.Sale;
            if (this.rbtOrderItem.Checked == true)
            {
                stock.ItemFor = ItemFor.Order;
                stock.OrderNo = Convert.ToInt32(this.cbxOrderNo.Text);
                stock.OItemId = this.cbxOItemId.Text;
            }
            stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;
            for (int i = 0; i < dgvStonesDetail.Rows.Count; i++)
            {
                if (this.dgvStonesDetail.Rows[i].Cells[1].Value != null)
                {
                    if (this.dgvStonesDetail.Rows[i].Cells[2].Value == null)
                    {
                        MessageBox.Show("First Select StoneName", Messages.Header);
                        return;
                    }
                }
            }
            if (this.txtTagNo.Text == "")
            {
                MessageBox.Show("There is no TagNo to Save", Messages.Header);
                return;
            }
            else
            {
                stock.TagNo = stkDAL.GenrateTagNo((int)this.cbxGroupItem.SelectedValue, stock.ItemName.Abrivation);
                jp.TagNo = stock.TagNo;
                stock.BarCode = "*" + stock.TagNo + "*";
            }
            stock.Silver = new Silver();
            stock.Silver.RateA = 0;
            stock.Silver.PriceA = 0;
            stock.Silver.RateD = 0;
            stock.Silver.PriceD = 0;
            stock.Silver.SalePrice = 0;
            if (this.rbtSilver.Checked == true)
            {
                if (this.txtSilverNetWeight.Text == "")
                {
                    MessageBox.Show("There is no Weight to Save", Messages.Header);
                    return;
                }
                else
                {
                    stock.NetWeight = FormControls.GetDecimalValue(this.txtSilverNetWeight, 3);
                    stock.TTola = 0;
                    stock.TMasha = 0;
                    stock.TRatti = 0;
                    stock.Silver = new Silver();
                    stock.Silver.RateA = FormControls.GetDecimalValue(this.txtRateA, 1);
                    stock.Silver.PriceA = FormControls.GetDecimalValue(this.txtPriceA, 0);
                    stock.Silver.RateD = FormControls.GetDecimalValue(this.txtRateD, 1);
                    stock.Silver.PriceD = FormControls.GetDecimalValue(this.txtPriceD, 0);
                    stock.Silver.SalePrice = FormControls.GetDecimalValue(this.txtSilverSalePrice, 0);
                    stock.BQuantity = 0;
                    stock.BWeight = 0;
                    stock.BStatus = "Standard";
                }
            }
            else
            {
                if (this.txtTotalNetWeight.Text == "")
                {
                    MessageBox.Show("There is no Weight to Save", Messages.Header);
                    this.txtTotalNetWeight.Select();
                    return;
                }
                else
                {
                    if (chkBulk.Checked == true)
                    {
                        if (txtQty.Text == "")
                        {
                            MessageBox.Show("Please Enter Bulk Quantity or uncheck Bulk", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtQty.Focus();
                            return;
                        }
                        else
                        {
                            stock.NetWeight = 0;
                            stock.BQuantity = FormControls.GetIntValue(txtQty);
                            stock.BWeight = FormControls.GetDecimalValue(txtTotalNetWeight, 3);
                            stock.BStatus = "Bulk";
                        }
                    }
                    else
                    {
                        stock.BStatus = "Standard";
                        stock.BQuantity = 0;
                        stock.BWeight = 0;
                        stock.NetWeight = FormControls.GetDecimalValue(this.txtTotalNetWeight, 3);
                    }
                    frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtTotalNetWeight, 3));
                    stock.TTola = frm.Tola;
                    stock.TMasha = frm.Masha;
                    stock.TRatti = frm.Ratti;
                }
            }
            if (this.txtWasteInGm.Text == "")
            {
                stock.WTola = 0;
                stock.WMasha = 0;
                stock.WRatti = 0;
            }
            else
            {
                frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtWasteInGm, 3));
                stock.WTola = frm.Tola;
                stock.WMasha = frm.Masha;
                stock.WRatti = frm.Ratti;
            }
            stock.TotalWeight = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
            stock.WastePercent = FormControls.GetDecimalValue(this.txtWaste, 1);
            stock.WasteInGm = FormControls.GetDecimalValue(this.txtWasteInGm, 3);
            stock.PWeight = FormControls.GetDecimalValue(this.txtPureWeight, 3);
            if (this.txtPureWeight.Text == "")
            {
                stock.PTola = 0;
                stock.PMasha = 0;
                stock.PRatti = 0;
            }
            else
            {
                frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtPureWeight, 3));
                stock.PTola = frm.Tola;
                stock.PMasha = frm.Masha;
                stock.PRatti = frm.Ratti;
            }
            stock.KaatInRatti = FormControls.GetDecimalValue(this.txtRatti, 1);
            stock.LakerGm = FormControls.GetDecimalValue(this.txtLacquer, 1);
            stock.TotalLaker = FormControls.GetDecimalValue(this.txtTotalLacquer, 0);
            stock.MakingPerGm = FormControls.GetDecimalValue(this.txtMakingPerGm, 1);
            stock.TotalMaking = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            stock.MakingType = this.txtMakingType.Text.ToString();
            if (cbxDesignNo.SelectedIndex == -1)
            {
                Design ds = new Design();
                ds.DesignId = 0;
                stock.DesignNo = ds;
            }
            else
                stock.DesignNo = (Design)this.cbxDesignNo.SelectedItem;
            if (txtDesNo.Text == "")
                stock.DesNo = "";
            else
                stock.DesNo = txtDesNo.Text;
            stock.Karrat = (string)this.cbxKarat.SelectedItem;
            stock.Qty = FormControls.GetIntValue(this.txtQty);
            stock.Pieces = FormControls.GetIntValue(this.txtPieces);
            stock.ItemSize = this.txtSize.Text.ToString();
            stock.StockDate = Convert.ToDateTime(dtpDate.Value);
            stock.Description = this.txtDescription.Text.ToString();
            stock.PurchaseRate = FormControls.GetDecimalValue(this.txtPurchaseRate, 1);
            stock.ItemCost = FormControls.GetDecimalValue(this.txtItemCost, 0);
            stock.SalePrice = FormControls.GetDecimalValue(this.txtSalePrice, 0);
            if (cbxWorkerName.SelectedIndex == -1)
            {
                Worker wr = new Worker();
                wr.ID = 0;
                stock.WorkerName = wr;
            }
            else
                stock.WorkerName = (Worker)this.cbxWorkerName.SelectedItem;

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

            if (stock.WeightLineItem == null)
            {
                List<WeightLineItem> wl = new List<WeightLineItem>();
                wl = null;
                stock.WeightLineItem = wl;
            }
            else
                stkDAL.AddWtLineItem(stock);
            stock.StoneList = this.getAllStones();
            if (rbtOrderItem.Checked == true)
            {
                stkDAL.UpDateOrderEstimate(this.cbxOItemId.Text, stock);
            }
            stkDAL.AddStock(stock);
            if (chkBulk.Checked == true)
            {
                stkDAL.AddBulkStockTag(stock);
            }
            stkDAL.AddPics(jp);
            MessageBox.Show(Messages.Saved, Messages.Header);
            Item k = (Item)this.cbxGroupItem.SelectedItem;
            string tagNo = stkDAL.GenrateTagNo((int)k.ItemId, k.Abrivation);
            this.txtTagNo.Text = tagNo;
            this.RefreshRecord();
            this.rbtStockItem.Checked = true;
            this.rbtOrderItem.Checked = false;
            cbxOrderNo.DataSource = null;
            cbxOItemId.DataSource = null;
            this.cbxGroupItem.Focus();
            this.dtpDate.Value = DateTime.Today;
        }

        private void rbtSilver_Click(object sender, EventArgs e)
        {
            this.pnlGold.Visible = false;
            this.pnlSilver.Visible = true;
        }

        private void rbtGold_Click(object sender, EventArgs e)
        {
            this.pnlGold.Visible = true;
            this.pnlSilver.Visible = false;
        }

        private void rbtDiamond_Click(object sender, EventArgs e)
        {
            this.pnlGold.Visible = true;
            this.pnlSilver.Visible = false;
        }

        private void rbtOrderItem_Click(object sender, EventArgs e)
        {
            this.pnlOrderItem.Visible = true;
        }

        private void rbtStockItem_Click(object sender, EventArgs e)
        {
            this.pnlOrderItem.Visible = false;
            this.rbtStockItem.Checked = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "&Edit")
            {
                bflag = true;
                this.cbxGroupItem.SelectedIndexChanged += new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
                this.cbxWorkerName.SelectedIndexChanged += new System.EventHandler(this.cbxWorkerName_SelectedIndexChanged);
                this.txtPureWeight.TextChanged += new System.EventHandler(this.txtPureWeight_TextChanged);
                StockSearch sts = new StockSearch(this);
                FormControls.FadeOut(this);
                sts.ShowDialog();
                FormControls.FadeIn(this);
                return;
            }
            if (btnEdit.Text == "&Update")
            {
                stock = new Stock();
                jp = new JewelPictures();

                if (this.rbtGold.Checked == true)
                    stock.ItemType = ItemType.Gold;
                else if (this.rbtDiamond.Checked == true)
                    stock.ItemType = ItemType.Diamond;
                else if (this.rbtSilver.Checked == true)
                    stock.ItemType = ItemType.Silver;
                else if (this.rbtPladium.Checked == true)
                    stock.ItemType = ItemType.Pladium;
                else
                    stock.ItemType = ItemType.Platinum;
                if (this.rbtSilver.Checked == false)
                {
                    if (this.txtTotalWeight.Text == "")
                    {
                        MessageBox.Show("Must Enter Total Weight", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (this.rbtStockItem.Checked == true)
                    stock.ItemFor = ItemFor.Sale;
                if (this.rbtOrderItem.Checked == true)
                {
                    stock.ItemFor = ItemFor.Order;
                    stock.OrderNo = Convert.ToInt32(this.cbxOrderNo.Text);
                    stock.OItemId = this.cbxOItemId.Text;
                }
                stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;
                stock.TagNo = this.txtTagNo.Text.ToString();
                jp.TagNo = this.txtTagNo.Text.ToString();
                stock.BarCode = "*" + this.txtTagNo.Text + "*";

                stock.Silver = new Silver();
                stock.Silver.RateA = 0;
                stock.Silver.PriceA = 0;
                stock.Silver.RateD = 0;
                stock.Silver.PriceD = 0;
                stock.Silver.SalePrice = 0;
                if (this.rbtSilver.Checked == true)
                {
                    if (this.txtSilverNetWeight.Text == "")
                    {
                        MessageBox.Show("There is no Weight to Save", Messages.Header);
                        return;
                    }
                    else
                    {
                        stock.NetWeight = FormControls.GetDecimalValue(this.txtSilverNetWeight, 3);
                        stock.Silver = new Silver();
                        stock.Silver.RateA = FormControls.GetDecimalValue(this.txtRateA, 1);
                        stock.Silver.PriceA = FormControls.GetDecimalValue(this.txtPriceA, 0);
                        stock.Silver.RateD = FormControls.GetDecimalValue(this.txtRateD, 1);
                        stock.Silver.PriceD = FormControls.GetDecimalValue(this.txtPriceD, 0);
                        stock.Silver.SalePrice = FormControls.GetDecimalValue(this.txtSilverSalePrice, 0);
                    }
                }
                else
                {
                    if (this.txtTotalNetWeight.Text == "")
                    {
                        MessageBox.Show("Enter Total Net Weight", Messages.Header);
                        return;
                    }
                    else
                        stock.NetWeight = FormControls.GetDecimalValue(this.txtTotalNetWeight, 3);
                }
                if (this.txtTotalNetWeight.Text == "")
                {
                    stock.TTola = 0;
                    stock.TRatti = 0;
                    stock.TMasha = 0;
                }
                else
                    frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtTotalNetWeight, 3));
                stock.TTola = frm.Tola;
                stock.TMasha = frm.Masha;
                stock.TRatti = frm.Ratti;
                stock.TotalWeight = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
                if (this.txtWasteInGm.Text == "")
                {
                    stock.WTola = 0;
                    stock.WMasha = 0;
                    stock.WRatti = 0;
                }
                else
                    frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtWasteInGm, 3));
                stock.WTola = frm.Tola;
                stock.WMasha = frm.Masha;
                stock.WRatti = frm.Ratti;
                stock.WastePercent = FormControls.GetDecimalValue(this.txtWaste, 1);
                stock.WasteInGm = FormControls.GetDecimalValue(this.txtWasteInGm, 3);
                stock.PWeight = FormControls.GetDecimalValue(this.txtPureWeight, 3);
                if (this.txtPureWeight.Text == "")
                {
                    stock.PTola = 0;
                    stock.PMasha = 0;
                    stock.PRatti = 0;
                }
                else
                    frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtPureWeight, 3));
                stock.PTola = frm.Tola;
                stock.PMasha = frm.Masha;
                stock.PRatti = frm.Ratti;
                stock.KaatInRatti = FormControls.GetDecimalValue(this.txtRatti, 1);
                stock.LakerGm = FormControls.GetDecimalValue(this.txtLacquer, 1);
                stock.TotalLaker = FormControls.GetDecimalValue(this.txtTotalLacquer, 0);
                stock.MakingPerGm = FormControls.GetDecimalValue(this.txtMakingPerGm, 1);
                stock.TotalMaking = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
                stock.MakingType = this.txtMakingType.Text.ToString();
                if (cbxDesignNo.SelectedIndex == -1)
                {
                    Design ds = new Design();
                    ds.DesignId = 0;
                    stock.DesignNo = ds;
                }
                else
                    stock.DesignNo = (Design)this.cbxDesignNo.SelectedItem;
                if (txtDesNo.Text == "")
                    stock.DesNo = "";
                else
                    stock.DesNo = txtDesNo.Text;
                stock.Karrat = (string)this.cbxKarat.SelectedItem;
                if (this.txtQty.Text == "")
                    stock.Qty = 1;
                else
                    stock.Qty = Convert.ToInt32(this.txtQty.Text);
                stock.Pieces = FormControls.GetIntValue(this.txtPieces);
                stock.ItemSize = this.txtSize.Text.ToString();
                stock.StockDate = Convert.ToDateTime(dtpDate.Value);
                stock.Description = this.txtDescription.Text.ToString();
                stock.PurchaseRate = FormControls.GetDecimalValue(this.txtPurchaseRate, 1);
                stock.ItemCost = FormControls.GetDecimalValue(this.txtItemCost, 0);
                stock.SalePrice = FormControls.GetDecimalValue(this.txtSalePrice, 0);
                if (cbxWorkerName.SelectedIndex == -1)
                {
                    Worker wr = new Worker();
                    wr.ID = 0;
                    stock.WorkerName = wr;
                }
                else
                    stock.WorkerName = (Worker)this.cbxWorkerName.SelectedItem;
                stock.ImageMemory = null;
                stock.ImageMemoryThumb = null;
                if (pbxMain.Image != null)
                {
                    mst = new MemoryStream();
                    jp.ImageMemorySmall = jp.ConvertImageToBinary(jp.resizeImage(pbxMain.Image, pbxMain.Size));
                    jp.ImageMemory = jp.ConvertImageToBinary(this.pbxMain.Image);
                }
                else
                {
                    jp.ImageMemory = null;
                    jp.ImageMemorySmall = null;
                }
                if (pbx1.Image != null)
                {
                    mst = new MemoryStream();
                    jp.ImageMemory1 = jp.ConvertImageToBinary(this.pbx1.Image);
                }
                else
                    jp.ImageMemory1 = null;
                if (pbx2.Image != null)
                {
                    mst = new MemoryStream();
                    jp.ImageMemory2 = jp.ConvertImageToBinary(this.pbx2.Image);
                }
                else
                    jp.ImageMemory2 = null;
                if (pbx3.Image != null)
                {
                    mst = new MemoryStream();
                    jp.ImageMemory3 = jp.ConvertImageToBinary(this.pbx3.Image);
                }
                else
                    jp.ImageMemory3 = null;
                if (pbx4.Image != null)
                {
                    mst = new MemoryStream();
                    jp.ImageMemory4 = jp.ConvertImageToBinary(this.pbx4.Image);
                }
                else
                    jp.ImageMemory4 = null;
                if (pbx5.Image != null)
                {
                    mst = new MemoryStream();
                    jp.ImageMemory5 = jp.ConvertImageToBinary(this.pbx5.Image);
                }
                else
                    jp.ImageMemory5 = null;
                if (pbx6.Image != null)
                {
                    mst = new MemoryStream();
                    jp.ImageMemory6 = jp.ConvertImageToBinary(this.pbx6.Image);
                }
                else
                    jp.ImageMemory6 = null;
                if (pbx7.Image != null)
                {
                    mst = new MemoryStream();
                    jp.ImageMemory7 = jp.ConvertImageToBinary(this.pbx7.Image);
                }
                else
                    jp.ImageMemory7 = null;
                if (pbx8.Image != null)
                {
                    mst = new MemoryStream();
                    jp.ImageMemory8 = jp.ConvertImageToBinary(this.pbx7.Image);
                }
                else
                    jp.ImageMemory8 = null;

                if (stock.WeightLineItem == null)
                {
                    List<WeightLineItem> wl = new List<WeightLineItem>();
                    wl = null;
                    stock.WeightLineItem = wl;
                }
                else
                    stkDAL.AddWtLineItem(stock);

                sDAL.DeleteStonesByTagNo(this.txtTagNo.Text.ToString());
                stock.StoneList = this.getAllStones();
                string tno = this.txtTagNo.Text.ToString();
                stock.costFlag = CostFlag;
                stkDAL.UpdateStock(tno, stock);
                CostFlag = false;
                bool pFlag = false;
                pFlag = stkDAL.isTagNoExistInDbPics(tno);
                if (pFlag == true)
                    stkDAL.UpdatePics(tno, jp);
                else
                    stkDAL.AddPics(jp);
                MessageBox.Show(Messages.Updated, Messages.Header);
                this.RefreshRecord();
                this.rbtStockItem.Checked = true;
                this.rbtOrderItem.Checked = false;
                cbxOrderNo.DataSource = null;
                cbxOItemId.DataSource = null;
                this.dtpDate.Value = DateTime.Today;
                this.btnEdit.Text = "&Edit";
                this.btnDelete.Enabled = true;
                this.btnSave.Enabled = true;
                this.btnReset.Enabled = true;
                this.btnExit.Enabled = true;
                this.ShowDataGrid();
                Item k = (Item)this.cbxGroupItem.SelectedItem;
                string tagNo = stkDAL.GenrateTagNo((int)k.ItemId, k.Abrivation);
                this.txtTagNo.Text = tagNo;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (str == "Administrator")
            {
                this.btnSave.Enabled = true;
                this.btnEdit.Text = "&Edit";
                this.btnDelete.Enabled = true;
                this.RefreshRecord();
                if (cbxGroupItem.Text != "")
                {
                    Item k = (Item)this.cbxGroupItem.SelectedItem;
                    string tagNo = stkDAL.GenrateTagNo((int)k.ItemId, k.Abrivation);
                    this.txtTagNo.Text = tagNo;
                }
                else
                    return;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            this.FillPictureBox();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.pbxMain.Image == null)
            {
                MessageBox.Show("No Picture is selected to Remove", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                this.pbxMain.Image = null;
            this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
        }

        private void selectPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.FillPictureBox();
        }

        private void btnAddPic_Click(object sender, EventArgs e)
        {
            this.FillPictureBox();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.btnDelete.Text == "&Delete")
            {
                bflag = false;
                StockSearch sts = new StockSearch(this);
                sts.ShowDialog();
                if (this.txtTotalNetWeight.Text == "")
                    return;
                else
                {
                    this.btnDelete.Text = "&Confirm";
                    return;
                }
            }
            if (this.btnDelete.Text == "&Confirm")
            {
                if (MessageBox.Show("Are you sure to delete this record:Press Yes to continue", Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    stkDAL.DeleteStock(Convert.ToInt32(this.lblStockId.Text));
                    stkDAL.UpdateDeleteStockDescription("update stock set DelDescription='" + this.txtDelDescription.Text + "' where tagno='" + this.txtTagNo.Text + "'");
                    MessageBox.Show(Messages.Deleted, Messages.Header);
                    this.RefreshRecord();
                    this.btnEdit.Enabled = true;
                    this.btnSave.Enabled = true;
                    this.btnReset.Enabled = true;
                    this.btnDelete.Text = "&Delete";
                    Item k = (Item)this.cbxGroupItem.SelectedItem;
                    string tagNo = stkDAL.GenrateTagNo((int)k.ItemId, k.Abrivation);
                    this.txtTagNo.Text = tagNo;
                }
            }
        }

        private void removePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.pbxMain.Image == null)
            {
                MessageBox.Show("There is no item to remove", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                this.pbxMain.Image = null;
            this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
        }
        #endregion

        #region dgvEvents
        private void dgvStonesDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                try
                {
                    string txtvalue = Convert.ToString(dgvStonesDetail.Rows[e.RowIndex].Cells["Column4"].Value);
                    decimal val = Convert.ToDecimal(txtvalue);
                    string s = val.ToString("N3");
                    dgvStonesDetail.Rows[e.RowIndex].Cells["Column4"].Value = s.ToString();
                }
                catch { }
            }
            if (Convert.ToInt32(dgvStonesDetail.Rows[e.RowIndex].Cells[1].Value) == 3)
            {
                Column8.ReadOnly = false;
                Column9.ReadOnly = false;
                Column10.ReadOnly = false;
            }
            else
            {
                Column8.ReadOnly = true;
                Column9.ReadOnly = true;
                Column10.ReadOnly = true;
            }
            decimal val1, val2;
            if (this.txtTotalNetWeight.Text == "")
                val1 = 0;
            else
                val1 = Convert.ToDecimal(txtTotalNetWeight.Text);
            val2 = upDateTextBox();
            this.txtStoneWeight.Text = val2.ToString("0.000");
            this.grossweight(val1, val2, txtGrossWeight);
            this.txtStonePrice.Text = updateSum().ToString("0");
            this.dgvStonesDetail.EndEdit();
        }
        #endregion

        #region cbxChangeEvents
        private void cbxGroupItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxGroupItem.SelectedIndex == -1)
            {
                this.txtTagNo.Text = "";
                return;
            }
            else
            {
                this.btnManual.Enabled = true;
                Item k = (Item)this.cbxGroupItem.SelectedItem;
                this.cbxDesignNo.DataSource = desDAL.GetAllDesignByItemId((int)k.ItemId);
                this.cbxDesignNo.DisplayMember = "DesignNo";
                this.cbxDesignNo.ValueMember = "DesignId";
                this.cbxDesignNo.SelectedIndex = -1;
                string tagNo = stkDAL.GenrateTagNo((int)k.ItemId, k.Abrivation);
                this.txtTagNo.Text = tagNo;
            }
        }

        private void cbxGroupItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged += new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
        }

        private void cbxWorkerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Worker w = (Worker)this.cbxWorkerName.SelectedItem;
            if (w == null)
                return;
            else
            {
                this.txtWorkerId.Text = w.ID.ToString();
                this.txtMakingTola.Text = w.MakingTola.ToString("0.0");
            }
        }

        private void cbxWorkerName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxWorkerName.SelectedIndexChanged += new System.EventHandler(this.cbxWorkerName_SelectedIndexChanged);
        }
        #endregion

        #region textChanged
        private void txtPureWeight_TextChanged(object sender, EventArgs e)
        {
            frm.RatiMashaTola(FormControls.GetDecimalValue(this.txtPureWeight, 3), lblRtmPureWeight);
        }
        #endregion

        #region functions
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

        private List<Stones> getAllStones()
        {
            List<Stones> stDetail = null;
            int j = Convert.ToInt32(this.dgvStonesDetail.Rows.Count - 1);
            if (j == 0)
                return stDetail;
            else
            {
                if (stDetail == null)
                    stDetail = new List<Stones>();
                DataGridViewComboBoxColumn cbxStone = (DataGridViewComboBoxColumn)dgvStonesDetail.Columns["Column3"];
                for (int i = 0; i < dgvStonesDetail.Rows.Count - 1; i++)
                {
                    Stones std = new Stones();
                    string str = "";
                    std.StoneTypeId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[1].Value.ToString());
                    str = (string)dgvStonesDetail.Rows[i].Cells[2].Value.ToString();
                    if (string.IsNullOrEmpty(str))
                        std.StoneId = null;
                    else
                        std.StoneId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[2].Value.ToString());
                    if (string.IsNullOrEmpty(Convert.ToString(dgvStonesDetail.Rows[i].Cells["Column4"].Value)))
                        std.StoneWeight = null;
                    else
                        std.StoneWeight = Math.Round(Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells["Column4"].Value.ToString()), 3);
                    if ((string)dgvStonesDetail.Rows[i].Cells[4].FormattedValue == "")
                        std.Qty = null;
                    else
                        std.Qty = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[4].Value.ToString());
                    if ((string)dgvStonesDetail.Rows[i].Cells[5].FormattedValue == "")
                        std.Rate = null;
                    else
                        std.Rate = Math.Round(Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[5].Value.ToString()), 1);
                    if ((string)dgvStonesDetail.Rows[i].Cells[6].FormattedValue == "")
                        std.Price = null;
                    else
                        std.Price = Math.Round(Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[6].Value.ToString()), 0);
                    if ((string)dgvStonesDetail.Rows[i].Cells[7].FormattedValue == "")
                        std.ColorName = null;
                    else
                    {
                        std.ColorName = new StoneColor();
                        std.ColorName.ColorId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[7].Value.ToString());
                        for (int k = 0; k < Column8.Items.Count; k++)
                        {
                            StoneColor stc = (StoneColor)Column8.Items[k];
                            if (stc.ColorId == std.ColorName.ColorId)
                                std.ColorName.ColorName = stc.ColorName;
                        }
                    }
                    if ((string)dgvStonesDetail.Rows[i].Cells[8].FormattedValue == "")
                        std.CutName = null;
                    else
                    {
                        std.CutName = new StoneCut();
                        std.CutName.CutId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[8].Value.ToString());
                        for (int k = 0; k < Column9.Items.Count; k++)
                        {
                            StoneCut stc = (StoneCut)Column9.Items[k];
                            if (stc.CutId == std.CutName.CutId)
                                std.CutName.CutName = stc.CutName;
                        }
                    }
                    if ((string)dgvStonesDetail.Rows[i].Cells[9].FormattedValue == "")
                        std.ClearityName = null;
                    else
                    {
                        std.ClearityName = new StoneClearity();
                        std.ClearityName.ClearityId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[9].Value.ToString());
                        for (int k = 0; k < Column10.Items.Count; k++)
                        {
                            StoneClearity stc = (StoneClearity)Column10.Items[k];
                            if (stc.ClearityId == std.ClearityName.ClearityId)
                                std.ClearityName.ClearityName = stc.ClearityName;
                        }
                    }
                    stDetail.Add(std);
                }
            }
            return stDetail;
        }

        public decimal upDateTextBox()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvStonesDetail.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvStonesDetail.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) == string.Empty || dgvStonesDetail.Rows[counter].Cells[3].Value == null)
                        weight += 0;
                    else
                        weight += decimal.Parse(dgvStonesDetail.Rows[counter].Cells[3].Value.ToString());
                }
            }
            return weight;
        }

        public decimal updateSum()
        {
            decimal sum = 0, k;
            int counter;
            for (counter = 0; counter < (dgvStonesDetail.Rows.Count - 1); counter++)
            {
                if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) == "" || Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) == "0")
                {
                    if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[4].Value) != string.Empty && Convert.ToString(dgvStonesDetail.Rows[counter].Cells[5].Value) != string.Empty)
                    {
                        k = (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[4].Value.ToString())) * (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[5].Value.ToString()));
                        dgvStonesDetail.Rows[counter].Cells[6].Value = k.ToString();
                        sum += k;
                    }
                    else
                    {
                        if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[6].Value) != string.Empty)
                        {
                            k = decimal.Parse(dgvStonesDetail.Rows[counter].Cells[6].Value.ToString());
                            sum += k;
                        }
                    }
                }
                else
                {
                    if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) != string.Empty && Convert.ToString(dgvStonesDetail.Rows[counter].Cells[5].Value) != string.Empty)
                    {
                        k = (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[3].Value.ToString())) * (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[5].Value.ToString()));
                        dgvStonesDetail.Rows[counter].Cells[6].Value = k.ToString();
                        sum += k;
                    }
                    else
                    {
                        if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[6].Value) != string.Empty)
                        {
                            k = decimal.Parse(dgvStonesDetail.Rows[counter].Cells[6].Value.ToString());
                            sum += k;
                        }
                    }
                }
            }
            return sum;
        }

        private void ShowAllRecord(int stkId)
        {
            stock = new Stock();
            stock = stkDAL.GetStockBySockIdForStock(stkId);
            if (stock == null)
            {
                MessageBox.Show("This tag not exist for edit!", Messages.Header);
                editFlag = false;
                return;
            }
            else
            {
                editFlag = true;
                this.lblStockId.Text = stkId.ToString();
                if (stock.ItemType == ItemType.Gold)
                    rbtGold.Checked = true;
                else if (stock.ItemType == ItemType.Diamond)
                    rbtDiamond.Checked = true;
                else if (stock.ItemType == ItemType.Silver)
                    rbtSilver.Checked = true;
                else if (stock.ItemType == ItemType.Pladium)
                    rbtPladium.Checked = true;
                else
                    rbtPlatinum.Checked = true;
                for (int i = 0; i < this.cbxGroupItem.Items.Count; i++)
                {
                    Item it = (Item)this.cbxGroupItem.Items[i];
                    if (stock.ItemName.ItemId == it.ItemId)
                        this.cbxGroupItem.SelectedIndex = i;
                }
                this.txtDesNo.Text = stock.DesNo;
                this.dtpDate.Value = stock.StockDate;
                this.txtTagNo.Text = stock.TagNo.ToString();
                if (this.rbtSilver.Checked)
                {
                    this.pnlGold.Visible = false;
                    this.pnlSilver.Visible = true;
                    this.txtSilverNetWeight.Text = Convert.ToDecimal(stock.NetWeight).ToString("0.000");
                    this.txtRateA.Text = Convert.ToDecimal(stock.Silver.RateA).ToString("0.0");
                    this.txtPriceA.Text = Convert.ToDecimal(stock.Silver.PriceA).ToString("0");
                    this.txtRateD.Text = Convert.ToDecimal(stock.Silver.RateD).ToString("0.0");
                    this.txtPriceD.Text = Convert.ToDecimal(stock.Silver.PriceD).ToString("0");
                    this.txtSilverSalePrice.Text = Convert.ToDecimal(stock.Silver.SalePrice).ToString("0");
                }
                else
                {
                    this.pnlGold.Visible = true;
                    this.pnlSilver.Visible = false;
                }
                if (stock.Qty == 0)
                    this.txtQty.Text = "";
                else
                    this.txtQty.Text = stock.Qty.ToString();

                if (stock.Pieces == 0)
                    this.txtPieces.Text = "";
                else
                    this.txtPieces.Text = stock.Pieces.ToString();

                this.txtSize.Text = stock.ItemSize.ToString();

                if (stock.KaatInRatti == 0)
                    this.txtRatti.Text = "";
                else
                    this.txtRatti.Text = stock.KaatInRatti.ToString();

                for (int i = 0; i < this.cbxKarat.Items.Count; i++)
                {
                    string str = (string)this.cbxKarat.Items[i];
                    if (stock.Karrat.Equals(str))
                        cbxKarat.SelectedIndex = i;
                }


                int m = (int)this.cbxGroupItem.SelectedValue;
                this.cbxDesignNo.DataSource = desDAL.GetAllDesignByItemId(m);
                this.cbxDesignNo.DisplayMember = "DesignNo";
                this.cbxDesignNo.ValueMember = "DesignId";
                for (int i = 0; i < this.cbxDesignNo.Items.Count; i++)
                {
                    Design d = (Design)this.cbxDesignNo.Items[i];
                    if (stock.DesignNo.DesignId == d.DesignId)
                    {
                        this.cbxDesignNo.SelectedIndex = i;
                        break;
                    }
                    else
                        this.cbxDesignNo.SelectedIndex = -1;
                }
                this.cbxWorkerName.DataSource = wrkDAL.GetAllWorkers();
                this.cbxWorkerName.DisplayMember = "Name";
                this.cbxWorkerName.ValueMember = "ID";

                for (int i = 0; i < this.cbxWorkerName.Items.Count; i++)
                {
                    Worker wrk = (Worker)this.cbxWorkerName.Items[i];
                    if (stock.WorkerName.ID == wrk.ID)
                    {
                        this.cbxWorkerName.SelectedIndex = i;
                        this.txtWorkerId.Text = wrk.ID.ToString();

                        this.txtMakingTola.Text = wrk.MakingTola.ToString();
                        break;
                    }
                    else
                        this.cbxWorkerName.SelectedIndex = -1;
                }
                this.txtDescription.Text = stock.Description.ToString();
                if (stock.NetWeight == 0)
                    this.txtTotalNetWeight.Text = "";
                else
                    this.txtTotalNetWeight.Text = Convert.ToDecimal(stock.NetWeight).ToString("0.000");
                if (stock.WastePercent == 0)
                    this.txtWaste.Text = "";
                else
                    this.txtWaste.Text = Convert.ToDecimal(stock.WastePercent).ToString("0.0");

                if (stock.WasteInGm == 0)
                    this.txtWasteInGm.Text = "";
                else
                    this.txtWasteInGm.Text = Convert.ToDecimal(stock.WasteInGm).ToString("0.000");

                if (stock.MakingPerGm == 0)
                    this.txtMakingPerGm.Text = "";
                else
                    this.txtMakingPerGm.Text = Convert.ToDecimal(stock.MakingPerGm).ToString("0.0");

                if (stock.TotalWeight == 0)
                    this.txtTotalWeight.Text = "";
                else
                    this.txtTotalWeight.Text = Convert.ToDecimal(stock.TotalWeight).ToString("0.000");

                if (stock.PWeight.HasValue)
                    this.txtPureWeight.Text = Convert.ToDecimal(stock.PWeight).ToString("0.000");
                else
                    this.txtPureWeight.Text = "";
                if (stock.TotalMaking == 0)
                    this.txtTotalMaking.Text = "";
                else
                    this.txtTotalMaking.Text = Convert.ToDecimal(stock.TotalMaking).ToString("0");
                this.txtMakingType.Text = Convert.ToString(stock.MakingType);
                if (stock.LakerGm == 0)
                    this.txtLacquer.Text = "";
                else
                    this.txtLacquer.Text = Convert.ToDecimal(stock.LakerGm).ToString("0.0");
                if (stock.TotalLaker == 0)
                    this.txtTotalLacquer.Text = "";
                else
                    this.txtTotalLacquer.Text = Convert.ToDecimal(stock.TotalLaker).ToString("0");
                if (stock.ItemCost.HasValue)
                    this.txtItemCost.Text = Convert.ToDecimal(stock.ItemCost).ToString("0");
                else
                    this.txtItemCost.Text = "";

                if (stock.SalePrice.HasValue)
                    this.txtSalePrice.Text = Convert.ToDecimal(stock.SalePrice).ToString("0");
                else
                    this.txtSalePrice.Text = "";

                if (stock.PurchaseRate.HasValue)
                    this.txtPurchaseRate.Text = Convert.ToDecimal(stock.PurchaseRate).ToString("0.0");
                else
                    this.txtPurchaseRate.Text = "";

                if (stock.ImageMemory == null)
                {
                    this.pbxMain.Image = null;
                    this.pbxMain.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(stock.ImageMemory);
                    Image img = Image.FromStream(mst);
                    Bitmap bitmap = new Bitmap(img);
                    this.pbxMain.Image = bitmap;
                }
                this.ShowPictures(stock.TagNo);
                if (stock.StoneList == null)
                    return;
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
                            this.dgvStonesDetail.Rows[i].Cells[3].Value = Convert.ToDecimal(stock.StoneList[i].StoneWeight).ToString("0.000");
                        else
                            this.dgvStonesDetail.Rows[i].Cells[3].Value = string.Empty;
                        if (stock.StoneList[i].Qty.HasValue)
                            this.dgvStonesDetail.Rows[i].Cells[4].Value = Convert.ToInt32(stock.StoneList[i].Qty);
                        else
                            this.dgvStonesDetail.Rows[i].Cells[4].Value = string.Empty;
                        if (stock.StoneList[i].Rate.HasValue)
                            this.dgvStonesDetail.Rows[i].Cells[5].Value = Convert.ToDecimal(stock.StoneList[i].Rate).ToString("0.0");
                        else
                            this.dgvStonesDetail.Rows[i].Cells[5].Value = string.Empty;
                        if (stock.StoneList[i].Price.HasValue)
                            this.dgvStonesDetail.Rows[i].Cells[6].Value = Convert.ToDecimal(stock.StoneList[i].Price).ToString("0");
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
                this.txtStoneWeight.Text = this.upDateTextBox().ToString("0.000");
                this.txtStonePrice.Text = this.updateSum().ToString("0");
                this.txtGrossWeight.Text = ((decimal)stock.NetWeight + this.upDateTextBox()).ToString("0.000");
                this.cbxGroupItem.SelectedIndexChanged += new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            }
        }

        public void ShowSerach(int id, int itmid)
        {
            this.cbxGroupItem.SelectedIndexChanged -= new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            cbxGroupItem.DataSource = itmDAL.GetAllItems();
            cbxGroupItem.DisplayMember = "ItemName";
            cbxGroupItem.ValueMember = "ItemId";
            for (int i = 0; i < cbxGroupItem.Items.Count; i++)
            {
                Item d = (Item)cbxGroupItem.Items[i];
                if (itmid == d.ItemId)
                    cbxGroupItem.SelectedIndex = i;
            }
            if (id == 0)
            {
                this.btnSave.Enabled = true;
                this.btnEdit.Text = "&Edit";
                this.btnDelete.Enabled = true;
                this.btnReset.Enabled = true;
            }
            else
            {
                editFlag = false;
                this.ShowAllRecord(id);
                if (editFlag == true)
                {
                    if (bflag == true)
                    {
                        if (this.btnEdit.Text == "&Edit")
                        {
                            btnDelete.Text = "&Delete";
                            this.btnEdit.Text = "&Update";
                            this.btnEdit.Enabled = true;
                            this.btnDelete.Enabled = false;
                        }
                        else
                        {
                            this.btnDelete.Text = "&Confirm";
                            this.btnEdit.Text = "&Edit";
                            this.btnEdit.Enabled = true;
                        }
                        this.btnSave.Enabled = false;
                        this.btnReset.Enabled = true;
                    }
                    else
                    {
                        if (this.btnDelete.Text == "&Delete")
                        {
                            btnEdit.Text = "&Edit";
                            this.btnDelete.Text = "&Confirm";
                            this.btnEdit.Enabled = false;
                            this.btnDelete.Enabled = true;
                        }
                        else
                        {
                            this.btnEdit.Text = "&Update";
                            this.btnDelete.Text = "&Delete";
                            this.btnDelete.Enabled = false;
                        }
                        this.btnSave.Enabled = false;
                        this.btnReset.Enabled = false;
                    }
                }
            }
        }

        private void RefreshRecord()
        {
            this.cbxDesignNo.SelectedIndex = -1;
            this.txtDesNo.Text = "";
            this.txtQty.Text = "1";
            this.txtPieces.Text = "";
            this.txtSize.Text = "";
            this.txtMakingTola.Text = "";
            this.txtDescription.Text = "";
            this.txtTotalNetWeight.Text = "";
            this.txtWaste.Text = "";
            this.txtWasteInGm.Text = "";
            this.txtTotalWeight.Text = "";
            this.txtRatti.Text = "";
            this.txtPureWeight.Text = "";
            this.txtMakingPerGm.Text = "";
            this.txtTotalMaking.Text = "";
            this.txtMakingType.Text = "";
            this.txtLacquer.Text = "";
            this.txtTotalLacquer.Text = "";
            this.txtItemCost.Text = "";
            this.txtSalePrice.Text = "";
            this.txtGrossWeight.Text = "";
            this.lblRtmPureWeight.Text = "";
            this.lblRtmTotalNetWeight.Text = "";
            this.lblRtmTotalWeight.Text = "";
            this.lblRtmWasteinGm.Text = "";
            this.txtStonePrice.Text = "";
            this.txtStoneWeight.Text = "";
            this.pbxMain.Image = null;
            this.pbx1.Image = null;
            this.pbx2.Image = null;
            this.pbx3.Image = null;
            this.pbx4.Image = null;
            this.pbx5.Image = null;
            this.pbx6.Image = null;
            this.pbx7.Image = null;
            this.pbx8.Image = null;
            this.dgvStonesDetail.Rows.Clear();
            this.txtRateA.Text = "";
            this.txtRateD.Text = "";
            this.txtPriceA.Text = "";
            this.txtPriceD.Text = "";
            this.txtSilverNetWeight.Text = "";
            this.txtSilverSalePrice.Text = "";
            this.txtPurchaseRate.Text = "";
            CloseVideoSource();
        }

        private void SearchComOrderByOItemId(string oItmId)
        {
            olitm = oDAL.GetComRecByOItemId(oItmId);
            if (olitm == null)
                return;
            else
            {
                if (olitm.Stock.ItemType == ItemType.Gold)
                    rbtGold.Checked = true;
                else if (olitm.Stock.ItemType == ItemType.Diamond)
                    rbtDiamond.Checked = true;
                else if (olitm.Stock.ItemType == ItemType.Silver)
                    rbtSilver.Checked = true;
                else if (olitm.Stock.ItemType == ItemType.Pladium)
                    rbtPladium.Checked = true;
                else
                    rbtPlatinum.Checked = true;
                if (this.rbtSilver.Checked)
                {
                    this.pnlGold.Visible = false;
                    this.pnlSilver.Visible = true;
                    this.txtSilverNetWeight.Text = Convert.ToDecimal(olitm.Stock.NetWeight).ToString("0.000");
                    this.txtRateD.Text = ((decimal)olitm.Stock.RatePerGm).ToString("0.0");
                    this.txtPriceD.Text = ((decimal)(olitm.Stock.RatePerGm * olitm.Stock.NetWeight)).ToString("0");
                    this.txtSilverSalePrice.Text = this.txtPriceD.Text;
                }
                else
                {
                    this.pnlGold.Visible = true;
                    this.pnlSilver.Visible = false;
                }

                for (int i = 0; i < this.cbxGroupItem.Items.Count; i++)
                {
                    Item it = (Item)this.cbxGroupItem.Items[i];
                    if (olitm.Stock.ItemName.ItemId == it.ItemId)
                        this.cbxGroupItem.SelectedIndex = i;
                }
                Item k = (Item)this.cbxGroupItem.SelectedItem;

                string tagNo = stkDAL.GenrateTagNo((int)k.ItemId, k.Abrivation);
                this.txtTagNo.Text = tagNo;

                if (olitm.Stock.Qty == 0)
                    this.txtQty.Text = "";
                else
                    this.txtQty.Text = olitm.Stock.Qty.ToString();
                this.dtpDate.Value = Convert.ToDateTime(olitm.Stock.StockDate);
                this.txtSize.Text = olitm.Stock.ItemSize.ToString();
                if (olitm.Stock.TotalWeight == 0)
                    this.txtTotalWeight.Text = "";
                else
                    this.txtTotalWeight.Text = olitm.Stock.TotalWeight.ToString("0.000");
                for (int i = 0; i < this.cbxKarat.Items.Count; i++)
                {
                    string str = (string)this.cbxKarat.Items[i];
                    if (olitm.Stock.Karrat.Equals(str))
                        cbxKarat.SelectedIndex = i;
                }
                FormControls.FillCombobox(cbxDesignNo, desDAL.GetAllDesign(), "DesignNo", "DesignId");
                this.cbxWorkerName.DataSource = wrkDAL.GetAllWorkers();
                this.cbxWorkerName.DisplayMember = "Name";
                this.cbxWorkerName.ValueMember = "ID";
                for (int i = 0; i < this.cbxWorkerName.Items.Count; i++)
                {
                    Worker wrk = (Worker)this.cbxWorkerName.Items[i];
                    if (olitm.Stock.WorkerName.ID == wrk.ID)
                    {
                        this.cbxWorkerName.SelectedIndex = i;

                        this.txtWorkerId.Text = wrk.ID.ToString();
                        this.txtMakingTola.Text = wrk.MakingTola.ToString("0.0");
                        break;
                    }
                    else
                        this.cbxWorkerName.SelectedIndex = -1;
                }
                this.txtDescription.Text = olitm.Stock.Description.ToString();
                if (olitm.Stock.NetWeight == 0)
                    this.txtTotalNetWeight.Text = "";
                else
                    this.txtTotalNetWeight.Text = Convert.ToDecimal(olitm.Stock.NetWeight).ToString("0.000");
                if (olitm.Stock.WastePercent == 0)
                    this.txtWaste.Text = "";
                else
                    this.txtWaste.Text = Convert.ToDecimal(olitm.Stock.WastePercent).ToString("0.0");

                if (olitm.Stock.WasteInGm == 0)
                    this.txtWasteInGm.Text = "";
                else
                    this.txtWasteInGm.Text = Convert.ToDecimal(olitm.Stock.WasteInGm).ToString("0.000");

                if (olitm.Stock.MakingPerGm == 0)
                    this.txtMakingPerGm.Text = "";
                else
                    this.txtMakingPerGm.Text = Convert.ToDecimal(olitm.Stock.MakingPerGm).ToString("0.0");

                if (olitm.Stock.TotalMaking == 0)
                    this.txtTotalMaking.Text = "";
                else
                    this.txtTotalMaking.Text = Convert.ToDecimal(olitm.Stock.TotalMaking).ToString("0");
                if (olitm.Stock.LakerGm == 0)
                    this.txtLacquer.Text = "";
                else
                    this.txtLacquer.Text = Convert.ToDecimal(olitm.Stock.LakerGm).ToString("0");

                if (olitm.Stock.TotalLaker == 0)
                    this.txtTotalLacquer.Text = "";
                else
                    this.txtTotalLacquer.Text = Convert.ToDecimal(olitm.Stock.TotalLaker).ToString("0");

                frm.GramsOfPercentStock(FormControls.GetDecimalValue(this.txtWaste, 1), FormControls.GetDecimalValue(this.txtTotalNetWeight, 3), txtWasteInGm, txtTotalWeight);
                frm.KaatInRatti(FormControls.GetDecimalValue(this.txtRatti, 1), FormControls.GetDecimalValue(this.txtTotalNetWeight, 3), txtPureWeight, lblRtmPureWeight);

                if (olitm.Stock.ImageMemory == null)
                {
                    this.pbxMain.Image = null;
                    this.pbxMain.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(olitm.Stock.ImageMemory);
                    this.pbxMain.Image = Image.FromStream(mst);
                }
                if (olitm.Stock.StoneList == null || olitm.Stock.StoneList.Count == 0)
                    return;
                else
                {
                    this.dgvStonesDetail.AutoGenerateColumns = false;
                    int count = olitm.Stock.StoneList.Count;
                    this.dgvStonesDetail.Rows.Add(count);
                    for (int i = 0; i < olitm.Stock.StoneList.Count; i++)
                    {
                        this.dgvStonesDetail.Rows[i].Cells[1].Value = olitm.Stock.StoneList[i].StoneTypeId;

                        this.Column3.DataSource = sDAL.GetAllStoneNamebyId(Convert.ToInt32(olitm.Stock.StoneList[i].StoneTypeId));
                        this.Column3.DisplayMember = "Name";
                        this.Column3.ValueMember = "Id";

                        this.dgvStonesDetail.Rows[i].Cells[2].Value = olitm.Stock.StoneList[i].StoneId;
                        if (olitm.Stock.StoneList[i].StoneWeight.HasValue)
                        {
                            decimal w;
                            this.dgvStonesDetail.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(olitm.Stock.StoneList[i].StoneWeight), 3);
                            w = Convert.ToDecimal(olitm.Stock.StoneList[i].StoneWeight) + Convert.ToDecimal(olitm.Stock.NetWeight);
                            this.txtGrossWeight.Text = w.ToString("0.000");
                            this.txtStoneWeight.Text = (Convert.ToDecimal(olitm.Stock.StoneList[i].StoneWeight)).ToString("0.000");
                        }
                        else
                            this.dgvStonesDetail.Rows[i].Cells[3].Value = string.Empty;
                        if (olitm.Stock.StoneList[i].Qty.HasValue)
                            this.dgvStonesDetail.Rows[i].Cells[4].Value = Convert.ToInt32(olitm.Stock.StoneList[i].Qty);
                        else
                            this.dgvStonesDetail.Rows[i].Cells[4].Value = string.Empty;
                        if (olitm.Stock.StoneList[i].Rate.HasValue)
                            this.dgvStonesDetail.Rows[i].Cells[5].Value = Convert.ToDecimal(olitm.Stock.StoneList[i].Rate).ToString("0.0");
                        else
                            this.dgvStonesDetail.Rows[i].Cells[5].Value = string.Empty;
                        if (olitm.Stock.StoneList[i].Price.HasValue)
                        {
                            this.dgvStonesDetail.Rows[i].Cells[6].Value = Convert.ToDecimal(olitm.Stock.StoneList[i].Price).ToString("0");
                            this.txtStonePrice.Text = (Convert.ToDecimal(olitm.Stock.StoneList[i].Price)).ToString("0");
                        }
                        else
                            this.dgvStonesDetail.Rows[i].Cells[6].Value = string.Empty;
                        if (!(string.IsNullOrEmpty(olitm.Stock.StoneList[i].ColorName.ColorName.ToString())))
                        {
                            for (int j = 0; j < this.Column8.Items.Count; j++)
                            {
                                StoneColor stc = (StoneColor)this.Column8.Items[j];
                                if (olitm.Stock.StoneList[i].ColorName.ColorName.Equals(stc.ColorName.ToString()))
                                    this.dgvStonesDetail.Rows[i].Cells[7].Value = Convert.ToInt32(stc.ColorId);
                            }
                        }
                        if (!(string.IsNullOrEmpty(olitm.Stock.StoneList[i].CutName.CutName)))
                        {
                            for (int j = 0; j < this.Column9.Items.Count; j++)
                            {
                                StoneCut stc = (StoneCut)this.Column9.Items[j];
                                if (olitm.Stock.StoneList[i].CutName.CutName.Equals(stc.CutName.ToString()))
                                    this.dgvStonesDetail.Rows[i].Cells[8].Value = Convert.ToInt32(stc.CutId);
                            }
                        }
                        if (!(string.IsNullOrEmpty(olitm.Stock.StoneList[i].ClearityName.ClearityName.ToString())))
                        {
                            for (int j = 0; j < this.Column10.Items.Count; j++)
                            {
                                StoneClearity stc = (StoneClearity)this.Column10.Items[j];
                                if (olitm.Stock.StoneList[i].ClearityName.ClearityName.Equals(stc.ClearityName.ToString()))
                                    this.dgvStonesDetail.Rows[i].Cells[9].Value = Convert.ToInt32(stc.ClearityId);
                            }
                        }
                    }
                }
            }
        }

        private void grossweight(decimal val1, decimal val2, TextBox textTotal)
        {
            textTotal.Text = (val1 - val2).ToString("0.000");
        }

        private void ShowPictures(string tagNo)
        {
            jp = new JewelPictures();

            jp = pDAL.GetPictures(tagNo);
            if (jp == null)
                return;
            else
            {
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

        private void FillPictureBox()
        {
            if (this.pbxMain.Image != null)
            {
                if (this.pbx1.Image == null)
                {
                    this.pbx1.Image = bitmap;
                    this.pbxMain.Image = null;
                }
                else if (this.pbx2.Image == null)
                {
                    this.pbx2.Image = bitmap;
                    this.pbxMain.Image = null;
                }
                else if (this.pbx3.Image == null)
                {
                    this.pbx3.Image = bitmap;
                    this.pbxMain.Image = null;
                }
                else if (this.pbx4.Image == null)
                {
                    this.pbx4.Image = bitmap;
                    this.pbxMain.Image = null;
                }
                else if (this.pbx5.Image == null)
                {
                    this.pbx5.Image = bitmap;
                    this.pbxMain.Image = null;
                }
                else if (this.pbx6.Image == null)
                {
                    this.pbx6.Image = bitmap;
                    this.pbxMain.Image = null;
                }
                else if (this.pbx7.Image == null)
                {
                    this.pbx7.Image = bitmap;
                    this.pbxMain.Image = null;
                }
                else if (this.pbx8.Image == null)
                {
                    this.pbx8.Image = bitmap;
                    this.pbxMain.Image = null;
                }
                else
                {
                    MessageBox.Show("No more space available for picture", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }
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

        private bool KeyCheck(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            if (e.KeyChar == 13 || e.KeyChar == 27)
                bFlag = true;
            return bFlag;
        }
        #endregion

        #region Pbx Click
        private void pbx1_Click(object sender, EventArgs e)
        {
            if (pbxMain != null)
            {
                Image tempimg;
                tempimg = this.pbxMain.Image;
                this.pbxMain.Image = null;
                this.pbxMain.Image = this.pbx1.Image;
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
        #endregion

        private void ManageStock_Load(object sender, EventArgs e)
        {
            UserRights ur = new UserRights();
            str = ur.GetRightsByUser();
            if (str == "Administrator")
            {
                this.btnSave.Enabled = true;
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
            }
            else if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            else
            {
                str = ur.GetUserRightsByUser("AddStock");
                if (str != "" && str != null)
                {
                    
                }
            }
            this.cbxSupplier.SelectedIndexChanged -= new EventHandler(cbxSupplier_SelectedIndexChanged);
            this.cbxGroupItem.SelectedIndexChanged -= new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            this.cbxWorkerName.SelectedIndexChanged -= new System.EventHandler(this.cbxWorkerName_SelectedIndexChanged);
            this.txtPureWeight.TextChanged -= new System.EventHandler(this.txtPureWeight_TextChanged);
            this.cbxOrderNo.SelectedIndexChanged -= new EventHandler(cbxOrderNo_SelectedIndexChanged);
            this.pnlSilver.Visible = false;
            this.pnlOrderItem.Visible = false;
            FormControls.FillCombobox(cbxSupplier, new SupplierDAL().GetAllSuppliers(), "PAbri", "PCode");
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
            FormControls.FillCombobox(cbxWorkerName, wrkDAL.GetAllWorkers(), "Name", "ID");
            this.ShowDataGrid();
            this.cbxKarat.SelectedIndex = 3;
            if (this.txtStoneWeight.Text == "")
                this.txtStoneWeight.Text = "0";
            if (this.txtStonePrice.Text == "")
                this.txtStonePrice.Text = "0";
            getCamList();
            this.cbxGroupItem.Select();

            str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnReports.Enabled = false;
            }
        }

        private void getCamList()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                comboBox1.Items.Clear();
                if (videoDevices.Count == 0)
                    throw new ApplicationException();
                DeviceExist = true;
                foreach (FilterInfo device in videoDevices)
                {
                    comboBox1.Items.Add(device.Name);
                }
                comboBox1.SelectedIndex = 0;
            }
            catch (ApplicationException)
            {
                DeviceExist = false;
                comboBox1.Items.Add("No capture device on your system");
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            pbxMain.Image = img;
        }

        private void CloseVideoSource()
        {
            if (!(videoSource == null))
            {
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
            }
        }

        private void txtTotalNetWeight_KeyDown(object sender, KeyEventArgs e)
        {
            frm.LabelZero(lblRtmTotalNetWeight, lblRtmPureWeight, lblRtmTotalWeight, lblRtmWasteinGm);
        }

        private void dgvStonesDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 2 && this.dgvStonesDetail.CurrentRow.Cells[1].Value != null)
            {
                int sty = (int)this.dgvStonesDetail.CurrentRow.Cells[1].Value;
                ComboBox cmb = e.Control as ComboBox;
                cmb.DataSource = sDAL.GetAllStoneNamebyId(sty);
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "Id";
            }
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 3)
            {
                TextBox txtbox = e.Control as TextBox;
                if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 3)
                    txtbox.KeyPress += new KeyPressEventHandler(txtbox_KeyPress);
                if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 7)
                    txtbox.KeyPress += new KeyPressEventHandler(txtbox_KeyPress);
            }
        }

        void txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 4 || this.dgvStonesDetail.CurrentCell.ColumnIndex == 5 || this.dgvStonesDetail.CurrentCell.ColumnIndex == 7)
            {
                if (e.KeyChar == 46)
                    e.Handled = true;
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    e.Handled = true;
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                    e.Handled = true;
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 3 || this.dgvStonesDetail.CurrentCell.ColumnIndex == 6 || this.dgvStonesDetail.CurrentCell.ColumnIndex == 7)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    e.Handled = true;
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                    e.Handled = true;
            }
        }

        private void rbtOrderItem_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtOrderItem.Checked == true)
            {
                this.pnlOrderItem.Visible = true;
                this.cbxOrderNo.DataSource = oDAL.GetAllOrderNo("select Distinct[OrderNo] from OrderEstimate where Status='Estimated' order by OrderNo");
                this.cbxOrderNo.DisplayMember = "OrderNo";
                this.cbxOrderNo.ValueMember = "OrderNo";
                this.cbxOrderNo.SelectedIndex = -1;
            }
            else
                this.pnlOrderItem.Visible = false;
        }

        private void cbxOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxOrderNo.SelectedIndexChanged += new EventHandler(cbxOrderNo_SelectedIndexChanged);
            if (this.cbxOrderNo.SelectedValue == null)
                return;
            else
            {
                int k = (int)this.cbxOrderNo.SelectedValue;
                this.cbxOItemId.DataSource = oDAL.GetAllOItemIdByOrderNo(k);
                this.cbxOItemId.DisplayMember = "OItemId";
            }
        }

        private void cbxOItemId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.RefreshRecord();
                this.SearchComOrderByOItemId(this.cbxOItemId.Text);
            }
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            if (this.cbxGroupItem.Text != string.Empty)
            {
                if (MessageBox.Show("Are Your Sure To Enter Manual Tag No.  ", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Item itm = (Item)this.cbxGroupItem.SelectedItem;
                    this.txtTagNo.Text = itm.Abrivation.ToString();
                    string st = this.txtTagNo.Text;
                    t = st.Length;
                    this.txtTagNo.Focus();
                    txtTagNo.SelectionStart = st.Length;
                    this.txtTagNo.ReadOnly = false;
                    this.btnManual.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please Select Item", Messages.Header);
                return;
            }
        }

        private void txtTotalNetWeight_Leave(object sender, EventArgs e)
        {
            decimal val = 0;
            if (this.txtTotalNetWeight.Text != "")
            {
                val = Convert.ToDecimal(this.txtTotalNetWeight.Text);
                this.txtTotalNetWeight.Text = val.ToString("N3");
            }
        }

        private void txtTagNo_Leave(object sender, EventArgs e)
        {
            itm = (Item)this.cbxGroupItem.SelectedItem;
            if (itm.Abrivation.Length + 4 == this.txtTagNo.Text.Length)
            {
                CultureInfo culInfo = CultureInfo.CurrentCulture;
                TextInfo txtInfo = culInfo.TextInfo;
                string str1 = this.txtTagNo.Text.ToString();
                this.txtTagNo.Text = txtInfo.ToTitleCase(str1);
            }
            else
            {
                MessageBox.Show("TagNo is Not Correct", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtTagNo.Focus();
            }
            if (stkDAL.isManualTagNoExist(this.txtTagNo.Text) == true)
            {
                MessageBox.Show("TagNo is Already Exist", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtTagNo.Focus();
                this.txtTagNo.Text = itm.Abrivation.ToString();
                this.txtTagNo.SelectionStart = itm.Abrivation.Length;
            }

        }

        private void dgvStonesDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 1)
                {
                    DataGridViewComboBoxCell cbx = (DataGridViewComboBoxCell)dgvStonesDetail.Rows[e.RowIndex].Cells[2];
                    cbx.DataSource = null;
                }
                if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 2)
                {
                    if (Convert.ToString(dgvStonesDetail.Rows[e.RowIndex].Cells[1].Value) == "")
                        MessageBox.Show("First Select Type", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {

                        int sty = (int)dgvStonesDetail.Rows[e.RowIndex].Cells[1].Value;
                        DataGridViewComboBoxCell cbx = (DataGridViewComboBoxCell)dgvStonesDetail.Rows[e.RowIndex].Cells[2];
                        cbx.DataSource = sDAL.GetAllStoneNamebyId(sty);
                        cbx.DisplayMember = "Name";
                        cbx.ValueMember = "Id";
                    }
                }
            }
        }

        private void txtMakingPerGm_Enter(object sender, EventArgs e)
        {
            if (txtTotalWeight.Text == "")
            {
                this.txtTotalNetWeight.Leave -= new EventHandler(txtTotalNetWeight_Leave);
                MessageBox.Show("Enter Weight First ", Messages.Header);
                this.txtTotalNetWeight.Focus();
                return;
            }
        }

        private void txtRatti_Enter(object sender, EventArgs e)
        {
            if (txtTotalNetWeight.Text == "")
            {
                this.txtTotalNetWeight.Leave -= new EventHandler(txtTotalNetWeight_Leave);
                MessageBox.Show("Enter Weight First ", Messages.Header);
                this.txtTotalNetWeight.Focus();
                return;
            }
        }

        private void txtWaste_Enter(object sender, EventArgs e)
        {
            if (txtTotalNetWeight.Text == "")
            {
                this.txtTotalNetWeight.Leave -= new EventHandler(txtTotalNetWeight_Leave);
                MessageBox.Show("Enter Weight First ", Messages.Header);
                this.txtTotalNetWeight.Focus();
                return;
            }
        }

        private void txtWasteInGm_Enter(object sender, EventArgs e)
        {
            if (txtTotalNetWeight.Text == "")
            {
                this.txtTotalNetWeight.Leave -= new EventHandler(txtTotalNetWeight_Leave);
                MessageBox.Show("Enter Weight First ", Messages.Header);
                this.txtTotalNetWeight.Focus();
                return;
            }
        }

        private void txtTotalMaking_Enter(object sender, EventArgs e)
        {
            if (txtTotalWeight.Text == "")
            {
                this.txtTotalNetWeight.Leave -= new EventHandler(txtTotalNetWeight_Leave);
                MessageBox.Show("Enter Weight First ", Messages.Header);
                this.txtTotalNetWeight.Focus();
                return;
            }
        }

        private void txtLacquer_Enter(object sender, EventArgs e)
        {
            if (txtTotalWeight.Text == "")
            {
                this.txtTotalNetWeight.Leave -= new EventHandler(txtTotalNetWeight_Leave);
                MessageBox.Show("Enter Weight First ", Messages.Header);
                this.txtTotalNetWeight.Focus();
                return;
            }
        }

        private void txtTotalLacquer_Enter(object sender, EventArgs e)
        {
            if (txtTotalWeight.Text == "")
            {
                this.txtTotalNetWeight.Leave -= new EventHandler(txtTotalNetWeight_Leave);
                MessageBox.Show("Enter Weight First ", Messages.Header);
                this.txtTotalNetWeight.Focus();
                return;
            }
        }

        private void txtTotalNetWeight_Click(object sender, EventArgs e)
        {
            this.txtTotalNetWeight.KeyPress += new KeyPressEventHandler(txtTotalNetWeight_KeyPress);
            this.txtTotalNetWeight.Leave += new EventHandler(txtTotalNetWeight_Leave);
        }

        private void txtGrossWeight_TextChanged(object sender, EventArgs e)
        {
            decimal val;
            if (this.txtGrossWeight.Text == "")
                return;
            else
            {
                val = Convert.ToDecimal(this.txtGrossWeight.Text);
                string s = val.ToString("N3");
                this.txtGrossWeight.Text = s;
            }
        }

        private void dgvStonesDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                try
                {
                    string txtvalue = Convert.ToString(dgvStonesDetail.Rows[e.RowIndex].Cells["Column4"].Value);
                    decimal val = Convert.ToDecimal(txtvalue);
                    dgvStonesDetail.Rows[e.RowIndex].Cells["Column4"].Value = val.ToString("N3");
                }
                catch { }
            }
        }

        private void cbxOrderNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxOrderNo.SelectedIndexChanged += new EventHandler(cbxOrderNo_SelectedIndexChanged);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            brv = new BarCodeReportViewer();
            brv.isPage = 0;
            brv.ReportNo = 1;
            brv.id = 1;
            brv.Show();
            brv = new BarCodeReportViewer();
            brv.isPage = 0;
            brv.ReportNo = 2;
            brv.id = 1;
            brv.Show();
            brv = new BarCodeReportViewer();
            brv.isPage = 0;
            brv.ReportNo = 3;
            brv.id = 1;
            brv.Show();
            brv = new BarCodeReportViewer();
            brv.isPage = 0;
            brv.ReportNo = 4;
            brv.id = 1;
            brv.Show();
            brv = new BarCodeReportViewer();
            brv.isPage = 0;
            brv.ReportNo = 5;
            brv.id = 1;
            brv.Show();
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (this.txtQty.Text == "")
                this.txtQty.Text = "1";
        }

        private void btnRowRemove_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dgvStonesDetail.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (this.dgvStonesDetail.Rows.Count == 1)
                return;
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    dgvStonesDetail.Rows.Remove(dgvStonesDetail.Rows[i]);
                }
            }
            else
                MessageBox.Show("Please Select Row", Messages.Header);
        }

        private void dgvStonesDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                if (e.Exception.Message == "DataGridViewComboBoxCell value is not valid.")
                {
                    object value = dgvStonesDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (!((DataGridViewComboBoxColumn)dgvStonesDetail.Columns[e.ColumnIndex]).Items.Contains(value))
                        ((DataGridViewComboBoxColumn)dgvStonesDetail.Columns[e.ColumnIndex]).Items.Add(value);
                }
                throw e.Exception;
            }
            catch (Exception ex)
            { }
        }

        private void dgvStonesDetail_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 2 && this.dgvStonesDetail.CurrentRow.Cells[1].FormattedValue.Equals(string.Empty))
                e.Cancel = true;
        }

        private void dgvStonesDetail_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 2; i < dgvStonesDetail.ColumnCount; i++)
            {
                if (Convert.ToString(this.dgvStonesDetail.CurrentRow.Cells[i].Value) != string.Empty)
                    return;
                else if (i == dgvStonesDetail.ColumnCount)
                    dgvStonesDetail.Rows.Remove(dgvStonesDetail.CurrentRow);
            }
        }

        public void AssignRights(Form frm, string frmRights)
        {
            string[] a;
            a = frmRights.Split(' ');
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == "Save")
                    this.btnSave.Enabled = false;
                else if (a[i] == "Edit")
                    this.btnEdit.Enabled = false;
            }
        }

        private void txtSilverNetWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtRateA_TextChanged(object sender, EventArgs e)
        {
            this.txtPriceA.Text = (FormControls.GetDecimalValue(this.txtSilverNetWeight, 3) * FormControls.GetDecimalValue(this.txtRateA, 1)).ToString("0");
        }

        private void txtRateD_TextChanged(object sender, EventArgs e)
        {
            this.txtPriceD.Text = (FormControls.GetDecimalValue(this.txtSilverNetWeight, 3) * FormControls.GetDecimalValue(this.txtRateD, 1)).ToString("0");
            this.txtSilverSalePrice.Text = (FormControls.GetDecimalValue(this.txtSilverNetWeight, 3) * FormControls.GetDecimalValue(this.txtRateD, 1)).ToString("0");
        }

        private void txtTagNo_KeyDown(object sender, KeyEventArgs e)
        {
            string st = this.txtTagNo.Text;
            if (e.KeyCode == Keys.Back)
            {
                if (t == st.Length)
                    e.SuppressKeyPress = true;
                else
                    e.SuppressKeyPress = false;
            }
            else if (st.Length == t + 4)
                e.SuppressKeyPress = true;
        }

        private void txtTagNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        public static void SaveImage(string imagePath, string savedName, int width, int height)
        {
            Image originalImage = Image.FromFile(imagePath);
            string filePath = AppDomain.CurrentDomain.BaseDirectory + savedName;

            if (width > 0 && height > 0)
            {
                Image.GetThumbnailImageAbort myCallback =
                new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image imageToSave = originalImage.GetThumbnailImage
                    (width, height, myCallback, IntPtr.Zero);
                imageToSave.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
                originalImage.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private static bool ThumbnailCallback()
        {
            return false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (DeviceExist)
            {
                videoSource = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                CloseVideoSource();
                videoSource.DesiredFrameSize = new Size(160, 120);
                videoSource.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            CloseVideoSource();

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.Name != "C:\\" && d.IsReady && d.DriveType == DriveType.Fixed)
                    st = d.Name;
            }
            if (this.pbxMain.Image != null)
            {
                try
                {
                    using (Bitmap bitmap1 = new Bitmap(this.pbxMain.Image))
                    {
                        activeDir = st;
                        //Create a new subfolder under the current active folder
                        newPath = System.IO.Path.Combine(activeDir, "CamPics");
                        string str = DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss");
                        System.IO.Directory.CreateDirectory(newPath);

                        bitmap1.Save(newPath + "\\CamImage" + str + ".Jpg", ImageFormat.Jpeg);
                        Image img = Image.FromFile(newPath + "\\CamImage" + str + ".Jpg");
                        Size newSize = new Size(3800, 3300);
                        bitmap = new Bitmap(img, newSize);
                        this.pbxMain.Image = bitmap;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error has occured: \n" + ex.Message);
                }
            }
        }

        private void WebCamCapture_ImageCaptured(object source, BusinesEntities.WebcamEventArgs e)
        {
            this.pbxMain.Image = e.WebCamImage;
        }

        private void rbtSilver_CheckedChanged(object sender, EventArgs e)
        {
            pnlGold.Visible = false;
            pnlSilver.Visible = false;
        }

        private void rbtDiamond_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlGold.Visible = true;
            this.pnlSilver.Visible = false;
        }

        private void rbtGold_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlGold.Visible = true;
            this.pnlSilver.Visible = false;
        }

        private void rbtPladium_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlGold.Visible = true;
            this.pnlSilver.Visible = false;
        }

        private void rbtPlatinum_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlGold.Visible = true;
            this.pnlSilver.Visible = false;
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            ReportViewer frm = new ReportViewer();
            frm.selectQuery = "{StockRpt.StockDate}>=Date('" + DateTime.Today + "')and {StockRpt.StockDate}<=Date('" + DateTime.Today + "')";
            frm.isPage = 1;
            frm.rpt = 1;
            frm.ShowDialog();
        }

        private void bnSale_Click(object sender, EventArgs e)
        {
            string selectQuery1 = "{CompleteSaleReport.IType}='" + "Gold" + "'";
            ReportViewer frm = new ReportViewer();
            selectQuery1 += " and {CompleteSaleReport.SaleDate}>= Date('" + DateTime.Today + "')";
            selectQuery1 += " and {CompleteSaleReport.SaleDate}<= Date('" + DateTime.Today + "')";
            frm.isPage = 3;
            frm.rpt = 1;
            frm.selectQuery = selectQuery1;
            frm.ShowDialog();
        }

        private void txtTotalNetWeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtTotalNetWeight.Text == "")
            {
                frm.RatiMashaTolaw(FormControls.GetDecimalValue(this.txtTotalNetWeight, 3), lblRtmTotalNetWeight);
                this.txtGrossWeight.Text = (FormControls.GetDecimalValue(this.txtTotalNetWeight, 3) + FormControls.GetDecimalValue(this.txtStoneWeight, 3)).ToString("0.000");
                frm.GramsOfPercentStock(FormControls.GetDecimalValue(this.txtWaste, 1), FormControls.GetDecimalValue(this.txtTotalNetWeight, 3), txtWasteInGm, txtTotalWeight);
                return;
            }
            else
            {
                if (txtTotalNetWeight.Text == ".")
                {
                    txtTotalNetWeight.Text = "0" + '.';
                    txtTotalNetWeight.SelectionStart = 2;
                }
                else
                {
                    frm.RatiMashaTola(FormControls.GetDecimalValue(this.txtTotalNetWeight, 3), lblRtmTotalNetWeight);
                    this.txtGrossWeight.Text = (FormControls.GetDecimalValue(this.txtTotalNetWeight, 3) + FormControls.GetDecimalValue(this.txtStoneWeight, 3)).ToString("0.000");
                    frm.GramsOfPercentStock(FormControls.GetDecimalValue(this.txtWaste, 1), FormControls.GetDecimalValue(this.txtTotalNetWeight, 3), txtWasteInGm, txtTotalWeight);
                }
                this.txtTotalWeight.Text = this.txtTotalNetWeight.Text;
            }
        }

        private void txtWaste_KeyUp(object sender, KeyEventArgs e)
        {
            frm.GramsOfPercentStock(FormControls.GetDecimalValue(this.txtWaste, 1), FormControls.GetDecimalValue(this.txtTotalNetWeight, 3), txtWasteInGm, txtTotalWeight);
        }

        private void txtWasteInGm_KeyUp(object sender, KeyEventArgs e)
        {
            frm.WasteInPercent(FormControls.GetDecimalValue(this.txtWasteInGm, 3), FormControls.GetDecimalValue(this.txtTotalNetWeight, 3), txtWaste, txtTotalWeight);
        }

        private void txtRatti_KeyUp(object sender, KeyEventArgs e)
        {
            frm.KaatInRatti(FormControls.GetDecimalValue(this.txtRatti, 1), FormControls.GetDecimalValue(this.txtTotalNetWeight, 3), txtPureWeight, lblRtmPureWeight);
        }

        private void txtMakingPerGm_KeyUp(object sender, KeyEventArgs e)
        {
            this.txtTotalMaking.Text = (FormControls.GetDecimalValue(this.txtMakingPerGm, 1) * FormControls.GetDecimalValue(this.txtTotalWeight, 3)).ToString("0");
        }

        private void txtTotalMaking_KeyUp(object sender, KeyEventArgs e)
        {
            this.txtMakingPerGm.Text = Math.Round((FormControls.GetDecimalValue(this.txtTotalMaking, 0) / FormControls.GetDecimalValue(this.txtTotalWeight, 3)), 0).ToString("0.0");
        }

        private void txtLacquer_KeyUp(object sender, KeyEventArgs e)
        {
            this.txtTotalLacquer.Text = (FormControls.GetDecimalValue(this.txtLacquer, 0) * FormControls.GetDecimalValue(this.txtTotalWeight, 3)).ToString("0");
        }

        private void txtTotalLacquer_KeyUp(object sender, KeyEventArgs e)
        {
            this.txtLacquer.Text = Math.Round((FormControls.GetDecimalValue(this.txtTotalLacquer, 0) / FormControls.GetDecimalValue(this.txtTotalWeight, 3)), 0).ToString("0.0");
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            StockReports frm = new StockReports();
            frm.Show();
        }

        private void cbxSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            supplier = new SupplierDAL().GetRecByAbri(this.cbxSupplier.Text);
            if (supplier != null)
            {
                this.txtName.Text = supplier.PName.ToString();
                pAcc = supplier.AccountCode;
            }
        }

        private void cbxSupplier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxSupplier.SelectedIndexChanged += new EventHandler(cbxSupplier_SelectedIndexChanged);
        }

        private void cbxSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                supplier = new SupplierDAL().GetRecByAbri(this.cbxSupplier.Text);
                if (supplier != null)
                {
                    this.txtName.Text = supplier.PName.ToString();
                    pAcc = supplier.AccountCode;
                }
                this.cbxGroupItem.Select();
            }
        }

        private void cbxKarat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Stock stok = new PurchaseDAL().GetPendingItemsBySupplier(pAcc, (int)this.cbxGroupItem.SelectedValue, cbxKarat.Text);
            }
            catch { }
        }

        private void cbxGroupItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cbxGroupItem.SelectedIndex == -1)
                {
                    this.txtTagNo.Text = "";
                    return;
                }
                else
                {
                    this.btnManual.Enabled = true;
                    Item k = (Item)this.cbxGroupItem.SelectedItem;
                    this.cbxDesignNo.DataSource = desDAL.GetAllDesignByItemId((int)k.ItemId);
                    this.cbxDesignNo.DisplayMember = "DesignNo";
                    this.cbxDesignNo.ValueMember = "DesignId";
                    this.cbxDesignNo.SelectedIndex = -1;
                    string tagNo = stkDAL.GenrateTagNo((int)k.ItemId, k.Abrivation);
                    this.txtTagNo.Text = tagNo;
                    this.cbxDesignNo.Select();
                }
                if (e.KeyCode == Keys.Enter)
                {
                    this.cbxKarat.Select();
                }
            }
        }

        private void cbxDesignNo_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxWorkerName);
            if (e.KeyCode == Keys.Enter)
            {
                this.txtSize.Select();
            }
        }

        private void cbxWorkerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDescription.Select();
                cbxWorkerName_SelectedIndexChanged(sender, e);
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtTotalNetWeight);
        }

        private void txtTotalNetWeight_KeyDown_1(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtWaste);
        }

        private void txtWaste_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtWasteInGm);
        }

        private void txtWasteInGm_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtRatti);
        }

        private void txtRatti_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtMakingPerGm);
        }

        private void txtMakingPerGm_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtTotalMaking);
        }

        private void txtTotalMaking_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtLacquer);
        }

        private void txtLacquer_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtTotalLacquer);
        }

        private void txtTotalLacquer_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtPurchaseRate);
        }

        private void btnDeletertReturn_Click(object sender, EventArgs e)
        {
            if (this.txtReturnTag.Text == "")
            {
                MessageBox.Show("Must Enter TagNo", Messages.Header);
                return;
            }
            else
            {
                if (MessageBox.Show("Are You sure Delete tag return in stock then click yes else no", Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    slDAL.DeleteStock(this.txtReturnTag.Text);
                    MessageBox.Show("Damage Tag Return Successfully", Messages.Header);
                    this.txtReturnTag.Text = "";
                }
            }
        }

        private void btnDamageReturn_Click(object sender, EventArgs e)
        {
            if (this.txtReturnTag.Text == "")
            {
                MessageBox.Show("Must Enter TagNo", Messages.Header);
                return;
            }
            else
            {
                if (MessageBox.Show("Are You sure Damage tag return in stock then click yes else no", Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    slDAL.DamageStock(this.txtReturnTag.Text);
                    MessageBox.Show("Damager Tag Return Successfully", Messages.Header);
                    this.txtReturnTag.Text = "";
                }
            }
        }

        private void txtRateA_TextChanged_1(object sender, EventArgs e)
        {
            txtRateA_TextChanged(sender, e);
        }

        private void txtRateD_TextChanged_1(object sender, EventArgs e)
        {
            txtRateD_TextChanged(sender, e);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddItem frmAddItem = new AddItem();
            frmAddItem.ShowDialog();
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
        }

        private void btnAddWorker_Click(object sender, EventArgs e)
        {
            ManageWorker frmAddItem = new ManageWorker();
            frmAddItem.ShowDialog();
            FormControls.FillCombobox(cbxWorkerName, wrkDAL.GetAllWorkers(), "Name", "ID");
        }

        private void btnDesign_Click(object sender, EventArgs e)
        {
            AddDesign add = new AddDesign();
            add.ShowDialog();
            if (this.cbxDesignNo.SelectedIndex != -1)
            {
                int k = (int)this.cbxGroupItem.SelectedValue;
                this.cbxDesignNo.DataSource = desDAL.GetAllDesignByItemId(k);
                this.cbxDesignNo.DisplayMember = "DesignNo";
                this.cbxDesignNo.ValueMember = "DesignId";
                this.cbxDesignNo.SelectedIndex = -1;
            }
        }

        private void btnStonesdetail_Click(object sender, EventArgs e)
        {
            StoneDetail sd = new StoneDetail();
            sd.ShowDialog();
            this.ShowDataGrid();
        }

        private void btnBarCode_Click(object sender, EventArgs e)
        {
            BarCodeReports br = new BarCodeReports();
            br.ShowDialog();
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.txtPieces.Select();
        }

        private void cbxKarat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.txtQty.Select();
        }

        private void txtPieces_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.txtTagNo.Select();
        }

        private void txtTagNo_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.txtDesNo.Select();
        }

        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.cbxWorkerName.Select();
        }

        private void txtDesNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.txtSize.Select();
        }

        private void btnAddPic_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void txtPurchaseRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtPurchaseRate_KeyUp(object sender, KeyEventArgs e)
        {
            this.txtItemCost.Text = Math.Round((FormControls.GetDecimalValue(this.txtTotalNetWeight, 3) * FormControls.GetDecimalValue(this.txtPurchaseRate, 1)), 0).ToString();
        }

        private void txtPurchaseRate_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtItemCost);
        }

        private void txtItemCost_KeyUp(object sender, KeyEventArgs e)
        {
            this.txtPurchaseRate.Text = Math.Round((FormControls.GetDecimalValue(this.txtItemCost, 0) / FormControls.GetDecimalValue(this.txtTotalNetWeight, 3)), 0).ToString();
        }

        private void txtItemCost_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnBrowse);
        }

        private void btnGetWeight_Click(object sender, EventArgs e)
        {
            if (_serialPort.IsOpen == false)
            {
                _serialPort.PortName = DALHelper.PortName;                 //COM1
                _serialPort.BaudRate = DALHelper.BaudRate;                   //9600
                _serialPort.DataBits = DALHelper.DataBits;                      //8
                _serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), DALHelper.Parity);              //None
                _serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), DALHelper.StopBits);           //1
                if (DALHelper.HandshakeONOFF == 1)
                    _serialPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), DALHelper.Handshake);
                if (DALHelper.RtsEnableONOFF == 1)
                    _serialPort.RtsEnable = true;
                try
                {
                    _serialPort.Open();
                }
                catch
                {
                    _serialPort = new SerialPort();
                    MessageBox.Show("COM port are not connected please try again", Messages.Header);
                }
            }
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var reading = "";
            if (DALHelper.ReadLine == "YES")
            {
                try
                {
                    reading = _serialPort.ReadLine();//_serialPort.ReadExisting();//
                }
                catch (System.IO.IOException error)
                {
                    return;
                }
                catch (System.InvalidOperationException error)
                {
                    return;
                }
            }
            else
                _serialPort.ReadExisting();
            Log(reading);
        }

        private void Log(string msg)
        {
            foreach (char c in msg)
            {
                if (Char.IsDigit(c) || c == '.')
                { }
                else
                    msg = msg.Replace(c.ToString(), string.Empty);
            }
            try
            {
                double wt = Convert.ToDouble(msg);
                strr = wt.ToString("0.00");
                txtTotalNetWeight.Invoke(new EventHandler(delegate
                {
                    txtTotalNetWeight.Text = string.Empty;
                    txtTotalNetWeight.Text = strr;
                }));
            }
            catch
            { }
        }
        delegate void SetTextCallback(string text);

        private void CloseSerialOnExit()
        {
            try
            {
                _serialPort.Close(); //close the serial port
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //catch any serial port closing error messages
            }
            this.BeginInvoke(new EventHandler(NowClose)); //now close back in the main thread
        }

        private void NowClose(object sender, EventArgs e)
        {
            this.Close(); //now close the form
        }

        private void CloseSerialOnDispose()
        {
            try
            {
                _serialPort.Close(); //close the serial port
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); //catch any serial port closing error messages
            }
            this.BeginInvoke(new EventHandler(NowClose)); //now close back in the main thread
        }

        private void NowDispose(object sender, EventArgs e)
        {
            this.Dispose(); //now close the form
        }

        private void ManageStock_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseVideoSource();

            if (_serialPort.IsOpen)
            {
                e.Cancel = true; //cancel the fom closing
                Thread CloseDown = new Thread(new ThreadStart(CloseSerialOnExit)); //close port in new thread to avoid hang
                CloseDown.Start(); //close port in new thread to avoid hang
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
    }
}


