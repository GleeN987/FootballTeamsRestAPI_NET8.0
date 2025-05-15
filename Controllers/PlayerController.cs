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
    public class PlayerController : ControllerBase
    {
        private AppDBContext _context;
        private IPlayerRepository _repo;
        public PlayerController(AppDBContext context, IPlayerRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _repo.GetPlayersAsync();
            var playersDto = players.Select(p => p.ToPlayerDTO());
            return Ok(playersDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] PlayerDTO dto)
        {
            var player = dto.ToPlayerFromPlayerDTO();
            await _repo.AddPlayerAsync(player);
            return Ok(dto);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> EditPlayer([FromRoute] string name, [FromBody] PlayerDTO dto)
        {
            var player = await _repo.EditPlayerAsync(dto, name);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePlayer([FromRoute] int id)
        {
            var player = await _repo.DeletePlayerAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player.ToPlayerDTO());
        }
    }
}