using Autofac;
using Autofac.Extras.DynamicProxy;
using HelloCore.Common;
using HelloCore.Interface.Manually;
using HelloCore.Repository;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebExtension;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HelloCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            CommonConstant.Configuration = configuration;

            //加载log4net日志配置文件
            CommonConstant.LoggerRepository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(CommonConstant.LoggerRepository, new FileInfo("log4net.config"));
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// IoC
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to AddAutofac in the Program.Main
            // method or this won't be called.

            var assemblys = Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.dll", SearchOption.AllDirectories)
                         .Where(t => t.Replace(".dll", "").EndsWith("Manager")
                                || t.Replace(".dll", "").EndsWith("Repository")
                                || t.Replace(".dll", "").EndsWith("Interface"))
                         .Select(Assembly.LoadFrom);


            builder.RegisterType<ContextUnitOfWork>().As<IContextUnitOfWork>();
            builder.RegisterType<HelloCoreDataContext>().Named<IDbContext>("HelloCoreDB");
            builder.RegisterType<UnitOfWorkInterceptionBehaviorBase>();
            

            builder.RegisterAssemblyTypes(assemblys.ToArray())
             .AsImplementedInterfaces()
             .InstancePerLifetimeScope()
             .EnableInterfaceInterceptors()
             .InterceptedBy(typeof(UnitOfWorkInterceptionBehaviorBase));

            builder.RegisterType<HttpContextAccessor>()
                 .As<IHttpContextAccessor>()
                 .InstancePerLifetimeScope();

            builder.RegisterType<AuthenticationEvents>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                // if don't want UTC format.
                .AddJsonOptions(option => option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss");
            
            services.AddCors();
            services.SetupCookie();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = "http://localhost:51846/Login/index",
                    Scopes = new Dictionary<string, string>
                    {
                        { "read", "Access read operations" },
                        { "write", "Access write operations" }
                    }
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", new[] { "readAccess", "writeAccess" } }
                });
            });
            
            services.AddDbContextPool<HelloCoreDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("sqlserverdb")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();

                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true
                //});
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRequestCookie();


            // invoke before MVC
            app.UseCors(policyBuilder => policyBuilder.WithOrigins("http://localhost:8080").AllowAnyHeader().AllowAnyMethod());

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<HelloCoreDataContext>();
                context.Database.EnsureCreated();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback", 
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
        

    }
}
