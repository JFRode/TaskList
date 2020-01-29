using FluentValidation.AspNetCore;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using TaskList.Application.Ioc;
using TaskList.Application.Mappers;
using TaskList.Data.Contexts;
using TaskList.Domain.Ioc;

namespace TaskList.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TasklistDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddOData();
            services.AddControllers(mvcOptions =>
                mvcOptions.EnableEndpointRouting = false);

            services.AddMvc().AddFluentValidation();

            IocService.Register(services);
            IocDomain.Register(services);

            ConfigureMappingService(services);
            ConfigureAuthenticationService(services);
            ConfigureSwaggerService(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureMigration(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasklist.API v1");
            });

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Expand().Select().Count().OrderBy();
            });
        }

        private void ConfigureMigration(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TasklistDbContext>();
                context.Database.Migrate();
            }
        }

        private void ConfigureMappingService(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToDomainProfile());
                cfg.AddProfile(new DomainToDtoProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void ConfigureAuthenticationService(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "TasklistAPI",
                        ValidAudience = "TasklistAPI",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Token inválido: {context.Exception.Message}");
                            return System.Threading.Tasks.Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine($"Token Válido: {context.SecurityToken}");
                            return System.Threading.Tasks.Task.CompletedTask;
                        }
                    };
                });
        }

        private void ConfigureSwaggerService(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Tasklist.API",
                    Version = "v1",
                    Description = "API de comunicação com lista de Tarefas",
                    TermsOfService = new Uri("https://github.com/JFRode/TaskList/blob/master/LICENSE"),
                    Contact = new OpenApiContact
                    {
                        Name = "João Felipe Gonçalves",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/jfrode"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://github.com/JFRode/TaskList/blob/master/LICENSE"),
                    }
                });
            });
        }
    }
}