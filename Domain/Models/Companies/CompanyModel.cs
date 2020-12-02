using Domain.Models.Employees;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Companies
{
    public class CompanyModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public IEnumerable<EmployeeModel> Employees { get; set; }
    }

    public class CompanyModelValidator : AbstractValidator<CompanyModel>
    {
        public CompanyModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.EstablishmentYear).NotNull();
            RuleForEach(x => x.Employees).ChildRules(employees =>
            {
                employees.RuleFor(e => e.FirstName).NotEmpty();
                employees.RuleFor(x => x.LastName).NotEmpty();
                employees.RuleFor(x => x.DateOfBirth).NotNull();
                employees.RuleFor(x => x.JobTitle).NotNull();

            });
        }
    }

  

}
