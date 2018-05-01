using System;
using NeuNet.Neural.Trainers.Abstractions;

namespace NeuNet.Neural.Trainers
{
    public class LimitTrainOptimizer : ILimitTrainOptimizer
    {
        private readonly ITrainer _trainer;
        private readonly object _locker;
        private bool _stoped;

        public LimitTrainOptimizer(ITrainer trainer)
        {
            _trainer = trainer;
            _locker = new object();
        }

        public event Action<ITrainOptimizer, ITrainer> OnEpoch;

        public void Train()
        {
            CurrentEpoch = 0;

            lock (_locker)
                _stoped = false;

            while (CurrentEpoch < MaxEpoch)
            {
                ++CurrentEpoch;
                _trainer.RunEpoch();

                if (CurrentEpoch % NotifyOnEachNthEpoch == 0)
                    FireOnEpoch();

                lock (_locker)
                {
                    if (_stoped)
                        break;
                }

                var epochError = _trainer.GetError();

                if (epochError <= MinError)
                    break;
            }
        }

        public void Stop()
        {
            lock (_locker)
            {
                _stoped = true;
            }
        }

        public int NotifyOnEachNthEpoch { get; set; }
        public int CurrentEpoch { get; private set; }
        public int MaxEpoch { get; set; }
        public double MinError { get; set; }

        private void FireOnEpoch()
        {
            OnEpoch?.Invoke(this, _trainer);
        }
    }
}
