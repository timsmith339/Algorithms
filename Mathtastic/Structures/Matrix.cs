using System;
using System.Text;

namespace Mathtastic.Structures
{
    public class Matrix
    {
        public int[,] Elements { get; set; }

        public Matrix(int rows, int cols)
        {
            Elements = new int[rows, cols];
        }

        public Matrix MultiplyScalar(int scalar)
        {
            var returnMatrix = new Matrix(GetRowCount(), GetColumnCount());

            for (int y = 0; y < GetRowCount(); y++)
            {
                for (int z = 0; z < GetColumnCount(); z++)
                {
                    returnMatrix.Elements[y, z] = Elements[y, z] * scalar;
                }
            }

            return returnMatrix;
        }

        public Matrix MultiplyMatrix(Matrix bMatrix)
        {
            var aMatrix = this;

            if (aMatrix.GetColumnCount() != bMatrix.GetRowCount())
                throw new Exception("Cannot multply matrices: A's column count does not equal B's row count.");

            var newRowCnt = aMatrix.GetRowCount();
            var newColCnt = bMatrix.GetColumnCount();
            
            var returnMatrix = new Matrix(newRowCnt, newColCnt);

            for (var y = 0; y < newRowCnt; y++)
            {
                for (var z = 0; z < newColCnt; z++)
                {
                    var total = 0;

                    for (var a = 0; a < aMatrix.GetColumnCount(); a++)
                    {
                        total += aMatrix.Elements[y, a] * bMatrix.Elements[a, z];
                    }

                    returnMatrix.Elements[y, z] = total;
                }
            }

            return returnMatrix;
        }

        public Matrix AddMatrix(Matrix bMatrix)
        {
            if (bMatrix.GetColumnCount() != GetColumnCount()
                || bMatrix.GetRowCount() != GetRowCount())
            {
                throw new Exception("Cannot add matrices: Matrices are not the same size");
            }

            var returnMatrix = CreateEmptyCopy(); 

            for (var y = 0; y < GetRowCount(); y++)
            {
                for (var z = 0; z < GetColumnCount(); z++)
                {
                    returnMatrix.Elements[y, z] = Elements[y, z] + bMatrix.Elements[y, z];
                }
            }

            return returnMatrix;
        }

        public int GetTotalElementCount()
        {
            return Elements.Length;
        }

        public int GetElement(int row, int col)
        {
            return Elements[row, col];
        }

        public int GetRowCount()
        {
            return Elements.GetLength(0);
        }

        public int GetColumnCount()
        {
            return Elements.GetLength(1);
        }

        public void AutoFillWithRandom(int low, int high)
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
         
            for (var y = 0; y < GetRowCount(); y++)
            {
                for (var z = 0; z < GetColumnCount(); z++)
                {
                    Elements[y, z] = rand.Next(low, high);
                }
            }
        }

        public void PrintMatrixToScreen()
        {
            var sb = new StringBuilder();

            for (var y = 0; y < GetRowCount(); y++)
            {
                for (var z = 0; z < GetColumnCount(); z++)
                {
                    var element = Elements[y, z];
                    sb.Append(element + "\t");
                }
                Console.WriteLine(sb);
                sb.Clear();
            }

            Console.WriteLine();
        }

        public Matrix CreateEmptyCopy()
        {
            return new Matrix(GetRowCount(), GetColumnCount());
        }
    }
}
