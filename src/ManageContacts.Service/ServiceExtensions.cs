using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using ManageContacts.Service.CacheServices.RoleCaches;
using ManageContacts.Service.Services.Contacts;
using ManageContacts.Service.Services.Groups;
using ManageContacts.Service.Services.PhoneTypes;
using ManageContacts.Service.Services.Roles;
using ManageContacts.Service.Services.UploadFiles;
using ManageContacts.Service.Services.Users;
using ManageContacts.Shared.Consts;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace ManageContacts.Service;

public static class ServiceExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {

        services.AddHttpContextAccessor();

        services.AddFluentValidation(options =>
        {
            options.DisableDataAnnotationsValidation = true;

            options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
                
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
        });;
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<IMemoryCache, MemoryCache>();

        services.AddScoped<IUploadFileService, UploadFileService>();
        
        services.AddScoped<IRoleCacheService, RoleCacheService>();
        
        services.AddScoped<IUserService, UserService>();
        
        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<IPhoneTypeService, PhoneTypeService>();
        
        services.AddScoped<IGroupService, GroupService>();
        
        services.AddScoped<IContactService, ContactService>();

        return services;
    }
}