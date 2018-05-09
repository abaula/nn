using System;
using System.Threading;
using WinFormsApp.Model;

namespace WinFormsApp.Services
{
    class EmployeeAnalyseService
    {
        private readonly int _windowSize;
        private readonly int _learningWindowSize;
        private readonly SynchronizationContext _context;
        private readonly Action<object> _onChangeCallback;
        private readonly Action<object> _onCompleteCallback;
        private Thread _thread;
        private Random _rnd;

        public EmployeeAnalyseService(EmployeeDataDto data,
            int windowSize,
            int learningWindowSize,
            SynchronizationContext context,
            Action<object> onChangeCallback,
            Action<object> onCompleteCallback)
        {
            _windowSize = windowSize;
            _learningWindowSize = learningWindowSize;
            _context = context;
            _onChangeCallback = onChangeCallback;
            _onCompleteCallback = onCompleteCallback;
            Data = data;
            IsInProgress = false;
            _rnd = new Random();
        }

        public EmployeeDataDto Data { get; }
        public bool IsInProgress { get; private set;  }

        public void Start()
        {
            IsInProgress = true;
            _thread = new Thread(Run);
            _thread.IsBackground = true;
            _thread.Start();
        }

        private void Run()
        {
            FireOnDataChanged(CreateEmptyPackage());
            Thread.Sleep(1500);
            var i = 0;

            while (i < 20)
            {
                FireOnDataChanged(CreatePackage());
                i++;
                Thread.Sleep(500);
            }

            IsInProgress = false;
            FireOnComplete();
        }

        private void FireOnComplete()
        {
            _context.Post(new SendOrPostCallback(_onCompleteCallback), Data.EmployeeId.ToString());
        }

        private void FireOnDataChanged(EmployeeDataPackageDto package)
        {
            _context.Post(new SendOrPostCallback(_onChangeCallback), package);
        }

        private EmployeeDataPackageDto CreatePackage()
        {
            var package = new EmployeeDataPackageDto { EmployeeId = Data.EmployeeId };

            for (var i = 0; i < _windowSize; i++)
            {
                var key = new SequenceItemKey { Date = DateTime.Now, Number = i };
                package.AnomaliesDataSequence.Add(new SequenceItemDto<SequenceItemKey, double> { Key = key, Value = i < _learningWindowSize ? double.NaN : _rnd.NextDouble() });

                if (i >= _learningWindowSize)
                    continue;

                package.AnalysingDataSequenceMaster.Add(Data.MasterDataSequence[i]);
                package.AnalysingDataSequencePredicted.Add(new SequenceItemDto<SequenceItemKey, double> { Key = key, Value = _rnd.NextDouble() });
            }

            return package;
        }

        private EmployeeDataPackageDto CreateEmptyPackage()
        {
            var dt = DateTime.Now;
            var package = new EmployeeDataPackageDto { EmployeeId = Data.EmployeeId };

            for (var i = 0; i < _windowSize; i++)
            {
                var key = new SequenceItemKey { Date = dt, Number = i };
                package.AnomaliesDataSequence.Add(new SequenceItemDto<SequenceItemKey, double> { Key = key, Value = double.NaN });

                if(i >= _learningWindowSize)
                    continue;

                package.AnalysingDataSequenceMaster.Add(Data.MasterDataSequence[i]);
                package.AnalysingDataSequencePredicted.Add(new SequenceItemDto<SequenceItemKey, double> { Key = key, Value = double.NaN });
            }

            return package;
        }
    }
}
