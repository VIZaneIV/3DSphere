using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSphere
{
    class Triangle
    {
        public Vertice vertice1 {get;set; }
        public Vertice vertice2 { get; set; }
        public Vertice vertice3 { get; set; }
        public Normal normal { get; set; }

        public Triangle(Vertice vertice1, Vertice vertice2, Vertice vertice3)
        {
            this.vertice1 = vertice1;
            this.vertice2 = vertice2;
            this.vertice3 = vertice3;

            Vector3 u = new Vector3(vertice1.point.px - vertice2.point.px,
                                     vertice1.point.py - vertice2.point.py,
                                     vertice1.point.pz - vertice2.point.pz);
            Vector3 v = new Vector3(vertice3.point.px - vertice2.point.px,
                                     vertice3.point.py - vertice2.point.py,
                                     vertice3.point.pz - vertice2.point.pz);

            this.normal = new Normal(v.Y * u.Z - v.Z * u.Y,
                                     v.Z * u.X - v.X * u.Z,
                                     v.X * u.Y - v.Y * u.X);
        }
    }
}
