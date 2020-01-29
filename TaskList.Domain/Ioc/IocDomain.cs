using Microsoft.Extensions.DependencyInjection;
using TaskList.Domain.Builders;

namespace TaskList.Domain.Ioc
{
    public static class IocDomain
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<ITaskBuilder, TaskBuilder>();
        }
    }
}