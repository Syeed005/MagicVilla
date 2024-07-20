using MagicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModels {
    public class VillaNumberUpdateViewModels {
        public VillaNumberDTOUpdated VillaNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }

        public VillaNumberUpdateViewModels() {
            this.VillaNumber = new VillaNumberDTOUpdated();
        }
    }
}
