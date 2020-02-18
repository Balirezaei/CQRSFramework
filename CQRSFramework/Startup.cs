using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CQRSFramework.Facade.Query;
using CQRSFramework.QueryModel;
using CQRSFramework.Utility;
using Framework.ApplicationService.Contract;
using Framework.ApplicationService.Contract.User;
using Framework.ApplicationService.UserCommandHandler;
using Framework.ApplicationService.UserQueryHandler;
using Framework.Core;
using Framework.Core.CommandBus;
using Framework.Core.CommandHandlerDecorator;
using Framework.Core.QueryHandler;
using Framework.Persistense.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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

            services.AddScoped<IBaseCommandHandler<CreateUserCommand, CreateUserCommandResult>, CreateUserHandler>();
            services.AddScoped<IBaseCommandHandler<DeactiveUserCommand,Nothing>, DeactiveUserHandler>();
            services.AddScoped<ITokenService, TokenService>();
            
            services.AddScoped<IErrorHandling, LogManagement.LogErrorHandle>();
            services.AddScoped<ILogManagement, LogManagement.LogManagement>();
            
            services.AddScoped(typeof(LoggingHandlerDecorator<,>));
            services.AddScoped(typeof(CatchErrorCommandHandlerDecorator<,>));
            services.AddScoped(typeof(AuthorizeCommandHandlerDecorator<,>));
            
            //services.AddScoped<IHttpContextAccessor>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<CurrentUser>(provider =>
            {
                var claims = provider.GetService<IHttpContextAccessor>().HttpContext.User.Claims;
                var userIdClaim = claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                string userId="";
                if (userIdClaim != null)
                {
                    userId = userIdClaim.Value;
                }
                var userNameClaim = claims.SingleOrDefault(c => c.Type == ClaimTypes.Name);
                string userName="";
                if (userNameClaim != null)
                {
                    userName = userNameClaim.Value;
                }
                var currentUser = new CurrentUser();
                currentUser.UserId = userId;
                currentUser.UserName = userName;
                return currentUser;
            });


            services.AddControllers();
            //            services.AddAuthentication(option =>
            //            {
            //                option.DefaultScheme = "bearer";
            //                
            //            });


            var key = Encoding.ASCII.GetBytes("serverSigningPassword");
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = "bearer";
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    x.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "bearer";
            //})

            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateAudience = false,
            //        ValidateIssuer = false,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["serverSigningPassword"])),
            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero //the default for this setting is 5 minutes
            //    };
            //    options.Events = new JwtBearerEvents
            //    {
            //        OnAuthenticationFailed = context =>
            //        {
            //            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            //            {
            //                context.Response.Headers.Add("Token-Expired", "true");
            //            }
            //            return Task.CompletedTask;
            //        }
            //    };
            //});

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
