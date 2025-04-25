namespace SportZoneServer.Data.Entities
{
    public class OrderItem : GenericEntity
    {
        public Guid OrderId { get; set; }

        public Order Order { get; set; } = null!;

        public Guid ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal SinglePrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
