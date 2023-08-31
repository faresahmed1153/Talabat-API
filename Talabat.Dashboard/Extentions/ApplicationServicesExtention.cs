using Talabat.Core;
using Talabat.Dashboard.Helpers;
using Talabat.Repository;

namespace Talabat.Dashboard.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }


}
