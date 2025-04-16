using AutoMapper;
using Microsoft.AspNetCore.Http;
using Vaccination.BussinessLogic.Commons.Constants;
using Vaccination.BussinessLogic.DTOs.ImageDTOs;
using Vaccination.DataAccess.Models;
using Vaccination.DataAccess.Repositories;

namespace Vaccination.BussinessLogic.Services.Impl
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private IMapper _mapper;

        public ImageService(IImageRepository imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public Image? GetImageByRef(string refId)
        {
            return _imageRepository.FindImage(i => i.RefId == refId);
        }

        public void AddImage(string refType, string refId, IFormFile file)
        {
            var imageDTO = new ImageDTO
            {
                RefId = $"{refType}_{refId}",
                Name = file.FileName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsDeleted = false
            };

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    imageDTO.Data = ms.ToArray();
                }
            }

            var image = _mapper.Map<Image>(imageDTO);

            _imageRepository.Add(image);
            _imageRepository.Save();
        }

        public void UpdateImage(string refType, string refId, IFormFile file)
        {
            var image = _imageRepository.FindImage(i => i.RefId == $"{refType}_{refId}");

            var imageDTO = new ImageDTO
            {
                RefId = $"{refType}_{refId}",
                Name = file.FileName,
                ModifiedDate = DateTime.Now,
                IsDeleted = false
            };

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    imageDTO.Data = ms.ToArray();
                }
            }

            if (image != null)
            {
                _mapper.Map<ImageDTO, Image>(imageDTO, image);
                image.CreatedDate = DateTime.Now;
                _imageRepository.Update(image);
            }
            else
            {
                image = _mapper.Map<Image>(imageDTO);
                _imageRepository.Add(image);
            }

            _imageRepository.Save();
        }
    }
}