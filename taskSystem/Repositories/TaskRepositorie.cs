using Microsoft.EntityFrameworkCore;
using taskSystem.Context;
using taskSystem.Models;
using taskSystem.Repositories.Interfaces;

namespace taskSystem.Repositories
{
    public class TaskRepositorie : ITaskRepositorie
    {
        private readonly SystemTasksDBContext _dbcontext;

        public TaskRepositorie(SystemTasksDBContext systemTasksDBContext)
        {
            _dbcontext = systemTasksDBContext;
        }
        public async Task<TaskModel> SearchById(int id)
        {
            return await _dbcontext.Tasks
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<TaskModel>> SearchAllTasks()
        {
            return await _dbcontext.Tasks
                .Include(x => x.User)
                .ToListAsync();

        }
        public async Task<TaskModel> Add(TaskModel task)
        {
            await _dbcontext.Tasks.AddAsync(task);
            await _dbcontext.SaveChangesAsync();

            return task;
        }
        public async Task<TaskModel> Update(TaskModel task, int id)
        {
            TaskModel taskById =  await SearchById(id);
            
            if (taskById == null)
            {
                throw new Exception($"Task for ID: {id} Was not found ind Database");
            }

            taskById.Name = task.Name;
            taskById.Description = task.Description;
            taskById.Status = task.Status;
            taskById.UserId = task.UserId;

            _dbcontext.Tasks.Update(taskById);
            await _dbcontext.SaveChangesAsync();

            return taskById;
        }

        public async Task<bool> Delete(int id)
        {
            TaskModel TaskById = await SearchById(id);

            if (TaskById == null)
            {
                throw new Exception($"Task for ID: {id} Was not found in Database");
            }

            _dbcontext.Tasks.Remove(TaskById);
            await _dbcontext.SaveChangesAsync();

            return true;

        }



    }
}
