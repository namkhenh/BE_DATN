using Common.Contract.Common.Interfaces;
using Common.Contract.ScheduledJobs;
using Common.Infrastructure.Common.Implements;
using Common.Infrastructure.ScheduledJobs;
using Microsoft.Extensions.ObjectPool;
using Portal.API.AutoMapper;
using Portal.Application.Interfaces.Action;
using Portal.Application.Interfaces.ActionPage;
using Portal.Application.Interfaces.Menu;
using Portal.Application.Interfaces.Outh2;
using Portal.Application.Interfaces.Page;
using Portal.Application.Interfaces.Permission;
using Portal.Application.Interfaces.PermissionAction;
using Portal.Application.Interfaces.Role;

using Portal.Application.Interfaces.User;
using Portal.Application.Repositories.Interfaces.Action;
using Portal.Application.Repositories.Interfaces.ActionPage;
using Portal.Application.Repositories.Interfaces.Menu;
using Portal.Application.Repositories.Interfaces.Page;
using Portal.Application.Repositories.Interfaces.Permission;
using Portal.Application.Repositories.Interfaces.PermissionAction;
using Portal.Application.Repositories.Interfaces.RefeshToken;
using Portal.Infrastructure.Implements.Action;
using Portal.Infrastructure.Implements.ActionPage;
using Portal.Infrastructure.Implements.Menu;
using Portal.Infrastructure.Implements.Outh2;
using Portal.Infrastructure.Implements.Page;
using Portal.Infrastructure.Implements.Permission;
using Portal.Infrastructure.Implements.PermissionAction;
using Portal.Infrastructure.Implements.Role;

using Portal.Infrastructure.Implements.User;
using Portal.Infrastructure.Repositories.Implements.Action;
using Portal.Infrastructure.Repositories.Implements.ActionPage;
using Portal.Infrastructure.Repositories.Implements.Menu;
using Portal.Infrastructure.Repositories.Implements.Page;
using Portal.Infrastructure.Repositories.Implements.Permission;
using Portal.Infrastructure.Repositories.Implements.PermissionAction;
using Portal.Infrastructure.Repositories.Implements.RefeshToken;
using RabbitMQ.Client;

namespace Portal.API.Extenstions
{
  public static class ServiceExtenstion
  {
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
      services.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>))
             .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
             .AddScoped(typeof(IActionRepository), typeof(ActionRepository))
             .AddScoped(typeof(IActionPageRepository), typeof(ActionPageRepository))
              .AddScoped(typeof(IMenuRepository), typeof(MenuRepository))
              .AddScoped(typeof(IPageRepository), typeof(PageRepository))
              .AddScoped(typeof(IPermissionRepository), typeof(PermissionRepository))
              .AddScoped(typeof(IPermissionActionRepository), typeof(PermissionActionRepository))
              .AddScoped(typeof(IRefeshTokenRepository), typeof(RefeshTokenRepository));

      services.AddScoped<IActionService, ActionService>();
      services.AddScoped<IActionPageService, ActionPageService>();
      services.AddScoped<IMenuService, MenuService>();
      services.AddScoped<IOuth2Service, Outh2Service>();
      services.AddScoped<IPageService, PageService>();
      services.AddScoped<IPermissionService, PermissionService>();
      services.AddScoped<IPermissionActionService, PermissionActionService>();
      services.AddScoped<IRoleService, RoleService>();
     
      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IOuth2Service, Outh2Service>();
      return services;
    }
    public static IServiceCollection ConfigRabbitMQ(this IServiceCollection services)
    {
     
      return services;
    }

    public static IServiceCollection ConfigAutoMapper(this IServiceCollection services)
    {
      services.AddAutoMapper(typeof(AutoMapping));
      return services;
    }
    public static IServiceCollection ConfigCORS(this IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
            builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
      });
      return services;
    }
  }
}
