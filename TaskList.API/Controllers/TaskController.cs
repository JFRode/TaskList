using DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskList.Application.Services.Interfaces;

namespace TaskList.API.Controllers
{
    [Route("api/tasks")]
    [Authorize()]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await _taskService.GetAllAsync(cancellationToken);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetById(Guid taskId, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskService.GetAsync(taskId, cancellationToken);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskDto taskDto, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskService.AddAsync(taskDto, cancellationToken);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromBody] TaskDto taskDto, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskService.UpdateAsync(taskDto, cancellationToken);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> Delete(Guid taskId, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskService.DeleteAsync(taskId, cancellationToken);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }

        [HttpPut("{taskId}/{status}")]
        public async Task<IActionResult> UpdateStatus(Guid taskId, bool status, CancellationToken cancellationToken)
        {
            try
            {
                var task = await _taskService.UpdateStatusAsync(taskId, status, cancellationToken);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }
    }
}