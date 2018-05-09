using System;
using System.Collections.Generic;
using System.Linq;
using WinFormsApp.Extensions;
using WinFormsApp.Model;

namespace WinFormsApp.Services
{
    class EmployeeDataBuilderService
    {
        public EmployeeDataDto[] Build(Skud[] skuds)
        {
            var groups = skuds.GroupBy(s => s.EmployeeId);
            var list = new List<EmployeeDataDto>();

            foreach (var group in groups)
            {
                var employeeDataItem = new EmployeeDataDto {EmployeeId = group.Key, IsEmployee = group.First().IsEmployee};
                var weekGroups = group.Where(g => g.Date.DayOfWeek != DayOfWeek.Saturday && g.Date.DayOfWeek != DayOfWeek.Sunday)
                    .GroupBy(g => g.Date.WeekOfYear());

                foreach (var weekGroup in weekGroups)
                {
                    var weekStartTimeAvg = weekGroup.Average(g => g.StartTime.Ticks);

                    foreach (var skud in weekGroup.OrderBy(wg => wg.Date))
                    {
                        var key = new SequenceItemKey {Date = skud.Date, Number = 0};
                        var hourValue = Math.Abs(skud.StartTime.Ticks - weekStartTimeAvg).TicksToHour();
                        var sequenceItem = new SequenceItemDto<SequenceItemKey, double> {Key = key, Value = hourValue};
                        employeeDataItem.MasterDataSequence.Add(sequenceItem);

                        sequenceItem = new SequenceItemDto<SequenceItemKey, double> { Key = key, Value = Double.NaN };
                        employeeDataItem.AnomaliesDataSequence.Add(sequenceItem);
                    }
                }

                list.Add(employeeDataItem);
            }

            return list.ToArray();
        }
    }
}
