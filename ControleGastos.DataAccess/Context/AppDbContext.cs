using ControleGastos.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.DataAccess.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasMany(p => p.Transacoes)
                    .WithOne(t => t.Pessoa)
                    .HasForeignKey(t => t.PessoaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Descricao)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Descricao)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.HasMany(p => p.Transacoes)
                    .WithOne(t => t.Categoria)
                    .HasForeignKey(t => t.CategoriaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Descricao)
                    .HasMaxLength(400);

                entity.Property(t => t.Valor)
                    .IsRequired();
            });
        }
    }
}
