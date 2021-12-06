using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infraestructure.Repository
{
  public interface IRepository<T> where T 
:class    {

        QueryResult GetAll(QueryOptions queryOptions);
        QueryResult Get(Expression<Func<T, bool>> predicate, QueryOptions queryOptions);
        T GetOne(Expression<Func<T, bool>> predicate);
        void Insert(T entity);
        void Delete(T entity);
        void Delete(object id);
        void Update(object id, T entity);
    }
}
