using GameWorld.Entities;
using GameWorld.Repositories;
using GameWorld.Resources.Utils;
using Moq;

namespace GameWorld.Services.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private User currentUser;

        [TestInitialize]
        public void Setup()
        {
            currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.Now, DateTime.Now);
            GameStateManager.SetCurrentUser(currentUser);
        }

        [TestMethod]
        public async Task GetUserByIdAsync_WhenValidUserId_ReturnsUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedUser = new User(userId, "TestUser", 100, 5, 2, DateTime.Now, DateTime.Now);
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(expectedUser);
            var userService = new UserService(userRepositoryMock.Object, null, null, null);

            // Act
            var result = await userService.GetUserByIdAsync(userId);

            // Assert
            Assert.AreEqual(expectedUser, result);
        }

        [TestMethod]
        public async Task GetInventoryResources_AllValidParameters_ReturnsDictionary()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User(userId, "TestUser", 100, 5, 2, DateTime.Now, DateTime.Now);
            Guid resource1Id = Guid.NewGuid();
            Guid resource2Id = Guid.NewGuid();
            var inventoryResources = new List<InventoryResource>
            {
                new InventoryResource(Guid.NewGuid(), userId, resource1Id, 10),
                new InventoryResource(Guid.NewGuid(), userId, resource2Id, 5)
            };
            var resources = new List<Resource>
            {
                new Resource(resource1Id, ResourceType.Steak),
                new Resource(resource2Id, ResourceType.SheepWool)
            };

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourcesAsync(userId)).ReturnsAsync(inventoryResources);

            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByIdAsync(inventoryResources[0].ResourceId)).ReturnsAsync(resources[0]);
            resourceRepositoryMock.Setup(repo => repo.GetResourceByIdAsync(inventoryResources[1].ResourceId)).ReturnsAsync(resources[1]);

            var userService = new UserService(null, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, null);

            // Act
            var result = await userService.GetInventoryResources(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(inventoryResources.Count, result.Count);
            foreach (var inventoryResource in inventoryResources)
            {
                Assert.IsTrue(result.ContainsKey(inventoryResource));
            }
        }

        [TestMethod]
        public async Task GetInventoryResources_CorrespondingResourceNotFound_ThrowsException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User(userId, "TestUser", 100, 5, 2, DateTime.Now, DateTime.Now);
            Guid resource1Id = Guid.NewGuid();
            Guid resource2Id = Guid.NewGuid();
            var inventoryResources = new List<InventoryResource>
            {
                new InventoryResource(Guid.NewGuid(), userId, resource1Id, 10),
                new InventoryResource(Guid.NewGuid(), userId, resource2Id, 5)
            };
            var resources = new List<Resource>
            {
                new Resource(resource1Id, ResourceType.Steak),
                new Resource(resource2Id, ResourceType.SheepWool)
            };

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourcesAsync(userId)).ReturnsAsync(inventoryResources);

            var resourceRepositoryMock = new Mock<IResourceRepository>();

            var userService = new UserService(null, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, null);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await userService.GetInventoryResources(userId));
        }

        [TestMethod]
        public async Task GetInventoryResourceByType_WhenResourceExists_ReturnsInventoryResource()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User(userId, "TestUser", 100, 5, 2, DateTime.Now, DateTime.Now);
            var expectedResourceType = ResourceType.Steak;
            Guid resourceId = Guid.NewGuid();
            var expectedInventoryResource = new InventoryResource(Guid.NewGuid(), userId, resourceId, 10);
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(expectedResourceType)).ReturnsAsync(new Resource(resourceId, expectedResourceType));
            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(userId, resourceId)).ReturnsAsync(expectedInventoryResource);
            var userService = new UserService(null, inventoryResourceRepositoryMock.Object, resourceRepositoryMock.Object, null);

            // Act
            var result = await userService.GetInventoryResourceByType(expectedResourceType, userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedInventoryResource, result);
        }

        [TestMethod]
        public async Task GetInventoryResourceByType_ResourceNotFound_ThrowsException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User(userId, "TestUser", 100, 5, 2, DateTime.Now, DateTime.Now);
            var resourceType = ResourceType.Water;
            var resourceRepositoryMock = new Mock<IResourceRepository>();
            resourceRepositoryMock.Setup(repo => repo.GetResourceByTypeAsync(resourceType)).ReturnsAsync((Resource)null);
            var userService = new UserService(null, null, resourceRepositoryMock.Object, null);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await userService.GetInventoryResourceByType(resourceType, userId));
        }

        [TestMethod]
        public async Task GetInventoryResourceByType_WhenUserIsNotLoggedIn_ThrowsException()
        {
            // Arrange
            GameStateManager.SetCurrentUser(null);
            var userService = new UserService(null, null, null, null);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await userService.GetInventoryResourceByType(ResourceType.Steak, currentUser.Id));
        }

        [TestMethod]
        public void IsTradeHallUnlocked_UserIsLoggedInAndTradeHallIsUnlocked_ReturnsTrue()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(-10)); // Trade hall unlocked 5 days ago
            GameStateManager.SetCurrentUser(user);

            var userService = new UserService(null, null, null, null);

            // Act
            var result = userService.IsTradeHallUnlocked();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsTradeHallUnlocked_UserIsLoggedInAndTradeHallIsNotUnlocked_ReturnsFalse()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, null, DateTime.UtcNow.AddDays(-5)); // Trade hall not unlocked
            GameStateManager.SetCurrentUser(user);

            var userService = new UserService(null, null, null, null);

            // Act
            var result = userService.IsTradeHallUnlocked();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsTradeHallUnlocked_UserIsNotLoggedIn_ThrowsException()
        {
            // Arrange
            GameStateManager.SetCurrentUser(null);

            var userService = new UserService(null, null, null, null);

            // Act & Assert
            Assert.ThrowsException<Exception>(() => userService.IsTradeHallUnlocked());
        }

        [TestMethod]
        public async Task UnlockTradeHall_UserIsNotLoggedIn_ThrowsException()
        {
            // Arrange
            GameStateManager.SetCurrentUser(null);

            var userService = new UserService(null, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await userService.UnlockTradeHall());
        }

        [TestMethod]
        public async Task UnlockTradeHall_TradeHallAlreadyUnlocked_ThrowsException()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "TestUser", Constants.TRADEHALL_UNLOCK_PRICE + 10, 5, 2, DateTime.UtcNow.AddDays(-5), DateTime.UtcNow.AddDays(-10)); // Trade hall already unlocked
            GameStateManager.SetCurrentUser(user);

            var userService = new UserService(null, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await userService.UnlockTradeHall());
        }

        [TestMethod]
        public async Task UnlockTradeHall_UserDoesNotHaveEnoughCoins_ThrowsException()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "TestUser", Constants.TRADEHALL_UNLOCK_PRICE - 10, 5, 2, null, DateTime.UtcNow.AddDays(-10)); // Not enough coins to unlock
            GameStateManager.SetCurrentUser(user);

            var userService = new UserService(null, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await userService.UnlockTradeHall());
        }

        [TestMethod]
        public async Task UnlockTradeHall_UserIsLoggedInAndTradeHallNotUnlockedAndEnoughCoins_UnlocksTradeHall()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "TestUser", Constants.TRADEHALL_UNLOCK_PRICE + 10, 5, 2, null, DateTime.UtcNow.AddDays(-10)); // Enough coins to unlock
            GameStateManager.SetCurrentUser(user);

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.UpdateUserAsync(user)).Returns(Task.CompletedTask);


            var userService = new UserService(userRepositoryMock.Object, null, null, null);

            // Act
            await userService.UnlockTradeHall();

            // Assert
            Assert.IsTrue((DateTime.UtcNow.Day - user.TradeHallUnlockTime.Value.Day) < Constants.TRADEHALL_LIFETIME_IN_DAYS);
            Assert.AreEqual(user.Coins, Constants.TRADEHALL_UNLOCK_PRICE + 10 - Constants.TRADEHALL_UNLOCK_PRICE); // Coins deducted
        }
        [TestMethod]
        public async Task AddCommentForAnotherUser_UserIsLoggedIn_CommentAddedSuccessfully()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);

            var targetUserId = Guid.NewGuid();
            var message = "Test comment";

            var commentRepositoryMock = new Mock<ICommentRepository>();
            commentRepositoryMock.Setup(repo => repo.CreateCommentAsync(It.IsAny<Comment>())).Returns(Task.CompletedTask);

            var userService = new UserService(null, null, null, commentRepositoryMock.Object);

            // Act
            await userService.AddCommentForAnotherUser(targetUserId, message);

            // Assert
            commentRepositoryMock.Verify(repo => repo.CreateCommentAsync(It.Is<Comment>(c =>
                c.PosterUserId == targetUserId &&
                c.CommentMessage == message &&
                c.CreationTime <= DateTime.UtcNow
            )), Times.Once);
        }

        [TestMethod]
        public async Task AddCommentForAnotherUser_UserIsNotLoggedIn_ThrowsException()
        {
            // Arrange
            GameStateManager.SetCurrentUser(null);
            var userService = new UserService(null, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await userService.AddCommentForAnotherUser(Guid.NewGuid(), "Test comment"));
        }

        [TestMethod]
        public async Task GetMyComments_UserIsLoggedIn_ReturnsUserComments()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);
            var expectedComments = new List<Comment> { new Comment(Guid.NewGuid(), currentUser.Id, "Test comment", DateTime.UtcNow) };

            var commentRepositoryMock = new Mock<ICommentRepository>();
            commentRepositoryMock.Setup(repo => repo.GetUserCommentsAsync(currentUser.Id)).ReturnsAsync(expectedComments);

            var userService = new UserService(null, null, null, commentRepositoryMock.Object);

            // Act
            var result = await userService.GetMyComments();

            // Assert
            CollectionAssert.AreEqual(expectedComments, result);
        }

        [TestMethod]
        public async Task GetMyComments_UserIsNotLoggedIn_ThrowsException()
        {
            // Arrange
            GameStateManager.SetCurrentUser(null);
            var userService = new UserService(null, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await userService.GetMyComments());
        }

        [TestMethod]
        public async Task DeleteComment_UserIsLoggedIn_CommentDeletedSuccessfully()
        {
            // Arrange
            var currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow);
            GameStateManager.SetCurrentUser(currentUser);
            var commentId = Guid.NewGuid();

            var commentRepositoryMock = new Mock<ICommentRepository>();
            commentRepositoryMock.Setup(repo => repo.DeleteCommentAsync(commentId)).Returns(Task.CompletedTask);

            var userService = new UserService(null, null, null, commentRepositoryMock.Object);

            // Act
            await userService.DeleteComment(commentId);

            // Assert
            commentRepositoryMock.Verify(repo => repo.DeleteCommentAsync(commentId), Times.Once);
        }

        [TestMethod]
        public async Task DeleteComment_UserIsNotLoggedIn_ThrowsException()
        {
            // Arrange
            GameStateManager.SetCurrentUser(null);
            var userService = new UserService(null, null, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () => await userService.DeleteComment(Guid.NewGuid()));
        }
        [TestMethod]
        public async Task GetAllUsersSortedByCoinsAsync_ReturnsUsersSortedByCoins()
        {
            // Arrange
            var users = new List<User>
            {
                new User(Guid.NewGuid(), "User1", 100, 5, 2, DateTime.UtcNow, DateTime.UtcNow),
                new User(Guid.NewGuid(), "User2", 200, 5, 2, DateTime.UtcNow, DateTime.UtcNow),
                new User(Guid.NewGuid(), "User3", 50, 5, 2, DateTime.UtcNow, DateTime.UtcNow)
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(users);

            var userService = new UserService(userRepositoryMock.Object, null, null, null);

            // Act
            var result = await userService.GetAllUsersSortedByCoinsAsync();

            // Assert
            CollectionAssert.AreEqual(users, result);
            CollectionAssert.AreEqual(users, result, Comparer<User>.Create((u1, u2) => u2.Coins.CompareTo(u1.Coins)));
        }
    }
}
