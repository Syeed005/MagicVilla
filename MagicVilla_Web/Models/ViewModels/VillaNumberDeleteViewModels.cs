﻿using MagicVilla_Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Models.ViewModels {
    public class VillaNumberDeleteViewModels {
        public VillaNumberDTO VillaNumber { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> VillaList { get; set; }

        public VillaNumberDeleteViewModels() {
            this.VillaNumber = new VillaNumberDTO();
        }
    }
}
