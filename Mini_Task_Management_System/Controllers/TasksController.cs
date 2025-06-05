using BusinessLogicLayer.Helper;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mini_Task_Management_System.Controllers;

public class TasksController : Controller
{
    private readonly ITaskService _taskService;
    private readonly IJWTService _JWTService;

    public TasksController(ITaskService taskService, IJWTService JWTService)
    {
        _taskService = taskService;
        _JWTService = JWTService;
    }

    public IActionResult Tasks()
    {
        string? token = Request.Cookies["JWTToken"];
        string? userId = _JWTService.GetClaimValue(token, "userId");

        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
        {
            TempData["ErrorMessage"] = NotificationMessage.TokenExpired;
            return RedirectToAction("Index", "Account");
        }

        return View();
    }

    public IActionResult GetDashboardData()
    {
        string? token = Request.Cookies["JWTToken"];
        string? userId = _JWTService.GetClaimValue(token, "userId");
        var (pending, completed) = _taskService.GetTaskCounts(userId: int.Parse(userId));
        TaskItemViewModel taskVM = new TaskItemViewModel();
        taskVM.PendingCount = pending;
        taskVM.CompletedCount = completed;
        return PartialView("_DashboardDataPartial", taskVM);
    }

    public async Task<IActionResult> List(string sortColumn = "", string sortDirection = "", int pageNumber = 1, int pageSize = 3, string filter = "", string fromDate = "", string toDate = "")
    {
        string? token = Request.Cookies["JWTToken"];
        string? userId = _JWTService.GetClaimValue(token, "userId");
        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
        {
            TempData["ErrorMessage"] = NotificationMessage.TokenExpired;
            return RedirectToAction("Index", "Account");
        }

        PaginationViewModel<TaskItemViewModel>? tasks = await _taskService.List(userId: int.Parse(userId), sortColumn, sortDirection, pageNumber, pageSize, filter, fromDate, toDate);
        return PartialView("_TaskListPartial", tasks);
    }

    [HttpGet]
    public async Task<IActionResult> Save(int id)
    {
        string? token = Request.Cookies["JWTToken"];
        string? userId = _JWTService.GetClaimValue(token, "userId");

        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
        {
            TempData["ErrorMessage"] = NotificationMessage.TokenExpired;
            return RedirectToAction("Index", "Account");
        }
        TaskItemViewModel taskVM = new TaskItemViewModel();
        if (id == 0)
        {
            taskVM = new TaskItemViewModel();

        }
        else
        {
            taskVM = await _taskService.GetTaskById(id, userId: int.Parse(userId));
        }


        ViewBag.CategoryList = new SelectList(_taskService.GetCategories(), "Id", "Name", taskVM.CategoryId);
        ViewBag.PriorityList = new SelectList(_taskService.GetPriorities(), "Id", "Name", taskVM.PriorityId);


        return PartialView("_SaveTaskPartial", taskVM);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromForm] TaskItemViewModel taskVM)
    {
        string? token = Request.Cookies["JWTToken"];
        string? userId = _JWTService.GetClaimValue(token!, "userId");
        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
        {
            TempData["ErrorMessage"] = NotificationMessage.TokenExpired;
            return RedirectToAction("Index", "Account");
        }

        taskVM.UserId = int.Parse(userId);

        if (_taskService.CheckTaskExists(taskVM, userId: int.Parse(userId)))
        {
            return Json(new { success = false, text = NotificationMessage.TaskExists });
        }

        bool taskStatus = await _taskService.Save(taskVM);
        return Json(taskStatus
            ? new { success = true, text = taskVM.Id == 0 ? NotificationMessage.TaskCreated : NotificationMessage.TaskUpdated }
            : new { success = false, text = taskVM.Id == 0 ? NotificationMessage.TaskCreationFailed : NotificationMessage.TaskUpdateFailed });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        string? token = Request.Cookies["JWTToken"];
        string? userId = _JWTService.GetClaimValue(token, "userId");
        if (userId == null)
        {
            TempData["ErrorMessage"] = NotificationMessage.TokenExpired;
            return RedirectToAction("Index", "Account");
        }

        bool data = await _taskService.Delete(id, userId: int.Parse(userId));

        if (data)
        {
            return Json(new { success = true, text = NotificationMessage.TaskDeleted });
        }
        return Json(new { success = false, text = NotificationMessage.TaskDeletionFailed });
    }
}