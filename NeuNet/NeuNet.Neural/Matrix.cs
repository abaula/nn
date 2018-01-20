using System;

namespace NeuNet.Neural
{
    public class Matrix
    {
        private ArraySegment<double> _source;

        public Matrix(double[] stream, int streamOffset, int cols, int rows)
        {
            _source = new ArraySegment<double>(stream, streamOffset, cols * rows);
            Cols = cols;
            Rows = rows;
        }

        public int Cols { get; }
        public int Rows { get; }

        public double this[int col, int row]
        {
            get => _source[GetSourceIndex(col, row)];
            set => _source[GetSourceIndex(col, row)] = value;
        }

        public void Load(double[] values)
        {
            for(var col = 0; col < Cols; col++)
                for (var row = 0; row < Rows; row++)
                    this[col, row] = values[GetSourceIndex(col, row)];
        }

        private int GetSourceIndex(int col, int row)
        {
            return Rows * col + row;
        }
    }
}
