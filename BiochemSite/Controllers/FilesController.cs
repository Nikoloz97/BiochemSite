using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace BiochemSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        // Inject service (allows files to actually be able to be downloaded by user; see program)

        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;
        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider) 
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider ?? 
                                                throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }

        [HttpGet("{directoryName}/{fileName}")]
        public ActionResult GetMP4Video(string directoryName, string fileName)
        {
            var pathToFile = $"{directoryName}/{fileName}.mp4";

            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
            {
                // This is a "catch-all" for files that this service cannot read
                contentType = "application/octet-stream";
            }
            
            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}
