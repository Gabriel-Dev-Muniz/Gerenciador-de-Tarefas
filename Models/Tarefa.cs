using GerenciadorDeTarefa.Models;

namespace GerenciadorDeTarefa.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public int Prioridade { get; set; }
        public DateTime DataEntrega { get; set; }
        public DateTime DataCriacao { get; set; }

        public StatusTarefa Status { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }
}
