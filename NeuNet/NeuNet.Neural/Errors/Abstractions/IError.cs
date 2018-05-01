using NeuNet.Neural.Matrixes;

namespace NeuNet.Neural.Errors.Abstractions
{
    public interface IError
    {
        Matrix Value { get; }
    }
}
