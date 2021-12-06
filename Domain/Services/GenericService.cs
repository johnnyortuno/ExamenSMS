using AutoMapper;
using Domain.Domain;
using Infraestructure;
using Infraestructure.Models;
using Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
 
namespace Domain.Services
{
    public class GenericService<Tv, Te> : IService<Tv, Te> where Tv : BaseDomain
                                     where Te : BaseEntity

    {

        protected IUnitofWork _unitOfWork;
        public GenericService(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public GenericService() { }

        public virtual QueryResult GetAll()
        {
            return GetAll(null, null, null, null);
        }

        public virtual QueryResult GetAll(short? page, short? limit, string order, string search)
        {
            var qo = new QueryOptions() { Page = page, Limit = limit, Order = order, Search = search };

            var qr = _unitOfWork.GetRepository<Te>().GetAll(qo);
            //qr.Data = Mapper.Map<IEnumerable<Tv>>(source: qr.Data);
            return qr;
        }

        public virtual Tv GetOne(object id)
        {
            var entity = _unitOfWork.GetRepository<Te>()
                .GetOne(predicate: x => x.IdContact == (int)id);
            return Mapper.Map<Tv>(source: entity);
        }
 

        public virtual Tv Add(Tv view)
        {
            var entity = Mapper.Map<Te>(source: view);
            _unitOfWork.GetRepository<Te>().Insert(entity);
            _unitOfWork.Save();
            return Mapper.Map<Tv>(source: entity); //GetOne(entity.Id);
        }

        public virtual bool Update(Tv view)
        {
            _unitOfWork.GetRepository<Te>().Update(view.IdContact, Mapper.Map<Te>(source: view));
            return _unitOfWork.Save();
        }


        public virtual bool Remove(object id)
        {
            _unitOfWork.GetRepository<Te>().Delete(id);
            return _unitOfWork.Save();
        }

        public virtual QueryResult Where(Expression<Func<Te, bool>> predicate, short? limit, string order)
        {
            var qo = new QueryOptions() { Limit = limit, Order = order };
            var qr = _unitOfWork.GetRepository<Te>()
                .Get(predicate: predicate, queryOptions: qo);
            qr.Data = Mapper.Map<IEnumerable<Tv>>(source: qr.Data);
            return qr;
        }

 
    }
}
