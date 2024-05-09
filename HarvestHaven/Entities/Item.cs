namespace HarvestHaven.Entities
{
    public class Item // Items are the ones you can place on a farm cell. You can buy them but cannot sell/trade them.
    {
        public Guid Id { get; set; }
        public ItemType ItemType { get; set; }
        public Guid ResourceToPlaceId { get; set; }
        public Guid ResourceToInteractId { get; set; }
        public Guid? ResourceToDestroyId { get; set; } // Nullable.

        public Item(Guid id, ItemType itemType, Guid requiredResourceId, Guid interactResourceId, Guid? destroyResourceId)
        {
            Id = id;
            ItemType = itemType;
            ResourceToPlaceId = requiredResourceId;
            ResourceToInteractId = interactResourceId;
            ResourceToDestroyId = destroyResourceId;
        }
    }

    public enum ItemType
    {
        Chicken,
        Cow,
        Sheep,
        Duck,
        WheatSeeds,
        CornSeeds,
        CarrotSeeds,
        TomatoSeeds
    }
}
