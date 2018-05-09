using System.Collections.Generic;

namespace WinFormsApp.Model
{
    class EmployeeDataPackageDto
    {
        public EmployeeDataPackageDto()
        {
            AnalysingDataSequenceMaster = new List<SequenceItemDto<SequenceItemKey, double>>();
            AnalysingDataSequencePredicted = new List<SequenceItemDto<SequenceItemKey, double>>();
            AnomaliesDataSequence = new List<SequenceItemDto<SequenceItemKey, double>>();
        }

        public int EmployeeId { get; set; }
        public List<SequenceItemDto<SequenceItemKey, double>> AnalysingDataSequenceMaster { get; }
        public List<SequenceItemDto<SequenceItemKey, double>> AnalysingDataSequencePredicted { get; }
        public List<SequenceItemDto<SequenceItemKey, double>> AnomaliesDataSequence { get; }
    }
}
