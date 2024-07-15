using AutoMapper;
using MagicVilla_API.DTO;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers {
    [Route("api/VillaNumberApi")]
    [ApiController]
    public class VillaNumberApiController : ControllerBase {
        private readonly IVillaNumberRepository dbVillaNo;
        private readonly IMapper mapper;
        protected APIResponse response;

        public VillaNumberApiController(IVillaNumberRepository dbVillaNo, IMapper mapper) {
            this.dbVillaNo = dbVillaNo;
            this.mapper = mapper;
            this.response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber() {
            try {
                response.Result = await dbVillaNo.GetAllAsync();
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            } catch (Exception ex) {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
            }
            return response;
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumberById(int id) {
            try {
                if (id == 0) {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                VillaNumber villaNo = await dbVillaNo.GetAsync(x => x.VillaNo == id);
                if (villaNo == null) {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    return NotFound(response);
                }
                response.Result = mapper.Map<VillaNumberDTO>(villaNo);
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);

            } catch (Exception ex) {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
            }
            return response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberDTOCreated villaNoCreated) {
            try {
                if (villaNoCreated == null) {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                if (await dbVillaNo.GetAsync(x => x.VillaNo == villaNoCreated.VillaNo) != null) {
                    ModelState.AddModelError("Custom Error", "Villa No is alreay exist");
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }                
                VillaNumber villaNumber = mapper.Map<VillaNumber>(villaNoCreated);
                await dbVillaNo.CreateAsync(villaNumber);
                response.Result = villaNumber;
                response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaNumber", new {id = villaNumber.VillaNo}, response);
            } catch (Exception ex) {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
            }
            return response;
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id) {
            try {
                if (id == null) {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
                VillaNumber villaNo = await dbVillaNo.GetAsync(x => x.VillaNo == id);
                if (villaNo == null) {
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.IsSuccess = false;
                    return NotFound(response);
                }
                await dbVillaNo.RemoveAsync(villaNo);
                response.StatusCode = HttpStatusCode.NoContent;
                return Ok(response);
            } catch (Exception ex) {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
            }
            return response;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaNumberDTOUpdated villaNumberDTOUpdated) {
            try {
                if (villaNumberDTOUpdated == null || id != villaNumberDTOUpdated.VillaNo) {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.IsSuccess = false;
                    return BadRequest(response);
                }
               
                //if (await dbVillaNo.GetAsync(x => x.VillaNo == villaNumberDTOUpdated.VillaNo) == null) {
                //    response.StatusCode = HttpStatusCode.NotFound;
                //    response.IsSuccess = false;
                //    return NotFound(response);
                //}
                VillaNumber model = mapper.Map<VillaNumber>(villaNumberDTOUpdated);
                await dbVillaNo.UpdateAsync(model);
                response.StatusCode = HttpStatusCode.NoContent;
                return Ok(response);
            } catch (Exception ex) {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
            }
            return response;
        }
    }
}
