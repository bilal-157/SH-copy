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
using System.Globalization;

namespace jewl
{
    public partial class ManageWorker : Form
    {
        Worker wrk, wrk1;

        WorkerDAL wrkDAL = new WorkerDAL();
        VouchersDAL vDAL = new VouchersDAL();
        List<Worker> wrks;
        ParentAccount p;
        ChildAccount c;
        Voucher vchr = new Voucher();
        AccountDAL acDAL = new AccountDAL();
        int wrkid = 0;
        public ManageWorker()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }
        private void Save_Click(object sender, EventArgs e)
        {
            bool dflag = false;
            bool cflag = false;
            bool nflag = false;
            if (txtName.Text == "")
            {
                MessageBox.Show("Please Enter Worker Name", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtName.Select();
                return;
            }
            //if (this.txtContactNo.Text == "")
            //{
            //    MessageBox.Show("Please Enter ContactNo", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.txtContactNo.Select();
            //    return;
            //}
            //if (this.txtAddress.Text == "")
            //{
            //    MessageBox.Show("Please Enter Address", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.txtAddress.Select();
            //    return;
            //}
             nflag = wrkDAL.IsWorkerNameExist(this.txtName.Text);
            if (nflag == true)
            {
                MessageBox.Show("Worker name already exist", Messages.Header);
                this.txtName.Select();
                return;
            }
            cflag = wrkDAL.IsWorkerMobileExist(this.txtContactNo.Text);
            if (cflag == true)
            {
                MessageBox.Show("Mobile No Already Exist", Messages.Header);
                this.txtContactNo.Select();
                return;
            }
            dflag = wrkDAL.IsWorkerAddressExist(this.txtAddress.Text);
            //if (dflag == true)
            //{
            //    MessageBox.Show("Worker Address Already Exist", Messages.Header);
            //    this.txtAddress.Select();
            //    return;
            //}
            //else
            {
            wrk = new Worker();
            c = new ChildAccount();

            wrk.Name = this.txtName.Text.ToString();
            wrk.Address = this.txtAddress.Text.ToString();
            wrk.ContactNo = this.txtContactNo.Text.ToString();
            if (this.txtMaking.Text == "")
                wrk.MakingTola = 0;
            else
                wrk.MakingTola = Convert.ToDecimal(this.txtMaking.Text);
            if (this.txtEmail.Text != string.Empty)
            {
                string email = this.txtEmail.Text;
                if (email.Contains('@'))
                    wrk.Email = this.txtEmail.Text.ToString();
                else
                {
                    MessageBox.Show("Enter correct Email address", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txtEmail.Focus();
                    return;
                }
            }
            wrk.Email = this.txtEmail.Text.ToString();
           
            if (this.txtCheejad.Text == "")
                wrk.Cheejad = 0;
            else
                wrk.Cheejad = Convert.ToDecimal(this.txtCheejad.Text);
            wrk.TKarrat = Convert.ToDecimal(this.cbxTransKarrat.Text);

            if (this.txtOpeningBalance.Text != string.Empty)
            {
                if (this.rbtDr.Checked)
                    wrk.OpeningCash = Convert.ToDecimal(this.txtOpeningBalance.Text);
                else if (this.rbtCr.Checked)
                    wrk.OpeningCash = -Convert.ToDecimal(this.txtOpeningBalance.Text);
                else
                    wrk.OpeningCash = 0;
            }
            else
                wrk.OpeningCash = 0;

            if (this.txtOpGold.Text != string.Empty)
            {
                if (this.rbtOpDebit.Checked)
                    wrk.OpeningGold= Convert.ToDecimal(this.txtOpGold.Text);
                else if (this.rbtOpCredit.Checked)
                    wrk.OpeningGold = -Convert.ToDecimal(this.txtOpGold.Text);
                else
                    wrk.OpeningGold = 0;
            }
            else
                wrk.OpeningGold = 0;

            c.ChildName = wrk.Name;
            c.OpCash = wrk.OpeningCash;
            c.OpGold = wrk.OpeningGold;


            wrk.AccountCode = acDAL.CreateAccount(2, "Worker", c, "Worker");
                wrkDAL.AddWorker(wrk);
                MessageBox.Show("Record is saved successfully");
                btnReset_Click(sender, e);
                this.ShowWorkerRecord();
                this.txtName.Select();
            }
        }
        private void ShowWorkerRecord()
        {
            wrks = wrkDAL.GetAllWorkers();
            if (wrks == null)
                return;
            else
            {
                this.dgvWorkerInformation.AutoGenerateColumns = false;
                this.dgvWorkerInformation.Rows.Clear();
                int count = wrks.Count;
                this.dgvWorkerInformation.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvWorkerInformation.Rows[i].Cells[0].Value = wrks[i].ID.ToString();
                    this.dgvWorkerInformation.Rows[i].Cells[1].Value = wrks[i].Name.ToString();
                    this.dgvWorkerInformation.Rows[i].Cells[2].Value = wrks[i].MakingTola.ToString();
                    this.dgvWorkerInformation.Rows[i].Cells[3].Value = wrks[i].ContactNo.ToString();
                    this.dgvWorkerInformation.Rows[i].Cells[4].Value = wrks[i].Address.ToString();
                    this.dgvWorkerInformation.Rows[i].Cells[5].Value = wrks[i].Email.ToString();
                    this.dgvWorkerInformation.Rows[i].Cells[6].Value = wrks[i].TKarrat.ToString();
                    this.dgvWorkerInformation.Rows[i].Cells[7].Value = wrks[i].AccountCode.ToString();
                }
            }
        }
        private void ManageWorker_Load(object sender, EventArgs e)
        {
            this.ShowWorkerRecord();
            cbxTransKarrat.SelectedIndex = 2;
            this.txtName.Select();

        }
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Edit_Click(object sender, EventArgs e)
        {
            if (this.btnEdit.Text == "&Update")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please Enter Worker Name", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                wrk = new Worker();

                wrk.Name = this.txtName.Text.ToString();
                wrk.Address = this.txtAddress.Text.ToString();
                wrk.ContactNo = this.txtContactNo.Text.ToString();
                if (this.txtMaking.Text == "")
                    wrk.MakingTola = 0;
                else
                    wrk.MakingTola = Convert.ToDecimal(this.txtMaking.Text);
                wrk.Email = this.txtEmail.Text.ToString();
                //foreach(Worker w in wrks)
                //{
                //    if (wrkid == w.ID)
                //        wrk.AccountCode = w.AccountCode;
                //}
                wrk.AccountCode = wrk1.AccountCode;
                if (this.txtCheejad.Text == "")
                    wrk.Cheejad = 0;
                else
                    wrk.Cheejad = Convert.ToDecimal(this.txtCheejad.Text);
                wrk.TKarrat = Convert.ToDecimal(this.cbxTransKarrat.Text);

                if (this.txtOpeningBalance.Text != string.Empty)
                {
                    if (this.rbtDr.Checked)
                        wrk.OpeningCash = Convert.ToDecimal(this.txtOpeningBalance.Text);
                    else if (this.rbtCr.Checked)
                        wrk.OpeningCash = -Convert.ToDecimal(this.txtOpeningBalance.Text);
                    else
                        wrk.OpeningCash = 0;
                }
                else
                    wrk.OpeningCash = 0;

                if (this.txtOpGold.Text != string.Empty)
                {
                    if (this.rbtOpDebit.Checked)
                        wrk.OpeningGold = Convert.ToDecimal(this.txtOpGold.Text);
                    else if (this.rbtOpCredit.Checked)
                        wrk.OpeningGold = -Convert.ToDecimal(this.txtOpGold.Text);
                    else
                        wrk.OpeningGold = 0;
                }
                else
                    wrk.OpeningGold = 0;
                //wrk.OpeningCash = Convert.ToDecimal(this.txtOpeningBalance.Text);
                //wrk.OpeningGold = Convert.ToDecimal(this.txtOpGold.Text);
                wrkDAL.UpdateWorker(wrkid, wrk);
                MessageBox.Show("Record updated successfully", Messages.Header);
                btnReset_Click(sender, e);
                this.ShowWorkerRecord();
            }
        }

        private void RefreshRecord()
        {
            this.txtName.Text = "";
            this.txtContactNo.Text = "";
            this.txtEmail.Text = "";
            this.txtMaking.Text = "";
            this.txtAddress.Text = "";
            this.txtCheejad.Text = "";
            this.btnSave.Enabled = true;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.txtName.Select();
        }
        private void ShowRecord(int id)
        {
            wrk1 = wrkDAL.GetWorkerById(id);
            if (wrk1 == null)
                return;
            else
            {
                this.txtName.Text = wrk1.Name.ToString();
                this.txtContactNo.Text = wrk1.ContactNo.ToString();
                this.txtEmail.Text = wrk1.Email.ToString();
                if (wrk1.MakingTola == 0)
                    this.txtMaking.Text = string.Empty;
                else
                    this.txtMaking.Text = wrk1.MakingTola.ToString();
                this.txtAddress.Text = wrk1.Address.ToString();
                if (wrk1.Cheejad == 0)
                    this.txtCheejad.Text = string.Empty;
                else
                    this.txtCheejad.Text = wrk1.Cheejad.ToString();
                this.cbxTransKarrat.Text = wrk1.TKarrat.ToString();
                if (wrk1.OpeningCash < 0)
                {
                    this.rbtCr.Checked = true;
                }
                if (wrk1.OpeningCash > 0)
                {
                    this.rbtDr.Checked = true;
                }
                if (wrk1.OpeningGold < 0)
                {
                    this.rbtOpCredit.Checked = true;
                }
                if (wrk1.OpeningGold > 0)
                {
                    this.rbtOpDebit.Checked = true;
                }
                this.txtOpeningBalance.Text = wrk1.OpeningCash.ToString();
                this.txtOpGold.Text = wrk1.OpeningGold.ToString();
                this.txtAccountCode.Text = wrk1.AccountCode.ToString();
            }
        }
        private void txtMaking_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtMaking.Text != "")
            {
                this.txtCheejad.Text = "";
                this.txtCheejad.Enabled = false;
            }
            if (this.txtMaking.Text == "")
            {
                this.txtCheejad.Enabled = true;
            }
        }
        private void txtCheejad_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.txtCheejad.Text != "")
            {
                this.txtMaking.Text = "";
                this.txtMaking.Enabled = false;
            }
            if (this.txtCheejad.Text == "")
            {
                this.txtMaking.Enabled = true;
            }
        }
        private void txtMaking_Leave(object sender, EventArgs e)
        {
            this.txtCheejad.Enabled = true;
        }
        private void dgvWorkerInformation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;
                else
                {
                    string str = this.dgvWorkerInformation.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (!(str == null || str == ""))
                    {
                        wrkid = Convert.ToInt32(this.dgvWorkerInformation.Rows[e.RowIndex].Cells[0].Value.ToString());
                        this.ShowRecord(wrkid);
                        this.btnEdit.Text = "&Update";
                        this.btnEdit.Enabled = true;
                        this.btnSave.Enabled = false;
                        this.btnDelete.Enabled = true;
                    }
                }
            }
            catch { }
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
                //else if (a(I) == "Delete")
                //{
                //    frm.cmdDelete.Enabled = true;
                //}
                //else if (a(I) == "Print")
                //{
                //    frm.CmdPrint.Enabled = true;
                //    frm.CmdReport.Enabled = true;
                //}
                //else if (a(I) == "Estimate")
                //{
                //    frm.CmdEstimated.Enabled = true;
                //}
                //else if (a(I) == "Sample")
                //{
                //    frm.CmdSample.Enabled = true;
                //}
                //else if (a(I) == "Damage")
                //{
                //    frm.CmdDamage.Enabled = true;
                //}

            }


        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32 && Convert.ToInt16(e.KeyChar) != 46)
            //    e.Handled = true;
            //else
            //    e.Handled = false;

            //if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;

            //}
            //if (Convert.ToInt16(e.KeyChar) == 32 && (sender as TextBox).Text.IndexOf(' ') > -1)
            //{
            //    e.Handled = true;
            //}
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
        //    CultureInfo culInfo = CultureInfo.CurrentCulture;
        //    TextInfo txtInfo = culInfo.TextInfo;
        //    string str = this.txtName.Text;
        //    this.txtName.Text = txtInfo.ToTitleCase(str);
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
            //    e.Handled = true;
            //else
            //    e.Handled = false;
        }

        private void txtRefrence_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32 && Convert.ToInt16(e.KeyChar) != 46)
            //    e.Handled = true;
            //else
            //    e.Handled = false;

            //if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;

            //}
            //if (Convert.ToInt16(e.KeyChar) == 32 && (sender as TextBox).Text.IndexOf(' ') > -1)
            //{
            //    e.Handled = true;
            //}
        }

        private void txtRefrence_Leave(object sender, EventArgs e)
        {
            CultureInfo culInfo = CultureInfo.CurrentCulture;
            TextInfo txtInfo = culInfo.TextInfo;
            string str = this.txtEmail.Text;
            this.txtEmail.Text = txtInfo.ToTitleCase(str);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if (!(string.IsNullOrEmpty(this.txtName.Text)))
            {
                bool dFlag = wrkDAL.IsWorkernameAnytableExist(this.wrkid);
                if (dFlag == true)
                {
                    MessageBox.Show("You Can not Delete This Record", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (this.txtName.Text == "")
                {
                    MessageBox.Show("Select Record To Delete", Messages.Header);
                    return;
                }
                else
                {

                    wrkDAL.DeleteWorker(this.txtAccountCode.Text);
                    MessageBox.Show("Record is Deleted", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnReset_Click(sender, e);
                    //this.dgvWorkerInformation.Rows.Clear();
                    this.ShowDataGrid();

                }
            }
        }

        private void ShowDataGrid()
        {
            wrks = wrkDAL.GetAllWorkers();
            if (wrks == null)
            {
                this.dgvWorkerInformation.Rows.Clear();
                return;
            }
            else
            {
                this.dgvWorkerInformation.AutoGenerateColumns = false;
                int count = wrks.Count;
                this.dgvWorkerInformation.Rows.Clear();
                this.dgvWorkerInformation.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvWorkerInformation.Rows[i].Cells[0].Value = wrks[i].Name.ToString();
                    this.dgvWorkerInformation.Rows[i].Cells[1].Value = Convert.ToDecimal(wrks[i].MakingTola.ToString());
                    this.dgvWorkerInformation.Rows[i].Cells[2].Value = Convert.ToInt32(wrks[i].ContactNo);
                    this.dgvWorkerInformation.Rows[i].Cells[3].Value = wrks[i].Address.ToString();
                    this.dgvWorkerInformation.Rows[i].Cells[4].Value = wrks[i].Refernce.ToString();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void AddWorker_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtContactNo);
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtAddress);
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtMaking);
        }

        private void txtMaking_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtCheejad);
        }

        private void txtCheejad_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtEmail);
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxTransKarrat);
        }

        private void cbxTransKarrat_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtOpeningBalance);
        }

        private void txtOpeningBalance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtOpGold.Focus();
            }
        }

        private void txtOpGold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSave.Select();
            }
        }

        private void rbtDr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSave.Select();
            }
        }

        private void rbtCr_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtOpGold);
        }

        private void rbtOpCredit_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void rbtOpDebit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.rbtOpCredit.Focus();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            FormControls.CrudRefresh(btnSave, btnEdit, btnDelete);
            IEnumerable<Control> list = FormControls.GetAll(this, typeof(TextBox));
            FormControls.Refresh(list);
            this.rbtCr.Checked = false;
            this.rbtDr.Checked = false;
            this.rbtOpDebit.Checked = false;
            this.rbtOpCredit.Checked = false;
        }

        private void txtMaking_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtCheejad_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtOpeningBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtOpGold_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }
    }
}

