using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructure.Models
{
    public class QueryOptions
    {
        public short? Page { get; set; }
        private short? _limit;
        public short? Limit
        {
            get => _limit ?? 10;
            set => _limit = value ?? _limit;
        }
        public string Order { get; set; }
        public string Search { get; set; }
    }
}
