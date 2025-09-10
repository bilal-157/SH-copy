using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class StoneDetail : Form
    {
        private StonesDAL sDAL= new StonesDAL();
        private StoneColor sColor;
        private Stones stone;
        private StoneCut sCut;
        private StoneClearity clearity;
        List<Stones> stns;
        List<StoneColor> stns1;
        List<StoneCut> stns2;
        List<StoneClearity> stns3;
        int k=0;
        string str;
        public StoneDetail()
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
                  

                    bool nFlag = false;
                    stone = new Stones();
                    if (this.cbxStoneType.Text != string.Empty)
                    {
                        string str = txtStoneName.Text.ToString();
                        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                        TextInfo textInfo = cultureInfo.TextInfo;
                        stone.StoneName = textInfo.ToTitleCase(str);
                        stone.StoneTypeId = (int)this.cbxStoneType.SelectedValue;
                        nFlag = sDAL.isStNameExist(stone.StoneName, stone.StoneTypeId);
                        if (this.txtStoneName.Text == "")
                        {
                            MessageBox.Show("First Enter Stone Name", Messages.Header);
                            return;
                        }
                        if (nFlag == true)
                        {
                            MessageBox.Show("Name already exist", Messages.Header);
                            return;
                        }
                        else
                            sDAL.AddStoneName(stone);
                        MessageBox.Show("Stone name is added", Messages.Header);
                        this.txtStoneName.Text = "";
                        this.ShowStoneNameRecord();
                        this.cbxStoneType.Select();
                    }
                    else
                    {
                        MessageBox.Show("Select Stone Type", Messages.Header);
                        return;
                    }

                }

            }
            if (tabControl1.SelectedTab == tabPage2)
            {
                bool nflag = false;
                sColor = new StoneColor();
                if (this.txtStoneColour.Text != string.Empty)
                {

                    string str1 = txtStoneColour.Text.ToString();
                    CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;
                    sColor.ColorName = textInfo.ToTitleCase(str1);
                    //sColor.ColorName = this.txtStoneColour.Text;
                    nflag = sDAL.isColorNameExist(sColor.ColorName);
                    if (this.txtStoneColour.Text == "")
                    {
                        MessageBox.Show("First Enter Stone Colour", Messages.Header);
                        return;
                    }
                    if (nflag == true)
                    {
                        MessageBox.Show("Color Name Aready Exist", Messages.Header);
                        return;
                    }

                    sDAL.AddStoneColor(sColor);

                    MessageBox.Show("Color name is added", Messages.Header);
                    this.txtStoneColour.Text = "";
                    this.ShowColorNameRecord();
                }

            }


            if (tabControl1.SelectedTab == tabPage3)
            {
                bool nflag = false;
                sCut = new StoneCut();
                string str = txtDiamondCut.Text.ToString();
                CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                sCut.CutName = textInfo.ToTitleCase(str);
                nflag = sDAL.isCutNameExist(sCut.CutName );
                if (this.txtDiamondCut.Text == "")
                {
                    MessageBox.Show("First Enter Stone Cut", Messages.Header);
                    return;
                }
                if (nflag == true)
                {
                    MessageBox.Show("Cut Name Aready Exist", Messages.Header);
                    return;
                }

                sDAL.AddCut(sCut);

                MessageBox.Show("Cut name is added ", Messages.Header);
                this.txtDiamondCut.Text = "";
                this.ShowStoneCutRecord();

            }
            if (tabControl1.SelectedTab == tabPage4)
            {
                bool nflag = false;
                clearity = new StoneClearity();
                string str = txtDiamondClearity.Text.ToString();
                CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                clearity.ClearityName = textInfo.ToTitleCase(str);
                nflag = sDAL.isClearityNameExist(clearity.ClearityName );
                if (this.txtDiamondClearity.Text == "")
                {
                    MessageBox.Show("First Enter Stone Clearity", Messages.Header);
                    return;
                }
                if (nflag == true)
                {
                    MessageBox.Show("Cut Name Aready Exist", Messages.Header);
                    return;
                }

                sDAL.AddClearity(clearity);

                MessageBox.Show("Clearity name is added", Messages.Header);
                this.txtDiamondClearity.Text = "";
                this.ShowStoneClearityRecord();
            }
            
        }
        

        private void ShowStoneNameRecord()
        {
            int j = 1;
            stns = sDAL.GetAllStoneName();
            if (stns == null)
                return;
            else
            {
                this.dgvExistingStones.AutoGenerateColumns = false;
                this.dgvExistingStones.Rows.Clear();
                int count = stns.Count;
                this.dgvExistingStones.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvExistingStones.Rows[i].Cells[0].Value = i + j;
                    this.dgvExistingStones.Rows[i].Cells[1].Value = stns[i].StoneName.ToString();
                    this.dgvExistingStones.Rows[i].Cells[2].Value = stns[i].StoneId.ToString();
                    //DataGridViewComboBoxColumn comboBox = (DataGridViewComboBoxColumn)dgvExistingStones.Rows[i].Cells[1].OwningColumn;
                    // this.dgvExistingDesignItemDetail.Rows[i].Cells[1].Value = items[i].DesignItem.DesignNo.ToString();
                }
            }

        }

        private void ShowColorNameRecord()
        {
            int j = 1;
            stns1 = sDAL.GetAllColorName();
            if (stns1 == null)
                return;
            else
            {
                this.dgvStoneColor.AutoGenerateColumns = false;
                this.dgvStoneColor.Rows.Clear();
                int count = stns1.Count;
                this.dgvStoneColor.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvStoneColor.Rows[i].Cells[0].Value = i + j;
                    this.dgvStoneColor.Rows[i].Cells[1].Value = stns1[i].ColorName.ToString();
                    this.dgvStoneColor.Rows[i].Cells[2].Value = stns1[i].ColorId.ToString();

                    //DataGridViewComboBoxColumn comboBox = (DataGridViewComboBoxColumn)dgvExistingStones.Rows[i].Cells[1].OwningColumn;
                    // this.dgvExistingDesignItemDetail.Rows[i].Cells[1].Value = items[i].DesignItem.DesignNo.ToString();
                }
            }

        }

        private void ShowStoneCutRecord()
        {
            int j = 1;
            stns2 = sDAL.GetAllCutName();
            if (stns2 == null)
                return;
            else
            {
                this.dgvDiamondCut.AutoGenerateColumns = false ;
                this.dgvDiamondCut.Rows.Clear();
                int count = stns2.Count;
                this.dgvDiamondCut.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvDiamondCut.Rows[i].Cells[0].Value = i + j;
                    this.dgvDiamondCut.Rows[i].Cells[1].Value = stns2[i].CutName.ToString();
                    this.dgvDiamondCut.Rows[i].Cells[2].Value = stns2[i].CutId.ToString();
                    //DataGridViewComboBoxColumn comboBox = (DataGridViewComboBoxColumn)dgvExistingStones.Rows[i].Cells[1].OwningColumn;
                    // this.dgvExistingDesignItemDetail.Rows[i].Cells[1].Value = items[i].DesignItem.DesignNo.ToString();
                }
            }

        }

        private void ShowStoneClearityRecord()
        {
            int j = 1;
            stns3 = sDAL.GetAllClearityName();
            if (stns3 == null)
                return;
            else
            {
                this.dgvDiamondClearity.AutoGenerateColumns = false;
                this.dgvDiamondClearity.Rows.Clear();
                int count = stns3.Count;
                this.dgvDiamondClearity.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvDiamondClearity.Rows[i].Cells[0].Value = i + j;
                    this.dgvDiamondClearity.Rows[i].Cells[1].Value = stns3[i].ClearityName.ToString();
                    this.dgvDiamondClearity.Rows[i].Cells[2].Value = stns3[i].ClearityId.ToString();
                    //DataGridViewComboBoxColumn comboBox = (DataGridViewComboBoxColumn)dgvExistingStones.Rows[i].Cells[1].OwningColumn;
                    // this.dgvExistingDesignItemDetail.Rows[i].Cells[1].Value = items[i].DesignItem.DesignNo.ToString();
                }
            }

        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StoneDetail_Load(object sender, EventArgs e)
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
            this.ShowStoneCutRecord();
            this.ShowStoneClearityRecord();
            this.ShowStoneNameRecord();
            this.ShowColorNameRecord();
            this.cbxStoneType.DataSource = sDAL.GetAllStoneTypeName();
            this.cbxStoneType.DisplayMember = "TypeName";
            this.cbxStoneType.ValueMember = "TypeId";
            this.cbxStoneType.SelectedIndex = -1;
            this.cbxStoneType.Select();
        }

        private void dgvExistingStones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                k = Convert.ToInt32(this.dgvExistingStones.Rows[e.RowIndex].Cells[2].Value);
                this.txtStoneId.Text = k.ToString();
                str = this.dgvExistingStones.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.ShowStoneName(k);
                this.btnEdit.Text = "&Update";
                this.btnSave.Enabled = false;
            }
        }

        private void ShowStoneName(int id)
        {
            stone = sDAL.GetStNameById(id);
            if (stone == null)
                return;
            else
            {
                this.cbxStoneType.SelectedValue = stone.StoneTypeId;
                this.txtStoneName.Text = stone.StoneName.ToString();
            }
        }

        private void ShowColorName(int id)
        {
            sColor=sDAL.GetClrNameById(id);
            if (sColor == null)
                return;
            else
            {
                this.txtStoneColour.Text = sColor.ColorName.ToString();
            }

        }

        private void ShowCutName(int id)
        {
            sCut = sDAL.GetCutNameById(id);
            if (sCut == null)
                return;
            else
            {
                this.txtDiamondCut.Text = sCut.CutName.ToString();
            }
        }

        private void ShowClearityName(int id)
        {
            clearity = sDAL.GetClearityNameById(id);
            if (clearity == null)
                return;
            else
            {
                this.txtDiamondClearity.Text = clearity.ClearityName.ToString();
            }
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
                    bool nFlag = false;
                    stone = new Stones();
                    if (this.cbxStoneType.Text != string.Empty)
                    {
                        string str = txtStoneName.Text.ToString();
                        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                        TextInfo textInfo = cultureInfo.TextInfo;
                        stone.StoneName = textInfo.ToTitleCase(str);
                        stone.StoneTypeId = (int)this.cbxStoneType.SelectedValue;
                        //nFlag = sDAL.isStNameExist(stone.StoneName, stone.StoneTypeId);
                        //if (nFlag == true)
                        //{
                        //    MessageBox.Show("Name already exist", Messages.Header);
                        //    return;
                        //}
                        //else
                            sDAL.UpdateStoneName(k, stone);
                        MessageBox.Show("Stone Name is Updated successfully", Messages.Header);
                        this.ShowStoneNameRecord();
                        this.txtStoneName.Text = "";
                        this.btnSave.Enabled = true;
                        this.btnEdit.Text = "&Edit";
                    }
                    else
                    {
                        MessageBox.Show("Please Select Stone Type");
                        return;
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
                     bool nFlag = false;
                     sColor = new StoneColor();

                     string str = txtStoneColour.Text.ToString();
                     CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                     TextInfo textInfo = cultureInfo.TextInfo;
                     sColor.ColorName = textInfo.ToTitleCase(str);
                     nFlag = sDAL.isClrNameExist(sColor.ColorName);
                     if (nFlag == true)
                     {
                         MessageBox.Show("Name already exist", Messages.Header);
                         return;
                     }
                     else
                     sDAL.UpdateColorName(k,sColor);

                     MessageBox.Show("Color is Updated successfully", Messages.Header);
                     this.ShowColorNameRecord();
                     this.txtStoneColour.Text = "";
                     this.btnSave.Enabled = true;
                     this.btnEdit.Text = "&Edit";
                 }

            }
            if (tabControl1.SelectedTab == tabPage3)
            {
                 if (k == 0)
                {
                    MessageBox.Show("Please select the record to update", Messages.Header);
                    return;
                }
                 if (btnEdit.Text == "&Update")
                 {
                     bool nFlag = false;
                     sCut = new StoneCut();

                     string str = txtDiamondCut.Text.ToString();
                     CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                     TextInfo textInfo = cultureInfo.TextInfo;
                     sCut.CutName = textInfo.ToTitleCase(str);
                     nFlag = sDAL.isCutNameExist(sCut.CutName);
                     if (nFlag == true)
                     {
                         MessageBox.Show("Name already exist", Messages.Header);
                         return;
                     }
                     else
                     sDAL.UpdateCutName(k,sCut);
                     MessageBox.Show("Record is Updated successfully ", Messages.Header);
                     this.ShowStoneCutRecord();
                     this.txtDiamondCut.Text = "";
                     this.btnSave.Enabled = true;
                     this.btnEdit.Text = "&Edit";
                 }

            }
            if (tabControl1.SelectedTab == tabPage4)
            {
                 if (k == 0)
                {
                    MessageBox.Show("Please select the record to update", Messages.Header);
                    return;
                }
                 if (btnEdit.Text == "&Update")
                 {
                     bool nFlag = false;
                     clearity = new StoneClearity();

                     string str = txtDiamondClearity.Text.ToString();
                     CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                     TextInfo textInfo = cultureInfo.TextInfo;
                     clearity.ClearityName = textInfo.ToTitleCase(str);
                     nFlag = sDAL.isCutNameExist(clearity.ClearityName);
                     if (nFlag == true)
                     {
                         MessageBox.Show("Name already exist", Messages.Header);
                         return;
                     }
                     else
                     sDAL.UpdateClearityName(k,clearity);
                     MessageBox.Show("Record is Updated successfully", Messages.Header);
                     this.ShowStoneClearityRecord();
                     this.txtDiamondClearity.Text = "";
                     this.btnSave.Enabled = true;
                     this.btnEdit.Text = "&Edit";
                 }
            }
        }

        private void dgvStoneColor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                k = Convert.ToInt32(this.dgvStoneColor.Rows[e.RowIndex].Cells[2].Value);
                str = this.dgvStoneColor.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.ShowColorName(k);
                this.btnEdit.Text = "&Update";
                this.btnSave.Enabled = false;
            }
        }

        private void dgvDiamondCut_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                k = Convert.ToInt32(this.dgvDiamondCut.Rows[e.RowIndex].Cells[2].Value);
                str = this.dgvDiamondCut.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.ShowCutName(k);
                this.btnEdit.Text = "&Update";
                this.btnSave.Enabled = false;
            }
        }

        private void dgvDiamondClearity_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                k = Convert.ToInt32(this.dgvDiamondClearity.Rows[e.RowIndex].Cells[2].Value);
                str = this.dgvDiamondClearity.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.ShowClearityName(k);
                this.btnEdit.Text = "&Update";
                this.btnSave.Enabled = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (str == null)
                {
                    MessageBox.Show("Please select the record to delete", Messages.Header);
                    return;
                }
                else
                {
                    bool sFlag,cFlag = true;
                    sFlag = sDAL.isStoneIdExistinStonesDetail(Convert.ToInt32(this.txtStoneId.Text));
                    cFlag = sDAL.isStoneIdExistCostingStonesDetail(Convert.ToInt32(this.txtStoneId.Text));
                    if (sFlag == true || cFlag == true )
                    {
                        MessageBox.Show("You can not delete this record it is already in use", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure to delete this record:Press Ok To Continue", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            stone = new Stones();
                            sDAL.DeleteStoneName(str);
                            MessageBox.Show("Record deleted Successfully", Messages.Header);
                            this.dgvExistingStones.Rows.Clear();
                            this.ShowStoneNameRecord();
                            this.btnEdit.Text = "&Edit";
                            this.btnSave.Enabled = true;
                            this.btnDelete.Enabled = true;
                            this.txtStoneName.Text = "";

                        }
                    }
                }
            }
            if (tabControl1.SelectedTab == tabPage2)
            {
                if (str == null)
                {
                    MessageBox.Show("Please select the record to delete", Messages.Header);
                    return;
                }
                else
                {
                    bool nFlag = false;
                    nFlag = sDAL.isColorNameExist(str);
                    if (nFlag == true)
                    {
                        MessageBox.Show("You can not delete this record it is already in use", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure to delete this record:Press Ok To Continue", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            stone = new Stones();
                            sDAL.DeleteColorName(str);
                            MessageBox.Show("Record deleted Successfully", Messages.Header);
                            this.dgvStoneColor.Rows.Clear();
                            this.ShowColorNameRecord();
                            this.btnEdit.Text = "&Edit";
                            this.btnSave.Enabled = true;
                            this.btnDelete.Enabled = true;
                            this.txtStoneColour.Text = "";

                        }
                    }
                }
            }
            if (tabControl1.SelectedTab == tabPage3)
            {
                if (str == null)
                {
                    MessageBox.Show("Please select the record to delete", Messages.Header);
                    return;
                }
                else
                {
                    bool nFlag = false;
                    nFlag = sDAL.isCuttNameExist(str);
                    if (nFlag == true)
                    {
                        MessageBox.Show("You can not delete this record it is already in use", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure to delete this record:Press Ok To Continue", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            stone = new Stones();
                            sDAL.DeleteCutName(str);
                            MessageBox.Show("Record deleted Successfully", Messages.Header);
                            this.dgvDiamondCut.Rows.Clear();
                            this.ShowStoneCutRecord();
                            this.btnEdit.Text = "&Edit";
                            this.btnSave.Enabled = true;
                            this.btnDelete.Enabled = true;
                            this.txtDiamondCut.Text = "";



                          
                        }
                    }
                }
            }
            if (tabControl1.SelectedTab == tabPage4)
            {
                if (str == null)
                {
                    MessageBox.Show("Please select the record to delete", Messages.Header);
                    return;
                }
                else
                {
                    bool nFlag = false;
                    nFlag = sDAL.isClearNameExist(str);
                    if (nFlag == true)
                    {
                        MessageBox.Show("You can not delete this record it is already in use", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("Are you sure to delete this record:Press Ok To Continue", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            stone = new Stones();
                            sDAL.DeleteClearityName(str);
                            MessageBox.Show("Record deleted Successfully", Messages.Header);
                            this.dgvDiamondClearity.Rows.Clear();
                            this.ShowStoneClearityRecord();
                            this.btnEdit.Text = "&Edit";
                            this.btnSave.Enabled = true;
                            this.btnDelete.Enabled = true;
                            this.txtDiamondClearity.Text = "";

                        }
                    }
                }
            }
            str = null;
        }

        private void txtStoneColour_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32)
            //    e.Handled = true;
            //else
            //    e.Handled = false;
        }

        private void txtStoneName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtDiamondCut_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32)
            //    e.Handled = true;
            //else
            //    e.Handled = false;
        }

        private void txtDiamondClearity_KeyPress(object sender, KeyPressEventArgs e)
        {
            //    if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32)
            //        e.Handled = true;
            //    else
            //        e.Handled = false;
            //}
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
            this.btnEdit.Text = "Edit";
            this.txtDiamondClearity.Text = "";
            this.txtDiamondCut.Text = "";
            this.txtStoneColour.Text = "";
            this.txtStoneName.Text = "";

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
            this.btnEdit.Text = "Edit";
            this.txtDiamondClearity.Text = "";
            this.txtDiamondCut.Text = "";
            this.txtStoneColour.Text = "";
            this.txtStoneName.Text = "";
        }

        private void StoneDetail_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void cbxStoneType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtStoneName.Select();
            }
        }

        private void txtStoneName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxStoneType);
        }

        private void cbxStoneType_Validating(object sender, CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(cbxStoneType.Text))
            //{
            //    e.Cancel = true;
            //    cbxStoneType.Focus();
            //    errorProvider1.SetError(cbxStoneType, "Stone Type" + Messages.Empty);
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(cbxStoneType, "");
            //}
        }

        private void txtStoneName_Validating(object sender, CancelEventArgs e)
        {
            //FormControls.validate_box(txtStoneName, e, errorProvider1, "Stone Name");
        }

        private void dgvExistingStones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            this.txtStoneColour.Select();
        }
    }
}
