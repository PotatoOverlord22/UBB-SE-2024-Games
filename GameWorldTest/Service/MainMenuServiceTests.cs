using GameWorld.Entities;
using GameWorld.Resources.Utils;

namespace GameWorld.Services.Tests
{
    [TestClass]
    public class MainMenuServiceTests
    {
        [TestMethod]
        public void Constructor_UserExists_SetsUserName()
        {
            // Arrange
            var expectedUserName = "testUser";
            var user = new User(Guid.NewGuid(), expectedUserName, 100, 0, 0, null, null);
            GameStateManager.SetCurrentUser(user);

            var mainMenuService = new MainMenuService();

            // Assert
            Assert.AreEqual(expectedUserName, mainMenuService.UserName);
        }

        [TestMethod]
        public void Constructor_UserDoesNotExist_UserNameIsNull()
        {
            // Arrange
            GameStateManager.SetCurrentUser(null);

            var mainMenuService = new MainMenuService();

            // Act

            // Assert
            Assert.IsNull(mainMenuService.UserName);
        }

        [TestMethod]
        public void WelcomeMessage_UserNameIsSet_ReturnsCorrectMessage()
        {
            // Arrange
            var expectedUserName = "testUser";
            var expectedMessage = $"Welcome, {expectedUserName}!";
            var user = new User(Guid.NewGuid(), expectedUserName, 100, 0, 0, null, null);
            GameStateManager.SetCurrentUser(user);

            var mainMenuService = new MainMenuService();

            // Act
            var result = mainMenuService.WelcomeUserMessage;

            // Assert
            Assert.AreEqual(expectedMessage, result);
        }

        [TestMethod]
        public void WelcomeMessage_UserNameIsNull_ReturnsDefaultMessage()
        {
            // Arrange
            var expectedMessage = "Welcome, !";
            GameStateManager.SetCurrentUser(null);

            var mainMenuService = new MainMenuService();

            // Act
            var result = mainMenuService.WelcomeUserMessage;

            // Assert
            Assert.AreEqual(expectedMessage, result);
        }
    }
}
