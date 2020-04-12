using AutoMapper;
using AutoMapper.EquivalencyExpression;
using EntityFrameworkCore.GenericRepository.Abstractions;
using EntityFrameworkCore.GenericRepository.Samples.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EntityFrameworkCore.GenericRepository.Samples.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddDbContext<DbContext, SampleDbContext>(ServiceLifetime.Transient);
            
            services.AddTransient<DbContext, SampleDbContext>();
            services.AddTransient(typeof(IEntityRepository<,>), typeof(EntityRepository<,>));

            services.AddAutoMapper(config => config.AddCollectionMappers(), typeof(Startup).Assembly);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting()
                .UseEndpoints(builder => builder.MapControllers());
        }
    }
}