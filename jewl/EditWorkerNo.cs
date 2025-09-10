using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
namespace jewl
{
    public partial class EditWorkerNo : Form
    {
        public EditWorkerNo()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void EditWorkerNo_Load(object sender, EventArgs e)
        {

        }
        public int editNum = 0;

        public int EditNum
        {
            get { return editNum; }
            set { editNum = value; }
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (this.txtEditNo.Text == "")
            {
                MessageBox.Show("Must Enter No", Messages.Header);
                this.txtEditNo.Focus();
                return;
            }

            else
            {
                this.editNum = Convert.ToInt32(this.txtEditNo.Text);
                this.Close();
            }
        }

        private void txtEditNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnEnter_Click(sender, e);
            }    
        }
    }
}
