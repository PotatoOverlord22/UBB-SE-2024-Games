namespace GameWorldClassLibrary.Models
{
    public class InventoryResource
    {
        public Guid Id { get; set; }
        public User Owner { get; set; }
        public Resource Resource { get; set; }
        public int Quantity { get; set; }
    }
}
