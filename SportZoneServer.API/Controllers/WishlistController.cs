using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportZoneServer.API.Helpers;
using SportZoneServer.Common.Requests.Wishlist;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WishlistController(IWishlistService wishlistService, IAuthService authService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return await ControllerProcessor.ProcessAsync(() => wishlistService.GetByJWT(), this);
    }
    
    [HttpPost("add-product")]
    public async Task<IActionResult> AddProductToWhishlistAsync([FromBody] AddToWishlistRequest request)
    {
        return await ControllerProcessor.ProcessAsync<object>(
            async () => await wishlistService.AddProductToWishlistAsync(request.ProductId), this);    
    }
    
    [HttpDelete("remove-product")]
    public async Task<IActionResult> RemoveProductFromWhishlistAsync([FromBody] RemoveFromWishlistRequest request)
    {
        return await ControllerProcessor.ProcessAsync<object>(
            async () => await wishlistService.RemoveProductFromWishlistAsync(request.ProductId), this); 
    }
}
