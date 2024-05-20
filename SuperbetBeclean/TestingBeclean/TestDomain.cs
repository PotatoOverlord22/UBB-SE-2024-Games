using SuperbetBeclean.Model;
using SuperbetBeclean.Models;

namespace TestingBeclean
{
    public class TestsDomain
    {
        private Icon icon;
        private Icon icon1;
        private Font font;
        private Font font1;
        private Challenge challenge;
        private Challenge challenge1;
        private Table table;
        private Table table1;
        private Title title;
        private Title title1;
        private CardDeck cardDeck;
        private User user;
        private User user1;

        [SetUp]
        public void Setup()
        {
            this.icon = new Icon();
            this.icon1 = new Icon();
            this.font = new Font();
            this.font1 = new Font();
            this.challenge = new Challenge();
            this.challenge1 = new Challenge();
            this.table = new Table();
            this.table1 = new Table();
            this.title = new Title();
            this.title1 = new Title();
            this.user1 = new User();
            this.user = new User();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void TestIconGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.icon.IconID, Is.EqualTo(this.icon1.IconID));
        }

        [TestCase(1)]
        public void TestIconSetter_SetToADifferentValue_TheGetterIsEqualToThatValue(int newId)
        {
            this.icon.IconID = newId;
            Assert.That(this.icon.IconID, Is.EqualTo(newId));
        }

        [Test]
        public void TestIconNameGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.icon.IconName, Is.EqualTo(this.icon1.IconName));
        }

        [TestCase("icon1")]
        public void TestIconNameSetter_SetToADifferentValue_TheGetterIsEqualToThatValue(string iconName)
        {
            this.icon.IconName = iconName;
            Assert.That(this.icon.IconName, Is.EqualTo(iconName));
        }

        [Test]
        public void TestIconPriceGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.icon.IconPrice, Is.EqualTo(this.icon1.IconPrice));
        }

        [TestCase(1)]
        public void TestIconPriceSetter_SetToADifferentValue_TheGetterIsEqualToThatValue(int newPrice)
        {
            this.icon.IconPrice = newPrice;
            Assert.That(this.icon.IconPrice, Is.EqualTo(newPrice));
        }

        [Test]
        public void TestIconPathGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.icon.IconPath, Is.EqualTo(this.icon1.IconPath));
        }

        [TestCase("iconPath")]
        public void TestIconPathSetter_SetToADifferentValue_TheGetterIsEqualToThatValue(string newPath)
        {
            this.icon.IconPath = newPath;
            Assert.That(this.icon.IconPath, Is.EqualTo(newPath));
        }

        [Test]
        public void TestFontGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.font.FontID, Is.EqualTo(this.font1.FontID));
        }

        [TestCase(1)]
        public void TestFontSetter_SetToADifferentValue_TheGetterIsEqualToThatValue(int newFontId)
        {
            this.font.FontID = newFontId;
            Assert.That(this.font.FontID, Is.EqualTo(newFontId));
        }

        [Test]
        public void TestFontNameGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.font.FontName, Is.EqualTo(this.font1.FontName));
        }

        [TestCase("font1")]
        public void TestFontNameSetter_IsEqualToWhatWasPassedInTheConstructor_True(string newFontName)
        {
            this.font.FontName = newFontName;
            Assert.That(this.font.FontName, Is.EqualTo(newFontName));
        }

        [Test]
        public void TestFontPriceGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.font.FontPrice, Is.EqualTo(this.font1.FontPrice));
        }

        [TestCase(1)]
        public void TestFontPriceSetter_IsEqualToWhatWasPassedInTheConstructor_True(int newFontPrice)
        {
            this.font.FontPrice = newFontPrice;
            Assert.That(this.font.FontPrice, Is.EqualTo(newFontPrice));
        }

        [Test]
        public void TestFontTypeGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.font.FontType, Is.EqualTo(this.font1.FontType));
        }

        [TestCase("fontType")]
        public void TestFontTypeSetter_SetToADifferentValue_TheGetterIsEqualToThatValue(string newFontType)
        {
            this.font.FontType = newFontType;
            Assert.That(this.font.FontType, Is.EqualTo(newFontType));
        }

        [Test]
        public void TestChallengeGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.challenge.ChallengeID, Is.EqualTo(this.challenge1.ChallengeID));
        }

        [TestCase(1)]
        public void TestChallengeSetter_SetToADifferentValue_TheGetterIsEqualToThatValue(int newChallengeId)
        {
            this.challenge.ChallengeID = newChallengeId;
            Assert.That(this.challenge.ChallengeID, Is.EqualTo(newChallengeId));
        }

        [Test]
        public void TestChallengeDescription_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.challenge.ChallengeDescription, Is.EqualTo(this.challenge1.ChallengeDescription));
        }

        [TestCase("challenge1")]
        public void TestChallengeDescriptionSetter_SetToADifferentValue_TheGetterIsEqualToThatValue(string newChallengeDesc)
        {
            this.challenge.ChallengeDescription = newChallengeDesc;
            Assert.That(this.challenge.ChallengeDescription, Is.EqualTo(newChallengeDesc));
        }

        [Test]
        public void TestChallengeRuleGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.challenge.ChallengeRule, Is.EqualTo(this.challenge1.ChallengeRule));
        }

        [TestCase("challengeRule")]
        public void TestChallengeRuleSetter_SetToADifferentValue_TheGetterIsEqualToThatValue(string newChallengeRule)
        {
            this.challenge.ChallengeRule = newChallengeRule;
            Assert.That(this.challenge.ChallengeRule, Is.EqualTo(newChallengeRule));
        }

        [Test]
        public void TestChallengeAmountGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.challenge.ChallengeAmount, Is.EqualTo(this.challenge1.ChallengeAmount));
        }

        [TestCase(1)]
        public void TestChallengeAmountSetter_SetToADifferentValue_TheGetterIsEqualToThatValue(int newChallengeAmmount)
        {
            this.challenge.ChallengeAmount = newChallengeAmmount;
            Assert.That(this.challenge.ChallengeAmount, Is.EqualTo(newChallengeAmmount));
        }

        [Test]
        public void TestChallengeRewardGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.challenge.ChallengeReward, Is.EqualTo(this.challenge1.ChallengeReward));
        }

        [TestCase(1)]
        public void TestChallengeRewardSetter_SetToADifferentValue_TheGetterIsEqualToThatValue(int newChallengeReward)
        {
            this.challenge.ChallengeReward = newChallengeReward;
            Assert.That(this.challenge.ChallengeReward, Is.EqualTo(newChallengeReward));
        }

        [TestCase(1, "imagePath", "name", 1, 0)]
        public void TestShopItem_IsEqualToWhatWasPassedInTheConstructor_True(int newId, string newImagePath, string newName, int newPrice, int expectedId)
        {
            ShopItem shopItem = new ShopItem(newId, newImagePath, newName, newPrice);

            Assert.That(shopItem.Id, Is.EqualTo(newId));
            Assert.That(shopItem.ImagePath, Is.EqualTo(newImagePath));
            Assert.That(shopItem.Name, Is.EqualTo(newName));
            Assert.That(shopItem.Price, Is.EqualTo(newPrice));
            Assert.That(shopItem.UserId, Is.EqualTo(expectedId));
        }

        [Test]
        public void TestTableGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.table.TableID, Is.EqualTo(this.table1.TableID));
        }

        [Test]
        public void TestTableConstructor_AreConstructorValuesCorrect_ReturnsTrue()
        {
            int tableId = 1;
            string tableName = "table";
            int tableBuyIn = 100;
            int tablePlayerLimit = 10;

            Table table = new Table(tableId, tableName, tableBuyIn, tablePlayerLimit);

            Assert.That(table.TableID, Is.EqualTo(tableId));
            Assert.That(table.TableName, Is.EqualTo(tableName));
            Assert.That(table.TableBuyIn, Is.EqualTo(tableBuyIn));
            Assert.That(table.TablePlayerLimit, Is.EqualTo(tablePlayerLimit));
        }

        [Test]
        public void TestTableSetter__SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.table.TableID = 1;
            Assert.That(this.table.TableID, Is.EqualTo(1));
        }

        [Test]
        public void TestTableNameGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.table.TableName, Is.EqualTo(this.table1.TableName));
        }

        [Test]
        public void TestTableNameSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.table.TableName = "tableName";
            Assert.That(this.table.TableName, Is.EqualTo("tableName"));
        }

        [Test]
        public void TestTableBuyInGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.table.TableBuyIn, Is.EqualTo(this.table1.TableBuyIn));
        }

        [Test]
        public void TestTableBuyInSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.table.TableBuyIn = 1;
            Assert.That(this.table.TableBuyIn, Is.EqualTo(1));
        }

        [Test]
        public void TestTablePlayerLimitGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.table.TablePlayerLimit, Is.EqualTo(this.table1.TablePlayerLimit));
        }

        [Test]
        public void TestTablePlayerLimitSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.table.TablePlayerLimit = 1;
            Assert.That(this.table.TablePlayerLimit, Is.EqualTo(1));
        }

        [Test]
        public void TestTitleIdGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.title.TitleID, Is.EqualTo(this.title1.TitleID));
        }

        [Test]
        public void TestTitleIdSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.title.TitleID = 1;
            Assert.That(this.title.TitleID, Is.EqualTo(1));
        }

        [Test]
        public void TestTitleNameGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.title.TitleName, Is.EqualTo(this.title1.TitleName));
        }

        [Test]
        public void TestTitleNameSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.title.TitleName = "titleName";
            Assert.That(this.title.TitleName, Is.EqualTo("titleName"));
        }

        [Test]
        public void TestTitlePriceGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.title.TitlePrice, Is.EqualTo(this.title1.TitlePrice));
        }

        [Test]
        public void TestTitlePriceSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.title.TitlePrice = 1;
            Assert.That(this.title.TitlePrice, Is.EqualTo(1));
        }

        [Test]
        public void TestTitleContentGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.title.TitleContent, Is.EqualTo(this.title1.TitleContent));
        }

        [Test]
        public void TestTitleContentSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.title.TitleContent = "titleContent";
            Assert.That(this.title.TitleContent, Is.EqualTo("titleContent"));
        }

        public void TestCardDeck_GetDeckSize_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.cardDeck.GetDeckSize(), Is.EqualTo(52));
        }

        [Test]
        public void TestCardDeck_RemoveCardFromIndex_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            CardDeck cardDeck = new CardDeck();
            cardDeck.RemoveCardFromIndex(0);
            Assert.That(cardDeck.GetDeckSize(), Is.EqualTo(51));
        }

        [Test]
        public void TestCardDeck_GetCardFromIndex_ShowsProperCard_ReturnsTrue()
        {
            CardDeck cardDeck = new CardDeck();
            PlayingCard cardTest = new ("2", "H");
            PlayingCard cardFromDeck = cardDeck.GetCardFromIndex(0);

            string cardTestValue = cardTest.Value;
            string cardTestSuit = cardTest.Suit;
            string cardFromDeckValue = cardFromDeck.Value;
            string cardFromDeckSuit = cardFromDeck.Suit;

            Assert.That(cardTestValue, Is.EqualTo(cardFromDeckValue));
            Assert.That(cardTestSuit, Is.EqualTo(cardFromDeckSuit));
        }

        [Test]
        public void TestChallengeIDGetter_IsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.challenge.ChallengeID, Is.EqualTo(this.challenge1.ChallengeID));
        }

        [Test]
        public void TestChallengeIDSetter_SetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.challenge.ChallengeID = 1;
            Assert.That(this.challenge.ChallengeID, Is.EqualTo(1));
        }

        [Test]
        public void TestChallengeDescriptionGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.challenge.ChallengeDescription, Is.EqualTo(this.challenge1.ChallengeDescription));
        }

        [Test]
        public void TestChallengeDescriptionSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.challenge.ChallengeDescription = "challengeDescription";
            Assert.That(this.challenge.ChallengeDescription, Is.EqualTo("challengeDescription"));
        }

        [Test]
        public void TestChallengeRuleGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.challenge.ChallengeRule, Is.EqualTo(this.challenge1.ChallengeRule));
        }

        [Test]
        public void TestChallengeRuleSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.challenge.ChallengeRule = "challengeRule";
            Assert.That(this.challenge.ChallengeRule, Is.EqualTo("challengeRule"));
        }

        [Test]
        public void TestChallengeAmountGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.challenge.ChallengeAmount, Is.EqualTo(this.challenge1.ChallengeAmount));
        }

        [Test]
        public void TestChallengeAmountSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.challenge.ChallengeAmount = 1;
            Assert.That(this.challenge.ChallengeAmount, Is.EqualTo(1));
        }

        [Test]
        public void TestChallengeRewardGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.challenge.ChallengeReward, Is.EqualTo(this.challenge1.ChallengeReward));
        }

        [Test]
        public void TestChallengeRewardSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.challenge.ChallengeReward = 1;
            Assert.That(this.challenge.ChallengeReward, Is.EqualTo(1));
        }

        [Test]
        public void TestPlayingCardValueGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            PlayingCard playingCard = new PlayingCard("value", "suit");
            PlayingCard playingCard1 = new PlayingCard("value", "suit");
            Assert.That(playingCard.Value, Is.EqualTo(playingCard1.Value));
        }

        [Test]
        public void TestPlayingCardValueSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            PlayingCard playingCard = new PlayingCard("value", "suit");
            playingCard.Value = "value";
            Assert.That(playingCard.Value, Is.EqualTo("value"));
        }

        [Test]
        public void TestPlayingCardSuitGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            PlayingCard playingCard = new PlayingCard("value", "suit");
            PlayingCard playingCard1 = new PlayingCard("value", "suit");
            Assert.That(playingCard.Suit, Is.EqualTo(playingCard1.Suit));
        }

        [Test]
        public void TestPlayingCardSuitSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            PlayingCard playingCard = new PlayingCard("value", "suit");
            playingCard.Suit = "suit";
            Assert.That(playingCard.Suit, Is.EqualTo("suit"));
        }

        [Test]
        public void TestPlayingCard_CompleteInformation_ReturnsTrue()
        {
            PlayingCard playingCard = new PlayingCard("10", "H");
            string completeInformationAboutCard = playingCard.CompleteInformation();

            Assert.That(completeInformationAboutCard, Is.EqualTo("10H"));
        }

        [Test]
        public void TestUserIDGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserID, Is.EqualTo(this.user1.UserID));
        }

        [Test]
        public void TestUserIDSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserID = 1;
            Assert.That(this.user.UserID, Is.EqualTo(1));
        }

        [Test]
        public void TestUserNameGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserName, Is.EqualTo(this.user1.UserName));
        }

        [Test]
        public void TestUserNameSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserName = "userName";
            Assert.That(this.user.UserName, Is.EqualTo("userName"));
        }

        [Test]
        public void TestUserCurrentFontGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserCurrentFont, Is.EqualTo(this.user1.UserCurrentFont));
        }

        [Test]
        public void TestUserCurrentFontSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserCurrentFont = 1;
            Assert.That(this.user.UserCurrentFont, Is.EqualTo(1));
        }

        [Test]
        public void TestUserCurrentTitleGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserCurrentTitle, Is.EqualTo(this.user1.UserCurrentTitle));
        }

        [Test]
        public void TestUserCurrentTitleSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserCurrentTitle = 2;
            Assert.That(this.user.UserCurrentTitle, Is.EqualTo(2));
        }

        [Test]
        public void TestUserCurrentIconPathGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserCurrentIconPath, Is.EqualTo(this.user1.UserCurrentIconPath));
        }

        [Test]
        public void TestUserCurrentIconPathSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserCurrentIconPath = "userCurrentIconPath";
            Assert.That(this.user.UserCurrentIconPath, Is.EqualTo("userCurrentIconPath"));
        }

        [Test]
        public void TestUserCurrentTableGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserCurrentTable, Is.EqualTo(this.user1.UserCurrentTable));
        }

        [Test]
        public void TestUserCurrentTableSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserCurrentTable = 3;
            Assert.That(this.user.UserCurrentTable, Is.EqualTo(3));
        }

        [Test]
        public void TestUserChipsGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserChips, Is.EqualTo(this.user1.UserChips));
        }

        [Test]
        public void TestUserChipsSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserChips = 4;
            Assert.That(this.user.UserChips, Is.EqualTo(4));
        }

        [Test]
        public void TestUserStackGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserStack, Is.EqualTo(this.user1.UserStack));
        }

        [Test]
        public void TestUserStackSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserStack = 5;
            Assert.That(this.user.UserStack, Is.EqualTo(5));
        }

        [Test]
        public void TestUserStreakGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserStreak, Is.EqualTo(this.user1.UserStreak));
        }

        [Test]
        public void TestUserStreakSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserStreak = 6;
            Assert.That(this.user.UserStreak, Is.EqualTo(6));
        }

        [Test]
        public void TestUserHandsPlayedGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserHandsPlayed, Is.EqualTo(this.user1.UserHandsPlayed));
        }

        [Test]
        public void TestUserHandsPlayedSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserHandsPlayed = 7;
            Assert.That(this.user.UserHandsPlayed, Is.EqualTo(7));
        }

        [Test]
        public void TestUserLevelGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserLevel, Is.EqualTo(this.user1.UserLevel));
        }

        [Test]
        public void TestUserLevelSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserLevel = 8;
            Assert.That(this.user.UserLevel, Is.EqualTo(8));
        }

        [Test]
        public void TestUserLastLogInGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserLastLogin, Is.EqualTo(this.user1.UserLastLogin));
        }

        [Test]
        public void TestUserLastLogInSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            DateTime current_time = DateTime.Now;
            this.user.UserLastLogin = current_time;
            Assert.That(this.user.UserLastLogin, Is.EqualTo(current_time));
        }

        [Test]
        public void TestUserStatusGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserStatus, Is.EqualTo(this.user1.UserStatus));
        }

        [Test]
        public void TestUserStatusSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserStatus = 1;
            Assert.That(this.user.UserStatus, Is.EqualTo(1));
        }

        [Test]
        public void TestUserBetGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserBet, Is.EqualTo(this.user1.UserBet));
        }

        [Test]
        public void TestUserBetSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserBet = 2;
            Assert.That(this.user.UserBet, Is.EqualTo(2));
        }

        [Test]
        public void TestUserCurrentHandGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserCurrentHand, Is.EqualTo(this.user1.UserCurrentHand));
        }

        [Test]
        public void TestUserCurrentHandSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserCurrentHand = new PlayingCard[2];
            Assert.That(this.user.UserCurrentHand, Is.EqualTo(new PlayingCard[2]));
        }

        [Test]
        public void TestUserTablePlaceGetterIsEqualToWhatWasPassedInTheConstructor_True()
        {
            Assert.That(this.user.UserTablePlace, Is.EqualTo(this.user1.UserTablePlace));
        }

        [Test]
        public void TestUserTablePlaceSetterSetToADifferentValue_TheGetterIsEqualToThatValue()
        {
            this.user.UserTablePlace = 3;
            Assert.That(this.user.UserTablePlace, Is.EqualTo(3));
        }
    }
}