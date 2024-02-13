using API_Nox.Model;
using API_Nox.ViewModel;

namespace API_Nox.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskViewModel>> GetAllTasksAsync();
        Task<TaskViewModel?> GetTaskByIdAsync(Guid id);
        Task<TaskViewModel?> AddTaskAsync(TaskViewModel task);
        Task<TaskViewModel?> UpdateTaskAsync(TaskViewModel task, Guid? id);
        Task<bool?> DeleteTaskAsync(Guid id);
    }
}
