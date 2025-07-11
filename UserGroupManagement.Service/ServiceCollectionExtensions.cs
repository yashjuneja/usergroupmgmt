using Microsoft.Extensions.DependencyInjection;
using UserGroupManagement.Repository.Interfaces;
using UserGroupManagement.Repository.Implementations;
using UserGroupManagement.Service.Interfaces;
using UserGroupManagement.Service.Implementations;

namespace UserGroupManagement.Service
{
    /// <summary>
    /// The class is added to mitigate Api layer directly referencing Repository layer
    /// Services resolution also here to keep the design clean
    /// 
    /// builder.Services.AddUserGroupManagementServices(); in program.cs should resolve this
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUserGroupManagementServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();

            return services;
        }
    }
}
