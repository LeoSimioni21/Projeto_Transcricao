using Microsoft.AspNetCore.Mvc;
using Projeto_Transcricao_C_.entities;
using Projeto_Transcricao_C_.services;

namespace Projeto_Transcricao_C_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranscriptionController : ControllerBase
    {
        private readonly ITranscriptionService _service;
        
        public TranscriptionController(ITranscriptionService service)
        {
            _service = service;
        }
        
        [HttpPost("Transcrever")]
        [RequestSizeLimit(100_000_000)] 
        [Consumes("multipart/form-data")] 
        public async Task<IActionResult> PostAudio(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
                return BadRequest("Arquivo de áudio inválido.");

            //usei para ajustar o formato do arquivo 
            var extension = Path.GetExtension(audioFile.FileName);
            var tempFileName = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            var tempPath = Path.Combine(Path.GetTempPath(), $"{tempFileName}{extension}");
            
            using (var stream = new FileStream(tempPath, FileMode.Create))
            {
                await audioFile.CopyToAsync(stream);
            }

            var response = await _service.TranscreverAudio(tempPath);
            
            if (response.Status)
                return Ok(response);
            else
                return StatusCode(500, response);
        }
        
        [HttpGet("retornaDados")]
        public async Task<IActionResult> GetTranscriptions()
        {
            var response = await _service.GetTranscriptionsAsync();
            
            if (response.Status)
                return Ok(response);
            else
                return StatusCode(500, response);
        }
    }
}
