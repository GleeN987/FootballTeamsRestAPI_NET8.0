using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Models;

namespace api.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetTeamsAsync();
        Task<Team?> GetTeamByIdAsync(int id);
        Task<List<Team>> GetTeamsByLeagueAsync(string league);
        Task<Team> AddTeamAsync(Team team);
        Task<Team?> EditTeamAsync(TeamDTO dto, int id);
        Task<Team?> DeleteTeamAsync(int id);
    }
}