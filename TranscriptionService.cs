using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using Projeto_Transcricao_C_.entities;
using Projeto_Transcricao_C_.repositories;
using Projeto_Transcricao_C_.settings;
using System.Diagnostics;
using System.IO;

namespace Projeto_Transcricao_C_.services
{
    public class TranscriptionService : ITranscriptionService
    {
        private readonly ITranscriptionRepository _repository;
        private readonly string _apiKey;
        
        public TranscriptionService(
            ITranscriptionRepository repository,
            IOptions<OpenAiSettings> openAiSettings)
        {
            _repository = repository;
            _apiKey = openAiSettings.Value.ApiKey;
        }
        
        public async Task<ResponseEntities<string>> TranscreverAudio(string filePath)
        {
            var openAiService = new OpenAIService(new OpenAiOptions
            {
                ApiKey = _apiKey
            });
            
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var response = new ResponseEntities<string>();

            try
            {
                var audioBytes = await File.ReadAllBytesAsync(filePath); 
                
                var request = new AudioCreateTranscriptionRequest
                {
                    FileName = Path.GetFileName(filePath),
                    File = audioBytes,
                    Model = "whisper-1"
                };

                var result = await openAiService.Audio.CreateTranscription(request);
                
                stopwatch.Stop();
                
                if (result.Successful)
                {
                    var transcription = new Transcricao
                    {
                        Texto = result.Text,
                        TranscriptionDate = DateTime.UtcNow,
                        TempoProcessamento = stopwatch.Elapsed.TotalSeconds
                    };

                    await _repository.AddTranscricao(transcription);
                    
                    response.Dados = transcription.Texto;
                    response.Mensagem = "Transcrição realizada com sucesso!";
                    response.Status = true;
                }
                else
                {
                    response.Mensagem = $"Erro da OpenAI: {result.Error?.Message}";
                    response.Status = false;
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                response.Mensagem = $"Erro ao transcrever o áudio: {ex.Message}";
                response.Status = false;
            }

            return response;
        }
        
        public async Task<ResponseEntities<List<Transcricao>>> GetTranscriptionsAsync()
        {
            var response = new ResponseEntities<List<Transcricao>>();

            try
            {
                var transcriptions = await _repository.GetTranscricaoAsync();
                response.Dados = transcriptions;
                response.Mensagem = "Transcrições recuperadas com sucesso!";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao recuperar transcrições: {ex.Message}";
                response.Status = false;
            }

            return response;
        }
    }
}
