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
using api.Interfaces;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private AppDBContext _context;
        private ITeamRepository _repo;
        public TeamController(AppDBContext context, ITeamRepository repo)
        {
            _context = context;
            _repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _repo.GetTeamsAsync();
            var teamsDto = teams.Select(t => t.TeamDTOFromTeam());
            
            return Ok(teamsDto);
        }

        [HttpGet]
        [Route("squad/{id:int}")]
        public async Task<IActionResult> GetTeamById([FromRoute] int id)
        {
            var team = await _repo.GetTeamByIdAsync(id);
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
            var teams = await _repo.GetTeamsByLeagueAsync(league);
            if (teams.Count == 0)
            {
                return NotFound();
            }
            var teamsDto = teams.Select(t => t.TeamDTOFromTeam());
            return Ok(teamsDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam([FromBody] CreateTeamDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var team = dto.TeamFromCreateTeamDTO();
            await _repo.AddTeamAsync(team);
            return Ok(team.TeamDTOFromTeam());

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> EditTeam([FromBody] CreateTeamDTO dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var team = await _repo.EditTeamAsync(dto, id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team.TeamDTOFromTeam());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTeam([FromRoute] int id)
        {
            var team = await _repo.DeleteTeamAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team.TeamDTOFromTeam());
        }
    }
}