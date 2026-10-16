using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.BLL.Interfaces;
using TaskManagementSystem.Core.Entities;

namespace TaskManagementSystem.Controllers;

public class TasksController : Controller
{
  private readonly ITaskService _service;

  public TasksController(ITaskService service)
  {
    _service = service;
  }

  public async Task<IActionResult> Index(bool? isCompleted, bool sortByDueDate = false)
  {
    ViewBag.IsCompleted = isCompleted;
    ViewBag.SortByDueDate = sortByDueDate;
    var tasks = await _service.GetAllAsync(isCompleted, sortByDueDate);
    return View(tasks);
  }

  public async Task<IActionResult> Details(int id)
  {
    var task = await _service.GetByIdAsync(id);
    if (task is null) return NotFound();
    return View(task);
  }

  public IActionResult Create()
  {
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Create([Bind("Title,Description,IsCompleted,DueDate")] TaskItem task)
  {
    if (!ModelState.IsValid) return View(task);

    try
    {
      await _service.CreateAsync(task);
      return RedirectToAction(nameof(Index));
    }
    catch (ValidationException ex)
    {
      ModelState.AddModelError(nameof(TaskItem.DueDate), ex.Message);
      return View(task);
    }
  }
}
