using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.DTO
{
    public class VillaNumberDTOCreated
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
    }
}
