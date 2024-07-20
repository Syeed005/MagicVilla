using MagicVilla_API.DTO;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/v{version:apiVersion}/UsersAuth")]
    [ApiController]
    [ApiVersionNeutral]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepo;
        private readonly APIResponse response;

        public UsersController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
            response = new();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await userRepo.Login(loginRequestDTO);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessage.Add("Username or password is incorrect");
                return BadRequest(response);
            }
            response.StatusCode = HttpStatusCode.OK;
            response.Result = loginResponse;
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            bool isUserUnique = userRepo.IsUniqueUser(registrationRequestDTO.Name);
            if (!isUserUnique)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessage.Add("Username already exists");
                return BadRequest(response);
            }
            var user = await userRepo.Register(registrationRequestDTO);
            if (user == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.IsSuccess = false;
                response.ErrorMessage.Add("Error while registering");
                return BadRequest(response);
            }
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response);
        }
    }
}
