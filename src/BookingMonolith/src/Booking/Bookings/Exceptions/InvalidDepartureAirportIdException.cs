using Griffin.Core.Exception;

namespace BookingMonolith.Booking.Bookings.Exceptions;

public class InvalidDepartureAirportIdException : DomainException
{
    public InvalidDepartureAirportIdException(Guid departureAirportId)
        : base($"departureAirportId: '{departureAirportId}' is invalid.")
    {
    }
}