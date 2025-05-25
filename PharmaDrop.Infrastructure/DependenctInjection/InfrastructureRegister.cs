using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PharmaDrop.Aplication.Contract.Interfaces;
using PharmaDrop.Application.Contract.Interfaces;
using PharmaDrop.Application.Contract.Services;
using PharmaDrop.Infrastructure.Data;
using PharmaDrop.Infrastructure.Implementition.Repositories;
using PharmaDrop.Infrastructure.Implementition.Services;
using StackExchange.Redis;
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
            services.AddTransient<ISendEmailServices, SendEmailServices>();

            services.AddSingleton<IConnectionMultiplexer>(r =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(config);
            });

            services.AddScoped<IOtpRepository , OtpRepository>();

            return services;
        }
    }
}
