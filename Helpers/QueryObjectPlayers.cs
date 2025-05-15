using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObjectPlayers
    {

        public string? Name { get; set; }
        public string? Nationality { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string? SortBy { get; set; }
        public bool Ascending { get; set; } = true;
        public int PageSize { get; set; } = 5;
        public int PageNum { get; set; } = 1;
    }
}