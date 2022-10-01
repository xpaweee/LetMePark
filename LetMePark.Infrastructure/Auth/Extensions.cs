using System.Text;
using LetMePark.Application.Security;
using LetMePark.Infrastructure.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LetMePark.Infrastructure.Auth;

public static class Extensions
{
    private const string SectionName = "Auth";
    public static IServiceCollection AddAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<AuthOptions>(configuration.GetRequiredSection(SectionName));
        var option = configuration.GetOptions<AuthOptions>(SectionName);

        serviceCollection
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddSingleton<ITokenStorage, HttpContextTokenStorage>()
            .AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Audience = option.Audience;
                x.IncludeErrorDetails = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = option.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(option.SigningKey))
                };
            });

        serviceCollection.AddAuthorization(authorization =>
        {
            authorization.AddPolicy("is-admin", policy =>
            {
                //chain builder
                policy.RequireRole("admin");
            });
        });
        
        return serviceCollection;
    }
}