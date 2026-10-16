using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.BLL.Interfaces;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Interfaces;

namespace TaskManagementSystem.BLL.Services;

public class TaskService : ITaskService
{
  private readonly ITaskRepository _repo;

  public TaskService(ITaskRepository repo)
  {
    _repo = repo;
  }

  public async Task CreateAsync(TaskItem task)
  {
    if (task.DueDate.HasValue && task.DueDate.Value <= DateTime.Now)
      throw new ValidationException("Due date must be in the future.");

    task.CreatedAt = DateTime.Now;
    await _repo.AddAsync(task);
  }

  public Task<List<TaskItem>> GetAllAsync(bool? isCompleted = null, bool sortByDueDate = false)
  {
    return _repo.GetAllAsync(isCompleted, sortByDueDate);
  }

  public Task<TaskItem?> GetByIdAsync(int id)
  {
    return _repo.GetByIdAsync(id);
  }
}
