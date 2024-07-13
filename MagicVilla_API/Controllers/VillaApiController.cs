using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.DTO;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase {
        private readonly IVillaRepository _dbVilla;
        private readonly ILogger<VillaApiController> _logger;
        private readonly IMapper _mapper;

        public VillaApiController(ILogger<VillaApiController> logger, IVillaRepository dbVilla, IMapper mapper)
        {
            this._logger = logger;
            this._dbVilla = dbVilla;
            this._mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas() {
            _logger.LogInformation("Get all villas");
            IEnumerable<Villa> villa = await _dbVilla.GetAllAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villa));
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDTO))]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id) {
            if (id == 0) {
                return BadRequest();
            }
            Villa villa = await _dbVilla.GetAsync(x => x.Id == id);
            if (villa == null) {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaDTOCreated villaCreateDTO) {
            if (await _dbVilla.GetAsync(x => x.Name.ToLower() == villaCreateDTO.Name.ToLower()) != null) {
                ModelState.AddModelError("Custom Error", "Villa named already exist");
                return BadRequest(ModelState);
            }

            if (villaCreateDTO == null) {
                return BadRequest();
            }

            Villa villa = _mapper.Map<Villa>(villaCreateDTO);
            
            await _dbVilla.CreateAsync(villa);
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(int id) {
            if (id == null) {
                return BadRequest();
            }
            Villa villa = await _dbVilla.GetAsync(x => x.Id == id);
            if (villa == null) {
                return NotFound();
            }
            await _dbVilla.RemoveAsync(villa);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaDTOUpdated villaUpdateDTO) {
            if (villaUpdateDTO == null || id != villaUpdateDTO.Id) {
                return BadRequest();
            }

            Villa villa = _mapper.Map<Villa>(villaUpdateDTO);

            await _dbVilla.UpdateAsync(villa);
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdateVillaPartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchVilla(int id, JsonPatchDocument<VillaDTOUpdated> patchDTO) {
            if (id == 0 || patchDTO == null) {
                return BadRequest();
            }
            //Villa villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            Villa villa = await _dbVilla.GetAsync(x => x.Id == id, tracked: false);
            VillaDTOUpdated villaDTO = _mapper.Map<VillaDTOUpdated>(villa);
            
            if (villa == null) {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaDTO,ModelState);
            Villa model = _mapper.Map<Villa>(villaDTO);
            
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            await _dbVilla.UpdateAsync(model);
            return NoContent();
        }
    }
}
