using DataTransferObjects;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskList.Application.Services;
using TaskList.Application.Services.Interfaces;
using TaskList.Application.Services.Validators;

namespace TaskList.Application.Ioc
{
    public static class IocService
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IValidator<TaskDto>, TaskValidator>();
        }
    }
}