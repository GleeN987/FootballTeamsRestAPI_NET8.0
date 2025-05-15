using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Models;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _context.Teams.ToListAsync();
            var teamsDto = teams.Select(t => t.TeamDTOFromTeam());
            
            return Ok(teamsDto);
        }

        [HttpGet]
        [Route("{id:int}/squad")]
        public async Task<IActionResult> GetTeamById([FromRoute] int id)
        {
            var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            var teamDto = team.TeamDTOWithPlayersFromTeam();
            return Ok(teamDto);
        }

        [HttpGet("{league}")]
        public async Task<IActionResult> GetTeamsByLeague([FromRoute] string league)
        {
            var teams = await _context.Teams
            .Where(t => t.League == league)
            .ToListAsync();

            var teamsDto = teams.Select(t => t.TeamDTOFromTeam());
            

            return Ok(teamsDto);
            
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam([FromBody] CreateTeamDTO dto)
        {
            var team = dto.TeamFromCreateTeamDTO();
            await _context.AddAsync(team);
            await _context.SaveChangesAsync();
            return Ok(team.TeamDTOFromTeam());

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditTeam([FromBody] TeamDTO dto, [FromRoute] int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
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

            await _context.SaveChangesAsync();
            return Ok(team.TeamDTOFromTeam());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTeam([FromRoute] int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (team == null)
            {
                return NotFound();
            }
            _context.Remove(team);
            await _context.SaveChangesAsync();
            return Ok(team.TeamDTOFromTeam());
        }
    }
}