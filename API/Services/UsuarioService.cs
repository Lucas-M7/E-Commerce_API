using API.Domain.DTOs;
using API.Domain.Interfaces;
using API.Domain.Models;
using API.Infrastucture.DB;

namespace API.Services;

public class UsuarioService(ConnectContext context) : IUsuarioService
{
    private readonly ConnectContext _context = context;

    public UsuarioModel Adicionar(UsuarioModel usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        return usuario;
    }

    public void Apagar(int id)
    {
        var usuario = ObterUsuario(id);

        if (usuario != null)
            _context.Usuarios.Remove(usuario);

        _context.SaveChanges();
    }

    public List<UsuarioModel> ListarUsuarios(int? pagina = 1)
    {
        var consulta = _context.Usuarios.AsQueryable();

        int itensPorPagina = 10;

        if (pagina != null)
        {
            consulta = consulta.Skip(((int)pagina - 1) * itensPorPagina).Take(itensPorPagina);
        }

        return [.. consulta];
    }

    public UsuarioModel? Login(LoginDTO loginDTO)
    {
        var usuario = _context.Usuarios.Where(x => x.Email == loginDTO.Email && x.Senha == loginDTO.Senha).FirstOrDefault();

        return usuario;
    }

    private UsuarioModel ObterUsuario(int id)
    {
        return _context.Usuarios.FirstOrDefault(u => u.ID == id)
            ?? throw new FileNotFoundException("Usuário não encontrado.");
    }
}