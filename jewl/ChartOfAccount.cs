using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BusinesEntities;
using jewl;

namespace jewl
{
    public partial class ChartOfAccount : Form
    {
        public ChartOfAccount()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }
        AccountDAL aDAL = new AccountDAL();
        TreeNode parentNode = null;
        string Str = "";
        public int Check;
        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();

        private void ChartOfAccount_Load(object sender, EventArgs e)
        {
            this.Reload();
        }
        public void Reload()
        {
            //tabPage.TabPages.Remove(tbpAsset1);
            //tabPage.TabPages.Remove(tbpLiability2);
            //tabPage.TabPages.Remove(tbpCapital5);
            tabPage.SizeMode = TabSizeMode.Fixed;
          //  tabPage.ItemSize = new Size(
          //tabPage.Width - tabPage.TabPages[0].Controls[0].Width - tabPage.Padding.X,
          //tabPage.ItemSize.Height);
            tabPage.ItemSize = new Size((tabPage.Width - 24) / tabPage.TabCount, 0);
            //tabPage.Padding = new System.Drawing.Point(95, 3);
            this.tvAsset.Nodes.Clear();
            this.tvLiability.Nodes.Clear();
            this.tvRevenue.Nodes.Clear();
            this.tvExpense.Nodes.Clear();
            this.tvCapital.Nodes.Clear();
            List<ParentAccount> gpl = aDAL.GetParentByHeadCode(1);
            if (gpl == null)
            {
                parentNode = new TreeNode("Create Group");
                tvAsset.Nodes.Add(parentNode);
            }
            else
            {
                this.FillTreeview(tvAsset, 1);

            }
            List<ParentAccount> l = aDAL.GetParentByHeadCode(2);
            if (l == null)
            {
                parentNode = new TreeNode("Create Group");
                tvLiability.Nodes.Add(parentNode);
            }
            else
            {
                this.FillTreeview(tvLiability, 2);

            }
            List<ParentAccount> m = aDAL.GetParentByHeadCode(3);
            if (m == null)
            {
                parentNode = new TreeNode("Create Group");
                tvExpense.Nodes.Add(parentNode);
            }
            else
            {
                this.FillTreeview(tvExpense, 3);

            }
            List<ParentAccount> r = aDAL.GetParentByHeadCode(4);
            if (r == null)
            {
                parentNode = new TreeNode("Create Group");
                tvRevenue.Nodes.Add(parentNode);
            }
            else
            {
                this.FillTreeview(tvRevenue, 4);

            }
            List<ParentAccount> c = aDAL.GetParentByHeadCode(5);
            if (c == null)
            {
                parentNode = new TreeNode("Create Group");
                tvCapital.Nodes.Add(parentNode);
            }
            else
            {
                this.FillTreeview(tvCapital, 5);

            }

        }
        private void tvAsset_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Check = 1;
                string str = tvAsset.SelectedNode.Text;
                string[] str2 = str.Split(' ');
                string word = str2[0];
                if (tvAsset.SelectedNode.Text.Contains("Create"))
                {
                    ManageParent frm = new ManageParent();
                    frm.pcode = word;
                    frm.headcode = Check;
                    frm.ShowDialog(this);
                }               
                if (word.Length == 5)
                {
                    ManageParent frm = new ManageParent();
                    frm.pcode = word;
                    frm.headcode = Check;
                    int Lenghtof = str2.Length;
                    for (int i = 1; i < Lenghtof; i++)
                    {
                        Str = Str + str2[i] + " ";
                    }
                    frm.ParentName = Str;
                    Str = "";
                    frm.deleteChk = aDAL.DeleteCheck("select DeleteCheck from ParentAccount where ParentCode='" + frm.pcode + "'and HeadCode='" + frm.headcode + "'");
                    frm.ShowDialog(this);
                }
                if (word.Length == 11)
                {
                    ManageChild frm = new ManageChild();
                    frm.Accountcode = word;
                    int Lenghtof = str2.Length;
                    for (int i = 1; i < Lenghtof; i++)
                    {
                        Str = Str + str2[i] + " ";
                    }
                    frm.ChildName = Str;
                    Str = "";
                    frm.deleteChk = aDAL.DeleteCheck("select DeleteCheck from ChildAccount where ChildCode='" + frm.Accountcode + "'");
                    frm.ShowDialog(this);
                }
            }
            catch { }
           
            
        }
        private void  FillTreeview(TreeView tv, int code)
        {
            List<ParentAccount> pAccounts = aDAL.GetParentByHeadCode(code);
            if (pAccounts != null)
            {
                foreach (ParentAccount p in pAccounts)
                {
                    TreeNode node = new TreeNode();
                    node.Text = p.ParentCode.ToString() + " " + p.ParentName.ToString();
                    tv.Nodes.Add(node);
                    List<ChildAccount> childs = aDAL.GetAllChildAccounts("select ca.*, ((select isnull(sum(OpeningCash), 0) from ChildAccount where ChildCode = ca.ChildCode) - (select isnull(sum(dr) - sum(cr), 0) from vouchers where AccountCode = ca.ChildCode)) as Balance from ChildAccount ca  where ca.ParentCode='" + p.ParentCode + "' order by ChildName");
                    if (childs != null)
                    {
                        foreach (ChildAccount c in childs)
                        {
                            TreeNode node1 = new TreeNode();
                            node1.Text = c.ChildCode.ToString() + " " + c.ChildName.ToString();
                            node.Nodes.Add(node1);
                        }
                    }
                }
            }
         
        }

        private void tvLiability_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Check = 2;
                string str = tvLiability.SelectedNode.Text;
                string[] str2 = str.Split(' ');
                string word = str2[0];
                if (tvLiability.SelectedNode.Text.Contains("Create"))
                {
                    ManageParent frm = new ManageParent();
                    frm.pcode = word;
                    frm.headcode = Check;
                    frm.ShowDialog(this);
                }

                if (word.Length == 5)
                {
                    ManageParent frm = new ManageParent();
                    frm.pcode = word;
                    frm.headcode = Check;
                    int Lenghtof = str2.Length;
                    for (int i = 1; i < Lenghtof; i++)
                    {
                        Str = Str + str2[i] + " ";
                    }
                    frm.ParentName = Str;
                    Str = "";
                    frm.ShowDialog(this);
                }
                if (word.Length == 11)
                {
                    ManageChild frm = new ManageChild();
                    frm.Accountcode = word;
                    int Lenghtof = str2.Length;
                    for (int i = 1; i < Lenghtof; i++)
                    {
                        Str = Str + str2[i] + " ";
                    }
                    frm.ChildName = Str;
                    Str = "";
                    frm.ShowDialog(this);
                    //EditGroupAccount frm = new EditGroupAccount();
                    //frm.GName = str;
                    //frm.Code = Check;
                    ////frm.Parent = this;
                    //frm.ShowDialog(this);
                }

            }
            catch { }
        }

        private void tvExpense_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Check = 3;
                string str = tvExpense.SelectedNode.Text;
                string[] str2 = str.Split(' ');
                string word = str2[0];
                if (tvExpense.SelectedNode.Text.Contains("Create"))
                {
                    ManageParent frm = new ManageParent();
                    frm.pcode = word;
                    frm.headcode = Check;
                    frm.ShowDialog(this);
                }

                if (word.Length == 5)
                {
                    ManageParent frm = new ManageParent();
                    frm.pcode = word;
                    frm.headcode = Check;
                    int Lenghtof = str2.Length;
                    for (int i = 1; i < Lenghtof; i++)
                    {
                        Str = Str + str2[i] + " ";
                    }
                    frm.ParentName = Str;
                    Str = "";
                    frm.ShowDialog(this);
                }
                if (word.Length == 11)
                {
                    ManageChild frm = new ManageChild();
                    frm.Accountcode = word;
                    int Lenghtof = str2.Length;
                    for (int i = 1; i < Lenghtof; i++)
                    {
                        Str = Str + str2[i] + " ";
                    }
                    frm.ChildName = Str;
                    Str = "";
                    frm.ShowDialog(this);
                }
            }
            catch { }
        }

        private void tvRevenue_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Check = 4;
                string str = tvRevenue.SelectedNode.Text;
                string[] str2 = str.Split(' ');
                string word = str2[0];
                if (tvRevenue.SelectedNode.Text.Contains("Create"))
                {
                    ManageParent frm = new ManageParent();
                    frm.pcode = word;
                    frm.headcode = Check;
                    frm.ShowDialog(this);
                }
                
                if (word.Length == 5)
                {
                    ManageParent frm = new ManageParent();
                    frm.pcode = word;
                    frm.headcode = Check;
                    int Lenghtof = str2.Length;
                    for (int i = 1; i < Lenghtof; i++)
                    {
                        Str = Str + str2[i] + " ";
                    }
                    frm.ParentName = Str;
                    Str = "";
                    frm.ShowDialog(this);
                }
                if (word.Length == 11)
                {
                    ManageChild frm = new ManageChild();
                    frm.Accountcode = word;
                    int Lenghtof = str2.Length;
                    for (int i = 1; i < Lenghtof; i++)
                    {
                        Str = Str + str2[i] + " ";
                    }
                    frm.ChildName = Str;
                    Str = "";
                    frm.ShowDialog(this);
                }
            }
            catch { }

        }
        private void tvCapital_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Check = 5;
                string str = tvCapital.SelectedNode.Text;
                string[] str2 = str.Split(' ');
                string word = str2[0];
                if (tvCapital.SelectedNode.Text.Contains("Create"))
                {
                    ManageParent frm = new ManageParent();
                    frm.pcode = word;
                    frm.headcode = Check;
                    frm.ShowDialog(this);
                }
                if (word.Length == 5)
                {
                    ManageParent frm = new ManageParent();
                    frm.pcode = word;
                    frm.headcode = Check;
                    int Lenghtof = str2.Length;
                    for (int i = 1; i < Lenghtof; i++)
                    {
                        Str = Str + str2[i] + " ";
                    }
                    frm.ParentName = Str;
                    Str = "";
                    frm.ShowDialog(this);
                }
                if (word.Length == 11)
                {
                    ManageChild frm = new ManageChild();
                    frm.Accountcode = word;
                    int Lenghtof = str2.Length;
                    for (int i = 1; i < Lenghtof; i++)
                    {
                        Str = Str + str2[i] + " ";
                    }
                    frm.ChildName = Str;
                    Str = "";
                    frm.ShowDialog(this);
                }
            }
            catch { }
        }

        private void tabPage_DrawItem(object sender, DrawItemEventArgs e)
        {
                                 
        }

        private void ChartOfAccount_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }
    }
}
