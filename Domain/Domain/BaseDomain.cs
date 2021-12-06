using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Domain
{
    public class BaseDomain
    {
        [Required]
        public int IdContact { get; set; }
    }
}
