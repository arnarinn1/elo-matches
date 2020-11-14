using EloMatches.Api.Application.Middleware.Exceptions;
using EloMatches.Api.Application.Middleware.RequestLogging;
using EloMatches.Api.Extensions;
using EloMatches.Api.Infrastructure.CompositionRoot.Implementations;
using EloMatches.Api.Infrastructure.CompositionRoot.WireUp;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SimpleInjector;

namespace EloMatches.Api
{
    public class Startup
    {
        private readonly Container _container = new Container();

        public Startup(IConfiguration configuration)
        {
            _container.Options.ResolveUnregisteredConcreteTypes = false;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();
            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore().AddControllerActivation();
                options.AddLogging();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            InitializeContainer();

            services.AddHostedService(_ => new MassTransitBusHostedService(_container.GetInstance<IBusControl>()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(_container);

            app.UseRequestLogging(options => options.ShouldLogRequest = context => context.DetermineIfRequestShouldBeLogged("/swagger/"));
            app.UseApiExceptionHandler();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            _container.Verify();
        }

        private void InitializeContainer()
        {
            _container
                .RegisterMediatorPipeline()
                .RegisterPersistence(Configuration)
                .RegisterQueryPipeline(Configuration)
                .RegisterDomainEventProcessors()
                .RegisterIntegrationEventPipeline()
                .RegisterBusControl();
        }
    }
}
