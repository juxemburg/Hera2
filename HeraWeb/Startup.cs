using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HeraWeb.Services;
using HeraDAL.Contexts;
using Entities.Usuarios;
using HeraServices.Services;
using HeraServices.MessageServices;
using HeraServices.UserServices;
using HeraDAL.DataAcess;
using HeraDAL.Services.FileServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HeraServices.Services.UserServices;
using HeraServices.Services.ApplicationServices;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System.IO;
using HeraServices.Services.UtilServices;
using HeraServices.Services.DesafiosServices;
using HeraServices.ApplicationServices;
using HeraServices.DesafiosServices;

namespace HeraWeb
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
            services.AddCors();

            services.AddDbContext
                <ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HeraDb")));

            services.AddDbContext<NotificationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("HeraDb")));


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 6;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication(config =>
            {
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    SaveSigninToken = true
                };
            });


            //Adding custom services
            services.AddScoped<JwtAuthenticationService>();
            services.AddSingleton<FileManagerService>();
            services.AddScoped<IDataAccess, DataAccess_Sql>();
            services.AddScoped<AccountService>();

            services.AddScoped<UserService>();
            services.AddScoped<ProfesorService>();
            services.AddScoped<ProfesorCursoService>();
            services.AddScoped<EstudianteService>();
            services.AddScoped<DesafioEstudianteService>();
            services.AddScoped<DesafioService>();
            services.AddScoped<ScratchService>();
            services.AddScoped<CursoService>();
            services.AddScoped<ColorService>();
            services.AddScoped<CalificacionDesafioService>();

            services.AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseCors(builder =>
                {
                    builder.AllowCredentials();
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
