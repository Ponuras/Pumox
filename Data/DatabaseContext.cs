using Data.Models.Companies;
using Data.Models.Employees;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class DatabaseContext : DbContext, IUnitOfWork
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {

        }

        public DbSet<Company> Companies { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetCompanies(modelBuilder);
            SetEmployees(modelBuilder);
        }

        private void SetCompanies(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().Property(c => c.ID).ValueGeneratedOnAdd();
        }
        private void SetEmployees(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(c => c.ID).ValueGeneratedOnAdd();
        }

        void IUnitOfWork.SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
