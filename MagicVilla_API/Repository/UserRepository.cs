using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.DTO;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_API.Repository {
    public class UserRepository : IUserRepository {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private string secretKey;

        public UserRepository(ApplicationDbContext db, IMapper mapper, IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
            this.db = db;
            this.mapper = mapper;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public bool IsUniqueUser(string username) {
            var user = db.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
            if (user == null) {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO) {
            var user = db.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

            bool isValid = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
            if (user == null || isValid == false) {
                return new LoginResponseDTO() {
                    Token = "",
                    User = null
                };
            }
            var roles = await userManager.GetRolesAsync(user);
            //generate JWT token by our defined secret key
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO() {
                Token = tokenHandler.WriteToken(token),
                User = mapper.Map<UserDTO>(user)
            };
            return loginResponseDTO;
        }

        public async Task<UserDTO> Register(RegistrationRequestDTO registrationRequestDTO) {
            ApplicationUser user = new() {
                UserName = registrationRequestDTO.UserName,
                Email = registrationRequestDTO.UserName,
                NormalizedEmail = registrationRequestDTO.UserName.ToUpper(),
                Name = registrationRequestDTO.Name
            };
            try {
                var result = await userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (result.Succeeded) {

                    //code to add role if not exist
                    if (!roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult()) {
                        await roleManager.CreateAsync(new IdentityRole("admin"));
                        await roleManager.CreateAsync(new IdentityRole("customer"));
                    }


                    await userManager.AddToRoleAsync(user, "admin");
                    var userToReturn = db.ApplicationUsers.FirstOrDefault(x => x.UserName == registrationRequestDTO.UserName);
                    return mapper.Map<UserDTO>(userToReturn);
                }
            } catch (Exception) {

                throw;
            }
            return new UserDTO();
        }
    }
}
