using SportZoneServer.Common.Requests.Order;
using SportZoneServer.Common.Requests.OrderItem;
using SportZoneServer.Common.Responses.Order;
using SportZoneServer.Common.Responses.OrderItem;
using SportZoneServer.Core.Enums;
using SportZoneServer.Core.Exceptions;
using SportZoneServer.Core.Pages;
using SportZoneServer.Core.StaticClasses;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Data.PaginationAndFiltering;
using SportZoneServer.Domain.Interfaces;

namespace SportZoneServer.Domain.Services;

public class OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IAuthService authService, IOrderItemRepository orderItemRepository) : IOrderService
{
    public async Task<bool> ChangeStatusAsync(ChangeOrderStatusRequest request)
    {
        await orderRepository.ChangeStatusAsync(request.OrderId, request.OrderStatus);
        return true;
    }

    public async Task<Paginated<OrderResponse>> SearchOrdersAsync(SearchOrderRequest request)
    {
        if (request.UserId == null)
        {
            string role = await authService.GetCurrentUserRole();
            if (role != Roles.Admin)
            {
                throw new AppException("Forbidden").SetStatusCode(403);
            }
        }
        Filter<Order> filter = new()
        {
           
            Predicate = request.GetPredicate(),
            PageNumber = request.PageNumber ?? 1,
            PageSize = request.PageSize ?? 10,
            SortBy = request.SortBy ?? "CreatedOn",
            SortDescending = request.SortDescending ?? false,
        };

        Paginated<Order> result = await orderRepository.SearchAsync(filter);

        List<OrderResponse> responses = new();

        foreach (Order order in result.Items!)
        {
            OrderResponse response = new()
            {
                Id = order.Id,
                OrderTotalPrice = order.OrderTotalPrice,
                Status = order.Status,
                CreatedOn = order.CreatedOn,
            };

            responses.Add(response);
        }

        Paginated<OrderResponse> paginated = new()
        {
            Items = responses,
            TotalCount = result.TotalCount
        };

        return paginated;    
    }
    
    public async Task<OrderResponse> GetAsync()
    {
        Guid userId = Guid.Parse(await authService.GetCurrentUserId());
        Order? order = await orderRepository.GetByUserIdAsync(userId);

        if (order == null)
        {
            throw new AppException("Order not found").SetStatusCode(404);
        }

        return MapOrderToResponse(order);
    }

    public async Task<OrderResponse> AddProductAsync(AddOrderItemRequest request)
    {
        Guid userId = Guid.Parse(await authService.GetCurrentUserId());
        Order? order = await orderRepository.GetByUserIdAsync(userId);
        
        if (order == null)
        {
            order = await orderRepository.AddAsync(userId);
        }

        Product? product = await productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            throw new AppException("Product not found").SetStatusCode(404);
        }
        
        OrderItem? existingItem = order.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += request.Quantity;
            existingItem.TotalPrice = existingItem.Quantity * existingItem.SinglePrice;
        }
        else
        {
            OrderItem newOrderItem = new()
            {
                OrderId = order.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                SinglePrice = product.DiscountedPrice,
                TotalPrice = request.Quantity * product.DiscountedPrice,
                PrimaryImageUri = product.PrimaryImageUrl,
                Title = product.Title,
            };
            await orderItemRepository.AddAsync(newOrderItem);
        }

        UpdateOrderPrices(order);

        await orderRepository.UpdateAsync(order);

        return MapOrderToResponse(order);
    }

    public async Task<OrderResponse> RemoveProductAsync(RemoveOrderItemRequest request)
    {
        Guid userId = Guid.Parse(await authService.GetCurrentUserId());
        Order? order = await orderRepository.GetByUserIdAsync(userId);

        if (order == null)
        {
            throw new AppException("Order not found").SetStatusCode(404);
        }

        OrderItem? item = order.Items.FirstOrDefault(i => i.ProductId == request.ProductId);
        if (item == null)
        {
            throw new AppException("Product not found").SetStatusCode(404);
        }

        item.Quantity -= request.Quantity;

        if (item.Quantity <= 0)
        {
            order.Items.Remove(item);
            if (order.Items.Count == 0)
            {
                await orderRepository.DeleteAsync(order.Id);

                throw new AppException("Order deleted").SetStatusCode(200);
            }
        }
        else
        {
            item.TotalPrice = item.Quantity * item.SinglePrice;
        }

        UpdateOrderPrices(order);

        await orderRepository.UpdateAsync(order);

        return MapOrderToResponse(order);
    }

    public async Task<bool> SendCurrentAsync(SendOrderRequest request)
    {
        Guid userId = Guid.Parse(await authService.GetCurrentUserId());
        Order? order = await orderRepository.GetByUserIdAsync(userId);

        if (order == null || !order.Items.Any())
        {
            throw new AppException("Order not found").SetStatusCode(404);
        }

        order.Names = request.Names;
        order.PostalCode = request.PostalCode;
        order.Country = request.Country;
        order.City = request.City;
        order.Address = request.Address;
        order.Phone = request.Phone;
        order.Status = OrderStatus.PendingVerification;

        await orderRepository.UpdateAsync(order);
        return true;
    }
    
    private void UpdateOrderPrices(Order order)
    {
        order.OrderTotalPrice = order.Items.Sum(i => i.TotalPrice);
    }

    private OrderResponse MapOrderToResponse(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
         OrderTotalPrice = order.OrderTotalPrice,
            Items = order.Items.Select(i => new OrderItemResponse
            {
                ProductId = i.ProductId,
                SinglePrice = i.SinglePrice,
                TotalPrice = i.TotalPrice,
                Quantity = i.Quantity,
                PrimaryImageUri = i.PrimaryImageUri,
                Title = i.Title
            }).ToList()
        };
    }
}
