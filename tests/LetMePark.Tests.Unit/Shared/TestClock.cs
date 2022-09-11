using LetMePark.Api.Services;

namespace LetMePark.Tests.Unit.Shared;

public class TestClock : IClock
{
    public DateTime Current()
    {
        return new DateTime(2022, 08, 11);
    }
}