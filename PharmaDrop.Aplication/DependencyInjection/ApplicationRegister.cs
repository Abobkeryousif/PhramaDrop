using Microsoft.Extensions.DependencyInjection;
using PharmaDrop.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Aplication.DependencyInjection
{
    public static class ApplicationRegister
    {
        public static IServiceCollection AppRegister(this IServiceCollection services) 
        {
            services.AddMediatR(m=> m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
            services.AddAutoMapper(typeof(AutoMapping).Assembly);
            return services;
        }
    }
}
