using System;

namespace Mathtastic.Structures
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Vector()
        {
            X = Y = Z = 0;
        }

        public Vector FindCrossProduct(Vector bVector)
        {
            return new Vector
            {
                X = (Y*bVector.Z) - (Z*bVector.Y),
                Y = (Z*bVector.X) - (X*bVector.Z),
                Z = (X*bVector.Y) - (Y*bVector.X)
            };
        }

        public Vector AddVector(Vector vector)
        {
            return new Vector
            {
                X = X + vector.X,
                Y = Y + vector.Y,
                Z = Z + vector.Z
            };
        }

        public Vector SubtractVector(Vector vector)
        {
            return new Vector
            {
                X = X - vector.X,
                Y = Y - vector.Y,
                Z = Z - vector.Z
            };
        }

        public void PrintVectorToScreen()
        {
            Console.WriteLine("{{{0}, {1}, {2}}}", X, Y, Z);
            Console.WriteLine();
        }
    }
}
