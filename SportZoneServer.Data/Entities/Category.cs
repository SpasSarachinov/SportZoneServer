namespace SportZoneServer.Data.Entities
{
    public class Category : GenericEntity
    {
        public required string Name { get; set; }
        public required Guid ImageId { get; set; }
        public Image? Image { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
