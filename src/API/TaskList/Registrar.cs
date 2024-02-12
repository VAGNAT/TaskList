using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskList.Application.Mapping;
using TaskList.Application.Repositories.Abstractions;
using TaskList.Application.Services.Comment.CommandHandlers;
using TaskList.Application.Services.Comment.Commands;
using TaskList.Application.Services.Comment.Queries;
using TaskList.Application.Services.Comment.QueriesHandlers;
using TaskList.Application.Services.Status.CommandHandlers;
using TaskList.Application.Services.Status.Commands;
using TaskList.Application.Services.Status.Queries;
using TaskList.Application.Services.Status.QueriesHandlers;
using TaskList.Application.Services.Task.CommandHandlers;
using TaskList.Application.Services.Task.Commands;
using TaskList.Application.Services.Task.Queries;
using TaskList.Application.Services.Task.QueriesHandlers;
using TaskList.Application.Services.TaskList.CommandHandlers;
using TaskList.Application.Services.TaskList.Commands;
using TaskList.Application.Services.TaskList.Queries;
using TaskList.Application.Services.TaskList.QueriesHandlers;
using TaskList.Domain.EntitiesDto;
using TaskList.Infrastructure;
using TaskList.Infrastructure.Repositories.Implementation;
using TaskList.Mapping;

namespace TaskList
{
    internal static class Registrar
    {
        internal static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSingleton((IConfigurationRoot)configuration)
                .AddAuth(configuration)
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
                .AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()))
                .AddInfrastructureServices(configuration)
                .InstallHandlers()
                .InstallRepositories();
        }

        private static IServiceCollection AddAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {

                    var secret = Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!);
                    var securityKey = new SymmetricSecurityKey(secret);

                    var issuer = configuration["Jwt:ValidIssuer"];
                    var audience = configuration["Jwt:ValidAudience"];


                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = securityKey,
                        ClockSkew = TimeSpan.Zero,
                    };
                });
            const string authPolicyName = "worktitle-auth-policy";
            serviceCollection.AddAuthorization(authorizationOptions =>
            {
                authorizationOptions.AddPolicy(authPolicyName, policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });

            serviceCollection.AddControllers(mvcOptions =>
            {
                mvcOptions.Filters.Add(new AuthorizeFilter(authPolicyName));
            });
            return serviceCollection;
        }

        private static IServiceCollection InstallHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection
            //TaskList
                .AddTransient<IRequestHandler<GetTaskListsQueryAsync, IEnumerable<TaskListDto>>, GetTasksListsHandler>()
                .AddTransient<IRequestHandler<GetTaskListByIdQueryAsync, TaskListDto>, GetTaskListByIdHandler>()
                .AddTransient<IRequestHandler<AddTaskListCommandAsync, Guid>, AddTaskListHandler>()
                .AddTransient<IRequestHandler<UpdateTaskListCommandAsync>, UpdateTaskListHandler>()
                .AddTransient<IRequestHandler<DeleteTaskListCommandAsync>, DeleteTaskListHandler>()
            //Task
                .AddTransient<IRequestHandler<GetTasksByListIdQueryAsync, IEnumerable<TaskDto>>, GetTasksByListIdHandler>()
                .AddTransient<IRequestHandler<GetTaskByIdQueryAsync, TaskDto>, GetTaskByIdHandler>()
                .AddTransient<IRequestHandler<MoveTaskCommandAsync>, MoveTaskHandler>()
                .AddTransient<IRequestHandler<AddTaskCommandAsync, Guid>, AddTaskHandler>()
                .AddTransient<IRequestHandler<UpdateTaskCommandAsync>, UpdateTaskHandler>()
                .AddTransient<IRequestHandler<DeleteTaskCommandAsync>, DeleteTaskHandler>()
            //Comment
                .AddTransient<IRequestHandler<GetCommentsByTaskIdQueryAsync, IEnumerable<CommentDto>>, GetCommentsByTaskIdHandler>()
                .AddTransient<IRequestHandler<GetCommentByIdQueryAsync, CommentDto>, GetCommentByIdHandler>()
                .AddTransient<IRequestHandler<AddCommentCommandAsync, Guid>, AddCommentHandler>()
                .AddTransient<IRequestHandler<UpdateCommentCommandAsync>, UpdateCommentHandler>()
                .AddTransient<IRequestHandler<DeleteCommentCommandAsync>, DeleteCommentHandler>()
            //Status
                .AddTransient<IRequestHandler<GetTaskStatusByIdQueryAsync, StatusTaskHistoryDto>, GetTaskStatusByIdHandler>()
                .AddTransient<IRequestHandler<ChangeStatusCommandAsync, StatusTaskHistoryDto>, ChangeStatusHandler>()            
               ;
            return serviceCollection;
        }

        private static IServiceCollection InstallRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<ICommentRepository, CommentRepository>()
                .AddTransient<IStatusTaskHistoryRepository, StatusTaskHistoryRepository>()
                .AddTransient<ITaskRepository, TaskRepository>()
                .AddTransient<ITaskListRepository, TaskListRepository>();
            ;
            return serviceCollection;
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TaskListUiProfile>();
                cfg.AddProfile<TaskListProfile>();
                cfg.AddProfile<TaskUiProfile>();
                cfg.AddProfile<TaskProfile>();
                cfg.AddProfile<CommentUiProfile>();
                cfg.AddProfile<CommentProfile>();
                cfg.AddProfile<StatusProfile>();
                cfg.AddProfile<StatusUiProfile>();
            });
            configuration.AssertConfigurationIsValid();

            return configuration;
        }
    }
}
