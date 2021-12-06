using Infraestructure.Models;
using Infraestructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;

 
 
  

namespace Infraestructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly IUnitofWork _unitOfWork;

        public Repository(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Delete(T entity)
        {
            if (entity != null)
            {
                _unitOfWork.Context.Set<T>().Remove(entity);
            }
        }

        public void Delete(object id)
        {
            T entity = _unitOfWork.Context.Set<T>().Find(id);
            Delete(entity);
        }

        public QueryResult Get(Expression<Func<T, bool>> predicate, QueryOptions queryOptions)
        {
            var result = _unitOfWork.Context.Set<T>().Where(predicate).AsNoTracking().AsQueryable();
            var qr = new QueryResult
            {
                Total = result.Count(),
            };

            if (queryOptions.Order != null)
            {
                result = result.OrderBy(queryOptions.Order);   //.OrderBy(queryOptions.Order);
            }

            if (queryOptions.Limit != null)
            {
                result = result.Take(queryOptions.Limit.Value);
            }

            qr.Data = result;

            return qr;
        }

        public IQueryable<T> GetAll()
        {
            var result = _unitOfWork.Context.Set<T>().AsQueryable<T>();
            return result;
        }

        public QueryResult GetAll(QueryOptions queryOptions)
        {
            var result = _unitOfWork.Context.Set<T>().AsNoTracking().AsQueryable();

            var columns = from t in typeof(T).GetProperties() select new { name = t.Name, type = t.PropertyType };

            if (queryOptions.Search != null)
            {
                StringBuilder predicate = new StringBuilder();
                foreach (var c in columns.Where(t => t.type == typeof(string)))
                {
                    if (predicate.Length > 0)
                    {
                        predicate.Append(" or ");
                    }
                    predicate.Append("(" + c.name + " != null && " + c.name + ".ToLower().Contains(@0))");
                }
                // TODO: Instalar el nuget  Dynamic.core 
                result = result.Where(predicate.ToString(),queryOptions.Search.ToLower());
            }

            var qr = new QueryResult
            {
                Total = result.Count()
            };

            
            if (queryOptions.Page.HasValue && queryOptions.Page.Value >= 1 && qr.Total > (queryOptions.Page.Value * queryOptions.Limit.Value))
            {
                result = result.Skip((queryOptions.Page.Value) * queryOptions.Limit.Value);

                if (queryOptions.Limit.Value > 0)
                {
                    result = result.Take(queryOptions.Limit.Value);
                }
            }

            qr.Data = result;

            return qr;
        }

        public T GetOne(Expression<Func<T, bool>> predicate)
        {
            return _unitOfWork.Context.Set<T>().FirstOrDefault(predicate);
        }

        public void Insert(T entity)
        {
            if (entity != null)
            {
                _unitOfWork.Context.Set<T>().Add(entity);
            }
        }

        public void Update(object id, T entity)
        {
            if (entity != null)
            {
                T entitytoUpdate = _unitOfWork.Context.Set<T>().Find(id);
                if (entitytoUpdate != null)
                {
                    _unitOfWork.Context.Entry(entitytoUpdate).CurrentValues.SetValues(entity);
                    var ps = _unitOfWork.Context.Entry(entitytoUpdate).Properties;
                    foreach (var p in ps.Where(p => p.Metadata.ValueGenerated == Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAdd
                           || p.Metadata.ValueGenerated == Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.OnAddOrUpdate))
                    {
                        p.IsModified = false;
                    }
                }

            }
        }
 
    }
}
