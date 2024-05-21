namespace Server.API.Models
{
    public class ShopItem
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public User? User { get; set; }
    }
}
