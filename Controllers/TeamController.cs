using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Models;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private AppDBContext _context;
        public TeamController(AppDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<TeamDTO> GetTeams()
        {
            var teamsDto = _context.Teams
            .ToList()
            .Select(t => t.TeamDTOFromTeam())
            .ToList();

            return teamsDto;
        }

        [HttpGet]
        [Route("{id:int}/squad")]
        public TeamDTOWithPlayers GetTeamById([FromRoute] int id)
        {
            var team = _context.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == id);
            if (team == null)
            {
                return null;
            }

            var teamDto = team.TeamDTOWithPlayersFromTeam();
            return teamDto;
        }

        [HttpGet("{league}")]
        public List<TeamDTO> GetTeamsByLeague([FromRoute] string league)
        {
            var teamsDto = _context.Teams
            .Where(t => t.League == league)
            .ToList()
            .Select(t => t.TeamDTOFromTeam())
            .ToList();

            return teamsDto;
            
        }

        [HttpPost]
        public IActionResult AddTeam([FromBody] CreateTeamDTO dto)
        {
            var team = dto.TeamFromCreateTeamDTO();
            _context.Add(team);
            _context.SaveChanges();
            return Ok(team.TeamDTOFromTeam());

        }

        [HttpPut("{id:int}")]
        public IActionResult EditTeam([FromBody] TeamDTO dto, [FromRoute] int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            team.Name = dto.Name;
            team.NameShort = dto.NameShort;
            team.League = dto.League;
            team.Country = dto.Country;
            team.Coach = dto.Coach;
            team.Stadium = dto.Stadium;

            _context.SaveChanges();
            return Ok(team.TeamDTOFromTeam());
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTeam([FromRoute] int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            _context.Remove(team);
            _context.SaveChanges();
            return Ok(team.TeamDTOFromTeam());
        }
    }
}