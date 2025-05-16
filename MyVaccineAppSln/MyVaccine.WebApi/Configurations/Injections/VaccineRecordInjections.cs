using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Repositories.Implementations;
using MyVaccine.WebApi.Services;
using MyVaccine.WebApi.Services.Contracts;
using MyVaccine.WebApi.Services.Implementations;

namespace MyVaccine.WebApi.Configurations.Injections
{
    public static class VaccineRecordInjections
    {
        public static IServiceCollection SetVaccineRecordInjection(this IServiceCollection services)
        {
            #region Repositories Injection
            services.AddScoped<IBaseRepository<VaccineRecord>, BaseRepository<VaccineRecord>>();
            #endregion

            #region Services Injection



            services.AddScoped<IVaccineRecordService, VaccineRecordService>();

            #endregion
            return services;
        }
    }
}
