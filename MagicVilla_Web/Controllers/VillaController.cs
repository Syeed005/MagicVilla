
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
    public class VillaController : Controller {
        private readonly IVillaService villaService;
        private readonly IMapper mapper;

        public VillaController(IVillaService villaService, IMapper mapper) {
            this.villaService = villaService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> IndexVilla() {
            List<VillaDTO> list = new();
            var response = await villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess) {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> CreateVilla() {            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaDTOCreated model) {
            if (ModelState.IsValid) {
                var response = await villaService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess) {
                    TempData["success"] = "Villa created successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateVilla(int villaId) {
            var response = await villaService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess) {
                VillaDTO villaDTO = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(mapper.Map<VillaDTOUpdated>(villaDTO));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaDTOUpdated model) {
            if (ModelState.IsValid) {
                var response = await villaService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess) {
                    TempData["success"] = "Villa updated successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error encountered!";
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteVilla(int villaId) {
            var response = await villaService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess) {
                VillaDTO villaDTO = JsonConvert.DeserializeObject<VillaDTO>(Convert.ToString(response.Result));
                return View(villaDTO);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO model) {
            var response = await villaService.DeleteAsync<APIResponse>(model.Id);
            if (response != null && response.IsSuccess) {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(IndexVilla));
            }
            TempData["error"] = "Error encountered!";
            return View(model);
        }
    }
}
