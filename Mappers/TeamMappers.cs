using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Models;

namespace api.Mappers
{
    public static class TeamMappers
    {
        public static Team TeamFromCreateTeamDTO(this CreateTeamDTO dto)
        {
            return new Team
            {
                Name = dto.Name,
                NameShort = dto.NameShort,
                Country = dto.Country,
                League = dto.League,
                Stadium = dto.Stadium,
                Coach = dto.Coach
            };
        }

        public static TeamDTO TeamDTOFromTeam(this Team team)
        {
            return new TeamDTO
            {
                Name = team.Name,
                NameShort = team.NameShort,
                Country = team.Country,
                League = team.League,
                Stadium = team.Stadium,
                Coach = team.Coach
            };
        }

        public static TeamDTOWithPlayers TeamDTOWithPlayersFromTeam(this Team team)
        {
            return new TeamDTOWithPlayers
            {
                Name = team.Name,
                Players = team.Players.Select(p => p.ToPlayerDTO()).ToList()
            };
        }
    }
}