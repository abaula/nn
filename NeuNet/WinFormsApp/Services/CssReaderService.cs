using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using WinFormsApp.Model;

namespace WinFormsApp.Services
{
    class CssReaderService
    {
        public Skud[] Parse(string path)
        {
            var result = new List<Skud>();

            var linesBuffer = File.ReadAllLines(path);

            for (var i = 1; i < linesBuffer.Length; i++)
            {
                var data = linesBuffer[i].Split(';');

                if (!int.TryParse(data[0], out var employeeId))
                    continue;

                result.Add(new Skud
                {
                    EmployeeId = employeeId,
                    Date = DateTime.ParseExact(data[1], "yyyyMMdd", CultureInfo.InvariantCulture),
                    StartTime = TimeSpan.ParseExact(data[2], @"hh\:mm\:ss", CultureInfo.InvariantCulture),
                    EndTime = TimeSpan.ParseExact(data[3], @"hh\:mm\:ss", CultureInfo.InvariantCulture),
                    IsEmployee = Convert.ToInt32(data[5]) == 1
                });
            }

            return result.ToArray();
        }
    }
}
