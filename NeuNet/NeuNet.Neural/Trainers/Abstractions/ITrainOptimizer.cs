using System;

namespace NeuNet.Neural.Trainers.Abstractions
{
    public interface ITrainOptimizer
    {
        int CurrentEpoch { get; }
        event Action<ITrainOptimizer, ITrainer> OnEpoch;
        int NotifyOnEachNthEpoch { get; set; }
        void Train();
        void Stop();
    }
}
