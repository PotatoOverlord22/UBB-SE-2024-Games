using GameWorldClassLibrary.Models;

namespace GameWorld.Resources.Utils
{
    public class HandRankCalculator
    {
        private Dictionary<string, int> cardValues;
        // Since our cards range in value from 2 to 14, we will use base 15 to hash a certain hand
        private List<int> pows;
        private const int NULL = 0;
        private const int INIT = 1;
        private const int ONE = 1;
        private const int PAIR = 2;
        private const int TRIPS = 3;
        private const int QUADS = 4;

        public HandRankCalculator()
        {
            cardValues = new Dictionary<string, int>
        {
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
            { "10", 10 },
            { "J", 11 },
            { "Q", 12 },
            { "K", 13 },
            { "A", 14 }
        };
            pows = new List<int>() { 1, 15, 225, 3375, 50625, 759375 }; /// Powers of 15
        }

        private void HandSort(List<PlayingCard> hand)
        {
            hand.Sort((x, y) => cardValues[y.Value].CompareTo(cardValues[x.Value]));
        }
        private bool IsRoyalFlush(List<PlayingCard> hand)
        {
            if (IsFlush(hand))
            {
                for (int i = 0; i < hand.Count; i++)
                {
                    if (cardValues[hand[i].Value] != 14 - i)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        private int HashRoyalFlush(List<PlayingCard> hand)
        {
            return pows[0];
        }

        private bool IsStraightFlush(List<PlayingCard> hand)
        {
            if (IsStraight(hand) && IsFlush(hand))
            {
                return true;
            }
            return false;
        }

        private int HashStraightFlush(List<PlayingCard> hand)
        {
            if (hand[0].Value == "A" && hand[1].Value == "5")
            {
                return cardValues[hand[1].Value] * pows[0];
            }
            else
            {
                return cardValues[hand[0].Value] * pows[0];
            }
        }

        private bool IsFourOfAKind(List<PlayingCard> hand)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            foreach (PlayingCard card in hand)
            {
                if (freq.ContainsKey(card.Value))
                {
                    freq[card.Value]++;
                }
                else
                {
                    freq[card.Value] = INIT;
                }
            }
            if (freq.Count != 2)
            {
                return false;
            }
            foreach (KeyValuePair<string, int> pair in freq)
            {
                if (pair.Value != ONE && pair.Value != QUADS)
                {
                    return false;
                }
            }
            return true;
        }

        private int HashFourOfAKind(List<PlayingCard> hand)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            foreach (PlayingCard card in hand)
            {
                if (freq.ContainsKey(card.Value))
                {
                    freq[card.Value]++;
                }
                else
                {
                    freq[card.Value] = INIT;
                }
            }
            int result = 0;
            foreach (KeyValuePair<string, int> pair in freq)
            {
                if (pair.Value == ONE)
                {
                    result += cardValues[pair.Key] * pows[0];
                }
                else
                {
                    result += cardValues[pair.Key] * pows[1];
                }
            }
            return result;
        }

        private bool IsFullHouse(List<PlayingCard> hand)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            foreach (PlayingCard card in hand)
            {
                if (freq.ContainsKey(card.Value))
                {
                    freq[card.Value]++;
                }
                else
                {
                    freq[card.Value] = INIT;
                }
            }
            if (freq.Count != 2)
            {
                return false;
            }
            foreach (KeyValuePair<string, int> pair in freq)
            {
                if (pair.Value != PAIR && pair.Value != TRIPS)
                {
                    return false;
                }
            }
            return true;
        }

        private int HashFullHouse(List<PlayingCard> hand)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            foreach (PlayingCard card in hand)
            {
                if (freq.ContainsKey(card.Value))
                {
                    freq[card.Value]++;
                }
                else
                {
                    freq[card.Value] = INIT;
                }
            }
            int result = 0;
            foreach (KeyValuePair<string, int> pair in freq)
            {
                if (pair.Value == PAIR)
                {
                    result += cardValues[pair.Key] * pows[0];
                }
                else
                {
                    result += cardValues[pair.Key] * pows[1];
                }
            }
            return result;
        }
        private bool IsFlush(List<PlayingCard> hand)
        {
            HashSet<string> suits = new HashSet<string>();
            foreach (PlayingCard card in hand)
            {
                suits.Add(card.Suit);
            }

            return suits.Count == 1;
        }

        private int HashFlush(List<PlayingCard> hand)
        {
            int result = 0, freebit = 4;
            foreach (PlayingCard card in hand)
            {
                result += cardValues[card.Value] * pows[freebit--];
            }
            return result;
        }

        private bool IsStraight(List<PlayingCard> hand)
        {
            bool isStraight = true;
            for (int i = 0; i < hand.Count - 1; i++)
            {
                if (cardValues[hand[i].Value] - 1 != cardValues[hand[i + 1].Value] && (hand[i].Value != "A" || hand[i + 1].Value != "5"))
                {
                    isStraight = false;
                }
            }
            return isStraight;
        }

        private int HashStraight(List<PlayingCard> hand)
        {
            if (hand[0].Value == "A" && hand[1].Value == "5")
            {
                return cardValues[hand[1].Value] * pows[0];
            }
            else
            {
                return cardValues[hand[0].Value] * pows[0];
            }
        }

        private bool IsThreeOfAKind(List<PlayingCard> hand)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            foreach (PlayingCard card in hand)
            {
                if (freq.ContainsKey(card.Value))
                {
                    freq[card.Value]++;
                }
                else
                {
                    freq[card.Value] = INIT;
                }
            }

            foreach (KeyValuePair<string, int> pair in freq)
            {
                if (pair.Value == TRIPS)
                {
                    return true;
                }
            }
            return false;
        }

        private int HashThreeOfAKind(List<PlayingCard> hand)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            int freebit = 1, result = 0;
            foreach (PlayingCard card in hand)
            {
                if (freq.ContainsKey(card.Value))
                {
                    freq[card.Value]++;
                }
                else
                {
                    freq[card.Value] = INIT;
                }
            }
            foreach (PlayingCard card in hand)
            {
                if (freq[card.Value] == TRIPS)
                {
                    result += cardValues[card.Value] * pows[2];
                    freq[card.Value] = NULL;
                }
                else if (freq[card.Value] != NULL)
                {
                    result += cardValues[card.Value] * pows[freebit--];
                }
            }
            return result;
        }

        private bool IsTwoPairs(List<PlayingCard> hand)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            foreach (PlayingCard card in hand)
            {
                if (freq.ContainsKey(card.Value))
                {
                    freq[card.Value]++;
                }
                else
                {
                    freq[card.Value] = INIT;
                }
            }
            return freq.Count == 3;
        }

        private int HashTwoPairs(List<PlayingCard> hand)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            int freebit = 2, result = 0;
            foreach (PlayingCard card in hand)
            {
                if (freq.ContainsKey(card.Value))
                {
                    freq[card.Value]++;
                }
                else
                {
                    freq[card.Value] = INIT;
                }
            }
            foreach (PlayingCard card in hand)
            {
                if (freq[card.Value] == PAIR)
                {
                    result += cardValues[card.Value] * pows[freebit--];
                    freq[card.Value] = NULL;
                }
                else if (freq[card.Value] != NULL)
                {
                    result += cardValues[card.Value] * pows[0];
                }
            }
            return result;
        }

        private bool IsOnePair(List<PlayingCard> hand)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            foreach (PlayingCard card in hand)
            {
                if (freq.ContainsKey(card.Value))
                {
                    freq[card.Value]++;
                }
                else
                {
                    freq[card.Value] = INIT;
                }
            }
            return freq.Count == 4;
        }

        private int HashOnePair(List<PlayingCard> hand)
        {
            Dictionary<string, int> freq = new Dictionary<string, int>();
            int freebit = 2, result = 0;
            foreach (PlayingCard card in hand)
            {
                if (freq.ContainsKey(card.Value))
                {
                    freq[card.Value]++;
                }
                else
                {
                    freq[card.Value] = INIT;
                }
            }
            foreach (PlayingCard card in hand)
            {
                if (freq[card.Value] == PAIR)
                {
                    result += cardValues[card.Value] * pows[3];
                    freq[card.Value] = NULL;
                }
                else if (freq[card.Value] != NULL)
                {
                    result += cardValues[card.Value] * pows[freebit--];
                }
            }
            return result;
        }

        private int HashHighCard(List<PlayingCard> hand)
        {
            int freebit = 4, result = 0;
            foreach (PlayingCard card in hand)
            {
                result += cardValues[card.Value] * pows[freebit--];
            }
            return result;
        }

        public Tuple<int, int> GetValue(List<PlayingCard> hand)
        {
            HandSort(hand);
            if (IsRoyalFlush(hand))
            {
                return new Tuple<int, int>(10, HashRoyalFlush(hand));
            }

            if (IsStraightFlush(hand))
            {
                return new Tuple<int, int>(9, HashStraightFlush(hand));
            }

            if (IsFourOfAKind(hand))
            {
                return new Tuple<int, int>(8, HashFourOfAKind(hand));
            }

            if (IsFullHouse(hand))
            {
                return new Tuple<int, int>(7, HashFullHouse(hand));
            }

            if (IsFlush(hand))
            {
                return new Tuple<int, int>(6, HashFlush(hand));
            }

            if (IsStraight(hand))
            {
                return new Tuple<int, int>(5, HashStraight(hand));
            }

            if (IsThreeOfAKind(hand))
            {
                return new Tuple<int, int>(4, HashThreeOfAKind(hand));
            }

            if (IsTwoPairs(hand))
            {
                return new Tuple<int, int>(3, HashTwoPairs(hand));
            }

            if (IsOnePair(hand))
            {
                return new Tuple<int, int>(2, HashOnePair(hand));
            }

            return new Tuple<int, int>(1, HashHighCard(hand));
        }
    }
}
