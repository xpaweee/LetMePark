using LetMePark.Api.Services;

namespace LetMePark.Application.Services
{
    public class Clock : IClock
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}