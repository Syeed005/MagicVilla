using AutoMapper;
using MagicVilla_API.DTO;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/VillaNumberApi")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaNumberApiController : ControllerBase
    {
        private readonly IVillaNumberRepository dbVillaNo;
        private readonly IVillaRepository dbVill;
        private readonly IMapper mapper;
        protected APIResponse response;

        public VillaNumberApiController(IVillaNumberRepository dbVillaNo, IMapper mapper, IVillaRepository dbVill)
        {
            this.dbVillaNo = dbVillaNo;
            this.mapper = mapper;
            response = new APIResponse();
            this.dbVill = dbVill;
        }


        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Samaira", "Shanaya" };
        }
    }
}
