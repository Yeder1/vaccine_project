using Vaccination.DataAccess.Models;
namespace Vaccination.DataAccess.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> Add(int userId, string refreshToken);
        Task<RefreshToken?> checkRefreshToken(int userId, string refreshToken);
    }
}
