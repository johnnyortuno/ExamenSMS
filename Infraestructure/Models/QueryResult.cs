using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Models
{
    public class QueryResult
    {
        public IEnumerable Data { get; set; }
        public int Total { get; set; }
    }
}
