using NeuNet.Neural.Networks.Abstractions;

namespace NeuNet.Neural.Errors.Abstractions
{
    public interface IErrorFunc<TNetwork>
        where TNetwork : INetwork
    {
        TNetwork Network { get; }
        double ComputeError(double[][] data);
    }
}
