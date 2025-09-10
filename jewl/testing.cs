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
    public partial class testing : Form
    {
        public testing()
        {
            InitializeComponent();
        }

        private void testing_Load(object sender, EventArgs e)
        {
            HaversineDistance(100, 200, 300, 400);
        }
        private decimal HaversineDistance(decimal firstLat, decimal firstLong, decimal secondLat, decimal secondLong)
        {
            decimal dLat = this.toRadian(secondLat - firstLat);
            decimal dLon = this.toRadian(secondLong - firstLong);

            //decimal a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            //Math.Cos(this.toRadian(firstLat)) * Math.Cos(this.toRadian(secondLat)) *
            //Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            //decimal c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            decimal d = 6371 * 2; //* Math.Asin(Math.Min(1, Math.Sqrt(a)));
            return d;

        }

        private decimal toRadian(decimal val)
        {
            return 10;//(Math.PI / 180) * val;
        }

        static void Main11(string[] args)
        {
            //decimal dist;
            //Program1 a = new Program1();
            //dist = a.HaversineDistance(28.6100, 77.2300, 26.8470, 80.9470);
            //Console.WriteLine("Harversine Distance  = " + dist);
            //Console.ReadKey();
        }
    }
}
