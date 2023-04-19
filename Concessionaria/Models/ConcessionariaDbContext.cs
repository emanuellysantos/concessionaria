using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Concessionaria.Models;

public class ConcessionariaDbContext : DbContext
{
    //public ConcessionariaDbContext()
    //{
    //}

    public ConcessionariaDbContext(DbContextOptions<ConcessionariaDbContext> options)
        : base(options)
    {
    }

    public DbSet<Veiculo> Veiculo { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Database=ConcessionariaDb;Username=concessionaria;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //mb.Entity<Veiculo>().HasKey(c => c.Id);
        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("veiculo_pkey");

            entity.ToTable("veiculos");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Ano).HasColumnName("ano");
            entity.Property(e => e.Chassi)
                .HasMaxLength(50)
                .HasColumnName("chassi");
            entity.Property(e => e.Cor)
                .HasMaxLength(50)
                .HasColumnName("cor");
            entity.Property(e => e.Modelo)
                .HasMaxLength(200)
                .HasColumnName("modelo");
            entity.Property(e => e.Valor)
                .HasPrecision(12, 2)
                .HasColumnName("valor");
        });

        //OnModelCreatingPartial(modelBuilder);
    }

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
