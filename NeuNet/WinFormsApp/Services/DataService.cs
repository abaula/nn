using System.IO;
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

        public void Load(string path)
        {
            var skuds = _cssReaderService.Parse(path);
            var employeeData = _employeeDataBuilderService.Build(skuds);
            ClearDataStorage();
            CreateDataStorage(employeeData);
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
