using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSphere
{
    class Point
    {
        public double px { get; set; } 
        public double py { get; set;}
        public double pz { get; set;}
        public int coordinate = 1;

        public Point(double px, double py, double pz)
        {
            this.px = px;
            this.py = py;
            this.pz = pz;
        }
    }
}
