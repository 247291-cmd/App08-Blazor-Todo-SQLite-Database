using App08_TodoDb.Data;
using Microsoft.EntityFrameworkCore;

namespace App08_TodoDb.Services;

public class TodoDbService
{
    private readonly TodoDbContext _db;
    public TodoDbService(TodoDbContext db) => _db = db;

    public Task<List<TodoTask>> GetAllAsync() => _db.Tasks.OrderByDescending(t => t.CreatedAt).ToListAsync();

    public async Task<TodoTask> AddAsync(string title)
    {
        var t = new TodoTask { Title = title.Trim(), CreatedAt = DateTime.UtcNow };
        _db.Tasks.Add(t);
        await _db.SaveChangesAsync();
        return t;
    }

    public async Task ToggleAsync(int id)
    {
        var t = await _db.Tasks.FindAsync(id);
        if (t is not null) { t.IsCompleted = !t.IsCompleted; await _db.SaveChangesAsync(); }
    }

    public async Task UpdateAsync(int id, string title)
    {
        var t = await _db.Tasks.FindAsync(id);
        if (t is not null) { t.Title = title.Trim(); await _db.SaveChangesAsync(); }
    }

    public async Task DeleteAsync(int id)
    {
        var t = await _db.Tasks.FindAsync(id);
        if (t is not null) { _db.Tasks.Remove(t); await _db.SaveChangesAsync(); }
    }

    public async Task ClearAllAsync() { _db.Tasks.RemoveRange(_db.Tasks); await _db.SaveChangesAsync(); }
}
