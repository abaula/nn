using NeuNet.Neural.Errors.Abstractions;
using NeuNet.Neural.Networks.Abstractions;

namespace NeuNet.Neural.Trainers.Abstractions
{
    public interface ILogisticBatchGradientDescentTrainer : ITrainer
    {
        double LearningRate { get; set; }
        ILogisticRegressionErrorFunc ErrorFunc { get; }
        ILogisticRegressionNetwork Network { get; }
    }
}
