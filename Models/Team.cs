using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameShort { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string League { get; set; } = string.Empty;
        public string Stadium { get; set; } = string.Empty;
        public string Coach { get; set; } = string.Empty;
        public List<Player> Players { get; set; } = new List<Player>();
    }
}