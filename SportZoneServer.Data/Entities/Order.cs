using SportZoneServer.Core.Enums;

namespace SportZoneServer.Data.Entities
{
    public class Order : GenericEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string? Names { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public decimal PriceBeforeDiscount { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
        
    }
}
