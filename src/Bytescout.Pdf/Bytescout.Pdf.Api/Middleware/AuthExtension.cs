using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Bytescout.Pdf.Api.Middleware
{
    public static class AuthExtension
    {
        public static IServiceCollection AddTokenAuth(this IServiceCollection services, IConfiguration config)
        {
            // TODO: move to Config facade and inject
            var secret = config.GetSection("JwtConfig").GetSection("secret").Value;
            var issuer = config.GetSection("JwtConfig").GetSection("validIssuer").Value;
            var audience = config.GetSection("JwtConfig").GetSection("validAudience").Value;

            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience
                    };
                });

            return services;
        }
    }
}
