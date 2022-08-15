using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Quiz.Extensions
{
    public static class PasswordExtentions
    {
        public static IServiceCollection SetPasswordRequirements(this IServiceCollection services)
        {
            services.AddIdentityCore<IdentityUser>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
            });

            return services;
        }
    }
}
