﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebMesaGestor.Infra.Data;

#nullable disable

namespace WebMesaGestor.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250116165223_AtualizandoMudancas")]
    partial class AtualizandoMudancas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Caixa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("AberturaData")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CaiStatus")
                        .HasColumnType("int");

                    b.Property<decimal?>("CaiValFechamento")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CaiValInicial")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("FechamentoData")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Caixas");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Categoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("CatDesc")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Empresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EmpCnpj")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("EmpNome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.GrupoOpcoes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GrupOpcDesc")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("GrupOpcMax")
                        .HasColumnType("int");

                    b.Property<int>("GrupOpcTipo")
                        .HasColumnType("int");

                    b.Property<Guid?>("ProdutoId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("GrupoOpcoes");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Mesa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MesaNumero")
                        .HasColumnType("int");

                    b.Property<int>("MesaStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Mesas");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CategoriaId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ProCodigo")
                        .HasColumnType("int");

                    b.Property<string>("ProDescricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("ProPreco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProUnidade")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<Guid>("SetorId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("SetorId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Setor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SetDesc")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("SetStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Setores");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("EmpresaId")
                        .HasColumnType("char(36)");

                    b.Property<string>("UsuEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UsuNome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UsuSenha")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.Property<string>("UsuTelefone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UsuTipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Caixa", b =>
                {
                    b.HasOne("WebMesaGestor.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.GrupoOpcoes", b =>
                {
                    b.HasOne("WebMesaGestor.Domain.Entities.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Produto", b =>
                {
                    b.HasOne("WebMesaGestor.Domain.Entities.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMesaGestor.Domain.Entities.Setor", "Setor")
                        .WithMany()
                        .HasForeignKey("SetorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Usuario", b =>
                {
                    b.HasOne("WebMesaGestor.Domain.Entities.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId");

                    b.Navigation("Empresa");
                });
#pragma warning restore 612, 618
        }
    }
}
