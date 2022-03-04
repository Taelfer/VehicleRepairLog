using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VehicleRepairLog.ApplicationServices.API.Domain.Responses;
using VehicleRepairLog.ApplicationServices.API.Validators;
using VehicleRepairLog.ApplicationServices.MappingProfiles;
using VehicleRepairLog.DataAccess;

namespace VehicleRepairLog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddPartRequestValidator>());

            services.AddTransient<ICommandExecutor, CommandExecutor>();

            services.AddTransient<IQueryExecutor, QueryExecutor>();

            services.AddAutoMapper(typeof(PartProfile).Assembly);

            services.AddMediatR(typeof(ResponseBase<>));

            services.AddDbContext<VehicleProfileStorageContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("VehicleProfileStorageContext")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VehicleRepairLog", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleRepairLog v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
