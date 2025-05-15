using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Models;

namespace api.Interfaces
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetPlayersAsync();
        Task<Player> AddPlayerAsync(Player player);
        Task<Player?> EditPlayerAsync(PlayerDTO dto, string name);
        Task<Player?> DeletePlayerAsync(int id);
    }
}