using HarvestHaven.Entities;
using HarvestHaven.Repositories;
using HarvestHaven.Services;
using HarvestHaven.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.Design;

namespace HarvestHaven.Tests.Services
{
    [TestClass]
    public class FarmServiceTests
    {
        private User currentUser;

        [TestInitialize]
        public void Setup()
        {
            currentUser = new User(Guid.NewGuid(), "TestUser", 100, 5, 2, DateTime.Now, DateTime.Now);
            GameStateManager.SetCurrentUser(currentUser);
        }

        [TestMethod]
        public async Task GetAllFarmCellsForUser_WhenValidUserId_ReturnsDictionary()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var farmCells = new List<FarmCell>
            {
                new FarmCell(Guid.NewGuid(), userId, 1, 1, Guid.NewGuid(), DateTime.Now, DateTime.Now),
                new FarmCell(Guid.NewGuid(), userId, 2, 2, Guid.NewGuid(), DateTime.Now, DateTime.Now)
            };
            var items = new List<Item>
            {
                new Item(Guid.NewGuid(), ItemType.CarrotSeeds, Guid.NewGuid(), Guid.NewGuid(), null),
                new Item(Guid.NewGuid(), ItemType.TomatoSeeds, Guid.NewGuid(), Guid.NewGuid(), null)
            };

            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellsAsync(userId)).ReturnsAsync(farmCells);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(It.IsAny<Guid>())).ReturnsAsync(items[0]);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, itemRepositoryMock.Object, null);

            // Act
            var result = await farmService.GetAllFarmCellsForUser(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(farmCells.Count, result.Count);
            foreach (var farmCell in farmCells)
            {
                Assert.IsTrue(result.ContainsKey(farmCell));
            }
        }

        [TestMethod]
        public async Task GetAllFarmCellsForUser_WhenOneCellHasLastInteractionLongerThanLifetime_DeletesCell()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var farmCells = new List<FarmCell>
            {
                new FarmCell(Guid.NewGuid(), userId, 1, 1, Guid.NewGuid(), DateTime.Now, DateTime.Now.AddDays(-10)), // Last interaction was 10 days ago
                new FarmCell(Guid.NewGuid(), userId, 2, 2, Guid.NewGuid(), DateTime.Now, DateTime.Now.AddDays(-1)) // Last interaction was 1 day ago
            };
            var items = new List<Item>
            {
                new Item(Guid.NewGuid(), ItemType.CarrotSeeds, Guid.NewGuid(), Guid.NewGuid(), null),
                new Item(Guid.NewGuid(), ItemType.TomatoSeeds, Guid.NewGuid(), Guid.NewGuid(), null)
            };

            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellsAsync(userId)).ReturnsAsync(farmCells);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(It.IsAny<Guid>())).ReturnsAsync(items[0]);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, itemRepositoryMock.Object, null);

            // Act
            var result = await farmService.GetAllFarmCellsForUser(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ContainsKey(farmCells[1]));
            Assert.IsFalse(result.ContainsKey(farmCells[0]));
        }
        [TestMethod]
        public async Task GetAllFarmCellsForUser_WhenItemsIsNull_ThrowsException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var farmCells = new List<FarmCell>
            {
                new FarmCell(Guid.NewGuid(), userId, 1, 1, Guid.NewGuid(), DateTime.Now, DateTime.Now.AddDays(-10)), // Last interaction was 10 days ago
                new FarmCell(Guid.NewGuid(), userId, 2, 2, Guid.NewGuid(), DateTime.Now, DateTime.Now.AddDays(-1)) // Last interaction was 1 day ago
            };
            var items = new List<Item>
            {
                new Item(Guid.NewGuid(), ItemType.CarrotSeeds, Guid.NewGuid(), Guid.NewGuid(), null),
                new Item(Guid.NewGuid(), ItemType.TomatoSeeds, Guid.NewGuid(), Guid.NewGuid(), null)
            };

            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellsAsync(userId)).ReturnsAsync(farmCells);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Item) null);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, itemRepositoryMock.Object, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                // Act: Call the method that should throw an exception
                await farmService.GetAllFarmCellsForUser(userId);
            });

        }

        [TestMethod]
        public async Task InteractWithCell_WhenValidPosition_InteractsAndUpdatesInventory()
        {
            // Arrange
            var row = 1;
            var column = 1;
            var farmCell = new FarmCell(Guid.NewGuid(), currentUser.Id, row, column, Guid.NewGuid(), DateTime.Now, DateTime.Now);
            var item = new Item(Guid.NewGuid(), ItemType.CarrotSeeds, Guid.NewGuid(), Guid.NewGuid(), null);
            var interactResource = new InventoryResource(Guid.NewGuid(), currentUser.Id, item.ResourceToInteractId, 10);
            var requiredResource = new InventoryResource(Guid.NewGuid(), currentUser.Id, item.ResourceToPlaceId, 5);

            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(currentUser.Id, row, column)).ReturnsAsync(farmCell);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(farmCell.ItemId)).ReturnsAsync(item);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(currentUser.Id, item.ResourceToInteractId)).ReturnsAsync(interactResource);
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(currentUser.Id, item.ResourceToPlaceId)).ReturnsAsync(requiredResource);

            var achievementServiceMock = new Mock<IAchievementService>();

            var farmService = new FarmService(achievementServiceMock.Object, farmCellRepositoryMock.Object, itemRepositoryMock.Object, inventoryResourceRepositoryMock.Object);

            // Act
            await farmService.InteractWithCell(row, column);

            // Assert
            inventoryResourceRepositoryMock.Verify(repo => repo.UpdateUserResourceAsync(interactResource), Times.Once);
            inventoryResourceRepositoryMock.Verify(repo => repo.UpdateUserResourceAsync(requiredResource), Times.Once);

        }

        [TestMethod]
        public async Task InteractWithCell_WhenUserIsNotLoggedIn_ThrowsException()
        {
            // Arrange
            var farmService = new FarmService(null, null, null, null);
            // Ensure that there is no current user logged in
            GameStateManager.SetCurrentUser(null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                // Act: Call the method that should throw an exception
                await farmService.InteractWithCell(1, 1);
            });

        }

        [TestMethod]
        public async Task InteractWithCell_WhenFarmCellIsNull_ThrowsException()
        {
            // Arrange
            var row = 1;
            var column = 1;
            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(currentUser.Id, row, column)).ReturnsAsync((FarmCell)null);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                // Act: Call the method that should throw an exception
                await farmService.InteractWithCell(row, column);
            });
        }

        [TestMethod]
        public async Task InteractWithCell_WhenFarmCellItemIsNull_ThrowsException()
        {
            // Arrange
            var row = 1;
            var column = 1;
            var farmCell = new FarmCell(Guid.NewGuid(), currentUser.Id, row, column, Guid.NewGuid(), DateTime.Now, DateTime.Now);
            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(currentUser.Id, row, column)).ReturnsAsync(farmCell);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(farmCell.ItemId)).ReturnsAsync((Item)null);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, itemRepositoryMock.Object, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                // Act: Call the method that should throw an exception
                await farmService.InteractWithCell(row, column);
            });
        }

        [TestMethod]
        public async Task InteractWithCell_WhenRequiredResourceIsNull_ThrowsException()
        {
            // Arrange
            var row = 1;
            var column = 1;
            var farmCell = new FarmCell(Guid.NewGuid(), currentUser.Id, row, column, Guid.NewGuid(), DateTime.Now, DateTime.Now);
            var item = new Item(Guid.NewGuid(), ItemType.CarrotSeeds, Guid.NewGuid(), Guid.NewGuid(), null);
            var interactResource = new InventoryResource(Guid.NewGuid(), currentUser.Id, item.ResourceToInteractId, 10);

            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(currentUser.Id, row, column)).ReturnsAsync(farmCell);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(farmCell.ItemId)).ReturnsAsync(item);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(currentUser.Id, item.ResourceToInteractId)).ReturnsAsync(interactResource);
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(currentUser.Id, item.ResourceToPlaceId)).ReturnsAsync((InventoryResource)null);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, itemRepositoryMock.Object, inventoryResourceRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                // Act: Call the method that should throw an exception
                await farmService.InteractWithCell(row, column);
            });
        }

        [TestMethod]
        public async Task InteractWithCell_WhenInteractResourceIsNull_CreateANewEntryAndInteractsAndUpdatesInventory()
        {
            // Arrange
            var row = 1;
            var column = 1;
            var farmCell = new FarmCell(Guid.NewGuid(), currentUser.Id, row, column, Guid.NewGuid(), DateTime.Now.AddDays(-3), DateTime.Now); // Set LastTimeEnhanced to a date in the past
            var item = new Item(Guid.NewGuid(), ItemType.CarrotSeeds, Guid.NewGuid(), Guid.NewGuid(), null);
            var requiredResource = new InventoryResource(Guid.NewGuid(), currentUser.Id, item.ResourceToPlaceId, 5);

            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(currentUser.Id, row, column)).ReturnsAsync(farmCell);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(farmCell.ItemId)).ReturnsAsync(item);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(currentUser.Id, item.ResourceToPlaceId)).ReturnsAsync(requiredResource);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, itemRepositoryMock.Object, inventoryResourceRepositoryMock.Object);

            //Act
            farmService.InteractWithCell(row, column);

            // Assert
            inventoryResourceRepositoryMock.Verify(repo => repo.UpdateUserResourceAsync(requiredResource), Times.Once);
        }


        [TestMethod]
        public async Task DestroyCell_WhenValidPosition_DestroyAndRemoveFromDatabase()
        {
            // Arrange
            var row = 1;
            var column = 1;
            var farmCell = new FarmCell(Guid.NewGuid(), currentUser.Id, row, column, Guid.NewGuid(), DateTime.Now, DateTime.Now);
            var item = new Item(Guid.NewGuid(), ItemType.CarrotSeeds, Guid.NewGuid(), Guid.NewGuid(), null);

            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(currentUser.Id, row, column)).ReturnsAsync(farmCell);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(farmCell.ItemId)).ReturnsAsync(item);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();

            var achievementServiceMock = new Mock<IAchievementService>();

            var farmService = new FarmService(achievementServiceMock.Object, farmCellRepositoryMock.Object, itemRepositoryMock.Object, inventoryResourceRepositoryMock.Object);

            // Act
            await farmService.DestroyCell(row, column);

            // Assert
            farmCellRepositoryMock.Verify(repo => repo.DeleteFarmCellAsync(farmCell.Id), Times.Once);
        }

        [TestMethod]
        public async Task DestroyCell_WhenUserIsNotLoggedIn_ThrowsException()
        {
            // Arrange
            var farmService = new FarmService(null, null, null, null);
            // Ensure that there is no current user logged in
            GameStateManager.SetCurrentUser(null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                // Act: Call the method that should throw an exception
                await farmService.DestroyCell(1, 1);
            });

        }

        [TestMethod]
        public async Task DestroyCell_WhenFarmCellIsNull_ThrowsException()
        {
            // Arrange
            var row = 1;
            var column = 1;
            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(currentUser.Id, row, column)).ReturnsAsync((FarmCell)null);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                // Act: Call the method that should throw an exception
                await farmService.DestroyCell(row, column);
            });
        }

        [TestMethod]
        public async Task DestroyCell_WhenFarmCellItemIsNull_ThrowsException()
        {
            // Arrange
            var row = 1;
            var column = 1;
            var farmCell = new FarmCell(Guid.NewGuid(), currentUser.Id, row, column, Guid.NewGuid(), DateTime.Now, DateTime.Now);
            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(currentUser.Id, row, column)).ReturnsAsync(farmCell);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(farmCell.ItemId)).ReturnsAsync((Item)null);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, itemRepositoryMock.Object, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                // Act: Call the method that should throw an exception
                await farmService.DestroyCell(row, column);
            });
        }
        [TestMethod]
        public async Task DestroyCell_WhenFarmCellHasADestroyResourceAndNotNull_DestroyAndRemoveFromDatabase()
        {
            // Arrange
            var row = 1;
            var column = 1;
            var farmCell = new FarmCell(Guid.NewGuid(), currentUser.Id, row, column, Guid.NewGuid(), DateTime.Now, DateTime.Now);
            var item = new Item(Guid.NewGuid(), ItemType.CarrotSeeds, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            var destroyResource = new InventoryResource(Guid.NewGuid(), currentUser.Id, (Guid) item.ResourceToDestroyId, 10);

            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(currentUser.Id, row, column)).ReturnsAsync(farmCell);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(farmCell.ItemId)).ReturnsAsync(item);

            var inventoryResourceRepositoryMock = new Mock<IInventoryResourceRepository>();
            inventoryResourceRepositoryMock.Setup(repo => repo.GetUserResourceByResourceIdAsync(currentUser.Id, (Guid) item.ResourceToDestroyId)).ReturnsAsync(destroyResource);

            var achievementServiceMock = new Mock<IAchievementService>();

            var farmService = new FarmService(achievementServiceMock.Object, farmCellRepositoryMock.Object, itemRepositoryMock.Object, inventoryResourceRepositoryMock.Object);

            // Act
            await farmService.DestroyCell(row, column);

            // Assert
            inventoryResourceRepositoryMock.Verify(repo => repo.UpdateUserResourceAsync(destroyResource), Times.Once);
            farmCellRepositoryMock.Verify(repo => repo.DeleteFarmCellAsync(farmCell.Id), Times.Once);
        }

        
        [TestMethod]
        public async Task EnchanceCellForUser_WhenValidUserIdAndPosition_UpdatesCellEnhancement()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var row = 1;
            var column = 1;
            var farmCell = new FarmCell(Guid.NewGuid(), userId, row, column, Guid.NewGuid(), DateTime.UtcNow.AddDays(-2), DateTime.Now); // Set LastTimeEnhanced to a date in the past
            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(userId, row, column)).ReturnsAsync(farmCell);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, null, null);

            // Act
            await farmService.EnchanceCellForUser(userId, row, column);

            // Assert
            Assert.IsTrue((DateTime.UtcNow - farmCell.LastTimeEnhanced) < TimeSpan.FromDays(Constants.ENCHANCE_DURATION_IN_DAYS));
        }

        [TestMethod]
        public async Task EnhanceCellForUser_WhenUserIsNotLoggedIn_ThrowsException()
        {
            // Arrange
            var farmService = new FarmService(null, null, null, null);
            // Ensure that there is no current user logged in
            GameStateManager.SetCurrentUser(null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                // Act: Call the method that should throw an exception
                await farmService.EnchanceCellForUser(currentUser.Id, 1, 1);
            });

        }

        [TestMethod]
        public async Task EnhanceCellForUser_WhenFarmCellIsNull_ThrowsException()
        {
            // Arrange
            var row = 1;
            var column = 1;
            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(currentUser.Id, row, column)).ReturnsAsync((FarmCell)null);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, null, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                // Act: Call the method that should throw an exception
                await farmService.EnchanceCellForUser(currentUser.Id, row, column);
            });
        }

        [TestMethod]
        public async Task EnhanceCellForUser_WhenFarmCellItemIsNull_ThrowsException()
        {
            // Arrange
            var row = 1;
            var column = 1;
            var farmCell = new FarmCell(Guid.NewGuid(), currentUser.Id, row, column, Guid.NewGuid(), DateTime.Now, DateTime.Now);
            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(currentUser.Id, row, column)).ReturnsAsync(farmCell);

            var itemRepositoryMock = new Mock<IItemRepository>();
            itemRepositoryMock.Setup(repo => repo.GetItemByIdAsync(farmCell.ItemId)).ReturnsAsync((Item)null);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, itemRepositoryMock.Object, null);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                // Act: Call the method that should throw an exception
                await farmService.EnchanceCellForUser(currentUser.Id, row, column);
            });
        }

        [TestMethod]
        public async Task IsCellEnchanced_WhenValidUserIdAndPosition_ReturnsTrueIfEnhanced()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var row = 1;
            var column = 1;
            var farmCell = new FarmCell(Guid.NewGuid(), userId, row, column, Guid.NewGuid(), DateTime.UtcNow.AddDays(-2), DateTime.Now); // Ensure the cell is enhanced
            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(userId, row, column)).ReturnsAsync(farmCell);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, null, null);

            // Act
            await farmService.EnchanceCellForUser(userId, row, column);
            var isEnhanced = await farmService.IsCellEnchanced(userId, row, column);

            // Assert
            Assert.IsTrue(isEnhanced);
        }

        [TestMethod]
        public async Task IsCellEnhanced_WhenValidUserIdAndPosition_ReturnsFalseIfNotEnhanced()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var row = 1;
            var column = 1;
            var farmCell = new FarmCell(Guid.NewGuid(), userId, row, column, Guid.NewGuid(), DateTime.UtcNow.AddDays(-1), DateTime.Now); // Ensure the cell is not enhanced
            var farmCellRepositoryMock = new Mock<IFarmCellRepository>();
            farmCellRepositoryMock.Setup(repo => repo.GetUserFarmCellByPositionAsync(userId, row, column)).ReturnsAsync(farmCell);

            var farmService = new FarmService(null, farmCellRepositoryMock.Object, null, null);

            // Act
            var isEnhanced = await farmService.IsCellEnchanced(userId, row, column);

            // Assert
            Assert.IsFalse(isEnhanced);
        }


        [TestMethod]
        public void GetPicturePathByItemType_WhenValidItemType_ReturnsCorrectPath()
        {
            // Arrange
            var farmService = new FarmService(null, null, null, null);

            // Act
            var carrotPath = farmService.GetPicturePathByItemType(ItemType.CarrotSeeds);
            var cornPath = farmService.GetPicturePathByItemType(ItemType.CornSeeds);
            var wheatPath = farmService.GetPicturePathByItemType(ItemType.WheatSeeds);
            var tomatoPath = farmService.GetPicturePathByItemType(ItemType.TomatoSeeds);
            var chickenPath = farmService.GetPicturePathByItemType(ItemType.Chicken);
            var duckPath = farmService.GetPicturePathByItemType(ItemType.Duck);
            var sheepPath = farmService.GetPicturePathByItemType(ItemType.Sheep);
            var cowPath = farmService.GetPicturePathByItemType(ItemType.Cow);

            // Assert
            Assert.AreEqual(Constants.CarrotPath, carrotPath);
            Assert.AreEqual(Constants.CornPath, cornPath);
            Assert.AreEqual(Constants.WheatPath, wheatPath);
            Assert.AreEqual(Constants.TomatoPath, tomatoPath);
            Assert.AreEqual(Constants.ChickenPath, chickenPath);
            Assert.AreEqual(Constants.DuckPath, duckPath);
            Assert.AreEqual(Constants.SheepPath, sheepPath);
            Assert.AreEqual(Constants.CowPath, cowPath);
        }
    }
}
