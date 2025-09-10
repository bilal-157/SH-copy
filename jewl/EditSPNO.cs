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
    public partial class EditSPNO : Form
    {
        public int PNO = 0;
        public EditSPNO()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            PNO =Convert.ToInt32( this.txtNo.Text);
            this.Close();
        }

        private void EditSPNO_Load(object sender, EventArgs e)
        {

        }
    }
}
