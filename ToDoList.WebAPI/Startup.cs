using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using ToDoList.BLL;
using ToDoList.DAL.Concreate.Entityframework.Context;
using ToDoList.Entity.DTO.Mail;
using ToDoList.Interface;
using ToDoList.Jobs.Schedules;

namespace ToDoList.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionString = Configuration["ConnectionStrings:ToDoListConn"];
            services.AddDbContext<ToDoListDbContext>(option => option.UseSqlServer(connectionString));

            var hangfireConnectionString = Configuration["ConnectionStrings:HangfireConn"];
            services.AddHangfire(config =>
            {
                var option = new SqlServerStorageOptions
                {
                    PrepareSchemaIfNecessary = true,
                    QueuePollInterval = TimeSpan.FromMinutes(5),
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                };

                config.UseSqlServerStorage(hangfireConnectionString, option)
                      .WithJobExpirationTimeout(TimeSpan.FromHours(6));

            });

            services.AddSwaggerDocument();

            services.AddScoped<IMailService, MailManager>();

            services.Configure<SmtpConfigDto>(Configuration.GetSection("SmtpConfig"));

        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ToDoListDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            db.Database.EnsureCreated();

            app.UseRouting();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJobs.SendAllTasksDaily();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
