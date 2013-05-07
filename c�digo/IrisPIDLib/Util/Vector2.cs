using System;

namespace IrisPIDLib
{
    public class Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector2()
        {
            X = Y = 0;
        }

        public double Distancia(Vector2 v)
        {
            return Math.Sqrt(Math.Pow(v.X - X, 2) + Math.Pow(v.Y - Y, 2));
        }

        public override string ToString()
        {
            return "X = " + X.ToString() + ", Y = " + Y.ToString();
        }

    }
}
