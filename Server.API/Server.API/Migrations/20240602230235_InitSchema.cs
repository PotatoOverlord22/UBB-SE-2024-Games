using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.API.Migrations
{
    /// <inheritdoc />
    public partial class InitSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfCoinsRewarded = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    AmountOfItemsBought = table.Column<int>(type: "int", nullable: false),
                    AmountOfTradesPerformed = table.Column<int>(type: "int", nullable: false),
                    TradeHallUnlockTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastTimeReceivedWater = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserLevel = table.Column<int>(type: "int", nullable: false),
                    UserStatus = table.Column<int>(type: "int", nullable: false),
                    UserLastLogin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    ResourceToPlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResourceToInteractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResourceToDestroyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Resources_ResourceToDestroyId",
                        column: x => x.ResourceToDestroyId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Resources_ResourceToInteractId",
                        column: x => x.ResourceToInteractId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Items_Resources_ResourceToPlaceId",
                        column: x => x.ResourceToPlaceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MarketSellResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceToSellId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SellPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketSellResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketSellResources_Resources_ResourceToSellId",
                        column: x => x.ResourceToSellId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PosterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Users_PosterId",
                        column: x => x.PosterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryResources_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResourceToGiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResourceToGiveQuantity = table.Column<int>(type: "int", nullable: false),
                    ResourceToGetResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResourceToGetQuantity = table.Column<int>(type: "int", nullable: false),
                    TradeCreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trades_Resources_ResourceToGetResourceId",
                        column: x => x.ResourceToGetResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trades_Resources_ResourceToGiveId",
                        column: x => x.ResourceToGiveId,
                        principalTable: "Resources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trades_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAchievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AchievementId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AchievementRewardedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAchievements_Achievements_AchievementId",
                        column: x => x.AchievementId,
                        principalTable: "Achievements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAchievements_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FarmCells",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastTimeEnhanced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastTimeInteracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarmCells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FarmCells_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FarmCells_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketBuyItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketBuyItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketBuyItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WinnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Turn = table.Column<int>(type: "int", nullable: false),
                    TimePlayed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Port = table.Column<int>(type: "int", nullable: true),
                    GameStateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_GameStates_GameStateId",
                        column: x => x.GameStateId,
                        principalTable: "GameStates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameStats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EloRating = table.Column<int>(type: "int", nullable: false),
                    HighestElo = table.Column<int>(type: "int", nullable: false),
                    TotalMatches = table.Column<int>(type: "int", nullable: false),
                    TotalWins = table.Column<int>(type: "int", nullable: false),
                    TotalDraws = table.Column<int>(type: "int", nullable: false),
                    TotalPlayTime = table.Column<int>(type: "int", nullable: false),
                    TotalNumberOfTurn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameStats_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerQueue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EloRating = table.Column<int>(type: "int", nullable: false),
                    ObstractionWidth = table.Column<int>(type: "int", nullable: true),
                    ObstractionHeight = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerQueue_Games_GameTypeId",
                        column: x => x.GameTypeId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerQueue_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PosterId",
                table: "Comments",
                column: "PosterId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmCells_ItemId",
                table: "FarmCells",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmCells_UserId",
                table: "FarmCells",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_WinnerId",
                table: "GameStates",
                column: "WinnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStats_GameId",
                table: "GameStats",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStats_PlayerId",
                table: "GameStats",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryResources_OwnerId",
                table: "InventoryResources",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryResources_ResourceId",
                table: "InventoryResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ResourceToDestroyId",
                table: "Items",
                column: "ResourceToDestroyId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ResourceToInteractId",
                table: "Items",
                column: "ResourceToInteractId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ResourceToPlaceId",
                table: "Items",
                column: "ResourceToPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketBuyItems_ItemId",
                table: "MarketBuyItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketSellResources_ResourceToSellId",
                table: "MarketSellResources",
                column: "ResourceToSellId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerQueue_GameTypeId",
                table: "PlayerQueue",
                column: "GameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerQueue_PlayerId",
                table: "PlayerQueue",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameStateId",
                table: "Players",
                column: "GameStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_ResourceToGetResourceId",
                table: "Trades",
                column: "ResourceToGetResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_ResourceToGiveId",
                table: "Trades",
                column: "ResourceToGiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Trades_UserId",
                table: "Trades",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_AchievementId",
                table: "UserAchievements",
                column: "AchievementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievements_UserId",
                table: "UserAchievements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStates_Players_WinnerId",
                table: "GameStates",
                column: "WinnerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStates_Players_WinnerId",
                table: "GameStates");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FarmCells");

            migrationBuilder.DropTable(
                name: "GameStats");

            migrationBuilder.DropTable(
                name: "InventoryResources");

            migrationBuilder.DropTable(
                name: "MarketBuyItems");

            migrationBuilder.DropTable(
                name: "MarketSellResources");

            migrationBuilder.DropTable(
                name: "PlayerQueue");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "UserAchievements");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "GameStates");
        }
    }
}
