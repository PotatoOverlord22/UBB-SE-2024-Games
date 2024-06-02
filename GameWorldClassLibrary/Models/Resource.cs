namespace GameWorldClassLibrary.Models
{
    public class Resource
    {
        public Guid Id { get; set; }
        public ResourceType ResourceType { get; set; }
    }

    public enum ResourceType
    {
        Water = 0,
        Wheat = 1,
        Corn = 2,
        Carrot = 3,
        Tomato = 4,
        ChickenEgg = 5,
        DuckEgg = 6,
        SheepWool = 7,
        CowMilk = 8,
        ChickenMeat = 9,
        DuckMeat = 10,
        Mutton = 11,
        Steak = 12,
    }
}
