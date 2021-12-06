using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Services
{
    public interface IService<Tv, Te>
    {
        QueryResult GetAll();
        QueryResult GetAll(short? page, short? limit, string order, string search);
        Tv Add(Tv view);
        bool Update(Tv view);
        bool Remove(object id);
        Tv GetOne(object id);
        QueryResult Where(Expression<Func<Te, bool>> predicate, short? limit, string order);
    }
}
