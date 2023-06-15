using Microservices.IDP.Infrastructure.Domains;
using Microservices.IDP.Infrastructure.Repositories;
using Microservices.IDP.Presentation;
using Microservices.IDP.Services.EmailService;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Microservices.IDP.Extensions;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();

        builder.Services.AddConfigurationSettings(builder.Configuration);

        builder.Services.AddAutoMapper(typeof(Program));


        builder.Services.AddScoped<IEmailSender, SmtpMailService>();

        builder.Services.ConfigureIdentity(builder.Configuration);

        builder.Services.ConfigureIdentityServer(builder.Configuration);

        builder.Services.ConfigureCookiePolicy();

        builder.Services.ConfigureCors();

        builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

        builder.Services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

        builder.Services.AddScoped<IPermissionRepository,PermissionRepository>();

        builder.Services.AddControllers(configure =>
        {
            configure.RespectBrowserAcceptHeader = true;
            configure.ReturnHttpNotAcceptable = true;
            configure.Filters.Add(new ProducesAttribute("application/json", "text/plain", "text/json"));

        }).AddApplicationPart(typeof(AssemblyReference).Assembly);

        builder.Services.ConfigureAuthentication();

        builder.Services.ConfigureAuthorization();

        builder.Services.ConfigureSwagger(builder.Configuration);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add a UI
        app.UseStaticFiles();

        app.UseCors("CorsPolicy");

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.OAuthClientId("tedu_microservices_swagger");
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API");
            c.DisplayRequestDuration();
        });

        app.UseRouting();

        app.UseMiddleware<ErrorWrappingMiddleware>();

        app.UseCookiePolicy();

        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute().RequireAuthorization("Bearer");
            endpoints.MapRazorPages().RequireAuthorization();

        });




        return app;
    }
}
