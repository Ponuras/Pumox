using Domain.Enums.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Companies
{
    public class CompanySearchModel
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public EmployeeJobTitleEnum? EmployeeJobTitles { get; set; }
    }
}
