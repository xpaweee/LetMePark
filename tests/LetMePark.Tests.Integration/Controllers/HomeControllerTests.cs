using System.Net;
using Shouldly;
using Xunit;

namespace LetMePark.Tests.Integration.Controllers;

public class HomeControllerTests : ControllerTestBase
{
    public HomeControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
    }
    
    [Fact]
    public async Task get_base_endpoint_should_return_200_ok_status_code_and_api_name()
    {
        var response = await Client.GetAsync("/");
       
        var content = await response.Content.ReadAsStringAsync();
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        content.ShouldBe("test");
    }

  
}