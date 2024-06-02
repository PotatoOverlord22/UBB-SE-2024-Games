using Microsoft.EntityFrameworkCore;
using Server.API.Models;
using Server.API.Services;
using Server.API.Utils;

public class TableRepository : ITableRepository
{
    private readonly GamesContext context;

    public TableRepository(GamesContext context)
    {
        this.context = context;
    }

    public async Task<List<Table>> GetTableAsync()
    {
        return await context.Tables.ToListAsync();
    }

    public async Task<Table> GetTableByIdAsync(Guid id)
    {
        var table = await context.Tables.FindAsync(id);

        if (table == null)
        {
            throw new KeyNotFoundException("Table not found");
        }

        return table;
    }
    public async Task AddTableAsync(Table table)
    {
        context.Tables.Add(table);
        await context.SaveChangesAsync();
    }
    public async Task DeleteTableAsync(Guid id)
    {
        var table = context.Tables.Find(id);
        if (table == null)
        {
            throw new KeyNotFoundException("Table not found");
        }
        context.Tables.Remove(table);
        await context.SaveChangesAsync();
    }
    public async Task UpdateTableAsync(Guid id, Table table)
    {
        if (context.Tables.Find(id) == null)
        {
            throw new KeyNotFoundException("Table not found");
        }
        context.Tables.Update(table);
        await context.SaveChangesAsync();
    }
}
