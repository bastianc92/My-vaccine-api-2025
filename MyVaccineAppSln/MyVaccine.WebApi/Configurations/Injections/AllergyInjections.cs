using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Repositories.Implementations;
using MyVaccine.WebApi.Services.Contracts;
using MyVaccine.WebApi.Services.Implementations;
using MyVaccine.WebApi.Services;

namespace MyVaccine.WebApi.Configurations.Injections
{
    public static class AllergyInjections
    {
        public static IServiceCollection SetAllergyInjection(this IServiceCollection services)
        {
            #region Repositories Injection

            services.AddScoped<IBaseRepository<Allergy>, BaseRepository<Allergy>>();
            #endregion

            #region Services Injection
            services.AddScoped<IAllergyService, AllergyService>();

            #endregion
            return services;
        }
    }
}

