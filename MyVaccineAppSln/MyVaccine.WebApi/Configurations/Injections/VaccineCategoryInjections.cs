using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Implementations;
using MyVaccine.WebApi.Services;
using MyVaccine.WebApi.Repositories.Implementations;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Configurations.Injections
{
    public static class VaccineCategoryInjections
    {
        public static IServiceCollection SetVaccineCategoryInjection(this IServiceCollection services)
        {
            #region Repositories Injection
            services.AddScoped<IBaseRepository<VaccineCategory>, BaseRepository<VaccineCategory>>();
            #endregion

            #region Services Injection

            services.AddScoped<IVaccineCategoryService, VaccineCategoryService>();

            #endregion
            return services;
        }
    }
}
