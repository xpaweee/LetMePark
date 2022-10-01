using System.Net;
using System.Net.Http.Json;
using LetMePark.Api.Commands;
using LetMePark.Api.DTO;
using LetMePark.Application.Services;
using LetMePark.Core.Entities;
using LetMePark.Core.Repository;
using LetMePark.Core.ValueObjects;
using LetMePark.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace LetMePark.Tests.Integration.Controllers;

public class UsersControllerTests : ControllerTestBase, IDisposable
{
    private readonly TestDatabase _testDb;
    private  IUserRepository _userRepository;

    public UsersControllerTests(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDb = new TestDatabase();
    }
    
    [Fact]
    public async Task post_users_should_return_created_201_status_code()
    {
        var command = new SignUp(Guid.Empty, "test1@test.com","test1", "secret", "John Doe", Role.User());

        var response = await Client.PostAsJsonAsync("users", command);
        
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ShouldNotBeNull();
    }

    [Fact]
    public async Task post_sign_in_should_return_200_ok_status_code_and_jwt()
    {
        var user = await CreateUserAsync();
        
        var command = new SignIn("test@test.com", Password);
        var response = await Client.PostAsJsonAsync("users/sign-in", command);
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();
        jwt.ShouldNotBeNull();
        jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();

    }

    [Fact]
    public async Task get_users_me_should_return_ok_200_status_code_and_user()
    {
        var user = await CreateUserAsync();
        Authorize(user.Id, user.Role);
        var userDto = await Client.GetFromJsonAsync<UserDto>("users/me");
        userDto.ShouldNotBeNull();
        userDto.Id.ShouldBe(user.Id.Value);
    }

    private const string Password = "secret";
    private async Task<User> CreateUserAsync()
    {
        var clock = new Clock();
        var passwordManager = new PasswordManager(new PasswordHasher<User>());
        var user = new User(Guid.NewGuid(), "test@test.com", "test", passwordManager.Secure(Password), "John Doe",
            Role.User(), clock.Current());
        await _userRepository.AddAsync(user);
        // await _testDb.DbContext.Users.AddAsync(user);
        // await _testDb.DbContext.SaveChangesAsync();

        return user;
    }


    protected override void ConfigureServices(IServiceCollection services)
    {
        _userRepository = new TestUserRepository();
        services.AddSingleton(_userRepository);
    }

    public void Dispose()
    {
        _testDb.Dispose();
    }
}