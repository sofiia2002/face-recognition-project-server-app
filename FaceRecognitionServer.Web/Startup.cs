namespace FaceRecognitionServer.Web
{
    using FaceRecognitionServer.Web.Application.Commands;
    using Microsoft.AspNetCore.Builder;
    using FaceRecognitionServer.Infrastructure.Repositories;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using FaceRecognitionServer.Web.Application.Queries;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FaceRecognitionServer.Web", Version = "v1" });
            });
            services.AddSingleton<IPersonRepository, PersonRepository>();
            services.AddTransient<IPersonQueriesHandler, PersonQueriesHandler>();
            services.AddTransient<ICommandHandler<AddPersonCommand>, AddPersonCommandHandler>();
            services.AddTransient<ICommandHandler<SetPersonDetailsCommand>, SetPersonDetailsCommandHandler>();
            services.AddTransient<ICommandHandler<SetPersonNameCommand>, SetPersonNameCommandHandler>();
            services.AddTransient<ICommandHandler<SetPersonTypeCommand>, SetPersonTypeCommandHandler>();
            services.AddTransient<ICommandHandler<SetPersonIdentificatorCommand>, SetPersonIdentificatorCommandHandler>();

            services.AddSingleton<IStatisticalDataRepository, StatisticalDataRepository>();
            services.AddTransient<IStatisticalDataQueriesHandler, StatisticalDataQueriesHandler>();
            services.AddTransient<ICommandHandler<AddStatisticalDataCommand>, AddStatisticalDataCommandHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FaceRecognitionServer.Web v1"));
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
