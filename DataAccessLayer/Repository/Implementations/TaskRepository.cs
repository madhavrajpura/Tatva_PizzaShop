using DataAccessLayer.Models;
using DataAccessLayer.Repository.Interfaces;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repository.Implementations;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDBContext _db;

    public TaskRepository(ApplicationDBContext db)
    {
        _db = db;
    }

    public async Task<IQueryable<TaskItemViewModel>> GetTasksByUserId(int userId)
    {
        IQueryable<TaskItemViewModel> query = _db.TaskItems
            .Where(t => t.UserLoginId == userId && !t.IsDelete)
            .Select(t => new TaskItemViewModel
            {
                Id = t.Id,
                Title = t.Title,
                DueDate = t.DueDate.ToUniversalTime(),
                IsCompleted = t.IsCompleted,
                Description = t.Description,
                PriorityId = t.PriorityId,
                CategoryId = t.CategoryId,
                UserId = t.UserLoginId,
                PriorityName = t.Priority.Name,
                CategoryName = t.Category.Name
            }).AsQueryable().OrderByDescending(t => t.DueDate);
        
        return query;
    }

    public async Task<TaskItemViewModel> GetTaskById(int id, int userId)
    {
        TaskItemViewModel? data = _db.TaskItems.Where(t => t.Id == id && t.UserLoginId == userId && !t.IsDelete)
            .Select(t => new TaskItemViewModel
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate.ToUniversalTime(),
                IsCompleted = t.IsCompleted,
                UserId = t.UserLoginId,
                PriorityId = t.PriorityId,
                CategoryId = t.CategoryId
            }).AsQueryable().FirstOrDefault();
        if (data == null)
        {
            return null!;
            // throw new CustomException($"Task with ID {id} not found for user {userId}.");
        }
        return data;
    }

    public async Task<bool> Save(TaskItemViewModel taskVM)
    {
        if (taskVM.Id == 0)
        {
            TaskItem task = new TaskItem
            {
                Title = taskVM.Title,
                Description = taskVM.Description,
                DueDate = taskVM.DueDate.ToUniversalTime(),
                IsCompleted = taskVM.IsCompleted,
                PriorityId = taskVM.PriorityId,
                CategoryId = taskVM.CategoryId,
                UserLoginId = taskVM.UserId,
                IsDelete = false,
            };
            _db.TaskItems.Add(task);
        }
        else
        {
            TaskItem? ExistTask = _db.TaskItems.FirstOrDefault(x => x.Id == taskVM.Id && !x.IsDelete);

            if (ExistTask == null)
            {
                return false;
            }
            ExistTask.Title = taskVM.Title;
            ExistTask.Description = taskVM.Description;
            ExistTask.DueDate = taskVM.DueDate.ToUniversalTime();
            ExistTask.IsCompleted = taskVM.IsCompleted;
            ExistTask.PriorityId = taskVM.PriorityId;
            ExistTask.CategoryId = taskVM.CategoryId;
            ExistTask.UserLoginId = taskVM.UserId;
            ExistTask.IsDelete = false;
            _db.TaskItems.Update(ExistTask);
        }

        _db.SaveChanges();
        return true;
    }

    public async Task<bool> Delete(int id, int userId)
    {
        TaskItem? task = await _db.TaskItems.FirstOrDefaultAsync(t => t.Id == id && t.UserLoginId == userId && !t.IsDelete);
        if (task != null)
        {
            task.IsDelete = true;
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public (int pending, int completed) GetTaskCounts(int userId)
    {
        int pending = _db.TaskItems.Count(t => t.UserLoginId == userId && !t.IsDelete && !t.IsCompleted);
        int completed = _db.TaskItems.Count(t => t.UserLoginId == userId && !t.IsDelete && t.IsCompleted);
        return (pending, completed);
    }

    public bool CheckTaskExists(TaskItemViewModel taskVM,int userId)
    {
        if (taskVM.Id == 0)
        {
            return _db.TaskItems.Any(x => x.Title.ToLower().Trim() == taskVM.Title.ToLower().Trim() && !x.IsDelete && x.UserLoginId == userId);
        }
        else
        {
            return _db.TaskItems.Any(x => x.Id != taskVM.Id && x.Title.ToLower().Trim() == taskVM.Title.ToLower().Trim() && !x.IsDelete && x.UserLoginId == userId);
        }
    }

    public List<Category> GetCategories()
    {
        return _db.Categories.ToList();
    }

    public List<Priority> GetPriorities()
    {
        return _db.Priorities.ToList();
    }

    public async Task<List<TaskReminderViewModel>> GetTasksDueTomorrow(DateTime tomorrow)
    {
        return await _db.TaskItems
            .Where(t => t.DueDate.Date == tomorrow && !t.IsCompleted && !t.IsDelete)
            .Join(_db.UserLogins,
                task => task.UserLoginId,
                user => user.Id,
                (task, user) => new TaskReminderViewModel
                {
                    TaskTitle = task.Title,
                    DueDate = task.DueDate.ToUniversalTime(),
                    UserEmail = user.Email
                })
            .ToListAsync();
    }

}