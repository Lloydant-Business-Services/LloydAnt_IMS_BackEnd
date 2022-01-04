using LiteHR.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using LiteHR.Interface;
using LiteHR.Services;
using Microsoft.AspNetCore.Mvc;
using LiteHR.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using LiteHR.Infrastructure;
using LiteHR.Services.Email;

namespace LiteHR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<HRContext>(opt =>
            //   opt.UseInMemoryDatabase("LiteHR"));


            //services.AddDbContext<HRContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("HR_DB")));

            services.AddDbContext<HRContext>(opt =>
               opt.UseSqlServer(Configuration.GetConnectionString("LloydAnt_HR"), sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                               maxRetryCount: 3,
                               maxRetryDelay: TimeSpan.FromSeconds(30),
                               errorNumbersToAdd: null);
                })
               , ServiceLifetime.Transient
               );

            services.AddControllers();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LloydAnt HR API", Version = "v1" });
            });

            //services.AddControllers().AddNewtonsoftJson(options =>
            //                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //                );
            //services.AddControllersWithViews()
            //        .AddNewtonsoftJson(options =>
            //        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //        );


            services.AddTransient<IStaffService, StaffService>();
            services.AddTransient<IStaffPfaService, StaffPfaService>();
            //services.AddTransient<ILeaveRequestService, LeaveRequestService>();
            services.AddTransient<ITrainingRequestService, TrainingRequestService>();
            //services.AddTransient<IAttendanceService, StaffAttendanceService>();
            services.AddTransient<IJobVacancyService, JobVacancyService>();
            services.AddTransient<IEmailBodyService, EmailBodyService>();
            //services.AddTransient<IApplicationFormService, ApplicationFormService>();
            services.AddTransient<ISalaryGradingSystem, SalaryGradingSystemService>();
            services.AddTransient<ISalaryExtraEarning, SalaryExtraEarningService>();
            services.AddTransient<IStaffRetirement, StaffRetirementService>();
            services.AddTransient<ILeaveTypeService, LeaveTypeService>();
            services.AddTransient<IStaffLeaveRequestService, StaffLeaveRequestService>();
            //services.AddTransient<ILeaveRequestManagementService, LeaveRequestManagementService>();
            services.AddTransient<IStaffDocumentService, StaffDocumentService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IChangeOfNameService, ChangeOfNameService>();
            services.AddTransient<IDepartmentTransferService, DepartmentTransferService>();
            services.AddTransient<IStaffNominalRollService, StaffNominalRollService>();
            services.AddTransient<IStaffPostingService, StaffPostingService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IPfaService, PfaService>();
            services.AddTransient<IStaffAdditionalInfoService, StaffAdditionalInfoService>();
            services.AddTransient<IServicomService, ServicomService>();
            //services.AddTransient<IInstituitionMemorandumService, InstituitionMemorandumService>();
            //services.AddTransient<IInstituitionMemorandumTargetService, InstituitionMemorandumTargetService>();
            services.AddTransient<IForeignVisitationService, ForeignVisitationService>();
            //services.AddTransient<IMailingService, MailingService>();
            services.AddTransient<IPersonEducationService, PersonEducationService>();
            //services.AddTransient<IMailingDeskChain, MailingDeskChain>();


            services.AddScoped<IFileUpload, FileUpload>();

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            services
            .AddFluentEmail("support@elearn.ng")
            .AddMailGunSender(
                Configuration.GetValue<string>("MailGun:domain"),
                Configuration.GetValue<string>("MailGun:apiKey"),
                FluentEmail.Mailgun.MailGunRegion.EU
                )
            .AddRazorRenderer();


            services.AddCors();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(MyAllowSpecificOrigins,
            //    builder =>
            //    {
            //        builder.WithOrigins("http://localhost",
            //                            "http://localhost:8000",
            //                            "http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
            //    });
            //});
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetService<HRContext>();
            //    context.Database.Migrate();
            //}
            //Remove in production!!!!
            //UpdateDatabase(app);
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = "/Resources"

            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Shows UseCors with CorsPolicyBuilder.
            //app.UseCors("MyAllowSpecificOrigins");
            app.UseCors(
                options => options.SetIsOriginAllowed(x => _ = true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                //.WithOrigins(MyAllowSpecificOrigins)
            );
            //app.UseCors(options => options.AllowAnyOrigin());

            //app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
            //    RequestPath = new PathString("/Resources")
            //});

            app.UseAuthentication();

            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "LloydAnt_IMS API v1");
                //c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<HRContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
