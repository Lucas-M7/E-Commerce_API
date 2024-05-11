using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace API.Domain.Token;

public class TokenGerador
{
    public static string GerarTokenJwT(UsuarioModel usuario)
    {
        var chave = "Chave_Super_Secreta_com_pelo_menos_32_bytes";
        var chaveSegura = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave));
        var credenciais = new SigningCredentials(chaveSegura, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new("Email", usuario.Email),
            new("Nome", usuario.Nome)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credenciais
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}