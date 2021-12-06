using Infraestructure.Context;
using Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.UnitOfWork
{
    public interface IUnitofWork : IDisposable
    {

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        APIContext Context { get; }
        bool Save();
   
    }
    public interface IUnitOfWork<TContext> : IUnitofWork where TContext : DbContext
    {
    }

    public class UnitofWork : IUnitofWork
    {
        public APIContext Context { get; }

        private Dictionary<Type, object> _repositories;
        private bool _disposed;

        public UnitofWork (APIContext context)
        {
            Context = context;
            _disposed = false;
        }
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(this);
            }
            return (IRepository<TEntity>)_repositories[type];
        }

        public bool Save()
        {
            return Context.SaveChanges() > 0;
        }

    

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool isDisposing)
        {
            if (!_disposed && isDisposing)
            {
                Context.Dispose();
            }
            _disposed = true;
        }

 
    }

}
