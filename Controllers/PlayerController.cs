using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs;
using api.Models;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

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
        public List<Player> GetPlayers()
        {
            var players = _context.Players.ToList();
            return players;
        }

        [HttpPost]
        public IActionResult AddPlayer([FromBody] PlayerDTO dto)
        {
            var player = dto.ToPlayerFromPlayerDTO();
            _context.Add(player);
            _context.SaveChanges();

            return Ok(dto);
        }

        [HttpPut("{name}")]
        public IActionResult EditPlayer([FromRoute] string name, [FromBody] PlayerDTO dto)
        {
            var player = _context.Players.FirstOrDefault(p => p.Name == name);
            if (player == null)
            {
                return NotFound();
            }
            player.Name = dto.Name;
            player.Age = dto.Age;
            player.Position = dto.Position;
            player.Nationality = dto.Nationality;

            _context.SaveChanges();

            return Ok(dto);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePlayer([FromRoute] int id)
        {
            var player = _context.Players.FirstOrDefault(p => p.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            _context.Remove(player);
            _context.SaveChanges();

            return Ok(player);
        }


    
    }
}