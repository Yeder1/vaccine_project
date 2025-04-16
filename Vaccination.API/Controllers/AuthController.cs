using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vaccination.API.Helpers;
using Vaccination.BussinessLogic.DTOs.AuthDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;

namespace Vaccination.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly VaccinationManagementContext _context;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtHelper _jwtHelper;

        public AuthController(IConfiguration configuration, VaccinationManagementContext context, IRefreshTokenRepository refreshTokenRepository)
        {
            _configuration = configuration;
            _context = context;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtHelper = new JwtHelper(configuration);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            // Check is validate username and password
            var username = request.Username.Trim();
            var password = request.Password.Trim();
            var userId = "0";
            var userRole = "Admin";

            if (username.ToLower() == _configuration["AdminAccount:Username"].ToLower()
                && password.ToLower() == _configuration["AdminAccount:Password"].ToLower())
            {
                var accessToken = _jwtHelper.GenerateJwtToken(username, userRole);
                var refreshToken = _jwtHelper.GenerateRefreshToken();
                // Store the refresh token in the database
                //var refreshTokenEntity = new RefreshToken
                //{
                //    UserId = int.Parse(userId),
                //    Token = refreshToken,
                //    ExpiresAt = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JwtSettings:ExpireMinutes"])), // Set an appropriate expiration
                //    IsRevoked = false
                //};
                //_context.RefreshTokens.Add(refreshTokenEntity);
                await _refreshTokenRepository.Add(int.Parse(userId), refreshToken);

                var response = new LoginResponseDTO
                {
                    Username = username,
                    UserId = int.Parse(userId),
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                };
                return Ok(response);
            }
            return BadRequest("Email or password incorrect");
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDTO tokenDto)
        {
            // Extract userId from the JWT token
            var userId = 0;

            // Check if refresh token is valid and not expired or revoked
            var storedRefreshToken = await _refreshTokenRepository.checkRefreshToken(userId, tokenDto.RefreshToken);
            if (storedRefreshToken == null)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }

            // Generate new tokens
            var userRole = "Admin"; // Retrieve role from token if needed
            var newAccessToken = _jwtHelper.GenerateJwtToken(userId.ToString(), userRole);
            var newRefreshToken = _jwtHelper.GenerateRefreshToken();

            // Mark old token as revoked and add new token to the database
            storedRefreshToken.IsRevoked = true;

            // Create new refreshToken
            await _refreshTokenRepository.Add(userId, newRefreshToken);

            return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }
    }
}
