using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShipmentAPI.Interfaces;
using ShipmentAPI.Model;

namespace ShipmentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        public IConfiguration _configuration;
        private IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _configuration = config;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _unitOfWork.User.GetAll().Include(x => x.Shipments).ToListAsync();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                _unitOfWork.User.Add(user);
                await _unitOfWork.Save();
                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, user));

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDTO _userData)
        {


            if (_userData != null && _userData.Email != null && _userData.Password != null)
            {
                var user = await _unitOfWork.User.GetUser(_userData);

                if (user != null)
                {
                    var token = GenerateToken(user);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private string GenerateToken(User user)
        {

            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.UserData, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role),
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(200),
                signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}