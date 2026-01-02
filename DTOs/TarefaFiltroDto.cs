namespace GerenciadorDeTarefa.Models.DTOs
{
    public class TarefaFiltroDto
    {
        public string? Titulo { get; set; }
        public StatusTarefa? Status { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
