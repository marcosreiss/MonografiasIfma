using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonografiasIfma.Models;

namespace MonografiasIfma.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Orientador> Orientadores { get; set;}
        public DbSet<Monografia> Monografias { get; set;}
        public DbSet<Funcionario> Funcionarios { get; set; }
    }
}