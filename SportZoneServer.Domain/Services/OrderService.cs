using SportZoneServer.Common.Requests.Order;
using SportZoneServer.Common.Requests.OrderItem;
using SportZoneServer.Common.Responses.Order;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.Domain.Services;

public class OrderService : IOrderService
{
    public async Task<OrderResponse> GetAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<OrderResponse> AddProductAsync(AddOrderItemRequest product)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderResponse> RemoveProductAsync(RemoveOrderItemRequest product)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SendCurrentAsync(SendOrderRequest request)
    {
        throw new NotImplementedException();
    }
}
