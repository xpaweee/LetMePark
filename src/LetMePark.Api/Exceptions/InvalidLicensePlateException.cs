namespace LetMePark.Api.Exceptions;

public sealed class InvalidLicensePlateException : CustomException
{
    public string LicensePlate { get; }

    public InvalidLicensePlateException(string licensePlate) : base($"Invalid license plate {licensePlate}")
    {
        LicensePlate = licensePlate;
    }
}