﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebMesaGestor.Infra.Data;

#nullable disable

namespace WebMesaGestor.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("CaiStatus")
                        .HasColumnType("int");

                    b.Property<decimal?>("CaiValFechamento")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("CaiValInicial")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("CaiValTotal")
                        .HasColumnType("decimal(10,2)");

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
                        .HasMaxLength(18)
                        .HasColumnType("varchar(18)");

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

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Opcao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("GrupoOpcoesId")
                        .HasColumnType("char(36)");

                    b.Property<string>("OpcaoDesc")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("OpcaoQuantMax")
                        .HasColumnType("int");

                    b.Property<decimal>("OpcaoValor")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("GrupoOpcoesId");

                    b.ToTable("Opcoes");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.OpcaoProPed", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OpcaoId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProdutoPedidoId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OpcaoId");

                    b.HasIndex("ProdutoPedidoId");

                    b.ToTable("OpcaoProPed");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("MesaId")
                        .HasColumnType("char(36)");

                    b.Property<int>("PedStatus")
                        .HasColumnType("int");

                    b.Property<int>("PedTipoPag")
                        .HasColumnType("int");

                    b.Property<decimal>("PedValor")
                        .HasColumnType("decimal(10,2)");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("MesaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CategoriaId")
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
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("ProUnidade")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.Property<Guid?>("SetorId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("SetorId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.ProdutoPedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PedDesconto")
                        .HasColumnType("int");

                    b.Property<int>("PedQuant")
                        .HasColumnType("int");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("char(36)");

                    b.Property<int>("statusProPed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ProdutoPedido");
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

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Transacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CaixaId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CriacaoData")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("PedidoId")
                        .HasColumnType("char(36)");

                    b.Property<string>("TraDescricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("TraValor")
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("TransacaoStatus")
                        .HasColumnType("int");

                    b.Property<Guid?>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CaixaId");

                    b.HasIndex("PedidoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Transacoes");
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
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

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

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Opcao", b =>
                {
                    b.HasOne("WebMesaGestor.Domain.Entities.GrupoOpcoes", "GrupoOpcoes")
                        .WithMany()
                        .HasForeignKey("GrupoOpcoesId");

                    b.Navigation("GrupoOpcoes");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.OpcaoProPed", b =>
                {
                    b.HasOne("WebMesaGestor.Domain.Entities.Opcao", "Opcao")
                        .WithMany()
                        .HasForeignKey("OpcaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMesaGestor.Domain.Entities.ProdutoPedido", "ProdutoPedido")
                        .WithMany()
                        .HasForeignKey("ProdutoPedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Opcao");

                    b.Navigation("ProdutoPedido");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Pedido", b =>
                {
                    b.HasOne("WebMesaGestor.Domain.Entities.Mesa", "Mesa")
                        .WithMany()
                        .HasForeignKey("MesaId");

                    b.HasOne("WebMesaGestor.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Mesa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Produto", b =>
                {
                    b.HasOne("WebMesaGestor.Domain.Entities.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId");

                    b.HasOne("WebMesaGestor.Domain.Entities.Setor", "Setor")
                        .WithMany()
                        .HasForeignKey("SetorId");

                    b.Navigation("Categoria");

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.ProdutoPedido", b =>
                {
                    b.HasOne("WebMesaGestor.Domain.Entities.Pedido", "Pedido")
                        .WithMany()
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMesaGestor.Domain.Entities.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("WebMesaGestor.Domain.Entities.Transacao", b =>
                {
                    b.HasOne("WebMesaGestor.Domain.Entities.Caixa", "Caixa")
                        .WithMany()
                        .HasForeignKey("CaixaId");

                    b.HasOne("WebMesaGestor.Domain.Entities.Pedido", "Pedido")
                        .WithMany()
                        .HasForeignKey("PedidoId");

                    b.HasOne("WebMesaGestor.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Caixa");

                    b.Navigation("Pedido");

                    b.Navigation("Usuario");
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
