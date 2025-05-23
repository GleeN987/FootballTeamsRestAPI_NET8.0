using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Models;

namespace api.Mappers
{
    public static class PlayerMappers
    {
        public static PlayerDTO ToPlayerDTO(this Player player)
        {
            return new PlayerDTO
            {
                Name = player.Name,
                Age = player.Age,
                Position = player.Position,
                Nationality = player.Nationality,
                TeamId = player.TeamId

            };
        }

        public static Player ToPlayerFromPlayerDTO(this PlayerDTO dto)
        {
            return new Player
            {
                Name = dto.Name,
                Age = dto.Age,
                Position = dto.Position,
                Nationality = dto.Nationality,
                TeamId = dto.TeamId
            };
        }
           public static Player ToPlayerFromCreatePlayerDTO(this CreatePlayerDTO dto)
        {
            return new Player
            {
                Name = dto.Name,
                Age = dto.Age,
                Position = dto.Position,
                Nationality = dto.Nationality,
                TeamId = dto.TeamId
            };
        }
    }
}