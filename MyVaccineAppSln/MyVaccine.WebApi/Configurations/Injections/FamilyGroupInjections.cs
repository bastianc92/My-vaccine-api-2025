using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Repositories.Implementations;
using MyVaccine.WebApi.Services.Contracts;
using MyVaccine.WebApi.Services.Implementations;
using MyVaccine.WebApi.Services;

namespace MyVaccine.WebApi.Configurations.Injections
{
    public static class FamilyGroupInjections
    {
        public static IServiceCollection SetFamilyGroupInjection(this IServiceCollection services)
        {
            #region Repositories Injection
            services.AddScoped<IBaseRepository<FamilyGroup>, BaseRepository<FamilyGroup>>();
            #endregion

            #region Services Injection
            services.AddScoped<IFamilyGroupService, FamilyGroupService>();


            #endregion
            return services;
        }
    }
}
