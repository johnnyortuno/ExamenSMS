using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Domain
{
  public  class ContactViewModel : BaseDomain
    {
 
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Company { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
