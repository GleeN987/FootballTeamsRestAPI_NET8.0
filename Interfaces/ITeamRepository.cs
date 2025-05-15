using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetTeamsAsync(QueryObjectTeams query);
        Task<Team?> GetTeamByIdAsync(int id);
        Task<List<Team>> GetTeamsByLeagueAsync(string league);
        Task<Team> AddTeamAsync(Team team);
        Task<Team?> EditTeamAsync(CreateTeamDTO dto, int id);
        Task<Team?> DeleteTeamAsync(int id);
    }
}