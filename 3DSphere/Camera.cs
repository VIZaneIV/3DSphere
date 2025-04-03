using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSphere
{
    class Camera
    {
        public double positionX, positionY, positionZ;
        public Vector3 direction;

        Vector3 cPos;
        Vector3 cTarget;
        Vector3 cUp = Vector3.UnitY;

        Vector3 cX;
        Vector3 cY;
        Vector3 cZ;

        public double[,] viewMatrix = new double[4, 4];

        public Camera(double x, double y, double z)
        {
            positionX = x;
            positionY = y;
            positionZ = z;
            direction = new Vector3(-positionX, -positionY, -positionZ);
            cTarget = new Vector3(0, 0, 0);
            cPos = new Vector3(x, y, z);
            AdjustCValues();
            CreateViewMatrix();
            return;
        }

        private void CreateViewMatrix()
        {
            viewMatrix[0, 0] = cX.X;
            viewMatrix[0, 1] = cX.Y;
            viewMatrix[0, 2] = cX.Z;
            viewMatrix[0, 3] = cX.Dot(cPos);

            viewMatrix[1, 0] = cY.X;
            viewMatrix[1, 1] = cY.Y;
            viewMatrix[1, 2] = cY.Z;
            viewMatrix[1, 3] = cY.Dot(cPos);

            viewMatrix[2, 0] = cZ.X;
            viewMatrix[2, 1] = cZ.Y;
            viewMatrix[2, 2] = cZ.Z;
            viewMatrix[2, 3] = cZ.Dot(cPos);

            viewMatrix[3, 0] = 0;
            viewMatrix[3, 1] = 0;
            viewMatrix[3, 2] = 0;
            viewMatrix[3, 3] = 1;
        }

        private void AdjustCValues()
        {
            cZ = cPos.Minus(cTarget).Divide(cPos.Minus(cTarget).Length());
            cX = cUp.Cross(cZ).Divide(cUp.Cross(cZ).Length());
            cY = cZ.Cross(cX).Divide(cZ.Cross(cX).Length());
        }

        public void Move(double xAngle = 0, double yAngle = 0, double zAngle = 0)
        {
            double theta;
            if(xAngle != 0)
            {
                theta = xAngle;
                positionY = positionY * Math.Cos(theta) - positionZ * Math.Sin(theta);
                positionZ = positionY * Math.Sin(theta) + positionZ * Math.Cos(theta);
            }
            else if(yAngle != 0)
            {
                theta = yAngle;
                positionX = positionX * Math.Cos(theta) + positionZ * Math.Sin(theta);
                positionZ = -positionX * Math.Sin(theta) + positionZ * Math.Cos(theta);
            }
            else if(zAngle != 0)
            {
                theta = zAngle;
                positionX = positionX * Math.Cos(theta) - positionY * Math.Sin(theta);
                positionY = positionX * Math.Sin(theta) + positionY * Math.Cos(theta);
            }
            direction = new Vector3(-positionX, -positionY, -positionZ);
            cPos = new Vector3(positionX, positionY, positionZ);
            AdjustCValues();
            CreateViewMatrix();
        }
    }
}
