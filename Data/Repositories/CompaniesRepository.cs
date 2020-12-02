using Data.Models.Companies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Repositories
{
    public interface ICompaniesRepository
    {

        Company Read(long ID);
        long Create(Company model);
        Company Update(Company model);
        void Delete(long ID);
        IEnumerable<Company> FindBy(Expression<Func<Company, bool>> predicate);
    }

    public class CompaniesRepository : BaseRepository, ICompaniesRepository
    {
        public CompaniesRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public long Create(Company model)
        {

            this.GetDbSet<Company>().Add(model);
            this.UnitOfWork.SaveChanges();
            return model.ID;
        }

        public void Delete(long ID)
        {
            var entity = Read(ID);
            this.SetEntityState(entity, EntityState.Deleted);
            this.UnitOfWork.SaveChanges();

        }

        public Company Update(Company model)
        {
            var entity = Read(model.ID);
            entity.Name = model.Name;
            entity.EstablishmentYear = model.EstablishmentYear;
            entity.Employees = model.Employees;

            this.SetEntityState(entity, EntityState.Modified);
            this.UnitOfWork.SaveChanges();

            return entity;
        }

        public IEnumerable<Company> FindBy(Expression<Func<Company, bool>> predicate)
        {
            return this.GetDbSet<Company>()
                .Include(e=>e.Employees)
                .Where(predicate);
        }

        public Company Read(long ID)
        {
            return this.GetDbSet<Company>()
                .Include(e => e.Employees)
               .Where(e => e.ID == ID).SingleOrDefault();
        }


    }
}
