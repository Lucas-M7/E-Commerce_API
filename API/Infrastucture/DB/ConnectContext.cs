using API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastucture.DB;

public class ConnectContext(DbContextOptions<ConnectContext> options) : DbContext(options)
{
    public DbSet <UsuarioModel> Usuarios { get; set; } = default!;
    public DbSet <ProdutoModel> Produtos { get; set; } = default!;
}