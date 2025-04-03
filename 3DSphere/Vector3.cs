using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSphere
{
    public class Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double Dot(Normal normal)
        {
            return this.X * normal.X + this.Y * normal.Y + this.Z * normal.Z;
        }

        public double Dot(Vector3 vector)
        {
            return this.X * vector.X + this.Y * vector.Y + this.Z * vector.Z;
        }

        public Vector3 Cross(Vector3 vector)
        {
            return new Vector3(this.Y * vector.Z - this.Z * vector.Y,
                                this.Z * vector.X - this.X * vector.Z,
                                this.X * vector.Y - this.Y * vector.X);
        }

        public Vector3 Minus(Vector3 vector)
        {
            return new Vector3(this.X - vector.X,
                                this.Y - vector.Y,
                                this.Z - vector.Z);
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Vector3 Divide(double d)
        {
            return new Vector3(X / d, Y / d, Z / d);
        }

        public static Vector3 Zero => new Vector3(0, 0, 0);
        public static Vector3 UnitX => new Vector3(1, 0, 0);
        public static Vector3 UnitY => new Vector3(0, 1, 0);
        public static Vector3 UnitZ => new Vector3(0, 0, 1);
    }
}
