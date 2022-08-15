using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quiz.Infrastructure.EF;
using System.Reflection;
using MediatR;
using Quiz.Core.Application.Queries;
using Quiz.Core.Repositories;
using Quiz.Infrastructure.Repositories;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Quiz.Infrastructure.IoC;
using System;
using AutoMapper;
using Quiz.Core.Application.Mapper;
using Quiz.Core.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Quiz.Core;
using MyMusic.Api.Extensions;
using System.Collections.Generic;
using Quiz.Extensions;

namespace Quiz
{
    public class Startup
    {
        const string ApiName = "Quiz API";
        const string ApiVersionName = "v1";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            var connectionString = Configuration.GetConnectionString("QuizDb");
            services.AddTransient<IQuestionsRepository, QuestionRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IQuizRepository, QuizRepository>();
            services.AddTransient<IQuizRoundResultRepository, QuizRoundResultRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();


        //https://blog.angular-university.io/angular-jwt-authentication/
            //var builder = new ContainerBuilder();
            //builder.Populate(services);
            //builder.RegisterModule(new ContainerModule(Configuration));
            //var applicationContainer = builder.Build();

            services.AddMediatR(typeof(GetAllQuestionsQuery).GetTypeInfo().Assembly);
            
            services.AddDbContext<QuizDbContext>(options => options.UseSqlServer(connectionString));
            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<QuizDbContext>().AddDefaultTokenProviders();
            services.AddAuth(jwtSettings);

            services.AddSwaggerGen(setupAction =>
            {

                setupAction.SwaggerDoc(
                    ApiVersionName,
                    new OpenApiInfo()
                    {
                        Title = ApiName,
                        Version = ApiVersionName
                    });

                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT containing userid claim",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                var security =
                    new OpenApiSecurityRequirement
                    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                UnresolvedReference = true
            },
            new List<string>()
        }
                    };
                setupAction.AddSecurityRequirement(security);
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });


            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.SetPasswordRequirements();
            //return new AutofacServiceProvider(applicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseStaticFiles();

            app.ConfigureCustomExceptionMiddleware();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{ApiVersionName}/swagger.json", ApiName));

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
