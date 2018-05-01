using NeuNet.Neural.Errors.Abstractions;
using NeuNet.Neural.Networks.Abstractions;

namespace NeuNet.Neural.Errors
{
    public class LogisticRegressionErrorFunc : ILogisticRegressionErrorFunc
    {
        public LogisticRegressionErrorFunc(ILogisticRegressionNetwork network)
        {
            Network = network;
        }

        public ILogisticRegressionNetwork Network { get; }

        public double ComputeError(double[][] data)
        {
            var yIndex = Network.NumberOfFeatures;
            var sumSquaredError = 0.0;

            for (int i = 0; i < data.Length; i++)
            {
                var computed = Network.Calculate(data[i]);
                var desired = data[i][yIndex];
                var error = desired - computed[0];
                sumSquaredError += (error * error);
            }

            return sumSquaredError / data.Length;
        }
    }
}
