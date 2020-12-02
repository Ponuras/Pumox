using AutoMapper;
using Data.Models.Companies;
using Domain.Models.Companies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.AutoMapperProfiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyModel>();
            CreateMap<CompanyModel, Company>();
        }
    }
}
