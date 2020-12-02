using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IUnitOfWork : IDisposable
    {
        //DbContext Context { get; }
        void SaveChanges();
    }
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }
        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();

        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
