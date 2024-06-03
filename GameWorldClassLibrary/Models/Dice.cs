namespace GameWorldClassLibrary.Models
{
    public class Dice
    {
        private Random randomNumberGenerator = new Random();
        public int RollDice()
        {
            return randomNumberGenerator.Next(1, 7);
        }
    }
}
