﻿// <auto-generated />
using System;
using Intelectah.Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Intelectah.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20240903190246_AtualizaTabelas")]
    partial class AtualizaTabelas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Intelectah.Models.ClientesModel", b =>
                {
                    b.Property<int>("ClienteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteID"), 1L, 1);

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("FezCompras")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("ClienteID");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Intelectah.Models.ConcessionariasModel", b =>
                {
                    b.Property<int>("ConcessionariaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConcessionariaID"), 1L, 1);

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("CapacidadeMax")
                        .HasColumnType("int");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EnderecoCompleto")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("ConcessionariaID");

                    b.ToTable("Concessionarias");
                });

            modelBuilder.Entity("Intelectah.Models.FabricantesModel", b =>
                {
                    b.Property<int>("FabricanteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FabricanteID"), 1L, 1);

                    b.Property<int>("AnoFundacao")
                        .HasColumnType("int");

                    b.Property<string>("NomeFabricante")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PaisOrigem")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("FabricanteID");

                    b.ToTable("Fabricantes");
                });

            modelBuilder.Entity("Intelectah.Models.UsuariosModel", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioID"), 1L, 1);

                    b.Property<int>("ConcessionariaID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("NivelAcesso")
                        .HasColumnType("int");

                    b.Property<string>("NomeUsuario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("UsuarioID");

                    b.HasIndex("ConcessionariaID");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Intelectah.Models.VeiculosModel", b =>
                {
                    b.Property<int>("VeiculoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VeiculoID"), 1L, 1);

                    b.Property<int>("AnoFabricacao")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("FabricanteID")
                        .HasColumnType("int");

                    b.Property<string>("ModeloVeiculo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorVeiculo")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("VeiculoID");

                    b.HasIndex("FabricanteID");

                    b.ToTable("Veiculos");
                });

            modelBuilder.Entity("Intelectah.Models.VendasModel", b =>
                {
                    b.Property<int>("VendaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VendaId"), 1L, 1);

                    b.Property<int>("ClienteID")
                        .HasColumnType("int");

                    b.Property<int>("ConcessionariaID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataVenda")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsuarioID")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("VendaId");

                    b.HasIndex("ClienteID");

                    b.HasIndex("ConcessionariaID");

                    b.HasIndex("UsuarioID");

                    b.ToTable("Vendas");
                });

            modelBuilder.Entity("Intelectah.Models.UsuariosModel", b =>
                {
                    b.HasOne("Intelectah.Models.ConcessionariasModel", "Concessionaria")
                        .WithMany("Usuarios")
                        .HasForeignKey("ConcessionariaID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Concessionaria");
                });

            modelBuilder.Entity("Intelectah.Models.VeiculosModel", b =>
                {
                    b.HasOne("Intelectah.Models.FabricantesModel", "Fabricantes")
                        .WithMany("Veiculos")
                        .HasForeignKey("FabricanteID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Fabricantes");
                });

            modelBuilder.Entity("Intelectah.Models.VendasModel", b =>
                {
                    b.HasOne("Intelectah.Models.ClientesModel", "Cliente")
                        .WithMany("Vendas")
                        .HasForeignKey("ClienteID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Intelectah.Models.ConcessionariasModel", "Concessionaria")
                        .WithMany("Vendas")
                        .HasForeignKey("ConcessionariaID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Intelectah.Models.UsuariosModel", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Concessionaria");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Intelectah.Models.ClientesModel", b =>
                {
                    b.Navigation("Vendas");
                });

            modelBuilder.Entity("Intelectah.Models.ConcessionariasModel", b =>
                {
                    b.Navigation("Usuarios");

                    b.Navigation("Vendas");
                });

            modelBuilder.Entity("Intelectah.Models.FabricantesModel", b =>
                {
                    b.Navigation("Veiculos");
                });
#pragma warning restore 612, 618
        }
    }
}
