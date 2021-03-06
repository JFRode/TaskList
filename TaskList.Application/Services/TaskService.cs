﻿using AutoMapper;
using DataTransferObjects;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskList.Application.Services.Interfaces;
using TaskList.Data.Contexts;
using TaskList.Domain.Builders;

namespace TaskList.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly TasklistDbContext _tasklistDbContext;
        private readonly IMapper _mapper;
        private readonly ITaskBuilder _taskBuilder;
        private readonly IValidator<TaskDto> _validator;

        public TaskService(TasklistDbContext tasklistDbContext, IMapper mapper, ITaskBuilder taskBuilder, IValidator<TaskDto> validator)
        {
            _tasklistDbContext = tasklistDbContext;
            _mapper = mapper;
            _taskBuilder = taskBuilder;
            _validator = validator;
        }

        public async Task<TaskDto> AddAsync(TaskDto taskDto, CancellationToken cancellationToken)
        {
            var validacao = _validator.Validate(taskDto);

            if (!validacao.IsValid)
                throw new Exception(validacao.Errors.First().ErrorMessage);

            var task = _mapper.Map<Domain.Task>(taskDto);

            await Task.Run(() =>
            {
                _tasklistDbContext.Tasks.AddAsync(task);
                _tasklistDbContext.SaveChangesAsync();
            }, cancellationToken);

            return taskDto;
        }

        public async Task<TaskDto> DeleteAsync(Guid taskId, CancellationToken cancellationToken)
        {
            var task = _taskBuilder.Build();

            await Task.Run(() =>
            {
                task = _tasklistDbContext.Tasks
                    .Where(t => t.Id == taskId)
                    .FirstOrDefault();

                if (task == null)
                    throw new Exception();

                task.Excluido = true;

                _tasklistDbContext.Tasks.Update(task);
                _tasklistDbContext.SaveChangesAsync();
            }, cancellationToken);

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<List<TaskDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var tasks = await Task.Run(() => _tasklistDbContext.Tasks.AsNoTracking(), cancellationToken);
            return _mapper.Map<List<TaskDto>>(tasks);
        }

        public async Task<TaskDto> GetAsync(Guid taskId, CancellationToken cancellationToken)
        {
            var task = await Task.Run(() => _tasklistDbContext.Tasks
                .AsNoTracking()
                .Where(t => t.Id == taskId)
                .FirstOrDefault(), cancellationToken);

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> UpdateAsync(TaskDto taskDto, CancellationToken cancellationToken)
        {
            var validacao = _validator.Validate(taskDto);

            if (!validacao.IsValid)
                throw new Exception(validacao.Errors.First().ErrorMessage);

            var task = _mapper.Map<Domain.Task>(taskDto);

            await Task.Run(() =>
            {
                _tasklistDbContext.Tasks.Update(task);
                _tasklistDbContext.SaveChangesAsync();
            }, cancellationToken);

            return taskDto;
        }

        public async Task<TaskDto> UpdateStatusAsync(Guid taskId, bool status, CancellationToken cancellationToken)
        {
            var task = _taskBuilder.Build();

            await Task.Run(() =>
            {
                task = _tasklistDbContext.Tasks
                    .Where(t => t.Id == taskId)
                    .FirstOrDefault();

                task.Status = status;

                _tasklistDbContext.Tasks.Update(task);
                _tasklistDbContext.SaveChangesAsync();
            }, cancellationToken);

            return _mapper.Map<TaskDto>(task);
        }
    }
}