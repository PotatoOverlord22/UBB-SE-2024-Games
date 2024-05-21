using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using GameWorld.Entities;

public class DatabaseContext : DbContext
{
    // Used by both games
    public DbSet<User> Users { get; set; }

    // Used by HarvestHaven
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<FarmCell> FarmCells { get; set; }
    public DbSet<InventoryResource> InventoryResources { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<MarketBuyItem> MarketBuyItems { get; set; }
    public DbSet<MarketSellResource> MarketSellResources { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Trade> Trades { get; set; }
    public DbSet<UserAchievement> UserAchievements { get; set; }

    // Used by SuperbetBeclean
    public DbSet<PlayingCard> PlayingCards { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<CardDeck> CardDecks { get; set; }
    public DbSet<Font> Fonts { get; set; }
    public DbSet<Icon> Icons { get; set; }
    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<ShopItem> ShopItems { get; set; }
    public DbSet<Table> Tables { get; set; }

    // What is below this comment is straight from the documentation.
    public string DbPath { get; }

    public DatabaseContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
