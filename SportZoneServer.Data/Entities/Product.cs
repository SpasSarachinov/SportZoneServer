namespace SportZoneServer.Data.Entities
{
    public class Product : GenericEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
