using System;

namespace Mathtastic.Structures
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Point()
        {
            X = Y = Z = 0;
        }

        public Point AddVector(Vector vector)
        {
            return new Point
            {
                X = X + vector.X,
                Y = Y + vector.Y,
                Z = Z + vector.Z
            };
        }

        public Point SubtractVector(Vector vector)
        {
            return new Point
            {
                X = X - vector.X,
                Y = Y - vector.Y,
                Z = Z - vector.Z
            };
        }

        public Vector SubtractPoint(Point point)
        {
            return new Vector
            {
                X = X - point.X,
                Y = Y - point.Y,
                Z = Z - point.Z
            };
        }

        public void PrintPointToScreen()
        {
            Console.WriteLine("{{{0}, {1}, {2}}}", X, Y, Z);
            Console.WriteLine();
        }
    }
}
