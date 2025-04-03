using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSphere
{
    class Edge
    {
        public int yMin;
        public int yMax;
        public double xMin;
        public int xMax;
        public double dydx;

        public Edge(int yMin, int yMax, int xMin, int xMax, double dydx)
        {
            this.yMin = yMin;
            this.yMax = yMax;
            this.xMin = xMin;
            this.xMax = xMax;
            this.dydx = dydx;
        }
    }
}
