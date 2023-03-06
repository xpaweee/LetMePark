using LetMePark.Core.ValueObjects;

namespace LetMePark.Core.Exceptions;

public class NoReservationPolicyFoundException : CustomException
{
    private readonly JobTitle _jobTitle;

    public NoReservationPolicyFoundException(JobTitle jobTitle) : base($"No reservation policy for {jobTitle.Value} has been found")
    {
        _jobTitle = jobTitle;
    }
}