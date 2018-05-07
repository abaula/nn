using System;

namespace WinFormsApp.Model
{
    class Skud
    {
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsEmployee { get; set; }
    }
}
