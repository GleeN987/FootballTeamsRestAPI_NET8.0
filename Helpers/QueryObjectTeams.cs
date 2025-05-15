using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObjectTeams
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? League { get; set; }
        public string? SortBy { get; set; }
        public bool Ascending { get; set; } = true;
        public int PageSize { get; set; } = 5;
        public int PageNum { get; set; } = 1;
    }
}