using GerenciadorDeTarefa.DTOs;
using GerenciadorDeTarefa.Models;
using GerenciadorDeTarefa.Models.DTOs;
using GerenciadorDeTarefa.Repository;

namespace GerenciadorDeTarefa.Service
{
    public class TarefaService
    {
        private readonly TarefaRepository _repository;

        public TarefaService(TarefaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Tarefa>> ListarAsync(int usuarioId, TarefaFiltroDto filtro)
        {
            return await _repository.ListarAsync(usuarioId, filtro);
        }

        public async Task CriarAsync(TarefaCreateDto dto, int usuarioId)
        {
            var tarefa = new Tarefa
            {
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                DataEntrega = dto.DataEntrega,
                Prioridade = dto.Prioridade,
                Status = StatusTarefa.Pendente,
                UsuarioId = usuarioId
            };

            await _repository.CriarAsync(tarefa);
        }

        public async Task ConcluirAsync(int id, int usuarioId)
        {
            var tarefa = await _repository.ObterPorIdAsync(id, usuarioId);
            if (tarefa == null) return;

            tarefa.Status = StatusTarefa.Concluida;
            await _repository.AtualizarAsync(tarefa);
        }

        public async Task RemoverAsync(int id, int usuarioId)
        {
            var tarefa = await _repository.ObterPorIdAsync(id, usuarioId);
            if (tarefa == null) return;

            await _repository.RemoverAsync(tarefa);
        }
    }
}
