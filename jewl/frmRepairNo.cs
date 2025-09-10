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
    public partial class frmRepairNo : Form
    {
        public frmRepairNo()
        {
            InitializeComponent();
        }

        private int repairNo;

        public int RepairNo
        {
            get { return repairNo; }
            set { repairNo = value; }
        }
        

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtRepairNo.Text == "")
            {
                //MessageBox.Show("No Repair No Entered ","");
                this.Close();
                //return;
            }
            else
                this.repairNo = Convert.ToInt32(this.txtRepairNo.Text);
            
                this.Close();

        }

    }
}
