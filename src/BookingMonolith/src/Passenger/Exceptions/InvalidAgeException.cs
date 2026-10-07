using Griffin.Core.Exception;

namespace BookingMonolith.Passenger.Exceptions;

public class InvalidAgeException : DomainException
{
    public InvalidAgeException() : base("Age Cannot be null or negative")
    {
    }
}