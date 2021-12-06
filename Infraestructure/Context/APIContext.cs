using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Context
{
   public class APIContext :DbContext
    {
        public APIContext(DbContextOptions<APIContext> options):base(options)
        {

        }

        public DbSet<Contacts> contacts { get; set; }
    }
}
