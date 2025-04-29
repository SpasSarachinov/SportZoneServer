using Moq;
using SportZoneServer.Common.Requests.Order;
using SportZoneServer.Common.Responses.Order;
using SportZoneServer.Core.Exceptions;
using SportZoneServer.Core.StaticClasses;
using SportZoneServer.Data.Entities;
using SportZoneServer.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportZoneServer.Common.Requests.OrderItem;
using SportZoneServer.Core.Enums;
using SportZoneServer.Core.Pages;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Data.PaginationAndFiltering;
using SportZoneServer.Domain.Interfaces;
using Xunit;

namespace SportZoneServer.Tests.Unit.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> orderRepositoryMock;
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly Mock<IAuthService> authServiceMock;
        private readonly Mock<IOrderItemRepository> orderItemRepositoryMock;
        private readonly OrderService orderService;

        public OrderServiceTests()
        {
            orderRepositoryMock = new Mock<IOrderRepository>();
            productRepositoryMock = new Mock<IProductRepository>();
            authServiceMock = new Mock<IAuthService>();
            orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            orderService = new OrderService(orderRepositoryMock.Object, productRepositoryMock.Object, authServiceMock.Object, orderItemRepositoryMock.Object);
        }

        [Fact]
        public async Task ChangeStatusAsync_ShouldThrowAppException_WhenOrderNotFound()
        {
            orderRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Order?)null);

            AppException exception = await Assert.ThrowsAsync<AppException>(async () => 
                await orderService.ChangeStatusAsync(new ChangeOrderStatusRequest { OrderId = Guid.NewGuid(), OrderStatus = OrderStatus.Cancelled }));

            Assert.Equal("Order not found", exception.Message);
            Assert.Equal(404, exception.StatusCode);
        }

        [Fact]
        public async Task ChangeStatusAsync_ShouldReturnTrue_WhenOrderStatusChanged()
        {
            Order order = new Order { Id = Guid.NewGuid(), Status = OrderStatus.Created };
            orderRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(order);
            orderRepositoryMock.Setup(x => x.ChangeStatusAsync(It.IsAny<Guid>(), It.IsAny<OrderStatus>())).ReturnsAsync((Order?)null);

            bool result = await orderService.ChangeStatusAsync(new ChangeOrderStatusRequest { OrderId = order.Id, OrderStatus = OrderStatus.Delivered });

            Assert.True(result);
        }

        [Fact]
        public async Task GetAsync_ShouldThrowAppException_WhenOrderNotFound()
        {
            authServiceMock.Setup(x => x.GetCurrentUserId()).ReturnsAsync(Guid.NewGuid().ToString());
            orderRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((Order?)null);

            AppException exception = await Assert.ThrowsAsync<AppException>(async () =>
                await orderService.GetAsync());

            Assert.Equal("Order not found", exception.Message);
            Assert.Equal(404, exception.StatusCode);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnOrderResponse()
        {
            Order order = new Order { Id = Guid.NewGuid(), OrderTotalPrice = 100, Status = OrderStatus.Created };
            authServiceMock.Setup(x => x.GetCurrentUserId()).ReturnsAsync(order.UserId.ToString());
            orderRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(order);

            OrderResponse result = await orderService.GetAsync();

            Assert.NotNull(result);
            Assert.Equal(order.Id, result.Id);
        }

        [Fact]
        public async Task AddProductAsync_ShouldThrowAppException_WhenProductNotFound()
        {
            AddOrderItemRequest request = new AddOrderItemRequest { ProductId = Guid.NewGuid(), Quantity = 1 };
            authServiceMock.Setup(x => x.GetCurrentUserId()).ReturnsAsync(Guid.NewGuid().ToString());
            productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Product?)null);

            AppException exception = await Assert.ThrowsAsync<AppException>(async () =>
                await orderService.AddProductAsync(request));

            Assert.Equal("Product not found", exception.Message);
            Assert.Equal(404, exception.StatusCode);
        }

        [Fact]
        public async Task RemoveProductAsync_ShouldThrowAppException_WhenOrderNotFound()
        {
            RemoveOrderItemRequest request = new RemoveOrderItemRequest
            {
                ProductId = Guid.NewGuid(),
                Quantity = 0
            };
            authServiceMock.Setup(x => x.GetCurrentUserId()).ReturnsAsync(Guid.NewGuid().ToString());
            orderRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((Order?)null);

            AppException exception = await Assert.ThrowsAsync<AppException>(async () =>
                await orderService.RemoveProductAsync(request));

            Assert.Equal("Order not found", exception.Message);
            Assert.Equal(404, exception.StatusCode);
        }

        [Fact]
        public async Task SendCurrentAsync_ShouldThrowAppException_WhenOrderNotFound()
        {
            SendOrderRequest request = new SendOrderRequest
            {
                Names = "John Doe",
                PostalCode = null,
                Country = null,
                City = null,
                Address = null,
                Phone = null
            };
            authServiceMock.Setup(x => x.GetCurrentUserId()).ReturnsAsync(Guid.NewGuid().ToString());
            orderRepositoryMock.Setup(x => x.GetByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((Order?)null);

            AppException exception = await Assert.ThrowsAsync<AppException>(async () =>
                await orderService.SendCurrentAsync(request));

            Assert.Equal("Order not found", exception.Message);
            Assert.Equal(404, exception.StatusCode);
        }
    }
}
