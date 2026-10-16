using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Core.Interfaces;

public interface ITaskRepository
{
  Task AddAsync(TaskItem task);
  Task<List<TaskItem>> GetAllAsync(bool? isCompleted = null, bool sortByDueDate = false);
  Task<TaskItem?> GetByIdAsync(int id);
}
