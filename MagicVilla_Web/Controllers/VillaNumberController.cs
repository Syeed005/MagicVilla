
using AutoMapper;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using MagicVilla_Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_Web.Controllers {
    public class VillaNumberController : Controller {
        private readonly IVillaNumberService villaNumberService;
        private readonly IVillaService villaService;
        private readonly IMapper mapper;

        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService) {
            this.villaNumberService = villaNumberService;
            this.mapper = mapper;
            this.villaService = villaService;
        }

        public async Task<IActionResult> IndexVillaNumber() {
            List<VillaNumberDTO> list = new();
            var response = await villaNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) {
                list = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> CreateVillaNumber() {
            VillaNumberCreateViewModels villaNumberCreate = new();
            var response = await villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) {
                villaNumberCreate.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result)).Select(x => new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
            return View(villaNumberCreate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateViewModels model) {
            if (ModelState.IsValid) {
                var response = await villaNumberService.CreateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSuccess) {
                    TempData["success"] = "Villa number created successfully";
                    return RedirectToAction(nameof(IndexVillaNumber));
                } else {
                    if (response.ErrorMessage.Count > 0) {
                        ModelState.AddModelError("ErrorMessage", response.ErrorMessage.FirstOrDefault());
                    }
                }
            }
            var res = await villaService.GetAllAsync<APIResponse>();
            if (res != null && res.IsSuccess) {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(res.Result)).Select(x => new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
            TempData["error"] = "Error encountered!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateVillaNumber(int villaNo) {
            VillaNumberUpdateViewModels villaNumberUpdateVM = new();
            var response = await villaNumberService.GetAsync<APIResponse>(villaNo);
            if (response != null && response.IsSuccess) {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberUpdateVM.VillaNumber = mapper.Map<VillaNumberDTOUpdated>(model);
            }
            response = await villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) {
                villaNumberUpdateVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result)).Select(x => new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
                return View(villaNumberUpdateVM);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateViewModels model) {
            if (ModelState.IsValid) {
                var response = await villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber);
                if (response != null && response.IsSuccess) {
                    TempData["success"] = "Villa number updated successfully";
                    return RedirectToAction(nameof(IndexVillaNumber));
                } else {
                    if (response.ErrorMessage.Count > 0) {
                        ModelState.AddModelError("ErrorMessage", response.ErrorMessage.FirstOrDefault());
                    }
                }
            }
            var res = await villaService.GetAllAsync<APIResponse>();
            if (res != null && res.IsSuccess) {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(res.Result)).Select(x => new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
            TempData["error"] = "Error encountered!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteVillaNumber(int villaNo) {
            VillaNumberDeleteViewModels villaNumberVM = new();
            var response = await villaNumberService.GetAsync<APIResponse>(villaNo);
            if (response != null && response.IsSuccess) {
                VillaNumberDTO model = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                villaNumberVM.VillaNumber = mapper.Map<VillaNumberDTO>(model);
            }
            response = await villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result)).Select(x => new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
                return View(villaNumberVM);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteViewModels model) {
            if (ModelState.IsValid) {
                var response = await villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo);
                if (response != null && response.IsSuccess) {
                    TempData["success"] = "Villa number deleted successfully";
                    return RedirectToAction(nameof(IndexVillaNumber));
                } else {
                    if (response.ErrorMessage.Count > 0) {
                        ModelState.AddModelError("ErrorMessage", response.ErrorMessage.FirstOrDefault());
                    }
                }
            }
            var res = await villaService.GetAllAsync<APIResponse>();
            if (res != null && res.IsSuccess) {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(res.Result)).Select(x => new SelectListItem {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
            TempData["error"] = "Error encountered!";
            return View(model);
        }
    }
}
