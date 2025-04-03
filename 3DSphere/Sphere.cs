using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DSphere
{
    class Sphere
    {
        double radius;
        int meridians;
        int parallels;

        public Vertice[] vertices; 
        public Triangle[] triangles;

        public Sphere(double radius, int meridians, int parallels)
        {
            this.radius = radius;
            this.meridians = meridians; 
            this.parallels = parallels;
            
            // Vertices
            int NoOfVertices = meridians * parallels + 2;
            this.vertices = new Vertice[NoOfVertices];
            Point[] points = new Point[NoOfVertices];
            Texture[] textures = new Texture[NoOfVertices];
            points[0] = new Point(0, radius, 0);
            points[meridians * parallels + 1] = new Point(0, -radius, 0);

            for(int i = 0; i <= parallels - 1; i++)
            {
                for(int j = 0; j <= meridians - 1; j++)
                {
                    points[(i * meridians) + j + 1] = new Point(radius * Math.Cos(((2 * Math.PI) / meridians) * j) * Math.Sin((Math.PI / (parallels + 1)) * (i + 1)),
                                                                radius * Math.Cos((Math.PI/(parallels+1))*(i+1)),
                                                                radius * Math.Sin(((2*Math.PI)/meridians)*j)*Math.Sin((Math.PI/(parallels+1))*(i+1)));
                }
            }

            // Normals
            CalculateNormals(points);

            // Textures
            vertices[0].texture = new Texture(1, 0.5);
            vertices[meridians * parallels + 1].texture = new Texture(0, 0.5);
            for (double i = 0; i <= parallels - 1; i++)
                for (double j = 0; j <= meridians - 1; j++)
                    vertices[(int)i * meridians + (int)j + 1].texture = new Texture(j / (meridians - 1), (i + 1) / (parallels + 1));

            // Triangles
            int NoOfTriangles = 2 * meridians * parallels;
            this.triangles = new Triangle[NoOfTriangles];
            // Upper lid
            for (int i = 0; i <= meridians - 2; i++)
                triangles[i] = new Triangle(vertices[0], vertices[i + 2], vertices[i + 1]);
            triangles[meridians - 1] = new Triangle(vertices[0], vertices[1], vertices[meridians]);

            // Lower lid
            for (int i = 0; i <= meridians - 2; i++)
                triangles[(2 * parallels - 1) * meridians + i] = new Triangle(vertices[meridians * parallels + 1],
                                                                                  vertices[(parallels - 1) * meridians + i + 1],
                                                                                  vertices[(parallels - 1) * meridians + i + 2]);
            triangles[(2 * parallels - 1) * meridians + meridians - 1] = new Triangle(vertices[meridians * parallels + 1],
                                                                                   vertices[meridians * parallels],
                                                                                   vertices[(parallels - 1) * meridians + 1]);

            // Strips
            for (int i = 0; i <= parallels - 2; i++)
            {
                for (int j = 1; j<=meridians - 1; j++)
                {
                    triangles[(2 * i + 1) * meridians + j - 1] = new Triangle(vertices[i * meridians + j],
                                                                              vertices[i * meridians + j + 1],
                                                                              vertices[(i + 1) * meridians + j + 1]);
                    triangles[(2 * i + 2) * meridians + j - 1] = new Triangle(vertices[i * meridians + j],
                                                                              vertices[(i + 1) * meridians + j + 1],
                                                                              vertices[(i + 1) * meridians + j]);
                }
                triangles[(2 * i + 1) * meridians + meridians - 1] = new Triangle(vertices[(i + 1) * meridians],
                                                                                  vertices[i * meridians + 1],
                                                                                  vertices[(i + 1) * meridians + 1]);
                triangles[(2 * i + 2) * meridians + meridians - 1] = new Triangle(vertices[(i + 1) * meridians],
                                                                                  vertices[(i + 1) * meridians + 1],
                                                                                  vertices[(i + 2) * meridians]);
            }
            return;
        }

        private void CalculateNormals(Point[]points)
        {
            for (int i = 0;i<points.Length;i++)
            {
                vertices[i] = new Vertice(points[i], new Normal(points[i].px / radius, points[i].py / radius, points[i].pz / radius));
            }
        }
    }
}
