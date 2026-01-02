// Data/ApplicationDbContext.cs
using GerenciadorDeTarefa.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
