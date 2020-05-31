using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ncstreamer
{
    class Vector
    {
        public double x, y;

        public Vector(double a, double b)
        {
            x = a;
            y = b;
        }

        public Vector(Vector v)
        {
            x = v.x;
            y = v.y;
        }

        public static string Print(Vector a)
        {
            return a.x.ToString() + " : " + a.y.ToString();
        }

        public static string PrintLine(Vector a)
        {
            return a.x.ToString() + " : " + a.y.ToString() + "\n";
        }

        public Vector(Vector a, Vector b)
        {
            x = b.x - a.x;
            y = b.y - a.y;
        }

        public static double Mod(Vector a, Vector b)
        {
            double x = b.x - a.x;
            double y = b.y - a.y;
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        public double ModuleVector()
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        public double VectorMultiply(Vector b)
        {
            return (x * b.x) + (y * b.y);
        }

        public static double Multiply(Vector a, Vector b, Vector c)
        {
            double x1 = c.x - a.x;
            double y1 = c.y - a.y;
            double x2 = b.x - a.x;
            double y2 = b.y - a.y;

            return ((x1 * y2) - (x2 * y1));
        }

        public void Scale(double magnitude)
        {
            x *= magnitude;
            y *= magnitude;
        }

        public void ScalarSubstract(Vector s)
        {
            x -= s.x;
            y -= s.y;
        }
        
        public static double Angle(Vector start, Vector stop, Vector center)
        {
            Vector v1 = new Vector(center, start);
            Vector v2 = new Vector(center, stop);

            double v1v2 = v1.VectorMultiply(v2);
            
            double mod_v1 = v1.ModuleVector();
            double mod_v2 = v2.ModuleVector();
            
            double cos_v1v2 = v1v2 / (mod_v1 * mod_v2);
            
            double angle = (double)(Math.Acos(cos_v1v2) * 180.0F / Math.PI);

            if (double.IsNaN(angle))
            {
                return 0.0;
            }

            return angle;
        }

    }
}
