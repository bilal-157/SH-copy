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
using System.IO;
using System.Drawing.Drawing2D;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;



namespace jewl
{
    public partial class frmStockCheck : Form
    {
        JewelConnection conn;
        ItemDAL itmDAL = new ItemDAL();
        Stock stk = new Stock();
        StockDAL sDAL = new StockDAL();
        Item itm;
        int qut;
        decimal wt;
        string itemName;
        bool ch = false, eflag = false;

        public frmStockCheck()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void ShowAllRecordByTag(string tagNo)
        {
            if (string.IsNullOrEmpty(tagNo))
                return;
            else
            {
                stk = sDAL.GetStkByStkTagStA(tagNo);
                if (stk == null)
                    return;
                else
                {
                    itemName = stk.ItemName.ItemName.ToString();
                    if (this.cbxGroupItem.Text == itemName)
                    {
                        this.txtTagNo.Text = stk.TagNo;
                        if (stk.NetWeight.HasValue)
                        {
                            this.txtNetWeight.Text = Convert.ToString(Math.Round((decimal)stk.NetWeight, 3));
                            wt = (decimal)stk.NetWeight;
                        }
                        else
                        {
                            this.txtNetWeight.Text = "";
                            wt = 0;
                        }
                        if (stk.WasteInGm.HasValue)
                            this.txtWasteInGm.Text = Convert.ToString(Math.Round((decimal)stk.WasteInGm, 3));
                        else
                            this.txtWasteInGm.Text = "0";
                        if (stk.WastePercent.HasValue)
                            this.txtWasteInPercent.Text = Convert.ToString(Math.Round((decimal)stk.WastePercent, 1));
                        else
                            this.txtWasteInPercent.Text = "0";
                        if (stk.TotalWeight > 0)
                            this.txtTotalWeight.Text = Convert.ToString(Math.Round((decimal)stk.TotalWeight, 3));
                        else
                            this.txtTotalWeight.Text = "0";
                        txtDate.Text = stk.StockDate.ToString();
                        this.txtKarat.Text = stk.Karrat;
                        if (stk.Qty.HasValue)
                        {
                            this.txtQty.Text = stk.Qty.ToString();
                            qut = (int)stk.Qty;
                        }
                        else
                            this.txtQty.Text = "1";
                        this.txtDescription.Text = stk.Description.ToString();

                        if (stk.ImageMemory == null)
                        {
                            this.pbxPicture.Image = null;
                            this.pbxPicture.BorderStyle = BorderStyle.FixedSingle;
                        }
                        else
                        {
                            MemoryStream mst = new MemoryStream(stk.ImageMemory);
                            this.pbxPicture.Image = Image.FromStream(mst);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not Found", Messages.Header);
                        return;
                    }
                }
            }
        }

        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && (Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 13)
                e.Handled = true;
            else
                e.Handled = false;
            string str = "";
            str = this.txtBarCode.Text;
            if (e.KeyChar == 13)
            {
                string h = itm.Abrivation.ToString();
                string g = str.Remove(str.Length - 5);
                if (g.ToUpper() == h.ToUpper())
                {
                    this.ShowAllRecordByTag(str);
                    CultureInfo culInfo = CultureInfo.CurrentCulture;
                    TextInfo txtinfo = culInfo.TextInfo;
                    string barCode = txtinfo.ToUpper(str);
                    txtBarCode.Text = "";
                    txtBarCode.Select();
                    if (txtTagNo.Text.ToUpper() == barCode)
                    {
                        if (rbtNewScan.Checked == true)
                        {
                            conn = new JewelConnection();
                            string query1, query2;
                            ch = sDAL.isSessionIdExist(Convert.ToInt32(this.txtNewScan.Text));
                            if (ch == true)
                            {
                                ch = false;
                                ch = sDAL.isTagScanned(Convert.ToInt32(this.txtNewScan.Text), str);
                                if (ch == true)
                                {
                                    MessageBox.Show("Tag Has Already Scanned!", Messages.Header);
                                    return;
                                }
                                query2 = "insert into tblStockCheck values ('" + txtNewScan.Text
                                                                       + "','" + cbxGroupItem.Text
                                                                       + "','" + cbxGroupItem.SelectedValue
                                                                       + "','" + txtTagNo.Text
                                                                       + "','" + txtNetWeight.Text
                                                                       + "','" + txtWasteInGm.Text
                                                                       + "','" + txtWasteInPercent.Text
                                                                       + "','" + txtQty.Text
                                                                       + "','" + txtDescription.Text
                                                                       + "','" + txtKarat.Text
                                                                       + "','" + DateTime.Now
                                                                       + "','" + "Scan" + "')";
                                conn.SaveRecordFromJMDB(query2);
                            }
                            else
                            {
                                query1 = "insert into tblMasterScan values ('" + DateTime.Now
                                                                       + "','" + this.cbxGroupItem.Text
                                                                       + "','" + cbxGroupItem.SelectedValue
                                                                       + "','" + "Not Completed" + "') ";
                                query2 = "insert into tblStockCheck values ('" + txtNewScan.Text
                                                                        + "','" + cbxGroupItem.Text
                                                                        + "','" + cbxGroupItem.SelectedValue
                                                                        + "','" + txtTagNo.Text
                                                                        + "','" + txtNetWeight.Text
                                                                        + "','" + txtWasteInGm.Text
                                                                        + "','" + txtWasteInPercent.Text
                                                                        + "','" + txtQty.Text
                                                                        + "','" + txtDescription.Text
                                                                        + "','" + txtKarat.Text
                                                                        + "','" + DateTime.Now
                                                                        + "','" + "Scan" + "')";
                                conn.SaveRecordFromJMDB(query1);
                                conn.SaveRecordFromJMDB(query2);
                            }
                        }
                        else if (rbtOldScan.Checked == true)
                        {
                            ch = false;
                            ch = sDAL.isTagScanned(Convert.ToInt32(this.cbxOldScan.Text), str);
                            if (ch == true)
                            {
                                MessageBox.Show("Tag Has Already Scanned!", Messages.Header);
                                return;
                            }
                            try
                            {
                                string query3 = "insert into tblStockCheck values ('" + int.Parse(cbxOldScan.Text)
                                                                    + "','" + cbxGroupItem.Text
                                                                    + "','" + cbxGroupItem.SelectedValue
                                                                    + "','" + txtTagNo.Text
                                                                    + "','" + txtNetWeight.Text
                                                                    + "','" + txtWasteInGm.Text
                                                                    + "','" + txtWasteInPercent.Text
                                                                    + "','" + txtQty.Text
                                                                    + "','" + txtDescription.Text
                                                                    + "','" + txtKarat.Text
                                                                    + "','" + DateTime.Now
                                                                    + "','" + "Scan" + "')";
                                conn.SaveRecordFromJMDB(query3);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Tage Not Found", Messages.Header);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Abbrivatiion", Messages.Header);
                    return;
                }
            }
        }

        private void frmStockCheck_Load(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged -= new EventHandler(cbxGroupItem_SelectedIndexChanged);
            conn = new JewelConnection();
            cbxGroupItem.DataSource = itmDAL.GetAllItems();
            cbxGroupItem.DisplayMember = "ItemName";
            cbxGroupItem.ValueMember = "ItemId";
            cbxGroupItem.SelectedIndex = -1;

            this.txtNewScan.Text = (sDAL.GetMaxStockCheckId() + 1).ToString();

            lblSessionNo.Text = txtNewScan.Text;
            eflag = true; 
            conn = new JewelConnection();
            try
            {
                conn.MyDataSet.Tables["Table123"].Rows.Clear();
            }
            catch (Exception)
            {
            }
            try
            {
                //conn.MyDataSet.Tables["Table123"].Rows.Clear();

                this.cbxOldScan.SelectedIndexChanged -= new EventHandler(cbxOldScan_SelectedIndexChanged);
                string query = "select SessionId from tblMasterScan where Status = 'Not Completed'";
                conn.GetDataFromJMDB(query, "Table123");
                this.cbxOldScan.DataSource = conn.MyDataSet.Tables["Table123"];
                this.cbxOldScan.DisplayMember = "SessionId";
                this.cbxOldScan.SelectedIndex = -1;
            }
            catch (Exception ex0)
            {
                throw ex0;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn = new JewelConnection();
            if (cbxOldScan.Text != "")
            {
                int value = int.Parse(cbxOldScan.Text);
                string UpdateQuery;
                UpdateQuery = " UPDATE tblMasterScan SET Status = 'Completed Now' Where SessionId =" + value;

                conn.UpdateRecordFromJMDB(UpdateQuery);
                MessageBox.Show("Completed Now ", Messages.Header);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmStockCheckRpt frm = new frmStockCheckRpt();
            if (this.rbtNewScan.Checked)
            {
                frm.k = 0;
                frm.sessionid = Convert.ToInt32(this.txtNewScan.Text);
            }
            else if (this.rbtOldScan.Checked)
            {
                frm.k = 0;
                frm.sessionid = Convert.ToInt32(this.cbxOldScan.Text);
            }
            frm.Show();
        }

        private void btnItemPrint_Click(object sender, EventArgs e)
        {
            if (cbxGroupItem.SelectedIndex == -1)
            {
                MessageBox.Show("Must Select Item", Messages.Header);
                cbxGroupItem.Select();
                return;
            }
            frmStockCheckRpt frm = new frmStockCheckRpt();
            frm.k = 1;
            frm.itemId = (int)this.cbxGroupItem.SelectedValue;
            frm.Show();
        }

        private void rbtOldScan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtOldScan.Checked == true)
            {
                cbxOldScan.Enabled = true;
                txtNewScan.Enabled = false;
            }
            else if (rbtOldScan.Checked == false)
            {
                cbxOldScan.Enabled = false;
                txtNewScan.Enabled = true;
            }
        }

        private void rbtNewScan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtNewScan.Checked == true)
            {
                cbxOldScan.Enabled = false;
                txtNewScan.Enabled = true;
            }
            else if (rbtOldScan.Checked == false)
            {
                cbxOldScan.Enabled = true;
                txtNewScan.Enabled = false;
            }
        }

        private void cbxGroupItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            itm = (Item)this.cbxGroupItem.SelectedItem;
        }

        private void cbxGroupItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged += new EventHandler(cbxGroupItem_SelectedIndexChanged);
        }

        private void btnSelectedItem_Click(object sender, EventArgs e)
        {
            if(eflag == true)
            {
                if (rbtNewScan.Checked == true)
                {
                    lblSessionNo.Text = txtNewScan.Text;
                    lblItemId.Text = cbxGroupItem.SelectedValue.ToString();
                }
                else if (rbtOldScan.Checked == true)
                {
                    lblSessionNo.Text = cbxOldScan.Text;
                    lblItemId.Text = cbxGroupItem.SelectedValue.ToString();
                }
                eflag = false;
            }
            if (rbtNewScan.Checked == true)
            {
                if (txtNewScan.Text == lblSessionNo.Text && cbxGroupItem.SelectedValue.ToString() == lblItemId.Text)
                {
                    lblSessionNo.Text = txtNewScan.Text;
                    lblItemId.Text = cbxGroupItem.SelectedValue.ToString();
                }
                else
                {
                    MessageBox.Show("Item cannot selected!", Messages.Header);
                    return;
                }
            }
            else if (rbtOldScan.Checked == true)
            {
                if (cbxOldScan.Text == lblSessionNo.Text && cbxGroupItem.SelectedValue.ToString() == lblItemId.Text)
                {
                    lblSessionNo.Text = cbxOldScan.Text;
                    lblItemId.Text = cbxGroupItem.SelectedValue.ToString();
                }
                else
                {
                    MessageBox.Show("Item cannot selected!", Messages.Header);
                    return;
                }
            }
        }

        private void cbxOldScan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxOldScan.SelectedIndex != -1)
            {
                lblSessionNo.Text = cbxOldScan.Text;
                string query = "select ItemId from tblMasterScan where Status = 'Not Completed' and SessionId = " + lblSessionNo.Text;
                conn.GetDataFromJMDB(query, "Table14");
                DataRow dr = conn.MyDataSet.Tables["Table14"].Rows[0];
                lblItemId.Text = dr["ItemId"].ToString();
                eflag = false;
            }
        }

        private void cbxOldScan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxOldScan.SelectedIndexChanged += new EventHandler(cbxOldScan_SelectedIndexChanged);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            frmStockCheck frm = new frmStockCheck();
            frm.ShowDialog();
        }
    }
}
