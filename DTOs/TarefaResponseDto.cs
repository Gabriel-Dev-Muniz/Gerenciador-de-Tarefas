using GerenciadorDeTarefa.Models;
namespace GerenciadorDeTarefa.DTOs
{
    public class TarefaResponseDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }

        public int Prioridade { get; set; }
        public StatusTarefa Status { get; set; }
        public DateTime? DataEntrega { get; set; }
    }
}