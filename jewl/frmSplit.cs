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
    public partial class frmSplit : Form
    {
        public string desc = "";
        public frmSplit()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            desc = txtCHW.Text;
            this.Close();
        }

        private void frmSplit_Load(object sender, EventArgs e)
        {
            txtCHW.Select();
        }
    }
}
