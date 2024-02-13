using API_Nox.Model;
using API_Nox.Repositories.Interfaces;
using API_Nox.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Nox.Controllers
{
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SearchAllTasks()
        {
            return Ok(await _taskRepository.GetAllTasksAsync());
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _taskRepository.GetTaskByIdAsync(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskViewModel taskModel)
        {
            return Ok(await _taskRepository.AddTaskAsync(taskModel));
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskViewModel taskModel, Guid id)
        {
            return Ok(await _taskRepository.UpdateTaskAsync(taskModel, id));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            return Ok(await _taskRepository.DeleteTaskAsync(id));
        }
    }
}
