using System;
using NeuNet.Neural;

namespace NeuNet.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            var arr = new double[21];
            var left = new Matrix(arr, 0, 3, 3);
            var right = new Matrix(arr, 9, 2, 3);
            var result = new Matrix(arr, 15, 2, 3);

            left.Load(new double[]
            {
                // col 0
                1, 4, 12,
                // col 1
                18, 2, 3,
                // col2
                6, 11, 1
            });

            right.Load(new double[]
            {
                // col 0
                1, 2, 9,
                // col 1
                4, 8, 6
            });

            var dt = DateTime.Now;

            for (var i = 0; i < 1000000; i++)
                result = MatrixOperations.Multiply(left, right, result);

            Console.WriteLine(DateTime.Now - dt);

            for (var row = 0; row < result.Rows; row++)
            {
                for (var col = 0; col < result.Cols; col++)
                    Console.Write($"{result[col, row]} ");

                Console.WriteLine();
            }


            Console.ReadKey();
        }
    }
}
