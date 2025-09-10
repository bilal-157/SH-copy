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
using System.Reflection;
using System.Globalization;


namespace jewl
{
    public partial class CreateUser : Form
    {
        string frmRights;
        string str;
        bool fFlag = false;
        bool eFlag = false;
        int uid = 0;
        Users u;
        List<Users> users; 
        UserDAL uDAL = new UserDAL();

        public CreateUser()
        {
            InitializeComponent();
            u = new Users();
            FormControls.GetAllControls(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtUserName.Text == "")
            {
                MessageBox.Show("Must Enter User Name");
                return;
            }
            if (txtPassword.Text != txtVerifyPassword.Text)
                return;
            else
            {
                bool nFlag = false;
                nFlag = uDAL.isNameExist(txtUserName.Text);
                if (nFlag == true)
                {
                    MessageBox.Show("Name already exist", Messages.Header);
                    return;
                }
                else
                {
                    u.UserName = txtUserName.Text.ToString();
                    u.Password = txtPassword.Text.ToString();
                    uDAL.AddUsers(u);
                    MessageBox.Show("User saved successfully", Messages.Header);
                    this.RefreshPage();
                    this.ShowUsers();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (eFlag == true)
            {
                this.CreateRights(sender, e);
                if (fFlag == true)
                    return;
                else
                {
                    object[] values = new Object[2];
                    if (chkAdministrator.Checked == true || chkLimited.Checked == true)
                        values[0] = "";
                    else                        
                        values[0] = str.Substring(2);
                    values[1] = frmRights.ToString();
                    this.dgvUserRights.Rows.Add(values);
                }
            }
            else
            {
                this.CreateRights(sender, e);
                if (fFlag == true)
                    return;
                else
                {
                    object[] values = new Object[2];
                    if (chkAdministrator.Checked == true || chkLimited.Checked == true)
                        values[0] = "";
                    else
                        values[0] = str.Substring(2);
                    values[1] = frmRights.ToString();
                    this.dgvUserRights.Rows.Add(values);
                }
            }
        }

        private void CreateRights(object sender, EventArgs e)
        {
            if (eFlag == true)
            {
                RightsLineItem rli = new RightsLineItem();
                str = (string)this.cbxForm.SelectedItem;
                for (int i = 0; i < dgvUserRights.Rows.Count; i++)
                {
                    if (str.Substring(2) == this.dgvUserRights.Rows[i].Cells[0].Value.ToString())
                    {
                        MessageBox.Show("Rights Already Defined For This Form ");
                        fFlag = true;
                        return;
                    }
                    else
                    {
                        if (chkAdministrator.Checked == true || chkLimited.Checked == true)
                            rli.FormName = "";
                        else
                            rli.FormName = str.Substring(2);
                        rli.Rights = this.SetRights();
                        u.AddLineItems(rli);
                        return;
                    }
                }
                if (chkAdministrator.Checked == true || chkLimited.Checked == true)
                    rli.FormName = "";
                else
                    rli.FormName = str.Substring(2);
                rli.Rights = this.SetRights();
                u.AddLineItems(rli);
            }
            else
            {
                RightsLineItem rli = new RightsLineItem();
                str = (string)this.cbxForm.SelectedItem;
                for (int i = 0; i < dgvUserRights.Rows.Count; i++)
                {
                    if (str.Substring(2) == this.dgvUserRights.Rows[i].Cells[0].Value.ToString())
                    {
                        MessageBox.Show("Rights Already Defined For This Form");
                        fFlag = true;
                        return;
                    }
                    else
                    {
                        if (chkAdministrator.Checked == true || chkLimited.Checked == true)
                            rli.FormName = "";
                        else
                            rli.FormName = str.Substring(2);
                        rli.Rights = this.SetRights();
                        u.AddLineItems(rli);
                        return;
                    }
                }
                if (chkAdministrator.Checked == true || chkLimited.Checked == true)
                    rli.FormName = "";
                else
                    rli.FormName = str.Substring(2);
                rli.Rights = this.SetRights();
                u.AddLineItems(rli);
            }
        }

        private string SetRights()
        {
            frmRights = "";
            if (chkAdministrator.Checked == true)
                frmRights = "Administrator";

            if (chkLimited.Checked == true)
                frmRights = "Limited";

            if (chkSave.Checked == true)
                frmRights = "Save";
           
            if (chkEdit.Checked == true)
            {
                if (frmRights == "")
                    frmRights = "Edit";
                else
                    frmRights = frmRights + " Edit";
            }
            if (chkDelete.Checked == true)
            {
                if (frmRights == "")
                    frmRights = "Delete";
                else
                    frmRights = frmRights + " Delete";
            }
            if (chkPrint.Checked == true)
            {
                if (frmRights == "")
                    frmRights = "Print";
                else
                    frmRights = frmRights + " Print";
            }
            if (chkView.Checked == true)
            {
                if (frmRights == "")
                    frmRights = "View";
                else
                    frmRights = frmRights + " View";
            }
            return frmRights;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateUser_Load(object sender, EventArgs e)
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
            getForms();
            this.ShowUsers();
            this.txtUserName.Select();
        }

        private void ShowUsers()
        {
            users = uDAL.GetAllUsers();
            if (users == null)
                return;
            else
            {
                this.dgvUserName.AutoGenerateColumns = false;
                this.dgvUserName.Rows.Clear();
                int count = users.Count;
                this.dgvUserName.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvUserName.Rows[i].Cells[0].Value = users[i].UserId.ToString();
                    this.dgvUserName.Rows[i].Cells[1].Value = users[i].UserName.ToString();

                }
            }
        }

        private void dgvUserName_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                if (dgvUserName.Rows[e.RowIndex].Cells[0].Value == null)
                    return;
                else
                {
                    eFlag = true;
                    this.btnSave.Enabled = false;
                    this.btnEdit.Text = "&Update";
                    this.btnDelete.Text = "&Confirm";
                    uid = Convert.ToInt32(dgvUserName.Rows[e.RowIndex].Cells[0].Value);
                    this.lblUserId.Text = uid.ToString();
                    this.ShowUser(uid);                    
                }
            }
        }

        private void ShowUser(int uid)
        {
            u = uDAL.GetUserById(uid);
            this.txtUserName.Text = u.UserName.ToString();
            this.txtPassword.Text = u.Password.ToString();
           
            this.dgvUserRights.AutoGenerateColumns = false;
            this.dgvUserRights.Rows.Clear();
            if (u.RightsLineItem != null && u.RightsLineItem.Count > 0)
            {
                foreach (RightsLineItem rli in u.RightsLineItem)
                {
                    object[] values1 = new Object[4];
                    values1[0] = rli.FormName.ToString();
                    values1[1] = rli.Rights.ToString();

                    this.dgvUserRights.Rows.Add(values1);
                }
            }
        }

        private void dgvUserRights_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                if (eFlag == true)
                {
                    string strTagNo = this.dgvUserRights.Rows[e.RowIndex].Cells[0].Value.ToString();
                    this.dgvUserRights.Rows.RemoveAt(e.RowIndex);
                    RightsLineItem rl = new RightsLineItem();
                    foreach (RightsLineItem rli in u.RightsLineItem)
                    {
                        if (rli.FormName == strTagNo)
                        {
                            this.cbxForm.SelectedItem = ">>" + strTagNo;
                            rl = rli;
                        }
                    }
                    if (rl != null)
                    {
                        u.RemoveLineItems(rl);
                    }
                }
                else
                {
                    string strTagNo = this.dgvUserRights.Rows[e.RowIndex].Cells[0].Value.ToString();
                    this.dgvUserRights.Rows.RemoveAt(e.RowIndex);
                    RightsLineItem rl = new RightsLineItem();
                    foreach (RightsLineItem rli in u.RightsLineItem)
                    {
                        if (rli.FormName == strTagNo)
                        {
                            this.cbxForm.SelectedItem = ">>" + strTagNo;
                            rl = rli;
                        }
                    }
                    if (rl != null)
                    {
                        u.RemoveLineItems(rl);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.lblUserId.Text == "")
            {
                MessageBox.Show("First select a user", Messages.Header);
                return;
            }
            if (this.btnEdit.Text == "&Update")
            {
                bool Mflag = false;
                Mflag = uDAL.NotDeleteJewelManager(u.UserName);
                if (txtPassword.Text != txtVerifyPassword.Text)
                {
                    MessageBox.Show("Enter same password", Messages.Header);
                    return;
                }
                if (u.UserName == "Jewl Manager")
                {
                    MessageBox.Show("You Cant Update this User", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    u.UserId = Convert.ToInt32(lblUserId.Text);
                    u.UserName = txtUserName.Text.ToString(); ;
                    u.Password = txtPassword.Text.ToString();

                    uDAL.UpdateUsers(u);
                    MessageBox.Show("User update successfully", Messages.Header);
                    this.btnEdit.Text = "Edit";
                    this.RefreshPage();
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

        private void RefreshPage()
        {
            this.dgvUserRights.Rows.Clear();
            this.txtPassword.Text = "";
            this.txtVerifyPassword.Text = "";
            this.txtUserName.Text = "";
            this.cbxForm.SelectedIndex = -1;
            this.chkSave.Checked = false;
            this.chkEdit.Checked = false;
            this.chkView.Checked = false;
            this.chkPrint.Checked = false;
            this.chkDelete.Checked = false;
            this.chkWorkerUpdate.Checked = false;
            this.chkRepairOut.Checked = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool dFlag = false;
            dFlag = uDAL.NotDeleteJewelManager(u.UserName);
            if (eFlag == false)
            {
                MessageBox.Show("First select a user", Messages.Header);
                return;
            }
            if (u.UserName == "Jewl Manager")
            {
                MessageBox.Show("You Cant delete this User", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.btnDelete.Text == "&Confirm")
            {
                if (MessageBox.Show("Are you sure to delete this User:Press Yes to continue", Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(lblUserId.Text);
                    uDAL.DeleteUser(id);
                    MessageBox.Show("User deleted successfully", Messages.Header);
                    this.btnEdit.Text = "Edit";
                    this.RefreshPage();
                }
            }
        }

        private void chkAdministrator_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAdministrator.Checked == true)
                this.panel4.Visible = false;
            else
                this.panel4.Visible = true;
        }

        public void getForms()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Form f = new Form();
            List<string> forms = new List<string>();
            List<string> crystalforms = new List<string>();

            foreach (Assembly a in assemblies)
            {
                if (a.FullName.Contains("jewl") == true)
                {
                    Type[] types = a.GetTypes();
                    foreach (Type t in types)
                    {
                        if (t.IsPublic && t.BaseType == typeof(Form))
                        {
                            f = (Form)Activator.CreateInstance(t);
                            foreach (Control c in f.Controls)
                            {
                                if (c is CrystalDecisions.Windows.Forms.CrystalReportViewer)
                                    crystalforms.Add(t.Name);
                            }
                            forms.Add(t.Name);
                        }
                    }
                }
            }
            foreach (string sl in crystalforms)
            {
                forms.Remove(sl);
            }
            this.cbxForm.DataSource = forms;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            CreateUser frm = new CreateUser();
            frm.ShowDialog();
        }
    }
}
