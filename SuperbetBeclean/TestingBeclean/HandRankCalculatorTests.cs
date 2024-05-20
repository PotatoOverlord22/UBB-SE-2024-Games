using SuperbetBeclean.Model;

namespace SuperbetBeclean.TestingBeclean
{
    public class HandRankCalculatorTests
    {
        private HandRankCalculator calculator;

        [SetUp]
        public void SetUp()
        {
            calculator = new HandRankCalculator();
        }

        [Test]
        public void GetValue_RoyalFlush_ReturnsCorrectValueAndHash()
        {
            var hand = new List<PlayingCard>
            {
            new PlayingCard("A", "H"),
            new PlayingCard("K", "H"),
            new PlayingCard("Q", "H"),
            new PlayingCard("J", "H"),
            new PlayingCard("10", "H")
            };

            var result = calculator.GetValue(hand);

            Assert.That(result.Item1, Is.EqualTo(10));
            Assert.That(result.Item2, Is.EqualTo(759375));
        }

        [Test]
        public void GetValue_StraightFlush_ReturnsCorrectValueAndHash()
        {
            var hand = new List<PlayingCard>
            {
            new PlayingCard("8", "H"),
            new PlayingCard("7", "H"),
            new PlayingCard("6", "H"),
            new PlayingCard("5", "H"),
            new PlayingCard("4", "H")
            };

            var result = calculator.GetValue(hand);

            Assert.That(result.Item1, Is.EqualTo(9));
            Assert.That(result.Item2, Is.EqualTo(50625));
        }

        [Test]
        public void GetValue_FourOfAKind_ReturnsCorrectValueAndHash()
        {
            var hand = new List<PlayingCard>
            {
            new PlayingCard("K", "H"),
            new PlayingCard("K", "C"),
            new PlayingCard("K", "D"),
            new PlayingCard("K", "S"),
            new PlayingCard("7", "H")
            };

            var result = calculator.GetValue(hand);

            Assert.That(result.Item1, Is.EqualTo(8));
            Assert.That(result.Item2, Is.EqualTo(1020320));
        }

        [Test]
        public void GetValue_FullHouse_ReturnsCorrectValueAndHash()
        {
            var hand = new List<PlayingCard>
            {
            new PlayingCard("A", "H"),
            new PlayingCard("A", "C"),
            new PlayingCard("A", "D"),
            new PlayingCard("K", "H"),
            new PlayingCard("K", "C")
            };

            var result = calculator.GetValue(hand);

            Assert.That(result.Item1, Is.EqualTo(7));
            Assert.That(result.Item2, Is.EqualTo(1339290));
        }

        [Test]
        public void GetValue_Flush_ReturnsCorrectValueAndHash()
        {
            var hand = new List<PlayingCard>
            {
            new PlayingCard("A", "H"),
            new PlayingCard("K", "H"),
            new PlayingCard("7", "H"),
            new PlayingCard("5", "H"),
            new PlayingCard("3", "H")
            };

            var result = calculator.GetValue(hand);

            Assert.That(result.Item1, Is.EqualTo(6));
            Assert.That(result.Item2, Is.EqualTo(977610));
        }

        [Test]
        public void GetValue_Straight_ReturnsCorrectValueAndHash()
        {
            var hand = new List<PlayingCard>
            {
            new PlayingCard("8", "H"),
            new PlayingCard("7", "C"),
            new PlayingCard("6", "D"),
            new PlayingCard("5", "S"),
            new PlayingCard("4", "H")
            };

            var result = calculator.GetValue(hand);

            Assert.That(result.Item1, Is.EqualTo(5));
            Assert.That(result.Item2, Is.EqualTo(50625));
        }

        [Test]
        public void GetValue_ThreeOfAKind_ReturnsCorrectValueAndHash()
        {
            var hand = new List<PlayingCard>
            {
            new PlayingCard("Q", "H"),
            new PlayingCard("Q", "C"),
            new PlayingCard("Q", "D"),
            new PlayingCard("8", "H"),
            new PlayingCard("6", "C")
            };

            var result = calculator.GetValue(hand);

            Assert.That(result.Item1, Is.EqualTo(4));
            Assert.That(result.Item2, Is.EqualTo(129600));
        }

        [Test]
        public void GetValue_TwoPairs_ReturnsCorrectValueAndHash()
        {
            var hand = new List<PlayingCard>
            {
            new PlayingCard("K", "H"),
            new PlayingCard("K", "C"),
            new PlayingCard("7", "D"),
            new PlayingCard("7", "H"),
            new PlayingCard("2", "C")
            };

            var result = calculator.GetValue(hand);

            Assert.That(result.Item1, Is.EqualTo(3));
            Assert.That(result.Item2, Is.EqualTo(1426260));
        }

        [Test]
        public void GetValue_OnePair_ReturnsCorrectValueAndHash()
        {
            var hand = new List<PlayingCard>
            {
            new PlayingCard("10", "H"),
            new PlayingCard("10", "C"),
            new PlayingCard("7", "D"),
            new PlayingCard("5", "H"),
            new PlayingCard("3", "C")
            };

            var result = calculator.GetValue(hand);

            Assert.That(result.Item1, Is.EqualTo(2));
            Assert.That(result.Item2, Is.EqualTo(121800));
        }

        [Test]
        public void GetValue_HighCard_ReturnsCorrectValueAndHash()
        {
            var hand = new List<PlayingCard>
            {
            new PlayingCard("A", "H"),
            new PlayingCard("K", "C"),
            new PlayingCard("8", "D"),
            new PlayingCard("6", "H"),
            new PlayingCard("4", "C")
            };

            var result = calculator.GetValue(hand);

            Assert.That(result.Item1, Is.EqualTo(1));
            Assert.That(result.Item2, Is.EqualTo(93750));
        }
    }
}
