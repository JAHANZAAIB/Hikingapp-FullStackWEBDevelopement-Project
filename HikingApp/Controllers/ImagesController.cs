using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.CompilerServices;
using HikingApp.Models.Domain;
using HikingApp.Models.DTO;
using HikingApp.Repositories;

namespace HikingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public ImagesController(IMapper mapper,IImageRepository imageRepository)
        {
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload ([FromForm] ImageUploadReqDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var images = new Image
                {
                    File = request.File,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length
                };

                images = await imageRepository.Upload(images);

                return Ok(mapper.Map<ImageResponseDto>(images));
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadReqDto request)
        {
            var allowedExtensions = new string[] { ".jpeg", ".jpg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File Size More Than 10MB");
            }
        }
    }
}
