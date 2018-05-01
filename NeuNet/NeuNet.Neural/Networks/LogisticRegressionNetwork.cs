using NeuNet.Neural.Helpers;
using NeuNet.Neural.Networks.Abstractions;

namespace NeuNet.Neural.Networks
{
    public class LogisticRegressionNetwork : ILogisticRegressionNetwork
    {
        public LogisticRegressionNetwork(int numberOfFeatures)
        {
            NumberOfFeatures = numberOfFeatures;
            Weights = new double[numberOfFeatures + 1];
        }

        public int NumberOfFeatures { get; }
        public double[] Weights { get; }

        public double[] Calculate(double[] input)
        {
            var z = 0.0;
            z += Weights[0];

            for (var i = 0; i < Weights.Length - 1; i++)
                z += Weights[i + 1] * input[i];

            return new[] { MathHelper.Sigmoid(z) };
        }
    }
}
