using Microsoft.AspNetCore.Mvc;
using Vaccination.BussinessLogic.Commons.Constants;
using Vaccination.BussinessLogic.Services;

namespace Vaccination.API.Controllers
{
    [Route("api/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("ref/{refId}")]
        public IActionResult GetImageByRef(string refId)
        {
            try
            {
                var image = _imageService.GetImageByRef(refId);
                if (image != null)
                {
                    var stream = new MemoryStream(image.Data);
                    var imageFile = File(
                        fileStream: stream,
                        contentType: MimeType.Image,
                        fileDownloadName: image.Name,
                        enableRangeProcessing: true);
                    return imageFile;
                } 
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
                throw;
            }
        }
    }
}