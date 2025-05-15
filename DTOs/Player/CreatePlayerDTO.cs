using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs
{
    public class CreatePlayerDTO
    {
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(15,50)]
        public int Age { get; set; }
        [Required]
        [MinLength(5), MaxLength(20)]
        public string Position { get; set; } = string.Empty;
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Nationality { get; set; } = string.Empty;
        public int? TeamId { get; set; }
    }
}