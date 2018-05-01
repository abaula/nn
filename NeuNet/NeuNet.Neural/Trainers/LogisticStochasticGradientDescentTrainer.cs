using NeuNet.Neural.Errors.Abstractions;
using NeuNet.Neural.Networks.Abstractions;
using NeuNet.Neural.Trainers.Abstractions;
using System;

namespace NeuNet.Neural.Trainers
{
    public class LogisticStochasticGradientDescentTrainer : ILogisticStochasticGradientDescentTrainer
    {
        private int[] _sequence;
        private readonly Random _random;

        public LogisticStochasticGradientDescentTrainer(ILogisticRegressionNetwork network,
            ILogisticRegressionErrorFunc errorFunc)
        {
            Network = network;
            ErrorFunc = errorFunc;
            _random = new Random();
        }

        public double[][] Data { get; set; }
        public double LearningRate { get; set; }
        public ILogisticRegressionErrorFunc ErrorFunc { get; }
        public ILogisticRegressionNetwork Network { get; }

        public void RunEpoch()
        {
            InitSequence();
            ShuffleSequence();
            var yIndex = Network.NumberOfFeatures;

            for (var rowI = 0; rowI < Data.Length; rowI++)
            {
                var row = _sequence[rowI];
                var computed = Network.Calculate(Data[row]);
                var target = Data[row][yIndex];
                var delta = target - computed[0];
                Network.Weights[0] += LearningRate * delta;

                for (var j = 1; j < Network.Weights.Length; j++)
                    Network.Weights[j] += LearningRate * delta * Data[row][j - 1];
            }
        }

        public double GetError()
        {
            return ErrorFunc.ComputeError(Data);
        }

        private void InitSequence()
        {
            if (_sequence != null)
                return;

            _sequence = new int[Data.Length];

            for (var i = 0; i < _sequence.Length; i++)
                _sequence[i] = i;
        }

        private void ShuffleSequence()
        {
            for (var i = 0; i < _sequence.Length; i++)
            {
                var r = _random.Next(i, _sequence.Length);
                var tmp = _sequence[r];
                _sequence[r] = _sequence[i];
                _sequence[i] = tmp;
            }
        }
    }
}
