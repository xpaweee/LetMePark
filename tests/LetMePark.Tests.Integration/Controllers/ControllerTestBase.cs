using System.Net.Http.Headers;
using LetMePark.Api.DTO;
using LetMePark.Application.Security;
using LetMePark.Application.Services;
using LetMePark.Infrastructure.Auth;
using LetMePark.Infrastructure.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace LetMePark.Tests.Integration.Controllers;

[Collection(("api"))]
public abstract class ControllerTestBase : IClassFixture<OptionsProvider>
{
    private readonly IAuthenticator _authenticator;
    protected HttpClient Client { get;}

    public ControllerTestBase(OptionsProvider optionsProvider)
    {
        var authOptions = optionsProvider.Get<AuthOptions>("auth");
        _authenticator = new Authenticator(new Clock(), new OptionsWrapper<AuthOptions>(authOptions) );
        var app = new LetMeParkTestApp(ConfigureServices);
        Client = app.Client;
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        
    }

    protected JwtDto Authorize(Guid userId, string role)
    {
        var jwt = _authenticator.CreateToken(userId, role);
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

        return jwt;
    }

}