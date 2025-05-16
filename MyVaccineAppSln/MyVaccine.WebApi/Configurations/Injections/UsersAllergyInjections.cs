using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Repositories.Implementations;
using MyVaccine.WebApi.Services.Contracts;
using MyVaccine.WebApi.Services.Implementations;
using MyVaccine.WebApi.Services;

namespace MyVaccine.WebApi.Configurations.Injections
{
    public static class UsersAllergyInjections
    {
        public static IServiceCollection SetUsersAllergyInjection(this IServiceCollection services)
        {
            #region Repositories Injection
            services.AddScoped<IBaseRepository<UsersAllergy>, BaseRepository<UsersAllergy>>();
            #endregion

            #region Services Injection
            services.AddScoped<IUsersAllergyService, UsersAllergyService>();
            #endregion
            return services;
        }
    }
}
