using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs
{
    public class PlayerDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Position { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
    }
}