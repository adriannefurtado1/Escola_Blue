using Escola.Models;
using Microsoft.EntityFrameworkCore;

namespace Escola.Context
{
    public class EscolaContext : DbContext
    {
        public EscolaContext(DbContextOptions<EscolaContext> options) : base(options) 
        { 
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().ToTable("Aluno");

            modelBuilder.Entity<Aluno>()
                .HasOne(e => e.Turma) // 1 aluno tem 1 turma
                .WithMany(e => e.Alunos) //1 turma tem varios alunos
                .HasForeignKey(e => e.TurmaID); //campo da FK

            modelBuilder.Entity<Turma>().ToTable("Turma");
        }


        public DbSet<Turma>? Turma { get; set; }

        public DbSet<Aluno>? Aluno { get; set; }
    }
}
