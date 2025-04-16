using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Vaccination.DataAccess.Models;

namespace Vaccination.DataAccess.Repositories.Impl
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly VaccinationManagementContext _context;
        private readonly IConfiguration _configuration;

        public RefreshTokenRepository(VaccinationManagementContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<RefreshToken> Add(int userId, string refreshToken)
        {
            var refreshTokenEntity = new RefreshToken
            {
                UserId = userId,
                Token = refreshToken,
                ExpiresAt = DateTime.Now.AddDays(7), // Set an appropriate expiration
                IsRevoked = false
            };
            _context.RefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync();
            return refreshTokenEntity;
        }

        public async Task<RefreshToken?> checkRefreshToken(int userId, string refreshToken)
        {
            var storedRefreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == userId && rt.Token == refreshToken && !rt.IsRevoked && rt.ExpiresAt > DateTime.Now);
            return storedRefreshToken;
        }
    }
}
