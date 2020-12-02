using AutoMapper;
using Data.Models.Employees;
using Domain.Models.Employees;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AutoMapperProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeModel>();
            CreateMap<EmployeeModel, Employee>();
        }
    }
}
