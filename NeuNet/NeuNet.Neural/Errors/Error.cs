using NeuNet.Neural.Errors.Abstractions;
using NeuNet.Neural.Matrixes;

namespace NeuNet.Neural.Errors
{
    public class Error : IError
    {
        public Error(Matrix value)
        {
            Value = value;
        }

        public Matrix Value { get; }
    }
}
