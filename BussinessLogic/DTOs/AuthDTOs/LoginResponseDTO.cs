namespace Vaccination.BussinessLogic.DTOs.AuthDTOs
{
    public class LoginResponseDTO
    {
        public string Username { get; set; }
        public int UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
