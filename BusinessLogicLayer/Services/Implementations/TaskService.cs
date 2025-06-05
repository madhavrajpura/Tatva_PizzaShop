using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interfaces;
using DataAccessLayer.ViewModels;

namespace BusinessLogicLayer.Services.Implementations;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {

        _taskRepository = taskRepository;
    }

    public async Task<PaginationViewModel<TaskItemViewModel>> List(int userId, string sortColumn, string sortDirection, int pageNumber, int pageSize, string filter, string fromDate = "", string toDate = "")
    {
        var query = await _taskRepository.GetTasksByUserId(userId);

        if (!string.IsNullOrEmpty(filter))
        {
            if (filter == "Pending")
            {
                query = query.Where(t => !t.IsCompleted);
            }
            else if (filter == "Completed")
            {
                query = query.Where(t => t.IsCompleted);
            }
        }

        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("UTC");

        if (!string.IsNullOrEmpty(fromDate) && DateTime.TryParse(fromDate, out DateTime from))
        {
            from = TimeZoneInfo.ConvertTimeToUtc(from, timeZone);
            query = query.Where(t => t.DueDate >= from);
        }

        if (!string.IsNullOrEmpty(toDate) && DateTime.TryParse(toDate, out DateTime to))
        {
            to = TimeZoneInfo.ConvertTimeToUtc(to, timeZone);
            query = query.Where(t => t.DueDate <= to);
        }


        int totalCount = query.Count();

        switch (sortColumn)
        {
            case "Name":
                query = sortDirection == "asc" ? query.OrderBy(t => t.Title) : query.OrderByDescending(t => t.Title);
                break;
            case "Date":
                query = sortDirection == "asc" ? query.OrderBy(t => t.DueDate) : query.OrderByDescending(t => t.DueDate);
                break;
            case "Status":
                query = sortDirection == "asc" ? query.OrderBy(t => t.IsCompleted) : query.OrderByDescending(t => t.IsCompleted);
                break;
        }

        List<TaskItemViewModel>? items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new PaginationViewModel<TaskItemViewModel>(items, totalCount, pageNumber, pageSize);
    }

    public async Task<TaskItemViewModel> GetTaskById(int id, int userId)
    {
        var taskVM = await _taskRepository.GetTaskById(id, userId);
        if (taskVM == null)
        {
            // throw new CustomException("Task not found.");
            return null;
        }
        return taskVM;
    }

    public async Task<bool> Save(TaskItemViewModel taskVM)
    {
        if (await _taskRepository.Save(taskVM))
        {
            return true;
        }
        return false;
    }


    public async Task<bool> Delete(int id, int userId)
    {
        if (await _taskRepository.Delete(id, userId))
        {
            return true;
        }
        return false;
    }

    public (int pending, int completed) GetTaskCounts(int userId)
    {
        return _taskRepository.GetTaskCounts(userId);
    }

    public bool CheckTaskExists(TaskItemViewModel taskVM, int userId)
    {
        return _taskRepository.CheckTaskExists(taskVM, userId);
    }

    public List<Category> GetCategories()
    {
        return _taskRepository.GetCategories();
    }
    public List<Priority> GetPriorities()
    {
        return _taskRepository.GetPriorities();
    }

     public async Task<List<TaskReminderViewModel>> GetTasksDueTomorrow()
    {
        var tomorrow = DateTime.Today.AddDays(1);
        return await _taskRepository.GetTasksDueTomorrow(tomorrow);
    }


}