using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Builder;
using Quiz.Core;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MyMusic.Api.Extensions
{
    public static class AuthExtensions
    {
        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            JwtSettings jwtSettings)
        {
            services
                .AddAuthorization()
                .AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.Message.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject("401 Not authorized");
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject("403 Not authorized");
                            return context.Response.WriteAsync(result);
                        },
                    };
                });

            return services;
        }
    }
}



//services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//               .AddJwtBearer(o =>
//               {
//                   o.RequireHttpsMetadata = false;
//                   o.SaveToken = false;
//                   o.TokenValidationParameters = new TokenValidationParameters
//                   {
//                       ValidateIssuerSigningKey = true,
//                       ValidateIssuer = true,
//                       ValidateAudience = true,
//                       ValidateLifetime = true,
//                       ClockSkew = TimeSpan.Zero,
//                       ValidIssuer = configuration["JwtSettings:Issuer"],
//                       ValidAudience = configuration["JwtSettings:Audience"],
//                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
//                   };

//                   o.Events = new JwtBearerEvents()
//                   {
//                       OnAuthenticationFailed = c =>
//                       {
//                           c.NoResult();
//                           c.Response.StatusCode = 500;
//                           c.Response.ContentType = "text/plain";
//                           return c.Response.WriteAsync(c.Exception.ToString());
//                       },
//                       OnChallenge = context =>
//                       {
//                           context.HandleResponse();
//                           context.Response.StatusCode = 401;
//                           context.Response.ContentType = "application/json";
//                           var result = JsonConvert.SerializeObject("401 Not authorized");
//                           return context.Response.WriteAsync(result);
//                       },
//                       OnForbidden = context =>
//                       {
//                           context.Response.StatusCode = 403;
//                           context.Response.ContentType = "application/json";
//                           var result = JsonConvert.SerializeObject("403 Not authorized");
//                           return context.Response.WriteAsync(result);
//                       },
//                   };
//               });