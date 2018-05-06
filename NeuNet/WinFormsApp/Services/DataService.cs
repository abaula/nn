using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.Model;

namespace WinFormsApp.Services
{
    class DataService
    {
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
