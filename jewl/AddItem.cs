using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DAL;
using BusinesEntities;

namespace jewl
{
    public partial class AddItem : Form
    {
        bool nFlag = false, aFlag = false, cFlag = false;
        Item itm;
        ItemDAL itmDAL = new ItemDAL();
        List<Item> items;
        UserRights ur = new UserRights();
        int k, l, m;
        public int ItemId = 0;

        public AddItem()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    if (this.txtGroupItemName.Text == "")
                    {
                        MessageBox.Show("No item to save", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (this.txtAbrivation.Text == "")
                    {
                        MessageBox.Show("You must enter the abbrivation", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        itm = new Item();
                        int id = itmDAL.GetMaxCode() + 1;
                        itm.ItemId = id;
                        ItemId = id;
                        CultureInfo culInfo = CultureInfo.CurrentCulture;
                        TextInfo txtInfo = culInfo.TextInfo;
                        string str1 = this.txtGroupItemName.Text.ToString();
                        itm.ItemName = txtInfo.ToTitleCase(str1);

                        string str = this.txtAbrivation.Text.ToString();

                        itm.Abrivation = txtInfo.ToUpper(str);
                        nFlag = itmDAL.isNameExist("select ItemName from Item where ItemName='" + itm.ItemName + "'");
                        if (nFlag == true || cFlag == true)
                        {
                            MessageBox.Show("Name already exist", Messages.Header);
                            return;
                        }
                        aFlag = itmDAL.isAbriExist(itm.Abrivation);
                        if (aFlag == true)
                        {
                            MessageBox.Show("Abrivation already exist", Messages.Header);
                            return;
                        }
                        else
                            itmDAL.AddItem(itm);

                        MessageBox.Show(Messages.Saved, Messages.Header);
                        this.ShowItemRecord();
                        this.txtGroupItemName.Text = "";
                        this.txtAbrivation.Text = "";
                        this.RefreshTabPage1();
                        this.txtGroupItemName.Select();
                    }
                }
            }            
        }

        private void AddItem_Load(object sender, EventArgs e)
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
            {
                this.btnSave.Enabled = true;
            }
            else
            {
                str = ur.GetUserRightsByUser("AddItem");
                if (str != "" && str != null)
                {
                    ur.AssignRights(str, btnSave, btnEdit, btnDelete);
                }
            }

            this.ShowItemRecord();

            this.txtGroupItemName.Select();
        }

        private void SearchItem(string name)
        {

            int j = 1;
            items = itmDAL.GetAllItemsByName(name);
            if (items == null)
                return;
            else
            {
                this.dgvExistingGroupDetail.AutoGenerateColumns = false;
                this.dgvExistingGroupDetail.Rows.Clear();
                int count = items.Count;
                this.dgvExistingGroupDetail.Rows.Add(count);

                for (int i = 0; i < count; i++)
                {

                    this.dgvExistingGroupDetail.Rows[i].Cells[0].Value = i + j;
                    this.dgvExistingGroupDetail.Rows[i].Cells[1].Value = items[i].ItemName.ToString();
                    this.dgvExistingGroupDetail.Rows[i].Cells[2].Value = items[i].Abrivation.ToString();
                    this.dgvExistingGroupDetail.Rows[i].Cells[3].Value = items[i].ItemId.ToString();
                }
            }

        }

        private void ShowItemRecord()
        {
            int j = 1;
            items = itmDAL.GetAllItems();
            if (items == null)
                return;
            else
            {
                this.dgvExistingGroupDetail.AutoGenerateColumns = false;
                this.dgvExistingGroupDetail.Rows.Clear();
                int count = items.Count;
                this.dgvExistingGroupDetail.Rows.Add(count);

                for (int i = 0; i < count; i++)
                {

                    this.dgvExistingGroupDetail.Rows[i].Cells[0].Value = i + j;
                    this.dgvExistingGroupDetail.Rows[i].Cells[1].Value = items[i].ItemName.ToString();
                    this.dgvExistingGroupDetail.Rows[i].Cells[2].Value = items[i].Abrivation.ToString();
                    this.dgvExistingGroupDetail.Rows[i].Cells[3].Value = items[i].ItemId.ToString();
                }
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.txtGroupItemName.CausesValidation = false;
            this.Dispose();
        }

        private void RefreshTabPage1()
        {
            this.txtGroupItemName.Text = "";
            this.txtAbrivation.Text = "";
        }

        private void dgvExistingGroupDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                k = Convert.ToInt32(this.dgvExistingGroupDetail.Rows[e.RowIndex].Cells[3].Value);
                // itmDAL.GetItemById(k);
                this.showRec(k);
                this.btnEdit.Text = "&Update";
                //this.btnDelete.Enabled = false;
                this.btnSave.Enabled = false;
            }
        }
        private void showRec(int id)
        {
            itm = itmDAL.GetItmById(id);
            if (itm == null)
                return;
            else
            {
                this.txtGroupItemName.Text = itm.ItemName.ToString();
                this.txtAbrivation.Text = itm.Abrivation.ToString();
            }
        }

        private bool KeyCheck(object sender, KeyPressEventArgs e)
        {

            bool bFlag = false;
            if (e.KeyChar == 13 || e.KeyChar == 27 || (Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32)
                bFlag = true;
            return bFlag;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (k == 0)
                {
                    MessageBox.Show("Please select the record to update", Messages.Header);
                    return;
                }
                if (btnEdit.Text == "&Update")
                {
                    bool iFlag = false;
                    bool sFlag = false;
                    bool cFlag = false;
                    iFlag = itmDAL.isItemIdExist(k);
                    //sFlag = itmDAL.isItemExist(k);
                    cFlag = itmDAL.isItemExistInCost(k);
                    if (iFlag == true || cFlag == true)
                    {
                        MessageBox.Show("You can not update this record it is already in use", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (this.txtGroupItemName.Text == "")
                    {
                        MessageBox.Show("No item to save", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (this.txtAbrivation.Text == "")
                    {
                        MessageBox.Show("You must enter the abbrivation", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        itm = new Item();

                        CultureInfo culInfo = CultureInfo.CurrentCulture;
                        TextInfo txtInfo = culInfo.TextInfo;
                        string str1 = this.txtGroupItemName.Text.ToString();
                        itm.ItemName = txtInfo.ToTitleCase(str1);
                        //itm.ItemName = this.txtGroupItemName.Text;
                        string str = this.txtAbrivation.Text.ToString();
                        itm.Abrivation = txtInfo.ToUpper(str);
                        //itm.Abrivation = this.txtAbrivation.Text;

                        // MessageBox.Show("k="+k);
                        itmDAL.UpdateItem(k, itm);
                        MessageBox.Show(Messages.Updated, Messages.Header);
                        this.ShowItemRecord();
                        this.btnEdit.Text = "&Edit";
                        this.btnSave.Enabled = true;
                        this.btnDelete.Enabled = true;
                        this.RefreshTabPage1();
                    }
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (k == 0)
                {
                    MessageBox.Show("Please select the record to delete", Messages.Header);
                    return;
                }
                if (k != 0)
                {
                    bool iFlag = false;
                    iFlag = itmDAL.isItemIdExist(k);
                    if (iFlag == true)
                    {
                        MessageBox.Show("You can not delete this record it is already in use", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure to delete this record:Press Ok To Continue", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            itm = new Item();
                            itmDAL.DeleteItem(k, itm);
                            MessageBox.Show("Record deleted Successfully", Messages.Header,MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                            this.ShowItemRecord();
                            this.btnEdit.Text = "Edit";
                            this.btnSave.Enabled = true;
                            //this.btnDelete.Enabled = true;
                            this.RefreshTabPage1();
                        }
                    }
                }
            }
        }

        private void txtGroupItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //bool bFlag = false;
            //bFlag = this.KeyCheck(sender, e);
            //if (bFlag == true)
            //    e.Handled = true;
        }

        private void txtAbrivation_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                this.txtGroupItemName.Text = "";
                this.txtAbrivation.Text = "";
                this.btnSave.Enabled = true;
                this.btnEdit.Text = "&Edit";
                this.txtSearch.Select();
            }
        }

        private void txtGroupItemName_Leave(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
            this.btnEdit.Text = "&Edit";
            this.txtAbrivation.Text = "";
            this.txtGroupItemName.Text = "";
        }

        private void AddItem_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void txtGroupItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtAbrivation.Select();
            }
        }

        private void txtAbrivation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSave.Select();
                btnSave_Click(sender, e);
            }
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtGroupItemName);
        }

        private void txtGroupItemName_Validating(object sender, CancelEventArgs e)
        {
            //FormControls.validate_box(txtGroupItemName, e, errorProvider1, "Item Name");
        }

        private void txtAbrivation_Validating(object sender, CancelEventArgs e)
        {
            string str = this.txtAbrivation.Text.ToString();
            if (str.Length > 4)
            {
                e.Cancel = true;
                this.txtAbrivation.Focus();
                errorProvider1.SetError(txtAbrivation, "Abbrivation Must Contain Only Two Characters");

            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtAbrivation, "");
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            SearchItem(this.txtSearch.Text);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtGroupItemName.Focus();
            }
        }

        private void txtGroupItemName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
