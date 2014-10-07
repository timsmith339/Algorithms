using Mathtastic.Structures;
using System;

namespace Mathtastic
{
    class Program
    {
        static void Main(string[] args)
        {
            var examples = new Examples();

            // Find Cross Product of two vectors
            examples.FindCrossProduct();

            // Add Two Matrices
            examples.AddMatrices();

            // Scalar Matrix Multipliaction
            examples.ScalarMatrixMultiplication();

            // Matrix Multiplication
            examples.MultiplyMatrices();

        }
    }

    public class Examples
    {
        public void MultiplyMatrices()
        {
            Console.WriteLine("Multiplying two matrices:");
            var aMatrix = new Matrix(4, 2);
            aMatrix.AutoFillWithRandom(1, 20);
            aMatrix.PrintMatrixToScreen();

            var bMatrix = new Matrix(2, 5);
            bMatrix.AutoFillWithRandom(1, 20);
            bMatrix.PrintMatrixToScreen();

            var cMatrix = aMatrix.MultiplyMatrix(bMatrix);
            cMatrix.PrintMatrixToScreen();
        }

        public void ScalarMatrixMultiplication()
        {
            Console.WriteLine("Scalar Multiplication (by -5):");
            var aMatrix = new Matrix(6, 4);
            aMatrix.AutoFillWithRandom(-10, 10);
            aMatrix.PrintMatrixToScreen();

            var cMatrix = aMatrix.MultiplyScalar(-5);
            cMatrix.PrintMatrixToScreen();
        }

        public void AddMatrices()
        {
            Console.WriteLine("Adding two matrices:");
            var aMatrix = new Matrix(4, 8);
            aMatrix.AutoFillWithRandom(0, 9);
            aMatrix.PrintMatrixToScreen();
            
            var bMatrix = new Matrix(4, 8);
            bMatrix.AutoFillWithRandom(0, 9);
            bMatrix.PrintMatrixToScreen();
            
            var cMatrix = aMatrix.AddMatrix(bMatrix);
            cMatrix.PrintMatrixToScreen();

        }

        public void FindCrossProduct()
        {
            Console.WriteLine("Finding Cross Product of two Vectors:");
            var aVector = new Vector()
            {
                X = 5,
                Y = 1,
                Z = 4
            };
            aVector.PrintVectorToScreen();

            var bVector = new Vector()
            {
                X = -1,
                Y = 0,
                Z = 2
            };
            bVector.PrintVectorToScreen();

            var cVector = aVector.FindCrossProduct(bVector);
            cVector.PrintVectorToScreen();
        }
    }
}
