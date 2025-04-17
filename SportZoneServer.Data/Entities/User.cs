using Microsoft.AspNetCore.Identity;

namespace SportZoneServer.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<WishlistItem> Wishlist { get; set; } = new List<WishlistItem>();

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}