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
using System.IO.Ports;
using System.Threading;
using System.Data.SqlClient;


namespace jewl
{
    public partial class SendSMS : Form
    {

        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        SqlCommand cmd;
        int t = 0, cnt = 0;
        int u = 160, v = 640;
        string s, lang, message;
        List<Customer> custs;
        List<Customer> selCusts;
        CustomerDAL custDAL = new CustomerDAL();
        Customer cust;
        clsSMS objclsSMS = new clsSMS();
        SerialPort port = new SerialPort();
        //ListItem[] item = new ListItem[1000];
        public SendSMS()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.txtMessage.Multiline = true;
            this.txtUrduMessage.Multiline = true;

        }

        private void SendSMS_Load(object sender, EventArgs e)
        {
            this.lblSMSCounter.Text = CRHelper.GetSMSCount().ToString();
            this.label2.Text = u.ToString();
            this.label6.Text = t.ToString();
            //this.lbxCustomersList.DataSource = custDAL.GetAllCustomer();
            //this.lbxCustomersList.DisplayMember = "Name";
            //this.lbxCustomersList.ValueMember = "ID";
            this.CustomerList();
            this.rbtEnglish.Checked = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Object temp = this.lbxCustomersList.SelectedItem;
            if (temp != null)
            {
                this.lbxCustomersSelected.Items.Add(temp);
                this.lbxCustomersList.Items.Remove(temp);
            }
            this.lbxCustomersSelected.DisplayMember = "Name";
            this.lbxCustomersSelected.ValueMember = "ID";
            this.label7.Text = this.lbxCustomersList.Items.Count.ToString();
            this.label8.Text = this.lbxCustomersSelected.Items.Count.ToString();
        }

        private void btnDeselect_Click(object sender, EventArgs e)
        {
            Object temp = this.lbxCustomersSelected.SelectedItem;
            if (temp != null)
            {
                this.lbxCustomersList.Items.Add(temp);
                this.lbxCustomersSelected.Items.Remove(temp);
            }
            this.label8.Text = this.lbxCustomersSelected.Items.Count.ToString();
            this.label7.Text = this.lbxCustomersList.Items.Count.ToString();
        }

        private void txtCustomerName_KeyUp(object sender, KeyEventArgs e)
        {
            string str = (this.txtCustomerName.Text);
            //this.lbxCustomersList.DataSource = custDAL.GetAllCustByName(this.txtCustomerName.Text);
            //this.lbxCustomersList.DisplayMember = "Name";
            //this.lbxCustomersList.ValueMember = "ID";

            this.lbxCustomersList.Items.Clear();
            if (this.chkName.Checked == true)
            {
                custs = custDAL.GetAllCustByNameForSMS(str);
            }
            if (this.chkMobile.Checked == true)
            {
                custs = custDAL.GetAllCustByMobileForSMS(str);
            }
            if (custs == null)
                return;
            else
            {
                foreach (Customer cust in this.custs)
                {
                    lbxCustomersList.Items.Clear();
                    lbxCustomersList.Items.Add(cust);
                }
                lbxCustomersList.DisplayMember = "Name";
                lbxCustomersList.ValueMember = "ID";
            }
            if (this.txtCustomerName.Text == "")
            {
                this.CustomerList();
                //custs = custDAL.GetAllCustomer();
                //if (custs == null)
                //    return;
                //else
                //{
                //    foreach (Customer cust in this.custs)
                //    {                       
                //        this.lbxCustomersList.Items.Add(cust);
                //    }
                //    this.lbxCustomersList.DisplayMember = "Name";
                //    this.lbxCustomersList.ValueMember = "ID";
                //}
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            lbxCustomersList.SelectionMode = SelectionMode.MultiExtended;
            //List<Object> lstObjects = new List<object>();
            custs = new List<Customer>();
            for (int i = 0; i < lbxCustomersList.Items.Count; i++)
            {
                lbxCustomersList.SetSelected(i, true);
                Customer temp = new Customer();
                temp = (Customer)lbxCustomersList.SelectedItems[i];
                custs.Add(temp);
            }
            foreach (Customer cust in this.custs)
            {
                lbxCustomersSelected.Items.Add(cust);
                lbxCustomersList.Items.Remove(cust);
            }
            lbxCustomersSelected.DisplayMember = "Name";
            lbxCustomersSelected.ValueMember = "ID";
            this.label7.Text = this.lbxCustomersList.Items.Count.ToString();
            this.label8.Text = this.lbxCustomersSelected.Items.Count.ToString();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            lbxCustomersSelected.SelectionMode = SelectionMode.MultiExtended;
            //List<Object> lstObjects = new List<object>();
            custs = new List<Customer>();
            for (int i = 0; i < lbxCustomersSelected.Items.Count; i++)
            {
                lbxCustomersSelected.SetSelected(i, true);
                Customer temp = new Customer();
                temp = (Customer)lbxCustomersSelected.SelectedItems[i];
                custs.Add(temp);
            }
            foreach (Customer cust in this.custs)
            {
                lbxCustomersList.Items.Add(cust);
                lbxCustomersSelected.Items.Remove(cust);
            }
            lbxCustomersList.DisplayMember = "Name";
            lbxCustomersList.ValueMember = "ID";
            this.label8.Text = this.lbxCustomersSelected.Items.Count.ToString();
            this.label7.Text = this.lbxCustomersList.Items.Count.ToString();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //lbxCustomersSelected.SelectionMode = SelectionMode.MultiExtended;
            //selCusts = new List<Customer>();
            //for (int i = 0; i < lbxCustomersSelected.Items.Count; i++)
            //{
            //    lbxCustomersSelected.SetSelected(i, true);
            //    Customer temp = new Customer();
            //    temp = (Customer)lbxCustomersSelected.SelectedItems[i];
            //    selCusts.Add(temp);
            //}

            //foreach (Customer cust in this.selCusts)
            //{               
            //    objclsSMS.SendSMS(cust.Mobile, this.txtMessage.Text);
            //    Thread.Sleep(5000);       
            //}
            //MessageBox.Show("Process completed", Messages.Header);

            lbxCustomersSelected.SelectionMode = SelectionMode.MultiExtended;
            selCusts = new List<Customer>();
            for (int i = 0; i < lbxCustomersSelected.Items.Count; i++)
            {
                lbxCustomersSelected.SetSelected(i, true);
                Customer temp = new Customer();
                temp = (Customer)lbxCustomersSelected.SelectedItems[i];
                selCusts.Add(temp);
            }
            con.Open();
            if (rbtEnglish.Checked == true)
            {
                message = this.txtMessage.Text;
                lang = this.rbtEnglish.Text;
            }
            if (rbtUrdu.Checked == true)
            {
                message = this.txtUrduMessage.Text;
                lang = this.rbtUrdu.Text;
            }
            foreach (Customer cust in this.selCusts)
            {
                try
                {
                    CRHelper.SendSMS(message, cust.Mobile, lang, "AlfalahJwlr");
                    cmd = new SqlCommand(@"insert into CustomerSMS values ('" + cust.ID + "','" + cust.Mobile + "','" + DateTime.Now + "')", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), Messages.Header);
                    con.Close();
                    return;
                }
                Thread.Sleep(3000);
                this.lblSMSCounter.Text = CRHelper.GetSMSCount().ToString();
            }
            MessageBox.Show("Process completed", Messages.Header);
            cmd = new SqlCommand(@"truncate table CustomerSMS", con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //private void SMS(string mobNo,string msg)
        //{
        //    string[] Ports = System.IO.Ports.SerialPort.GetPortNames();
        //    foreach (string port in Ports)
        //    {
        //        s = port;
        //        //this.cbxPortName.Items.Add(port);
        //    }
        //    try
        //    {
        //        //Open communication port 
        //        this.port = objclsSMS.OpenPort(s, 9600, 8, 300, 300);

        //        if (this.port != null)
        //        {
        //            //MessageBox.Show("Modem is connected at PORT " + this.textBox1.Text);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Modem is not connected");
        //            //this.statusBar1.Text = "Invalid port settings";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        //ErrorLog(ex.Message);
        //    }
        //    try
        //    {

        //        if (objclsSMS.sendMsg(this.port, mobNo, msg))
        //        {
        //            MessageBox.Show("Message has sent successfully");
        //            //this.statusBar1.Text = "Message has sent successfully";
        //        }
        //        else
        //        {
        //            MessageBox.Show("Failed to send message");
        //            //this.statusBar1.Text = "Failed to send message";
        //        }

        //    }
        //    catch 
        //    {  }
        //}

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str;
            //bool bFlag = false;
            //bFlag = this.KeyCheck(sender, e);
            //if (bFlag == true)
            //    e.Handled = true;

            if (e.Handled == false)
            {
                if (e.KeyChar == '\b')
                {
                    if (this.txtMessage.Text == "")
                        return;
                    str = this.txtMessage.Text;
                    t = str.Length;
                    str = str.Remove(t - 1);
                    this.label6.Text = t.ToString();
                }
                else
                {

                    str = this.txtMessage.Text;
                    t = str.Length;
                    this.label6.Text = t.ToString();

                }
            }

        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            string st = this.txtMessage.Text;
            if (e.KeyCode == Keys.Back)
            {
                if (t == 0)
                    e.SuppressKeyPress = true;
                else
                    e.SuppressKeyPress = false;
            }
            else if (st.Length == u)
            {
                e.SuppressKeyPress = true;
            }
        }

        private bool KeyCheck(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            if (e.KeyChar == 13 || e.KeyChar == 27)
                bFlag = true;
            return bFlag;
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            string str;
            str = this.txtMessage.Text;
            t = str.Length;
            this.label6.Text = t.ToString();
        }

        private void SendSMS_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void rbtEnglish_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtEnglish.Checked == true)
            {
                this.txtMessage.BringToFront();
                this.txtUrduMessage.SendToBack();
                this.chkCharacter.Visible = true;
            }
        }

        private void rbtUrdu_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtUrdu.Checked == true)
            {
                this.txtMessage.SendToBack();
                this.txtUrduMessage.BringToFront();
                this.chkCharacter.Visible = false;
            }
        }

        private void chkCharacter_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCharacter.Checked == true)
                this.label2.Text = v.ToString();
            if (chkCharacter.Checked == false)
                this.label2.Text = u.ToString();
        }

        private void CustomerList()
        {
            custs = custDAL.GetAllCustomerForSMS();
            if (custs == null)
                return;
            else
            {
                foreach (Customer cust in this.custs)
                {
                    this.lbxCustomersList.Items.Add(cust);
                }
                this.lbxCustomersList.DisplayMember = "Name";
                this.lbxCustomersList.ValueMember = "ID";
                this.label7.Text = custs.Count.ToString();
            }
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {

        }

        private void txtUrduMessage_TextChanged(object sender, EventArgs e)
        {
            string str;
            str = this.txtUrduMessage.Text;
            t = str.Length;
            this.label6.Text = t.ToString();
        }

        private void txtUrduMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str;
            //bool bFlag = false;
            //bFlag = this.KeyCheck(sender, e);
            //if (bFlag == true)
            //    e.Handled = true;

            if (e.Handled == false)
            {
                if (e.KeyChar == '\b')
                {
                    if (this.txtUrduMessage.Text == "")
                        return;
                    str = this.txtUrduMessage.Text;
                    t = str.Length;
                    str = str.Remove(t - 1);
                    this.label6.Text = t.ToString();
                }
                else
                {

                    str = this.txtUrduMessage.Text;
                    t = str.Length;
                    this.label6.Text = t.ToString();

                }
            }
        }

        private void txtUrduMessage_KeyDown(object sender, KeyEventArgs e)
        {
            string st = this.txtUrduMessage.Text;
            if (e.KeyCode == Keys.Back)
            {
                if (t == 0)
                    e.SuppressKeyPress = true;
                else
                    e.SuppressKeyPress = false;
            }
            else
            {
                if (chkCharacter.Checked == true)
                {
                    if (st.Length == v)
                        e.SuppressKeyPress = true;
                }
                else //if (st.Length == u)
                {
                    if (st.Length == u)
                        e.SuppressKeyPress = true;
                }
            }
        }

        private void chkName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkName.Checked == true)
            {
                this.chkMobile.Checked = false;
                this.chkName.Checked = true;
            }
            else
            {
                this.chkMobile.Checked = true;
                this.chkName.Checked = false;
            }
        }

        private void chkMobile_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkMobile.Checked == true)
            {
                this.chkMobile.Checked = true;
                this.chkName.Checked = false;
            }
            else
            {
                this.chkMobile.Checked = false;
                this.chkName.Checked = true;
            }
        }
    }
}
