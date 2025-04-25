using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportZoneServer.API.Helpers;
using SportZoneServer.Common.Requests.Order;
using SportZoneServer.Common.Requests.OrderItem;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return await ControllerProcessor.ProcessAsync(() => orderService.GetAsync(), this);
    }
    
    [HttpPut]
    public async Task<IActionResult> GetAllAsync([FromBody] AddOrderItemRequest request)
    {
        return await ControllerProcessor.ProcessAsync(() => orderService.AddProductAsync(request), this, true);
    }
    
    [HttpDelete]
    public async Task<IActionResult> GetAllAsync([FromBody] RemoveOrderItemRequest request)
    {
        return await ControllerProcessor.ProcessAsync(() => orderService.RemoveProductAsync(request), this, true);
    }
    
    [HttpPost]
    public async Task<IActionResult> SendOrder([FromBody] SendOrderRequest request)
    {
        return await ControllerProcessor.ProcessAsync(() => orderService.SendCurrentAsync(request), this);
    }
    
    [HttpGet("get-list")]
    public async Task<IActionResult> GetAllAsync([FromQuery] SearchOrderRequest? request)
    {
        return await ControllerProcessor.ProcessAsync(() => orderService.SearchOrdersAsync(request), this, true);
    }
}
