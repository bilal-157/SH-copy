using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class EditOrdderNo : Form
    {
          SaleDAL slDAL = new SaleDAL();
          public EditOrdderNo()
        {
            InitializeComponent();
        }

        private int editNum = 0;

        public int EditNum
        {
            get { return editNum; }
            set { editNum = value; }
        }

        public string LabelText
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;
            }
        }
        private string msg;
        public string Msg
        {
            get
            {
                return this.msg;
            }
            set
            {
                this.msg = value;
            }
        }

        private void txtEditNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnEnter_Click(sender, e);
            }  
        }

        private void EditOrdderNo_Load(object sender, EventArgs e)
        {

        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
             if (this.txtEditNo.Text == "")
            {
                MessageBox.Show(msg, Messages.Header);
                this.txtEditNo.Focus();
                return;
            }
            
            else
            {
                //int SaleNo = slDAL.GetSaleNoByBillBookNo(this.txtEditNo.Text);
               // if (SaleNo == 0)
                {
                    //MessageBox.Show("SaleNo not found");
                   // this.Close();
                }
               // else
                   // this.editNum = SaleNo;
                    this.editNum = Convert.ToInt32(this.txtEditNo.Text);
                this.Close();
            }
        }
    }
}
