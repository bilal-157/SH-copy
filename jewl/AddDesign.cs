using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using DAL;
using BusinesEntities;

namespace jewl
{
    public partial class AddDesign : Form
    {
        Design des;
        DesignDAL desDAL = new DesignDAL();
        ItemDAL itmDAL = new ItemDAL();
        WorkerDAL wDAL = new WorkerDAL();
        Item itm;
        List<Item> items;
        List<Design> desi;
        int k;

        public AddDesign()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool nFlag = false;
            if (tabControl1.SelectedTab == tabPage1)
            {               
                if (this.txtDesignNo.Text == "")
                {
                    MessageBox.Show("First Enter Design No",Messages.Header,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    this.txtDesignNo.Select();
                    return;
                }
                des = new Design();
                des.DesignNo = this.txtDesignNo.Text.ToString();
                nFlag = desDAL.isNameExist(des.DesignNo);
                if (nFlag == true)
                {
                    MessageBox.Show("Design No already exist", Messages.Header);
                    return;
                }
                else
                {
                    desDAL.AddDesignNo(des);
                    MessageBox.Show("Record is saved successfully", Messages.Header,MessageBoxButtons.OK);

                    this.ShowDesignRecord();
                    this.txtDesignNo.Text = "";
                    this.cbxDesignNo.DataSource = desDAL.GetAllDesign();
                    this.cbxDesignNo.DisplayMember = "DesignNo";
                    this.cbxDesignNo.ValueMember = "DesignId";
                }
            }
            if (tabControl1.SelectedTab == tabPage2)
            {
                if (this.cbxDesignNo.Text==String.Empty)
                {
                     MessageBox.Show("Please Select Design No", Messages.Header,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                     this.txtDesignNo.Select();
                     return;
                }
                else if (this.cbxItemName.Text == String.Empty)
                {
                    MessageBox.Show("Please Select Item Name", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.cbxItemName.Select();
                    return;
                }
                itm = new Item();
                des = new Design();
                Item itmVal;
                itm.ItemId = (int)this.cbxItemName.SelectedValue;
                itm.DesignItem = (Design)this.cbxDesignNo.SelectedItem;
                nFlag = itmDAL.isItemAndDesignExist((int)itm.ItemId, (int)itm.DesignItem.DesignId);
                if (nFlag == true)
                {
                    MessageBox.Show("Design No already exist against this Item", Messages.Header);
                    return;
                }
                else
                {
                    desDAL.AddDesignItem(itm);
                    MessageBox.Show("Record saved successfully ", Messages.Header);
                    this.dgvExistingDesignItemDetail.Rows.Clear();
                    this.dgvExistingDesignItemDetail.Refresh();
                    this.ShowDesignItemRecord();
                    this.cbxDesignNo.SelectedIndex = -1;
                    this.cbxItemName.SelectedIndex = -1;
                }
            }
        }

        private void ShowDesignRecord()
        {
            int j = 1;
            desi = desDAL.GetAllDesign();
            if (desi == null)
                return;
            else
            {
                this.dgvExistingDesignDetail.AutoGenerateColumns = false;
                this.dgvExistingDesignDetail.Rows.Clear();
                int count = desi.Count;
                this.dgvExistingDesignDetail.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvExistingDesignDetail.Rows[i].Cells[0].Value = i + j;
                    this.dgvExistingDesignDetail.Rows[i].Cells[1].Value = desi[i].DesignNo.ToString();
                    this.dgvExistingDesignDetail.Rows[i].Cells[2].Value = desi[i].DesignId.ToString();
                }
            }
        }

        private void ShowDesignItemRecord()
        {
            int j = 1;
            items = desDAL.GetAllDesignAndItem();
            if (items == null)
                return;
            else
            {
                this.dgvExistingDesignItemDetail.AutoGenerateColumns = false;
                this.dgvExistingDesignItemDetail.Rows.Clear();
                int count = items.Count;
                this.dgvExistingDesignItemDetail.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvExistingDesignItemDetail.Rows[i].Cells[0].Value = i + j;
                    this.dgvExistingDesignItemDetail.Rows[i].Cells[1].Value = items[i].ItemName.ToString();
                    this.dgvExistingDesignItemDetail.Rows[i].Cells[2].Value = items[i].DesignItem.DesignNo.ToString();
                    this.dgvExistingDesignItemDetail.Rows[i].Cells[3].Value = items[i].DesItmid.ToString();
                }
            }
        }

        private void AddDesign_Load(object sender, EventArgs e)
        {
            UserRights ur = new UserRights();
            string str;
            str = ur.GetRightsByUser();
            if (str == "Administrator")
            {
                this.btnSave.Enabled = true;
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
            }
            else if (str == "Limited")
                this.btnSave.Enabled = true;
            else
            {
                str = ur.GetUserRightsByUser("AddDesign");
                if (str != "" && str != null)
                {
                    ur.AssignRights(str, btnSave, btnEdit, btnDelete);
                }
            }
            this.cbxDesignNo.DataSource = desDAL.GetAllDesign();
            this.cbxDesignNo.DisplayMember = "DesignNo";
            this.cbxDesignNo.ValueMember = "DesignId";

            this.cbxItemName.DataSource = itmDAL.GetAllItems();
            this.cbxItemName.DisplayMember = "ItemName";
            this.cbxItemName.ValueMember = "ItemId";

            this.cbxDesignNo.SelectedIndex = -1;
            this.cbxItemName.SelectedIndex = -1;

            this.ShowDesignRecord();
            this.ShowDesignItemRecord();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            bool dFlag = false;
            bool eFlag = false;
            bool gFlag = false;
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (k == 0)
                {
                    MessageBox.Show("Please select the record to update", Messages.Header);
                    return;
                }
                if (btnEdit.Text == "&Update")
                {
                    des = new Design();

                    des.DesignNo = this.txtDesignNo.Text.ToString();
                    dFlag = desDAL.isDesignIdExist(k);
                    eFlag = desDAL.isDesinIdExist(k);
                    gFlag = desDAL.isDesignExist(k);
                    if (dFlag == true || eFlag == true || gFlag == true)
                    {
                        MessageBox.Show("You can not update this record it is already in use", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        desDAL.UpdateDesignNo(k, des);

                        MessageBox.Show("Record update successfully", Messages.Header);
                        this.dgvExistingDesignDetail.Rows.Clear();
                        this.dgvExistingDesignDetail.Refresh();
                        this.ShowDesignRecord();
                        this.btnEdit.Text = "&Edit";
                        this.btnSave.Enabled = true;
                        this.txtDesignNo.Text = "";
                    }
                }
            }
            if (tabControl1.SelectedTab == tabPage2)
            {
                if (k == 0)
                {
                    MessageBox.Show("Please select the record to update", Messages.Header);
                    return;
                }
                if (btnEdit.Text == "&Update")
                {
                    itm = new Item();
                    Item itmVal;
                    itmVal = (Item)this.cbxItemName.SelectedItem;
                    itm.ItemId = itmVal.ItemId;
                    itm.DesignItem = (Design)this.cbxDesignNo.SelectedItem;
                    dFlag = desDAL.isDesignIdExist(k);
                    eFlag = desDAL.isDesinIdExist(k);
                    if (dFlag == true || eFlag == true)
                    {
                        MessageBox.Show("You can not update this record it is already in use", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        desDAL.UpdateDesignItem(k, itm);
                        MessageBox.Show("Record update successfully ", Messages.Header);
                        this.dgvExistingDesignItemDetail.Rows.Clear();
                        this.dgvExistingDesignItemDetail.Refresh();
                        this.ShowDesignItemRecord();
                        this.cbxDesignNo.SelectedIndex = -1;
                        this.cbxItemName.SelectedIndex = -1;
                        this.btnEdit.Text = "&Edit";
                        this.btnSave.Enabled = true;
                    }
                }
            }
        }

        private void dgvExistingDesignDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                k = Convert.ToInt32(this.dgvExistingDesignDetail.Rows[e.RowIndex].Cells[2].Value);
                this.ShowDesById(k);
                this.btnEdit.Text = "&Update";
                this.btnSave.Enabled = false;
            }
        }

        private void ShowDesById(int g)
        {
            des = desDAL.GetDesignById(g);
            if (des == null)
                return;
            else
                this.txtDesignNo.Text = des.DesignNo.ToString();
        }

        private void ShowDesItmById(int g)
        {
            itm = desDAL.GetDesItmById(g);
            if (itm == null)
                return;
            else
            {
                for (int i = 0; i < this.cbxItemName.Items.Count; i++)
                {
                    Item it = (Item)this.cbxItemName.Items[i];
                    if (itm.ItemId == it.ItemId)
                        this.cbxItemName.SelectedIndex = i;
                }
                for (int i = 0; i < this.cbxDesignNo.Items.Count; i++)
                {
                    Design d = (Design)this.cbxDesignNo.Items[i];
                    if (itm.DesignItem.DesignId == d.DesignId)
                        this.cbxDesignNo.SelectedIndex = i;
                }
            }
        }

        private void dgvExistingDesignItemDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                k = Convert.ToInt32(this.dgvExistingDesignItemDetail.Rows[e.RowIndex].Cells[3].Value);
                this.ShowDesItmById(k);
                this.btnEdit.Text = "&Update";
                this.btnSave.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool dFlag = false;
            bool eFlag = false;
            bool gFlag = false;
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (k == 0)
                {
                    MessageBox.Show("Please select the record to delete", Messages.Header);
                    return;
                }
                if (k != 0)
                {
                    dFlag = desDAL.isDesignIdExist(k);
                    eFlag = desDAL.isDesinIdExist(k);
                    gFlag = desDAL.isDesignExist(k);
                    if (dFlag == true || eFlag == true || gFlag == true)
                    {
                        MessageBox.Show("You can not delete this record it is already in use", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure to delete this record:Press Ok To Continue", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            des = new Design();

                            desDAL.DeleteDesign(k, des);
                            MessageBox.Show("Record deleted Successfully", Messages.Header);
                            this.dgvExistingDesignDetail.Rows.Clear();
                            this.dgvExistingDesignDetail.Refresh();
                            this.ShowDesignRecord();
                            this.btnEdit.Text = "&Edit";
                            this.btnSave.Enabled = true;
                            this.txtDesignNo.Text = "";
                        }
                    }
                }
            }
            if (tabControl1.SelectedTab == tabPage2)
            {
                if (k == 0)
                {
                    MessageBox.Show("Please select the record to delete", Messages.Header);
                    return;
                }
                if (k != 0)
                {
                    if (MessageBox.Show("Are you sure to delete this record:Press Ok To Continue", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        itm = new Item();
                        desDAL.DeleteDesignItem(k, itm);
                        MessageBox.Show("Record deleted Successfully", Messages.Header);
                        this.dgvExistingDesignItemDetail.Rows.Clear();
                        this.dgvExistingDesignItemDetail.Refresh();
                        this.ShowDesignItemRecord();
                        this.cbxDesignNo.SelectedIndex = -1;
                        this.cbxItemName.SelectedIndex = -1;
                        this.btnEdit.Text = "&Edit";
                        this.btnSave.Enabled = true;
                    }
                }
            }
        }

        public void AssignRights(Form frm, string frmRights)
        {
            string[] a;
            a = frmRights.Split(' ');

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == "Save")
                {
                    this.btnSave.Enabled = false;
                }
                else if (a[i] == "Edit")
                {
                    this.btnEdit.Enabled = false;
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtDesignNo.Text = "";
            this.btnEdit.Text = "Edit";
            this.btnSave.Enabled = true;
            this.cbxDesignNo.Text = "";
            this.cbxItemName.Text = "";
        }

        private void txtDesignNo_KeyUp(object sender, KeyEventArgs e)
        {
            string str = this.txtDesignNo.Text;
            if (string.IsNullOrEmpty(str))
            {
                dgvExistingDesignDetail.Rows.Clear();
                this.ShowDesignRecord();
                return;
            }
            else
            {
                desi = null;
                dgvExistingDesignDetail.Visible = true;
                desi = desDAL.GetAllDesignByDesign(str);
                if (desi == null)
                    return;
                else
                {
                    this.dgvExistingDesignDetail.AutoGenerateColumns = false;
                    int count = desi.Count;
                    this.dgvExistingDesignDetail.Rows.Clear();
                    this.dgvExistingDesignDetail.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        int k = 1;
                        this.dgvExistingDesignDetail.Rows[i].Cells[0].Value = i + k;
                        this.dgvExistingDesignDetail.Rows[i].Cells[1].Value = desi[i].DesignNo.ToString();
                        this.dgvExistingDesignDetail.Rows[i].Cells[2].Value = Convert.ToInt32(desi[i].DesignId);
                    }
                }
            }
        }

        private void txtDesignNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}