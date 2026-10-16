using DAL.Data;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Interfaces;
using DAL.Data;

namespace TaskManagementSystem.DAL.Repositories;

public class TaskRepository : ITaskRepository
{
  private readonly AppDbContext _context;

  public TaskRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task AddAsync(TaskItem task)
  {
    await _context.Tasks.AddAsync(task);
    await _context.SaveChangesAsync();
  }

  public async Task<List<TaskItem>> GetAllAsync(bool? isCompleted = null, bool sortByDueDate = false)
  {
    IQueryable<TaskItem> query = _context.Tasks.AsNoTracking();

    if (isCompleted.HasValue)
      query = query.Where(t => t.IsCompleted == isCompleted.Value);

    if (sortByDueDate)
      query = query.OrderBy(t => t.DueDate);

    return await query.ToListAsync();
  }

  public async Task<TaskItem?> GetByIdAsync(int id)
  {
    return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
  }
}
