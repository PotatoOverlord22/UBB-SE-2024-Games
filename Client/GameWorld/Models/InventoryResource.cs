namespace GameWorld.Models
{
    public class InventoryResource
    {
        public Guid Id { get; set; }
        public User Owner { get; set; }
        public Resource Resource { get; set; }
        public int Quantity { get; set; }

        public InventoryResource(Guid id, User owner, Resource resource, int quantity)
        {
            Id = id;
            Owner = owner;
            Resource = resource;
            Quantity = quantity;
        }
    }
}
