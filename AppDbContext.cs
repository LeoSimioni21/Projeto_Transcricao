using Microsoft.EntityFrameworkCore;
using Projeto_Transcricao_C_.entities;
//
namespace Projeto_Transcricao_C_.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 

        }
      
        public DbSet<Transcricao> Transcricoes { get; set; } = null!;

    }

}
