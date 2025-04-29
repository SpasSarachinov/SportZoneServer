using Moq;
using SportZoneServer.Common.Responses.Product;
using SportZoneServer.Common.Responses.Wishlist;
using SportZoneServer.Core.Exceptions;
using SportZoneServer.Data.Entities;
using SportZoneServer.Data.Interfaces;
using SportZoneServer.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportZoneServer.Domain.Interfaces;
using Xunit;

namespace SportZoneServer.Tests.Unit.Services
{
    public class WishlistServiceTests
    {
        private readonly Mock<IAuthService> authServiceMock;
        private readonly Mock<IWishlistRepository> wishlistRepositoryMock;
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly WishlistService wishlistService;

        public WishlistServiceTests()
        {
            authServiceMock = new Mock<IAuthService>();
            wishlistRepositoryMock = new Mock<IWishlistRepository>();
            userRepositoryMock = new Mock<IUserRepository>();
            productRepositoryMock = new Mock<IProductRepository>();
            wishlistService = new WishlistService(
                authServiceMock.Object,
                wishlistRepositoryMock.Object,
                userRepositoryMock.Object,
                productRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByJWT_NoWishlistItems_ShouldThrowNotFound()
        {
            authServiceMock.Setup(a => a.GetCurrentUserId()).Equals(Guid.NewGuid().ToString());
            wishlistRepositoryMock.Setup(r => r.GetAllByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync((ICollection<WishlistItem>)null);

            await Assert.ThrowsAsync<AppException>(() => wishlistService.GetByJWT());
        }

        [Fact]
        public async Task GetByJWT_ShouldReturnWishlistItems()
        {
            Guid userId = Guid.NewGuid();
            List<WishlistItem> wishlistItems = new List<WishlistItem>
            {
                new WishlistItem { Product = new Product { Id = Guid.NewGuid(), Title = "Product1", RegularPrice = 100 } },
                new WishlistItem { Product = new Product { Id = Guid.NewGuid(), Title = "Product2", RegularPrice = 200 } }
            };

            authServiceMock.Setup(a => a.GetCurrentUserId()).Equals(userId.ToString());
            wishlistRepositoryMock.Setup(r => r.GetAllByUserIdAsync(userId)).ReturnsAsync(wishlistItems);

            WishlistResponse result = await wishlistService.GetByJWT();

            Assert.NotNull(result);
            Assert.Equal(2, result.Products.Count);
            Assert.Contains(result.Products, p => p.Title == "Product1");
            Assert.Contains(result.Products, p => p.Title == "Product2");
        }

        [Fact]
        public async Task AddProductToWishlistAsync_ProductAlreadyInWishlist_ShouldThrowConflict()
        {
            Guid productId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            authServiceMock.Setup(a => a.GetCurrentUserId()).Equals(userId.ToString());
            wishlistRepositoryMock.Setup(r => r.IsProductAlreadyInWishlist(productId, userId)).ReturnsAsync(true);

            await Assert.ThrowsAsync<AppException>(() => wishlistService.AddProductToWishlistAsync(productId));
        }

        [Fact]
        public async Task AddProductToWishlistAsync_ValidProduct_ShouldAddToWishlist()
        {
            Guid productId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            authServiceMock.Setup(a => a.GetCurrentUserId()).Equals(userId.ToString());
            wishlistRepositoryMock.Setup(r => r.IsProductAlreadyInWishlist(productId, userId)).ReturnsAsync(false);
            wishlistRepositoryMock.Setup(r => r.AddAsync(It.IsAny<WishlistItem>())).ReturnsAsync((WishlistItem?)null);

            bool result = await wishlistService.AddProductToWishlistAsync(productId);

            Assert.True(result);
        }

        [Fact]
        public async Task RemoveProductFromWishlistAsync_ProductNotInWishlist_ShouldThrowConflict()
        {
            Guid productId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            authServiceMock.Setup(a => a.GetCurrentUserId()).Equals(userId.ToString());
            wishlistRepositoryMock.Setup(r => r.IsProductAlreadyInWishlist(productId, userId)).ReturnsAsync(false);

            await Assert.ThrowsAsync<AppException>(() => wishlistService.RemoveProductFromWishlistAsync(productId));
        }

        [Fact]
        public async Task RemoveProductFromWishlistAsync_WishlistItemNotFound_ShouldThrowNotFound()
        {
            Guid productId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            authServiceMock.Setup(a => a.GetCurrentUserId()).Equals(userId.ToString());
            wishlistRepositoryMock.Setup(r => r.IsProductAlreadyInWishlist(productId, userId)).ReturnsAsync(true);
            wishlistRepositoryMock.Setup(r => r.GetWishlistItemIdAsync(userId, productId)).ReturnsAsync((Guid?)null);

            await Assert.ThrowsAsync<AppException>(() => wishlistService.RemoveProductFromWishlistAsync(productId));
        }

        [Fact]
        public async Task RemoveProductFromWishlistAsync_ValidProduct_ShouldRemoveFromWishlist()
        {
            Guid productId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();

            authServiceMock.Setup(a => a.GetCurrentUserId()).Equals(userId.ToString());
            wishlistRepositoryMock.Setup(r => r.IsProductAlreadyInWishlist(productId, userId)).ReturnsAsync(true);
            wishlistRepositoryMock.Setup(r => r.GetWishlistItemIdAsync(userId, productId)).ReturnsAsync(Guid.NewGuid());
            wishlistRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).Returns((Task<bool>)Task.CompletedTask);

            bool result = await wishlistService.RemoveProductFromWishlistAsync(productId);

            Assert.True(result);
        }
    }
}
