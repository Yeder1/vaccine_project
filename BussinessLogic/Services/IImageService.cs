using Microsoft.AspNetCore.Http;
using Vaccination.DataAccess.Models;

namespace Vaccination.BussinessLogic.Services
{
    public interface IImageService
    {
        Image? GetImageByRef(string refId);

        void AddImage(string refType, string refId, IFormFile file);

        void UpdateImage(string refType, string refId, IFormFile file);
    }
}