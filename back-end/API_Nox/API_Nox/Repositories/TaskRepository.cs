using API_Nox.Data;
using API_Nox.Model;
using API_Nox.Repositories.Interfaces;
using API_Nox.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace API_Nox.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly NoxDBContext _dbContext;

        public TaskRepository(NoxDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<TaskViewModel>> GetAllTasksAsync()
        {
            return _dbContext.Tasks.ToListAsync();
        }

        public async Task<TaskViewModel?> GetTaskByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O ID é inválido ou inexistente", nameof(id));
            }

            return await _dbContext.Tasks.FindAsync(id);
        }

        public async Task<TaskViewModel?> AddTaskAsync(TaskViewModel task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task), " não pode ser nulo");
            }

            task.Id = Guid.NewGuid();
            task.Data = DateTime.UtcNow;

            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<TaskViewModel?> UpdateTaskAsync(TaskViewModel task, Guid? id)
        {
            var existingTask = await _dbContext.Tasks.FindAsync(id);

            if (existingTask != null)
            {
                existingTask.Name = task.Name;
                existingTask.Status = task.Status;
                existingTask.UserId = task.UserId;

                _dbContext.Tasks.Update(existingTask);
                await _dbContext.SaveChangesAsync();
            }

            return existingTask;
        }


        public async Task<bool?> DeleteTaskAsync(Guid id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);

            if (task != null)
            {
                _dbContext.Tasks.RemoveRange(task);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
