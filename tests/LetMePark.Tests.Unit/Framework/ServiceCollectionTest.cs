using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LetMePark.Tests.Unit.Framework;

public class ServiceCollectionTest
{
    [Fact]
    public void Test()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<IMessanger,Messanger>();
        
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var messangert = serviceProvider.GetService<IMessanger>();
    }

    private interface IMessanger
    {
        void Send();
    }
    
    private class Messanger : IMessanger
    {
        private readonly Guid _id = Guid.NewGuid();
        public void Send()
        {
            Console.WriteLine($"Sending... {_id}");
        }
    }
}