using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSphere
{
    class Vertice
    {
        public Point point;
        public Normal normal;
        public Texture texture;

        public Vertice (Point point, Normal normal)
        {
            this.point = point;
            this.normal = normal;
        }
    }
}
