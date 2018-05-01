
namespace NeuNet.Neural.Trainers.Abstractions
{
    public interface ILimitTrainOptimizer : ITrainOptimizer
    {
        int MaxEpoch { get; set; }
        double MinError { get; set; }
    }
}
