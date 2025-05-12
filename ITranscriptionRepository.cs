using Projeto_Transcricao_C_.entities;


namespace Projeto_Transcricao_C_.repositories
{
    public interface ITranscriptionRepository
    {
        Task<Transcricao> AddTranscricao(Transcricao Transcricao);
        Task<List<Transcricao>> GetTranscricaoAsync();
    }
}
