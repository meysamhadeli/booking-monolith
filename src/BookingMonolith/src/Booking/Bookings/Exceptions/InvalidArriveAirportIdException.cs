using Griffin.Core.Exception;

namespace BookingMonolith.Booking.Bookings.Exceptions;

public class InvalidArriveAirportIdException : DomainException
{
    public InvalidArriveAirportIdException(Guid arriveAirportId)
        : base($"arriveAirportId: '{arriveAirportId}' is invalid.")
    {
    }
}