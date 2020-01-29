using Microsoft.Extensions.DependencyInjection;
using TaskList.Application.Services;
using TaskList.Application.Services.Interfaces;

namespace TaskList.Application.Ioc
{
    public static class IocService
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
        }
    }
}