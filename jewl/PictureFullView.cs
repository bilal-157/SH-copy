using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using DAL;
using System.Drawing.Imaging;

namespace jewl
{
    public partial class PictureFullView : Form
    {
        public string tagNo;
        public System.Windows.Forms.PictureBox pbxFullView;
        Stock stock;
        PictureDAL pDAL = new PictureDAL();

        public PictureFullView()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.pbxFullView = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFullView)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxFullView
            // 
            this.pbxFullView.Location = new System.Drawing.Point(0, 2);
            this.pbxFullView.Name = "pbxFullView";
            this.pbxFullView.Size = new System.Drawing.Size(812, 567);
            this.pbxFullView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxFullView.TabIndex = 0;
            this.pbxFullView.TabStop = false;
            // 
            // PictureFullView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(813, 571);
            this.Controls.Add(this.pbxFullView);
            this.Name = "PictureFullView";
            this.Text = "PictureFullView";
            this.Load += new System.EventHandler(this.PictureFullView_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PictureFullView_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PictureFullView_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pbxFullView)).EndInit();
            this.ResumeLayout(false);

        }

        private void ShowPix(string tagNo)
        {
            stock = new Stock();

            stock = pDAL.GetStkPics(tagNo);

                       

            if (stock.ImageMemory == null)
            {
                this.pbxFullView.Image = null;
                this.pbxFullView.BorderStyle = BorderStyle.FixedSingle;
                MessageBox.Show("No Picture to display", "Jewel Manager 1.1");

            }
            else
            {

                MemoryStream mst = new MemoryStream(stock.ImageMemory);
                this.pbxFullView.Image = Image.FromStream(mst);

                this.SetImage(this.pbxFullView);
                //this.pbxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;

            }
           
        }
        //Generate new image dimensions
        public Size GenerateImageDimensions(int currW, int currH, int destW, int destH)
        {
            //decimal to hold the final multiplier to use when scaling the image
            decimal multiplier = 0;

            //string for holding layout
            string layout;

            //determine if it's Portrait or Landscape
            if (currH > currW) layout = "portrait";
            else layout = "landscape";

            switch (layout.ToLower())
            {
                case "portrait":
                    //calculate multiplier on heights
                    if (destH > destW)
                    {
                        if (destW <= 100)
                        {
                            destW = 100;
                            multiplier = (decimal)destW / (decimal)currW;
                        }
                        else
                            multiplier = (decimal)destW / (decimal)currW;
                    }

                    else
                    {
                        if (destH <= 100)
                        {
                            destH = 100;
                            multiplier = (decimal)destH / (decimal)currH;
                        }
                        else
                            multiplier = (decimal)destH / (decimal)currH;
                    }
                    break;
                case "landscape":
                    //calculate multiplier on widths
                    if (destH > destW)
                    {
                        if (destW <= 100)
                        {
                            destW = 100;
                            multiplier = (decimal)destW / (decimal)currW;
                        }
                        else
                            multiplier = (decimal)destW / (decimal)currW;
                    }

                    else
                    {
                        if (destH <= 100)
                        {
                            destH = 100;
                            multiplier = (decimal)destH / (decimal)currH;
                        }
                        else
                            multiplier = (decimal)destH / (decimal)currH;
                    }
                    break;
            }

            //return the new image dimensions
            return new Size((int)(currW * multiplier), (int)(currH * multiplier));
        }
        //Resize the image
        private void SetImage(PictureBox pb)
        {
            try
            {
                //create a temp image
                Image img = pb.Image;

                //calculate the size of the image
                Size imgSize = GenerateImageDimensions(img.Width, img.Height, this.pbxFullView.Width, this.pbxFullView.Height);

                //create a new Bitmap with the proper dimensions
                Bitmap finalImg = new Bitmap(img, imgSize.Width, imgSize.Height);

                //create a new Graphics object from the image
                Graphics gfx = Graphics.FromImage(img);

                //clean up the image (take care of any image loss from resizing)
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //empty the PictureBox
                pb.Image = null;

                //center the new image
                pb.SizeMode = PictureBoxSizeMode.CenterImage;

                //set the new image
                pb.Image = finalImg;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void PictureFullView_Load(object sender, EventArgs e)
        {
            //this.SetImage(this.pbxFullView);
            //this.ShowPix(tagNo);
        }

        private void PictureFullView_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void PictureFullView_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == 43)
            {
                pbxFullView.Size = GenerateImageDimensions(pbxFullView.Width, pbxFullView.Height, pbxFullView.Width + 100, pbxFullView.Height + 100);
                //pbxFullView.Scale(new SizeF(pbxFullView.Width + 100, pbxFullView.Height + 100));
                
            }
            else
            {
                //pbxFullView.Scale(new SizeF(pbxFullView.Width - 100, pbxFullView.Height - 100));
                pbxFullView.Size = GenerateImageDimensions(pbxFullView.Width, pbxFullView.Height, pbxFullView.Width - 100, pbxFullView.Height - 100);
               
            }
        }



    }
}
