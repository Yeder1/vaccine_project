namespace Vaccination.API.Helpers
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;

    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // 1. Phương thức tạo JWT Token
        public string GenerateJwtToken(string userId, string userRole)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Role, userRole)
                }),
                Expires = DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:ExpireMinutes"])),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

        // 2. Phương thức xác thực JWT Token
        public ClaimsPrincipal ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtSettings:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero // Không cho phép thời gian chậm trễ
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Kiểm tra nếu token không phải là JWT hợp lệ
                if (validatedToken is JwtSecurityToken jwtToken && jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal; // Token hợp lệ
                }
            }
            catch (Exception)
            {
                // Token không hợp lệ
                return null;
            }

            return null;
        }

        // 3. Phương thức kiểm tra JWT Token có hợp lệ hay không (Optional)
        public bool IsJwtValid(string token)
        {
            var principal = ValidateJwtToken(token);
            return principal != null;
        }

        // 4. Phương thức lấy thông tin người dùng từ JWT Token
        public string GetUserIdFromJwtToken(string token)
        {
            var principal = ValidateJwtToken(token);
            if (principal == null)
            {
                return null;
            }

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return userIdClaim?.Value;
        }

        // 5. Phương thức lấy vai trò (role) từ JWT Token
        public string GetUserRoleFromJwtToken(string token)
        {
            var principal = ValidateJwtToken(token);
            if (principal == null)
            {
                return null;
            }

            var roleClaim = principal.FindFirst(ClaimTypes.Role);
            return roleClaim?.Value;
        }
    }

}
