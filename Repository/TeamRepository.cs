using System;
using System.Collections.Generic;
using System.Linq;
using api.DTOs;
using api.Models;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Helpers;
using Microsoft.IdentityModel.Tokens;

namespace api.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private AppDBContext _context;
        public TeamRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Team> AddTeamAsync(Team team)
        {
            await _context.AddAsync(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<Team?> DeleteTeamAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return null;
            }
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<Team?> EditTeamAsync(CreateTeamDTO dto, int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return null;
            }
            team.Name = dto.Name;
            team.NameShort = dto.NameShort;
            team.League = dto.League;
            team.Country = dto.Country;
            team.Coach = dto.Coach;
            team.Stadium = dto.Stadium;
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<Team?> GetTeamByIdAsync(int id)
        {
            var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t=>t.Id == id);
            if (team == null)
            {
                return null;
            }
            return team;
        }

        public async Task<List<Team>> GetTeamsAsync(QueryObjectTeams query)
        {
            var teams = _context.Teams.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                teams = teams.Where(t => t.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.Country))
            {
                teams = teams.Where(t => t.Country.Contains(query.Country));
            }
            if (!string.IsNullOrWhiteSpace(query.League))
            {
                teams = teams.Where(t => t.League.Contains(query.League));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    teams = query.Ascending ? teams.OrderBy(p => p.Name) : teams.OrderByDescending(p => p.Name);
                }
            }   
            var PagesToSkip = (query.PageNum - 1) * query.PageSize;
            return await teams.Skip(PagesToSkip).Take(query.PageSize).ToListAsync();
        }

        public async Task<List<Team>> GetTeamsByLeagueAsync(string league)
        {
            var teams = await _context.Teams.Where(t => t.League == league).ToListAsync();
            if (teams == null)
            {
                return null;
            }
            return teams;
        }
    }
}