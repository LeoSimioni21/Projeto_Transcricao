namespace Projeto_Transcricao_C_.entities
{
    public class Transcricao
    {
        public int Id { get; set; }
        public string? Texto { get; set; }
        public DateTime TranscriptionDate { get; set; } = DateTime.Now;
        public double TempoProcessamento { get; set; }
    }
}

