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
    public partial class EditNo : Form
    {
        SaleDAL slDAL = new SaleDAL(); 
        public EditNo()
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
        private void btnEnter_Click(object sender, EventArgs e)
        {
            
            if (this.txtEditNo.Text == "")
            {
                MessageBox.Show(msg,Messages.Header);
                this.txtEditNo.Focus();
                return;
            }
            
            else
            {
                if (this.rbtBillBookNo.Checked == true)
                {
                    int SaleNo = slDAL.GetSaleNoByBillBookNo(this.txtEditNo.Text);
                    if (SaleNo == 0)
                    {
                        //MessageBox.Show("SaleNo not found");
                        this.Close();
                    }
                    else
                        this.editNum = SaleNo;
                }
                else
                {
                    this.editNum = Convert.ToInt32(this.txtEditNo.Text);
                }
                this.Close();
            }
        }

        private void txtOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode ==Keys.Enter )
            {
                this.btnEnter_Click(sender, e);
            }            
        }

        private void EditNo_Load(object sender, EventArgs e)
        {

        }       
    }
}
