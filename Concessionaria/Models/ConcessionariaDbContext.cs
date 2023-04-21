using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Concessionaria.Models;

public partial class ConcessionariaDbContext : DbContext
{
    public ConcessionariaDbContext()
    {
    }

    public ConcessionariaDbContext(DbContextOptions<ConcessionariaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrador> Administradors { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Endereco> Enderecos { get; set; }

    public virtual DbSet<Veiculo> Veiculo { get; set; }

    public virtual DbSet<Veiculo1> Veiculos1 { get; set; }

    public virtual DbSet<Vendedor> Vendedors { get; set; }

    public virtual DbSet<Venda> Venda { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=ConcessionariaDb;Username=concessionaria;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("administrador_pkey");

            entity.ToTable("administrador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cliente_pkey");

            entity.ToTable("cliente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cpf)
                .HasMaxLength(14)
                .HasColumnName("cpf");
            entity.Property(e => e.DataNasc).HasColumnName("data_nasc");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EnderecoId).HasColumnName("endereco_id");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");

            entity.HasOne(d => d.Endereco).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.EnderecoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cliente_endereco_id_fkey");
        });

        modelBuilder.Entity<Endereco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("endereco_pkey");

            entity.ToTable("endereco");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bairro)
                .HasMaxLength(100)
                .HasColumnName("bairro");
            entity.Property(e => e.Cep)
                .HasMaxLength(10)
                .HasColumnName("cep");
            entity.Property(e => e.Cidade)
                .HasMaxLength(100)
                .HasColumnName("cidade");
            entity.Property(e => e.Complemento)
                .HasMaxLength(255)
                .HasColumnName("complemento");
            entity.Property(e => e.Estado)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.Logradouro)
                .HasMaxLength(255)
                .HasColumnName("logradouro");
            entity.Property(e => e.Numero).HasColumnName("numero");
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("veiculo_pkey1");

            entity.ToTable("veiculo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ano).HasColumnName("ano");
            entity.Property(e => e.Chassi)
                .HasMaxLength(50)
                .HasColumnName("chassi");
            entity.Property(e => e.Cor)
                .HasMaxLength(50)
                .HasColumnName("cor");
            entity.Property(e => e.Modelo)
                .HasMaxLength(255)
                .HasColumnName("modelo");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");
        });

        modelBuilder.Entity<Veiculo1>(entity =>
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

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vendedor_pkey");

            entity.ToTable("vendedor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<Venda>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("venda_pkey");

            entity.ToTable("venda");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.VeiculoId).HasColumnName("veiculo_id");
            entity.Property(e => e.VendedorId).HasColumnName("vendedor_id");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Venda)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("venda_cliente_id_fkey");

            entity.HasOne(d => d.Veiculo).WithMany(p => p.Venda)
                .HasForeignKey(d => d.VeiculoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("venda_veiculo_id_fkey");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.Venda)
                .HasForeignKey(d => d.VendedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("venda_vendedor_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
