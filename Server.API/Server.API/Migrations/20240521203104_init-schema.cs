using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.API.Migrations
{
    /// <inheritdoc />
    public partial class Initschema : Migration
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
                name: "Challenges",
                columns: table => new
                {
                    ChallengeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChallengeDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChallengeRule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChallengeAmount = table.Column<int>(type: "int", nullable: false),
                    ChallengeReward = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.ChallengeId);
                });

            migrationBuilder.CreateTable(
                name: "Fonts",
                columns: table => new
                {
                    FontID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FontName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FontPrice = table.Column<int>(type: "int", nullable: false),
                    FontType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fonts", x => x.FontID);
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    IconID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IconName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconPrice = table.Column<int>(type: "int", nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.IconID);
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
                name: "Tables",
                columns: table => new
                {
                    TableID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableBuyIn = table.Column<int>(type: "int", nullable: false),
                    TablePlayerLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableID);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    TitleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TitlePrice = table.Column<int>(type: "int", nullable: false),
                    TitleContent = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.TitleID);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    ResourceToPlaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResourceToInteractId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ResourceToDestroyIdId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Resources_ResourceToDestroyIdId",
                        column: x => x.ResourceToDestroyIdId,
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
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coins = table.Column<int>(type: "int", nullable: false),
                    AmountOfItemsBought = table.Column<int>(type: "int", nullable: false),
                    AmountOfTradesPerformed = table.Column<int>(type: "int", nullable: false),
                    TradeHallUnlockTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastTimeReceivedWater = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserCurrentFontFontID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCurrentTitleTitleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCurrentIconPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCurrentTableTableID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserChips = table.Column<int>(type: "int", nullable: false),
                    UserStack = table.Column<int>(type: "int", nullable: false),
                    UserStreak = table.Column<int>(type: "int", nullable: false),
                    UserHandsPlayed = table.Column<int>(type: "int", nullable: false),
                    UserLevel = table.Column<int>(type: "int", nullable: false),
                    UserStatus = table.Column<int>(type: "int", nullable: false),
                    UserBet = table.Column<int>(type: "int", nullable: false),
                    UserTablePlace = table.Column<int>(type: "int", nullable: false),
                    UserLastLogin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Fonts_UserCurrentFontFontID",
                        column: x => x.UserCurrentFontFontID,
                        principalTable: "Fonts",
                        principalColumn: "FontID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Tables_UserCurrentTableTableID",
                        column: x => x.UserCurrentTableTableID,
                        principalTable: "Tables",
                        principalColumn: "TableID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Titles_UserCurrentTitleTitleID",
                        column: x => x.UserCurrentTitleTitleID,
                        principalTable: "Titles",
                        principalColumn: "TitleID",
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
                        name: "FK_Comments_User_PosterId",
                        column: x => x.PosterId,
                        principalTable: "User",
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
                        name: "FK_FarmCells_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
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
                        name: "FK_InventoryResources_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayingCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayingCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayingCards_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShopItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopItems_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
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
                        name: "FK_Trades_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
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
                name: "IX_InventoryResources_OwnerId",
                table: "InventoryResources",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryResources_ResourceId",
                table: "InventoryResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ResourceToDestroyIdId",
                table: "Items",
                column: "ResourceToDestroyIdId");

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
                name: "IX_PlayingCards_UserId",
                table: "PlayingCards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItems_UserId",
                table: "ShopItems",
                column: "UserId");

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
                name: "IX_User_UserCurrentFontFontID",
                table: "User",
                column: "UserCurrentFontFontID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserCurrentTableTableID",
                table: "User",
                column: "UserCurrentTableTableID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserCurrentTitleTitleID",
                table: "User",
                column: "UserCurrentTitleTitleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FarmCells");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropTable(
                name: "InventoryResources");

            migrationBuilder.DropTable(
                name: "MarketBuyItems");

            migrationBuilder.DropTable(
                name: "MarketSellResources");

            migrationBuilder.DropTable(
                name: "PlayingCards");

            migrationBuilder.DropTable(
                name: "ShopItems");

            migrationBuilder.DropTable(
                name: "Trades");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Fonts");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Titles");
        }
    }
}
