using System;

namespace NeuNet.Neural
{
    public static class MatrixOperations
    {
        public static Matrix Multiply(Matrix left, Matrix right, Matrix result)
        {
            CheckMatricesAreCompatableOrThrow(left, right, result);

            for (var resRow = 0; resRow < result.Rows; resRow++)
            {
                for (var resCol = 0; resCol < result.Cols; resCol++)
                {
                    var sum = 0.0;

                    for (var leftCol = 0; leftCol < left.Cols; leftCol++)
                        sum += left[leftCol, resRow] * right[resCol, leftCol];

                    result[resCol, resRow] = sum;
                }
            }

            return result;
        }

        private static void CheckMatricesAreCompatableOrThrow(Matrix left, Matrix right, Matrix result)
        {
            if (left.Cols != right.Rows)
                throw new ArgumentException("Matrices are not compatible");

            if (result.Cols != right.Cols)
                throw new ArgumentException($"The result matrix must have {right.Cols} columns");

            if (result.Rows != left.Rows)
                throw new ArgumentException($"The result matrix must have {left.Rows} rows");
        }
    }
}
