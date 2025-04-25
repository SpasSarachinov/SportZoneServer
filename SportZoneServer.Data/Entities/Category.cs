namespace SportZoneServer.Data.Entities
{
    public class Category : GenericEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string ImageURI { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
