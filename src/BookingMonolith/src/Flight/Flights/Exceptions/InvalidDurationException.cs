using Griffin.Core.Exception;

namespace BookingMonolith.Flight.Flights.Exceptions;

public class InvalidDurationException : DomainException
{
    public InvalidDurationException()
        : base("Duration cannot be negative.")
    {
    }
}