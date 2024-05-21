namespace GameWorld.Entities
{
    public class CardDeck
    {
        public Guid Id { get; set; }
        private List<PlayingCard> cardDeck;

        public CardDeck()
        {
            Id = Guid.NewGuid();
            cardDeck = new List<PlayingCard>();

            foreach (string cardValue in PlayingCard.CARD_VALUES.Values)
            {
                AddAllCardsOfThisValue(cardValue);
            }
        }

        public void AddAllCardsOfThisValue(string value)
        {
            cardDeck.Add(new PlayingCard(value, PlayingCard.HEART_SYMBOL));
            cardDeck.Add(new PlayingCard(value, PlayingCard.DIAMOND_SYMBOL));
            cardDeck.Add(new PlayingCard(value, PlayingCard.SPADE_SYMBOL));
            cardDeck.Add(new PlayingCard(value, PlayingCard.CLUB_SYMBOL));
        }

        public void RemoveCardFromIndex(int index)
        {
            cardDeck.RemoveAt(index);
        }
        public PlayingCard GetCardFromIndex(int index)
        {
            return cardDeck[index];
        }

        public int GetDeckSize()
        {
            return cardDeck.Count;
        }
    }
}
