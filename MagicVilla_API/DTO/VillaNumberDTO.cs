using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.DTO
{
    public class VillaNumberDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
