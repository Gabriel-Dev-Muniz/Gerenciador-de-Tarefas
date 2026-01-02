using GerenciadorDeTarefa.Data;
using GerenciadorDeTarefa.DTOs;
using GerenciadorDeTarefa.Models;

namespace GerenciadorDeTarefa.Service
{
    public class UsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Usuario Criar(UsuarioCreateDto dto)
        {
            if (_context.Usuarios.Any(u => u.Email == dto.Email))
                throw new Exception("Email já cadastrado");

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public Usuario? Autenticar(string email, string senha)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
                return null;

            return usuario;
        }
    }
}
