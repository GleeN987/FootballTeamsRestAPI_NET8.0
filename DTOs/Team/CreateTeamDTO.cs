using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs
{
    public class CreateTeamDTO
    {
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MinLength(2), MaxLength(4)]
        public string NameShort { get; set; } = string.Empty;
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Country { get; set; } = string.Empty;
        [Required]
        [MinLength(3), MaxLength(100)]
        public string League { get; set; } = string.Empty;
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Stadium { get; set; } = string.Empty; 
        [Required]
        [MinLength(3), MaxLength(100)]
        public string Coach { get; set; } = string.Empty;
    }
}