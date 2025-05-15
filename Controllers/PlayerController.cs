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
    public class PlayerController : ControllerBase
    {
        private AppDBContext _context;
        public PlayerController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _context.Players
            .Include(p => p.Team)
            .ToListAsync();

            var playersDto = players.Select(p => p.ToPlayerDTO());
            
            return Ok(playersDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] PlayerDTO dto)
        {
            var player = dto.ToPlayerFromPlayerDTO();
            await _context.AddAsync(player);
            await _context.SaveChangesAsync();

            return Ok(dto);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> EditPlayer([FromRoute] string name, [FromBody] PlayerDTO dto)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Name == name);
            if (player == null)
            {
                return NotFound();
            }
            player.Name = dto.Name;
            player.Age = dto.Age;
            player.Position = dto.Position;
            player.Nationality = dto.Nationality;

            await _context.SaveChangesAsync();

            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] int id)
        {
            var player = await _context.Players.FirstOrDefaultAsync(p => p.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            _context.Remove(player);
            await _context.SaveChangesAsync();

            return Ok(player);
        }
    }
}