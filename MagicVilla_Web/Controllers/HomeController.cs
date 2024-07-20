using AutoMapper;
using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla_Web.Controllers {
    public class HomeController : Controller {
        private readonly IVillaService villaService;
        private readonly IMapper mapper;

        public HomeController(IVillaService villaService, IMapper mapper) {
            this.villaService = villaService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index() {
            List<VillaDTO> list = new();
            var response = await villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess) {
                list = JsonConvert.DeserializeObject<List<VillaDTO>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
