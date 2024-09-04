﻿using Intelectah.Models;
using Microsoft.EntityFrameworkCore;

namespace Intelectah.Dapper
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }
        public DbSet<FabricantesModel> Fabricantes { get; set; }
        public DbSet<VeiculosModel> Veiculos { get; set; }
        public DbSet<ConcessionariasModel> Concessionarias { get; set; }
        public DbSet<VendasModel> Vendas { get; set; }
        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<UsuariosModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientesModel>()
       .HasMany(c => c.Vendas)
       .WithOne(v => v.Cliente)
       .HasForeignKey(v => v.ClienteID)
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VendasModel>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Vendas)
                .HasForeignKey(v => v.ClienteID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VendasModel>()
                .HasOne(v => v.Usuario)
                .WithMany()
                .HasForeignKey(v => v.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FabricantesModel>()
                .HasMany(f => f.Veiculos)
                .WithOne(v => v.Fabricantes)
                .HasForeignKey(v => v.FabricanteID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConcessionariasModel>()
                .HasMany(c => c.Usuarios)
                .WithOne(u => u.Concessionaria)
                .HasForeignKey(u => u.ConcessionariaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConcessionariasModel>()
                .HasMany(c => c.Vendas)
                .WithOne(v => v.Concessionaria)
                .HasForeignKey(v => v.ConcessionariaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConcessionariasModel>()
                .HasMany(c => c.Veiculos)
                .WithOne(v => v.Concessionarias) 
                .HasForeignKey(v => v.ConcessionariaID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
