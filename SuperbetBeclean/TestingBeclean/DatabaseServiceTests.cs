using SuperbetBeclean.Models;
using SuperbetBeclean.Services;

namespace SuperbetBeclean.TestingBeclean
{
    /*[TestFixture]
    public class DatabaseServiceTests
    {
        private IDataBaseService databaseService;

        [SetUp]
        public void Setup()
        {
            databaseService = new DataBaseService();
        }

        [TearDown]
        public void TearDown()
        {
        }

        // UpdateUser
        [Test]
        public void TestUpdateUser_UpdatesUserInDatabase_ReturnsTrue()
        {
            int userId = 1;
            string newUsername = "NewUsername";
            int newChips = 15000;

            databaseService.UpdateUser(userId, newUsername, currentFont: 1, currentTitle: 1, currentIcon: 1, currentTable: 1, chips: newChips, stack: 200, streak: 11, handsPlayed: 10, level: 10, lastLogin: DateTime.Now);

            int updatedChips = databaseService.GetChipsByUserId(userId);
            string updatedUsername = databaseService.GetUserNameByUserId(userId);
            Assert.That(newChips, Is.EqualTo(updatedChips));
            Assert.That(newUsername, Is.EqualTo(updatedUsername));
        }

        // UpdateUserFont
        [Test]
        public void TestUpdateUserFont_InvalidUserId_ThrowsException()
        {
            int invalidUserId = -1;
            int fontValue = 1;
            Assert.Throws<ArgumentException>(() => databaseService.UpdateUserFont(invalidUserId, fontValue));
        }

        // UpdateUserTitle
        [Test]
        public void TestUpdateUserTitle_InvalidUserId_ThrowsException()
        {
            int invalidUserId = -1;
            int titleValue = 1;
            Assert.Throws<ArgumentException>(() => databaseService.UpdateUserTitle(invalidUserId, titleValue));
        }

        // UpdateUserIcon
        [Test]
        public void TestUpdateUserIcon_InvalidUserId_ThrowsException()
        {
            int invalidUserId = -1;
            int iconValue = 1;
            Assert.Throws<ArgumentException>(() => databaseService.UpdateUserIcon(invalidUserId, iconValue));
        }

        // UpdateUserChips
        [Test]
        public void TestUpdateUserChips_ValidUserIdAndChips_ChangesUserChips()
        {
            int userId = 1;
            int initialChips = databaseService.GetChipsByUserId(userId);
            int chipsToAdd = 500;
            int expectedChips = initialChips + chipsToAdd;

            databaseService.UpdateUserChips(userId, initialChips + chipsToAdd);
            int updatedChips = databaseService.GetChipsByUserId(userId);
            Assert.That(updatedChips, Is.EqualTo(expectedChips));
        }

        [Test]
        public void TestUpdateUserChips_NegativeChips_ThrowsException()
        {
            int userId = 1;
            int negativeChips = -100;
            Assert.Throws<ArgumentException>(() => databaseService.UpdateUserChips(userId, negativeChips));
        }

        // UpdateUserStack
        [Test]
        public void TestUpdateUserStack_InvalidUserId_ThrowsException()
        {
            int invalidUserId = -1;
            int stackValue = 100;
            Assert.Throws<ArgumentException>(() => databaseService.UpdateUserStack(invalidUserId, stackValue));
        }

        // UpdateUserStreak
        [Test]
        public void TestUpdateUserStreak_InvalidUserId_ThrowsException()
        {
            int invalidUserId = -1;
            int streakValue = 5;
            Assert.Throws<ArgumentException>(() => databaseService.UpdateUserStreak(invalidUserId, streakValue));
        }

        // UpdateUserHandsPlayed
        [Test]
        public void TestUpdateUserHandsPlayed_InvalidUserId_ThrowsException()
        {
            int invalidUserId = -1;
            int handsPlayedValue = 50;
            Assert.Throws<ArgumentException>(() => databaseService.UpdateUserHandsPlayed(invalidUserId, handsPlayedValue));
        }

        // UpdateUserLevel
        [Test]
        public void TestUpdateUserLevel_InvalidUserId_ThrowsException()
        {
            int invalidUserId = -1;
            int levelValue = 10;
            Assert.Throws<ArgumentException>(() => databaseService.UpdateUserLevel(invalidUserId, levelValue));
        }

        // UpdateUserLastLogin
        [Test]
        public void TestUpdateUserLastLogin_InvalidUserId_ThrowsException()
        {
            int invalidUserId = -1;
            DateTime lastLoginValue = DateTime.Now.AddDays(-1);
            Assert.Throws<ArgumentException>(() => databaseService.UpdateUserLastLogin(invalidUserId, lastLoginValue));
        }

        // UpdateChallenge
        [Test]
        public void TestUpdateChallenge_InvalidChallengeId_ThrowsException()
        {
            int invalidChallengeId = -1;
            string description = "Test Description";
            string rule = "Test Rule";
            int amount = 100;
            int reward = 50;
            Assert.Throws<ArgumentException>(() => databaseService.UpdateChallenge(invalidChallengeId, description, rule, amount, reward));
        }

        // UpdateFont
        [Test]
        public void TestUpdateFont_InvalidFontId_ThrowsException()
        {
            int invalidFontId = -1;
            string fontName = "Test Font";
            int fontPrice = 100;
            string fontType = "Test Type";
            Assert.Throws<ArgumentException>(() => databaseService.UpdateFont(invalidFontId, fontName, fontPrice, fontType));
        }

        // UpdateIcon
        [Test]
        public void TestUpdateIcon_InvalidIconId_ThrowsException()
        {
            int invalidIconId = -1;
            string iconName = "Test Icon";
            int iconPrice = 100;
            string iconPath = "test/icon/path";
            Assert.Throws<ArgumentException>(() => databaseService.UpdateIcon(invalidIconId, iconName, iconPrice, iconPath));
        }

        // UpdateTitle
        [Test]
        public void TestUpdateTitle_InvalidTitleId_ThrowsException()
        {
            int invalidTitleId = -1;
            string titleName = "Test Title";
            int titlePrice = 100;
            string titleContent = "Test Content";
            Assert.Throws<ArgumentException>(() => databaseService.UpdateTitle(invalidTitleId, titleName, titlePrice, titleContent));
        }

        // GetIconPath
        [Test]
        public void TestGetIconPath_ExistingIconId_ReturnsPath()
        {
            int existingIconId = 1;
            string iconPath = databaseService.GetIconPath(existingIconId);
            Assert.That(iconPath, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void TestGetIconPath_NonExistingIconId_ThrowsException()
        {
            int nonExistingIconId = -1;
            Assert.Throws<Exception>(() => databaseService.GetIconPath(nonExistingIconId));
        }

        // GetLeaderboard
        [Test]
        public void TestGetLeaderboard_ReturnsLeaderboardList()
        {
            var leaderboard = databaseService.GetLeaderboard();
            Assert.That(leaderboard, Is.Not.Null.And.Not.Empty);
        }

        // GetShopItems
        [Test]
        public void TestGetShopItems_ReturnsShopItemList()
        {
            var shopItems = databaseService.GetShopItems();
            Assert.That(shopItems, Is.Not.Null.And.Not.Empty);
        }

        // GetAllUserIconsByUserId
        [Test]
        public void TestGetAllUserIconsByUserId_ReturnsUserIconList()
        {
            int userId = 1;
            var userIcons = databaseService.GetAllUserIconsByUserId(userId);
            Assert.That(userIcons, Is.Not.Null);
        }

        [Test]
        public void TestGetAllUserIconsByUserId_InvalidUserId_ReturnsEmptyList()
        {
            int invalidUserId = -1;
            List<ShopItem> result = databaseService.GetAllUserIconsByUserId(invalidUserId);
            Assert.That(result, Is.Empty);
        }

        // CreateUserIcon
        [Test]
        public void TestCreateUserIcon_InvalidUserId_ThrowsException()
        {
            int invalidUserId = -1;
            int validIconId = 1;
            Assert.That(() => databaseService.CreateUserIcon(invalidUserId, validIconId), Throws.ArgumentException);
        }

        [Test]
        public void TestCreateUserIcon_InvalidIconId_ThrowsException()
        {
            int validUserId = 1;
            int invalidIconId = -1;
            Assert.That(() => databaseService.CreateUserIcon(validUserId, invalidIconId), Throws.ArgumentException);
        }

        // GetIconIDByIconName
        [Test]
        public void TestGetIconIDByIconName_InvalidIconName_ReturnsNegativeOne()
        {
            string invalidIconName = "InvalidIconName";
            int result = databaseService.GetIconIDByIconName(invalidIconName);
            Assert.That(result, Is.EqualTo(-1));
        }

        // SetCurrentIcon
        [Test]
        public void TestSetCurrentIcon_InvalidUserId_ThrowsException()
        {
            int invalidUserId = -1;
            int validIconId = 1;
            Assert.Throws<ArgumentException>(() => databaseService.SetCurrentIcon(invalidUserId, validIconId));
        }

        [Test]
        public void TestSetCurrentIcon_InvalidIconId_ThrowsException()
        {
            int validUserId = 1;
            int invalidIconId = -1;
            Assert.Throws<ArgumentException>(() => databaseService.SetCurrentIcon(validUserId, invalidIconId));
        }

        // GetAllRequestsByToUserIDSimplified
        [Test]
        public void TestGetAllRequestsByToUserIDSimplified_InvalidUserId_ReturnsEmptyList()
        {
            int invalidUserId = -1;
            List<Tuple<int, int>> result = databaseService.GetAllRequestsByToUserIDSimplified(invalidUserId);
            Assert.That(result, Is.Empty);
        }

        // GetAllRequestsByToUserID
        [Test]
        public void TestGetAllRequestsByToUserID_ReturnsListOfRequests()
        {
            int userId = 1;
            List<string> requests = databaseService.GetAllRequestsByToUserID(userId);
            Assert.That(requests, Is.Not.Null);
            Assert.That(requests, Has.Count.GreaterThan(0));
        }

        // GetUserNameByUserId
        [Test]
        public void TestGetUserNameByUserId_ExistingUserId_ReturnsUserName()
        {
            int existingUserId = 1;
            string username = databaseService.GetUserNameByUserId(existingUserId);
            Assert.That(username, Is.Not.Null);
        }

        [Test]
        public void TestGetUserNameByUserId_NonExistingUserId_ReturnsNull()
        {
            int nonExistingUserId = -1;
            string username = databaseService.GetUserNameByUserId(nonExistingUserId);
            Assert.That(username, Is.Null);
        }

        // GetUserIdByUserName
        [Test]
        public void TestGetUserIdByUserName_ExistingUsername_ReturnsUserId()
        {
            string existingUsername = "player1";
            int userId = databaseService.GetUserIdByUserName(existingUsername);
            Assert.That(userId, Is.GreaterThan(0));
        }

        [Test]
        public void TestGetUserIdByUserName_NonExistingUsername_ReturnsNegativeOne()
        {
            string nonExistingUsername = "nonexistinguser";
            int userId = databaseService.GetUserIdByUserName(nonExistingUsername);
            Assert.That(userId, Is.EqualTo(-1));
        }

        // CreateRequest
        [Test]
        public void TestCreateRequest_InvalidFromUserId_ThrowsException()
        {
            int invalidFromUserId = -1;
            int validToUserId = 1;
            Assert.Throws<ArgumentException>(() => databaseService.CreateRequest(invalidFromUserId, validToUserId));
        }

        [Test]
        public void TestCreateRequest_InvalidToUserId_ThrowsException()
        {
            int validFromUserId = 1;
            int invalidToUserId = -1;
            Assert.Throws<ArgumentException>(() => databaseService.CreateRequest(validFromUserId, invalidToUserId));
        }

        // DeleteRequestsByUserId
        [Test]
        public void TestDeleteRequestsByUserId_InvalidUserId_NoExceptionThrown()
        {
            int invalidUserId = -1;
            Assert.DoesNotThrow(() => databaseService.DeleteRequestsByUserId(invalidUserId));
        }
    }
    */
}