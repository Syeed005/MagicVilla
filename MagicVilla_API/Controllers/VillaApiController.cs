using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.DTO;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_API.Controllers {
    //[Route("api/[controller]")]
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase {
        protected APIResponse response;
        private readonly IVillaRepository _dbVilla;
        private readonly ILogger<VillaApiController> _logger;
        private readonly IMapper _mapper;

        public VillaApiController(ILogger<VillaApiController> logger, IVillaRepository dbVilla, IMapper mapper) {
            this._logger = logger;
            this._dbVilla = dbVilla;
            this._mapper = mapper;
            this.response = new();
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillas() {
            try {
                //_logger.LogInformation("Get all villas");
                response.Result = await _dbVilla.GetAllAsync();
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            } catch (Exception ex) {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
            }
            return response;
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDTO))]
        public async Task<ActionResult<APIResponse>> GetVilla(int id) {
            try {
                if (id == 0) {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(response);
                }
                Villa villa = await _dbVilla.GetAsync(x => x.Id == id);
                if (villa == null) {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(response);
                }
                response.Result = _mapper.Map<VillaDTO>(villa);
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
        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaDTOCreated villaCreateDTO) {
            try {
                if (await _dbVilla.GetAsync(x => x.Name.ToLower() == villaCreateDTO.Name.ToLower()) != null) {
                    ModelState.AddModelError("Custom Error", "Villa named already exist");
                    return BadRequest(ModelState);
                }

                if (villaCreateDTO == null) {
                    return BadRequest();
                }

                Villa villa = _mapper.Map<Villa>(villaCreateDTO);

                await _dbVilla.CreateAsync(villa);
                response.Result = _mapper.Map<VillaDTO>(villa);
                response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, response);
            } catch (Exception ex) {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
            }
            return response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id) {
            try {
                if (id == null) {
                    return BadRequest();
                }
                Villa villa = await _dbVilla.GetAsync(x => x.Id == id);
                if (villa == null) {
                    return NotFound();
                }
                await _dbVilla.RemoveAsync(villa);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            } catch (Exception ex) {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
            }
            return response;
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody] VillaDTOUpdated villaUpdateDTO) {
            try {
                if (villaUpdateDTO == null || id != villaUpdateDTO.Id) {
                    return BadRequest();
                }

                Villa villa = _mapper.Map<Villa>(villaUpdateDTO);

                await _dbVilla.UpdateAsync(villa);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            } catch (Exception ex) {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
            }
            return response;
        }

        [HttpPatch("{id:int}", Name = "UpdateVillaPartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> PatchVilla(int id, JsonPatchDocument<VillaDTOUpdated> patchDTO) {
            try {
                if (id == 0 || patchDTO == null) {
                    return BadRequest();
                }
                //Villa villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                Villa villa = await _dbVilla.GetAsync(x => x.Id == id, tracked: false);
                VillaDTOUpdated villaDTO = _mapper.Map<VillaDTOUpdated>(villa);

                if (villa == null) {
                    return BadRequest();
                }
                patchDTO.ApplyTo(villaDTO, ModelState);
                Villa model = _mapper.Map<Villa>(villaDTO);

                if (!ModelState.IsValid) {
                    return BadRequest();
                }
                await _dbVilla.UpdateAsync(model);
                response.StatusCode = HttpStatusCode.NoContent;
                response.IsSuccess = true;
                return Ok(response);
            } catch (Exception ex) {
                response.IsSuccess = false;
                response.ErrorMessage = new List<string>() { ex.Message };
            }
            return response;
        }
    }
}
