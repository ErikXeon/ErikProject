using System;

namespace ErikProject
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
    }

    public class Vacation
    {
        public int VacationID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string VacationType { get; set; }
        public string EmployeeName { get; set; }
    }
}