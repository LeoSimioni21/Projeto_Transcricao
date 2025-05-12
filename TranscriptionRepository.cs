using Microsoft.EntityFrameworkCore;
using Projeto_Transcricao_C_.data;
using Projeto_Transcricao_C_.entities;

namespace Projeto_Transcricao_C_.repositories
{
    public class TranscriptionRepository : ITranscriptionRepository
    {
        private readonly AppDbContext _context;

        public TranscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Transcricao> AddTranscricao(Transcricao transcription)
        {
            _context.Transcricoes.Add(transcription);
            await _context.SaveChangesAsync();
            return transcription;
        }

        public async Task<List<Transcricao>> GetTranscricaoAsync()
        {
            return await _context.Transcricoes.ToListAsync();
        }
    }
}
