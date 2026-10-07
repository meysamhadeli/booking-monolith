using Griffin.Core.Exception;

namespace BookingMonolith.Booking.Bookings.Exceptions;

public class InvalidAircraftIdException : DomainException
{
    public InvalidAircraftIdException(Guid aircraftId)
        : base($"aircraftId: '{aircraftId}' is invalid.")
    {
    }
}