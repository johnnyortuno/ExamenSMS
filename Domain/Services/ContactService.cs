using Domain.Domain;
using Infraestructure.Models;
using Infraestructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{

 
    public class ContactService<T> : GenericService<T, Contacts> 
        where T : ContactViewModel
    {
        public ContactService(IUnitofWork unitOfWork)
        {
            if (_unitOfWork == null)
            {
                _unitOfWork = unitOfWork;
            }
        }

    }
}
