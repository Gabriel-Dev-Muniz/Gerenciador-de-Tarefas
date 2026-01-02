namespace GerenciadorDeTarefa.DTOs
{
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}
