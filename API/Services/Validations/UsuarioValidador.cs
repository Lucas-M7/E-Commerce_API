using System.Text.RegularExpressions;
using API.Domain.DTOs;
using API.Domain.ModelViews;
using API.Infrastucture.DB;

namespace API.Services.Validations;

public class UsuarioValidador(ConnectContext context)
{
    private readonly ConnectContext _context = context;

    public ErrorValidacao UsuarioValidacao(UsuarioDTO usuarioDTO)
    {
        var validacao = new ErrorValidacao()
        {
            Mensagens = []
        };

        string padraoDoEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        string padraoDoNome = @"^[a-zA-Z]+$";

        if (!Regex.IsMatch(usuarioDTO.Email, padraoDoEmail))
            validacao.Mensagens.Add("Email inválido. Não utilize caracteres inválidos.");   

        if (_context.Usuarios.Any(x => x.Email == usuarioDTO.Email))
            validacao.Mensagens.Add("Esse email já está sendo utilizado por outro usuário");

        if (!Regex.IsMatch(usuarioDTO.Nome, padraoDoNome))
            validacao.Mensagens.Add("Nome inválido. Certifique-se de colocar apenas letras.");

        if (_context.Usuarios.Any(x => x.Nome == usuarioDTO.Nome))
            validacao.Mensagens.Add("Esse nome já está sendo utilizado por outro usuário.");

         if (string.IsNullOrEmpty(usuarioDTO.Senha) || usuarioDTO.Senha.Length < 4)
            validacao.Mensagens.Add("Check that the password is empty or has at least 4 characters.");

        return validacao;      
    }
}