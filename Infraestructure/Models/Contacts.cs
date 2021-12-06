using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infraestructure.Models
{
   public class Contacts :BaseEntity
    {        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Company { get; set; }
        
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
