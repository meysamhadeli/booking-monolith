using Griffin.Core.Exception;

namespace BookingMonolith.Passenger.Passengers.Exceptions;

public class InvalidAgeException : DomainException
{
    public InvalidAgeException() : base("Age Cannot be null or negative")
    {
    }
}