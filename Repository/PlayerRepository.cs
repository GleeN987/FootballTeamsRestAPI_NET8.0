using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private AppDBContext _context;
        public PlayerRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Player> AddPlayerAsync(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<Player?> DeletePlayerAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return null;
            }
            _context.Remove(player);
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<Player?> EditPlayerAsync(PlayerDTO dto, string name)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Name == name);
            if (player == null)
            {
                return null;
            }
            player.Name = dto.Name;
            player.Age = dto.Age;
            player.Position = dto.Position;
            player.Nationality = dto.Nationality;
            await _context.SaveChangesAsync();
            return player;
        }

        public async Task<List<Player>> GetPlayersAsync()
        {
            var players = await _context.Players.ToListAsync();
            return players;
        }
    }
}