using Microsoft.AspNetCore.Mvc;
using Server.Core.Services;
using Server.Core.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _FileService;

        public FilesController(IFileService FileService)
        {
            _FileService = FileService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetrFile(string id)
        {
            var oFile = await _FileService.GetFileAsync(id);
            if (oFile == null)
                return NotFound();
            return Ok(oFile);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            var orderFiles = await _FileService.GetAllFilesAsync();
            return Ok(orderFiles);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile([FromBody] OFile orderFile)
        {
            if (orderFile == null)
                return BadRequest();
            await _FileService.AddFileAsync(orderFile);
             return   Ok(orderFile);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFile(string id, [FromBody] OFile orderFile)
        {
            if (id != orderFile.Id)
                return BadRequest();
            await _FileService.UpdateFileAsync(orderFile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(string id)
        {
            await _FileService.DeleteFileAsync(id);
            return NoContent();
        }
    }
}
