
namespace NeuNet.Neural.Trainers.Abstractions
{
    public interface ITrainer
    {
        double[][] Data { get; set; }
        void RunEpoch();
        double GetError();
    }
}
