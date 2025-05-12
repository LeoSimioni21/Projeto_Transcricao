using Projeto_Transcricao_C_.entities;
//
namespace Projeto_Transcricao_C_.services
{
    public interface ITranscriptionService
    {
        Task<ResponseEntities<string>> TranscreverAudio(string filePath);
        Task<ResponseEntities<List<Transcricao>>> GetTranscriptionsAsync();
    }
}
