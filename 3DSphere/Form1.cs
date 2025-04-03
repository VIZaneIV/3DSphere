using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3DSphere
{
    public partial class Form1 : Form
    {
        Bitmap image;
        double aspectRatio;
        int bitmapCenterX, bitmapCenterY;


        int radius = 100;
        int parallels = 12;
        int meridians = 12;
        Sphere sphere;
        Camera camera;

        double rotationAngle = Math.PI / 3;
        int shiftPixels = 20;

        int brushColor = 0;

        double[,] M = new double[,] { { 1, 0, 0, 0 },
                                      { 0, 1, 0, 0 },
                                      { 0, 0, 0, 0 },
                                      { 0, 0, 0, 1 }};

        public Form1()
        {
            InitializeComponent();

            Bitmap bmp = new Bitmap(Canvas.Width, Canvas.Height);

            Canvas.Image = bmp;
            image = bmp;
            bitmapCenterX = bmp.Width / 2;
            bitmapCenterY = bmp.Height / 2;
            aspectRatio = (double)bmp.Height / (double)bmp.Width;

            sphere = new Sphere(radius, meridians, parallels);
            camera = new Camera(0, 0, 100);
            Draw();
        }

        public void Draw()
        {

            using (Graphics g = Graphics.FromImage(image))
            {
                g.FillRectangle(Brushes.White, 0, 0, image.Width, image.Height);
            }

            Pen blackPen = new Pen(Color.Black, 1);
            foreach (var triangle in sphere.triangles)
            {
                if (camera.direction.Dot(triangle.normal) < 0) // Backface Culling
                {
                    Vertice vertex1 = new Vertice(new Point(triangle.vertice1.point.px * camera.viewMatrix[0, 0] + triangle.vertice1.point.py * camera.viewMatrix[0, 1] + triangle.vertice1.point.pz * camera.viewMatrix[0, 2] + camera.viewMatrix[0, 3] + bitmapCenterX,
                                                            triangle.vertice1.point.px * camera.viewMatrix[1, 0] + triangle.vertice1.point.py * camera.viewMatrix[1, 1] + triangle.vertice1.point.pz * camera.viewMatrix[1, 2] + camera.viewMatrix[1, 3] + bitmapCenterY,
                                                            triangle.vertice1.point.px * camera.viewMatrix[2, 0] + triangle.vertice1.point.py * camera.viewMatrix[2, 1] + triangle.vertice1.point.pz * camera.viewMatrix[2, 2] + camera.viewMatrix[2, 3]),
                                                            triangle.vertice1.normal);
                    Vertice vertex2 = new Vertice(new Point(triangle.vertice2.point.px * camera.viewMatrix[0, 0] + triangle.vertice2.point.py * camera.viewMatrix[0, 1] + triangle.vertice2.point.pz * camera.viewMatrix[0, 2] + camera.viewMatrix[0, 3] + bitmapCenterX,
                                                            triangle.vertice2.point.px * camera.viewMatrix[1, 0] + triangle.vertice2.point.py * camera.viewMatrix[1, 1] + triangle.vertice2.point.pz * camera.viewMatrix[1, 2] + camera.viewMatrix[1, 3] + bitmapCenterY,
                                                            triangle.vertice2.point.px * camera.viewMatrix[2, 0] + triangle.vertice2.point.py * camera.viewMatrix[2, 1] + triangle.vertice2.point.pz * camera.viewMatrix[2, 2] + camera.viewMatrix[2, 3]),
                                                            triangle.vertice2.normal);
                    Vertice vertex3 = new Vertice(new Point(triangle.vertice3.point.px * camera.viewMatrix[0, 0] + triangle.vertice3.point.py * camera.viewMatrix[0, 1] + triangle.vertice3.point.pz * camera.viewMatrix[0, 2] + camera.viewMatrix[0, 3] + bitmapCenterX,
                                                            triangle.vertice3.point.px * camera.viewMatrix[1, 0] + triangle.vertice3.point.py * camera.viewMatrix[1, 1] + triangle.vertice3.point.pz * camera.viewMatrix[1, 2] + camera.viewMatrix[1, 3] + bitmapCenterY,
                                                            triangle.vertice3.point.px * camera.viewMatrix[2, 0] + triangle.vertice3.point.py * camera.viewMatrix[2, 1] + triangle.vertice3.point.pz * camera.viewMatrix[2, 2] + camera.viewMatrix[2, 3]),
                                                            triangle.vertice3.normal);

                    Fill(new Triangle(vertex1, vertex2, vertex3));

                    using (var graphics = Graphics.FromImage(image))
                    {
                        graphics.DrawLine(blackPen, (int)vertex1.point.px, (int)vertex1.point.py,
                                                    (int)vertex2.point.px, (int)vertex2.point.py);
                        graphics.DrawLine(blackPen, (int)vertex2.point.px, (int)vertex2.point.py,
                                                    (int)vertex3.point.px, (int)vertex3.point.py);
                        graphics.DrawLine(blackPen, (int)vertex3.point.px, (int)vertex3.point.py,
                                                    (int)vertex1.point.px, (int)vertex1.point.py);
                    }
                }

            }
            Canvas.Refresh();
        }

        private void Fill(Triangle triangle)
        {
            List<Edge> ET = new List<Edge>();
            
            // Define edges calculate dx/dy
            Edge tmp1 = new Edge(triangle.vertice1.point.py < triangle.vertice2.point.py ? (int)triangle.vertice1.point.py : (int)triangle.vertice2.point.py,
                    triangle.vertice1.point.py > triangle.vertice2.point.py ? (int)triangle.vertice1.point.py : (int)triangle.vertice2.point.py,
                    triangle.vertice1.point.py < triangle.vertice2.point.py ? (int)triangle.vertice1.point.px : (int)triangle.vertice2.point.px,
                    triangle.vertice1.point.py > triangle.vertice2.point.py ? (int)triangle.vertice1.point.px : (int)triangle.vertice2.point.px,
                    triangle.vertice1.point.px - triangle.vertice2.point.px != 0 ? (triangle.vertice2.point.px - triangle.vertice1.point.px) / (triangle.vertice2.point.py - triangle.vertice1.point.py) : int.MaxValue);

            Edge tmp2 = new Edge(triangle.vertice2.point.py < triangle.vertice3.point.py ? (int)triangle.vertice2.point.py : (int)triangle.vertice3.point.py,
                    triangle.vertice2.point.py > triangle.vertice3.point.py ? (int)triangle.vertice2.point.py : (int)triangle.vertice3.point.py,
                    triangle.vertice2.point.py < triangle.vertice3.point.py ? (int)triangle.vertice2.point.px : (int)triangle.vertice3.point.px,
                    triangle.vertice1.point.py > triangle.vertice2.point.py ? (int)triangle.vertice1.point.px : (int)triangle.vertice2.point.px,
                    triangle.vertice2.point.px - triangle.vertice3.point.px != 0 ? (triangle.vertice3.point.px - triangle.vertice2.point.px) / (triangle.vertice3.point.py - triangle.vertice2.point.py) : int.MaxValue);
            
            Edge tmp3 = new Edge(triangle.vertice3.point.py < triangle.vertice1.point.py ? (int)triangle.vertice3.point.py : (int)triangle.vertice1.point.py,
                    triangle.vertice3.point.py > triangle.vertice1.point.py ? (int)triangle.vertice3.point.py : (int)triangle.vertice1.point.py,
                    triangle.vertice3.point.py < triangle.vertice1.point.py ? (int)triangle.vertice3.point.px : (int)triangle.vertice1.point.px,
                    triangle.vertice1.point.py > triangle.vertice2.point.py ? (int)triangle.vertice1.point.px : (int)triangle.vertice2.point.px,
                    triangle.vertice3.point.px - triangle.vertice1.point.px != 0 ? (triangle.vertice1.point.px - triangle.vertice3.point.px) / (triangle.vertice1.point.py - triangle.vertice3.point.py) : int.MaxValue);
           
            // Remove edges parallel to scanlines
            if (tmp1.dydx != 0 && !Double.IsInfinity(tmp1.dydx) && tmp1.yMin != tmp1.yMax)
                ET.Add(tmp1);
            if (tmp2.dydx != 0 && !Double.IsInfinity(tmp2.dydx) && tmp2.yMin != tmp2.yMax)
                ET.Add(tmp2);
            if (tmp3.dydx != 0 && !Double.IsInfinity(tmp3.dydx) && tmp3.yMin != tmp3.yMax)
                ET.Add(tmp3);

            ET.Sort((t1, t2) => t1.yMin.CompareTo(t2.yMin));
            int scanlineY = 0;
            if(ET.Count()!= 0)
                scanlineY = ET.ElementAt(0).yMin;
            List<Edge> AET = new List<Edge>();

            Pen pen =  new Pen(Color.Red, 1);
            switch (brushColor)
            {
                case 0:
                    pen = new Pen(Color.Blue, 1);
                    brushColor = (brushColor + 1) % 3;
                    break;
                case 1:
                    pen = new Pen(Color.LightBlue, 1);
                    brushColor = (brushColor + 1) % 3;
                    break;
                case 2:
                    pen = new Pen(Color.LightCyan, 1);
                    brushColor = (brushColor + 1) % 3;
                    break;

            }

            while (AET.Count() != 0 || ET.Count() != 0)
            {
                // Add edges to AET from ET delete edges from ET
                foreach (var edge in ET)
                    if (scanlineY >= edge.yMin && scanlineY <= edge.yMax)
                        AET.Add(edge);
                foreach (var edge in AET)
                    ET.Remove(edge);

                // Sort edges by xMin color between pairs
                AET.Sort((t1, t2) => t1.xMin.CompareTo(t2.xMin));
                if (AET.Count() % 2 == 0 && AET.Count() != 0)
                {
                    if(AET.ElementAt(0).xMin > 0 && AET.ElementAt(1).xMin > 0)
                    using (var graphics = Graphics.FromImage(image))
                    {
                        graphics.DrawLine(pen, (int)AET.ElementAt(0).xMin, scanlineY,
                                               (int)AET.ElementAt(1).xMin, scanlineY);
                    }

                    // Remove active edges that fall outside scanline
                    scanlineY++;
                    List<Edge> toDel = new List<Edge>();
                    foreach (var edge in AET)
                        if (edge.yMax == scanlineY)
                            toDel.Add(edge);
                    foreach (var edge in toDel)
                        AET.Remove(edge);
                    toDel.Clear();

                    // change xMin of each active edge
                    foreach (var edge in AET)
                        edge.xMin = edge.dydx != int.MaxValue ? edge.xMin + edge.dydx : edge.xMin;

                }
                else
                {
                    AET.Clear();
                    scanlineY++;
                }
            }
            pen.Dispose();
        }

        private void Shift(int x = 0, int y = 0)
        {
            if (x != 0)
            {
                bitmapCenterX += x;
            }
            if (y != 0)
            {
                bitmapCenterY += y;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    camera.Move(yAngle: rotationAngle);
                    break;
                case Keys.Left:
                    camera.Move(yAngle: -rotationAngle);
                    break;
                case Keys.Up:
                    camera.Move(zAngle: rotationAngle);
                    break;
                case Keys.Down:
                    camera.Move(zAngle: -rotationAngle);
                    break;
                case Keys.W:
                    Shift(y: shiftPixels);
                    break;
                case Keys.S:
                    Shift(y: -shiftPixels);
                    break;
                case Keys.A:
                    Shift(x: shiftPixels);
                    break;
                case Keys.D:
                    Shift(x: -shiftPixels);
                    break;
                default:
                    return;
            }
            Draw();
        }
    }
}
