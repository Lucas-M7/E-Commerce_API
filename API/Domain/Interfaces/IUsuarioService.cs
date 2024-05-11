using API.Domain.DTOs;
using API.Domain.Models;

namespace API.Domain.Interfaces;

public interface IUsuarioService
{
    UsuarioModel? Login(LoginDTO loginDTO);
    void Apagar(int id);
    UsuarioModel Adicionar(UsuarioModel usuario);
    List<UsuarioModel> ListarUsuarios(int? pagina);
}