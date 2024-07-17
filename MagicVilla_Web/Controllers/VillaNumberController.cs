
using AutoMapper;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Models;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MagicVilla_Web.Controllers {
    public class VillaNumberController : Controller {
        private readonly IVillaNumberService villaNumberService;
        private readonly IMapper mapper;

        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper) {
            this.villaNumberService = villaNumberService;
            this.mapper = mapper;
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberDTOCreated model) {
            if (ModelState.IsValid) {
                var response = await villaNumberService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess) {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateVillaNumber(int villaNumberId) {
            var response = await villaNumberService.GetAsync<APIResponse>(villaNumberId);
            if (response != null && response.IsSuccess) {
                VillaNumberDTO villaDTO = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                return View(mapper.Map<VillaNumberDTOUpdated>(villaDTO));
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberDTOUpdated model) {
            if (ModelState.IsValid) {
                var response = await villaNumberService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess) {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteVillaNumber(int villaId) {
            var response = await villaNumberService.GetAsync<APIResponse>(villaId);
            if (response != null && response.IsSuccess) {
                VillaNumberDTO villaDTO = JsonConvert.DeserializeObject<VillaNumberDTO>(Convert.ToString(response.Result));
                return View(villaDTO);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDTO model) {
            var response = await villaNumberService.DeleteAsync<APIResponse>(model.VillaNo);
            if (response != null && response.IsSuccess) {
                return RedirectToAction(nameof(IndexVillaNumber));
            }

            return View(model);
        }
    }
}
