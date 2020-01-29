using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskList.Application.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetAllAsync(CancellationToken cancellationToken);

        Task<TaskDto> GetAsync(Guid taskId, CancellationToken cancellationToken);

        Task<TaskDto> UpdateAsync(TaskDto taskDto, CancellationToken cancellationToken);

        Task<TaskDto> AddAsync(TaskDto taskDto, CancellationToken cancellationToken);

        Task<TaskDto> DeleteAsync(Guid taskId, CancellationToken cancellationToken);

        Task<TaskDto> UpdateStatusAsync(Guid taskId, bool status, CancellationToken cancellationToken);
    }
}