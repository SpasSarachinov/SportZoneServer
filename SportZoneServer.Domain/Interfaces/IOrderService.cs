using SportZoneServer.Common.Requests.Order;
using SportZoneServer.Common.Requests.OrderItem;
using SportZoneServer.Common.Responses.Order;
using SportZoneServer.Core.Pages;

namespace SportZoneServer.Domain.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> GetAsync();
    Task<OrderResponse> AddProductAsync(AddOrderItemRequest product);
    Task<OrderResponse> RemoveProductAsync(RemoveOrderItemRequest product);
    Task<bool> SendCurrentAsync(SendOrderRequest request);
    Task<bool> ChangeStatusAsync(ChangeOrderStatusRequest request);
    Task<Paginated<OrderResponse>> SearchOrdersAsync(SearchOrderRequest request);
}
 
