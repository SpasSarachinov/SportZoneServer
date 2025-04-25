namespace SportZoneServer.Data.Entities
{
    public class Product : GenericEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; } 
        public string ImageUrl { get; set; }
        public decimal RegularPrice { get; set; }

        public byte DiscountPercentage { get; set; }
        public decimal DiscountedPrice { get; set; }
        public uint Quantity { get; set; }

        public double Rating { get; set; } = 0;
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
