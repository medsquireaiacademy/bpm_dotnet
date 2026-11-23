using Microsoft.AspNetCore.Mvc;
using beeproperties.admin.webapi.models.DTO.Helper;
using beeproperties.admin.webapi.models.DTO.Response;

namespace beeproperties.admin.webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(SuccessResponse<FileUploadResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile? file, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new SuccessResponse("No file was uploaded.", StatusCodes.Status400BadRequest) { Success = false });
            }

            var webRoot = _environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var uploadsDir = Path.Combine(webRoot, "uploads");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            var ext = Path.GetExtension(file.FileName);
            var uniqueFileName = $"{Guid.NewGuid():N}{ext}";
            var filePath = Path.Combine(uploadsDir, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            var formattedSize = file.Length > 1024 * 1024
                ? $"{file.Length / (1024.0 * 1024.0):F1} MB"
                : $"{file.Length / 1024.0:F1} KB";

            var result = new FileUploadResponseDto
            {
                Url = $"/uploads/{uniqueFileName}",
                FileName = file.FileName,
                FileType = file.ContentType,
                FileSizeBytes = file.Length,
                FileSizeFormatted = formattedSize
            };

            return Ok(new SuccessResponse<FileUploadResponseDto>(result, "File uploaded successfully."));
        }
    }
}
