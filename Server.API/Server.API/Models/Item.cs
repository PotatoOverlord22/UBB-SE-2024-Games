namespace Server.API.Models
{
    public class Item // Items are the ones you can place on a farm cell. You can buy them but cannot sell/trade them.
    {
        public Guid Id { get; set; }
        public ItemType ItemType { get; set; }
        public Resource? ResourceToPlace { get; set; }
        public Resource? ResourceToInteract { get; set; }
        public Resource? ResourceToDestroy { get; set; } // Nullable.
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
