using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using VehicleRepairLog.Application.Authentication;
using VehicleRepairLog.Application.Features.Parts;
using VehicleRepairLog.Application.Features.Users;
using VehicleRepairLog.Application.MappingProfiles;
using VehicleRepairLog.Infrastructure;
using VehicleRepairLog.Infrastructure.Entities;
using VehicleRepairLog.Infrastructure.Repositories;
using VehicleRepairLog.Middleware;

namespace VehicleRepairLog.API
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
            // Same-origin policy.
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowBlazorOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5151",
                            "https://localhost:7151")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddScoped<ExceptionHandlingMiddleware>();

            services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);

            services.AddMvcCore()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddPartCommandValidator>());

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddAutoMapper(typeof(PartMappingProfile).Assembly);

            services.AddHttpContextAccessor();

            services.AddTransient<IJwtAuth, JwtAuth>();
            services.AddTransient<IUserService, UserService>();

            services.AddScoped<IPartRepository, PartRepository>();
            services.AddScoped<IRepairRepository, RepairRepository>();

            services.AddDbContext<VehicleProfileStorageContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("VehicleProfileStorageContext")));

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

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowBlazorOrigin");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
