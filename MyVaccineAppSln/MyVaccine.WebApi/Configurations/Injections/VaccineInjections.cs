using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Repositories.Implementations;
using MyVaccine.WebApi.Services.Contracts;
using MyVaccine.WebApi.Services.Implementations;
using MyVaccine.WebApi.Services;

namespace MyVaccine.WebApi.Configurations.Injections
{
    public static class VaccineInjections
    {
        public static IServiceCollection SetVaccineInjection(this IServiceCollection services)
        {
            #region Repositories Injection
            services.AddScoped<IBaseRepository<Vaccine>, BaseRepository<Vaccine>>();
            #endregion

            #region Services Injection
            services.AddScoped<IVaccineService, VaccineService>();



            #endregion
            return services;
        }
    }
}
