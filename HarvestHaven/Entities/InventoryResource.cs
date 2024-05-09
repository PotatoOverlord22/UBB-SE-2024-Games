namespace HarvestHaven.Entities
{
    public class InventoryResource
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public Guid ResourceId { get; set; }
        public int Quantity { get; set; }

        public InventoryResource(Guid id, Guid userId, Guid resourceId, int quantity)
        {
            Id = id;
            OwnerId = userId;
            ResourceId = resourceId;
            Quantity = quantity;
        }
    }
}
