using Griffin.Core.Exception;

namespace BookingMonolith.Flight.Flights.Exceptions;

public class InvalidPriceException : DomainException
{
    public InvalidPriceException()
        : base($"Price Cannot be negative.")
    {
    }
}