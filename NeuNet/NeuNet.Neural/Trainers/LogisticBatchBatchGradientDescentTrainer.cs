using NeuNet.Neural.Errors.Abstractions;
using NeuNet.Neural.Networks.Abstractions;
using NeuNet.Neural.Trainers.Abstractions;

namespace NeuNet.Neural.Trainers
{
    public class LogisticBatchBatchGradientDescentTrainer : ILogisticBatchGradientDescentTrainer
    {
        public LogisticBatchBatchGradientDescentTrainer(ILogisticRegressionNetwork network,
            ILogisticRegressionErrorFunc errorFunc)
        {
            Network = network;
            ErrorFunc = errorFunc;
        }

        public double[][] Data { get; set; }
        public double LearningRate { get; set; }
        public ILogisticRegressionErrorFunc ErrorFunc { get; }
        public ILogisticRegressionNetwork Network { get; }

        public void RunEpoch()
        {
            var yIndex = Network.NumberOfFeatures;
            var accumulatedGradients = new double[Network.Weights.Length];

            for (var row = 0; row < Data.Length; row++)
            {
                var computed = Network.Calculate(Data[row]);
                var target = Data[row][yIndex];
                var delta = target - computed[0];
                accumulatedGradients[0] += delta;

                for (var j = 1; j < Network.Weights.Length; j++)
                    accumulatedGradients[j] += delta * Data[row][j - 1];
            }

            for (var j = 0; j < Network.Weights.Length; j++)
               Network.Weights[j] += LearningRate * accumulatedGradients[j];
        }

        public double GetError()
        {
            return ErrorFunc.ComputeError(Data);
        }
    }
}
