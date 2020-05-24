using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyPersonalPlannerBackend.Handler;
using MyPersonalPlannerBackend.Model;
using MyPersonalPlannerBackend.Repository;
using MyPersonalPlannerBackend.Repository.IRepository;
using MyPersonalPlannerBackend.Service;
using MyPersonalPlannerBackend.Service.IService;
using AuthenticationService = MyPersonalPlannerBackend.Service.AuthenticationService;
using IAuthenticationService = MyPersonalPlannerBackend.Service.IService.IAuthenticationService;

namespace MyPersonalPlannerBackend
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
            var dbhost = Configuration["dbhost"];
            var dbname = Configuration["dbname"];
            var dbuser = Configuration["dbuser"];
            var dbpassword = Configuration["dbpassword"];

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddControllers();

            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<MariaDBContext>(options => {
                options.UseMySql("Server=" + dbhost + "; Database=" + dbname + ";User=" + dbuser + ";Password=" + dbpassword);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!!");
                });
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}
