namespace GerenciadorDeTarefa.DTOs
{
    public class TarefaCreateDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public int Prioridade { get; set; }
        public DateTime DataEntrega { get; set; }
    }
}
