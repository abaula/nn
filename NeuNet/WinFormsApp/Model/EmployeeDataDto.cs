using System;
using System.Collections.Generic;

namespace WinFormsApp.Model
{
    [Serializable]
    class EmployeeDataDto
    {
        public EmployeeDataDto()
        {
            MasterDataSequence = new List<SequenceItemDto<SequenceItemKey, float>>();
            PredictedDataSequence = new List<SequenceItemDto<SequenceItemKey, float>>();
        }

        public int EmployeeId { get; set; }
        public bool IsEmployee { get; set; }
        public List<SequenceItemDto<SequenceItemKey, float>> MasterDataSequence { get; }
        public List<SequenceItemDto<SequenceItemKey, float>> PredictedDataSequence { get; }
    }
}
