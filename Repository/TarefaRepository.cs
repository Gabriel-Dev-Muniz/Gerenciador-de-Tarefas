using GerenciadorDeTarefa.Data;
using GerenciadorDeTarefa.DTOs;
using GerenciadorDeTarefa.Models;
using GerenciadorDeTarefa.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefa.Repository
{
    public class TarefaRepository
    {
        private readonly ApplicationDbContext _context;

        public TarefaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tarefa>> ListarAsync(int usuarioId, TarefaFiltroDto filtro)
        {
            var query = _context.Tarefas
                .Where(t => t.UsuarioId == usuarioId)
                .AsQueryable();

            // 🔎 filtro por título
            if (!string.IsNullOrWhiteSpace(filtro.Titulo))
            {
                query = query.Where(t => t.Titulo.Contains(filtro.Titulo));
            }

            // 🔎 filtro por status (CORRETO)
            if (filtro.Status.HasValue)
            {
                query = query.Where(t => t.Status == filtro.Status.Value);
            }

            // 🔎 filtro por data inicial
            if (filtro.DataInicio.HasValue)
            {
                query = query.Where(t => t.DataEntrega >= filtro.DataInicio.Value);
            }

            // 🔎 filtro por data final
            if (filtro.DataFim.HasValue)
            {
                query = query.Where(t => t.DataEntrega <= filtro.DataFim.Value);
            }

            return await query
                .OrderBy(t => t.DataEntrega)
                .ToListAsync();
        }

        public async Task<Tarefa?> ObterPorIdAsync(int id, int usuarioId)
        {
            return await _context.Tarefas
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);
        }

        public async Task CriarAsync(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Tarefa tarefa)
        {
            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
        }
    }
}
