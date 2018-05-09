using System;
using System.Collections.Generic;

namespace WinFormsApp.Model
{
    [Serializable]
    class EmployeeDataDto
    {
        public EmployeeDataDto()
        {
            MasterDataSequence = new List<SequenceItemDto<SequenceItemKey, double>>();
            AnomaliesDataSequence = new List<SequenceItemDto<SequenceItemKey, double>>();
        }

        public int EmployeeId { get; set; }
        public bool IsEmployee { get; set; }
        public List<SequenceItemDto<SequenceItemKey, double>> MasterDataSequence { get; }
        public List<SequenceItemDto<SequenceItemKey, double>> AnomaliesDataSequence { get; }
    }
}
