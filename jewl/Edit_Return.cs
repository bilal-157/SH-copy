using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace jewl
{
    public partial class Edit_Return : Form
    {
        public DateTime Df;
        public DateTime Dt;
        public Edit_Return()
        {
           
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void btnVeiw_Click(object sender, EventArgs e)
        {
            if (this.rbtEdit.Checked == true)
            {
                frmEdit_ReturnRpt frmerRpt = new frmEdit_ReturnRpt();
                frmerRpt.id = 1;
                frmerRpt.ShowDialog();
            }
            if (this.rbtReturn.Checked == true)
            {
                frmEdit_ReturnRpt frmerRpt = new frmEdit_ReturnRpt();
                frmerRpt.Df = Convert.ToDateTime(this.dtpFrom.Value);
                frmerRpt.Dt = Convert.ToDateTime(this.dtpTo.Value);
                frmerRpt.id = 2;
                frmerRpt.ShowDialog();
            }
        }

        private void Edit_Return_Load(object sender, EventArgs e)
        {
            this.lbldateFrom.Visible = false;
            this.lbldateTo.Visible = false;
            this.dtpFrom.Visible = false;
            this.dtpTo.Visible = false;
        }

        private void rbtReturn_CheckedChanged(object sender, EventArgs e)
        {
            this.lbldateFrom.Visible = true;
            this.lbldateTo.Visible = true;
            this.dtpFrom.Visible = true;
            this.dtpTo.Visible = true;
        }

        private void rbtEdit_CheckedChanged(object sender, EventArgs e)
        {
            this.lbldateFrom.Visible = false;
            this.lbldateTo.Visible = false;
            this.dtpFrom.Visible = false;
            this.dtpTo.Visible = false;
        }
    }
}
