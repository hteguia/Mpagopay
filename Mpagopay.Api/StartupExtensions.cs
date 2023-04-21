using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Mpagopay.Api.Middleware;
using Mpagopay.Api.Services;
using Mpagopay.Api.Tools;
using Mpagopay.Application;
using Mpagopay.Application.Contrats;
using Mpagopay.Identity;
using Mpagopay.Identity.Models;
using Mpagopay.Identity.Seed;
using Mpagopay.Infrastructure;
using Mpagopay.Persistence;
using Mpagopay.Persistence.Seed;

namespace Mpagopay.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigurationService(this WebApplicationBuilder builder)
        {
            AddSwagger(builder.Services);

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            return builder.Build();

        }

        public static WebApplication ConfigurationPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MpaGoPay API");
                });
            }

            app.UseHttpsRedirection();

            //app.UseRouting();

            app.UseCustomExceptionHandler();

            app.UseCors("Open");

            app.MapControllers();

            return app;
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                            Enter 'Bearer [space] and then your token in the new input below.
                            \r\n\rExamble 'Bearer 123abcdef'",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "MpaGoPay API",
                });

                c.OperationFilter<FileResultContentTypeOperationFilter>();
            });
        }

        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<MpagopayDbContext>();
                if(context != null)
                {
                    //await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                    await CreateFirstData.SeedAsync(context);
                }

				var identityContext = scope.ServiceProvider.GetService<MpagopayIdentityDbContext>();
				if (identityContext != null)
				{
					//await identityContext.Database.EnsureDeletedAsync();
					await identityContext.Database.MigrateAsync();

                    var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                    if(userManager != null)
                    {
                        await CreateFisrtUser.SeedAsync(userManager);
                    }
                }
			}
            catch(Exception ex)
            {

            }
        }
    }
}
