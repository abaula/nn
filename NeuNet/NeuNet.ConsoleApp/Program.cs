using System;
using NeuNet.Neural;

namespace NeuNet.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            /*
            Console.WriteLine("Hello World!");

            var arr = new float[20];
            var seg1 = new ArraySegment<float>(arr, 0, 10);
            var seg2 = new ArraySegment<float>(arr, 10, 10);

            for (var i = 0; i < seg1.Count; i++)
                seg1[i] = 1;

            for (var i = 0; i < seg2.Count; i++)
                seg2[i] = 2;

            foreach (var f in arr)
                Console.Write(f);
            */

            var arr = new double[21];
            var left = new Matrix(arr, 0, 3, 3);
            var right = new Matrix(arr, 9, 2, 3);
            var result = new Matrix(arr, 15, 2, 3);

            left[0, 0] = 1; left[1, 0] = 18; left[2, 0] = 6;
            left[0, 1] = 4; left[1, 1] = 2; left[2, 1] = 11;
            left[0, 2] = 12; left[1, 2] = 3; left[2, 2] = 1;

            right[0, 0] = 1; right[1, 0] = 4;
            right[0, 1] = 2; right[1, 1] = 8;
            right[0, 2] = 9; right[1, 2] = 6;

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
