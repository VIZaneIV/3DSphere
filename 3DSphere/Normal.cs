using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSphere
{
    public class Normal
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        int coordinate = 0;

        public Normal(double nx, double ny, double nz)
        {
            this.X = nx;
            this.Y = ny;
            this.Z = nz;
        }
    }
}
