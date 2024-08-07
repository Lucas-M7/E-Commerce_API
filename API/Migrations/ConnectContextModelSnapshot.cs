﻿// <auto-generated />
using API.Infrastucture.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(ConnectContext))]
    partial class ConnectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Domain.Models.CarrinhoModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("ProdutoID")
                        .HasColumnType("int");

                    b.Property<string>("ProdutoNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProdutoPreco")
                        .HasColumnType("float");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<string>("UsuarioNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Carrinho");
                });

            modelBuilder.Entity("API.Domain.Models.PedidosModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarrinhoId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<string>("ProdutoNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ProdutoPreco")
                        .HasColumnType("float");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ValorUnitario")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CarrinhoId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("API.Domain.Models.ProdutoModel", b =>
                {
                    b.Property<int>("ProdutoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdutoID"));

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Estoque")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.HasKey("ProdutoID");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("API.Domain.Models.UsuarioModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("ID");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("API.Domain.Models.WishlistModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<string>("ProdutoNome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<double>("ProdutoPreco")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Wishlist");
                });

            modelBuilder.Entity("API.Domain.Models.PedidosModel", b =>
                {
                    b.HasOne("API.Domain.Models.CarrinhoModel", null)
                        .WithMany()
                        .HasForeignKey("CarrinhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
