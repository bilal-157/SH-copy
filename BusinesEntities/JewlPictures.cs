using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;


namespace BusinesEntities
{
  public   class JewlPictures
    {
      public int PicId { get; set; }
      public string PicName { get; set; }
      public string PicPath { get; set; }
      public Image PicImg { get; set; }
      public string TagNo { get; set; }
      
      public JewlPictures() { }
      public JewlPictures(int id, string picname)
      {
          this.PicId = id;
          this.PicName = picname;
      }
      public JewlPictures(int id, string name, string path)
      {
          this.PicId = id;
          this.PicName = name;
          this.PicPath = path;
      }
      public void saveImage(string tagNo, string name, Image img,string dirPath)
      {
          bool bFlag =true ;
         // string folderName;
          DirectoryInfo folder = new DirectoryInfo(dirPath);
          if (folder.Exists)
          { 
              
              string[] Folder = Directory .GetDirectories(dirPath);
              //for(int i=0;i<Folder .Length ;i++)
              //{

              // // string name=Folder [i].ToString ();
              //}
              foreach ( string  folderName in Folder)
              {
                  
                  if (folderName == tagNo)
                  {
                      bFlag = false;
                      
                  }
                  else
                      bFlag = true;
              }
              if (bFlag)
              {
                  folder.CreateSubdirectory(tagNo);


              }
          }

         
 
      }
      
  }
}
