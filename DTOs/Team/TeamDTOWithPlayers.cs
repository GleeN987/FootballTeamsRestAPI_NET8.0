using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.DTOs
{
    public class TeamDTOWithPlayers
    {
        public string Name { get; set; } = string.Empty;
        public List<PlayerDTO> Players { get; set; } = new List<PlayerDTO>();
    }
}