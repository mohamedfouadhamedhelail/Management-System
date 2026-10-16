using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.BLL.Interfaces;

public interface ITaskService
{
  Task CreateAsync(TaskItem task);
  Task<List<TaskItem>> GetAllAsync(bool? isCompleted = null, bool sortByDueDate = false);
  Task<TaskItem?> GetByIdAsync(int id);
}
