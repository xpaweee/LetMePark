﻿namespace LetMePark.Api.Services;

public class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}