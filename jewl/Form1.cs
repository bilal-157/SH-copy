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
using System.Diagnostics;
using Microsoft.VisualBasic;
//using System.Drawing;
using System.IO;
using System.Globalization;

namespace jewl
{
    
    public partial class Form1 : Form
    {

        StockDAL stkDAL = new StockDAL();
        Formulas frm = new Formulas();
        public Form1()
        {
            InitializeComponent();
        }

     
        private void button1_Click(object sender, EventArgs e)
        {
            //decimal  kat =Convert .ToDecimal  (this.textBox2.Text);
            //frm.KaatInRatti(kat, Convert.ToDecimal(this.textBox4.Text), textBox3, label1);
           // frm.RatiMashaToala(Convert.ToDecimal(this.textBox1.Text), label1);

            Interaction.Shell("C:\\Program Files (x86)\\DAP\\DAP.exe", (AppWinStyle)2, false, -1);
            
        }

        //private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    Formulas frm = new Formulas();
        //    frm.RatiMashaToala(Convert.ToDecimal(this.textBox1.Text), label1);
        //}

        private bool NonCharEnter = false;
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            this.textBox1.BackColor = Color.Red;
            this.textBox1.ForeColor = Color.Gold ;
            NonCharEnter =false ;
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    if (e.KeyCode == Keys.Decimal)
                    {
                        if (e.KeyCode != Keys.Back)
                        {
                            NonCharEnter = true;
                        }
                        //decimal val = e.KeyValue;
                    }
                }
            }
            
            
            //Formulas frm = new Formulas();
            //frm.RatiMashaToala(Convert.ToDecimal(this.textBox1.Text), label1);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (NonCharEnter == true)
            {
                e.Handled = true;
            }
           // else e.Handled = false;
            // int ln = Convert .ToInt32 ( this.textBox1.Text.Length);e.KeyChar.ToString () + this.textBox1.Text+
            string str;
            if (e.KeyChar == '\b')
            {
                
                str = this.textBox1.Text;
                int i = str.Length;
               str= str.Remove(i - 1);
               if (str==string .Empty )
               {
                   decimal  val1 =0;
                   frm.RatiMashaTola(val1, label1);
                   return;
               } 
            }
            else
                str = this.textBox1.Text + e.KeyChar.ToString();
            //str.Replace(str, str);
            decimal  val = Convert .ToDecimal (str);
            frm.RatiMashaTola(val , label1);
            
        }
        //public static void Mai()
        //{
        //    try
        //    {
        //        Console.WriteLine("-->Your Current Directory");
        //        String curDir = Directory.GetCurrentDirectory();
        //        Console.WriteLine(curDir);
        //        String newDir = "TestDir";
        //        Directory.CreateDirectory(newDir);
        //        Console.WriteLine("--> '" + newDir + "' Directory created");
        //        Directory.SetCurrentDirectory(newDir);
        //        Console.WriteLine("--> Changing to '" + newDir +
        //                          "'  Directory ");
        //        Console.WriteLine(Directory.GetCurrentDirectory());
        //        Console.WriteLine("--> Changing to '" +
        //                          curDir + "' Directory ");
        //        Directory.SetCurrentDirectory(curDir);
        //        Console.WriteLine(Directory.GetCurrentDirectory());
        //        Console.WriteLine("--> Deleting  '" + newDir + "' Directory ");
        //        if (Directory.Exists(newDir))
        //            Directory.Delete(newDir);
        //        Console.WriteLine("--> Checking  '" + newDir +
        //                          "' Directory Exists or not");

        //        if (!Directory.Exists(newDir))
        //            Console.WriteLine("'" + newDir + "' Does not exists ");
        //    }
        //    catch (IOException e) { Console.WriteLine(e.ToString()); }
        //}
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath );
            //DirectoryInfo dir=new DirectoryInfo (path );
            //DirectoryInfo [] dirSub=dir .GetDirectories (path );

            DirectoryInfo dir1 = new DirectoryInfo(path);

            MessageBox.Show("root =" + dir1.Root);
           // string reqDir=
           
            //string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            //string caminho = System.Environment.CurrentDirectory;

            //MessageBox.Show(caminho);
           
            
        //string strFullPath = @"F:\Back Up\Projects\Jewel Manager\jewl\Picture";
        //string strDirName;
        //int intLocation, intLength;

        //intLength = strFullPath.Length;
        //intLocation = strFullPath.IndexOf("Picture");

        //strDirName = strFullPath.Substring(0, intLocation);
        //MessageBox.Show(strDirName);
            //string path = @"F:\Back Up\Projects\Jewel Manager\jewl\Picture";
            //string path = System.IO.Path.GetDirectoryName();
            //string[] files = new DirectoryInfo(path).Parent;
            
            //////string tagNo = "br0004";
            //////string dirPath = Directory.GetParent("../../../").FullName;
            ////////String curDir = Directory.GetCurrentDirectory();
            //////String newDir = "Picture";
            //////Directory.SetCurrentDirectory(dirPath);
            ////////string newPath = dirPath + "/" + tagNo;
            //////MessageBox.Show(dirPath);
            //////Directory.SetCurrentDirectory(newDir);
            //////String  dir = Directory.GetCurrentDirectory();
            //////DirectoryInfo folder = new DirectoryInfo(dir);
            //DirectoryInfo[] dirsub = folder.GetDirectories();
            //foreach (DirectoryInfo df in dirsub)
            //{
            //    if (df.Name == tagNo)
            //        MessageBox.Show("folder found name=" + df.Name);
            //    FileInfo[] files = df.GetFiles();
            //    for (int i = 0; i < files.Length; i++)
            //    {
            //        int j = i + 1;
            //        string picName = tagNo + j.ToString();
            //        string filename = files[i].Name ;
            //        string picPath=files[i].FullName ;
            //        if (i == 0)
            //        {
            //            pictureBox1.Image = Image.FromFile(picPath);
            //        }
            //        else 
            //        {
            //            pictureBox2.Image = Image.FromFile(picPath);
            //        }
            //        //if (filename == picName+".jpg")
            //        //{
            //        //    MessageBox.Show("file found" + filename);
            //        //    pictureBox1.Image = Image.FromFile(picPath);
            //        //}
 
            //    }
            //}
            //MessageBox.Show(dir);
            //MessageBox.Show(Directory.GetCurrentDirectory().ToString());
            //MessageBox.Show(newDir);
            //bool bFlag = false ;
            
            //DirectoryInfo folder = new DirectoryInfo(dirPath);
            //DirectoryInfo[] dirSubs = folder.GetDirectories();
            //if (folder.Exists)
            //{

                

               
            //    foreach (DirectoryInfo fName in dirSubs)
            //    {
            //        if (fName.Name == tagNo)
            //        {
            //            bFlag = true;
            //        }
            //    }
            //    if (!(bFlag))
            //    {
                    
                  
            //       DirectoryInfo f= folder.CreateSubdirectory(tagNo);
                 

                    


            //    }
            //    else
            //    {
            //        MessageBox.Show("Folder is already exists");
            //    }
              

                

            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //label2.Text = "*" + textBox5.Text + "*";
            Formulas frm = new Formulas();
            frm.RatiMashaTolaGeneral(Convert.ToDecimal(textBox1.Text));
            MessageBox.Show(frm.Tola.ToString() + "T-" + frm.Masha.ToString() + "M-" + frm.Ratti.ToString() + "R");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {


            string tagNo = "br0004";
            string dirPath = Directory.GetParent("../../../").FullName;
            
            
            //String curDir = Directory.GetCurrentDirectory();
            String newDir = "Picture";
            Directory.SetCurrentDirectory(dirPath);
            //string newPath = dirPath + "/" + tagNo;
            MessageBox.Show(dirPath);
            Directory.SetCurrentDirectory(newDir);
            String dir = Directory.GetCurrentDirectory();
            DirectoryInfo folder = new DirectoryInfo(dir);
            DirectoryInfo[] dirsub = folder.GetDirectories();
            foreach (DirectoryInfo df in dirsub)
            {
                if (df.Name == tagNo)
                    MessageBox.Show("folder found name=" + df.Name);
                string folderPath = df.FullName;
                string fileName=folderPath +"1";
                string file=tagNo +"3";
                pictureBox1.Image.Save(file, System.Drawing.Imaging.ImageFormat.Jpeg);
               // FileInfo[] files = df.GetFiles();
                //for (int i = 1; i < files.Length; i++)
                //{
                //    string picName = tagNo + i.ToString();
                //    string filename = files[i].FullName;
                //    if (filename == picName)
                //    {
                //        MessageBox.Show("file found" + filename);
                //    }
                //    // FileInfo file=files [i].f


                //}
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.pictureBox1.Image = Image.FromFile(this.openFileDialog1.FileName);
                this.pictureBox1.BorderStyle = BorderStyle.None;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ItemDAL idal = new ItemDAL();
            idal.GetAllItemByType("Gold");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AccountDAL adal = new AccountDAL();
            //adal.CreateGroup(1);
          //  adal.CreatSubGroupAccountCode("1-01", 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string str = "imran paracha";
            TextInfo txtinfo = CultureInfo.CurrentCulture.TextInfo;
            MessageBox.Show(txtinfo.ToTitleCase(str));
            //GoldRateDAL dD = new GoldRateDAL();
            //DateTime dt = DateTime.Today;
            //string val = "23";
            //dD.GetRateByKarat(val, dt);
        }
    }
}
