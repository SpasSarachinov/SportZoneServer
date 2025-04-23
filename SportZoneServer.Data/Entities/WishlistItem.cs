namespace SportZoneServer.Data.Entities
{
    public class WishlistItem : GenericEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        public Guid ProductId { get; set; }

        public Product Product { get; set; } = null!;
    }
}
