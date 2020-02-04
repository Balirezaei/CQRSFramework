using System.Collections.Generic;
using CQRSFramework.Facade.Query;
using CQRSFramework.QueryModel;
using Framework.ApplicationService.Contract;
using Framework.ApplicationService.Contract.User;
using Framework.ApplicationService.UserCommandHandler;
using Framework.ApplicationService.UserQueryHandler;
using Framework.Core;
using Framework.Core.LogCommandHandler;
using Framework.Persistense.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CQRSFramework
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<MainContext>(options =>
             options.UseInMemoryDatabase(databaseName: "MainContext"));


            services.AddDbContext<MainQueryModel>(options =>
                options.UseInMemoryDatabase(databaseName: "MainQueryModel"));

            //var sp = services.BuildServiceProvider();

            //// Resolve the services from the service provider
            //var fooService = sp.GetService<MainContext>();

            services.AddSingleton<ICommandBus, CommandBus>();

            services.AddScoped<IQueryProcessor, QueryProcessor>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserQueryFacade, UserQueryFacade>();
            services.AddScoped<IBaseQueryHandler<PagingContract, List<UserDto>>, GetUserQueryHandler>();


            services.AddScoped<IBaseCommandHandler<CreateUserCommand>, CreateUserHandler>();
            services.AddScoped<IBaseCommandHandler<DeactiveUserCommand>, DeactiveUserHandler>();

            services.AddScoped(typeof(LoggingHandler<>));

            //services.AddScoped< typeof(IBaseCommandHandler<>)> ();
            //services.AddSingleton(typeof(IBaseCommandHandler<>)));
            //typeof(ICommandHandler<>)

            //services.AddScoped<IBaseCommandHandler<CreateUserCommand>>(n => new LoggingHandler<CreateUserCommand>(new CreateUserHandler(fooService)));


            //services.AddScoped<IAuthorRepository, AuthorRepository>();
            //services.Decorate<IAuthorRepository, CachedAuthorRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
