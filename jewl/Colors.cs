using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jewl;

namespace BusinesEntities
{
    public class MyColors 
    {
       
        public Rgb FormBorder { get { return new Rgb { Red = 0, Green = 188, Blue = 212, Radius = 6, FontSize = 9 }; } }
        
        public Rgb BtnTextColor { get { return new Rgb { Red = 0, Green = 0, Blue = 0, Radius = 0, FontSize = 9 }; } }
        public Rgb BtnBorderColor { get { return new Rgb { Red = 0, Green = 188, Blue = 212, Radius = 0 }; } }
        public Rgb ComboBoxBackColor { get { return new Rgb { Red = 255, Green = 255, Blue = 255, Radius = 0 }; } }

        public Rgb BtnColor { get { return new Rgb { Red = 255, Green = 255, Blue = 255, Radius = 0, BorderSize = 2 }; } }
        public Rgb BtnHover { get { return new Rgb { Red = 175, Green = 238, Blue = 238 }; } }
        public Rgb TitleLabel { get { return new Rgb { Red = 0, Green = 188, Blue = 212 }; } }
        public Rgb SimpleLabel { get { return new Rgb { Red = 0, Green = 188, Blue = 212, FontSize = 10 }; } }

        public Rgb TextBoxBackground { get { return new Rgb { Red = 255, Green = 255, Blue = 255 }; } }
        public Rgb TextBoxBorder { get { return new Rgb { Red = 171, Green = 173, Blue = 179, Radius = 0 }; } }
        public Rgb TextBoxHover
        {
            get
            {
                return new Rgb { Red = 126, Green = 180, Blue = 234, Radius = 0 };
            }
        }
        public Rgb FormBackground
        {
            get { return new Rgb { Red = 255, Green = 255, Blue = 255, Radius = 0 }; }

        }
        public Rgb TabActive { get { return new Rgb { Red = 190, Green = 170, Blue = 160, Radius = 0 }; } }
        public Rgb TabNotActive { get { return new Rgb { Red = 239, Green = 235, Blue = 224 }; } }
        public Rgb TextBoxDisabled { get { return new Rgb { Red = 255, Green = 222, Blue = 173 }; } }
        public Rgb GridViewBackground { get { return new Rgb { Red = 224, Green = 255, Blue = 255, Radius = 0 }; } }
        public Rgb GridViewRow { get { return new Rgb { Red = 255, Green = 222, Blue = 173 }; } }
        public Rgb PanelBackGround
        {
            get
            {
                return new Rgb { Red = 255, Green = 255, Blue = 255, Radius = 0 };
            }
        }
        public Rgb PanelBorder { get { return new Rgb { Red = 221, Green = 221, Blue = 221, Radius = 1 }; } }
        public Rgb GroupBox { get { return new Rgb { Red = 255, Green = 255, Blue = 255, Radius = 0 }; } }

        public Rgb Heading { get { return new Rgb { Red = 0, Green = 188, Blue = 212 }; } }


    }



    public class Rgb
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Radius { get; set; }
        public int BorderSize { get; set; }

        public decimal FontSize { get; set; }

    }


}
