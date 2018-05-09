using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using WinFormsApp.Model;

namespace WinFormsApp.Services
{
    class DataService
    {
        private const string DataFileName = "data";
        private readonly string _dataStoragePath;
        private readonly CssReaderService _cssReaderService;
        private readonly EmployeeDataBuilderService _employeeDataBuilderService;

        public DataService(string dataStoragePath)
        {
            _dataStoragePath = dataStoragePath;
            _cssReaderService = new CssReaderService();
            _employeeDataBuilderService = new EmployeeDataBuilderService();
        }

        public void ReLoad(string path)
        {
            var skuds = _cssReaderService.Parse(path);
            var employeeData = _employeeDataBuilderService.Build(skuds);
            var filteredEmployeeData = FilterData(employeeData);
            ClearDataStorage();
            CreateDataStorage(filteredEmployeeData);
        }

        public string[] GetList()
        {
            var result = new List<string>();
            var dir = new DirectoryInfo(_dataStoragePath);

            foreach (var subdir in dir.EnumerateDirectories())
            {
                result.Add(subdir.Name);
            }

            return result.ToArray();
        }

        public EmployeeDataDto GetEmployee(string employeeId)
        {
            var file = new FileInfo(Path.Combine(_dataStoragePath, employeeId, DataFileName));

            using (var stream = file.OpenRead())
            {
                var formatter = new BinaryFormatter();
                var result = (EmployeeDataDto)formatter.Deserialize(stream);
                stream.Close();

                return result;
            }
        }

        public void SaveEmployeeData(EmployeeDataDto data)
        {
            var dir = new DirectoryInfo(_dataStoragePath);
            var subdir = dir.CreateSubdirectory(data.EmployeeId.ToString());
            var file = new FileInfo(Path.Combine(subdir.FullName, DataFileName));
            using (var stream = file.Create())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
                stream.Close();
            }
        }

        private EmployeeDataDto[] FilterData(EmployeeDataDto[] employeeData)
        {
            const int windowSizeDays = 60;
            const int skipFiredEmployeeLastDays = 10;
            var result = new List<EmployeeDataDto>();

            foreach (var dto in employeeData)
            {
                var dataLength = dto.MasterDataSequence.Count;

                if (dto.IsEmployee && dataLength < windowSizeDays)
                    continue;

                if (!dto.IsEmployee && dataLength < windowSizeDays + skipFiredEmployeeLastDays)
                    continue;

                var skip = dto.IsEmployee
                    ? dataLength - windowSizeDays
                    : dataLength - windowSizeDays - skipFiredEmployeeLastDays;

                var employeeDto = new EmployeeDataDto {EmployeeId = dto.EmployeeId, IsEmployee = dto.IsEmployee};
                employeeDto.MasterDataSequence.AddRange(dto.MasterDataSequence.Skip(skip).Take(windowSizeDays));
                employeeDto.AnomaliesDataSequence.AddRange(dto.AnomaliesDataSequence.Skip(skip).Take(windowSizeDays));
                result.Add(employeeDto);
            }

            return result.ToArray();
        }

        private void CreateDataStorage(EmployeeDataDto[] employeeData)
        {
            var dir = new DirectoryInfo(_dataStoragePath);

            foreach (var dto in employeeData)
            {
                var subdir = dir.CreateSubdirectory(dto.EmployeeId.ToString());
                var file = new FileInfo(Path.Combine(subdir.FullName, DataFileName));
                using (var stream = file.Create())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, dto);
                    stream.Close();
                }
            }
        }

        private void ClearDataStorage()
        {
            var dir = new DirectoryInfo(_dataStoragePath);

            foreach (var subdir in dir.GetDirectories())
            {
                foreach(var file in subdir.GetFiles())
                    file.Delete();

                subdir.Delete();
            }
        }
    }
}
