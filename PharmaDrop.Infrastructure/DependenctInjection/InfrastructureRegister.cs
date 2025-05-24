using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Infrastructure.Data;
using PharmaDrop.Infrastructure.Implementition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Infrastructure.DependenctInjection
{
    public static class  InfrastructureRegister
    {
        public static IServiceCollection InfraRegister (this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<ApplicationDbContext>(s => s.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitofWork, UnitofWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
