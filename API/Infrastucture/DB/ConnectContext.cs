using API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastucture.DB;

public class ConnectContext(DbContextOptions<ConnectContext> options) : DbContext(options)
{
    public DbSet<UsuarioModel> Usuarios { get; set; } = default!;
    public DbSet<ProdutoModel> Produtos { get; set; } = default!;
    public DbSet<CarrinhoModel> Carrinho { get; set; } = default!;
    public DbSet<WishlistModel> Wishlist { get; set; } = default!;
    public DbSet<PedidosModel> Pedidos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PedidosModel>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<PedidosModel>()
            .HasOne<CarrinhoModel>()
            .WithMany()
            .HasForeignKey(p => p.CarrinhoId);    
    }
}