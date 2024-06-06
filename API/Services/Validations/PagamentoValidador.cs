using System.Text.RegularExpressions;
using API.Domain.DTOs;
using API.Domain.ModelViews;

namespace API.Services.Validations;

public class PagamentoValidador
{
    public ErrorValidacao ValidacaoPagamento(PagamentoDTO cartaoDTO)
    {
        var validacao = new ErrorValidacao
        {
            Mensagens = []
        };

        string numeroCartaoRegex = @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|3[47][0-9]{13})$";
        string nomeTitularRegex = @"^[a-zA-Z\s]+$";
        string validadeRegex = @"^(0[1-9]|1[0-2])\/?([0-9]{2}|[0-9]{4})$";
        string codigoSegurançaRegex = @"^\d{3,4}$";

        if (!Regex.IsMatch(cartaoDTO.NomeTitular, nomeTitularRegex))
            validacao.Mensagens.Add("Nome do titular inválido, certifique-se de estar certo.");

        if (!Regex.IsMatch(cartaoDTO.NumeroCartao, numeroCartaoRegex))
            validacao.Mensagens.Add("Numero do cartão inválido, certifique-se de estar certo.");

        if (!Regex.IsMatch(cartaoDTO.DataValidade, validadeRegex))
            validacao.Mensagens.Add("Data de validade inválida.");

        if (!Regex.IsMatch(cartaoDTO.CVV, codigoSegurançaRegex))
            validacao.Mensagens.Add("CVV inválido.");

        return validacao;
    }
}