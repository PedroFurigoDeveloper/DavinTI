using DavinTI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DavinTI.Data {
    public class DavinTIContext : DbContext {
        public DavinTIContext(DbContextOptions<DavinTIContext> options) : base(options) { }

        public DbSet<Contato> Contato { get; set; }
        public DbSet<Telefone> Telefone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            #region Contato

            /// <summary>
            /// Contexto do banco de dados referente à entidade Contato
            /// </summary>

            modelBuilder.Entity<Contato>(entity => {
                entity.ToTable("contato");

                entity.HasKey(e => e.IdContato)
                      .HasName("pk_contato");

                entity.Property(e => e.IdContato)
                      .HasColumnName("id_contato")
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.Nome)
                      .HasColumnName("nome")
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.Idade)
                      .HasColumnName("idade");

                entity.HasMany(e => e.Telefones)
                     .WithOne(t => t.Contato)
                     .HasForeignKey(t => t.IdContato)
                     .HasConstraintName("fk_telefone_contato")
                     .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion

            #region Telefone

            /// <summary>
            /// Contexto do banco de dados referente à entidade Telefone
            /// </summary>

            modelBuilder.Entity<Telefone>(entity => {
                entity.ToTable("telefone");

                entity.HasKey(e => new { e.Id, e.IdContato })
                      .HasName("pk_telefone");

                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .IsRequired();

                entity.Property(e => e.IdContato)
                      .HasColumnName("id_contato")
                      .IsRequired();

                entity.Property(e => e.Numero)
                      .HasColumnName("numero")
                      .HasMaxLength(16)
                      .IsRequired();

            });

            #endregion
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseNpgsql(
                    "Host=localhost;Port=5432;Database=DavinTI;Username=postgres;Password=postgres"
                );
            }
        }
    }
}
