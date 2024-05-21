namespace GameWorld.Models
{
    public class ShopItem
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public User? User { get; set; }
        public ShopItem(Guid id, string imagePath, string name, int price)
        {
            Id = id;
            ImagePath = imagePath;
            Name = name;
            Price = price;
        }

        // Method to show a message when buying the item
        /*public void Buy()
        {
            MessageBox.Show($"You have bought {Name} for {Price} chips.");
        }*/
    }
}
