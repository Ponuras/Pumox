using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Employees
{
    public class EmployeeModel
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int JobTitle { get; set; }
    }
}
