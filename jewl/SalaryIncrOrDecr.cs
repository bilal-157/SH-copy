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

namespace jewl
{
    public partial class SalaryIncrOrDecr : Form
    {
        SaleMan slm;
        SaleMan sm;
        List<SaleMan> slms = new List<SaleMan>();
        SaleManDAL slmDAL = new SaleManDAL();
        public SalaryIncrOrDecr()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void SalaryIncrOrDecr_Load(object sender, EventArgs e)
        {
            this.cbxEmloyeeName.SelectedIndexChanged -= new EventHandler(cbxEmloyeeName_SelectedIndexChanged);
            this.cbxEmloyeeName.DataSource = slmDAL.GetAllSaleMen();
            this.cbxEmloyeeName.DisplayMember = "Name";
            this.cbxEmloyeeName.ValueMember = "ID";
            this.cbxEmloyeeName.SelectedIndex = -1;
            this.rbtIncrement.Checked = true;
        }

        private void cbxEmloyeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sm = (SaleMan)this.cbxEmloyeeName.SelectedItem;
                this.txtSalary.Text = Math.Round((decimal)sm.Salary, 0).ToString();
            }
            catch
            { }
        }

        private void cbxEmloyeeName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxEmloyeeName.SelectedIndexChanged += new EventHandler(cbxEmloyeeName_SelectedIndexChanged);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.cbxEmloyeeName.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an Employee first", Messages.Header);
                return;
            }
            else if (this.txtAmount.Text == "")
            {
                MessageBox.Show("There is no amount to save", Messages.Header);
                return;
            }
            else
            {
                slm = new SaleMan();
                slm.ID = sm.ID;
                if (this.rbtIncrement.Checked == true)
                {
                    slm.IncreDate = this.dtpDate.Value;
                    slm.IncreAmount = Convert.ToDecimal(this.txtAmount.Text);
                    slm.Salary = sm.Salary + slm.IncreAmount;
                }
                if (this.rbtDecrement.Checked == true)
                {
                    slm.DecreDate = this.dtpDate.Value;
                    slm.DecreAmount = Convert.ToDecimal(this.txtAmount.Text);
                    slm.Salary = sm.Salary - slm.DecreAmount;
                }
                slmDAL.AddIncreOrDecre(slm);
                MessageBox.Show("Record saved successfully", Messages.Header);
                this.txtAmount.Text = "";
                this.cbxEmloyeeName.SelectedIndexChanged -= new EventHandler(cbxEmloyeeName_SelectedIndexChanged);
                this.cbxEmloyeeName.DataSource = slmDAL.GetAllSaleMen();
                this.cbxEmloyeeName.DisplayMember = "Name";
                this.cbxEmloyeeName.ValueMember = "ID";
                this.cbxEmloyeeName.SelectedIndex = -1;
                this.txtSalary.Text = "";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
