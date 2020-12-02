using AutoMapper;
using Data.Models.Companies;
using Data.Repositories;
using Domain.Extensions.Attributes;
using Domain.Extensions.Linq;
using Domain.Models.Companies;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;

namespace pumox.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CompanyController : AuthorizedController
    {

        private readonly IMapper _mapper;

        public CompanyController(
          IMapper mapper)
        {

            _mapper = mapper;
        }


        [HttpPost]
       [CustomAuthAtt]
        public IActionResult Create(CompanyModel model)
        {



            CompanyModelValidator validator = new CompanyModelValidator();

            ValidationResult results = validator.Validate(model);

            if (results.IsValid)
            {
                model.ID = Using<ICompaniesRepository>().Create(_mapper.Map<Company>(model));
            }
            else
            {
                return StatusCode(400, results.Errors);
            }


            return Ok(new { ID=model.ID});
        }

        [HttpPost]
        [ResponseType(typeof(IEnumerable<CompanyModel>))]
        public IActionResult Search(CompanySearchModel model)
        {
            var predicate = PredicateBuilder.True<Company>();

            if (!String.IsNullOrEmpty(model.Keyword))
            {
                predicate = predicate.And(c => c.Name.ToUpper().Contains(model.Keyword.ToUpper()) || model.Keyword.ToUpper().Contains(c.Name.ToUpper())
                || c.Employees.Any(e => e.FirstName.ToUpper().Contains(model.Keyword.ToUpper()) || model.Keyword.ToUpper().Contains(e.FirstName.ToUpper()))
                || c.Employees.Any(e => e.LastName.ToUpper().Contains(model.Keyword.ToUpper()) || model.Keyword.ToUpper().Contains(e.LastName.ToUpper())));
            }

            if (model.EmployeeDateOfBirthFrom.HasValue)
            {
                predicate = predicate.And(c => c.Employees.Any(e => e.DateOfBirth >= model.EmployeeDateOfBirthFrom));
            }

            if (model.EmployeeDateOfBirthTo.HasValue)
            {
                predicate = predicate.And(c => c.Employees.Any(e => e.DateOfBirth <= model.EmployeeDateOfBirthFrom));
            }

            if (model.EmployeeJobTitles.HasValue)
            {
                predicate = predicate.And(c => c.Employees.Any(e => e.JobTitle == (int)model.EmployeeJobTitles));
            }

            return Ok(Using<ICompaniesRepository>().FindBy(predicate).Select(c => _mapper.Map<CompanyModel>(c)));
        }

        [HttpPut("{id}")]
        [CustomAuthAtt]
        public IActionResult Update(long id, CompanyModel model)
        {
            var entity = _mapper.Map<CompanyModel>(Using<ICompaniesRepository>().Read(id));

            CompanyModelValidator validator = new CompanyModelValidator();

            ValidationResult results = validator.Validate(model);
            if (results.IsValid && entity != null)
            {
                entity.Employees = model.Employees;
                entity.EstablishmentYear = model.EstablishmentYear;
                entity.Name = model.Name;

                model = _mapper.Map<CompanyModel>(Using<ICompaniesRepository>().Update(_mapper.Map<Company>(entity)));

                return Ok(model);
            }
            else
            {
                if (entity == null)
                {
                    results.Errors.Add(new ValidationFailure("ID", "Błędny ID"));
                }

                return StatusCode(400, results.Errors);
            }
        }

        [HttpDelete("{id}")]
        [CustomAuthAtt]
        public IActionResult Delete(long id)
        {
            var entity = _mapper.Map<CompanyModel>(Using<ICompaniesRepository>().Read(id));
            ValidationResult results = new ValidationResult();

            if (entity != null)
            {
                Using<ICompaniesRepository>().Delete(entity.ID);
                return Ok();
            }

            results.Errors.Add(new ValidationFailure("ID", "Błędny ID"));

            return StatusCode(400, results.Errors);
        }


        [HttpGet]
        public IActionResult Test()
        {
            return Content("TEST OK");
        }

    }
}
