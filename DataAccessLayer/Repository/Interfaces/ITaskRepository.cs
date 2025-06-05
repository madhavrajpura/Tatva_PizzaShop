using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;

namespace DataAccessLayer.Repository.Interfaces;

public interface ITaskRepository
{
    Task<IQueryable<TaskItemViewModel>> GetTasksByUserId(int userId);
    Task<TaskItemViewModel> GetTaskById(int id, int userId);
    Task<bool> Save(TaskItemViewModel task);
    Task<bool> Delete(int id, int userId);
    (int pending, int completed) GetTaskCounts(int userId);
    bool CheckTaskExists(TaskItemViewModel taskVM, int userId);
    List<Category> GetCategories();
    List<Priority> GetPriorities();
    Task<List<TaskReminderViewModel>> GetTasksDueTomorrow(DateTime tomorrow);
}
