
namespace NeuNet.Neural.Networks.Abstractions
{
    public interface INetwork
    {
        double[] Weights { get; }
        double[] Calculate(double[] input);
    }
}
