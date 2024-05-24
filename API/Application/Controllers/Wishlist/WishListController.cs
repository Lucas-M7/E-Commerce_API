using API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Application.Controllers.Wishlist;

[Authorize]
[ApiController]
[Route("api/")]
public class WishListController(IWishlistService wishlist) : ControllerBase
{
    private readonly IWishlistService _wishhList = wishlist;

    [HttpPost("WishList/{produtoId}")]
    public IActionResult AdicionarNaLista(int produtoId)
    {
        _wishhList.AdicionarNaLista(produtoId);
        return Ok("Item adicionado.");
    }
}