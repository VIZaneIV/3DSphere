using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSphere
{
    class Texture
    {
        public double U { get; set; }
        public double V { get; set; }

        public Texture(double U,double V)
        {
            this.U = U;
            this.V = V;
        }
    }
}
