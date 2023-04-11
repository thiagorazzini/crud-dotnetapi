using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using taskSystem.Models;
using taskSystem.Repositories.Interfaces;

namespace taskSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepositorie _taskRepositorie;
        public TaskController(ITaskRepositorie taskRepositorie) {
            _taskRepositorie = taskRepositorie;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> SearchAllTask()
        {
            List<TaskModel> task = await _taskRepositorie.SearchAllTasks();
            return Ok(task);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> SearchById(int id)
        {
            TaskModel task = await _taskRepositorie.SearchById(id);
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> Register([FromBody] TaskModel taskModel)
        {
            TaskModel task = await _taskRepositorie.Add(taskModel);

            return Ok(task);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> Update([FromBody] TaskModel taskModel, int id)
        {
            taskModel.Id = id;
            TaskModel task = await _taskRepositorie.Update(taskModel, id);

            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskModel>> Delete(int id)
        {
            bool deleted = await _taskRepositorie.Delete(id);
            return Ok(deleted);
        }


    }
}
