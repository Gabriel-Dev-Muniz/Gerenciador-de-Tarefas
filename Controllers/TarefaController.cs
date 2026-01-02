using GerenciadorDeTarefa.DTOs;
using GerenciadorDeTarefa.Models.DTOs;
using GerenciadorDeTarefa.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GerenciadorDeTarefa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _service;

        public TarefaController(TarefaService service)
        {
            _service = service;
        }

        private int UsuarioId =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] TarefaFiltroDto filtro)
        {
            var tarefas = await _service.ListarAsync(UsuarioId, filtro);
            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] TarefaCreateDto dto)
        {
            await _service.CriarAsync(dto, UsuarioId);
            return NoContent();
        }

        [HttpPut("{id}/concluir")]
        public async Task<IActionResult> Concluir(int id)
        {
            await _service.ConcluirAsync(id, UsuarioId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            await _service.RemoverAsync(id, UsuarioId);
            return NoContent();
        }
    }
}
