using Griffin.Core.Exception;

namespace BookingMonolith.Booking.Bookings.Exceptions;

public class InvalidPassengerNameException : DomainException
{
    public InvalidPassengerNameException(string passengerName)
        : base($"Passenger Name: '{passengerName}' is invalid.")
    {
    }
}