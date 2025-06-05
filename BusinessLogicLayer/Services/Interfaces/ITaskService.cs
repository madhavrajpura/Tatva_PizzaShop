using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;

namespace BusinessLogicLayer.Services.Interfaces;

public interface ITaskService
{
    Task<PaginationViewModel<TaskItemViewModel>> List(int userId, string sortColumn = "", string sortDirection = "", int pageNumber = 1, int pageSize = 3, string filter = "", string fromDate = "", string toDate = "");
    Task<TaskItemViewModel> GetTaskById(int Id, int userId);
    // Task<bool> CreateTask(TaskItemViewModel taskItemVM, int userId);
    // Task<bool> UpdateTask(TaskItemViewModel taskItemVM, int userId);
    Task<bool> Save(TaskItemViewModel taskItemVM);
    Task<bool> Delete(int Id, int userId);
    public (int pending, int completed) GetTaskCounts(int userId);
    public bool CheckTaskExists(TaskItemViewModel taskVM, int userId);
    List<Category> GetCategories();
    List<Priority> GetPriorities();

    Task<List<TaskReminderViewModel>> GetTasksDueTomorrow(); // New method

}
