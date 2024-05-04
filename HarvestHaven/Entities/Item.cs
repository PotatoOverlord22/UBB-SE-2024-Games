namespace HarvestHaven.Entities
{
    public class Item // Items are the ones you can place on a farm cell. You can buy them but cannot sell/trade them.
    {
        public Guid Id { get; set; }
        public ItemType ItemType { get; set; }
        public Guid RequiredResourceId { get; set; }
        public Guid InteractResourceId { get; set; }
        public Guid? DestroyResourceId { get; set; } // Nullable.

        public Item(Guid id, ItemType itemType, Guid requiredResourceId, Guid interactResourceId, Guid? destroyResourceId)
        {
            Id = id;
            ItemType = itemType;
            RequiredResourceId = requiredResourceId;
            InteractResourceId = interactResourceId;
            DestroyResourceId = destroyResourceId;
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
