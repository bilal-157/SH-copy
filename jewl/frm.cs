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
    public partial class frm : Form
    {
        string splitDesc = "";
        public frm()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Maximized;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frm_Load(object sender, EventArgs e)
        {

        }

        private void btnReason_Click(object sender, EventArgs e)
        {
            frmSplit frm = new frmSplit();
            frm.ShowDialog();
            splitDesc = frm.desc;
        }

        private void cbxItemType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
