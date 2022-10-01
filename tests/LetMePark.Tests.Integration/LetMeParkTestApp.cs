using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace LetMePark.Tests.Integration;

internal sealed class LetMeParkTestApp : WebApplicationFactory<Program>
{
    public HttpClient Client { get;  }
    
    public LetMeParkTestApp(Action<IServiceCollection> services)
    {
        Client  = base.WithWebHostBuilder(builder =>
        {
            if (services is not null)
            {
                builder.ConfigureServices(services);
            }
            builder.UseEnvironment("test");

        }).CreateClient();
    }

   
}