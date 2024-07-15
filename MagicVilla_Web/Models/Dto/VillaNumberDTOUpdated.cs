﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.DTO
{
    public class VillaNumberDTOUpdated
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
