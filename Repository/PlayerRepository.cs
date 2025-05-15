using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Helpers;
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

        public async Task<Player?> EditPlayerAsync(CreatePlayerDTO dto, string name)
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

        public async Task<List<Player>> GetPlayersAsync(QueryObjectPlayers query)
        {
            var players = _context.Players.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                players = players.Where(p => p.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.Nationality))
            {
                players = players.Where(p => p.Nationality.Contains(query.Nationality));
            }
            if (query.MinAge.HasValue)
            {
                players = players.Where(p => p.Age >= query.MinAge);
            }
            if (query.MaxAge.HasValue)
            {
                players = players.Where(p => p.Age <= query.MaxAge);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    players = query.Ascending ? players.OrderBy(p => p.Name) : players.OrderByDescending(p => p.Name);
                }
                if (query.SortBy.Equals("Age", StringComparison.OrdinalIgnoreCase))
                {
                    players = query.Ascending ? players.OrderBy(p => p.Age) : players.OrderByDescending(p => p.Age);
                }    
            }
            var PagesToSkip = (query.PageNum - 1) * query.PageSize;
            return await players.Skip(PagesToSkip).Take(query.PageSize).ToListAsync();
        }
    }
}